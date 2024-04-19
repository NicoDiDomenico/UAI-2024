#ifs anidados
ingreso_mensual = 1500
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