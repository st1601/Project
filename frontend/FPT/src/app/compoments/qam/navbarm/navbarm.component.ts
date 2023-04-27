import { Component, OnInit } from '@angular/core';
import { AppService } from 'src/app/services/app.service';

@Component({
  selector: 'app-navbarm',
  templateUrl: './navbarm.component.html',
  styleUrls: ['./navbarm.component.scss']
})
export class NavbarmComponent implements OnInit{
  constructor(public appService: AppService) {}
  ngOnInit(): void {}
}
