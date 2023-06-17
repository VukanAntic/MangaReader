import psycopg2
import psycopg2.extras

from urllib.parse import urlparse

import os
import requests
import time


base_url = "https://api.mangadex.org"
languages = ["en"]

# scripts for creating the dbs
create_manga_table_script =     ''' CREATE TABLE IF NOT EXISTS manga (
                                    id             varchar(100) PRIMARY KEY,
                                    title          varchar(100) NOT NULL,
                                    description    varchar(3000),
                                    status         varchar(100),
                                    cover_art      varchar(200),
                                    content_rating boolean,
                                    author_id      varchar(100) NOT NULL,
                                    FOREIGN KEY (author_id) REFERENCES author(id)); '''

insert_manga_script = 'INSERT INTO manga (id, title, description, status, cover_art, content_rating, author_id) VALUES (%s, %s, %s, %s, %s, %s, %s)'

create_manga_genre_table_script =   ''' CREATE TABLE IF NOT EXISTS manga_genre (
                                        id_manga  varchar(100),
                                        id_genre  varchar(100),
                                        PRIMARY KEY (id_manga, id_genre),
                                        FOREIGN KEY (id_manga) REFERENCES manga(id),
                                        FOREIGN KEY (id_genre) REFERENCES genre(id)); '''

insert_manga_genre_script = 'INSERT INTO manga_genre (id_manga, id_genre) VALUES (%s, %s)'


create_genre_table_script =     ''' CREATE TABLE IF NOT EXISTS genre (
                                    id      varchar(100) PRIMARY KEY,
                                    name    varchar(100) NOT NULL); '''

insert_genre_script = '''INSERT INTO genre (id, name) VALUES (%s, %s)
                        ON CONFLICT DO NOTHING'''


create_chapter_table_script =   ''' CREATE TABLE IF NOT EXISTS chapter (
                                    id             varchar(100) PRIMARY KEY,
                                    id_manga       varchar(100),
                                    chapter_number varchar(10) NOT NULL,
                                    title          varchar(100) NOT NULL,
                                    FOREIGN KEY (id_manga) REFERENCES manga(id)); '''

insert_chapter_script = 'INSERT INTO chapter (id, id_manga, chapter_number, title) VALUES (%s, %s, %s, %s)'


create_page_table_script =   ''' CREATE TABLE IF NOT EXISTS page (
                                    chapter_id     varchar(100),
                                    page_number    int NOT NULL,
                                    image_link     varchar(200) NOT NULL,
                                    PRIMARY KEY (chapter_id, page_number),
                                    FOREIGN KEY (chapter_id) REFERENCES chapter(id)); '''

insert_page_script = 'INSERT INTO page (chapter_id, page_number, image_link) VALUES (%s, %s, %s)'


create_author_table_script =   '''  CREATE TABLE IF NOT EXISTS author (
                                    id             varchar(100) PRIMARY KEY,
                                    name           varchar(100) NOT NULL,
                                    biography      varchar(3000)); '''

insert_author_script = '''INSERT INTO author (id, name, biography) VALUES (%s, %s, %s)
                          ON CONFLICT DO NOTHING'''


def find_given_relationship(manga, relationship_type):
    return next(item for item in manga["relationships"] if item["type"] == relationship_type)

def download_chapter(chapter_id, chapter_number, manga_id, cursor):
    
    r = requests.get(f"{base_url}/chapter/{chapter_id}")
    chapter_title = r.json()["data"]["attributes"]["title"]
    cur.execute(insert_chapter_script, (chapter_id, manga_id, chapter_number, chapter_title))
    #print(chapter_id) 
    #print(chapter_number)
    #print(chapter_title)
    
    print(f"{base_url}/at-home/server/{chapter_id}")
    r = requests.get(f"{base_url}/at-home/server/{chapter_id}")
    r_json = r.json()
        
    if "errors" in r_json:
        print(r_json)
        # rate exceeded
        if r_json["errors"][0]["status"] == 429:
            time.sleep(60)
            r = requests.get(f"{base_url}/at-home/server/{chapter_id}")
            r_json = r.json()
        else:
            print("For some reason, cannot proccess given chapter.")
            return
        
    host = r_json["baseUrl"]
    chapter_hash = r_json["chapter"]["hash"]
    data = r_json["chapter"]["data"]
    data_saver = r_json["chapter"]["dataSaver"]
        
    for (page_number, page) in enumerate(data):
        #page_number = int(page[0:page.find("-")])
        page_url = f"{host}/data/{chapter_hash}/{page}"
        print(page_number)
        print(page_url)
        cur.execute(insert_page_script, (chapter_id, page_number, page_url))

    print(f"Downloaded {len(data)} pages.")


