'''
Estructura:
if condicion:
    accion
'''
edad = int(input('¿Cuantos años tienes? '))
if edad >= 18:
    print('Sos legal pibe')
else:
    print('Sos menor wachin')
    
contraseña_ingresada = 'Roberto123'
contraseña_correcta = 'Robrto123'
if contraseña_ingresada == contraseña_correcta:
    print('Contraseña correcta')
else:
    print('Contraseña incorrecta')