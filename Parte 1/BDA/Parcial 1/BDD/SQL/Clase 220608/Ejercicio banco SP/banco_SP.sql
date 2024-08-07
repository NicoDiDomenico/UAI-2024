--Creo la base de datos
create database banco_SP

use banco_SP

--Creo las tablas
create table clientes(
id_cliente int primary key,
apellido_cli nvarchar(100),
nombre_cli nvarchar(100),
dni nvarchar(100),
telefono nvarchar(100),
fecha_nac datetime)

create table cuentas(
id_cuenta int primary key,
id_cliente int foreign key references clientes(id_cliente),
tipo_cuenta nvarchar(100),
saldo decimal(18,2))

create table hco_saldos(
id_cuenta int foreign key references cuentas(id_cuenta),
saldo decimal(18,2),
fxsaldo decimal(18,2))

create table movimientos(
id_movimiento int primary key,
id_cuenta int foreign key references cuentas(id_cuenta),
saldo_ant decimal(18,2),
saldo_post decimal(18,2),
importe decimal(18,2),
fecha_mov datetime)

--Creo los SPU

--spu agregar clientes
create procedure spu_addClientes
	@id_cliente int,
	@apellido_cli nvarchar(100),
	@nombre_cli nvarchar(100),
	@dni nvarchar(100),
	@telefono nvarchar(100),
	@fecha_nac datetime
as 
insert into clientes values
(@id_cliente,@apellido_cli,@nombre_cli,@dni,@telefono,@fecha_nac)

--ejecuta spu
exec spu_addClientes 3,'lopez','pedro','25673333','4355866','20/11/1960'

--spu agregar cuentas
create procedure spu_addCuentas
	@id_cuenta int,
	@id_cliente int,
	@tipo_cuenta nvarchar(100),
	@saldo decimal(18,2)
as
insert into cuentas values
(@id_cuenta,@id_cliente,@tipo_cuenta,@saldo)

--ejecuta spu
exec spu_addCuentas 13,3,'caja ahorro', 12342.55

create procedure spu_addMovimientos
	@id_movimiento int,
	@id_cuenta int,
	@saldo_ant decimal(18,2),
	@saldo_post decimal(18,2),
	@importe decimal(18,2),
	@fecha_mov datetime
as
insert into movimientos values
(@id_movimiento,@id_cuenta,@saldo_ant,@saldo_post,@importe,@fecha_mov)

--ejecuta spu
exec spu_addMovimientos 23,13,12342.55,12000.55,342.00,'15/06/2022'

--spu eliminar clientes
create procedure spu_delClientes
	@id_cliente int
as
delete from clientes where id_cliente=@id_cliente

--ejecuta spu
exec spu_delClientes 3

--spu eliminar cuentas
create procedure spu_delCuentas
	@id_cuenta int
as
delete from cuentas where id_cuenta=@id_cuenta

--ejecuta spu
exec spu_delCuentas 13

--spu eliminar movimientos
create procedure spu_delMovimientos
	@id_movimiento int
as
delete from movimientos where id_movimiento=@id_movimiento

--ejecuta spu
exec spu_delMovimientos 23

--spu actualizar datos clientes
create procedure spu_upClientes
	@id_cliente int,
	@apellido_cli nvarchar(100),
	@nombre_cli nvarchar(100),
	@dni nvarchar(100),
	@telefono nvarchar(100),
	@fecha_nac datetime
as 
update clientes
set
	apellido_cli=@apellido_cli,
	nombre_cli=@nombre_cli,
	dni=@dni,
	telefono=@telefono,
	fecha_nac=@fecha_nac
where
	id_cliente=@id_cliente

--ejecuta proc
exec spu_upClientes 3, 'lopez','pedro juan','45678765','567876545','12/09/1961'

--spu actualizar datos cuentas
create procedure spu_upCuentas
	@id_cuenta int,
	@id_cliente int,
	@tipo_cuenta nvarchar(100),
	@saldo decimal (18,2)
as
update cuentas
set
	id_cliente=@id_cliente,
	tipo_cuenta=@tipo_cuenta,
	saldo=@saldo
where
	id_cuenta=@id_cuenta

--spu actualizar datos movimientos
create procedure spu_upMovimientos
	@id_movimiento int,
	@id_cuenta int,
	@saldo_ant decimal(18,2),
	@saldo_post decimal(18,2),
	@importe decimal(18,2),
	@fecha_mov datetime
as
update movimientos
set
	id_cuenta=@id_cuenta,
	saldo_ant=@saldo_ant,
	saldo_post=@saldo_post,
	importe=@importe,
	fecha_mov=@fecha_mov
where
	id_movimiento=@id_movimiento

--spu con parametro de salida
create procedure spu_obtenerSaldoCuenta
	@id_cuenta int,
	@saldo decimal(18,2) output
as
begin
	select @saldo=saldo
	from cuentas
	where id_cuenta=@id_cuenta
end

--ejecuta spu obtener saldo
declare @saldo decimal(18,2)
exec spu_obtenerSaldoCuenta 13, @saldo output
print @saldo

--procedimiento de salida de movimientos
create procedure spu_movimientosCuenta 
	@fecha_mov datetime
as
begin
	select apellido_cli,nombre_cli,saldo_ant,saldo_post,importe,fecha_mov
	from movimientos 
	inner join cuentas on movimientos.id_cuenta=cuentas.id_cliente
	inner join clientes on cuentas.id_cliente=clientes.id_cliente
	where fecha_mov=@fecha_mov
	order by fecha_mov desc
end

--ejecuta proc
exec spu_movimientosCuenta '15/06/2022'

