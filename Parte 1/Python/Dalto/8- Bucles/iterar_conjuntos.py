# iterar conjuntos
nombres = set(['Ivan', 'Bruno', 'Nico'])

for i,v in enumerate(nombres): # para iterar conjuntos se debe usar <=> enumerate()
    print(f'{i+1}:{v}')