create database AerouertoDB
use AerouertoDB

create table Localidades(
CP int primary key,
Localidad nvarchar(60))

create table Pasajeros(
DNI_Pasajero int primary key,
Apellido nvarchar(60),
Nombre nvarchar(60),
Telefono int,
Domicilio nvarchar(60),
Mail nvarchar(60),
CP int foreign key references Localidades(CP))

Create table Propietarios(
CUIT_Propietario int primary key,
Razon_Social nvarchar(60),
CP int foreign key references Localidades(CP))

Create table Aviones(
Num_Avion int primary key,
Cant_Pasajeros int,
Tipo_Aeronave nvarchar(60),
Year_Fabricacion int,
CUIT_Propietario int foreign key references Propietarios(CUIT_Propietario))

create table Vuelos(
Num_Vuelo int primary key,
Origen nvarchar(60),
Destino nvarchar(60),
Km decimal(18,2),
Fecha date,
Hora_Salida time,
Hora_Llegada time,
Num_Avion int foreign key references Aviones(Num_Avion))

create table Pasajeros_Vuelos(
DNI_Pasajero int foreign key references Pasajeros(DNI_Pasajero),
Num_Vuelo int foreign key references Vuelos(Num_Vuelo),
primary key(DNI_Pasajero, Num_Vuelo))

Create table Escalas(
ID_Escalas int primary key,
Origen nvarchar(60),
Destino nvarchar(60),
Num_Vuelo int foreign key references Vuelos(Num_Vuelo),
Num_Avion int foreign key references Aviones(Num_Avion))

--INPUTS
select*from Localidades
insert into Localidades
values (2000, 'Rosario'), (1001, 'CABA'), (5000, 'Códoba'),(8400, 'Bariloche'),(4600, 'Jujuy')

select*from Pasajeros
insert into Pasajeros
values (42704984, 'Nardi', 'Florencia', 4513935, 'Bv. Argentino 8276', 'florenciaxnardi@gmail.com', 2000),(12345678, 'Perez', 'Juan Carlos', 11234543, 'Cabildo 133', 'JuanCa@hotmail.com', 1001), (6028035, 'Lopez', 'Pablo', 1567754, 'Roca 2200', 'PL@gmail.com', 5000), (45673233, 'Ramirez', 'Claudia', 24564321, 'San Martín 728', 'claurr@gmail.com', 1001), (2345533, 'Grillo', 'Conan', 45189399, 'JJPaso 8289','cg@gmail.com', 2000),(4657444,'Dee','Sandra',532222444, 'Av. Libertador 4322', 'sande@yahoo.com.ar', 1001)

select*from Propietarios
insert into Propietarios
values (26789345, 'RI', 1001),(15678903, 'SRL', 2000), (37364674, 'SA', 5000)

select*from Aviones
insert into Aviones
values (1, 300, 'Jumbo', 1999, 26789345),(2, 10, 'Jet Privado', 2007, 15678903),(3, 80, 'Turbo Jet', 2012, 26789345), (4, 25, 'Jet Privado', 2017, 37364674)

select*from Vuelos
insert into Vuelos
values (1, 'BsAs', 'Rosario', 300, '2022-04-16','05:20:00', '6:10:00', 2), (2, 'Córdoba', 'Mendoza', 609.6, '2022-05-18', '12:15:00', '14:45:00', 1), (3, 'Rosario', 'BsAs', 300, '2022-04-27', '08:00:00', '09:00:00', 4), (4, 'BsAs', 'Bariloche', 700, '2022-05-25','15:10:00', '18:00:00', 3)  

select*from Pasajeros_Vuelos
insert into Pasajeros_Vuelos
values (42704984, 2), (12345678, 1), (2345533, 3), (4657444, 4), (6028035, 1), (12345678, 2), (42704984, 3), (45673233, 4), (42704984, 4), (2345533,1)

select*from Escalas
insert into Escalas
values(1, 'Córdoba', 'San Juan', 2,1),(2, 'San Juan', 'Mendoza', 2, 2)

--CONSULTAS	
--1_ Mostrar todos los propietarios de Aviones.
select Num_Avion, CUIT_Propietario from Aviones

--2_ Mostrar todos los propietarios de Aviones ordenados por apellido(Cambio por Razon social)
select Razon_Social from Propietarios
Order by Razon_Social asc

--4_ Mostrar todos los propietarios que tienen mas de un Avion y mostrar cuantos
select CUIT_Propietario, count(*)as Cantidad_Aviones from Aviones 
group by CUIT_Propietario
having count(*)>1

--3_ Mostrar la cantidad de pasajeros que volaron en nuestra empresa
select count(*) from Pasajeros

--5_ mostrar los 3 pasajero que mas veces volaron.
select top 3 DNI_Pasajero, count(DNI_Pasajero) as Vuelos_Tomados
from Pasajeros_Vuelos 
group by DNI_Pasajero
order by Vuelos_Tomados desc

--6_ Mostrar aquellos pasajeros que solo volaron una vez
select DNI_Pasajero, count (DNI_Pasajero) as Vuelos_Tomados
from Pasajeros_Vuelos
group by DNI_Pasajero
having count(DNI_Pasajero) = 1

--7_ mostrar las localidades en las cual residen la mayoria de nuestros pasajeros. (las 3 --principales)
select top 3 Localidad, Localidades.CP, count(Localidades.CP) as Cantidad_Residentes
from Pasajeros
inner join Localidades on Pasajeros.CP = Localidades.CP
group by Localidades.CP, Localidad
order by Cantidad_Residentes desc

--8_ Mostrar las 3 aeronaves mas antiguas junto con los datos de su propietario
select top 3 (Year_Fabricacion), CUIT_Propietario
from Aviones
group by Year_Fabricacion, CUIT_Propietario

--9_ Mostrar todos los vuelos que realizaron escalas
select Num_Vuelo from Escalas
group by Num_Vuelo

--10_ Mostrar la cantidad total de pasajeros por vuelos
select Num_Vuelo, count(DNI_Pasajero) as Total_Pasajeros
from Pasajeros_Vuelos
group by  Num_Vuelo
