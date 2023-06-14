import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Manga } from "../models/manga.model";

@Injectable({
  providedIn: "root",
})
export class HomepageService {
  constructor(private httpClient: HttpClient) {}

  public getMangasByGenre(genreName: string): Observable<Manga[]> {
    return this.httpClient.get<Manga[]>(`http://localhost:8000/api/Catalog/GetMangasByGenreName/${genreName}`);
  }
}
