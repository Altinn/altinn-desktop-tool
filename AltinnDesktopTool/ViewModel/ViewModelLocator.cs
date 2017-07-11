/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:AltinnDesktopTool"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using AltinnDesktopTool.Utils.Helpers;
using AltinnDesktopTool.Utils.PubSub;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

using log4net;
using Microsoft.Practices.ServiceLocation;

using RestClient;

namespace AltinnDesktopTool.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Logging
            SimpleIoc.Default.Register(() => LogManager.GetLogger(this.GetType())); // ILog
            log4net.Config.XmlConfigurator.Configure();

            // AutoMapper
            SimpleIoc.Default.Register(AutoMapperHelper.RunCreateMaps); // IMapper

            // View models
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SearchOrganizationInformationViewModel>();
            SimpleIoc.Default.Register<SearchResultViewModel>();
            SimpleIoc.Default.Register<TopViewModel>();
            SimpleIoc.Default.Register<FooterViewModel>();

            // Proxy
            SimpleIoc.Default.Register<IRestQuery>(() => new RestQuery(ProxyConfigHelper.GetConfig(), ServiceLocator.Current.GetInstance<ILog>()));
        }

        /// <summary>
        /// Gets the MainViewModel
        /// </summary>
        public ViewModelBase Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        
        /// <summary>
        /// Gets the SearchOrganizationInformationViewModel
        /// </summary>
        public SearchOrganizationInformationViewModel SearchOrganizationInformationViewModel => ServiceLocator.Current.GetInstance<SearchOrganizationInformationViewModel>();
        
        /// <summary>
        /// Gets the SearchResultViewModel
        /// </summary>
        public SearchResultViewModel SearchResultViewModel => ServiceLocator.Current.GetInstance<SearchResultViewModel>();
        
        /// <summary>
        /// Gets the TopViewModel
        /// </summary>
        public TopViewModel TopViewModel => ServiceLocator.Current.GetInstance<TopViewModel>();
        
        /// <summary>
        /// Gets the FooterViewModel
        /// </summary>
        public FooterViewModel FooterViewModel => ServiceLocator.Current.GetInstance<FooterViewModel>();

        /// <summary>
        /// Dispose allocated resources here
        /// </summary>
        public static void Cleanup()
        {
            PubSub<object>.ClearEvents();
        }
    }
}