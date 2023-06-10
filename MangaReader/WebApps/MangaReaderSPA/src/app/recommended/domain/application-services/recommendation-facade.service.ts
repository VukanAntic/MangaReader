import { Injectable } from '@angular/core'
import { AppStateService } from 'src/app/shared/app-state/app-state.service'
import { RecommendationService } from '../infrastructure/recommendation.service'
import { RecommendationPage } from '../models/recommendation-page.model'
import { Observable, map } from 'rxjs'

@Injectable({
  providedIn: 'root',
})
export class RecommendationFacadeService {
  private recommended?: Observable<RecommendationPage>
  constructor(
    private appStateService: AppStateService,
    private recommendationService: RecommendationService,
  ) {}

  public getRecommenationPage(): Observable<RecommendationPage> | undefined {
    this.appStateService.getAppState().subscribe((appState) => {
      this.recommended = this.recommendationService.getRecommenationPage(
        appState.accessToken,
      )
    })
    return this.recommended
  }
}
