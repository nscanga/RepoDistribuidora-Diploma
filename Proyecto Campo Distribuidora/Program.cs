using BLL.Implementations;
using DAL.Factory;
using DAL.Implementations.SqlServer;
using DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Services.BaseService;
using System;
using System.Windows.Forms;

namespace Proyecto_Campo_Distribuidora
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configurar servicios y logger
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.AddConsole();
                })
                .AddScoped<IUsuarioRepository, UsuarioRepository>()  // Asume que UsuarioRepository es la implementación de IUsuarioRepository
                .AddScoped<BitacoraService>()
                .AddScoped<UsuarioService>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<UsuarioService>>();  // Obtener el logger genérico

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicializar los servicios aquí
            try
            {
                var usuarioService = serviceProvider.GetService<UsuarioService>();
                var bitacoraService = serviceProvider.GetService<BitacoraService>();
                Application.Run(new Form1(usuarioService, bitacoraService));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Aquí podrías añadir más lógica para manejar el error
            }
        }
    }
}
