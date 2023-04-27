import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavbarcComponent } from './navbarc.component';

describe('NavbarcComponent', () => {
  let component: NavbarcComponent;
  let fixture: ComponentFixture<NavbarcComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NavbarcComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NavbarcComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
