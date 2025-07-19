using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;

namespace CargadorAfip
{
    public class Factura
    {
        public string fecha;
        public string tipo;
        public string puntoDeVenta;
  
        public string tipoDocReceptor;
        public string nroDocReceptor;
        public string denominacionReceptor;

        public string fechaDesde;
        public string fechaHasta;
        public string concepto;
        public string actividadAsoc;
        public string condicionIVA;
        public string condicionVenta;
        public string detalle;
        public string precioUnitario;
        public string cargada;

        public string Cargada
        {
            get => cargada;
            set => cargada = value;
        }


        public string CondicionVenta
        {
            get => condicionVenta;
            set => condicionVenta = value;
        }
        public string Detalle
        {
            get => detalle;
            set => detalle = value;
        }
        public string PrecioUnitario
        {
            get => precioUnitario;
            set => precioUnitario = value;
        }
        
    

        public string CondicionIVA
        {
            get => condicionIVA;
            set => condicionIVA = value;
        }
        public string ActividadAsoc
        {
            get => actividadAsoc;
            set => actividadAsoc = value;
        }
        public string Concepto
        {
            get => concepto;
            set => concepto = value;
        }
        public string FechaDesde
        {
            get => fechaDesde;
            set => fechaDesde = value;

        }
        public string FechaHasta
        {
            get => fechaHasta;
            set => fechaHasta = value;
        }
        public string Fecha
        {
            get => fecha; 
            set => fecha = value;

        }

        public string Tipo
        {
            get => tipo;
            set => tipo = value;
        }

        public string PuntoDeVenta
        {
            get => puntoDeVenta;
            set => puntoDeVenta = value;

        }
        
        
        public string TipoDocReceptor
        {
            get => tipoDocReceptor;
            set => tipoDocReceptor = value;
        }

        public string DenominacionReceptor
        {
            get => denominacionReceptor;
            set => denominacionReceptor = value;
        }

       
        public string NroDocReceptor
        {
            get => nroDocReceptor;
            set => nroDocReceptor = value;
        }

       

        public Factura(string fecha, string fechaDesde, string fechaHasta, string concepto, string actividadAsoc, string condicionIVA, string tipo, string puntoDeVenta, string tipoDocReceptor, string nroDocReceptor, string denominacionReceptor, string condicionVenta, string detalle, string precioUnitario, string cargada)
        {
            Fecha = fecha;
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            Concepto = concepto;
            ActividadAsoc = actividadAsoc;
            CondicionIVA = condicionIVA;
            Tipo = tipo;
            PuntoDeVenta = puntoDeVenta;
            
            TipoDocReceptor = tipoDocReceptor;
            NroDocReceptor = nroDocReceptor;
            DenominacionReceptor = denominacionReceptor;
            CondicionVenta = condicionVenta;
            Detalle = detalle;
            PrecioUnitario = precioUnitario;
            Cargada = cargada;
            


        
        }
    }
}
