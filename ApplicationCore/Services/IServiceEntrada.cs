﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public  interface IServiceEntrada
    {
        IEnumerable<Entrada> GetEntrada();
        void Save(Entrada e);
    }
}
