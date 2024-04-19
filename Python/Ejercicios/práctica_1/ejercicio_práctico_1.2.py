'''
    1 segundo          ¿Cuantos segundos?
██------º----------------------º--------------██
        |                      |                            
    2 palabras             x palabras
'''
print('-----------------')
# a) Pedirle al usuario que diga cualquier texto real y:
# - calcular cuanto tardaría en decir esa frase  
texto = input("Ingrese un texto: ")
palabras = texto.split(' ')
print(palabras)
cant_palabras = len(palabras) 
print(f'Tardaste {cant_palabras / 2 * 1} segundos en decir esa frase.')
# - ¿Cuantas palabras dijo?
print(f'Dijiste {cant_palabras} palabras.\n')
print('-----------------')

# b) si se tarda más de 1 minuto:
# - decirle: "para flaco tampoco te pedí un testamento".
texto = input("Ingrese un texto: ")
palabras = texto.split(' ')
print(palabras)
cant_palabras = len(palabras) 
tiempo_en_decirlas = cant_palabras / 2 * 1
if tiempo_en_decirlas <= 60 :
    print(f'Tardaste {cant_palabras / 2 * 1} segundos en decir esa frase.')
    print(f'Dijiste {cant_palabras} palabras.')
else:
    print('para flaco tampoco te pedí un testamento')

# c) Dalto habla un 30% más rápido: ¿Cúanto tardaría él en decirlo?
print('-----------------')
#print(f'Dalto tardaria {round(cant_palabras / 2.6 * 1, 2)} segundos en decir esa frase.')
print(f'Dalto tardaria {round(cant_palabras / 2 * 0.7, 2)} segundos en decir esa frase.')