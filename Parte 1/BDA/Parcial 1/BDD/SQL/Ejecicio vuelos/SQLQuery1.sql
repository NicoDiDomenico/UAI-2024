create database Ej_clase2_aviones
drop database Ej_clase2_aviones
use Ej_clase2_aviones
use cursomvc

create table localidades (
cp_localidad int primary key,
localidad nvarchar(60))

create table pasajeros(
dni_pasajero bigint primary key,
apellido_pasajero nvarchar(60),
nombre_pasajero nvarchar(60),
telefono_pasajero bigint,
domicilio_pasajero nvarchar(60),
mail_pasajero nvarchar(60),
cp_localidad int foreign key references localidades(cp_localidad))

create table propietarios(
cuit_prop bigint primary key,
razon_social nvarchar(60),
cp_localidad int foreign key references localidades(cp_localidad))

create table aviones(
id_avion int primary key,
cuit_prop bigint foreign key references propietarios (cuit_prop),
cant_pasajeros int,
tipo_avion nvarchar(60),
a�o_avion int)

create table vuelos(
numero_vuelo int primary key,
origen_vuelo nvarchar(60),
destino_vuelo nvarchar(60),
distancia_vuelo int,
fecha_vuelo date,
hora_salida time,
hora_llegada time,
id_avion int foreign key references aviones(id_avion))

create table pasajeros_vuelos(
numero_vuelo int foreign key references vuelos (numero_vuelo),
dni_pasajero bigint foreign key references pasajeros (dni_pasajero)
primary key (numero_vuelo, dni_pasajero))

create table escalas(
id_escala int primary key,
numero_vuelo int foreign key references vuelos(numero_vuelo),
origen nvarchar(60),
destino nvarchar(60),
matricula_avion nvarchar(60))

--CARGA DE DATOS

insert into localidades values
(2500,'Ca�ada de gomez'),
(2000, 'Rosario'),
(3500, 'Cordoba')

insert into pasajeros values
(11111111,'lopez','pedro',42424242,'moreno 512','pedro@mail.com',2500),
(22222222,'mu�oz','anibla',32323232,'lavalle 1222','aniba@mail.com',3500),
(33333333,'taiana','jorge',43434343,'san martin 4342','jorege@mail.com',2500),
(44444444,'moreno','antonio',67676767,'alvear 3123','antonio@mail.com',2000),
(55555555,'pereyra','juan',56565656,'balcarce 7878','juan@mai.com',2000)

insert into propietarios values
(66666666666,'razon1',2000),
(88888888888,'razon2',2500),
(77777777777,'razon3',3500),
(99999999999,'razon4',2500)

insert into aviones values
(6666,77777777777,4,'piston',1970),
(9999,66666666666,30,'bimotor',1998),
(8888,77777777777,6,'turbohelice',2005),
(7777,88888888888,8,'jet privado',1990),
(5555,99999999999,60,'comercial',2009)

insert into vuelos values 
(1000,'buenos aires','bariloche',3000,'12-4-2021','12:30:00','17:00:00',9999),
(1001,'cordoba','rosario',400,'14-5-2021','09:00:00','10:30:00',8888),
(1002,'rosario','salta',5000,'16-8-2020','17:00:00','23:30:00',7777)

insert into pasajeros_vuelos values
(1000,22222222),
(1000,11111111),
(1001,11111111),
(1002,22222222),
(1001,33333333),
(1002,44444444),
(1001,55555555),
(1000,55555555),
(1002,55555555)

insert into escalas values
(2000,1000,'la pampa','bariloche',9999),
(2001,1002,'cordoba','tucuman',8888),
(2002,1002,'tucuman','salta',8888)

--1_ Mostrar todos los propietarios de Aviones.
select cuit_prop, id_avion from aviones
--2_ Mostrar todos los propietarios de Aviones ordenados por apellido
select*from propietarios order by razon_social
--4_ Mostrar todos los propietarios que tienen mas de un Avion y mostrar cuantos aviones
select cuit_prop from aviones where COUNT()
--3_ Mostrar la cantidad de pasajeros que volaron en nuestra empresa
select*from pasajeros
--5_ mostrar los 3 pasajero que mas veces volaron.
select
--6_ Mostrar aquellos pasajeros que solo volaron una vez
--7_ mostrar las localidades en las cual residen la mayoria de nuestros pasajeros. (las 3 --principales)
--8_ Mostrar las 3 aeronaves mas antiguas junto con los datos de su propietario
--9_ Mostrar todos los vuelos que realizaron escalas

--10_ Mostrar la catidad total de pasajeros por vuelos
	