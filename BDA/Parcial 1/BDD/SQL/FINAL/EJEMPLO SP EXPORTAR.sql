create procedure [dbo].[spu_LIBRO_IVA_VENTAS_CBTE]
@fecha_ini as nvarchar(10),
@fecha_fin as nvarchar(10)
as
delete from REGINFO_CV_VENTAS_CBTE 
delete from REGINFO_CV_VENTAS_CBTE_TXT
INSERT INTO REGINFO_CV_VENTAS_CBTE
	select fecha_cmp=convert(varchar(8),fecha_cmp,112),
	codigo_afip=(RIGHT('000' + Ltrim(Rtrim(codigo_afip)),3)),
	punto_venta=(RIGHT('00000' + Ltrim(Rtrim(punto_venta)),5)),
	cbte_nro=(RIGHT('00000000000000000000' + Ltrim(Rtrim(cbte_nro)),20)),
	cbte_nro=(RIGHT('00000000000000000000' + Ltrim(Rtrim(cbte_nro)),20)),
	tipo_doc_afip_cf,
	nro_doc_cf=(RIGHT('00000000000000000000' + Ltrim(Rtrim(nro_doc_cf)),20)),
	razon_social_cf=LEFT(convert(varchar,razon_social_cf) + '                              ',30),
	total=REPLACE(total,'.',''),
	imp_neto_nogravado,
	ONCE,
	DOCE,
	TRECE,
	CATORCE,
	QUINCE,
	DIECISEIS,
	moneda,
	moneda_ctz,
	cant_alicuotas,
	codigo_operacion_afip,
	VEINTIUNO=REPLACE(veintiuno,'.',''),
	VEINTIDOS
	from comprobantes_fiscales
	WHERE fecha_cmp between @fecha_ini and @fecha_fin
	order by fecha_cmp asc
insert into REGINFO_CV_VENTAS_CBTE_TXT
	select uno,
	dos=(RIGHT('000' + Ltrim(Rtrim(dos)),3)),
	tres=(RIGHT('00000' + Ltrim(Rtrim(tres)),5)),
	cuatro,
	cinco,
	seis,
	siete,
	ocho,
	nueve=(RIGHT('000000000000000' + Ltrim(Rtrim(nueve)),15)),
	diez=(RIGHT('000000000000000' + Ltrim(Rtrim(diez)),15)),
	once=(RIGHT('000000000000000' + Ltrim(Rtrim(once)),15)),
	doce=(RIGHT('000000000000000' + Ltrim(Rtrim(doce)),15)),
	trece=(RIGHT('000000000000000' + Ltrim(Rtrim(trece)),15)),
	catorce=(RIGHT('000000000000000' + Ltrim(Rtrim(catorce)),15)),
	quince=(RIGHT('000000000000000' + Ltrim(Rtrim(quince)),15)),
	dieciseis=(RIGHT('000000000000000' + Ltrim(Rtrim(dieciseis)),15)),
	diecisiete,
	dieciocho,
	diecinueve,
	veinte=' ',
	veintiuno=(RIGHT('000000000000000' + Ltrim(Rtrim(veintiuno)),15)),
	veintidos=(RIGHT('00000000' + Ltrim(Rtrim(veintiuno)),8))
	from REGINFO_CV_VENTAS_CBTE

exec xp_cmdshell 'bcp "select uno + dos + tres + cuatro + cinco + seis + siete + ocho + nueve + diez + once + doce + trece + catorce + quince + dieciseis + diecisiete + dieciocho + diecinueve + veinte + veintiuno + veintidos FROM nombre_bd.dbo.nombre_tabla order by cuatro,dos" queryout "C:\ArchivosAfip\LIBRO_IVA_DIGITAL_VENTAS_CBTE.txt"  -T -S NOMBRE-SERVER -c -t'



GO


