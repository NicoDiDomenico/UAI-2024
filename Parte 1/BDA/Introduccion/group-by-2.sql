select nombre as Materias
from Materia m
group by m.id_estudiante
/*
ERROR: Column 'Materia.nombre' is invalid in the select list because it is not contained in either an aggregate function or the GROUP BY clause.
SOLUCI�N: Este error en SQL Server, el error 8120, generalmente ocurre cuando est�s utilizando una cl�usula GROUP BY en una consulta y est�s 
seleccionando columnas que no est�n incluidas en la cl�usula GROUP BY ni est�n dentro de una funci�n de agregado como SUM, COUNT, AVG, etc.
*/