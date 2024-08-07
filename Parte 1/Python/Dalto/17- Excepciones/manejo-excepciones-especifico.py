# Es un evento detectado durante la ejecucion que interrumpe el flujo de un programa
try:
    num = int(input('Ingrese numero: '))
    den = int(input('Ingrese numero: '))
    rta = num / den
except ZeroDivisionError as error: # con as guardamos el error en una varaiable
    print('No puedes dividir por cero!')
    print(f'Error: {error}')
except ValueError as error:
    print('Ingresa solo numeros!')
    print(f'Error: {error}')
else:
    print(rta)
finally: # Atrapemos o no una excepcion -> Siempre ejecutaremos el codigo que se encuentre aqui
    print('----------------------')

#Try-except block (Bloque try-except): El código dentro del bloque try es el código que se intentará ejecutar. Si ocurre una excepción durante la ejecución de este código, en lugar de que el programa se detenga, se capturará la excepción y se manejará de acuerdo con lo especificado en el bloque except.
# except #####: Si en lugar de utilizar except Exception: especificas la excepción exacta que esperas manejar, el manejo de excepciones será más preciso y dirigido. Esto te permite capturar y manejar específicamente ciertos tipos de excepciones mientras permites que otras excepciones pasen sin ser manejadas explícitamente.