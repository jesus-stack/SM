using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryEntrada
    {
        IEnumerable<Entrada> GetEntrada();
        void Save(Entrada Entrada);
    }
}
