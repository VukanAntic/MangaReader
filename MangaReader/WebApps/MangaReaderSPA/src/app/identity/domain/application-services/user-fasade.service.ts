import { Injectable } from "@angular/core";
import { UserService } from "../infrastructure/user.service";
import { Observable, catchError, map, of, switchMap } from "rxjs";
import { IUserDetails } from "../models/user-details";
import { AppStateService } from "src/app/shared/app-state/app-state.service";
import { IUpdateLastAndFirstName } from "../models/updateLastAndFirstName-request";
import { IUpdateEmail } from "../models/updateEmail-request";
import { IChangePassword } from "../models/changePassword-request";

@Injectable({
  providedIn: "root",
})
export class UserFasadeService {
  constructor(private userService: UserService, private appStateService: AppStateService) {}

  public getUserDetails(): Observable<IUserDetails> {
    return this.userService.getUserDetails();
  }

  public updateLastAndFirstName(firstName: string, lastName: string): Observable<boolean> {
    const request: IUpdateLastAndFirstName = { firstName, lastName };

    return this.userService.updateLastAndFirstname(request).pipe(
      switchMap(() => {
        return this.userService.getUserDetails();
      }),
      map((userDetails: IUserDetails) => {
        this.appStateService.setfirstName(firstName);
        this.appStateService.setlastName(lastName);

        return true;
      }),
      catchError((err) => {
        console.log(err);
        this.appStateService.clearAppState();
        return of(false);
      })
    );
  }

  public updateEmail(newEmail: string): Observable<boolean> {
    const request: IUpdateEmail = { newEmail };

    return this.userService.updateEmail(request).pipe(
      switchMap(() => {
        return this.userService.getUserDetails();
      }),
      map((userDetails: IUserDetails) => {
        this.appStateService.setEmail(newEmail);

        return true;
      }),
      catchError((err) => {
        console.log(err);
        this.appStateService.clearAppState();
        return of(false);
      })
    );
  }

  public changePassword(oldPassword: string, newPassword: string): Observable<boolean> {
    const request: IChangePassword = { oldPassword, newPassword };

    return this.userService.changePassword(request).pipe(
      map(() => {
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
