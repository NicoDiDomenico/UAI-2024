números = [0,1,2,3,4]
nombres = ['Nico','Ivan','Juan','Bruno']

lista = list() 
for i in zip(números,nombres):
    print(i)
    lista.append(i)
    
print(lista)

mi_nombre= lista[0][1]
print(mi_nombre)

