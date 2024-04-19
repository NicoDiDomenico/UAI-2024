# Si el módulo estuviera dentro de una carpeta en la misma ruta:
# import funciones_buenas.saludar

# Si el módulo estuviese afuera de la carpeta.
# Sys:
import sys
print(sys) # -> <module 'sys' (built-in)> // built-in = Integrado en python

print(sys.builtin_module_names) # Devuelve todos los módulo integrados a python que se pueden importar
                                # - Es importante tener en cuenta que si tenemos un módulo con el mismo 
                                #nombre no se va a importar el nuestro sino el que está integrado a python
              
# Path - nos permite ver la ruta actual, la ruta de los módulos instalados de python, y rutas alternativas.                  
print(sys.path)

sys.path.append('C:\\Users\\Nicol\\Desktop\\Curso Python\\funciones_buenas') # Ruta donde se aloja wl archivo saludar.py
print(sys.path)

import saludar as módulo_saludar
print(módulo_saludar.funcion_saludar('Emilio'))

# Ejercicio propuesto: haer una calculadora cuyas funciones estén en distintos módulos
# 5:47:06 / 8:06:29