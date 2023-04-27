import { Component } from '@angular/core';
import { IdeasService } from 'src/app/services/ideas.service';

interface infor {

  id: number,
  ideaTitle: string,
  ideaDescription: string,
  dateSubmitted: Date,
  hashTag: string,
  userName: string,
  department: number,

}
export interface Idea {
  id: number;
  ideaTitle: string;
  ideaDescription: string;
  userName: string,
}
interface detail {
  id: number,
  ideaTitle: string,
  ideaDescription: string,
  dateSubmitted: Date,
  hashTag: string,
  userName: string,
  department: number,

}
interface create {

}
interface Search {
  ideaTitle: string;
}

@Component({
  selector: 'app-idea',
  templateUrl: './idea.component.html',
  styleUrls: ['./idea.component.scss']
})
export class IdeaComponent {
  showc: boolean = false;
  showp: boolean = false;
  allIdea: Idea[] = [];
  title = 'angular-text-search-highlight';
  searchText = '';
  characters: Search[] = [];
  detailIdea: detail = {
    id: 0,
  ideaTitle: '',
  ideaDescription: '',
  dateSubmitted: new Date(),
  hashTag: '',
  userName: '',
  department: 0,
  };
  createIdea: create = {
    userName: '',
    password: '',
    fullName: '',
    doB: new Date(),
    email: '',
    phoneNumber: 0,
    role: 0,
    department: 0,
  };

  constructor(private ideaService: IdeasService) {}

  ngOnInit() {
    this.ideaService.getAllIdea().subscribe((res: Idea[]) => {
      this.allIdea = res;
    });
  }
  cr() {
    this.showc = !this.showc;
    console.log(this.showc);
  }
  po(id: number) {
    this.showp = !this.showp;
    console.log(this.showp);
  }
  clo() {
    this.showc = false;
    this.showp = false;
  }
}

