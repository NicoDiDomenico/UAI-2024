# 1)
# Datos:
#   1 Alumno es profesor 
#   1 Alumno es asistente

# A) Pedir la edad de los compañeros que vinieron hoy a clase y ordenar los datos de menor a mayor 
# B) El mayor  es el profesor y el menor es el asistente: ¿Quien es quien?

# Función para obtener al sistente y al profesor según la edad.
def armar_lista(cant):
    
    # Creando la lista con los compañeros 
    compañeros = list() 
    
    # Ejecutando un for para pedir información de cada compañero
    for a in range(cant):
        nombre = input(f'Nombre del alumno {a+1}°: ')
        edad = int(input(f'Edad del alumno {a+1}°: '))
        
        # Agregando la información a la lista 
        compañeros.append((nombre,edad)) 
        
    # Ordenando de menor a mayor según su edad
    lista_nombre_edad.sort(key = lambda x : x[1]) 
    
    return compañeros

cant_alumnos = int(input('¿Cuántos alumnos desea agregar?: '))
lista_nombre_edad = armar_lista(cant_alumnos)

# lista_nombre_edad[0] accedemos al primer valor de la lista, lista_nombre_edad[0][0] accedemos al primer valor de la lista/tupla que está dentro de la lista principal
print(f'El profesor designado es {lista_nombre_edad[-1][0]} de {lista_nombre_edad[-1][1]}')
print(f'El asistente designado es {lista_nombre_edad[0][0]} de {lista_nombre_edad[0][1]}')
