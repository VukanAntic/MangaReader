import { Component, OnInit } from '@angular/core'
import { RecommendationPage } from './domain/models/recommendation-page.model'
import { Observable, Subscription } from 'rxjs'
import { Manga } from './domain/models/manga.model'
import { RecommendationFacadeService } from './domain/application-services/recommendation-facade.service'

@Component({
  selector: 'app-recommended',
  templateUrl: './recommended.component.html',
  styleUrls: ['./recommended.component.css'],
})
export class RecommendedComponent implements OnInit {
  public recommendationPage?: Observable<RecommendationPage> | null
  public recommendationPageSub!: Subscription | null

  public count = 0

  constructor(private recommendationService: RecommendationFacadeService) {}

  ngOnInit(): void {
    this.count += 1
    this.recommendationPage = this.recommendationService.getRecommenationPage()
    // this.recommendationPage?.subscribe((res) => {
    //   console.log(res)
    //   console.log(res.readMangas)
    // })
  }
}
