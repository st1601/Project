import { Component } from '@angular/core';
import { EventsService } from 'src/app/services/events.service';

interface Event {
  id: number,
  eventName: string,
  eventDescription: string,
  userName: string,
}

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent {
  allEvent: Event[] = [];
  constructor(private eventService: EventsService) {}

  ngOnInit() {
    this.eventService.getAllEvent().subscribe((res: Event[]) => {
      this.allEvent = res;
    });
  }

}
