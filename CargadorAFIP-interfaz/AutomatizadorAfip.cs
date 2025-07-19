using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using CargadorAfip; // Para acceder a LeerExcel y Factura
using System.Linq;
using System.Windows;

namespace CargadorAFIP_interfaz
{
    public static class AutomatizadorAfip
    {
        public static void Ejecutar()
        {
            List<Factura> facturas = LeerExcel.Leer();
            string chromeDriverPath = AppDomain.CurrentDomain.BaseDirectory;
            var options = new ChromeOptions();
            IWebDriver driver = new ChromeDriver(chromeDriverPath, options);

            //IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://auth.afip.gob.ar/contribuyente_/login.xhtml?action=SYSTEM&system=rcel");

            Console.WriteLine("Ingrese su usuario en la web");
            Console.ReadLine();

            System.Windows.MessageBox.Show(
                 "🧾 Ingresá tu CUIT y clave fiscal en la web de AFIP. Luego hacé click en 'Ok'.",
                 "Esperando al usuario",
                 MessageBoxButton.OK,
                 MessageBoxImage.Information
            );

            IWebElement input = driver.FindElement(By.XPath("//input[contains(@value, 'MURA ZUÑIGA GISSELE MARCELA')]"));
            input.Click();

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

                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

                    IAlert alert = driver.SwitchTo().Alert();
                    Console.WriteLine("Texto del alert: " + alert.Text);
                    alert.Accept(); //dismiss para no subir nada

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

            Application.Current.Dispatcher.Invoke(() =>
            {
                driver.Quit();

                System.Windows.MessageBox.Show(
                    "✅ Se terminaron de cargar las facturas.\nEl programa se cerrará ahora.",
                    "Proceso finalizado",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                Application.Current.Shutdown();
            });



        }
    }
}
