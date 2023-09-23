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
using DAL.Tools;
using System.Data;

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
            get => "INSERT INTO [dbo].[Usuario] (IdUsuario, Nombre, Email, Contrasena, id_perfil) VALUES (@IdUsuario, @Nombre, @Email, @Contrasena, @id_perfil)";
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

        public void Add(Usuario obj, Perfil perfil)
        {
            try
            {
                obj.IdUsuario = Guid.NewGuid();  // Generar un nuevo Guid
                obj.Contrasena = HashPassword(obj.Contrasena);  // Hashear la contraseña


                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@IdUsuario", obj.IdUsuario),
                new SqlParameter("@Nombre", obj.Nombre),
                new SqlParameter("@Email", obj.Email),
                new SqlParameter("@Contrasena", obj.Contrasena),
                new SqlParameter("@id_perfil", perfil.IdPerfil)
                };

                SqlHelper.ExecuteNonQuery(InsertStatement, System.Data.CommandType.Text, parameters);

                _bitacoraService.Write("Usuario agregado exitosamente", LogLevel.Information);
            }
            catch (Exception ex)
            {
                _bitacoraService.Write($"Error al agregar usuario: {ex.Message}", LogLevel.Error);
                throw new Exception("Error al agregar usuario", ex);
            }
        }

        // Método para hashear la contraseña
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Método ValidatePassword en tu servicio
        private bool ValidatePassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }




        public void Delete(Guid id)
        {
            throw new NotImplementedException(); //ver baja logica 
        }

        public Usuario FindByEmail(string email, string password)
        {
            try
            {
                Usuario user = null;

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Email", email)
                };

                // Aquí asumo que tienes una implementación de SqlHelper similar a la que usaste en Add
                using (SqlDataReader reader = SqlHelper.ExecuteReader(FindByEmailStatement, System.Data.CommandType.Text, parameters))
                {
                    if (reader.Read())
                    {
                        user = new Usuario
                        {
                            IdUsuario = reader.GetGuid(0),
                            Nombre = reader.GetString(1),
                            Email = reader.GetString(2),
                            Contrasena = reader.GetString(3),
                        };

                        // Verificar la contraseña con ValidatePassword
                        if (!ValidatePassword(password, user.Contrasena))
                        {
                            _bitacoraService.Write($"Contraseña incorrecta para el email: {email}", LogLevel.Warning);
                            return null;
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
                                    Contrasena = reader.GetString(3),
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
                        cmd.Parameters.AddWithValue("@PasswordHash", obj.Contrasena);
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
