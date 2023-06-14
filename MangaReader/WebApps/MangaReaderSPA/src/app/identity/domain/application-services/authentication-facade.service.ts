import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, switchMap, take } from 'rxjs';
import { AuthenticationService } from '../infrastructure/authentication.service';
import { ILoginRequest } from '../models/login-request';
import { ILoginResponse } from '../models/login-response';
import { AppStateService } from 'src/app/shared/app-state/app-state.service';
import { JwtService } from 'src/app/shared/jwt/jwt.service';
import { JwtPayloadKeys } from 'src/app/shared/jwt/jwt-payload-keys';
import { UserFasadeService } from './user-fasade.service';
import { IUserDetails } from '../models/user-details';
import { IAppState } from 'src/app/shared/app-state/app-state';
import { ILogoutRequest } from '../models/logout-request';
import { IRefreshTokenRequest } from '../models/refresh-token-request';
import { IRefreshTokenResponse } from '../models/refresh-token-response';
import { IRegisterRequest } from '../models/register-request';

@Injectable({
  providedIn: 'root',
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

  public logout(): Observable<boolean> {
    return this.appStateService.getAppState().pipe(
      take(1),
      map((appState: IAppState) => {
        const request: ILogoutRequest = { userName: appState.username as string, refreshToken: appState.refreshToken as string };
        return request;
      }),
      switchMap((request: ILogoutRequest) => this.authenticationService.logout(request)),
      map(() => {
        this.appStateService.clearAppState();
        return true;
      }),
      catchError((err) => {
        console.error(err);
        return of(false);
      })
    );
  }

  public refreshToken(): Observable<string | null> {
    return this.appStateService.getAppState().pipe(
      take(1),
      map((appState: IAppState) => {
        const request: IRefreshTokenRequest = { userName: appState.username as string, refreshToken: appState.refreshToken as string };
        return request;
      }),
      switchMap((request: IRefreshTokenRequest) => this.authenticationService.refreshToken(request)),
      map((response: IRefreshTokenResponse) => {
        this.appStateService.setAccessToken(response.accessToken);
        this.appStateService.setRefreshToken(response.refreshToken);

        return response.accessToken;
      }),
      catchError((err) => {
        console.log(err);
        this.appStateService.clearAppState();
        return of(null);
      })
    );
  }

  public register(firstname: string, lastname: string, username: string, password: string, email: string, phoneNumber: string): Observable<boolean> {
    const request: IRegisterRequest = { firstname, lastname, username, password, email, phoneNumber };

    return this.authenticationService.register(request).pipe(
      switchMap((registerResponse: any) => {
        console.log(registerResponse);

        return this.login(username, password);
      })
    );
  }
}
