import { Injectable } from "@angular/core";
import { UserService } from "../infrastructure/user.service";
import { Observable } from "rxjs";
import { IUserDetails } from "../models/user-details";

@Injectable({
  providedIn: "root",
})
export class UserFasadeService {
  constructor(private userService: UserService) {}

  public getUserDetails(): Observable<IUserDetails> {
    return this.userService.getUserDetails();
  }
}
