import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainmComponent } from './mainm.component';

describe('MainmComponent', () => {
  let component: MainmComponent;
  let fixture: ComponentFixture<MainmComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MainmComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MainmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
