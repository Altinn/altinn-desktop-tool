***REMOVED***
***REMOVED***
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

using AltinnDesktopTool.Model;
using AltinnDesktopTool.Utils.Helpers;
using AltinnDesktopTool.Utils.PubSub;
using AltinnDesktopTool.View;

using AutoMapper;
using GalaSoft.MvvmLight.Command;
using log4net;

***REMOVED***
***REMOVED***
using RestClient.Resources;

namespace AltinnDesktopTool.ViewModel
***REMOVED***
***REMOVED***
    /// ViewModel for SearchResult view
***REMOVED***
    public sealed class SearchResultViewModel : AltinnViewModelBase
    ***REMOVED***
        private readonly ILog logger;
        private readonly IMapper mapper;
        private IRestQuery restQuery;

    ***REMOVED***
        /// Initializes a new instance of the SearchResultViewModel class.
    ***REMOVED***
        /// <param name="logger">The logger to be used by the instance.</param>
        /// <param name="mapper">The AutoMapper instance to use by the view model.</param>
        /// <param name="restQuery">The query proxy to use in the actual context.</param>
        public SearchResultViewModel(ILog logger, IMapper mapper, IRestQuery restQuery)
        ***REMOVED***
            this.logger = logger;
            this.mapper = mapper;
            this.restQuery = restQuery;

            this.Model = new SearchResultModel ***REMOVED*** ResultCollection = new ObservableCollection<OrganizationModel>() ***REMOVED***;

            PubSub<ObservableCollection<OrganizationModel>>.RegisterEvent(EventNames.SearchResultReceivedEvent, this.SearchResultReceivedEventHandler);
            PubSub<bool>.RegisterEvent(EventNames.SearchStartedEvent, this.SearchStartedEventHandler);
            PubSub<string>.RegisterEvent(EventNames.EnvironmentChangedEvent, this.EnvironmentChangedEventHandler);
            PubSub<OrganizationModel>.RegisterEvent(EventNames.OrganizationSelectedChangedEvent, this.OrganizationSelectedChangedEventHandler);
            PubSub<SearchResultModel>.RegisterEvent(EventNames.OrganizationSelectedAllChangedEvent, this.OrganizationSelectedAllChangedEventHandler);

            this.GetContactsCommand = new RelayCommand<OrganizationModel>(this.GetContactsCommandHandler);
            this.CopyToClipboardPlainTextCommand = new RelayCommand(this.CopyToClipboardPlainTextHandler);
            this.CopyToClipboardExcelFormatCommand = new RelayCommand(this.CopyToClipboardExcelFormatHandler);
***REMOVED***

    ***REMOVED***
        /// Gets or sets the SearchResult model
    ***REMOVED***
        public new SearchResultModel Model ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets the GetContacts command
    ***REMOVED***
        public RelayCommand<OrganizationModel> GetContactsCommand ***REMOVED*** get; private set; ***REMOVED***

    ***REMOVED***
        /// Gets the CopyToClipboardPlainText command
    ***REMOVED***
        public ICommand CopyToClipboardPlainTextCommand ***REMOVED*** get; private set; ***REMOVED***

    ***REMOVED***
        /// Gets the CopyToClipboardExcelFormat command
    ***REMOVED***
        public ICommand CopyToClipboardExcelFormatCommand ***REMOVED*** get; private set; ***REMOVED***

    ***REMOVED***
        /// Handler for SearchResultReceived event
    ***REMOVED***        
        /// <param name="sender">Sender object - not used in this context</param>
        /// <param name="args">Result collection</param>
        public void SearchResultReceivedEventHandler(object sender, PubSubEventArgs<ObservableCollection<OrganizationModel>> args)
        ***REMOVED***
            this.logger.Debug("Handling search result received event.");
            this.Model.ResultCollection = args.Item;
            this.Model.SelectAllChecked = false;
            this.Model.IsBusy = false;
***REMOVED***

    ***REMOVED***
        /// Copy to clipboard in plain text logic
    ***REMOVED***
        public void CopyToClipboardPlainTextHandler()
        ***REMOVED***
            StringBuilder stringBuilder = new StringBuilder();
            IEnumerable<OrganizationModel> organizationsWithContacts = this.GetOrganizationsWithContacts(this.Model.ResultCollection.Where(x => x.IsSelected));

            foreach (OrganizationModel organizationModel in organizationsWithContacts)
            ***REMOVED***
                stringBuilder.Append(organizationModel.Name + " " + organizationModel.Type + " " + organizationModel.OrganizationNumber + Environment.NewLine);

                if (organizationModel.OfficalContactsCollection.Count > 0)
                ***REMOVED***
                    stringBuilder.Append(Resources.OfficialContactsTitle + ":" + Environment.NewLine);

                    foreach (OfficialContactModel officialContactModel in organizationModel.OfficalContactsCollection)
                    ***REMOVED***
                        if (!string.IsNullOrEmpty(officialContactModel.EmailAddress) && !string.IsNullOrEmpty(officialContactModel.MobileNumber))
                        ***REMOVED***
                            stringBuilder.Append(officialContactModel.EmailAddress + " " + officialContactModel.MobileNumber + Environment.NewLine);
                            continue;
                ***REMOVED***

                        if (!string.IsNullOrEmpty(officialContactModel.EmailAddress))
                        ***REMOVED***
                            stringBuilder.Append(officialContactModel.EmailAddress + Environment.NewLine);
                            continue;
                ***REMOVED***

                        if (!string.IsNullOrEmpty(officialContactModel.MobileNumber))
                        ***REMOVED***
                            stringBuilder.Append(officialContactModel.MobileNumber + Environment.NewLine);
                ***REMOVED***
            ***REMOVED***
        ***REMOVED***

                stringBuilder.Append(Environment.NewLine);
    ***REMOVED***

            Clipboard.SetText(stringBuilder.ToString());
