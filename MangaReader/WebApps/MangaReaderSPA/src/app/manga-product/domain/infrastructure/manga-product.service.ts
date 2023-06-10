import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, switchMap, take } from 'rxjs';
import { Manga } from '../models/manga.model';
import { Chapter } from '../models/chapter.model';
import { AppStateService } from 'src/app/shared/app-state/app-state.service';
import { IAppState } from 'src/app/shared/app-state/app-state';

@Injectable({
  providedIn: 'root'
})
export class MangaProductService {

  constructor(private  httpClient: HttpClient, private appStateService: AppStateService) { }

  public getMangaData(chapterId: string): Observable<Manga> {
    return this.appStateService.getAppState().pipe(
      take(1),
      switchMap((appState: IAppState) => {
        const accessToken: string | undefined = appState.accessToken;
        const headers: HttpHeaders = new HttpHeaders().append("Authorization", `Bearer ${accessToken}`);

        return this.httpClient.get<Manga>(
          'http://localhost:8000/api/Catalog/' + chapterId, { headers }
        );
      })
    );
  }

  public getMangaChapters(mangaId : string) : Observable<[Chapter]> {
    return this.appStateService.getAppState().pipe(
      take(1),
      switchMap((appState: IAppState) => {
        const accessToken: string | undefined = appState.accessToken;
        const headers: HttpHeaders = new HttpHeaders().append("Authorization", `Bearer ${accessToken}`);

        return this.httpClient.get<[Chapter]>(
          "http://localhost:8000/api/Chapter/GetAllChaptersForMangaId/" + mangaId,  { headers }
        )
      })
    );
    
  }
}
