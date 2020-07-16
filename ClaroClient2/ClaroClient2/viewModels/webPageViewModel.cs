using ClaroClient2.viewModels.itemsViewModel;
using ClaroNet.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ClaroClient2.viewModels
{
    public class webPageViewModel:baseViewModel
    {

        #region Properties
        private string _Monto;

        public string Monto
        {
            get { return _Monto; }
            set { _Monto = value; onPropertyChanged(nameof(Monto)); }
        }

        private string _FechaActual;

        public string FechaActual
        {
            get { return _FechaActual; }
            set { _FechaActual = value; onPropertyChanged(nameof(FechaActual)); }
        }

        private string _establecimiento;

        public string Establecimiento
        {
            get { return _establecimiento; }
            set { _establecimiento = value; onPropertyChanged(nameof(Establecimiento)); }
        }


        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; onPropertyChanged(nameof(Description)); }
        }


        private int _total;

        public int Total
        {
            get { return _total; }
            set { _total = value; onPropertyChanged(nameof(Total)); }
        }

        private string _FechaRecarga;

        public string FechaRecarga
        {
            get { return _FechaRecarga; }
            set { _FechaRecarga = value;onPropertyChanged(nameof(FechaActual)); }
        }

        private string  _Factura;

        public string Factura
        {
            get { return _Factura; }
            set { _Factura = value;onPropertyChanged(nameof(Factura)); }
        }

        #endregion

        public webPageViewModel(itemRecargasViewModel itemRecargasViewModel)
        {
            FechaActual = DateTime.Now.ToLongDateString();
            var mensaje = itemRecargasViewModel.mensaje.Split(' ');
            Monto = mensaje[5];
            Description = string.Join(" ", mensaje);
            FechaRecarga = itemRecargasViewModel.fechaRecarga.ToShortDateString();
            Establecimiento = Session.GetInstance().UsuarioLogueado.data.pcr.nombre;
            Factura = $"{itemRecargasViewModel.fechaRecarga.Day}{itemRecargasViewModel.fechaRecarga.DayOfYear}{itemRecargasViewModel.fechaRecarga.Year}{itemRecargasViewModel.fechaRecarga.Hour}{itemRecargasViewModel.fechaRecarga.Minute}{itemRecargasViewModel.fechaRecarga.Second}";
        }












   
    }
}
