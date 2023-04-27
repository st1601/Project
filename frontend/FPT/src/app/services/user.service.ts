import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface User {
  id: number;
  userName: string;
  role: string;
}
export interface UserDetail {
  isSuccess: boolean;
  message: string;
  data: {
    id: number;
    userName: string;
    fullName: string;
    doB: string;
    email: string;
    phoneNumber: number;
    role: string;
    department: string;
  };
}
export interface CreateUser{
  isSuccess: boolean,
  message: string,
  data: {
    userName: string,
    fullName: string,
    doB: string,
    email: string,
    phoneNumber: number,
    role: number,
    department: number
  }
}
@Injectable({
  providedIn: 'root',
})
export class UserService {
  private _api = 'http://dungmdung-001-site1.atempurl.com/api/Users';
  constructor(private http: HttpClient) {}
  public getAllUser(): Observable<User[]> {
    return this.http.get<User[]>(this._api);
  }
  getUser(id: number): Observable<UserDetail> {
    return this.http.get<UserDetail>(this._api + '/' + id);
  }
  postUser(data: any): Observable<any> {
    return this.http.post<CreateUser>(this._api, data);
  }
  deleteUser(id: number): Observable<any> {
    return this.http.delete<any>(this._api + '/' + id);
  }
  editUser(data: any): Observable<any> {
    return this.http.put<any>(this._api, data);
  }
}
