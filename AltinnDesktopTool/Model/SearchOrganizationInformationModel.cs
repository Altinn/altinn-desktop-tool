using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

using AltinnDesktopTool.Properties;

namespace AltinnDesktopTool.Model
***REMOVED***
***REMOVED***
    /// This model holds the data required to perform a search and details about the label that displays the result of the search.
***REMOVED***
    public class SearchOrganizationInformationModel : ModelBase
    ***REMOVED***
        private string searchText;
        private string labelText;
        private Brush labelBrush;

    ***REMOVED***
        /// Gets or sets the text to be displayed as feedback from the search.
    ***REMOVED***
        public string LabelText
        ***REMOVED***
            get
            ***REMOVED***
                return this.labelText; 
    ***REMOVED***

            set
            ***REMOVED***
                this.labelText = value;
                this.RaisePropertyChanged(() => this.LabelText);
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Gets or sets the brush used to define the look of the label foreground color.
    ***REMOVED***
        public Brush LabelBrush
        ***REMOVED***
            get
            ***REMOVED***
                return this.labelBrush;
    ***REMOVED***

            set
            ***REMOVED***
                this.labelBrush = value;
                this.RaisePropertyChanged(() => this.LabelBrush);
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Gets or sets the search text.
    ***REMOVED***
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "SearchOrganizationInformationModel_SearchText_You_must_enter_a_string_")]
        public string SearchText
        ***REMOVED***
            get
            ***REMOVED***
                return this.searchText; 
    ***REMOVED***

            set
            ***REMOVED***
                this.searchText = value;
                this.RaisePropertyChanged(() => this.SearchText);
                this.ValidateModelProperty(value, "SearchText");
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Gets or sets the search type indicating what the user is searching for.
    ***REMOVED***
        public SearchType SearchType ***REMOVED*** get; set; ***REMOVED***
***REMOVED***
***REMOVED***