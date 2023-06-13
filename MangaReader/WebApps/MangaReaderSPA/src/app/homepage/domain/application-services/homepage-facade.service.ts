import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Manga } from "../models/manga.model";
import { HomepageService } from "../infrastructure/homepage.service";

@Injectable({
  providedIn: "root",
})
export class HomepageFacadeService {
  private mangaList?: Observable<Manga[]>;
  constructor(private homepageService: HomepageService) {}

  public getMangasByGenre(genreName: string): Observable<Manga[]> {
    return this.homepageService.getMangasByGenre(genreName);
  }
}
