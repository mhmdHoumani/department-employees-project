create database EmployeeDepartDB;

create table Department (
	depart_ID int primary key identity(1,1),
	depart_NAME varchar(100)
);

create table Employee(
	emp_ID int primary key identity(15000, 1),
	emp_NAME varchar(100),
	emp_PHOTO varchar(200),
	emp_DATE_START date,
	department varchar(100)
);

insert into Department values
('IT'),('Insurance'),('Security');

insert into Employee values
('Sam', 'unknown.png', '2021-11-17', 'IT'),
('Peter', 'unknown.png', '2020-06-01', 'Security'),
('John', 'unknown.png', '2020-06-21', 'Security'),
('Carla', 'unknown.png', '2019-12-01', 'Insurance');

select * from Department;
select * from Employee;