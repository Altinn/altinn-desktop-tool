/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:AltinnDesktopTool"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="***REMOVED***Binding Source=***REMOVED***StaticResource Locator***REMOVED***, Path=ViewModelName***REMOVED***"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using AltinnDesktopTool.Utils.Helpers;
using AltinnDesktopTool.Utils.PubSub;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

using log4net;
using Microsoft.Practices.ServiceLocation;

***REMOVED***

namespace AltinnDesktopTool.ViewModel
***REMOVED***
***REMOVED***
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
***REMOVED***
    public class ViewModelLocator
    ***REMOVED***
    ***REMOVED***
        /// Initializes a new instance of the ViewModelLocator class.
    ***REMOVED***
        public ViewModelLocator()
        ***REMOVED***
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
***REMOVED***

    ***REMOVED***
        /// Gets the MainViewModel
    ***REMOVED***
        public ViewModelBase Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        
    ***REMOVED***
        /// Gets the SearchOrganizationInformationViewModel
    ***REMOVED***
        public SearchOrganizationInformationViewModel SearchOrganizationInformationViewModel => ServiceLocator.Current.GetInstance<SearchOrganizationInformationViewModel>();
        
    ***REMOVED***
        /// Gets the SearchResultViewModel
    ***REMOVED***
        public SearchResultViewModel SearchResultViewModel => ServiceLocator.Current.GetInstance<SearchResultViewModel>();
        
    ***REMOVED***
        /// Gets the TopViewModel
    ***REMOVED***
        public TopViewModel TopViewModel => ServiceLocator.Current.GetInstance<TopViewModel>();
        
    ***REMOVED***
        /// Gets the FooterViewModel
    ***REMOVED***
        public FooterViewModel FooterViewModel => ServiceLocator.Current.GetInstance<FooterViewModel>();

    ***REMOVED***
        /// Dispose allocated resources here
    ***REMOVED***
        public static void Cleanup()
        ***REMOVED***
            PubSub<object>.ClearEvents();
***REMOVED***
***REMOVED***
***REMOVED***