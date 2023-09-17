using BLL.Interfaces;
using DAL.Interfaces;
using Domain;
using Microsoft.Extensions.Logging;
using Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Implementations
{
    public class UsuarioService : IUsuarioService<Usuario>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger _logger;
        private readonly BitacoraService _bitacoraService;

        public UsuarioService(IUsuarioRepository usuarioRepository, ILogger logger, BitacoraService bitacoraService)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
            _bitacoraService = bitacoraService;
        }

        public Usuario Login(string email, string password)
        {
            try
            {
                var user = _usuarioRepository.FindByEmail(email);
                if (user == null || !ValidatePassword(password, user.Contraseña))
                {
                    _logger.Log(LogLevel.Error, "Intento de inicio de sesión fallido");
                    _bitacoraService.Write("Intento de inicio de sesión fallido", LogLevel.Error);
                    throw new Exception("Credenciales inválidas");
                }
                _logger.Log(LogLevel.Information, "Inicio de sesión exitoso");
                _bitacoraService.Write("Inicio de sesión exitoso", LogLevel.Information);
                return user;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                _bitacoraService.Write($"Error durante el inicio de sesión: {ex.Message}", LogLevel.Error);
                throw;
            }
        }

        public void Add(Usuario obj)
        {
            try
            {
                _usuarioRepository.Add(obj);  // Asume que tienes un método Add en tu IUsuarioRepository

                _logger.Log(LogLevel.Information, "Usuario agregado exitosamente");
                _bitacoraService.Write("Usuario agregado exitosamente", LogLevel.Information);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error al agregar el usuario: {ex.Message}");
                _bitacoraService.Write($"Error al agregar el usuario: {ex.Message}", LogLevel.Error);
                throw;
            }
        }



        private bool ValidatePassword(string password, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash == storedHash;
            }
        }
    }

}
