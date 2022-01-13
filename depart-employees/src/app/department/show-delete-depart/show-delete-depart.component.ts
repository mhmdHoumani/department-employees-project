import { Component, OnInit } from '@angular/core';
import { Department } from 'src/app/DepartEmployee';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-show-delete-depart',
  templateUrl: './show-delete-depart.component.html',
  styleUrls: ['./show-delete-depart.component.css']
})
export class ShowDeleteDepartComponent implements OnInit {

  departList: any = [];
  ModalTitle: string = "";
  depart: any;
  ActivateAddEditDepartment: boolean = false;
  departIDFilter: string = "";
  departNameFilter: string = "";
  departListWithoutFilter: any;

  constructor(private service: SharedService) { }

  ngOnInit(): void {
    this.RefreshDepList();
  }

  public RefreshDepList() {
    this.service.GetDepartList().subscribe(data => {
      this.departList = data;
      this.departListWithoutFilter = data;
    });
  }

  public AddDepartment() {
    this.depart = {
      deptID: 0,
      name: ""
    }
    this.ModalTitle = "Add Department";
    this.ActivateAddEditDepartment = true;
  }
  public EditDepartment(dept: object) {
    this.depart = dept;
    this.ActivateAddEditDepartment = true;
    this.ModalTitle = "Edit Department";
  }
  public DeleteDepartment(deptID: number) {
    if (confirm("Are you sure about it?")) {
      this.service.DeleteDepartment(deptID).subscribe(res => {
        this.RefreshDepList();
        alert("Deleted Successfully!");
      });
    }
  }

  FilterDepartment() {
    var idFilter = this.departIDFilter;
    var nameFilter = this.departNameFilter;
    this.departList = this.departListWithoutFilter.filter((element: Department) => {
      return element.deptID.toString().toLowerCase().includes(
        idFilter.toString().trim().toLowerCase()
      ) && element.name.toString().toLowerCase().includes(
        nameFilter.toString().trim().toLowerCase()
      )
    });
  }

  SortResult(dept: any, asc: boolean) {
    this.departList = this.departListWithoutFilter.sort((a: any, b: any) => {
      if (asc) return (a[dept] > b[dept]) ? 1 : ((a[dept] < b[dept]) ? -1 : 0);
      else return (b[dept] > a[dept]) ? 1 : ((b[dept] < a[dept]) ? -1 : 0);
    });
  }

  public CloseAddDepartment() {
    this.ActivateAddEditDepartment = false;
    this.RefreshDepList();
  }

}
