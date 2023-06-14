import { Component, OnInit } from "@angular/core";
import { Observable, forkJoin } from "rxjs";
import { Manga } from "./domain/models/manga.model";
import { HomepageFacadeService } from "./domain/application-services/homepage-facade.service";

@Component({
  selector: "app-homepage",
  templateUrl: "./homepage.component.html",
  styleUrls: ["./homepage.component.css"],
})
export class HomepageComponent implements OnInit {
  public genreNames = ["Action", "Adventure", "Fantasy", "Drama"];
  public genreMangasList?: Observable<Manga[][]>;
  constructor(private homepageService: HomepageFacadeService) {}

  ngOnInit(): void {
    this.genreMangasList = forkJoin([
      this.homepageService.getMangasByGenre("Action"),
      this.homepageService.getMangasByGenre("Adventure"),
      this.homepageService.getMangasByGenre("Fantasy"),
      this.homepageService.getMangasByGenre("Drama"),
    ]);
  }

  public getMangasByGenre(genreName: string): Observable<Manga[]> {
    return this.homepageService.getMangasByGenre(genreName);
  }
}
