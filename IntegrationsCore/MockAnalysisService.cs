using BackEnd_PGI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationsCore
{
    public class MockAnalysisService : IMockAnalysisService
    {
        public async Task<(string resumen, string descripcion)> AnalyzeAssetAsync(Asset asset)
        {
            //await Task.Delay(100); // Simular un tiempo de espera para el análisis

            return asset.ID switch
            {
                1 => ("Ejecutable sospechoso detectado en secretaria",
                      "El archivo ejecutable en la computadora de la secretaria contiene código potencialmente malicioso que podría comprometer el sistema."),
                2 => ("Documento PDF con enlaces sospechosos",
                      "Este PDF encontrado en la computadora de la secretaria contiene enlaces a sitios externos que podrían representar amenazas de phishing."),
                3 => ("Imagen con metadatos sensibles",
                      "La imagen contiene metadatos que podrían incluir detalles confidenciales como la ubicación de creación."),
                4 => ("Script Python con funciones de acceso remoto",
                      "El script Python tiene funciones que podrían permitir acceso remoto no autorizado al sistema."),
                5 => ("Archivo JSON de configuración",
                      "Este archivo JSON contiene configuraciones de red y credenciales sensibles."),
                6 => ("Documento de texto con información privada",
                      "El archivo de texto contiene notas importantes que podrían exponer información privada de la empresa."),
                7 => ("Archivo ZIP con múltiples documentos",
                      "El archivo ZIP incluye varios documentos que necesitan revisión debido a su origen desconocido."),
                8 => ("Registro de eventos con accesos irregulares",
                      "El registro de eventos del sistema muestra intentos de acceso inusuales en la computadora de la secretaria."),
                9 => ("Clave de registro de Windows modificada",
                      "Se detectó una modificación reciente en la clave de registro que podría estar relacionada con la actividad de malware."),
                10 => ("Dirección IP sospechosa en el historial",
                      "La dirección IP en el historial de conexiones corresponde a un origen identificado como malicioso."),
                11 => ("Ejecutable sospechoso en servidor de archivos",
                      "Un archivo ejecutable sospechoso se detectó en el servidor de archivos. Podría permitir ejecución de código no autorizado."),
                12 => ("PDF potencialmente malicioso en servidor de archivos",
                      "El documento PDF contiene referencias a scripts externos que podrían representar un riesgo para el servidor."),
                13 => ("Imagen con datos de ubicación en servidor de archivos",
                      "La imagen encontrada en el servidor tiene datos de ubicación que podrían revelar información sobre su origen."),
                14 => ("Script de automatización sospechoso",
                      "El script contiene referencias a direcciones IP externas, lo cual podría ser una amenaza de seguridad."),
                15 => ("Archivo JSON con configuración de red",
                      "Este archivo de configuración JSON incluye información de red crítica y credenciales."),
                16 => ("Log del sistema con errores críticos",
                      "El log muestra una serie de errores críticos que podrían indicar problemas de seguridad."),
                17 => ("Archivo ZIP con documentos sensibles",
                      "El archivo comprimido contiene documentos confidenciales que necesitan revisión."),
                18 => ("Registro de eventos con patrones irregulares",
                      "Eventos sospechosos detectados en el registro del sistema en el servidor."),
                19 => ("Clave de registro alterada en servidor de archivos",
                      "Se detectó una modificación en una clave de registro crítica, posiblemente indicando manipulación no autorizada."),
                20 => ("IP interna en servidor de archivos",
                      "La dirección IP interna puede estar expuesta, representando un riesgo de acceso interno no autorizado."),
                21 => ("Ejecutable sospechoso en servidor de bases de datos",
                      "El archivo ejecutable en el servidor de bases de datos podría comprometer la integridad del servidor."),
                22 => ("PDF con contenido confidencial",
                      "Este documento PDF podría contener información confidencial relacionada con la base de datos."),
                23 => ("Imagen con datos privados en el servidor de bases de datos",
                      "La imagen tiene metadatos de creación que podrían revelar información sensible."),
                24 => ("Script en el servidor de bases de datos",
                      "Este script Python tiene funciones que permiten extracción de datos."),
                25 => ("Archivo JSON de configuración del servidor de bases de datos",
                      "Contiene configuraciones del sistema y credenciales importantes."),
                26 => ("Notas confidenciales en el servidor",
                      "Documento de texto con notas de usuario que podrían contener información privada."),
                27 => ("Copia de seguridad comprimida",
                      "Archivo ZIP contiene una copia de seguridad de la base de datos. Requiere medidas de seguridad adicionales."),
                28 => ("Registro de eventos del sistema en el servidor",
                      "Se identificaron accesos no autorizados en el registro de eventos del servidor."),
                29 => ("Clave de registro modificada en el servidor",
                      "Clave de registro en el servidor de bases de datos ha sido alterada."),
                30 => ("Dirección IP pública sospechosa",
                      "La dirección IP pública del servidor de bases de datos coincide con una fuente previamente identificada como maliciosa."),
                _ => ("Análisis no definido", "No se pudo generar un análisis ficticio para este tipo de archivo.")
            };
        }

        public async Task<byte[]> MockDownloadFileFromFtpAsync(string fileName)
        {
            // Mapea extensiones a contenido ficticio
            var mockContentByExtension = new Dictionary<string, string>
            {
                { ".exe", "Contenido ficticio de archivo ejecutable malicioso." },
                { ".pdf", "Contenido ficticio de un documento PDF con información sensible." },
                { ".png", "Metadatos de imagen ficticios." },
                { ".py", "Código de script Python ficticio para análisis." },
                { ".json", "Configuración JSON ficticia." },
                { ".txt", "Texto ficticio con notas importantes." },
                { ".zip", "Archivo comprimido ficticio con varios documentos." },
                { ".log", "Registro de eventos del sistema ficticio." },
                { ".reg", "Clave de registro de Windows ficticia." }
            };

            // Extrae la extensión del archivo
            var fileExtension = Path.GetExtension(fileName);

            // Obtén el contenido ficticio según la extensión
            if (mockContentByExtension.TryGetValue(fileExtension, out var mockContent))
            {
                return Encoding.UTF8.GetBytes(mockContent);
            }

            // Si no se encuentra la extensión, devuelve contenido genérico
            return Encoding.UTF8.GetBytes("Contenido de archivo ficticio genérico.");
        }

    }
}
