using Dimension.Environment.Dimension.System.SystemApps.Dimension.AuthentificationApp.Views;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Unity.Windows;
using Prism.Windows.Navigation;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Dimension.Environment
{
    sealed partial class App : PrismUnityApplication
    {
        public App()
        {
            InitializeComponent();
        }

        protected override object Resolve(Type type)
        {
            return _unitycontainer.Resolve(type);
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(GetViewModelType);
            _unitycontainer = new UnityContainer();

            _unitycontainer.RegisterInstance<INavigationService>(this.NavigationService);

            return base.OnInitializeAsync(args);
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            Frame ZeroFrame = Window.Current.Content as Frame;
            ZeroFrame.Navigate(typeof(LoginView), null);
            return Task.FromResult<object>(null);
        }

        protected override Type GetPageType(string pageToken) // Имя View;
        {
            switch (pageToken)
            {
                case "LoginView":
                    return Type.GetType(String.Format(CultureInfo.InvariantCulture, VIEW_AUTHENTIFICATION_SERVICE_FORMAT, pageToken));
            }

            return null;
        }

        private Type GetViewModelType(Type viewType) // Type viewType = typeof(SomeTestView);
        {
            viewTypeString = Convert.ToString(viewType);

            switch (viewTypeString)
            {
                case "Dimension.Environment.Dimension.System.SystemApps.Dimension.AuthentificationApp.Views.LoginView":
                    return Type.GetType(String.Format(CultureInfo.InvariantCulture, VIEW_MODEL_AUTHENTIFICATION_SERVICE_FORMAT, viewType.Name));
                default:
                    Fault();
                    break;
            }

            return null;
        }

        private async void Fault()
        {
            MessageDialog _Fault = new MessageDialog("Не распознана ViewModel");
            await _Fault.ShowAsync();
        }

        private UnityContainer _unitycontainer;

        private string viewTypeString = null;

        private const string VIEW_SHELL_FORMAT = "DimensionOS.DimensionSystem.SystemGUI.Views.{0}";
        private const string VIEW_MODEL_SHELL_FORMAT = "DimensionOS.DimensionSystem.SystemGUI.ViewModels.{0}Model";

        private const string VIEW_AUTHENTIFICATION_SERVICE_FORMAT = "Dimension.Environment.Dimension.System.SystemApps.Dimension.AuthentificationApp.Views.{0}";
        private const string VIEW_MODEL_AUTHENTIFICATION_SERVICE_FORMAT = "Dimension.Environment.Dimension.System.SystemApps.Dimension.AuthentificationApp.ViewModels.{0}Model";
    }
}
