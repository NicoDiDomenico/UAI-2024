class Socio {
    constructor({
      id, nombre, fechaNacimiento, genero, dni, direccion, telefono, email,
      obraSocial, diasAsistencia, nombreUsuario, contrasena, preguntaSeguridad,
      respuestaSeguridad, plan, estado = 'Nuevo', fechaInicioActividades, fechaFinActividades
    }) {
      this.id = id;
      this.nombre = nombre;
      this.fechaNacimiento = fechaNacimiento;
      this.genero = genero;
      this.dni = dni;
      this.direccion = direccion;
      this.telefono = telefono;
      this.email = email;
      this.obraSocial = obraSocial;
      this.diasAsistencia = diasAsistencia;
      this.nombreUsuario = nombreUsuario;
      this.contrasena = contrasena;
      this.preguntaSeguridad = preguntaSeguridad;
      this.respuestaSeguridad = respuestaSeguridad;
      this.plan = plan;
      this.estado = estado;
      this.fechaInicioActividades = fechaInicioActividades;
      this.fechaFinActividades = fechaFinActividades;
    }
  
    calcularFechas() {
      const fechaInicio = new Date();
      this.fechaInicioActividades = fechaInicio.toISOString().split('T')[0];
  
      const fechaFin = new Date(fechaInicio);
      if (this.plan === 'Mensual') {
        fechaFin.setDate(fechaFin.getDate() + 30);
      } else if (this.plan === 'Anual') {
        fechaFin.setDate(fechaFin.getDate() + 365);
      }
      this.fechaFinActividades = fechaFin.toISOString().split('T')[0];
    }
  }
  
  module.exports = Socio;
  