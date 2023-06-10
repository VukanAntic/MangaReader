import { Manga } from './manga.model'

export interface RecommendationPage {
  favouriteAuthorName: string
  mangasByFavouriteAuthor: Manga[]
  favouriteGenreName: string
  mangasFromFavouriteGenre: Manga[]
  readMangas: Manga[]
  wishListMangas: Manga[]
}