with open("url.txt", "r") as f:
    url_to_database = f.read()


result = urlparse(url_to_database)

username = result.username
password = result.password
database = result.path[1:]
hostname = result.hostname
port = result.port

try:
    with psycopg2.connect(
                host = hostname,
                dbname = database,
                user = username,
                password = password,
                port = port) as conn:
        print("Started working with the database...")

        titles = ["Claymore", "Chainsaw man", "Demon slayer", "Vagabond", "Pluto", "20th Century Boys",
                   "Vinland saga", "Berserk", "Fire punch"]
        
        with conn.cursor(cursor_factory=psycopg2.extras.DictCursor) as cur:
            
            #cur.execute('DROP TABLE IF EXISTS manga')

            cur.execute(create_author_table_script)
            cur.execute(create_manga_table_script)
            cur.execute(create_genre_table_script)
            cur.execute(create_manga_genre_table_script)
            cur.execute(create_chapter_table_script)
            cur.execute(create_page_table_script)

            
            for title in titles:
                
                print("Started working on: " + title)
                # POCETAK FUNCKIJE

                # Searching for the id of the given manga
                res = requests.get(
                    f"{base_url}/manga",
                    params={"title": title, "includes[]": ["author", "manga", "cover_art"]}
                )
                    
                if not "data" in res.json():
                    continue
                
                    
                manga_data = res.json()["data"][0]

                # basic manga info
                manga_id = manga_data["id"]
                if not "en" in manga_data["attributes"]["title"]:
                    title = manga_data["attributes"]["title"]
                else:
                    title = manga_data["attributes"]["title"]["en"]
                    
                if not "en" in manga_data["attributes"]["description"]:
                    description = manga_data["attributes"]["description"]
                else:
                    description = manga_data["attributes"]["description"]["en"]
                        
                status = manga_data["attributes"]["status"]
                public_demographic = manga_data["attributes"]["publicationDemographic"]
                if (public_demographic == "shounen" or public_demographic == "shoujo"):
                    public_demographic = True
                else:
                    public_demographic = False

                # author
                author_data = find_given_relationship(manga_data, "author")
                author_id = author_data["id"]
                author_name = author_data["attributes"]["name"]
                if "biography" in author_data["attributes"] and "en" in author_data["attributes"]["biography"]:
                    author_biography = author_data["attributes"]["biography"]["en"]
                else:
                    author_biography = None

                # Adding the author to the table
                cur.execute(insert_author_script, (author_id, author_name, author_biography))

                # cover art
                cover_art_data = find_given_relationship(manga_data, "cover_art")
                filename = cover_art_data["attributes"]["fileName"]
                cover_art_url = f"https://uploads.mangadex.org/covers/{manga_id}/{filename}"

                # Adding the manga to tbe table
                cur.execute(insert_manga_script, (manga_id, title, description, status, cover_art_url, public_demographic, author_id))
                
                # genres
                tags = manga_data["attributes"]["tags"]
                for tag in tags:
                    if tag["attributes"]["group"] == "genre":
                        genre_id = tag["id"]
                        genre_name = tag["attributes"]["name"]["en"]
                        cur.execute(insert_genre_script, (genre_id, genre_name))
                        cur.execute(insert_manga_genre_script, (manga_id, genre_id))
                

                    
                res = requests.get(
                    f"{base_url}/manga/{manga_id}/aggregate",
                    params={"translatedLanguage[]": languages},
                )
                        
                volumes = res.json()["volumes"]
                # dict (chapter_number -> chapter_id)
                chapters = []
                
                for (volume_number, volume_data) in volumes.items():
                    print(volume_number)
                    print("=====================================================")
                    #print(volume_data)
                    
                    chapters = volume_data["chapters"]
                    for (chapter_number, chapter_data) in chapters.items():
                        chapter_id = chapter_data["id"]
                        download_chapter(chapter_id, chapter_number, manga_id, cur)

                    print("Finished: " + title)
                    # KRAJ FUNKCIJE

            
        print("Finished working with the database!!!")

except Exception as error:
    print("Something went wrong...")
    print(error)
finally:
    if conn is not None:
        conn.close()