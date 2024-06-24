CREATE DATABASE VENTAS
use ventas

create table clientes(
id_cli int primary key,
nombre nvarchar(60),
acum decimal(18,2))

create table productos(
id_prod int primary key,
descrip nvarchar(60),
precio decimal(18,2),
stock int)

create table ventas(
id_venta int identity(1,1) primary key,
fecha date,
id_prod int foreign key references productos(id_prod),
importe decimal(18,2),
id_cli int foreign key references clientes(id_cli))

select * from sys.objects where type = 'TR'
create trigger acum_cli
on ventas for insert as
update clientes
set acum = acum + (select importe from ventas where id_venta in (select id_venta from inserted))
where id_cli in (select id_cli from inserted)

insert into clientes
values(1,'Juan',0),(2,'Luis',0)

insert into productos
values(1,'CPU',5000,5),(2,'RAM',2000,10)

select * from clientes
select * from productos
select * from ventas

insert into ventas
values(GETDATE(),1,100,1)


create trigger baja_stock
on ventas for insert as
update productos
set stock = stock - 1
where id_prod in (select id_prod from inserted)


create trigger [dbo].[tr_bajaStock] on [dbo].[detalle_venta]
for insert as
UPDATE a SET a.stock = a.stock - i.cant 
FROM articulos a
JOIN Inserted i ON a.codigo = i.codigo and a.talle =i.talle and a.color = i.color
GO



--RESET
delete from ventas
update clientes
set acum = 0