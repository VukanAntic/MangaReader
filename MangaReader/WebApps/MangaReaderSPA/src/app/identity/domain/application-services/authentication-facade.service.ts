import { Injectable } from "@angular/core";
import { catchError, map, Observable, of, switchMap } from "rxjs";
import { AuthenticationService } from "../infrastructure/authentication.service";
import { ILoginRequest } from "../models/login-request";
import { ILoginResponse } from "../models/login-response";
import { AppStateService } from "src/app/shared/app-state/app-state.service";
import { JwtService } from "src/app/shared/jwt/jwt.service";
import { JwtPayloadKeys } from "src/app/shared/jwt/jwt-payload-keys";
import { UserFasadeService } from "./user-fasade.service";
import { IUserDetails } from "../models/user-details";

@Injectable({
  providedIn: "root",
})
export class AuthenticationFacadeService {
  constructor(
    private authenticationService: AuthenticationService,
    private appStateService: AppStateService,
    private jwtService: JwtService,
    private userService: UserFasadeService
  ) {}

  public login(username: string, password: string): Observable<boolean> {
    const request: ILoginRequest = { username, password };

    return this.authenticationService.login(request).pipe(
      switchMap((loginResponse: ILoginResponse) => {
        console.log(loginResponse);
        this.appStateService.setAccessToken(loginResponse.accessToken);
        this.appStateService.setRefreshToken(loginResponse.refreshToken);

        const payload = this.jwtService.parsePayload(loginResponse.accessToken);
        this.appStateService.setId(payload[JwtPayloadKeys.ID]);
        this.appStateService.setUsername(payload[JwtPayloadKeys.Username]);
        this.appStateService.setEmail(payload[JwtPayloadKeys.Email]);

        return this.userService.getUserDetails();
      }),
      map((userDetails: IUserDetails) => {
        this.appStateService.setfirstName(userDetails.firstName);
        this.appStateService.setlastName(userDetails.lastName);

        return true;
      }),
      catchError((err) => {
        console.log(err);
        this.appStateService.clearAppState();
        return of(false);
      })
    );
  }
}
