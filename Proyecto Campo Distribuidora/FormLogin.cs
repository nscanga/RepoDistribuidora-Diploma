using System;
using System.Windows.Forms;
using BLL.Implementations;
using BLL.Interfaces;
using DAL.Interfaces;
using Domain;
using Microsoft.Extensions.Logging; // Asegúrate de tener esta referencia
using Services.BaseService;
using Microsoft.Extensions.DependencyInjection;


namespace Proyecto_Campo_Distribuidora
{
    public partial class FormLogin : Form
    {
        private readonly UsuarioService _usuarioService;
        private readonly BitacoraService _bitacoraService;

        public FormLogin(UsuarioService usuarioService, BitacoraService bitacoraService)
        {
            InitializeComponent();
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
                    _bitacoraService.Write("Inicio de sesión exitoso", LogLevel.Information);
                    MessageBox.Show("Inicio de sesión exitoso");
                }
            }
            catch (Exception ex)
            {
                _bitacoraService.Write($"Error al iniciar sesión: {ex.Message}", LogLevel.Error);
                MessageBox.Show("Error al iniciar sesión");
            }
        }

        private void buttonCrearUsuario_Click(object sender, EventArgs e)
        {

            FormCreacionUsuarios formCreacionUsuarios = new FormCreacionUsuarios(_usuarioService, _bitacoraService);
            formCreacionUsuarios.Show();
            /*
            // Crear una instancia del objeto Usuario
            Usuario newUser = new Usuario
            {
                IdUsuario = Guid.NewGuid(),
                Nombre = textBoxNombre.Text,  // Asegúrate de tener un TextBox para el nombre
                Email = textBoxUsuario.Text,  // Ídem para el email
                Contrasena = textBoxPass.Text, // Asegúrate de encriptar la contraseña
                id_perfil = 1  // Genera un nuevo GUID y lo convierte a string
            };

            try
            {
                // Agregar el nuevo usuario
                _usuarioService.Add(newUser);  // Ahora pasamos newUser como argumento

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
            */
        }

    }
}
