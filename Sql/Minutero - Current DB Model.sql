use Minutero;
drop table ItemMinuta
drop table Minuta
drop table Usuario
drop table Suscriptor

create table Usuario
(
	Id int not null identity(1,1) primary key,
	Usuario char(20) not null unique,
	NombreAMostrar char(50) not null,
	Mail varchar(60) not null,
	EsEncargado bit not null DEFAULT 0,
	OrdenEnMinuta int not null,
	Activo bit not null DEFAULT 1
)

create table Minuta
(
	Id int not null identity(1,1) primary key,
	Fecha datetime not null,
	Usuario char(20) not null foreign key references Usuario(Usuario)
	constraint fechaYUsuarioUnicos unique nonclustered (Fecha, Usuario)
)

create table ItemMinuta
(
	Id int not null identity(1,1),
	IdMinuta int not null foreign key references Minuta(Id),
	Descripcion varchar(100) not null,
	Tipo char(15) not null,
	primary key(Id, IdMinuta)
)

create table Suscriptor
(
	Id int not null identity(1,1) primary key,
	Descripcion char(50) not null,
	Mail varchar(60) not null
)

create table Configuracion
(
	Id int not null identity(1,1) primary key,
	Descripcion char(50) not null,
	Valor varchar(max) not null
)
go



