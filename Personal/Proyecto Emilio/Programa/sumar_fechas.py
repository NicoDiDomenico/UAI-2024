import os
import re
from datetime import datetime, timedelta
from docx import Document

# Carpeta de entrada y salida
CARPETA = r"C:\Users\ecost\Desktop\Proyecto Emilio\Words"
CARPETA_NUEVA = r"C:\Users\ecost\Desktop\Proyecto Emilio\Words Actualizados"

# Crear carpeta de salida si no existe
os.makedirs(CARPETA_NUEVA, exist_ok=True)

# Mapeo meses en español
MESES = {
    "enero": 1, "febrero": 2, "marzo": 3, "abril": 4,
    "mayo": 5, "junio": 6, "julio": 7, "agosto": 8,
    "septiembre": 9, "setiembre": 9, "octubre": 10,
    "noviembre": 11, "diciembre": 12
}

# Patrón para fechas tipo "02 de noviembre de 2025"
PATRON_FECHA = re.compile(
    r"(\b\d{1,2})\s+de\s+(enero|febrero|marzo|abril|mayo|junio|julio|agosto|septiembre|setiembre|octubre|noviembre|diciembre)\s+de\s+(\d{4})",
    re.IGNORECASE
)

def sumar_30_dias_en_texto(texto):
    """
    Suma 30 días a todas las fechas encontradas en el texto.
    Devuelve el texto nuevo y, si existe, la primera nueva fecha encontrada.
    """
    primera_nueva_fecha = None

    def reemplazar_fecha(match):
        nonlocal primera_nueva_fecha
        dia, mes_texto, anio = match.groups()
        mes_num = MESES[mes_texto.lower()]
        fecha_original = datetime(int(anio), mes_num, int(dia))
        nueva_fecha = fecha_original + timedelta(days=30)

        mes_nuevo = list(MESES.keys())[list(MESES.values()).index(nueva_fecha.month)]
        nueva_fecha_texto = f"{nueva_fecha.day:02d} de {mes_nuevo} de {nueva_fecha.year}"

        # Guardamos solo la primera fecha convertida para usar en el nombre del archivo
        if not primera_nueva_fecha:
            primera_nueva_fecha = nueva_fecha_texto

        return nueva_fecha_texto

    texto_modificado = PATRON_FECHA.sub(reemplazar_fecha, texto)
    return texto_modificado, primera_nueva_fecha

def procesar_word(ruta_archivo):
    doc = Document(ruta_archivo)
    nueva_fecha_archivo = None

    for p in doc.paragraphs:
        nuevo_texto, nueva_fecha = sumar_30_dias_en_texto(p.text)
        p.text = nuevo_texto
        if nueva_fecha and not nueva_fecha_archivo:
            nueva_fecha_archivo = nueva_fecha

    # Si encontramos una fecha nueva, la usamos en el nombre del archivo
    nombre_base = os.path.splitext(os.path.basename(ruta_archivo))[0]
    if nueva_fecha_archivo:
        nombre_archivo = f"{nombre_base} - {nueva_fecha_archivo}.docx"
    else:
        nombre_archivo = f"{nombre_base}_+30dias.docx"

    ruta_salida = os.path.join(CARPETA_NUEVA, nombre_archivo)
    doc.save(ruta_salida)
    print(f"✅ Guardado: {ruta_salida}")

for archivo in os.listdir(CARPETA):
    if archivo.endswith(".docx"):
        procesar_word(os.path.join(CARPETA, archivo))
