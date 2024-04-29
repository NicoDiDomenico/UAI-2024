select nombre as Materias
from Materia m
group by m.id_estudiante
/*
ERROR: Column 'Materia.nombre' is invalid in the select list because it is not contained in either an aggregate function or the GROUP BY clause.
SOLUCIÓN: Este error en SQL Server, el error 8120, generalmente ocurre cuando estás utilizando una cláusula GROUP BY en una consulta y estás 
seleccionando columnas que no están incluidas en la cláusula GROUP BY ni están dentro de una función de agregado como SUM, COUNT, AVG, etc.
*/