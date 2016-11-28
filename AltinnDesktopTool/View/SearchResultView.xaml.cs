using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace AltinnDesktopTool.View
***REMOVED***
***REMOVED***
    /// Interaction logic for SearchResultView.xaml
***REMOVED***
    public partial class SearchResultView
    ***REMOVED***
    ***REMOVED***
        /// Initializes a new instance of the SearchResultView class.
    ***REMOVED***
        public SearchResultView()
        ***REMOVED***
            this.InitializeComponent();
***REMOVED***

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        ***REMOVED***
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.IsChecked ?? false)
            ***REMOVED***
                checkBox.SetCurrentValue(ToggleButton.IsCheckedProperty, false);
    ***REMOVED***
            else
            ***REMOVED***
                checkBox.SetCurrentValue(ToggleButton.IsCheckedProperty, true);
    ***REMOVED***

            e.Handled = true;
***REMOVED***
***REMOVED***
***REMOVED***
