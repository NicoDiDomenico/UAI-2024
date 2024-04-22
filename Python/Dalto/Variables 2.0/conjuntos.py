# Creando un conjunto con set()
print('-----------o-----------')
conjunto = set(['Dato1','Dato2'])
print(conjunto)
print('-----------o-----------')
# Agregar un conjunto dentro de otro conjunto
conjunto1 = frozenset(['dato1, dato2']) # Recordar que los conjuntos son mutables => con frozenset() podemos hacerlo inmutable
conjunto2 = {conjunto1, 'dato3'}
print(conjunto2)
print('-----------o-----------')
# Teoria de conjuntos
# Subconjuntos
conjuntoA = {1,2,3,4,5,6,7,8,9,10}
conjuntoB = {1,5,9}
# Subconjuntos
rta = conjuntoB.issubset(conjuntoB)
print(f'Es B = {conjuntoB} un subconjunto de A = {conjuntoA}: {rta}')
# Superconjuntos
rta2 = conjuntoA.issuperset(conjuntoA)
print(f'Es A = {conjuntoA} un superconjunto de B = {conjuntoB}: {rta2}')
# Conjunto Disjunto - son aquellos conjuntos que no tiene elementos en común
conjuntoC = {2,6,8}
rta3 = conjuntoB.isdisjoint(conjuntoA) 
print(f'Es B = {conjuntoB} un conjunto disjunto de A = {conjuntoA}: {rta3}') 
rta4 = conjuntoC.isdisjoint(conjuntoB) 
print(f'Es B = {conjuntoB} un conjunto disjunto de C = {conjuntoC}: {rta4}') 
print('-----------o-----------')