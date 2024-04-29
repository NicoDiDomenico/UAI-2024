select e.apellido as Apellido, e.nombre as Nombre, m.nombre as Materia
from Estudiante e
right join Materia m -- Caso contrario al left join, los null corresponderian a la tabla izquierda que será la de Estudiante, este no seria el caso.
on e.id = m.id_estudiante
order by e.apellido;