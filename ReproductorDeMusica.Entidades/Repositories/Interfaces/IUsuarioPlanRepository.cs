﻿using ReproductorDeMusica.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Entidades.Repositories.Interfaces
{
    public interface IUsuarioPlanRepository
    {
        List<Plan> GetListPlan();
        UsuarioPlan AgregarNuevoUsuarioPlan(int idPlan, int idUsuario);
        List<UsuarioPlan> GetUsuariosPlansPorUsuario(int idUsuario);
        Usuario ObtenerUsuarioConPlan(int usuarioId);
        UsuarioPlanDTO GetUltimoPlanUsuario(int idUsuario);
        UsuarioPlan RealizarPagoAPremium(int idUsuario);
    }
}
