using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI; // necesario para SelectElement


namespace CargadorAfip
{
    public static class MetodosHtml
    {


        //public static void botonContinuar(IWebDriver driver, string value)
        //{
        //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        //    //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//input[contains(@value, '{value}')]")));
        //    //var input = driver.FindElement(By.XPath($"//input[contains(@value, '{value}')]"));
        //    //input.Click();

        //    var input = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//input[contains(@value, '{value}')]")));
        //    input.Click();
        //}
        public static void botonContinuar(IWebDriver driver, string value)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            By xpath = By.XPath($"//input[contains(@value, '{value}')]");

            for (int intento = 0; intento < 2; intento++)
            {
                try
                {
                    var input = wait.Until(ExpectedConditions.ElementToBeClickable(xpath));
                    input.Click();
                    return; // éxito
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine($"⚠️ Elemento '{value}' se volvió stale. Reintentando...");
                    Thread.Sleep(1000); // Pequeña pausa antes del reintento
                }
                catch (Exception e)
                {
                    Console.WriteLine($"❌ Otro error en botonContinuar: {e.Message}");
                    throw;
                }
            }

            throw new Exception($"❌ No se pudo clickear el botón con valor '{value}' tras reintentos.");
        }


        //public static void botonSelector(IWebDriver driver, string id, string value)
        //{
        //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        //    var input = wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
        //    SelectElement selector = new SelectElement(input);
        //    selector.SelectByValue(value);
        //}
        public static void botonSelector(IWebDriver driver, string id, string value)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            for (int intento = 0; intento < 2; intento++)
            {
                try
                {
                    var input = wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
                    SelectElement selector = new SelectElement(input);
                    selector.SelectByValue(value);
                    Thread.Sleep(500);
                    return; // éxito
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine($"⚠️ Selector con id '{id}' se volvió stale. Reintentando...");
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"❌ Error en botonSelector con id '{id}': {e.Message}");
                    throw;
                }
            }

            throw new Exception($"❌ No se pudo seleccionar '{value}' en el combo con id '{id}'.");
        }


        //public static void campoALlenar(IWebDriver driver, string id, string keys)
        //{
        //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        //    var input = wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
        //    input.SendKeys(keys);
        //}
        public static void campoALlenar(IWebDriver driver, string id, string keys)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            for (int intento = 0; intento < 2; intento++)
            {
                try
                {
                    var input = wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
                    input.SendKeys(keys);
                    return; // éxito
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine($"⚠️ Campo con id '{id}' se volvió stale. Reintentando...");
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"❌ Error en campoALlenar con id '{id}': {e.Message}");
                    throw;
                }
            }

            throw new Exception($"❌ No se pudo llenar el campo con id '{id}'.");
        }

        public static void checkbox(IWebDriver driver, string id)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            By xpath = By.Id(id);

            for (int intento = 0; intento < 2; intento++)
            {
                try
                {
                    var input = wait.Until(ExpectedConditions.ElementToBeClickable(xpath));
                    input.Click();
                    return; // éxito
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine($"⚠️ Elemento '{id}' se volvió stale. Reintentando...");
                    Thread.Sleep(1000); // Pequeña pausa antes del reintento
                }
                catch (Exception e)
                {
                    Console.WriteLine($"❌ Otro error en botonCheckbox: {e.Message}");
                    throw;
                }
            }

            throw new Exception($"❌ No se pudo clickear el botón con valor '{id}' tras reintentos.");
        }

    }
}
