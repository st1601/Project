import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Category {
  id: number;
  categoryName: string,
  categoryDescription: string
}
export interface CategoryDetail {
  isSuccess: boolean,
  message: string,
  data: {
    id: number,
    categoryName: string,
    categoryDescription: string
  }
  };
@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private _api = 'http://dungmdung-001-site1.atempurl.com/api/Categories';
  constructor(private http: HttpClient) {}

  public getAllCategory(): Observable<Category[]> {
    return this.http.get<Category[]>(this._api);
  }
  getCategory(id: number): Observable<CategoryDetail> {
    return this.http.get<CategoryDetail>(this._api + '/' + id);
  }
  postCategory(data: any): Observable<CategoryDetail> {
    return this.http.post<CategoryDetail>(this._api, data);
  }
}
