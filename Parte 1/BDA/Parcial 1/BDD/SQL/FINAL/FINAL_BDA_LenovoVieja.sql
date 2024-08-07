create database FINAL_BDA_CIMARELLI

use FINAL_BDA_CIMARELLI

create table tipo_documentos(
id_tipo_doc int identity(0,1) primary key,
descrip_doc nvarchar(60))

insert into tipo_documentos values
('Ningun documento'),
('D.N.I'),
('C.U.I.L'),
('C.U.I.T'),
('Cedula de Identificacion'),
('Pasaporte'),
('Libreta Civica'),
('Libreta de Enrolamiento')

select*from tipo_documentos

create table tipo_comprobantes(
id_tipo_comp int primary key,
descrip_comp nvarchar(60))

insert into tipo_comprobantes values
(1,'Factura A'),
(2,'Nota debito A'),
(3,'Nota credito A'),
(4,'Recibos A'),
(5,'Nota Venta Contado A'),
(6,'Factura B'),
(7,'Nota debito B'),
(8,'Nota credito B'),
(9,'Recibo B'),
(10,'Nota Venta Contado B'),
(11,'Factura C'),
(12,'Nota debito C'),
(13,'Nota credito C'),
(15,'Recibo C'),
(16,'Nota venta contado C'),
(81,'Tique Factura A'),
(82,'Tique Factura B'),
(83,'Tique'),
(111,'Tique Factura C'),
(112,'Tique Nota Credito A'),
(113,'Tique Nota Credito B'),
(114,'Tique Nota Credito C')

select*from tipo_comprobantes

create table clientes(
id_cliente int identity(1,1) primary key,
nombre_or_razon nvarchar(60),
id_tipo_doc int foreign key references tipo_documentos(id_tipo_doc),
nro_documento bigint)

insert into clientes values
('GUSTAVO MAZOLI',6,67890),
('FABRIZIO CIMARELLI',2,41240989),
('MAURICIO GARCIA',3,20256997991)

select*from clientes

create table comprobantes(
nro_comp int primary key,
fecha datetime,
id_tipo_comp int foreign key references tipo_comprobantes(id_tipo_comp),
id_cliente int foreign key references clientes(id_cliente),
importe decimal(13,2))

insert into comprobantes values
(1,'14/07/2022',1,1,7500),
(2,'02/12/2022',2,2,3400),
(3,'9/8/2012',83,1,7890.67),
(4,'9/9/2013',13,3,800),
(5,'8/9/2015',113,3,5000.50),
(6,'7/4/2015',3,2,50700),
(7,'5/5/2020',8,1,650)

select*from comprobantes

--Consulta para armar la tabla a exportar.

select fecha,id_tipo_comp,nro_comp,nro_comp,id_tipo_doc,nro_documento,nombre_or_razon,importe from comprobantes
inner join clientes on clientes.id_cliente=comprobantes.id_cliente

--Creo la tabla con los datos requeridos
create table comprobantes_txt(
fecha_comprob nvarchar(8),
tipo_comprob nvarchar(3),
num_comprob_desde nvarchar(20),
num_comprob_hasta nvarchar(20),
id_doc_comprador nvarchar(2),
nro_doc_comprador nvarchar(20),
nombre_or_razon nvarchar(30),
importe_total nvarchar(15))

--Cargo los datos con formato requerido en la nueva tabla PRUEBA
insert into comprobantes_txt
select fecha=CONVERT(varchar(8),fecha,112),
id_tipo_comp=(RIGHT('000'+LTRIM(rtrim(id_tipo_comp)),3)),
nro_comp=(RIGHT('00000000000000000000'+LTRIM(rtrim(id_tipo_comp)),20)),
nro_comp=(RIGHT('00000000000000000000'+LTRIM(rtrim(id_tipo_comp)),20)),
id_tipo_doc=(RIGHT('00'+LTRIM(rtrim(id_tipo_comp)),2)),
nro_documento=(RIGHT('00000000000000000000'+LTRIM(rtrim(nro_documento)),20)),
nombre_or_razon=LEFT(convert(varchar,nombre_or_razon) + '                              ',30),
importe=REPLACE(importe,'.','')--(RIGHT('000000000000000'+LTRIM(rtrim(importe)),15))
from comprobantes
inner join clientes on clientes.id_cliente=comprobantes.id_cliente

