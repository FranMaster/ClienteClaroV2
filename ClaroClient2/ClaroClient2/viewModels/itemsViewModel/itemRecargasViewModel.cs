using ApiConsumer.Services.Modulogriselda.RecaragsListados.Response;
using ClaroClient2.views;
using GalaSoft.MvvmLight.Command;
using System;

namespace ClaroClient2.viewModels.itemsViewModel
{
    public class itemRecargasViewModel : GetRecarga
    {
        public static int cont = 0;
        public String Id { get; set; }
        public RelayCommand SelectWebView => new RelayCommand(MostrarFactura);

        private async void MostrarFactura()
        {
      
            MainViewModel.GetInstance.Facturas = new webPageViewModel(this);
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ClaroClient2.views.webViewPage());
            cont++;

        }
    }
}
