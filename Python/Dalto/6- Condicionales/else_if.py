#ifs anidados
ingreso_mensual = 500
if ingreso_mensual > 1000: 
    print('Tenes plata')
if ingreso_mensual > 10000:
    print('Tenes mucha plata')
else:
    print('Sos pobre')

print()    
#else-if (elif)
if ingreso_mensual > 10000:
    print('Tenes mucha plata')  
elif ingreso_mensual > 1000:
    print('Tenes plata')
else:
    print('Sos pobre')

print()
edad = 10
if edad == 100:
    print('Tienes un siglo de vida')
elif edad >= 18:
    print('Eres mayor de edad')
else:
    print('Eres menor de edad')