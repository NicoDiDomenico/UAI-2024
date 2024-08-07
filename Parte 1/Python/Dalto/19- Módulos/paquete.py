# Un paquete es una carpeta con varios archivos/módulos python
#  - No es lo mismo importar muchos módulos que un solo paquete on todos los necesarios, recordemos que un 
# programa bien hecho es aquel que tiene módulos ya que c/u es una porcion de códico.
# - Python entiende que una carpeta con módulos es un paquete si se crea un archivo vacio que se llame: __init__.py,
# de esta forma si un módulo no tiene __init__.py se dará cuenta que está ante un módulo y no un paquete.

import Paquete.módulo_saludar_raro, Paquete.módulo_saludar

#print(type(Paquete)) # aunque arroje que es módulo teóricamente es un paquete
#print(Paquete.__path__) # encuentro la ruta del paquete

print(Paquete.módulo_saludar.saludar('Nico'))
print(Paquete.módulo_saludar_raro.saludar_raro('Nico'))

# Puedo tener subpaquetes <=>  la carpeta principal tanto como la subcarpeta tienen el __init__.py para 
# convertir ambos en paquetes, sintaxis: import Paquete.Subpaquete.módulo_saludar_raro

