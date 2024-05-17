--PARCIAL 2 CIMARELLI FABRIZIO
--Creo la base de datos y pongo en uso
create database PARCIAL2_CIMARELLI
use FINAL
use PARCIAL2_CIMARELLI
drop database PARCIAL2_CIMARELLI
--creo tablas, claves y diagrama-------------------------------------------------------------------------
create table vendedores(
id_vendedor int primary key,
nombre_vend nvarchar(60),
acum_venta decimal(18,2),
)

create table localidades(
codigo_postal int primary key,
localidad nvarchar(60))

create table productos (
id_producto int primary key,
descripcion nvarchar(60),
precio decimal(18,2),
stock int)

create table clientes(
id_cliente int primary key,
nombre_cli nvarchar(60),
direccion_cli nvarchar(60) null,
codigo_postal int foreign key references localidades(codigo_postal),
acum_compra decimal(18,2))

create table orden_compra(
id_orden int primary key,
id_cliente int foreign key references clientes(id_cliente),
id_vendedor int foreign key references vendedores(id_vendedor),
fecha datetime,
importe decimal(18,2))


create table detalle_orden_compra(
id_orden int foreign key references orden_compra(id_orden),
id_producto int foreign key references productos(id_producto),
cantidad int,
primary key(id_orden,id_producto))

--CARGO DATOS-------------------------------------------------------------------------------------------------

insert into vendedores values
(100,'Juan Carlos',650),
(101,'Anibal Gonge',4500),
(102,'Jorge Maria',7400)

insert into localidades values
(2500,'Cañada de gomez'),
(2000,'Rosario'),
(3000,'Cordoba'),
(2400,'Parana')

insert into productos values
(905,'luz led',450,0),
(900,'Silla gamer',2300,10),
(901,'Mouse',900,330),
(902,'Teclado',1000,20),
(903,'Monitor',4000,15),
(904,'Notebook',6000,5)

insert into clientes values
(204,'Nancy Toledo',null,2500,4500),
(200,'Marco Torres',null,2400,40000),
(201,'Franco Galvez','balcarce 78',2500,3500),
(202,'Mariano Moreno','alvear 2400',2000,60990),
(203,'Maximo Antin','lavalle 450',2500,15000)
--select*from clientes
insert into orden_compra values
(805,203,100,'05/12/20',650),
(804,201,100,'17/02/19',3500),
(800,200,100,'18/06/22',4000),
(801,200,101,'30/05/22',2500),
(802,202,102,'4/8/21',1000),
(803,201,101,'7/7/21',10000)

insert into detalle_orden_compra values
(803,904,5),
(800,903,5),
(800,900,1),
(800,902,1),
(801,904,2),
(802,903,3),
(803,901,1)


select*from productos
--PROCEDIMIENTO ALMACENADOS----------------------------------------------------------------------------------
--1_a OK
create procedure spu_elimina_cli
	@id_cliente int
as
delete from clientes where id_cliente=@id_cliente

exec spu_elimina_cli 200

--1_b OK
create procedure spu_consulta_compras
	@fecha_inicio datetime,
	@fecha_fin datetime
as
select id_orden from orden_compra where fecha between @fecha_inicio and @fecha_fin

exec spu_consulta_compras '01/07/2021','15/08/21'

--FUNCIONES--------------------------------------------------------------------------------------------------
--2_a 

select avg(importe) as Promedio from orden_compra
--No supe resolver utilizando funciones

--2_b
select clientes.nombre_cli, importe, avg(importe) from orden_compra
inner join clientes
on orden_compra.id_cliente=clientes.id_cliente
group by id_cliente
having avg(importe)

--2_c OK
create function fun_StockProductos
   (@stock int)
returns table
as
return
    (select * from productos where stock = @stock)
go

select * from dbo.fun_StockProductos(0)

--2_d OK
create function fun_direccion
 (@direccion nvarchar(30)) 
 returns nvarchar(30)
begin 
 if @direccion IS NULL
 set @direccion = 'No Informa'
 return @direccion 
end

select id_cliente, nombre_cli, dbo.fun_direccion(direccion_cli) as direccionC_cli, codigo_postal, acum_compra
from clientes

--2_e OK
create function fun_FechaFormatoEspecial
	(@fecha datetime)
	
returns nvarchar(30)
begin
	 set @fecha=(CONVERT(nvarchar(30),@fecha,107))
	 return @fecha
end
go

select id_cliente, id_orden, id_vendedor, dbo.fun_FechaFormatoEspecial(fecha) as Fecha,importe 
from orden_compra

--TRIGGERS-------------------------------------------------------------------------------------------------
--3_a OK
create trigger baja_stock
on detalle_orden_compra for insert as 
update productos
	set stock = stock - (select cantidad from detalle_orden_compra where id_orden in (select id_orden from inserted) and id_producto in(select id_producto from inserted))
	where id_producto in (select id_producto from inserted)

--3_b OK
create trigger actualiza_acum
on orden_compra for insert as 
update clientes
	set acum_compra=acum_compra+(select importe from orden_compra where id_orden in (select id_orden from inserted))
	where id_cliente in (select id_cliente from inserted)
update vendedores
	set acum_venta=acum_venta+(select importe from orden_compra where id_orden in (select id_orden from inserted))
	where id_vendedor in (select id_vendedor from inserted)

