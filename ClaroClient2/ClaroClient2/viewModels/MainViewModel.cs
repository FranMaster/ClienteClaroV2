using System;
using System.Collections.Generic;
using System.Text;

namespace ClaroClient2.viewModels
{
    public class MainViewModel
    {
        private static MainViewModel instance;

        #region ViewModels
        public HomePageViewModel Home { get; set; }
        public loginPageViewModel Login { get; set; } 
        #endregion
        public MainViewModel()
        {
            Login = new loginPageViewModel();
        }
        public static MainViewModel GetInstance
        {
            get
            {
                if (instance==null)                
                    instance = new MainViewModel();
                return instance;
            }
        }




    }
}
