using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using AltinnDesktopTool.Configuration;
using AltinnDesktopTool.Utils.PubSub;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AltinnDesktopTool.ViewModel
{
    /// <summary>
    /// ViewModel class, container for Environment change related components
    /// </summary>
    public class FooterViewModel : ViewModelBase
    {                
        /// <summary>
        /// Initializes a new instance of the <see cref="FooterViewModel"/> class.
        /// ViewModel for Footer view
        /// </summary>
        public FooterViewModel()
        {
            this.ChangeEnvironmentCommand = new RelayCommand(this.ChangeEnvironmentHandler);

            this.EnvironmentNames = new ObservableCollection<string>(EnvironmentConfigurationManager.EnvironmentConfigurations.Select(c => c.Name).ToList());
            this.SelectedEnvironment = EnvironmentConfigurationManager.ActiveEnvironmentConfiguration.Name;

            PubSub<string>.AddEvent(EventNames.EnvironmentChangedEvent, this.EnvironmentChangedEventHandler);
        }

        /// <summary>
        /// Environment changed event
        /// </summary>
        public event PubSubEventHandler<string> EnvironmentChangedEventHandler;

        /// <summary>
        /// Gets or sets the Selected environment name
        /// </summary>
        public string SelectedEnvironment { get; set; }

        /// <summary>
        /// Gets or sets the available environment names
        /// </summary>
        public ObservableCollection<string> EnvironmentNames { get; set; }

        /// <summary>
        /// Gets a command which will define a behavior on Change environment event
        /// </summary>
        public ICommand ChangeEnvironmentCommand { get; private set; }        

        /// <summary>
        /// Event handler for the ChangeEnvironment command. Sets the active environment configuration and raises EnvironmentChanged event.
        /// </summary>
        public void ChangeEnvironmentHandler()
        {
            EnvironmentConfigurationManager.ActiveEnvironmentConfiguration = EnvironmentConfigurationManager.EnvironmentConfigurations.Single(c => c.Name == this.SelectedEnvironment);
            PubSub<string>.RaiseEvent(EventNames.EnvironmentChangedEvent, this, new PubSubEventArgs<string>(this.SelectedEnvironment));
        }
    }
}
