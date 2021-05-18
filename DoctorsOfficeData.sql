create database DoctorsOffice
go

create table dbo.PatientInfo (
	PatientID int identity (1,1) primary key not null,
	FirstName nvarchar(50) not null,
	MiddleName nvarchar(50),
	LastName nvarchar(50) not null,
	DOB nvarchar(50) not null,
	DoctorID int not null foreign key references dbo.DoctorInfo,
	AppointmentID int not null foreign key references dbo.AppointmentInfo,
	PaymentID int not null foreign key references dbo.PaymentInfo)
go

create table dbo.DoctorInfo (
	DoctorID int primary key not null,
	DoctorName nvarchar(50) not null)
go

create table dbo.PaymentInfo (
	PaymentID int primary key not null,
	Description nvarchar(50) not null)
go

create table dbo.AppointmentInfo (
	AppointmentID int primary key not null,
	Description nvarchar(50) not null)
go