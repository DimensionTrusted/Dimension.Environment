using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Dimension.Environment.Dimension.System.SystemApps.Dimension.AuthentificationApp.ViewModels
{
    public sealed class LoginViewModel : ViewModelBase
    {
        public DelegateCommand LoginCommand
        {
            get
            {
                return _authorizeCommand ?? (_authorizeCommand = new DelegateCommand(LoginClick));
            }
        }

        private async void LoginClick()
        {
            var dialog = new MessageDialog("Добро пожаловать");
            await dialog.ShowAsync();
            Frame StartScreenNavigationFrame = Window.Current.Content as Frame;
        }

        private DelegateCommand _authorizeCommand;
    }
}
