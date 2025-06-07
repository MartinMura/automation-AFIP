using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;

namespace CargadorAfip
{
    internal class Class1
    {
        public static void CrearExcel()
        {
            Console.WriteLine("Estoy leyendo la clase desde mi main");
            var nombreArchivo = "DatosFacturasAFIP";
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Hoja1");
            worksheet.Cell("A1").Value = "CUIT emisor";
            worksheet.Cell("B1").Value = "Cliente";
            worksheet.Cell("C1").Value = "Monto";
            worksheet.Cell("A2").Value = "30-12345678-9";
            worksheet.Cell("B2").Value = "Cliente A";
            worksheet.Cell("C2").Value = 1000.50;
            worksheet.Cell("A3").Value = "30-12355331-9";
            worksheet.Cell("B3").Value = "Cliente B";
            worksheet.Cell("C3").Value = 2500.00;

            workbook.SaveAs("DatosFacturasAFIP.xlsx");
            Console.WriteLine($"Excel creado {nombreArchivo}.xlsx");
        }

        
    }
}
