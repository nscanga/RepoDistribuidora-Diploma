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

            comboBoxTipo.DataSource = Enum.GetValues(typeof(Perfil.TipoPerfil));
        }


        private void buttonCrearUsuario_Click(object sender, EventArgs e)
        {
            Usuario newUser = new Usuario
            {
                IdUsuario = Guid.NewGuid(),
                Nombre = textBoxNombre.Text,  
                Email = textBoxNombre.Text,  
                Contrasena = textBoxContrasena.Text, 
                //id_perfil = 1  
            };

            string perfilSeleccionado = comboBoxTipo.SelectedItem.ToString();
            Perfil perfil = null; 

            Console.WriteLine($"Perfil seleccionado: {perfilSeleccionado}");


            if (Enum.TryParse<Perfil.TipoPerfil>(perfilSeleccionado, out Perfil.TipoPerfil perfilTipo))
            {
                // Cargar el perfil correspondiente de la base de datos
                perfil = _perfilRepository.GetPerfilByTipo(perfilTipo);

                if (perfil == null || perfil.IdPerfil == 0)
                {
                    // Log y manejar el error si no se encuentra el perfil
                    _logger.Log(LogLevel.Error, $"No se encontró un perfil válido para el tipo {perfilTipo}");
                    return;
                }

                // Logging del valor de perfilTipo
                Console.WriteLine($"PerfilTipo convertido: {perfilTipo}");

                try
                {
                    _usuarioService.Add(newUser, perfil);
                    _bitacoraService.Write("Usuario agregado exitosamente", LogLevel.Information);
                    MessageBox.Show("Usuario creado exitosamente.");
                }
                catch (Exception ex)
                {
                    _bitacoraService.Write($"Error al crear el usuario: {ex.Message}", LogLevel.Error);
                    MessageBox.Show("Error al crear el usuario.");
                }
            }
            else
            {
                MessageBox.Show($"Error: {perfilSeleccionado} no es un valor válido de TipoPerfil");
                return; 
            }
        }

        private void VolverPrelogin_Click(object sender, EventArgs e)
        {

        }
    }
}
