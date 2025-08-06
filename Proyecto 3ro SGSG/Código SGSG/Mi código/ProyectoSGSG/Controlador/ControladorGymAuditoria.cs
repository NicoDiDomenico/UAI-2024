using System.Data.SqlClient;
using System;
using DAO;
using Modelo;
using System.Collections.Generic;

public static class AuditoriaAccesosService
{
    // private AuditoriaDAO auditoriaDAO = new AuditoriaDAO(); --> No hace falta porque es estática

    public static void RegistrarEvento(int idUsuario, string tipoEvento)
    {
        AuditoriaDAO.RegistrarEvento(idUsuario, tipoEvento);
    }
}
