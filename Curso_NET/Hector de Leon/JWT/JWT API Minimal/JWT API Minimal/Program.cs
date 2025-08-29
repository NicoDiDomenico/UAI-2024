using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string key = "MiClaveSuperSecreta12345"; // Clave secreta para firmar el token

// Para poder proteger mis endpoints con JWT 
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(opt =>
{
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

    opt.RequireHttpsMetadata = false;

    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = signingKey,
    };
});


var app = builder.Build();

app.MapGet("/", () => "Hello World!");
// Para proteger mis endpoints tengo que utilizar JWT
/*
JWT me va a especificar quien eres y cuales son tus privilegios, 
pero todo esto encriptado y codificado con un algoritmo. Al ver 
este token no se podrá identifcar qué es lo que hay dentro.
// El token se divide en 3 partes;
/*
Header: Aquí va el algoritmo que se está utilizando para encriptar y codificar el token.
Payload: Aquí van los datos que queremos enviar, como el usuario, sus privilegios, etc.
Signature: Aquí va la firma que se genera a partir del header y el payload, utilizando una clave secreta.
*/
app.MapGet("/Protected", () => "Hello World!")
    .RequireAuthorization();

app.Run();

// Para instalar JWT: dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.*
// Para generar tokens JWT con un manejador de tokkens: dotnet user-jwts create
// ME QUEDÉ EN EL 11:32 PERO NO VOY A SEGUIR EL VIDEO, MEJOR ME COMPRO UN CURSO QUE YA ENSEÑE SOBRE JWT. SUERTE LOCO!!