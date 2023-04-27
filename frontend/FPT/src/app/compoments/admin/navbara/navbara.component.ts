import { Component, OnInit } from '@angular/core';
import { AppService } from 'src/app/services/app.service';

@Component({
  selector: 'app-navbara',
  templateUrl: './navbara.component.html',
  styleUrls: ['./navbara.component.scss']
})
export class NavbaraComponent implements OnInit{
  constructor(public appService: AppService) {}
  ngOnInit(): void {}
}
