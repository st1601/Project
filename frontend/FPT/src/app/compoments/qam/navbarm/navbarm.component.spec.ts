import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavbarmComponent } from './navbarm.component';

describe('NavbarmComponent', () => {
  let component: NavbarmComponent;
  let fixture: ComponentFixture<NavbarmComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NavbarmComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NavbarmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
