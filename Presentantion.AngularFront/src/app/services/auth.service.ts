import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7251/api/Login';

  constructor(private http: HttpClient) {}

  login(login: string, password: string): Observable<any> {
    return this.http.post<any>(this.apiUrl, { login, password }).pipe(
      tap(response => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('roleId', response.roleId);
      }),
      catchError(error => {
        throw error;
      })
    );
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
}
