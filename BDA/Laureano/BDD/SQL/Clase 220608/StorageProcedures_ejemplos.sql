-- ejemplo procedimiento almacenado 
--(storage procedure-sp=reservado para el sistema) (spu=storage procedure usuario)

use Ej_clase2_aviones

select*from pasajeros

--sp sistema
sp_help pasajeros

--spu (creamos procedimiento de usuario local)
create proc spu_mostrarPasajeros
as
select*from pasajeros

--eliminar spu
drop procedure spu_mostrarPasajeros

--ejecutar procedimiento spu
exec spu_mostrarPasajeros

--spu con inner join
create proc spu_mostrarClientesLocalidad
as
select nombre_pasajero, apellido_pasajero,pasajeros.cp_localidad,localidad from pasajeros
inner join localidades on pasajeros.cp_localidad=localidades.cp_localidad

exec spu_mostrarClientesLocalidad

--crear spu con variable
create proc spu_mostrarPasajeroCp
@cp int
as
select nombre_pasajero, apellido_pasajero,pasajeros.cp_localidad,localidad from pasajeros
inner join localidades on pasajeros.cp_localidad=localidades.cp_localidad
where pasajeros.cp_localidad=@cp

exec spu_mostrarPasajeroCp 2500

--crear spu DELETE
create proc spu_deleteLocalidades
@cp int
as
delete localidades where cp_localidad=@cp

--crear spu INSERT
create proc spu_insertLocalidades
@cp int,
@localidad nvarchar(60)
as
insert into localidades
values(@cp,@localidad)

exec spu_insertLocalidades 9000,Rafaela
exec spu_insertLocalidades '6700',Mendoza
exec spu_insertLocalidades '8900',san_jose
exec spu_deleteLocalidades 8900
select*from localidades
--convertir tipos de datos
convert(nvarchar(60),@variable)

--crear spu que convierte a archivo de texto
create procedure spu_LIBRO_LOCALIDADES
as
exec xp_cmdshell 'bcp "select convert(nvarchar(30),cp_localidad) + localidad FROM localidades" queryout "C:\UAI\Cursada 2022\BDA\LIBRO_LOCALIDADES.txt"  -T -S LENOVO-FABRIZIO\SQLEXPRESS -c -t'
GO

select convert(nvarchar(30),cp_localidad) +' '+ localidad FROM localidades
EXEC spu_LIBRO_LOCALIDADES

drop procedure spu_LIBRO_LOCALIDADES

--Activamos las opciones avanzadas requisito indispensable para activar xp_cmdshell
sp_configure 'show advanced options', '1'
--Aplicamos los cambios
RECONFIGURE
--Habilitamos xp_cmdshell
sp_configure 'xp_cmdshell', '1' 
--Aplicamos los cambios
RECONFIGURE
-------------------