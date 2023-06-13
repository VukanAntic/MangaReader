import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NotAuthenticatedGuard } from "../shared/guards/not-authenticated.guard";
import { LoginFormComponent } from "./feature-authentication/login-form/login-form.component";
import { LogoutComponent } from "./feature-authentication/logout/logout.component";
import { UserProfileComponent } from "./feature-user-info/user-profile/user-profile.component";
import { IdentityComponent } from "./identity.component";
import { ChangePasswordComponent } from "./feature-user-info/change-password/change-password.component";

// podrazumeva se prefiks /identity
const routes: Routes = [
  {
    path: "",
    component: IdentityComponent,
    children: [{ path: "login", component: LoginFormComponent }],
  },
  {
    path: "profile",
    component: UserProfileComponent,
    canActivate: [NotAuthenticatedGuard],
  },
  { path: "logout", component: LogoutComponent },
  { path: "profile/changePassword", component: ChangePasswordComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class IdentityRoutingModule {}
