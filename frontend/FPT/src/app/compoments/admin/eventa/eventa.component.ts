import { Component } from '@angular/core';
import { EventsService } from 'src/app/services/events.service';
import { UserService } from 'src/app/services/user.service';

interface Event {
  id: number,
  eventName: string,
  eventDescription: string,
  userName: string,
}
interface User {
  id: number;
  userName: string;
  role: string;
}
interface create {
  userId: number,
  eventName: string,
  eventDescription: string,
  firstClosingDate: Date,
  lastClosingDate: Date,
}
interface CreateEvent {
  isSuccess: true,
  message: string,
  data: {
    eventName: string,
    eventDescription: string,
    firstClosingDate: Date,
    lastClosingDate: Date,
  }
}

@Component({
  selector: 'app-eventa',
  templateUrl: './eventa.component.html',
  styleUrls: ['./eventa.component.scss']
})
export class EventaComponent {
  showc: boolean = false;
  allEvent: Event[] = [];
  allUser: User[] = [];
  createEvent: create = {
    userId: Number(localStorage.getItem("id")),
    eventName: '',
    eventDescription: '',
    firstClosingDate: new Date(),
    lastClosingDate: new Date(),
  };

  constructor(private eventService: EventsService, private userService: UserService) {}

  ngOnInit() {
    this.eventService.getAllEvent().subscribe((res: Event[]) => {
      this.allEvent = res;
    });
  }
  cr() {
    this.showc = !this.showc;
    if(this.showc){
      this.userService.getAllUser().subscribe((res: User[]) => {
        let arr = res.filter((repo: User)=> {return repo.role == 'QACoordinator'})
        this.allUser = arr;
      });
    }
  }
  cl() {
    this.showc = false;
  }
  save() {
    this.eventService.postEvent(this.createEvent).subscribe((res: CreateEvent) => {
      if (res.isSuccess) {
        this.showc = !this.showc;
        this.eventService.getAllEvent().subscribe((res: Event[]) => {
          this.allEvent = res;
        });
      }
    });
  }
}
