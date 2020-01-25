CREATE DATABASE PersonasDB
GO
USE PersonasDB
GO
CREATE TABLE Personas
(
	PersonaId int primary key identity,
	Nombre varchar(30),
	Telefono varchar(13),
	Cedula varchar(13),
	Direccion varchar(max),
	FechaNacimiento date Default Getdate()
);