using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML;
using ClosedXML.Excel;

namespace CargadorAfip
{
    internal class LeerExcel
    {
           public static List<Factura> Leer()
           {
               
                using (var workbook = new XLWorkbook("excel-datos-cargar.xlsx"))
                {
                    List<Factura> facturas = new List<Factura>();

                    var worksheet = workbook.Worksheet("Sheet1");
                    for(int fila = 3; !worksheet.Cell(fila, 1).IsEmpty(); fila++)
                        {
                            DateTime fecha = worksheet.Cell(fila, 1).GetDateTime();
                            string fechaFormateada = fecha.ToString("dd/MM/yyyy");
                            DateTime fechaDesde = worksheet.Cell(fila, 2).GetDateTime();
                            string fechaFormateadaDesde = fechaDesde.ToString("dd/MM/yyyy");
                            DateTime fechaHasta = worksheet.Cell(fila, 3).GetDateTime();
                            string fechaFormateadaHasta = fechaHasta.ToString("dd/MM/yyyy");
                            string concepto = worksheet.Cell(fila, 4).GetString();
                            string actividadAsoc = worksheet.Cell(fila, 5).GetString();
                            string condicionIVA = worksheet.Cell(fila, 6).GetString();
                    



                            string tipo = worksheet.Cell(fila, 7).GetString();
                            
                            string puntoDeVenta = worksheet.Cell(fila, 8).GetString();
                            
                            string tipoDocReceptor = worksheet.Cell(fila, 9).GetString();
                            string nroDocReceptor = worksheet.Cell(fila, 10).GetString();
                            string denominacionReceptor= worksheet.Cell(fila, 11).GetString();
                            string condicionVenta = worksheet.Cell(fila, 12).GetString();
                            string detalle = worksheet.Cell(fila, 13).GetString();
                            string precioUnitario= worksheet.Cell(fila, 14).GetString();
                            string cargada = worksheet.Cell(fila, 15).GetString();
                    




                           

                            facturas.Add(new Factura(fechaFormateada, fechaFormateadaDesde, fechaFormateadaHasta, concepto, actividadAsoc, condicionIVA, tipo, puntoDeVenta,tipoDocReceptor, nroDocReceptor, denominacionReceptor, condicionVenta, detalle, precioUnitario, cargada));

                    
                        }


                foreach (var factura in facturas)
                {
                    Console.WriteLine();
                    Console.WriteLine(factura.Fecha);
                    Console.WriteLine(factura.FechaDesde);
                    Console.WriteLine(factura.FechaHasta);
                    Console.WriteLine(factura.Concepto.GetType());
                    Console.WriteLine(factura.ActividadAsoc);
                    Console.WriteLine(factura.CondicionIVA);
                    Console.WriteLine(factura.Tipo);
                    Console.WriteLine(factura.PuntoDeVenta);

                    Console.WriteLine(factura.TipoDocReceptor);
                    Console.WriteLine(factura.NroDocReceptor);
                    Console.WriteLine(factura.DenominacionReceptor);
                    Console.WriteLine(factura.CondicionVenta);
                    Console.WriteLine(factura.Detalle);
                    Console.WriteLine(factura.PrecioUnitario);

                    Console.WriteLine();
                }

                return facturas;

                }

            }
            public static void MarcarFacturaComoCargada(int filaActual, string value)
            {
                string rutaArchivo = @"C:\Users\Martin\Desktop\proyecto-afip-final\CargadorAfip\excel-datos-cargar.xlsx"; //ruta a modificar si cambiamos el archivo de lugar, ver como hacerlo dinámico

                using (var workbook = new XLWorkbook(rutaArchivo))
                {
                    var worksheet = workbook.Worksheet(1);
                    worksheet.Cell(filaActual, 15).Value = value;
                    workbook.Save();
                }
            }

    }
}
