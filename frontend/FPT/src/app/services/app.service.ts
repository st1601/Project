import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AppService {
  private tokenKey: string = 'token';
  private idKey: string = 'id';
  constructor(private http: HttpClient, private router:Router) {}

  isLogIn() {
    let token = localStorage.getItem(this.tokenKey);
    if (token) {
      return true;
    }
    return false;
  }
  login(user: any) {
    return this.http.post('http://dungmdung-001-site1.atempurl.com/api/Accounts/login', user);
  }
  logout() {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.idKey);
    this.router.navigate(['/login']);
  }

  getToken() {
    let token = localStorage.getItem(this.tokenKey);
    if (token) {
      return token;
    } else {
      return false;
    }
  }
}
