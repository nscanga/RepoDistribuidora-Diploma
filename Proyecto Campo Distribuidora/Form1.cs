using System;
using System.Windows.Forms;
using BLL.Implementations;
using BLL.Interfaces;
using DAL.Interfaces;
using Domain;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging; // Asegúrate de tener esta referencia
using Services.BaseService;

namespace Proyecto_Campo_Distribuidora
{
    public partial class Form1 : Form
    {
        private UsuarioService _usuarioService;
        private BitacoraService _bitacoraService;


        public Form1(UsuarioService usuarioService, BitacoraService bitacoraService)
        {
            InitializeComponent();

            // 
            _usuarioService = usuarioService;
            _bitacoraService = bitacoraService;
        }

        private void ButtonIngresar_Click(object sender, EventArgs e)
        {
            string email = textBoxUsuario.Text;
            string password = textBoxPass.Text;

            try
            {
                var user = _usuarioService.Login(email, password);
                if (user != null)
                {
                    // Iniciar sesión exitosamente
                    _bitacoraService.Write("Inicio de sesión exitoso", Microsoft.Extensions.Logging.LogLevel.Information);
                    MessageBox.Show("Inicio de sesión exitoso");
                }
            }
            catch (Exception ex)
            {
                _bitacoraService.Write($"Error al iniciar sesión: {ex.Message}", Microsoft.Extensions.Logging.LogLevel.Error);
                MessageBox.Show("Error al iniciar sesión");
            }
        }

        private void buttonCrearUsuario_Click(object sender, EventArgs e)
        {
            // Crear una instancia del objeto Usuario
            Usuario newUser = new Usuario
            {
                Nombre = textBoxUsuario.Text,  // Puedes reemplazar esto con un valor de un TextBox, por ejemplo
                Email = textBoxPass.Text,  // Ídem
                Contraseña = "contraseña_encriptada", // Asegúrate de encriptar la contraseña
                PerfilId = 1 // ID del perfil al que pertenece el usuario
            };

            try
            {
                // Agregar el nuevo usuario
                _usuarioService.Add(newUser);  // Nota el cambio aquí: ahora pasamos newUser como argumento

                // Registrar en la bitácora
                _bitacoraService.Write("Usuario agregado exitosamente", LogLevel.Information);

                MessageBox.Show("Usuario creado exitosamente.");
            }
            catch (Exception ex)
            {
                // Registrar el error en la bitácora
                _bitacoraService.Write($"Error al crear el usuario: {ex.Message}", LogLevel.Error);

                MessageBox.Show("Error al crear el usuario.");
            }
        }


    }
}
