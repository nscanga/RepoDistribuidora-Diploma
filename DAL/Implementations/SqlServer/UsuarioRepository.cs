using DAL.Interfaces;
using Domain;
using Services.BaseService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DAL.Implementations.SqlServer
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BitacoraService _bitacoraService;

        public UsuarioRepository()
        {
        }

        public UsuarioRepository(BitacoraService bitacoraService)
        {
            _bitacoraService = bitacoraService;
        }

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Usuario] (Nombre, Email, Contraseña, PerfilId) VALUES (@Nombre, @Email, @Contraseña, @PerfilId)";
        }
        private string FindByEmailStatement
        {
            get => "SELECT * FROM [dbo].[Usuario] WHERE Email = @Email";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Usuario] SET Nombre = @Nombre, Email = @Email, Contraseña = @Contraseña, PerfilId = @PerfilId WHERE Id = @Id";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Cliente] WHERE IdUsuario = @IdUsuario";
        }

        private string SelectOneStatement
        {
            get => "SELECT IdUsuario , Nombre, FechaNac FROM [dbo].[Cliente] WHERE IdUsuario = @IdUsuario";
        }
        private string ListStatement
        {
            get => "SELECT * FROM [dbo].[Usuario]";
        }

        private string SelectAllStatement
        {
            get => "SELECT IdUsuario , Nombre, FechaNac FROM [dbo].[Cliente]";
        }
        #endregion

        public void Add(Usuario obj)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("MainConString"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(InsertStatement, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                        cmd.Parameters.AddWithValue("@Email", obj.Email);
                        cmd.Parameters.AddWithValue("@PasswordHash", obj.Contraseña);
                        cmd.Parameters.AddWithValue("@PerfilId", obj.PerfilId);
                        cmd.ExecuteNonQuery();
                    }
                }
                _bitacoraService.Write("Usuario agregado exitosamente", LogLevel.Information);
            }
            catch (Exception ex)
            {
                _bitacoraService.Write($"Error al agregar usuario: {ex.Message}", LogLevel.Error);
                throw new Exception("Error al agregar usuario", ex);
            }
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException(); //ver baja logica 
        }

        public Usuario FindByEmail(string email)
        {
            try
            {
                Usuario user = null;
                using (SqlConnection conn = new SqlConnection("MainConString"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(FindByEmailStatement, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new Usuario
                                {
                                    IdUsuario = reader.GetGuid(0),
                                    Nombre = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    Contraseña = reader.GetString(3),
                                    PerfilId = reader.GetInt32(4)
                                };
                            }
                        }
                    }
                }
                _bitacoraService.Write($"Usuario encontrado por email: {email}", LogLevel.Information);
                return user;
            }
            catch (Exception ex)
            {
                _bitacoraService.Write($"Error al buscar usuario por email: {ex.Message}", LogLevel.Error);
                throw new Exception("Error al buscar usuario por email", ex);
            }
        }

        public List<Usuario> GetAll()
        {
            try
            {
                List<Usuario> users = new List<Usuario>();
                using (SqlConnection conn = new SqlConnection("MainConString"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(ListStatement, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Usuario user = new Usuario
                                {
                                    IdUsuario = reader.GetGuid(0),
                                    Nombre = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    Contraseña = reader.GetString(3),
                                    PerfilId = reader.GetInt32(4)
                                };
                                users.Add(user);
                            }
                        }
                    }
                }
                _bitacoraService.Write("Usuarios listados exitosamente", LogLevel.Information);
                return users;
            }
            catch (Exception ex)
            {
                _bitacoraService.Write($"Error al listar usuarios: {ex.Message}", LogLevel.Error);
                throw new Exception("Error al listar usuarios", ex);
            }
        }

        public Usuario GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario obj)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("MainConString"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(UpdateStatement, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", obj.IdUsuario);
                        cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                        cmd.Parameters.AddWithValue("@Email", obj.Email);
                        cmd.Parameters.AddWithValue("@PasswordHash", obj.Contraseña);
                        cmd.Parameters.AddWithValue("@PerfilId", obj.PerfilId);
                        cmd.ExecuteNonQuery();
                    }
                }
                _bitacoraService.Write($"Usuario actualizado exitosamente: {obj.IdUsuario}", LogLevel.Information);
            }
            catch (Exception ex)
            {
                _bitacoraService.Write($"Error al actualizar usuario: {ex.Message}", LogLevel.Error);
                throw new Exception("Error al actualizar usuario", ex);
            }
        }
        // Implementar otros métodos de IGenericRepository
    }

}
