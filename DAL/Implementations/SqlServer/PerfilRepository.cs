using DAL.Interfaces;
using DAL.Tools;
using Domain;
using Microsoft.Extensions.Logging;
using Services.BaseService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations.SqlServer
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly BitacoraService _bitacoraService;

        public PerfilRepository(BitacoraService bitacoraService)
        {
            _bitacoraService = bitacoraService;
        }
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Perfil] (IdPerfil, TipoPerfil, descripcion) VALUES (@IdPerfil, @TipoPerfil, @descripcion)";
        }

        public PerfilRepository() { }
        public void Add(Perfil obj)
        {
            try
            {
                obj.IdPerfil = Guid.NewGuid(); // Generar un nuevo Guid

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@IdPerfil", obj.IdPerfil),
                    new SqlParameter("@TipoPerfil", obj.PerfilTipo.ToString()),
                    new SqlParameter("@descripcion", obj.Descripcion)
                };

                SqlHelper.ExecuteNonQuery(InsertStatement, System.Data.CommandType.Text, parameters);

                _bitacoraService.Write("Perfil agregado exitosamente", LogLevel.Information);
            }
            catch (Exception ex)
            {
                _bitacoraService.Write($"Error al agregar perfil: {ex.Message}", LogLevel.Error);
                throw new Exception("Error al agregar perfil", ex);
            }
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Perfil> GetAll()
        {
            throw new NotImplementedException();
        }

        public Perfil GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Perfil GetByTipo(Perfil.TipoPerfil tipo)
        {
            throw new NotImplementedException();
        }

        public void Update(Perfil obj)
        {
            throw new NotImplementedException();
        }
    }
}