***REMOVED***

    ***REMOVED***
        /// Copy to clipboard in excel format logic
    ***REMOVED***
        public void CopyToClipboardExcelFormatHandler()
        ***REMOVED***
            StringBuilder stringBuilder = new StringBuilder();
            string separator = "\t";

            IEnumerable<OrganizationModel> organizationsWithContacts = this.GetOrganizationsWithContacts(this.Model.ResultCollection.Where(x => x.IsSelected));
            foreach (OrganizationModel organizationModel in organizationsWithContacts)
            ***REMOVED***
                foreach (OfficialContactModel officialContactModel in organizationModel.OfficalContactsCollection)
                ***REMOVED***
                    stringBuilder.Append(organizationModel.Name + " " + organizationModel.Type + separator + organizationModel.OrganizationNumber + separator);

                    string mobilNumber = !string.IsNullOrEmpty(officialContactModel.MobileNumber) ? officialContactModel.MobileNumber : string.Empty;
                    if (mobilNumber.StartsWith("0"))
                    ***REMOVED***
                        mobilNumber = "=" + "\"" + mobilNumber + "\"";
            ***REMOVED***

                    stringBuilder.Append(officialContactModel.EmailAddress + separator + mobilNumber + Environment.NewLine);
        ***REMOVED***

                if (organizationModel.OfficalContactsCollection.Count != 0)
                ***REMOVED***
                    stringBuilder.AppendLine();
        ***REMOVED***
    ***REMOVED***

            Clipboard.SetText(stringBuilder.ToString());
***REMOVED***

        private void OrganizationSelectedAllChangedEventHandler(object sender, PubSubEventArgs<SearchResultModel> e)
        ***REMOVED***
            int selectedCount = this.Model.ResultCollection.Select(x => x.IsSelected).Count();
            if (selectedCount != this.Model.ResultCollection.Count)
            ***REMOVED***
                return;
    ***REMOVED***
            //// Set all items to the same selected value 
            foreach (OrganizationModel organizationModel in this.Model.ResultCollection)
            ***REMOVED***
                organizationModel.SetIsSelected(this.Model.SelectAllChecked);
    ***REMOVED***
***REMOVED***

        private void EnvironmentChangedEventHandler(object sender, PubSubEventArgs<string> e)
        ***REMOVED***
            this.logger.Debug("Handling environment changed received event.");
            this.restQuery = new RestQuery(ProxyConfigHelper.GetConfig(e.Item), this.logger);
            this.Model.ResultCollection = new ObservableCollection<OrganizationModel>();
            this.Model.EmptyMessageVisibility = false;
            this.Model.SelectAllChecked = false;
***REMOVED***

        private void OrganizationSelectedChangedEventHandler(object sender, PubSubEventArgs<OrganizationModel> e)
        ***REMOVED***
            int selectedCount = this.Model.ResultCollection.Select(x => x.IsSelected).Count();
            if ((selectedCount != this.Model.ResultCollection.Count) || !e.Item.IsSelected)
            ***REMOVED***
                this.Model.SetSelectAllChecked(false);
    ***REMOVED***
***REMOVED***

        private IEnumerable<OrganizationModel> GetOrganizationsWithContacts(IEnumerable<OrganizationModel> orgs)
        ***REMOVED***
            IEnumerable<OrganizationModel> organizationsWithContacts = orgs as IList<OrganizationModel> ?? orgs.ToList();
            foreach (OrganizationModel organizationModel in organizationsWithContacts)
            ***REMOVED***
                this.GetContactsCommandHandler(organizationModel);
    ***REMOVED***

            return organizationsWithContacts;
***REMOVED***

        private void SearchStartedEventHandler(object sender, PubSubEventArgs<bool> e)
        ***REMOVED***
            this.Model.IsBusy = true;
            this.Model.EmptyMessageVisibility = false;
***REMOVED***

        private void GetContactsCommandHandler(OrganizationModel obj)
        ***REMOVED***
            if (obj == null)
            ***REMOVED***
                return;
    ***REMOVED***

            if (obj.OfficalContactsCollection == null && !string.IsNullOrEmpty(obj.OfficialContacts))
            ***REMOVED***
                IList<OfficialContact> officialContactDtoCollection = new List<OfficialContact>();

                try
                ***REMOVED***
                    officialContactDtoCollection = this.restQuery.GetByLink<OfficialContact>(obj.OfficialContacts);
        ***REMOVED***
                catch (RestClientException rex)
                ***REMOVED***
                    this.logger.Error("Exception from the RestClient", rex);
        ***REMOVED***

                obj.OfficalContactsCollection =
                    this.mapper.Map<ICollection<OfficialContact>, ObservableCollection<OfficialContactModel>>(
                        officialContactDtoCollection);
    ***REMOVED***

            if (obj.PersonalContactsCollection != null || string.IsNullOrEmpty(obj.PersonalContacts))
            ***REMOVED***
                return;
    ***REMOVED***

            IList<PersonalContact> personalContactDtoCollecton = new List<PersonalContact>();

            try
            ***REMOVED***
                personalContactDtoCollecton = this.restQuery.GetByLink<PersonalContact>(obj.PersonalContacts);
    ***REMOVED***
            catch (RestClientException rex)
            ***REMOVED***
                this.logger.Error("Exception from the RestClient", rex);
    ***REMOVED***

            obj.PersonalContactsCollection =
                this.mapper.Map<ICollection<PersonalContact>, ObservableCollection<PersonalContactModel>>(
                    personalContactDtoCollecton);
***REMOVED***
***REMOVED***
***REMOVED***
