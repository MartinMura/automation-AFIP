using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML;
using ClosedXML.Excel;

namespace CargadorAfip
{
    public class LeerExcel
    {
           public static List<Factura> Leer()
           {
            try
            {

            string rutaExcel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "excel-datos-cargar.xlsx");
            var workbook = new XLWorkbook(rutaExcel);

            using (workbook)
                {
                    List<Factura> facturas = new List<Factura>();

                    var worksheet = workbook.Worksheet("Sheet1");
                    int ultimaFila = worksheet.LastRowUsed().RowNumber();
                    for(int fila = 3; fila <= ultimaFila; fila++)
                        {
                            DateTime fecha = worksheet.Cell(fila, 1).GetDateTime();
                            string fechaFormateada = fecha.ToString("dd/MM/yyyy");
                            DateTime fechaDesde = worksheet.Cell(fila, 2).GetDateTime();
                            string fechaFormateadaDesde = fechaDesde.ToString("dd/MM/yyyy");
                            DateTime fechaHasta = worksheet.Cell(fila, 3).GetDateTime();
                            string fechaFormateadaHasta = fechaHasta.ToString("dd/MM/yyyy");
                            //string concepto = worksheet.Cell(fila, 4).GetString();
                            string concepto = ValidarNoVacia(worksheet, fila, 4, "concepto");
                            //string actividadAsoc = worksheet.Cell(fila, 5).GetString();
                            string actividadAsoc = ValidarNoVacia(worksheet, fila, 5, "actividad asociada");
                            string condicionIVA = ValidarNoVacia(worksheet, fila, 6, "condicion Iva");
                            //string condicionIVA = worksheet.Cell(fila, 6).GetString();
                    

                            

                            string tipo = worksheet.Cell(fila, 7).GetString();
                            //string tipo = ValidarNoVacia(worksheet, fila, 4, "tipo factura");
                            
                            //string puntoDeVenta = worksheet.Cell(fila, 8).GetString();
                            string puntoDeVenta = ValidarNoVacia(worksheet, fila, 8, "punto de venta");
                            
                            string tipoDocReceptor = worksheet.Cell(fila, 9).GetString();
                            //string tipoDocReceptor = ValidarNoVacia(worksheet, fila, 4, "tipo doc receptor");
                            //string nroDocReceptor = worksheet.Cell(fila, 10).GetString();
                            string nroDocReceptor = ValidarNoVacia(worksheet, fila, 10, "nro Doc repector");
                            string denominacionReceptor= ValidarNoVacia(worksheet, fila, 11, "Denominacion Receptor");
                            //string denominacionReceptor= worksheet.Cell(fila, 11).GetString();
                            string condicionVenta = ValidarNoVacia(worksheet, fila, 12, "condicion de venta");
                            //string condicionVenta = worksheet.Cell(fila, 12).GetString();
                            string detalle = ValidarNoVacia(worksheet, fila, 13, "detalle");
                            //string detalle = worksheet.Cell(fila, 13).GetString();
                            string precioUnitario= ValidarNoVacia(worksheet, fila, 14, "precio unitario");
                            //string precioUnitario= worksheet.Cell(fila, 14).GetString();
                            string cargada = ValidarNoVacia(worksheet, fila, 15, "cargada");
                            //string cargada = worksheet.Cell(fila, 15).GetString();
                    

                            


                           

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
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Archivo no encontrado: {ex.Message}");
                Console.WriteLine($"Por favor, chequear nombre de archivo y ubicación");
                throw;
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine($"Error de formato: {ex.Message}");
                Console.WriteLine($"El nombre de la hoja debe ser Sheet1");
                throw;
            }
            catch(InvalidCastException ex)
            {
                Console.WriteLine($"Error de formato: {ex.Message}");
                Console.WriteLine($"Ingresar una fecha válida en las celdas de fechas (dd/MM/yyyy)");
                throw;
            }



            }


            
            public static void MarcarFacturaComoCargada(int filaActual, string value)
            {
                //string rutaArchivo = @"C:\Users\Martin\Desktop\proyecto-afip-final\CargadorAfip\excel-datos-cargar.xlsx"; //ruta a modificar si cambiamos el archivo de lugar, ver como hacerlo dinámico
                string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "excel-datos-cargar.xlsx");
                using (var workbook = new XLWorkbook(rutaArchivo))
                {
                    var worksheet = workbook.Worksheet(1);
                    worksheet.Cell(filaActual, 15).Value = value;
                    workbook.Save();
                }
            }

            public static string ValidarNoVacia(IXLWorksheet hoja, int fila, int columna, string nombreCampo)
            {
                var celda = hoja.Cell(fila, columna);
                
                if (celda.IsEmpty() || string.IsNullOrWhiteSpace(celda.GetString()))
                {
                    
                    throw new Exception($"La celda '{nombreCampo}' (columna {columna}) está vacía en la fila {fila}.");

                } else
                {
                    return celda.GetString();
                }
                
                
            
            }


    }
}
