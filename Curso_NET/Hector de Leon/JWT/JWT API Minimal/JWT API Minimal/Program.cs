var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!"); 
// Para proteger mis endpoints tengo que utilizar JWT
/*
JWT me va a especificar quien eres y cuales son tus privilegios, 
pero todo esto encriptado y codificado con un algoritmo. Al ver 
este token no se podr� identifcar qu� es lo que hay dentro.
// Me qued� en 
// Me qud� en min 2:50
app.Run();
