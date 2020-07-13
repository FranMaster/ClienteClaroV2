namespace ClaroClient2.Insfrastructure
{
    using viewModels;
    public class InstanceLocator
    {
        #region properties
        public MainViewModel Main { get; set; }
        #endregion

        #region ctor
        public InstanceLocator()
        {
            this.Main =  MainViewModel.GetInstance;
        }

        #endregion


    }
}
