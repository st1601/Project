import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Idea {
    id: number;
    ideaTitle: string;
    ideaDescription: string;
    userName: string,
}
export interface IdeaDetail {
    isSuccess: boolean,
    message: string,
    data: {
      id: number;
      ideaTitle: string,
      ideaDescription: string,
      dateSubmitted: string,
      file: string,
      hashTag: string,
      userName: string,
      department: 0,
      eventName: string,
      firstClosingDate: string,
      lastClosingDate: string,
      categories: [
        {
          id: number,
          categoryName: string,
          categoryDescription: string
        }
      ]
    };
}
export interface CreateIdea{
  ideaTitle: string,
  ideaDescription: string,
  file: string,
  hashTag: string,
  userId: 0,
  eventId: 0,
  categoryIds: [
    0
  ]
}

@Injectable({
  providedIn: 'root'
})
export class IdeasService {
  private _api = 'http://dungmdung-001-site1.atempurl.com/api/Ideas';
  constructor(private http: HttpClient) {}
  public getAllIdea(): Observable<Idea[]> {
    return this.http.get<Idea[]>(this._api);
  }
  getIdea(id: number): Observable<IdeaDetail> {
    return this.http.get<IdeaDetail>(this._api + '/' + id);
  }
  postIdea(data: any): Observable<any> {
    return this.http.post<CreateIdea>(this._api, data);
  }
}
