use MindFitIntelligence;
select 
	t.IdTurno,
	d.NombreDia, 
	cf.Fecha, 
	CONCAT(cf.CupoActual,'/',drh.CupoMaximo) AS Cupos, 
	LEFT(CONVERT(VARCHAR, rh.HoraDesde, 108), 5) AS Hora,
	CONCAT(r.Nombre, ' ', r.Apellido) AS Entrenador,
	CONCAT(s.Nombre, ' ', s.Apellido) AS Socio, 
	t.EstadoTurno 
from Turno t
inner join PersonaResponsable r
on t.IdUsuarioResponsable = r.IdUsuario
inner join PersonaSocio s
on t.IdUsuarioSocio = s.IdUsuario
inner join CupoFecha cf
on t.IdCupoFecha = cf.IdCupoFecha
inner join DiaRangoHorario drh
on cf.IdDiaRangoHorario = drh.IdDiaRangoHorario
inner join RangoHorario rh
on drh.IdRangoHorario = rh.IdRangoHorario
inner join Dia d
on drh.IdDia = d.IdDia;