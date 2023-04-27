import { Component, OnInit } from '@angular/core';
import { AppService } from 'src/app/services/app.service';

@Component({
  selector: 'app-navbarc',
  templateUrl: './navbarc.component.html',
  styleUrls: ['./navbarc.component.scss']
})
export class NavbarcComponent implements OnInit{
  constructor(public appService: AppService) {}
  ngOnInit(): void {}
}
