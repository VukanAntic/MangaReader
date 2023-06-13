import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Manga } from 'src/app/manga-product/domain/models/manga.model';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private  httpClient: HttpClient) { }

  public getSearchMangas(searchPrefix: string): Observable<Manga[]> {
    return this.httpClient.get<Manga[]>(
      'http://localhost:8000/api/Catalog/GetMangasBySearch/' + searchPrefix
    );
  }
}
