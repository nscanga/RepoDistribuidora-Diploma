using BLL.Implementations;
using DAL.Factory;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Campo_Distribuidora
{
    internal static class Program
    {
        private static ILogger logger;

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        // Program.cs
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicializar los servicios aquí
            FactoryDAL factoryDAL = new FactoryDAL();
            IUsuarioRepository usuarioRepository = factoryDAL.GetUsuarioRepository();
            BitacoraService bitacoraService = new BitacoraService();
            UsuarioService usuarioService = new UsuarioService(usuarioRepository, logger, bitacoraService);

            Application.Run(new Form1(usuarioService, bitacoraService));
        }

    }
}
