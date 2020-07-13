
using ApiConsumer.Services;
using ClaroClient2.Helpers;
using ClaroNet.models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ClaroClient2.viewModels
{
    public class RecargaPageViewModel:baseViewModel
    {
        private string nombreUsuario;
        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set
            {
                nombreUsuario = value;
                onPropertyChanged(nameof(NombreUsuario));
            }
        }
        private string _telefono;
        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; onPropertyChanged(nameof(Telefono)); }
        }

        private string _Monto;
        public string Monto
        {
            get { return _Monto; }
            set { _Monto = value; onPropertyChanged(nameof(Monto)); }
        }
        public RecargaPageViewModel()
        {
            mensajeAnterior = string.Empty;
            this.Subscribe<string>(Events.SmsRecieved, code =>
            {
                Notificacion(code);
            });
            NombreUsuario = $"{Session.GetInstance().UsuarioLogueado.data.usuario.nombre}";
        }
        public RelayCommand btnRecargar => new RelayCommand(Recargar);
        public async void Recargar()
        {
            try
            {
                if (!string.IsNullOrEmpty(Telefono) && (!string.IsNullOrEmpty(Monto)))
                    DependencyService.Get<IServiceCaller>()
                        .MakeCall(Telefono, Monto, Session.GetInstance().
                        UsuarioLogueado
                        .data
                        .pcr
                        .pin
                        .ToString());
                else
                    await App.Current.MainPage.DisplayAlert("Error", $"LLene todos los campos", "Accept");

            }
            catch (Exception ed)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ed.Message}.", "Accept");

            }
        }
        public string mensajeAnterior { get; set; }
        public async void Notificacion(string codigo)
        {
            if (mensajeAnterior.Equals(codigo))
            {
                mensajeAnterior = string.Empty;
                return;
            }

            var mensajeDescompuesto = codigo.Split('\n');
            if (mensajeDescompuesto.Count() <= 0 && !mensajeDescompuesto.First().Contains("RecargaCLR"))
                return;           
            mensajeAnterior = codigo;
            var MensajedeSaldo = mensajeDescompuesto[1].Split('.');
            var NuevoSaldo = MensajedeSaldo[2].Split(' ');
            Session.GetInstance().Saldo = NuevoSaldo.Last();
            ModuloGriseldaApi service = new ModuloGriseldaApi();
            var result =service.InsertarRecarga(
                new ApiConsumer.Services.Modulogriselda.Recarags.Request.InsertRecargasRequest
                {
                    email = Session.GetInstance().UsuarioLogueado.data.usuario.email,
                    mensaje = MensajedeSaldo[1],
                    pcr = Session.GetInstance().UsuarioLogueado.data.pcr.nombre
                },Session.GetInstance().UsuarioLogueado.token);
            Session.GetInstance().CambioRealizado();

        }
    }
}
