import { Injectable } from "@angular/core";
import { AppState, IAppState } from "./app-state";
import { BehaviorSubject, Observable } from "rxjs";
import { LocalStorageService } from "../local-storage/local-storage.service";
import { LocalStorageKeys } from "../local-storage/local-storage-keys";

@Injectable({
  providedIn: "root",
})
export class AppStateService {
  private appState: IAppState = new AppState();
  private appStateSubject: BehaviorSubject<IAppState> = new BehaviorSubject<IAppState>(this.appState);
  private appStateObservable: Observable<IAppState> = this.appStateSubject.asObservable();

  constructor(private localStorageService: LocalStorageService) {
    this.restoreFromLocalStorage();
  }

  public getAppState(): Observable<IAppState> {
    return this.appStateObservable;
  }

  public clearAppState(): void {
    this.localStorageService.clear(LocalStorageKeys.AppState);
    this.appState = new AppState();
    this.appStateSubject.next(this.appState);
  }

  public setAccessToken(accessToken: string): void {
    this.appState = this.appState.clone();
    this.appState.accessToken = accessToken;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setRefreshToken(refreshToken: string): void {
    this.appState = this.appState.clone();
    this.appState.refreshToken = refreshToken;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setUsername(username: string): void {
    this.appState = this.appState.clone();
    this.appState.username = username;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setId(id: string): void {
    this.appState = this.appState.clone();
    this.appState.id = id;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setEmail(email: string): void {
    this.appState = this.appState.clone();
    this.appState.email = email;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setfirstName(firstName: string): void {
    this.appState = this.appState.clone();
    this.appState.firstName = firstName;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setlastName(lastName: string): void {
    this.appState = this.appState.clone();
    this.appState.lastName = lastName;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  private restoreFromLocalStorage(): void {
    const appState: IAppState | null = this.localStorageService.get(LocalStorageKeys.AppState);
    if (appState !== null) {
      this.appState = new AppState(
        appState.accessToken,
        appState.refreshToken,
        appState.id,
        appState.username,
        appState.email,
        appState.firstName,
        appState.lastName
      );
      this.appStateSubject.next(this.appState);
    }
  }
}
