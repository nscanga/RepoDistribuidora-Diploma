using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUsuarioService<T>
    {
        Usuario Login(string email, string password);
        void Add(T obj);  // Método añadido
    }

}
