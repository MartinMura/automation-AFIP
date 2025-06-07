# Automatización de carga de facturas en AFIP

Este proyecto es un prototipo para automatizar la carga de facturas en el sitio de AFIP utilizando **C#**, **Selenium WebDriver** y **ClosedXML**.

## 🚀 Funcionalidades

- 📄 Lectura de datos desde un archivo Excel (.xlsx)
- 🌐 Completado automático de formularios web en el portal de AFIP
- ⚙️ Manejo de errores comunes como elementos dinámicos o tiempos de carga lentos

## 🛠️ Requisitos

- .NET 8
- Visual Studio 2022 o superior
- Google Chrome y ChromeDriver instalado
- Archivo Excel con estructura predefinida (no incluido en el repositorio por contener datos sensibles)

## ▶️ Uso

1. Abrí la terminal en la carpeta del proyecto (`CargadorAfip`).
2. Ejecutá el comando:

   ```
   dotnet run
   ```

3. El sistema abrirá una ventana de Chrome y comenzará a cargar las facturas automáticamente en AFIP según los datos del archivo Excel.

> ⚠️ **Importante:** El archivo Excel debe tener validaciones para evitar errores de tipeo. Por ejemplo, campos con listas desplegables para evitar errores humanos.



## 📄 Licencia

Este proyecto está licenciado bajo la [Licencia MIT](LICENSE).
