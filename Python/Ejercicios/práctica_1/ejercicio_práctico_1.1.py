'''
    este curso                CRUDO             CRUDO
        |         Mínimo        |   Promedio      |                  Máximo
██------º-----------º-----------º------º----------º---------------------º-----------██
        |           |           |      |          |                     |
        |          2.5 hs       |     4 hs        |                    7 hs
       1.5 hs                  3.5 hs            5 hs
'''
# DATOS DURACIÓN DE LOS CURSOS:
curso_actual = 1.5 # curso que estoy haciendo

otros_cursos_min = 2.5 # duracion mínima de otros cursos que existen en internet
otros_cursos_prom = 4 # duracion promedio de otros cursos que existen en internet
otros_cursos_max = 7 # duracion máxima de otros cursos que existen en internet

prom_en_crudo = 5 # duracion promedio en crudo de otros cursos que existen en internet
actual_en_crudo = 3.5 # duracion en crudo del curso que estoy haciendo
print('-------------')    
# a) Diferencia en porcentaje entre el curso actual y:
print('a)')
# - el más rápido de otros cursos
diferencia_min = 100 - curso_actual / otros_cursos_min * 100
print(f'El curso actual dura {diferencia_min} % menos que el mas rápido')

# - el más lento de otros cursos 
diferencia_max = 100 - curso_actual / otros_cursos_max * 100
print(f'El curso actual dura {round(diferencia_max, 2)} % menos que el mas lento')

# - el promedio de los cursos
diferencia_prom = 100 - curso_actual / otros_cursos_prom * 100
print(f'El curso actual dura {round(diferencia_prom, 2)} % menos que el promedio de los cursos\n')

# b) Porcentaje de material inservible que se reduce en:
print('b)')
# - el promedio de los cursos
prom_reduccion = 100 - otros_cursos_prom / prom_en_crudo * 100
print(f'El {round(prom_reduccion, 2)} % de las horas de un curso promedio correspone a material inservible')

# - el curso actual (este curso)
actual_reduccion = 100 - curso_actual / actual_en_crudo * 100
print(f'El {round(actual_reduccion, 2)} % de las horas del curso actual correspone a material inservible\n')

# c) Ver 10 horas de este curso a cuantas de otros cursos equivale?¿y al revés?
print('c)')
print(f'Ver 10 horas del curso actual equivale a ver {round(otros_cursos_prom / curso_actual * 10, 2)} horas de otros cursos')
print(f'Ver 10 horas de otros cursos equivale a ver {round(curso_actual / otros_cursos_prom * 10, 2)} del curso actual')
print('-------------')
