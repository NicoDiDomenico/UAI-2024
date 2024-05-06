# TP01 - Ejercicio Clubes:

# Luego de recabar datos de los socios en cada uno de los 17 clubes
# más importantes de la ciudad se quiere determinar, para cada una de ellos,
# entre los socios censados mayores de edad (tienen 18 años o más) quienes son
# más numerosos, los que son socios temporales (código 1) o los que son
# socios permanentes (código 2).
# Para resolver esto se dispone, por cada socio de cada uno de los clubes,
# su código de asociado (1 para temporal, 2 para permanente) y edad.
# Un código de asociado 0 (cero) indica que no hay más datos de ese club.
# Validar el código entre 0, 1 y 2; no permita otros valores.
# Validar la edad que no sea negativa y reconfirme (¿esta seguro?)
# si es mayor a 100.

# Utilizar funciones,listas para guardar los datos (por ejemplo para la edad y socios), no vamos a usar objetos, usar while para validar.

# TP01 - Nicolás Alejandro Di Domenico
def validar_codigo(codigo):
    while codigo != '0' and codigo != '1' and codigo != '2':
        codigo = input("Ingrese un código válido: ")
    return codigo

def validar_edad(edad):
    confirmacion = None
    while edad < 0 or edad > 100:
        while confirmacion != 's' and confirmacion != 'n':
            confirmacion = input("¿Está seguro de que la edad es correcta? (s/n): ").lower()
        if confirmacion == 's':
            break
        else:
            edad = int(input("Ingrese la edad del socio nuevamente: "))
    return edad

def validar_socio(n):
    codigo = input(f"Ingrese el código del socio {n}: ")
    codigo_socio = validar_codigo(codigo) # Chequeamos que sea una opcion entre 0, 1 o 2 y lo retomamos.
    if codigo_socio == '0':
        return None, None, n # Si es 0 retorno none y no ejecuto el resto de codigo para pedir la edad y validarla.
    edad = int(input(f"Ingrese la edad del socio {n}: "))
    edad_socio = validar_edad(edad) # Chequemos que esté entre 0 y 100, si es valida la retornamos.
    return codigo_socio, edad_socio, n

def contar_socios(club):
    temporales = 0
    permanentes = 0
    n = 1
    codigo_socio, edad_socio, n = validar_socio(n) # Acá ya ingreso y valido el código y la edad del socio.
    while codigo_socio is not None:
        if edad_socio >= 18:
            if codigo_socio == '1':
                temporales += 1
            elif codigo_socio == '2':
                permanentes += 1
        print()
        n += 1
        codigo_socio, edad_socio, n = validar_socio(n)
    return temporales, permanentes

def ingresoDatos():
    clubes = list()
    x = 4 # Son 17 clubes -> x = 18
    for i in range(1, x):
        print('----------------------------------------------------------------------------')
        print(f"Club {i} - Ingresar opcion codigo: | (1) Socio temporal | (2) Socio permanente | (0) Salir |")
        temporales, permanentes = contar_socios(i)
        clubes.append((temporales, permanentes))
    return clubes

def procesoDatos(clubes):
    resultados = list()
    for temporales, permanentes in clubes:
        if temporales == 0 and permanentes == 0:
            resultados.append("No existen socios temporales ni permanentes.")
        elif temporales > permanentes:
            resultados.append("Hay más socios temporales que permanentes.")
        elif temporales < permanentes:
            resultados.append("Hay más socios permanentes que temporales.")
        elif temporales == permanentes:
            resultados.append("Hay igual cantidad de socios temporales y permanentes.")
    return resultados

def salidaDatos(resultados):
    i = 1
    for r in resultados:
        print(f"En el club {i}, {r}")
        i += 1


print("Ingreso de datos:")
clubes = ingresoDatos()
print('----------------------------------------------------------------------------')
print(clubes)

resultados = procesoDatos(clubes)

print("\nPara socios mayores de edad:")
salidaDatos(resultados)

print("\nFin programa.")


