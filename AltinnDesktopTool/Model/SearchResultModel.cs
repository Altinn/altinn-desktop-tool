using System.Collections.ObjectModel;

using AltinnDesktopTool.Utils.PubSub;

namespace AltinnDesktopTool.Model
***REMOVED***
***REMOVED***
    /// Search Result Model
***REMOVED***
    public class SearchResultModel : ModelBase
    ***REMOVED***
        private ObservableCollection<OrganizationModel> resultCollection;

        private bool emptyMessageVisibility;

        private string infoText = string.Empty;
        private bool selectAllChecked;

    ***REMOVED***
        /// Gets or sets a value indicating whether the Info text is visible in the result grid
    ***REMOVED***
        public bool EmptyMessageVisibility
        ***REMOVED***
            get
            ***REMOVED***
                return this.emptyMessageVisibility;
    ***REMOVED***

            set
            ***REMOVED***
                this.emptyMessageVisibility = value;
                this.RaisePropertyChanged(() => this.EmptyMessageVisibility);
                this.InfoText = value ? View.Resources.NoDataText : string.Empty;
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Gets or sets the Info text shown in the result grid
    ***REMOVED***
        public string InfoText
        ***REMOVED***
            get
            ***REMOVED***
                return this.infoText;
    ***REMOVED***

            set
            ***REMOVED***
                this.infoText = value;
                this.RaisePropertyChanged(() => this.InfoText);
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Gets or sets the Result collection presented in the Organization grid
    ***REMOVED***
        public ObservableCollection<OrganizationModel> ResultCollection
        ***REMOVED***
            get
            ***REMOVED***
                return this.resultCollection;
    ***REMOVED***

            set
            ***REMOVED***
                this.resultCollection = value;
                this.RaisePropertyChanged(() => this.ResultCollection);
                this.EmptyMessageVisibility = (this.resultCollection == null) || (this.resultCollection.Count == 0);
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Gets or sets a value indicating whether all items are checked or not
    ***REMOVED***
        public bool SelectAllChecked
        ***REMOVED***
            get
            ***REMOVED***
                return this.selectAllChecked;
    ***REMOVED***

            set
            ***REMOVED***
                this.selectAllChecked = value;
                this.RaisePropertyChanged(() => this.SelectAllChecked);
                PubSub<SearchResultModel>.RaiseEvent(EventNames.OrganizationSelectedAllChangedEvent, this, new PubSubEventArgs<SearchResultModel>(this));
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Sets SelectAllChecked
    ***REMOVED***
        /// <param name="value">boolean value</param>
        public void SetSelectAllChecked(bool value)
        ***REMOVED***
            this.selectAllChecked = false;
            this.RaisePropertyChanged(() => this.SelectAllChecked);
***REMOVED***
***REMOVED***
***REMOVED***