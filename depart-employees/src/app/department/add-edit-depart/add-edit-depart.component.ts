import { Component, Input, OnInit } from '@angular/core';
import { Department } from 'src/app/DepartEmployee';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-add-edit-depart',
  templateUrl: './add-edit-depart.component.html',
  styleUrls: ['./add-edit-depart.component.css']
})

export class AddEditDepartComponent implements OnInit {

  @Input() department: Department = { deptID: 0, name: "" };
  dept: Department = { deptID: 0, name: "" };

  constructor(private service: SharedService) { }

  ngOnInit(): void {
    this.dept.deptID = this.department.deptID;
    this.dept.name = this.department.name;
  }

  public AddDepartment() {
    this.service.AddDepartment(this.dept).subscribe(res => alert("Added Successfully!"));
    this.dept.name = "";
  }
  public UpdateDepartment() {
    this.service.UpdateDepartment(this.dept).subscribe(res => alert("Updated Successfully!"));
  }

}
