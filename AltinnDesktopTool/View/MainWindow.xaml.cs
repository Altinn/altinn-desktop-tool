using System.Windows;
using MahApps.Metro.Controls.Dialogs;

namespace AltinnDesktopTool.View
***REMOVED***
***REMOVED***
    /// Interaction logic for MainWindow.xaml
***REMOVED***
    public partial class MainWindow
    ***REMOVED***
    ***REMOVED***
        /// Initializes a new instance of the MainWindow class.
    ***REMOVED***
        public MainWindow()
        ***REMOVED***
            this.InitializeComponent();
***REMOVED***

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        ***REMOVED***
            await this.ShowMessageAsync(View.Resources.HeaderText, View.Resources.InfoText);
***REMOVED***
***REMOVED***
***REMOVED***
