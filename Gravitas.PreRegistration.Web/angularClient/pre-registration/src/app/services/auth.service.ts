import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly accessTokenPropertyName = 'access_token';
  private readonly emailPropertyName = 'email';
  private apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  public registerUser(registerData: any): Observable<any> {
    localStorage.setItem(this.emailPropertyName, registerData.email);
    return this.http.post<any>(
      `${this.apiUrl}api/account/register`,
      registerData
    );
  }

  public deleteUser(): Observable<any> {
    return this.http.delete<any>(
      `${this.apiUrl}api/account/delete`);
  }

  public changePassword(data: any) {
    return this.http.post<any>(`${this.apiUrl}api/account/ChangePassword`, data);
  }

  public logout() {
    localStorage.removeItem(this.accessTokenPropertyName);
    localStorage.removeItem(this.emailPropertyName);
  }

  public setToken(result: any) {
    localStorage.setItem(this.accessTokenPropertyName, result.access_token);
  }

  public isAuthenticated() {
    const accessToken = localStorage.getItem(this.accessTokenPropertyName);

    return accessToken !== 'undefined' && accessToken !== undefined && accessToken !== null;
}

  public getToken() {
    return localStorage.getItem(this.accessTokenPropertyName);
  }

  public getEmail() {
    return localStorage.getItem(this.emailPropertyName);
  }

  public login(userData: any): Observable<any> {
    localStorage.setItem(this.emailPropertyName, userData.email);
    const options = {
      headers: new HttpHeaders().set(
        'Content-Type',
        'application/x-www-form-urlencoded'
      )
    };
    const body = new URLSearchParams();
    body.set('grant_type', 'password');
    body.set('username', userData.email);
    body.set('password', userData.password);

    return this.http.post<any>(`${this.apiUrl}token`, body.toString(), options);
  }
}
