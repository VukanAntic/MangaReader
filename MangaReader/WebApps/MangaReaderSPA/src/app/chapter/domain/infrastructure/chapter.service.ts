import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, switchMap, take } from 'rxjs';
import { Page } from 'src/app/manga-product/domain/models/page.model';
import { IAppState } from 'src/app/shared/app-state/app-state';
import { AppStateService } from 'src/app/shared/app-state/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class ChapterService {

  constructor(private  httpClient: HttpClient, private appStateService: AppStateService) { }

  // TODO: NEED TO ADD A REQUEST TO GET ALL THE DATA FOR THE CHAPTERID (BACKEND)

  
  public getPages(chapterId : string): Observable<[Page]> {
    return this.appStateService.getAppState().pipe(
      take(1),
      switchMap((appState: IAppState) => {
        const accessToken: string | undefined = appState.accessToken;
        const headers: HttpHeaders = new HttpHeaders().append("Authorization", `Bearer ${accessToken}`);

        return this.httpClient.get<[Page]>(
          "http://localhost:8000/api/Chapter/GetPagesForChapterId/" + chapterId,  { headers }
        )
      })
    );
    
  }
}
