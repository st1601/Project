import { Component } from '@angular/core';

@Component({
  selector: 'app-maina',
  templateUrl: './maina.component.html',
  styleUrls: ['./maina.component.scss']
})
export class MainaComponent {
  showp: boolean = false;
  constructor() {}

  ngOnInit() {}
  po() {
    this.showp = !this.showp;
    console.log(this.showp);
  }
  clo() {
    this.showp = false;
  }
}
