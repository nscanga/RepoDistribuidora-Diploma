using Microsoft.Extensions.Logging;
using Services.DAL.Interfaces;
using Services.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DAL.Implementations.PlainText
{
    internal class BitacoraRepository : IBitacora
    {
        private String pathFile;
        public BitacoraRepository()
        {
            try
            {
                string directoryPath = ConfigurationManager.AppSettings["PathFile"];
                pathFile = directoryPath + DateTime.Now.ToString("ddMMyyyy") + ".log";

                // Imprimir la ruta para debugging
                Console.WriteLine("Directorio: " + directoryPath);

                // Comprobar si el directorio existe
                if (!Directory.Exists(directoryPath))
                {
                    // Crear el directorio
                    Directory.CreateDirectory(directoryPath);
                }
            }
            catch (Exception ex)
            {
                // Imprimir el mensaje de error
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public List<LogEntry> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<LogEntry> GetByFilter(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public List<LogEntry> GetByFilter(DateTime from, DateTime to, LogLevel level)
        {
            //?

            //Pensar un método que busque los archivos que cumplen la condición from/to
            List<FileInfo> archivosBitacora = GetFiles(from, to);

            List<LogEntry> entries = new List<LogEntry>();

            foreach (var item in archivosBitacora)
            {
                using (StreamReader streamReader = new StreamReader(item.FullName))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string line = streamReader.ReadLine();

                        if(line.Contains($"[{level.ToString()}]"))
                        {
                            //Mapper
                            string[] cabecera = line.Split('[');
                            string[] mensaje = line.Split(new string[] { "]: " }, StringSplitOptions.RemoveEmptyEntries);

                            LogEntry log = new LogEntry();
                            log.Fecha = DateTime.Parse(cabecera[0].Trim());
                            log.Level = level;
                            log.Descripcion = mensaje[1].Substring(0, mensaje[1].IndexOf(',') );
                            log.Usuario = line.Split(new string[] { ", usuario:" }, StringSplitOptions.None)[1].Trim();
                            entries.Add(log);
                        }
                    }

                }
            }

            return entries;
        }

        private List<FileInfo> GetFiles(DateTime from, DateTime to)
        {
            //Calcular nombres de archivos desde (from) hasta to()

            //Por ejemplo: 10/06/2023 hasta 15/06/2023: 10/06/2023, 11/06/2023, 12/06/2023, 13/06/2023, 14/06/2023 y 15/06/2023
            string directory = ConfigurationManager.AppSettings["PathFile"].Replace("bitacora", "");

            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            List<FileInfo> entries = new List<FileInfo>();

            foreach (var item in directoryInfo.GetFiles())
            {
                if(item.Name.Contains(from.ToString("ddMMyyyy")) || item.Name.Contains(to.ToString("ddMMyyyy")))
                {
                    entries.Add(item);
                }
            }
            return entries;
        }

        public List<LogEntry> GetByFilter(DateTime from, DateTime to, string usuario)
        {
            throw new NotImplementedException();
        }

        public List<LogEntry> GetByFilter(DateTime from, DateTime to, string mensaje, LogLevel level)
        {
            throw new NotImplementedException();
        }

        public void Write(string mensaje, LogLevel level)
        {
            StreamWriter str = null;
            try
            {
                // Intentar escribir en el archivo
                str = new StreamWriter(pathFile, true);
                string formattedMessage = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} [{level.ToString()}]: {mensaje}, usuario:";
                str.WriteLine(formattedMessage);
            }
            catch (DirectoryNotFoundException dirEx)
            {
                // Manejar excepciones relacionadas con el directorio
                Console.WriteLine("Error: Directorio no encontrado. " + dirEx.Message);
            }
            catch (IOException ioEx)
            {
                // Manejar otras excepciones de E/S
                Console.WriteLine("Error de E/S. " + ioEx.Message);
            }
            catch (Exception ex)
            {
                // Manejar cualquier otra excepción
                Console.WriteLine("Error desconocido. " + ex.Message);
            }
            finally
            {
                // Asegurarse de cerrar el StreamWriter
                if (str != null)
                {
                    str.Close();
                }
            }
        }


        public void Write(LogEntry log)
        {   
            throw new NotImplementedException();
        }
    }
}
