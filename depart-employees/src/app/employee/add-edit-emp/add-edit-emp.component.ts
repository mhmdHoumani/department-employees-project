import { Component, Input, OnInit } from '@angular/core';
import { Employee } from 'src/app/DepartEmployee';
import { SharedService } from 'src/app/services/shared.service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-add-edit-emp',
  templateUrl: './add-edit-emp.component.html',
  styleUrls: ['./add-edit-emp.component.css']
})
export class AddEditEmpComponent implements OnInit {

  @Input() employee: any;
  emp: any;
  departNames: any = [];
  safeURL: SafeUrl = {};

  constructor(private readonly service: SharedService, private readonly sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.service.GetAllDepartNames().subscribe(data => this.departNames = data);
    this.emp = this.employee;
    this.safeURL = this.sanitizer.bypassSecurityTrustResourceUrl(this.emp.photoName);
  }

  public AddEmployee() {
    this.service.AddEmployee(this.emp).subscribe(res => alert("Added Successfully!"));
    this.emp = {};
    this.emp.department = "--Select--";
  }
  public UpdateEmployee() {
    this.service.UpdateEmployee(this.emp).subscribe(res => alert("Updated Successfully!"));
  }

  public UploadPhoto(event: any) {
    var file = event.target.files[0];
    const formData = new FormData();
    formData.append('file', file);
    this.service.UploadPhoto(formData).subscribe(res => {
      if (Object.values(res)[0] != "Invalid") {
        this.emp.photoName = this.service.PhotoPath + Object.values(res)[0];
        this.safeURL = this.sanitizer.bypassSecurityTrustResourceUrl(this.emp.photoName);
      }
      else {
        alert("Invalid file type...\nMake sure to choose a .png/jpg file only.");
      }
      console.warn("Photo name : " + this.emp.photoName);
    });
  }

}
