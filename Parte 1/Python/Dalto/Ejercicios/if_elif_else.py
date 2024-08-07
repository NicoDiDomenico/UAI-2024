# https://www.mclibre.org/consultar/python/ejercicios/ej-if-else.html

# 1 Divisor de números
# Escriba un programa que pida dos números enteros y que calcule su división, 
# escribiendo si la división es exacta o no.

def main(): #esto no lo entiendo
    print("DIVISOR DE NÚMEROS")
    dividendo = int(input("Escriba el dividendo: ")) 
    divisor = int(input("Escriba el divisor: "))
    #La función input toma la entrada del usuario como una cadena de texto, y 'int' la convierte a un número entero.
    
    cociente = dividendo // divisor  # Calcula el cociente
    
    if (dividendo % divisor == 0) & (dividendo == divisor * cociente):
        #print(f"La división es exacta. Cociente: {dividendo // divisor}") #MEJOR FORMA
        #print('La división es exacta.')
        #print('El cociente es ' + str(dividendo // divisor))
        print('La división es exacta.\n'
              'El cociente es ' + str(dividendo // divisor)
              )
    else:
        print(
            f"La división no es exacta. Cociente: {dividendo // divisor}\n "
            f"Resto: {dividendo % divisor}"
        )

if __name__ == "__main__": #esto no lo entiendo
    main() #esto no lo entiendo
