using CargadorAfip;
using ClosedXML.Excel; // Para trabajar con Excel
using DocumentFormat.OpenXml.Office.CustomUI;
using OpenQA.Selenium; // Selenium WebDriver
using OpenQA.Selenium.Chrome; // Controlador para Chrome
using SeleniumExtras.WaitHelpers; // Para condiciones de espera
using OpenQA.Selenium.Support.UI; // necesario para SelectElement
using System;


namespace CargadorAfip
{


    class Program
    {
        static void Main()
        {




            List<Factura> facturas = LeerExcel.Leer();




            // 1. Creamos el navegador Chrome
            //IWebDriver driver = new ChromeDriver();
            
            
            try
            {
                Console.WriteLine("Iniciando ChromeDriver...");

                IWebDriver driver = new ChromeDriver();

                Console.WriteLine("Chrome abierto correctamente");

                driver.Navigate().GoToUrl("https://auth.afip.gob.ar/contribuyente_/login.xhtml?action=SYSTEM&system=rcel");

                Console.ReadLine(); // 👈 para que no se cierre
            


            // 2. Vamos a una página web

            


            Console.WriteLine("Ingrese su usuario en la web");
            Console.ReadLine();
            IWebElement input = driver.FindElement(By.XPath($"//input[contains(@value, '{facturas[0].usuario}')]"));
            input.Click();

            

            // Lo siguiente entra en loop

            for (int i = 0; i < facturas.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(facturas[i].Cargada) || facturas[i].Cargada.ToLower() == "no")
                {

                    input = driver.FindElement(By.Id("btn_gen_cmp"));
                    input.Click();




                    MetodosHtml.botonSelector(driver, "puntodeventa", facturas[i].PuntoDeVenta.ToString());





                    MetodosHtml.botonContinuar(driver, "Continuar >");


                    string conceptoOriginal = facturas[i].Concepto.ToString();
                    string concepto = conceptoOriginal.ToLowerInvariant().Replace(" ", "");
                    string valor;
                    var fechaFormateada = facturas[i].Fecha;


                    Console.WriteLine(fechaFormateada);

                    if (concepto.Contains("producto"))
                    {
                        valor = "1";
                        MetodosHtml.botonSelector(driver, "idconcepto", valor);
                        IWebElement fecha = driver.FindElement(By.Id("fc"));

                        fecha.Clear();
                        fecha.SendKeys(facturas[i].Fecha); //fecha del comprobante
                    }
                    else if (concepto.Contains("servicios"))
                    {
                        valor = "2";
                        MetodosHtml.botonSelector(driver, "idconcepto", valor);
                        IWebElement fecha = driver.FindElement(By.Id("fc"));

                        fecha.Clear();
                        fecha.SendKeys(facturas[i].Fecha); //fecha del comprobante
                        fecha = driver.FindElement(By.Id("fsd"));
                        fecha.Clear();
                        fecha.SendKeys(facturas[i].FechaDesde); //fecha desde agregar al excel
                        fecha = driver.FindElement(By.Id("fsh"));
                        fecha.Clear();
                        fecha.SendKeys(facturas[i].FechaHasta); //fecha hasta agregar al excel
                        IWebElement fechaActual = driver.FindElement(By.Id("vencimientopago"));
                        fechaActual.Clear();
                        fechaActual.SendKeys(DateTime.Now.ToString("dd/MM/yyyy")); //fecha actual de la compu,      AGREGAR AL EXCEL PARA QUE NO SEA SIEMPRE FECHA ACTUAL
                    }
                    else if (concepto.Contains("productos y servicios"))
                    {
                        valor = "3";
                        MetodosHtml.botonSelector(driver, "idconcepto", valor);
                        IWebElement fecha = driver.FindElement(By.Id("fc"));

                        fecha.Clear();
                        fecha.SendKeys(facturas[i].Fecha); //fecha del comprobante
                        fecha = driver.FindElement(By.Id("fsd"));
                        fecha.Clear();
                        fecha.SendKeys(facturas[i].FechaDesde); //fecha desde agregar al excel
                        fecha = driver.FindElement(By.Id("fsh"));
                        fecha.Clear();
                        fecha.SendKeys(facturas[i].FechaHasta); //fecha hasta agregar al excel
                        IWebElement fechaActual = driver.FindElement(By.Id("vencimientopago"));
                        fechaActual.Clear();
                        fechaActual.SendKeys(DateTime.Now.ToString("dd/MM/yyyy")); //fecha actual de la compu
                    }









                    string actividadOriginal = facturas[i].ActividadAsoc.ToString();
                    string actividad = actividadOriginal.ToLowerInvariant().Replace(" ", "");

                    if (actividad.Contains("620100"))
                    {
                        valor = "620100";
                        MetodosHtml.botonSelector(driver, "actiAsociadaId", valor);
                    }
                    else if (actividad.Contains("692000"))
                    {
                        valor = "692000";
                        MetodosHtml.botonSelector(driver, "actiAsociadaId", valor);
                    }





                    MetodosHtml.botonContinuar(driver, "Continuar >");



                    string condicionIVAHTML = facturas[i].CondicionIVA.ToString();
                    string condicionIVAFiltrado = condicionIVAHTML.ToLowerInvariant().Replace(" ", "");
                    if (condicionIVAFiltrado.Contains("ivaresponsableinscripto"))
                    {
                        valor = "1";
                        MetodosHtml.botonSelector(driver, "idivareceptor", valor);


                    }
                    else if (condicionIVAFiltrado.Contains("ivasujetoexento"))
                    {
                        valor = "4";
                        MetodosHtml.botonSelector(driver, "idivareceptor", valor);

                    }
                    else if (condicionIVAFiltrado.Contains("consumidorfinal"))
                    {
                        valor = "5";
                        MetodosHtml.botonSelector(driver, "idivareceptor", valor);

                    }
                    else if (condicionIVAFiltrado.Contains("responsablemonotributo"))
                    {
                        valor = "6";
                        MetodosHtml.botonSelector(driver, "idivareceptor", valor);

                    }
                    else if (condicionIVAFiltrado.Contains("sujetonocategorizado"))
                    {
                        valor = "7";
                        MetodosHtml.botonSelector(driver, "idivareceptor", valor);

                    }
                    else if (condicionIVAFiltrado.Contains("proveedordelexterior"))
                    {
                        valor = "8";
                        MetodosHtml.botonSelector(driver, "idivareceptor", valor);

                    }
                    else if (condicionIVAFiltrado.Contains("clientedelexterior"))
                    {
                        valor = "9";
                        MetodosHtml.botonSelector(driver, "idivareceptor", valor);

                    }
                    else if (condicionIVAFiltrado.Contains("ivaliberado-leynº19.640"))
                    {
                        valor = "10";
                        MetodosHtml.botonSelector(driver, "idivareceptor", valor);

                    }
                    else if (condicionIVAFiltrado.Contains("monotributistasocial"))
                    {
                        valor = "13";
                        MetodosHtml.botonSelector(driver, "idivareceptor", valor);

                    }
                    else if (condicionIVAFiltrado.Contains("ivanoalcanzado"))
                    {
                        valor = "15";
                        MetodosHtml.botonSelector(driver, "idivareceptor", valor);

                    }
                    else if (condicionIVAFiltrado.Contains("monotributistatrabajadorindependientepromovido"))
                    {
                        valor = "16";
                        MetodosHtml.botonSelector(driver, "idivareceptor", valor);

                    }

                    Thread.Sleep(500);

                    MetodosHtml.campoALlenar(driver, "nrodocreceptor", facturas[i].NroDocReceptor);
                    Thread.Sleep(500);


                    Thread.Sleep(500);


                    Thread.Sleep(500);

                    string condicionDeVentaOriginal = facturas[i].CondicionVenta.ToString();
                    string condicionDeVenta = condicionDeVentaOriginal.ToLowerInvariant();

                    if (condicionDeVenta.Contains("contado"))
                    {
                        Thread.Sleep(500);
                        MetodosHtml.checkbox(driver, "formadepago1");

                    }
                    else if (condicionDeVenta.Contains("tarjeta de debito"))
                    {
                        Console.WriteLine("ingrese los datos de la tarjeta manualmente en el formulario");
                        Thread.Sleep(500);
                        MetodosHtml.checkbox(driver, "formadepago2");
                        Console.ReadLine();

                    }
                    else if (condicionDeVenta.Contains("tarjeta de credito"))
                    {
                        Console.WriteLine("Ingrese los datos de la tarjeta manualmente en el formulario");
                        Thread.Sleep(500);
                        MetodosHtml.checkbox(driver, "formadepago3");
                        Console.ReadLine();

                    }
                    else if (condicionDeVenta.Contains("cuenta corriente"))
                    {
                        Thread.Sleep(500);
                        MetodosHtml.checkbox(driver, "formadepago4");

                    }
                    else if (condicionDeVenta.Contains("cheque"))
                    {
                        Thread.Sleep(500);
                        MetodosHtml.checkbox(driver, "formadepago5");

                    }
                    else if (condicionDeVenta.Contains("transferencia bancaria"))
                    {
                        Thread.Sleep(500);
                        MetodosHtml.checkbox(driver, "formadepago6");

                    }
                    else if (condicionDeVenta.Contains("otra"))
                    {
                        Thread.Sleep(500);
                        MetodosHtml.checkbox(driver, "formadepago7");

                    }
                    else if (condicionDeVenta.Contains("otros medios de pago electronico"))
                    {
                        Thread.Sleep(500);
                        MetodosHtml.checkbox(driver, "formadepago8");

                    }

                    MetodosHtml.botonContinuar(driver, "Continuar >");





                    MetodosHtml.campoALlenar(driver, "detalle_descripcion1", facturas[i].Detalle);

                    MetodosHtml.botonSelector(driver, "detalle_medida1", "7");

                    MetodosHtml.campoALlenar(driver, "detalle_precio1", facturas[i].PrecioUnitario);

                    MetodosHtml.botonContinuar(driver, "Continuar >");


                    MetodosHtml.botonContinuar(driver, "Confirmar Datos..."); //ahora despues de esto aparece el alert

                    //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    By xpath = By.XPath("//span[contains(text(),'Cancelar')]");
                    var ultimoPaso = wait.Until(ExpectedConditions.ElementToBeClickable(xpath));
                    ultimoPaso.Click();

                    //IAlert alert = driver.SwitchTo().Alert();
                    //Acá hay un nuevo botón, ya no es una alert, sino un cuadro de diálogo con un botón aceptar y otro cancelar en un div con clase ui-dialog ui-widget ui-widget-content ui-corner-all ui-front dialog-con-sombra ui-dialog-buttons ui-draggable


                    //Console.WriteLine("Texto del alert: " + alert.Text);
                    //alert.Dismiss(); //dismiss para no subir nada

                    Thread.Sleep(2000);

                    MetodosHtml.botonContinuar(driver, "Menú Principal");
                   


                    LeerExcel.MarcarFacturaComoCargada(i + 3, "si"); //cambiamos la condicion de la factura a cargada

                    Console.WriteLine($"Se cargó la factura {i + 1}, del cuit N° {facturas[i].NroDocReceptor} ");
                }
                else
                {
                    Console.WriteLine($"La factura numero {i + 1} ya fue cargada previamente");
                }
            }

            Console.WriteLine("Se terminaron de cargar las facturas, presione enter para cerrar el programa");
            Console.ReadKey();




            // 6. Cerramos el navegador
            driver.Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:");
                Console.WriteLine(ex.ToString());

                File.WriteAllText("error_selenium.txt", ex.ToString());

                Console.ReadLine(); // 👈 para poder leer el error
            }

        }
    }
}
