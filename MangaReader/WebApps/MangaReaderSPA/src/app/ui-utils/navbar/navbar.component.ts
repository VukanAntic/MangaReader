import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { IAppState } from 'src/app/shared/app-state/app-state';
import { AppStateService } from 'src/app/shared/app-state/app-state.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  public appState$: Observable<IAppState>;
  public searchString : string;

  constructor(private appStateService: AppStateService, private router: Router) {
    this.appState$ = this.appStateService.getAppState();
    this.searchString = "";
  }

  ngOnInit(): void {}

  public getUserName(appState: IAppState): string {
    return `${appState.firstName} ${appState.lastName}`;
  }

  public userLoggedIn(appState: IAppState): boolean {
    return !this.userLoggedOut(appState);
  }

  public userLoggedOut(appState: IAppState): boolean {
    return appState.isEmpty();
  }

  public onKeydown(ev : Event) {
    if (ev.target as HTMLInputElement != null) {
      this.searchString = (ev.target as HTMLInputElement).value;      
    }
    if (this.searchString.trim() === "") {
      window.alert("Must enter a search request!");
    }
    else {
      this.router.navigate(['/search', this.searchString]);
      (ev.target as HTMLInputElement).value = ""; 
    }
  }

}
