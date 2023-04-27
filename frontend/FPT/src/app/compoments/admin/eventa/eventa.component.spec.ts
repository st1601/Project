import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventaComponent } from './eventa.component';

describe('EventaComponent', () => {
  let component: EventaComponent;
  let fixture: ComponentFixture<EventaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EventaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
