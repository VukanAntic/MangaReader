import { Component, OnInit } from '@angular/core';
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

  constructor(private appStateService: AppStateService) {
    this.appState$ = this.appStateService.getAppState();
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
}
