using ApiConsumer.Services;
using ApiConsumer.Services.Modulogriselda.RecaragsListados.Response;
using ClaroClient2.viewModels.itemsViewModel;
using ClaroClient2.views;
using ClaroNet.models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ClaroClient2.viewModels
{
    public class PcrPageViewModel:baseViewModel
    {
		public PcrPageViewModel()
		{
			NombreUsuario = $"{Session.GetInstance().UsuarioLogueado.data.usuario.nombre} ";
			CargarRecargas();
			Session.GetInstance().CambiosEnSaldo += PcrViewModel_CambiosEnSaldo;
			VisibilidadSaldo = false;
			CargarSaldo();


		}


		public void CargarSaldo()
		{

            if (ListaRecientes != null && ListaRecientes.Count > 0)
            {
                var a = ListaRecientes.First().mensaje;
                var mensajeDescompuesto = a.Split('\n');
                if (mensajeDescompuesto.Count() <= 0 && !mensajeDescompuesto.First().Contains("RecargaCLR"))
                    return;
               // var MensajedeSaldo = mensajeDescompuesto[1].Split('.');
                var e = a.Split(' ');
                SaldoActual = $" $ {e.Last()}";
                VisibilidadSaldo = true;

            }

        }

        private void PcrViewModel_CambiosEnSaldo(object sender, EventArgs e)
		{
			CargarRecargas();
			SaldoActual = Session.GetInstance().SaldoActual;
			VisibilidadSaldo = true;
		}

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
		private string _pcr;
		public string Pcr
		{
			get { return _pcr; }
			set { _pcr = value; onPropertyChanged(nameof(Pcr)); }
		}
        private ObservableCollection<itemRecargasViewModel> _listaRecientes;
        public ObservableCollection<itemRecargasViewModel> ListaRecientes
        {
            get { return _listaRecientes; }
            set
            {
                _listaRecientes = value; onPropertyChanged(nameof(ListaRecientes));
            }
        }
        private bool _VisibilidadSaldo;

		public bool VisibilidadSaldo
		{
			get { return _VisibilidadSaldo; }
			set { _VisibilidadSaldo = value; onPropertyChanged(nameof(VisibilidadSaldo)); }
		}


		private string saldoActual;

		public string SaldoActual
		{
			get { return saldoActual; }
			set { saldoActual = value; onPropertyChanged(nameof(SaldoActual)); }
		}

		public RelayCommand NuevaRecarga => new RelayCommand(btnNuevaRecarga);



		private async void btnNuevaRecarga()
		{
			RecargasPage vw = new RecargasPage();
			vw.BindingContext = new RecargaPageViewModel();
			await Application.Current.MainPage.Navigation.PushAsync(vw);
		}
		

		public  void CargarRecargas()
		{
			var sesion = Session.GetInstance();				  
			var service =  new ModuloGriseldaApi();
              var resp=service.ObtenerRecargasRecientes(
				 new ApiConsumer.Services.Modulogriselda.Recarags.Request.GetRecargasRequest { email = sesion.UsuarioLogueado.data.usuario.email },
				  sesion.UsuarioLogueado.token
				 );
            if (!resp.Success)
                return;
            resp.ObjectData.recarga.Reverse();
            ListaRecientes = new ObservableCollection<itemRecargasViewModel>(this.ToItemRecargaViewModel(resp.ObjectData.recarga));
			

		}

        #region Methods
        private IEnumerable<itemRecargasViewModel> ToItemRecargaViewModel(List<GetRecarga> recarga)
        {
		
			return recarga.Select(l => new itemRecargasViewModel
			{
				fechaRecarga = l.fechaRecarga,
				mensaje = l.mensaje
			}).ToList();
        } 
        #endregion
    }
}
