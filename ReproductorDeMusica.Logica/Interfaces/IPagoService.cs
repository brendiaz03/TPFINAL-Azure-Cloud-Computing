﻿using ReproductorDeMusica.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Logica.Interfaces
{
    public interface IPagoService
    {
        List<Plan> ObtenerTodosLosPlanes();
        void RealizarPago(int idPlan, int idUsuario);
        List<UsuarioPlan> ObtenerPlanesPorUsuarioId(int idUsuario);
    }
}