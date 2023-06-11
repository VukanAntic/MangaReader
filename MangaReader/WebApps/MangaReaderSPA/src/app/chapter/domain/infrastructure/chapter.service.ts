import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, switchMap, take } from 'rxjs';
import { Chapter } from 'src/app/manga-product/domain/models/chapter.model';
import { Page } from 'src/app/manga-product/domain/models/page.model';
import { IAppState } from 'src/app/shared/app-state/app-state';
import { AppStateService } from 'src/app/shared/app-state/app-state.service';

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
}
