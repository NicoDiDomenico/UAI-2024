# Si el módulo se encuentra en una subcarpeta: 
from funciones_buenas.saludar import saludar
# from  (subcarpeta) .(módulo)import (función)
saludo = saludar('Nico')
print(saludo)
print()

# Notar como cambia el print(__name__) al usar un módulo ubicado en la carpeta Enrutamiento y no en una subcarpeta del mismo
from módulo_saludar import saludar
saludo = saludar('Nico')
print(saludo)
print()