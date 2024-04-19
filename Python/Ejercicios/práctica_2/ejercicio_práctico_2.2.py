# Creando una función que nos devuelva los números primos entre 0 y el argumento que pasamos.

# Crear una función que verifique si un número es primo  
def es_primo(n):
    # Verificamos que el número pasado no pueda dividirse por ningún número entre 2 y ese mismo número -1
    for i in range(2,n-1):
        # Si es divisible por alguno retomamos false y termina el bucle 
        print(i)
        if n%i == 0: 
            return False
    # Si termina el bucle, significa que no fue divisible entonces es primo  
    return True

# Creando una funcion que retome una lista con todos los primos 
def agrego_primos(nro_max):
    # Creamos la lista 
    lista_nros = list()
    for n in range(3,nro_max+1):
        # Verificando si el valor es primo 
        rta = es_primo(n)
        # En caso de que sea lo agregamos a la lista 
        if rta == True:
            lista_nros.append(n)
    
    # Devolvemos la lista 
    return lista_nros

#   Llamamos a la función y mostramos el resultado
lista = agrego_primos(10)
print(lista)
