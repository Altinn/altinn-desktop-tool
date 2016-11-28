using System.ComponentModel;
using System.Windows;

using AltinnDesktopTool.Configuration;
using AltinnDesktopTool.Utils.PubSub;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MahApps.Metro;

namespace AltinnDesktopTool.ViewModel
***REMOVED***
***REMOVED***
    /// ViewModel for MainView
***REMOVED***
    public class MainViewModel : ViewModelBase
    ***REMOVED***
    ***REMOVED***
        /// Initializes a new instance of the MainViewModel class.
    ***REMOVED***        
        public MainViewModel()
        ***REMOVED***
            PubSub<string>.RegisterEvent(EventNames.EnvironmentChangedEvent, this.EnvironmentChangedEventHandler);
            this.ClosingWindowCommand = new RelayCommand<CancelEventArgs>(this.ClosingWindowCommandHandler);
***REMOVED***

    ***REMOVED***
        /// Gets or sets Closing window command
    ***REMOVED***
        public RelayCommand<CancelEventArgs> ClosingWindowCommand ***REMOVED*** get; set; ***REMOVED***

        private void ClosingWindowCommandHandler(CancelEventArgs obj)
        ***REMOVED***
            ViewModelLocator.Cleanup();
***REMOVED***
        
        private void EnvironmentChangedEventHandler(object sender, PubSubEventArgs<string> args)
        ***REMOVED***
            ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(EnvironmentConfigurationManager.ActiveEnvironmentConfiguration.ThemeName), ThemeManager.GetAppTheme("BaseLight"));
***REMOVED***
***REMOVED***
***REMOVED***