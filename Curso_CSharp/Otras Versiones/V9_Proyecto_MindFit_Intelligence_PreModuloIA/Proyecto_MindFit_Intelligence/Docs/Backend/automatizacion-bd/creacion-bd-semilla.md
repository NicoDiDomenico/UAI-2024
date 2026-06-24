Actualmente el endpoint "POST api/Gyms/onboarding" se manda con esta REQ (NuevoGymRequestDto):
{
"nombreGym": "string",
"usuarioMaster": {
"username": "string",
"password": "string",
"personaResponsable": {
"nombre": "string",
"apellido": "string",
"email": "string",
"telefono": "string",
"direccion": "string",
"ciudad": "string",
"tipoDocumento": "string",
"nroDocumento": "string",
"genero": "Masculino",
"fechaNacimiento": "2026-06-20T19:30:38.405Z"
}
}
}

Pero no hace mas que guarda cada dato que tare el dto en las entidades correspondeintes de MindFitMasterContext en GymRepository y luego manualmente creo una nueva BD con los datos del Gym y el usuario nuevo dado de alta en la BD. Quisiera automaizar esto.
