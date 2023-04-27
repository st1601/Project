import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategorymComponent } from './categorym.component';

describe('CategorymComponent', () => {
  let component: CategorymComponent;
  let fixture: ComponentFixture<CategorymComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CategorymComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CategorymComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
