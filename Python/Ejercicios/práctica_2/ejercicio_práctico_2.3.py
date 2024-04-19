# Creando una función que muestre la serie fibonacci entre 0 y el número dado
def fibinacci(x):
    lista = list()
    j,k = 0,1
    for i in range(1,x):
        if (k) <= x:
            lista.append(k)
            #j2 = j
            #j = k
            #k = j2+k
            j, k = k, j+k
    return lista

serie_de_fibonacci = fibinacci(1000)
print([0, *serie_de_fibonacci]) 