select*from comprobantes_txt

--Creo una segunda tabla para aplicar mas formatos a los campos
create table comprobantes_txt_export(
uno nvarchar(8),
dos nvarchar(3),
tres nvarchar(20),
cuatro nvarchar(20),
cinco nvarchar(2),
seis nvarchar(20),
siete nvarchar(30),
ocho nvarchar(15))

insert into comprobantes_txt_export
select
fecha_comprob,
tipo_comprob, 
num_comprob_desde,
num_comprob_hasta,
id_doc_comprador,
nro_doc_comprador,
nombre_or_razon,
importe_total=(RIGHT('000000000000000'+LTRIM(rtrim(importe_total)),15))
from comprobantes_txt

select*from comprobantes_txt_export

--Activamos el xp_cmdshell-----------------------------------------
sp_configure 'show advanced options', '1'
RECONFIGURE
sp_configure 'xp_cmdshell', '1'
RECONFIGURE

--Exporta el txt
exec xp_cmdshell 'bcp "select uno + dos + tres + cuatro + cinco + seis + siete + ocho FROM FINAL_BDA_CIMARELLI.dbo.comprobantes_txt_export order by uno" queryout "C:\FINAL BDA\Comprobantes_export.txt"  -T -S LENOVO-FABRIZIO\SQLEXPRESS -c -t'
--------------------------------------------------------------------

--Prodedimiento almacendado para exportar comprobantes entre un rango de fechas determinados
create procedure spu_exportar_comprobantes
@fecha_ini as nvarchar(10),
@fecha_fin as nvarchar(10)
as
delete from comprobantes_txt 
delete from comprobantes_txt_export

insert into comprobantes_txt
	select fecha=CONVERT(varchar(8),fecha,112),
	id_tipo_comp=(RIGHT('000'+LTRIM(rtrim(id_tipo_comp)),3)),
	nro_comp=(RIGHT('00000000000000000000'+LTRIM(rtrim(id_tipo_comp)),20)),
	nro_comp=(RIGHT('00000000000000000000'+LTRIM(rtrim(id_tipo_comp)),20)),
	id_tipo_doc=(RIGHT('00'+LTRIM(rtrim(id_tipo_comp)),2)),
	nro_documento=(RIGHT('00000000000000000000'+LTRIM(rtrim(nro_documento)),20)),
	nombre_or_razon=LEFT(convert(varchar,nombre_or_razon) + '                              ',30),
	importe=REPLACE(importe,'.','')--(RIGHT('000000000000000'+LTRIM(rtrim(importe)),15))
	from comprobantes
	inner join clientes on clientes.id_cliente=comprobantes.id_cliente
WHERE fecha between @fecha_ini and @fecha_fin
order by fecha asc

insert into comprobantes_txt_export
	select
	fecha_comprob,
	tipo_comprob, 
	num_comprob_desde,
	num_comprob_hasta,
	id_doc_comprador,
	nro_doc_comprador,
	nombre_or_razon,
	importe_total=(RIGHT('000000000000000'+LTRIM(rtrim(importe_total)),15))
	from comprobantes_txt

exec xp_cmdshell 'bcp "select uno + dos + tres + cuatro + cinco + seis + siete + ocho FROM FINAL_BDA_CIMARELLI.dbo.comprobantes_txt_export order by uno" queryout "C:\FINAL BDA\Comprobantes_export.txt"  -T -S LENOVO-FABRIZIO\SQLEXPRESS -c -t'


GO

--ejecuta el proc
exec spu_exportar_comprobantes '01/01/2013','02/02/2021'

