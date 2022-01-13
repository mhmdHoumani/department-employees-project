import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowDeleteEmpComponent } from './show-delete-emp.component';

describe('ShowDeleteEmpComponent', () => {
  let component: ShowDeleteEmpComponent;
  let fixture: ComponentFixture<ShowDeleteEmpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowDeleteEmpComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowDeleteEmpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
