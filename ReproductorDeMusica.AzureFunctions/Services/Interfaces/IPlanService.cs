﻿using ReproductorDeMusica.AzureFunctions.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.AzureFunctions.Services.Interfaces
{
    public interface IPlanService
    {
        Task<Plan> ObtenerPlanPorId(int id);
    }
}