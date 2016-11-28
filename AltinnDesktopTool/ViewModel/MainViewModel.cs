using System.ComponentModel;
using System.Windows;

using AltinnDesktopTool.Configuration;
using AltinnDesktopTool.Utils.PubSub;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MahApps.Metro;

namespace AltinnDesktopTool.ViewModel
{
    /// <summary>
    /// ViewModel for MainView
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>        
        public MainViewModel()
        {
            PubSub<string>.RegisterEvent(EventNames.EnvironmentChangedEvent, this.EnvironmentChangedEventHandler);
            this.ClosingWindowCommand = new RelayCommand<CancelEventArgs>(this.ClosingWindowCommandHandler);
        }

        /// <summary>
        /// Gets or sets Closing window command
        /// </summary>
        public RelayCommand<CancelEventArgs> ClosingWindowCommand { get; set; }

        private void ClosingWindowCommandHandler(CancelEventArgs obj)
        {
            ViewModelLocator.Cleanup();
        }
        
        private void EnvironmentChangedEventHandler(object sender, PubSubEventArgs<string> args)
        {
            ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(EnvironmentConfigurationManager.ActiveEnvironmentConfiguration.ThemeName), ThemeManager.GetAppTheme("BaseLight"));
        }
    }
}