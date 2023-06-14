import { Component, OnInit } from "@angular/core";
import { UserFasadeService } from "../../domain/application-services/user-fasade.service";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";

interface IChangePasswordFormData {
  oldPassword: string;
  newPassword: string;
  repeatedPassword: string;
}

@Component({
  selector: "app-change-password",
  templateUrl: "./change-password.component.html",
  styleUrls: ["./change-password.component.css"],
})
export class ChangePasswordComponent implements OnInit {
  public changePasswordForm: FormGroup;

  constructor(private userService: UserFasadeService, private routerService: Router) {
    this.changePasswordForm = new FormGroup({
      oldPassword: new FormControl("", [Validators.required, Validators.minLength(8)]),
      newPassword: new FormControl("", [Validators.required, Validators.minLength(8)]),
      repeatedPassword: new FormControl("", [Validators.required, Validators.minLength(8)]),
    });
  }

  ngOnInit(): void {}

  public onChangePasswordFormSubmit(): void {
    if (this.changePasswordForm.invalid) {
      window.alert("Form has errors!");
      return;
    }

    const data: IChangePasswordFormData = this.changePasswordForm.value as IChangePasswordFormData;

    if (data.newPassword !== data.repeatedPassword) {
      window.alert("Passwords are different!");
      return;
    }

    this.userService.changePassword(data.oldPassword, data.newPassword).subscribe((success: boolean) => {
      if (!success) {
        window.alert(`Password is not changed!`);
      }
      this.changePasswordForm.reset();
      if (success) {
        window.alert(`Password is successfully changed!`);
        this.routerService.navigate(["/identity", "profile"]);
      }
    });
  }
}
