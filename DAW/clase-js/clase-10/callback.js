//callback: las funciones callback son una forma de asegurarnos que un determinado codigo no se ejecute hasta que otro codigo haya terminado de ejecutarse

// Sin callback
// JS al ser un lengiaje interpretado se ejecutara de arriba hacia abajo e instantaneamente linea por linea
function primero(){
    console.log("Me ejecuto primero");
};
function segundo(){
    console.log("Me ejecuto segundo");
};
primero();
segundo();

// Pero además al ser un lenjuage asincronico JavaScript puede manejar operaciones que no se completan inmediatamente, como solicitudes de red, temporizadores (setTimeout), operaciones de entrada/salida, etc. Estas operaciones no bloquean la ejecución del código subsiguiente.
function primero(){
    setTimeout(function(){
        console.log("Me ejecuto primero");
    }, 3000);
};
function segundo(){
    console.log("Me ejecuto segundo");
};
primero(); // Se mostrará después de segundo() para no bloquear el flujo
segundo();

// Con callback - las funciones callback nos permiten usar o recibir como parametros a otra funcion. Entonces paso la funcion A como parametro de la funcion B y luego llamamos a la funcion A dentro de B. Basicamente es ejecutar una porcion de codigo cuando la otra haya terminado de ejecutarse.
function primero(segundo){
    setTimeout(function(){
        console.log("Me ejecuto primero");
        segundo(); // llamo acá a la funcion que pasé como parametro
    }, 3000);
};
function segundo(){
    console.log("Me ejecuto segundo");
};
primero(segundo); // Ahora si, al ser una funcion callback, se respeta el orden de ejecucion que yo quiero. (De forma Sincronizada y no asincronica como funciona JS)