import random

lista = ['Piedra', 'Papel', 'Tijera']
op = random.choice(lista)

rta = ''
while rta == '':
    jugador = None
    while jugador not in lista:
        jugador = input('¿ Piedra | Papel | Tijera ?: ').capitalize()

    print(f'La maquina hizo: {op}!!')
    if jugador == op:
        print('Empate')
    elif jugador == 'Piedra' and op == 'Papel':
        print('Perdiste')
    elif jugador == 'Papel' and op == 'Piedra':
        print('Ganaste')
    elif jugador == 'Piedra' and op == 'Tijera':
        print('Ganaste')
    elif jugador == 'Tijera' and op == 'Piedra':
        print('Perdiste')
    elif jugador == 'Papel' and op == 'Tijera':
        print('Perdiste')
    elif jugador == 'Tijera' and op == 'Papel':
        print('Ganaste')
    rta = input('Aprete enter para jugar de nuevo | Ingrese cualquier letra para salir: ')
    # Otra forma de dalir del bucle es con un if y un break
    print()