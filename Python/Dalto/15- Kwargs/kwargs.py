# KWARGS - Keyword arguments (Argumentos de 'Palabra clave')
# Es un parametro que empaquetará todos los argumentos en un diccionario.
# Es muy util para que una funcion pueda aceptar una cantidad variable de argumento de palabra clave.
# Similar a ARGS, excepto que con estos aceptamos una cantidad variable de argumentos posicionales y los empaquetamos en una tupla, con los KWARGS, aceptamos una cantidad variable de argumentos de palabra clave y los empaquetamos en un diccionario.

def hola(**kwargs):
    #print('Hola '+ kwargs['nombre'] + ' ' + kwargs['apellido'])
    print('Hola ', end='')
    for k, v in kwargs.items():
        print(v, end=' ')
hola(titulo= 'Señor', nombre = 'Alex', apellido = 'Smith', segundo_nombre = 'Python')