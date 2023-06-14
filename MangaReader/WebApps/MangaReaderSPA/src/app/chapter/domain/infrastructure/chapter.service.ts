import { adjacentChapter } from './../../../manga-product/domain/models/adjacentChapter.model';
import { Chapter } from './../../../manga-product/domain/models/chapter.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, switchMap, take } from 'rxjs';
import { Page } from 'src/app/manga-product/domain/models/page.model';

@Injectable({
  providedIn: 'root'
})
export class ChapterService {

  constructor(private  httpClient: HttpClient) { }

  // TODO: NEED TO ADD A REQUEST TO GET ALL THE DATA FOR THE CHAPTERID (BACKEND)

  
  public getPages(chapterId : string): Observable<[Page]> {
    return this.httpClient.get<[Page]>(
      "http://localhost:8000/api/Chapter/GetPagesForChapterId/" + chapterId
    ); 
  }

  public getChapterInfo(chapterId : string): Observable<Chapter> {
    return this.httpClient.get<Chapter>(
      "http://localhost:8000/api/Chapter/GetChapterById/" + chapterId
    ); 
  }

  public getAdjacentChapters(chapterId : string): Observable<adjacentChapter> {
    return this.httpClient.get<adjacentChapter>(
      "http://localhost:8000/api/Chapter/GetAdjacentChapters/" + chapterId
    );
  }

}
