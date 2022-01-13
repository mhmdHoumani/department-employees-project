import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowDeleteDepartComponent } from './show-delete-depart.component';

describe('ShowDeleteDepartComponent', () => {
  let component: ShowDeleteDepartComponent;
  let fixture: ComponentFixture<ShowDeleteDepartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowDeleteDepartComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowDeleteDepartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
