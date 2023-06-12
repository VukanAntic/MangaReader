import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { RecommendationPage } from "../models/recommendation-page.model";

@Injectable({
  providedIn: "root",
})
export class RecommendationService {
  constructor(private httpClient: HttpClient) {}

  public getRecommenationPage(accessToken?: string): Observable<RecommendationPage> {
    return this.httpClient.get<RecommendationPage>("http://localhost:8004/api/Recommendation/GetRecommendationPage");
  }
}
