using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using AltinnDesktopTool.Configuration;
using AltinnDesktopTool.Utils.PubSub;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AltinnDesktopTool.ViewModel
***REMOVED***
***REMOVED***
    /// ViewModel class, container for Environment change related components
***REMOVED***
    public class FooterViewModel : ViewModelBase
    ***REMOVED***                
    ***REMOVED***
        /// Initializes a new instance of the <see cref="FooterViewModel"/> class.
        /// ViewModel for Footer view
    ***REMOVED***
        public FooterViewModel()
        ***REMOVED***
            this.ChangeEnvironmentCommand = new RelayCommand(this.ChangeEnvironmentHandler);

            this.EnvironmentNames = new ObservableCollection<string>(EnvironmentConfigurationManager.EnvironmentConfigurations.Select(c => c.Name).ToList());
            this.SelectedEnvironment = EnvironmentConfigurationManager.ActiveEnvironmentConfiguration.Name;

            PubSub<string>.AddEvent(EventNames.EnvironmentChangedEvent, this.EnvironmentChangedEventHandler);
***REMOVED***

    ***REMOVED***
        /// Environment changed event
    ***REMOVED***
        public event PubSubEventHandler<string> EnvironmentChangedEventHandler;

    ***REMOVED***
        /// Gets or sets the Selected environment name
    ***REMOVED***
        public string SelectedEnvironment ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the available environment names
    ***REMOVED***
        public ObservableCollection<string> EnvironmentNames ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets a command which will define a behavior on Change environment event
    ***REMOVED***
        public ICommand ChangeEnvironmentCommand ***REMOVED*** get; private set; ***REMOVED***        

    ***REMOVED***
        /// Event handler for the ChangeEnvironment command. Sets the active environment configuration and raises EnvironmentChanged event.
    ***REMOVED***
        public void ChangeEnvironmentHandler()
        ***REMOVED***
            EnvironmentConfigurationManager.ActiveEnvironmentConfiguration = EnvironmentConfigurationManager.EnvironmentConfigurations.Single(c => c.Name == this.SelectedEnvironment);
            PubSub<string>.RaiseEvent(EventNames.EnvironmentChangedEvent, this, new PubSubEventArgs<string>(this.SelectedEnvironment));
***REMOVED***
***REMOVED***
***REMOVED***
