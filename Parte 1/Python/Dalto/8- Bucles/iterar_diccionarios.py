# Iterar diccionarios 
personas = dict(nombre1 = 'Juan', nombre2 = 'Ivan', nombre3 = 'Pepe')

# Muestra solo las llaves
print('\nMuestra solo las llaves')
for k in personas:
    print(k) # Notar que de esta forma imprime solo las keys
    
# Muestra llave y valor    
print('\nMuestra llave y valor')    
for i,v in personas.items(): # .items() permite que se pueda iterar el diccionario
    print(f'{i}:{v}') 