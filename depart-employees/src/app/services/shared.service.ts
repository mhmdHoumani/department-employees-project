import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  readonly APIUrl = "https://localhost:44310/";
  readonly PhotoPath = "file:///C:/Users/User/source/repos/Angular%20Web%20API%20Tutorial/api/api/wwwroot/assets/";
  //readonly PhotoPath = "https://localhost:44310/upload/";

  constructor(private http: HttpClient) { }

  public GetDepartList(): Observable<object> {
    return this.http.get(this.APIUrl);
  }
  public AddDepartment(val: any): Observable<object> {
    return this.http.post(this.APIUrl, val);
  }
  public UpdateDepartment(val: any): Observable<object> {
    return this.http.put(this.APIUrl + val.deptID, val);
  }
  public UpdatePatchDepartment(val: any): Observable<object> {
    return this.http.patch(this.APIUrl + val.deptID, val);
  }
  public DeleteDepartment(val: number): Observable<object> {
    return this.http.delete(this.APIUrl + val);
  }

  public GetEmployeeList(): Observable<object> {
    return this.http.get(this.APIUrl + "employees/");
  }
  public AddEmployee(val: any): Observable<object> {
    return this.http.post(this.APIUrl + "employees", val);
  }
  public UpdateEmployee(val: any): Observable<object> {
    return this.http.put(this.APIUrl + "employees/" + val.empID, val);
  }
  public UpdatePatchEmployee(val: any): Observable<object> {
    return this.http.patch(this.APIUrl + "employees/" + val.empID, val);
  }
  public DeleteEmployee(val: number): Observable<object> {
    return this.http.delete(this.APIUrl + "employees/" + val);
  }

  public UploadPhoto(val: any): Observable<object> {
    return this.http.post(this.APIUrl + "employees/upload/", val);
  }
  public GetAllDepartNames(): Observable<object> {
    return this.http.get(this.APIUrl + "employees/GetAllDepartmentsNames");
  }
}
