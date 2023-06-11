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

  public getNavbarTitle(appState: IAppState): string {
    if (appState.firstName !== undefined && appState.lastName !== undefined) {
      return `Welcome to MangaReader, ${appState.firstName} ${appState.lastName}`;
    }

    return `MangaReader`;
  }

  public userLoggedIn(appState: IAppState): boolean {
    return !this.userLoggedOut(appState);
  }

  public userLoggedOut(appState: IAppState): boolean {
    return appState.isEmpty();
  }
}
