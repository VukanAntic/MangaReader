import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUserDetails } from '../models/user-details';
import { Observable, switchMap, take } from 'rxjs';
import { AppStateService } from 'src/app/shared/app-state/app-state.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private httpClient: HttpClient, private appStateService: AppStateService) {}

  public getUserDetails(): Observable<IUserDetails> {
    return this.httpClient.get<IUserDetails>('http://localhost:4000/api/v1/User');
  }
}
