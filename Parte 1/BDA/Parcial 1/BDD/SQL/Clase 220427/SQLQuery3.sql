create database Empresa_Cimarelli
--drop database Practico1_Empresas
use Empresa_Cimarelli
--use cursomvc
create table localidades(
cp int primary key,
localidad nvarchar(60))

create table proveedores(
id_proveedor int primary key,
razon_social nvarchar(60),
cp int foreign key references localidades(cp),
telefono nvarchar(60))

create table productos(
id_producto int primary key,
descripcion nvarchar(60),
precio decimal(18,2),
peso decimal(18,2),
id_proveedor int foreign key references proveedores(id_proveedor))

create table clientes(
id_cliente int primary key,
nombre nvarchar(60),
cp int foreign key references localidades(cp),
telefono nvarchar(60),
dom_ent_calle nvarchar(60),
dom_ent_num nvarchar(60))

create table pedidos(
id_pedido int primary key,
id_cliente int foreign key references clientes(id_cliente),
fecha date,
importe decimal(18,2))

create table detalle_pedidos(
id_producto int foreign key references productos (id_producto),
id_pedido int foreign key references pedidos (id_pedido),
cant_producto int,
precio decimal(18,2),
precio_r decimal(18,2),
primary key (id_producto, id_pedido))

---CARGA DE DATOS-------------------------------------------------------------------------

insert into localidades values
(2500,'ca�ada de gomez'),
(2000,'Rosario'),
(3000,'Cordoba'),
(2450,'Moron'),
(1450,'Coronda'),
(3500,'Santa Fe'),
(4000,'Resistencia'),
(4500,'Casilda'),
(4800,'Las parejas')

insert into proveedores values
(9005,'Mariana nanis',2000,'6786556'),
(9000, 'Juan carlos',2500,'456789876'),
(9001,'Anibal Maderas',2000,'56789987'),
(9002,'Jorge S.A',4000,'6789876789'),
(9003,'Nicolas Carmen',2450,'9876789876'),
(9004,'Metales del sur',4500,'6545678765')

insert into productos values
(8000,'Silla metalica',5500.00,1.50,9000),
(8001,'Sillon doble',16000.00,7.50,9001),
(8002,'cama matrimonial',30000.00,15.75,9000),
(8003,'Mesa individual',10000.00,5.00,9004),
(8004,'alfombra',3500.00,1.50,9003),
(8005,'reloj de pared',5500.00,0.50,9003),
(8006,'mueble labatorio',8600.00,4.50,9002),
(8007,'calefactor',7000.00,3.25,9003)

insert into clientes values
(7006,'javier anoton',3000,'31254354','jalde','123'),
(7000,'anibal gomez',3000,'567876567','moreno','678'),
(7001,'juan perez',4000,'98765678','san martin','8765'),
(7002,'Juliana marin',4500,'5678765','lavalle','456'),
(7003,'maria nane',4800,'456765678','alvear','1233'),
(7004,'gabriela manes',2000,'12342233','balcarce','1244'),
(7005,'matias colaso',2500,'1232365454','necochea','6544')

insert into pedidos values
(1000,7005,'22-3-2020',11000),
(1001,7002,'12-4-2021',5500),
(1002,7004,'20-4-2022',7000),
(1003,7002,'25-3-2022',8600),
(1004,7004,'19-1-2022',10500)

insert into detalle_pedidos values
(8000,1000,2,5500,11000),
(8005,1001,1,5500,5500),
(8006,1003,1,8600,8600),
(8007,1002,1,7000,7000),
(8004,1004,3,3500,10500)
--CONSULTAS-------------------------------------------------------------------------
--4_ Mostrar todas las localidades (sin repetir) en la que existen proveedores
select localidad from proveedores
inner join localidades
on proveedores.cp=localidades.cp
group by localidad
--5_ Mostrar proveedores ordenados por raz�n social
select *from proveedores
order by razon_social
--6_ Mostrar id_producto y peso de los productos 8 y 11 (4 y 7)
with producto4y7 
  as
  (select row_number() over(order by id_producto, peso) as numeroFila, id_producto, peso
   from productos
  )
  select * from producto4y7
  where numeroFila=7 or numeroFila=4
  --7_ Calcular el promedio de peso de todos los productos
select avg(all peso) as promedio_pesos from productos
--8_ Contar cuantos clientes hay por ciudad
select localidad, count(id_cliente) as cantidad_clientes from clientes
inner join localidades 
on clientes.cp=localidades.cp
group by localidad
order by cantidad_clientes desc
--9_ Mostrar de todos los pedidos el id_pedido y fecha con su direcci�n completa de entrega
select id_pedido, clientes.dom_ent_calle, clientes.dom_ent_num from pedidos
inner join clientes
on pedidos.id_cliente=clientes.id_cliente
--10_ Mostrar el nombre de proveedor afectado a cada pedido
select proveedores.razon_social, proveedores.id_proveedor, id_pedido from detalle_pedidos
inner join  productos
on detalle_pedidos.id_producto=productos.id_producto
inner join proveedores
on productos.id_proveedor=proveedores.id_proveedor


