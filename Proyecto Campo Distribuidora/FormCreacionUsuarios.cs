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
           
        }

        private void VolverPrelogin_Click(object sender, EventArgs e)
        {

        }
    }
}
