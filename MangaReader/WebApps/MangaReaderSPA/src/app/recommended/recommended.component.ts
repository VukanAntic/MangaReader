import { Component, OnInit } from '@angular/core'
import { RecommendationPage } from './domain/models/recommendation-page.model'
import { Observable, Subscription } from 'rxjs'
import { RecommendationService } from './domain/infrastructure/recommendation.service'
import { Manga } from './domain/models/manga.model'

@Component({
  selector: 'app-recommended',
  templateUrl: './recommended.component.html',
  styleUrls: ['./recommended.component.css'],
})
export class RecommendedComponent implements OnInit {
  public recommendationPage?: Observable<RecommendationPage> | null
  public recommendationPageSub!: Subscription | null

  public favouriteAuthorName!: String | null
  public mangasByFavouriteAuthor!: [Manga] | null
  public favouriteGenreName!: string | null
  public mangasFromFavouriteGenre!: [Manga] | null
  public wishListMangas!: [Manga] | null
  public readMangas!: [Manga] | null

  constructor(private recommendationService: RecommendationService) {}

  ngOnInit(): void {
    this.recommendationPage = this.recommendationService.getRecommenationPage()
    this.recommendationPageSub = this.recommendationPage.subscribe((result) => {
      this.favouriteAuthorName = result.favouriteAuthorName
      this.mangasByFavouriteAuthor = result.mangasByFavouriteAuthor
      this.favouriteGenreName = result.favouriteGenreName
      this.mangasFromFavouriteGenre = result.mangasFromFavouriteGenre
      this.readMangas = result.ReadMangas
      this.wishListMangas = result.WishListMangas
    })
  }
}
