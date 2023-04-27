import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Event {
  id: number,
  eventName: string,
  eventDescription: string,
  userName: string,
}
export interface CreateEvent{
  isSuccess: boolean,
  message: string,
  data: {
    eventName: string,
    eventDescription: string,
    firstClosingDate: string,
    lastClosingDate: string,
    userName: string
  }
}

@Injectable({
  providedIn: 'root'
})
export class EventsService {
  private _api = 'http://dungmdung-001-site1.atempurl.com/api/Events';
  constructor(private http: HttpClient) {}
  public getAllEvent(): Observable<Event[]> {
    return this.http.get<Event[]>(this._api);
  }
  postEvent(data: any): Observable<any> {
    return this.http.post<CreateEvent>(this._api, data);
  }
}
