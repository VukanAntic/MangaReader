import { Component, OnChanges, OnDestroy, OnInit, SimpleChanges } from "@angular/core";
import { Observable, Subscription } from "rxjs";
import { IAppState } from "src/app/shared/app-state/app-state";
import { AppStateService } from "src/app/shared/app-state/app-state.service";
import { UserFasadeService } from "../../domain/application-services/user-fasade.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-user-profile",
  templateUrl: "./user-profile.component.html",
  styleUrls: ["./user-profile.component.css"],
})
export class UserProfileComponent implements OnInit {
  public appState$: Observable<IAppState>;

  public editableEmail?: boolean;
  public editableName?: boolean;
  public changingPassword?: boolean;

  constructor(private AppStateService: AppStateService, private userService: UserFasadeService) {
    this.appState$ = this.AppStateService.getAppState();

    this.editableEmail = false;
    this.editableName = false;
    this.changingPassword = false;
  }

  ngOnInit(): void {}

  editEmail() {
    this.editableEmail = !this.editableEmail;
  }

  editName() {
    this.editableName = !this.editableName;
  }

  updateEmail(newEmail: string): void {
    this.userService.updateEmail(newEmail).subscribe();
    this.editableEmail = !this.editableEmail;
  }

  updateName(value: string): void {
    const input: string[] = value.split(" ");
    if (input.length !== 2) {
      window.alert("Wrong format!");
      return;
    }
    const firstName: string = input[0];
    const lastName: string = input[1];
    this.userService.updateLastAndFirstName(firstName, lastName).subscribe();
    this.editableName = !this.editableName;
  }

  openChangePasswordForm(): void {
    this.changingPassword = true;
  }
}
