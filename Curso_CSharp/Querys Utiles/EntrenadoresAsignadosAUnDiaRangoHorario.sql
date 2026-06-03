use MindFitIntelligence;

SELECT 
    drh.IdDiaRangoHorario, 
    CONCAT(LEFT(CONVERT(VARCHAR, rh.HoraDesde, 108), 5), ' - ', LEFT(CONVERT(VARCHAR, rh.HoraHasta, 108), 5)) AS Rango_Horario, 
    drh.CupoMaximo, 
    d.NombreDia, 
	pr.IdUsuario,
	CONCAT(pr.Nombre,' ',pr.Apellido ) AS Entrenador
FROM DiaRangoHorario drh
INNER JOIN dia d ON drh.IdDia = d.IdDia
INNER JOIN RangoHorario rh ON drh.IdRangoHorario = rh.IdRangoHorario
INNER JOIN DiaRangoHorarioResponsable drhr ON drh.IdDiaRangoHorario = drhr.IdDiaRangoHorario
INNER JOIN PersonaResponsable pr ON drhr.IdUsuarioResponsable = pr.IdUsuario
WHERE d.NombreDia LIKE 'Sábado';