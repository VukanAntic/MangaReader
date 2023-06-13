import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IUserDetails } from "../models/user-details";
import { Observable, switchMap, take } from "rxjs";
import { AppStateService } from "src/app/shared/app-state/app-state.service";
import { IUpdateLastAndFirstName } from "../models/updateLastAndFirstName-request";
import { IUpdateEmail } from "../models/updateEmail-request";
import { IChangePassword } from "../models/changePassword-request";

@Injectable({
  providedIn: "root",
})
export class UserService {
  constructor(private httpClient: HttpClient, private appStateService: AppStateService) {}

  public getUserDetails(): Observable<IUserDetails> {
    return this.httpClient.get<IUserDetails>("http://localhost:4000/api/v1/User");
  }

  public updateLastAndFirstname(request: IUpdateLastAndFirstName): Observable<boolean> {
    return this.httpClient.put<boolean>("http://localhost:4000/api/v1/User/ChangeLastAndFirstName", request);
  }

  public updateEmail(request: IUpdateEmail): Observable<boolean> {
    return this.httpClient.put<boolean>("http://localhost:4000/api/v1/User/ChangeEmail", request);
  }

  public changePassword(request: IChangePassword): Observable<boolean> {
    return this.httpClient.put<boolean>("http://localhost:4000/api/v1/User", request);
  }
}
