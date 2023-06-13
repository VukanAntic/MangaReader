import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { IdentityRoutingModule } from "./identity-routing.module";
import { IdentityComponent } from "./identity.component";
import { LoginFormComponent } from "./feature-authentication/login-form/login-form.component";
import { ReactiveFormsModule } from "@angular/forms";
import { UserProfileComponent } from "./feature-user-info/user-profile/user-profile.component";
import { LogoutComponent } from "./feature-authentication/logout/logout.component";
import { ChangePasswordComponent } from "./feature-user-info/change-password/change-password.component";
import { RegisterFormComponent } from './feature-authentication/register-form/register-form.component';


@NgModule({
  declarations: [IdentityComponent, LoginFormComponent, UserProfileComponent, LogoutComponent, RegisterFormComponent, ChangePasswordComponent],
  imports: [CommonModule, IdentityRoutingModule, ReactiveFormsModule],
})
export class IdentityModule {}
