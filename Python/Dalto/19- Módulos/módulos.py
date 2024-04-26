# Módulos - Es un archivo que contiene codigo .py en donde dentro del mismo  contiene funciones, clases, etc., se utiliza en la programacion modular. Esto es dividir el programa en partes útiles diferentes.

import módulo_saludar # saludar() pasa de ser una función a un método del módulo importado
#             |----> a esto se lo denomina namespace, y es un objeto de tipo módulo. Al crearlo también crea una subcarpeta llamada __pycache__ que lo que hace es hacer que el proceso de ejecución sea mas rápido 
saludo = módulo_saludar.saludar('Nico')
print(saludo)
print()

# As:
# 'Valor' as variable --> Sirve para renombrar los módulos
import módulo_despedir as chau # Muy útil para nombres de módulos muy largos
despedir = chau.despedir('Nico')
print(despedir)
print()

#  Con la siguiente manera podemos usar directamente la función sin tener que escribir el módulo 
# seguido de la función como método
from módulo_preguntar import preguntar1, preguntar2 # *
pregunta1 = preguntar1('Bruno')
pregunta2 = preguntar2('Ramiro')
print(pregunta1)
print(pregunta2)
print()

# Al igual que los módulas, las funciones también las podemos renombrar si usamos import
from módulo_preguntar import preguntar1 as pizza , preguntar2 as pan
pregunta1 = pizza('Bruno')
pregunta2 = pan('Ramiro')
print(pregunta1)
print(pregunta2)
print()

#  Notar que si no usamos from podemos usar todas las funciones del módulo, en cambio con from tenemos 
# que especificar que funciones vamos a utilizar acá... A NO SER que usemos * ver ->(1)
import módulo_preguntar
saludo1 = módulo_preguntar.preguntar1('Nico')
saludo2 = módulo_preguntar.preguntar2('Nico')
print(saludo1)
print(saludo2)
print()

# (1): Usamos * para invocar todas las funciones de determinado módulo
from módulo_preguntar import *
pregunta1 = preguntar1('Bruno')
pregunta2 = preguntar2('Ramiro')
print(pregunta1)
print(pregunta2)
print()
# No hacer esto porque hace que el programa sea pesado. 

# Para ver las propiedades y métodos de el namespace
print(dir(módulo_preguntar)) # Aparecen después de __spec__ 
print()

# Accedemos al nombre del módulo llamado/importado
print(chau.__name__)
print()

# Accedemos al nombre de este módulo
print(__name__)
print() # Arroja main porque el nombre es del que se está ejecutando

# Con esto podemos ver todos los modulos que posee python
help('modules')

