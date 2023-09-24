using DAL.Interfaces;
using DAL.Tools;
using Domain;
using Microsoft.Extensions.Logging;
using Services.BaseService;
using System;
using System.Collections.Generic;
using System.Data;
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

        private string FindByTipoStatement
        {
            get => "SELECT * FROM [dbo].[Perfil] WHERE TipoPerfil = @TipoPerfil";
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

        public Perfil FindByTipo(Perfil.TipoPerfil tipo)
        {
            try
            {
                Perfil perfil = null;

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TipoPerfil", tipo.ToString())
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(FindByTipoStatement, System.Data.CommandType.Text, parameters))
                {
                    if (reader.Read())
                    {
                        perfil = new Perfil
                        {
                            IdPerfil = reader.GetGuid(0),
                            PerfilTipo = (Perfil.TipoPerfil)Enum.Parse(typeof(Perfil.TipoPerfil), reader.GetString(1)),
                            Descripcion = reader.GetString(2)
                        };
                    }
                }

                return perfil;
            }
            catch (Exception ex)
            {
                _bitacoraService.Write($"Error al buscar perfil por tipo: {ex.Message}", LogLevel.Error);
                throw new Exception("Error al buscar perfil por tipo", ex);
            }
        }
    }
}
