numeros = [4, 7, 75, 2 , 1, 3, 486]
print()

# Encunetra el numero mayor de una lista
numero_mas_alto = max(numeros)
print(f'El numero mas alto es: {numero_mas_alto}')
print()

# Encunetra el numero menor de una lista
numero_mas_bajo = min(numeros)
print(f'El numero mas bajo es: {numero_mas_bajo}')
print()

# Redondear (ya lo use para hacer ejercicios prácticos cuando era que usar un tipo de cálculo)
n = float(input('Ingrese un número con decimales: '))
print(f'El número {n} redondeado vale: {round(n)}')
print()

n2 = int(input('Ingrese cuantos numeros despues de la coma desea redondear: '))
print(f'El número {n} redondeado con {n2} n°s despues de la coma vale: {round(n,n2)}')
print()

# Retorna False <=> (vacio, False, 0, ninguno)
rta = bool(())
print(rta)
rta = bool([])
print(rta)
rta = bool(False)
print(rta)
rta = bool(0)
print(rta)
rta = bool()
print(rta)
rta = bool(None)
print(rta)
print()

# Retorna True <=> (algún valor, True, <> 0, cadena)
rta = bool((1,2,3))
print(rta)
rta = bool([5,3])
print(rta)
rta = bool(True)
print(rta)
rta = bool(-10)
print(rta)
rta = bool('fsdfsdf')
print(rta)
rta = bool('.') # Si pongo '' cuenta como vacio
print(rta)
print()

# Retorna True <=> todos los elementos son verdaderos
rta2 = all([(1,2,3),[5,3],True,-10,'fsdfsdf','.']) # Hay que pasarle un elemento iterable
print(rta2)
rta3 = all([(),[],False,0,'']) 
print(rta3)
rta4 = all([(1,2,3),[5,3],True,-10,'fsdfsdf',None])
print(rta4)
print()

# Suma todos los valores de un iterable
sum_total = sum(numeros)
print(f'La suma total de {numeros} es: {sum_total}\n')
