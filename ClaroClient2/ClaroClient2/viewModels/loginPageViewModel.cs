using ApiConsumer.Services;
using ApiConsumer.Services.Modulogriselda.login.Request;
using ClaroClient2.views;
using ClaroNet.models;
using GalaSoft.MvvmLight.Command;
using System.Linq;
using Xamarin.Forms;

namespace ClaroClient2.viewModels
{
   public class loginPageViewModel:baseViewModel
   {
        private string email;
        private string password;

        public string Email
        {
            get { return email; }
            set { email = value; onPropertyChanged(nameof(Email)); }
        }
        public string Password
        {
            get { return password; }
            set { password = value; onPropertyChanged(nameof(Password)); }
        }

        public RelayCommand btnLogin => new RelayCommand(Login);

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(password))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Por favor llena todos los Campos", "Volver");
                return;
            }
            var service = new ModuloGriseldaApi();
            var result=service.LogIN(new UserRequest { email=Email.Trim(),password=Password.Trim()});
            if (result.Success)
            {
                Session.GetInstance().UsuarioLogueado = result.ObjectData;
                HomePage vistaHome = new HomePage();
                Password = string.Empty;
                vistaHome.BindingContext = new HomePageViewModel();
                await Application.Current.MainPage.Navigation.PushAsync(vistaHome);
                var Vista= Application.Current.MainPage.Navigation.NavigationStack.FirstOrDefault();
                Application.Current.MainPage.Navigation.RemovePage(Vista);

            }
            else
            {
                string mensaje = "Tenemos Dificultades , intente en unos minutos";
                if (!result.MessageError.Contains("500"))                
                    mensaje = "Credenciales incorrectas";
                await Application.Current.MainPage.DisplayAlert("aviso", mensaje, "Salir");
            }
            
        }
    }
}
