# Es un evento detectado durante la ejecucion que interrumpe el flujo de un programa
try:
    num = int(input('Ingrese numero: '))
    den = int(input('Ingrese numero: '))
    rta = num / den
    print(rta)
except Exception:
    print('Algo salió mal!')

#Try-except block (Bloque try-except): El código dentro del bloque try es el código que se intentará ejecutar. Si ocurre una excepción durante la ejecución de este código, en lugar de que el programa se detenga, se capturará la excepción y se manejará de acuerdo con lo especificado en el bloque except.
# except Exception: Este bloque captura cualquier excepción que pueda ocurrir dentro del bloque try. Exception es la clase base para todas las excepciones en Python. Capturarlo de esta manera significa que manejará cualquier tipo de excepción que ocurra dentro del bloque try.