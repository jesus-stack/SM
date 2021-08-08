﻿using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTipoUsuario : IServiceTipoUsuario
    {
        public IEnumerable<TipoUsuario> GetTipoUsuarios()
        {
            IRepositoryTipoUsuario repository = new RepositoryTipoUsuario();
            return repository.GetTipoUsuarios();
        }
    }
}
