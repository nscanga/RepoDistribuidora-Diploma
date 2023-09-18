using BLL.Implementations;
using Domain;
using Microsoft.Extensions.Logging;
using Services.BaseService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Campo_Distribuidora
{
    public partial class FormCreacionUsuarios : Form
    {
        private readonly UsuarioService _usuarioService;
        private readonly BitacoraService _bitacoraService;


        public FormCreacionUsuarios(UsuarioService usuarioService, BitacoraService bitacoraService)
        {
            InitializeComponent();
            _usuarioService = usuarioService;
            _bitacoraService = bitacoraService;
        }

        private void buttonCrearUsuario_Click(object sender, EventArgs e)
        {
            Usuario newUser = new Usuario
            {
                IdUsuario = Guid.NewGuid(),
                Nombre = textBoxNombre.Text,  // Asegúrate de tener un TextBox para el nombre
                Email = textBoxNombre.Text,  // Ídem para el email
                Contrasena = textBoxContrasena.Text, // Asegúrate de encriptar la contraseña
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
        }
    }
}
