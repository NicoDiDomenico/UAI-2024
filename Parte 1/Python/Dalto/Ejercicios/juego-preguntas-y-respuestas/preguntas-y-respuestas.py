#-----------------------------------
def new_game():
    respuestas = []
    respuestas_correctas = 0
    pregunta_num = 0

    for key in preguntas:
        print('---------------------------')
        print(key)
        for i in opciones[pregunta_num]:
            print(i)

        respuesta = input('Ingrese (A, B, C o D): ').upper()
        respuestas.append(respuesta)

        respuestas_correctas += check_answer(preguntas.get(key), respuesta)
        pregunta_num += 1

    display_score(respuestas_correctas, respuestas)
#-----------------------------------
def check_answer(respuesta_correcta, respuesta):
    if respuesta_correcta == respuesta:
        print('Correcto!')
        return 1
    else:
        print('Incorrecto!')
        return 0
#-----------------------------------
def display_score(respuestas_correctas, respuestas):
    print('---------------------------')
    print('RESULTADO')
    print('---------------------------')

    print('Respuestas Correctas: ', end='')
    for i in preguntas:
        print(preguntas.get(i), end=' ')
    print()

    print('Tus Respuestas: ', end='')
    for i in respuestas:
        print(i, end=' ')
    print()

    puntaje = int((respuestas_correctas/len(preguntas))*100)
    print(f'Puntaje: {puntaje} %')
#-----------------------------------
def play_again():
    respusta = input('¿Quieres jugar de nuevo? (SI o NO): ').upper()

    if respusta == 'SI':
        return True
    else:
        return False

#-----------------------------------
preguntas = {
    "¿Cuál es la capital de Francia?": "A",
    "¿Quién escribió 'Don Quijote de la Mancha'?": "B",
    "¿Cuál es el símbolo químico del oro?": "C",
    "¿Cuál es el planeta más grande del sistema solar?": "D",
}
opciones = [["A. París", "B. Londres", "C. Roma", "D. Madrid"],
            ["A. William Shakespeare", "B. Miguel de Cervantes", "C. Gabriel García Márquez", "D. Pablo Neruda"],
            ["A. Fe", "B. Ag", "C. Au", "D. Cu"],
            ["A. Urano", "B. Saturno", "C. Neptuno", "D. Júpiter"]]

new_game()

while play_again():
    new_game()

print('FIN')