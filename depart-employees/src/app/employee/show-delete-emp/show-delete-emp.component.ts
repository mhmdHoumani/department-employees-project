import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-show-delete-emp',
  templateUrl: './show-delete-emp.component.html',
  styleUrls: ['./show-delete-emp.component.css']
})
export class ShowDeleteEmpComponent implements OnInit {

  empList: any = [];
  ModalTitle: string = "";
  emp: any;
  ActivateAddEditEmployee: boolean = false;
  constructor(private service: SharedService) { }

  ngOnInit(): void {
    this.RefreshEmpList();
  }

  public RefreshEmpList() {
    this.service.GetEmployeeList().subscribe(data => {
      this.empList = data;
    });
  }

  public AddDepartment() {
    this.emp = {
      empID: 0,
      name: "",
      department: "",
      startDate: "",
      photoName: this.service.PhotoPath + "unknown.png"
    }
    this.ModalTitle = "Add Department";
    this.ActivateAddEditEmployee = true;
  }
  public EditEmployee(emp: object) {
    this.emp = emp;
    this.ActivateAddEditEmployee = true;
    this.ModalTitle = "Edit Employee";
  }
  public DeleteEmployee(empID: number) {
    if (confirm("Are you sure about it?")) {
      this.service.DeleteEmployee(empID).subscribe(res => {
        this.RefreshEmpList();
        alert("Deleted Successfully!");
      });
    }
  }

  public CloseAddEmployee() {
    this.ActivateAddEditEmployee = false;
    this.RefreshEmpList();
  }

}
