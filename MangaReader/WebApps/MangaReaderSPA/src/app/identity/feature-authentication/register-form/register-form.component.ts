import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthenticationFacadeService } from "../../domain/application-services/authentication-facade.service";

interface IRegisterFormData {
  firstname: string;
  lastname: string;
  username: string;
  email: string;
  phoneNumber: string;
  password: string;
}

@Component({
  selector: "app-register-form",
  templateUrl: "./register-form.component.html",
  styleUrls: ["./register-form.component.css"],
})
export class RegisterFormComponent implements OnInit {
  public registerForm: FormGroup;

  constructor(private authenticationService: AuthenticationFacadeService, private routerService: Router) {
    this.registerForm = new FormGroup({
      firstname: new FormControl("", [Validators.required, Validators.minLength(3)]),
      lastname: new FormControl("", [Validators.required, Validators.minLength(3)]),
      username: new FormControl("", [Validators.required, Validators.minLength(3)]),
      email: new FormControl("", [Validators.required, Validators.minLength(3), Validators.email]),
      phoneNumber: new FormControl("", [Validators.minLength(3)]),
      password: new FormControl("", [Validators.required, Validators.minLength(3)]),
    });
  }

  ngOnInit(): void {}

  public onRegisterFormSubmit(): void {
    if (this.registerForm.invalid) {
      window.alert("Form has errors!");
      return;
    }

    const data: IRegisterFormData = this.registerForm.value as IRegisterFormData;
    this.authenticationService
      .register(data.firstname, data.lastname, data.username, data.password, data.email, data.phoneNumber)
      .subscribe((success: boolean) => {
        if (!success) {
          window.alert(`Register is not successful!`);
        }
        this.registerForm.reset();
        if (success) {
          this.routerService.navigate(["homepage"]);
        }
      });
  }
}
