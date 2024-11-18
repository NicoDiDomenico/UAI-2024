class Socio {
  constructor({
    idUsuario, idGimnasio, nombreApellido, fechaNacimiento, genero, nroDocumento, ciudad,
    direccion, telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta,
    plan, estadoSocio, fechaInicioActividades, fechaFinActividades, fechaNotificacion, respuestaNotificacion
  }) {
    this.idUsuario = idUsuario;
    this.idGimnasio = idGimnasio;
    this.nombreApellido = nombreApellido;
    this.fechaNacimiento = fechaNacimiento;
    this.genero = genero;
    this.nroDocumento = nroDocumento;
    this.ciudad = ciudad;
    this.direccion = direccion;
    this.telefono = telefono;
    this.email = email;
    this.obraSocial = obraSocial;
    this.nombreUsuario = nombreUsuario;
    this.contrasena = contrasena;
    this.pregunta = pregunta;
    this.respuesta = respuesta;
    this.plan = plan;
    this.estadoSocio = estadoSocio;
    this.fechaInicioActividades = fechaInicioActividades;
    this.fechaFinActividades = fechaFinActividades;
    this.fechaNotificacion = fechaNotificacion;
    this.respuestaNotificacion = respuestaNotificacion;
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
  