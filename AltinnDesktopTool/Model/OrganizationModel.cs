using System.Collections.ObjectModel;

using AltinnDesktopTool.Utils.PubSub;

namespace AltinnDesktopTool.Model
***REMOVED***
***REMOVED***
    /// Model for Organization
***REMOVED***
    public class OrganizationModel : ModelBase
    ***REMOVED***
        private ObservableCollection<OfficialContactModel> officalContactsCollection;

        private ObservableCollection<PersonalContactModel> personalContactsCollection;
        private bool isSelected;

    ***REMOVED***
        /// Gets or sets the Organization Name (as mapped from DTO)
    ***REMOVED***
        public string Name ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the Organization Number (as mapped from DTO)
    ***REMOVED***
        public string OrganizationNumber ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the Organization Type (AS, ANS etc) (as mapped from DTO)
    ***REMOVED***
        public string Type ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the Link to official contacts (as mapped from DTO)
    ***REMOVED***
        public string OfficialContacts ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the Link to personal contacts (as mapped from DTO)
    ***REMOVED***
        public string PersonalContacts ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the Collection of Official Contacts, populate this to automatically update view
    ***REMOVED***
        public ObservableCollection<OfficialContactModel> OfficalContactsCollection
        ***REMOVED***
            get
            ***REMOVED***
                return this.officalContactsCollection;
    ***REMOVED***

            set
            ***REMOVED***
                this.officalContactsCollection = value;
                this.RaisePropertyChanged();
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Gets or sets the Collection of Personal Contacts, populate this to automatically update view
    ***REMOVED***
        public ObservableCollection<PersonalContactModel> PersonalContactsCollection
        ***REMOVED***
            get
            ***REMOVED***
                return this.personalContactsCollection;
    ***REMOVED***

            set
            ***REMOVED***
                this.personalContactsCollection = value;
                this.RaisePropertyChanged();
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Gets or sets a value indicating whether an item is selected
    ***REMOVED***
        public bool IsSelected
        ***REMOVED***
            get
            ***REMOVED***
                return this.isSelected;
    ***REMOVED***

            set
            ***REMOVED***
                this.isSelected = value;
                this.RaisePropertyChanged(() => this.IsSelected);
                PubSub<OrganizationModel>.RaiseEvent(EventNames.OrganizationSelectedChangedEvent, this, new PubSubEventArgs<OrganizationModel>(this));
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Sets IsSelected
    ***REMOVED***
        /// <param name="value">boolean value</param>
        public void SetIsSelected(bool value)
        ***REMOVED***
            this.isSelected = value;
            this.RaisePropertyChanged(() => this.IsSelected);
***REMOVED***
***REMOVED***
***REMOVED***
