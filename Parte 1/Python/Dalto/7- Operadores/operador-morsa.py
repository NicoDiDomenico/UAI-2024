# 51 - Operador Walrus, expresion de asignacion, -> :=
feliz = True
print(feliz)

# Esto no se puede hacer
#print(feliz = True)

# Entonces usamos Walrus
print(feliz := True)


# Ejemplo practico
comidas = []
while True:
    comida = input('¿Comida favorita?: ')
    if comida == "salir":
        break
        comidas.append(comida)
print()
# Mas optimo con Walrus
comidas2 = list()
while(comida2 := input('¿Comida favorita?: ')) != 'salir':
    comidas2.append(comida2)

