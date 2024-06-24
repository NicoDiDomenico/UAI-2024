select * from clientes
select * from localidades

--sp sistema
sp_help clientes
--sp local
--SELECT
drop procedure spu_mostrarClientes
CREATE PROC spu_mostrarClientes
AS
SELECT nombre,clientes.cp,localidad FROM clientes
inner join localidades on clientes.cp = localidades.cp

exec spu_mostrarClientes

select * from sys.objects where type = 'P'
select * from sys.syscomments where id = 581577110

create procedure spu_clientesCp
@cp int
as
SELECT nombre,localidad FROM clientes
inner join localidades on clientes.cp = localidades.cp
where clientes.cp = @cp

exec spu_clientesCp 8000
--DELETE
create procedure spu_deleteLocalidades
@cp int
as
delete localidades where cp = @cp
exec spu_deleteLocalidades 4000
--UPDATE
--INSERT
create procedure spu_inertLocalidades
@cp int,
@localidad nvarchar(60)
as
insert into localidades 
values(@cp,@localidad)

exec spu_inertLocalidades '12000',Rafaela

select * from localidades
insert into localidades
values('13000',San Luis)

create procedure [dbo].[spu_LIBRO_LOCALIDADES]
as
exec xp_cmdshell 'bcp "select convert(nvarchar(30),cp) + localidad FROM uai2022.dbo.LOCALIDADES" queryout "C:\UAI\LIBRO_LOCALIDADES.txt"  -T -S tuSERVER -c -t'

GO
EXEC spu_LIBRO_LOCALIDADES


--Activamos las opciones avanzadas requisito indispensable para activar xp_cmdshell
sp_configure 'show advanced options', '1'
--Aplicamos los cambios
RECONFIGURE
--Habilitamos xp_cmdshell
sp_configure 'xp_cmdshell', '1' 
--Aplicamos los cambios
RECONFIGURE


create procedure [dbo].[spu_LIBRO_IVA_COMPRAS_CBTE]
@fecha_ini as nvarchar(10),
@fecha_fin as nvarchar(10)
as
delete from IVA_COMPRAS_CBTE
delete from IVA_COMPRAS_CBTE_TXT
INSERT INTO IVA_COMPRAS_CBTE
	select fecha_cmp=convert(varchar(8),fecha_cmp,112),
	codigo_afip=(RIGHT('000' + Ltrim(Rtrim(codigo_afip)),3)),
	punto_venta=(RIGHT('00000' + Ltrim(Rtrim(punto_venta)),5)),
	cbte_nro=(RIGHT('00000000000000000000' + Ltrim(Rtrim(cbte_nro)),20)),
	despacho_importacion=' ',
	tipo_doc_afip_cf,
	nro_doc_cf=(RIGHT('00000000000000000000' + Ltrim(Rtrim(nro_doc_cf)),20)),
	razon_social_cf=LEFT(convert(varchar,razon_social_cf) + '                              ',30),
	total,
	total_no_gravado,
	total_exento,
	importe_perc_iva,
	importe_perc_impuestos,
	importe_perc_ib,
	importe_impuestos_muni,
	importe_impuestos_internos,
	moneda,
	moneda_ctz,
	cant_alicuotas,
	codigo_operacion_afip,
	credito_fiscal_computable,
	importe_otros_tributos,
	cuit_emisor_corredor=(RIGHT('00000000000000000000' + Ltrim(Rtrim(cuit_emisor_corredor)),11)),
	denominacion_emisor_corredor=LEFT(convert(varchar,denominacion_emisor_corredor) + '                              ',30),
	iva_comision
	from comprobantes_fiscales_r
	WHERE fecha_cmp between @fecha_ini and @fecha_fin
	order by fecha_cmp asc

insert into IVA_COMPRAS_CBTE_TXT
	select uno,
	dos=(RIGHT('000' + Ltrim(Rtrim(dos)),3)),
	tres=(RIGHT('00000' + Ltrim(Rtrim(tres)),5)),
	cuatro,
	cinco=LEFT(convert(varchar,cinco) + '                ',16),
	seis,
	siete,
	ocho,
	nueve=(RIGHT('000000000000000' + Ltrim(Rtrim(REPLACE(nueve,'.',''))),15)),
	diez=(RIGHT('000000000000000' + Ltrim(Rtrim(REPLACE(diez,'.',''))),15)),
	once=(RIGHT('000000000000000' + Ltrim(Rtrim(REPLACE(once,'.',''))),15)),
	doce=(RIGHT('000000000000000' + Ltrim(Rtrim(REPLACE(doce,'.',''))),15)),
	trece=(RIGHT('000000000000000' + Ltrim(Rtrim(REPLACE(trece,'.',''))),15)),
	catorce=(RIGHT('000000000000000' + Ltrim(Rtrim(REPLACE(catorce,'.',''))),15)),
	quince=(RIGHT('000000000000000' + Ltrim(Rtrim(REPLACE(quince,'.',''))),15)),
	dieciseis=(RIGHT('000000000000000' + Ltrim(Rtrim(REPLACE(dieciseis,'.',''))),15)),
	diecisiete,
	dieciocho,
	diecinueve,
	veinte=' ',
	veintiuno=(RIGHT('000000000000000' + Ltrim(Rtrim(REPLACE(veintiuno,'.',''))),15)),
	veintidos=(RIGHT('000000000000000' + Ltrim(Rtrim(REPLACE(veintidos,'.',''))),15)),
	veintitres=(RIGHT('00000000000' + Ltrim(Rtrim(REPLACE(veintitres,'.',''))),11)),
	veinticuatro=LEFT(convert(varchar,veinticuatro) + '                              ',30),
	veinticinco=(RIGHT('000000000000000' + Ltrim(Rtrim(REPLACE(veinticinco,'.',''))),15))
	from IVA_COMPRAS_CBTE

exec xp_cmdshell 'bcp "select uno + dos + tres + cuatro + cinco + seis + siete + ocho + nueve + diez + once + doce + trece + catorce + quince + dieciseis + diecisiete + dieciocho + diecinueve + veinte + veintiuno + veintidos + veintitres + veinticuatro + veinticinco FROM tuBD.dbo.IVA_COMPRAS_CBTE_TXT order by uno" queryout "C:\ArchivosAfip_CTDIAZ\LIBRO_IVA_DIGITAL_COMPRAS_CBTE.txt"  -T -S tuSERVER -c -t'

GO


