select apellido as Apellido, nombre as Nombre, (year(CURRENT_TIMESTAMP) - year(fecha_nac)) AS Edad
from Estudiante e
order by (year(CURRENT_TIMESTAMP) - year(fecha_nac)) ASC