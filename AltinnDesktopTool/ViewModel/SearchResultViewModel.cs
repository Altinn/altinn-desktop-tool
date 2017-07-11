using System;
using System.Collections.Generic;
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

using RestClient;
using RestClient.DTO;
using RestClient.Resources;

namespace AltinnDesktopTool.ViewModel
{
    /// <summary>
    /// ViewModel for SearchResult view
    /// </summary>
    public sealed class SearchResultViewModel : AltinnViewModelBase
    {
        private readonly ILog logger;
        private readonly IMapper mapper;
        private IRestQuery restQuery;

        /// <summary>
        /// Initializes a new instance of the SearchResultViewModel class.
        /// </summary>
        /// <param name="logger">The logger to be used by the instance.</param>
        /// <param name="mapper">The AutoMapper instance to use by the view model.</param>
        /// <param name="restQuery">The query proxy to use in the actual context.</param>
        public SearchResultViewModel(ILog logger, IMapper mapper, IRestQuery restQuery)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.restQuery = restQuery;

            this.Model = new SearchResultModel { ResultCollection = new ObservableCollection<OrganizationModel>() };

            PubSub<ObservableCollection<OrganizationModel>>.RegisterEvent(EventNames.SearchResultReceivedEvent, this.SearchResultReceivedEventHandler);
            PubSub<bool>.RegisterEvent(EventNames.SearchStartedEvent, this.SearchStartedEventHandler);
            PubSub<string>.RegisterEvent(EventNames.EnvironmentChangedEvent, this.EnvironmentChangedEventHandler);
            PubSub<OrganizationModel>.RegisterEvent(EventNames.OrganizationSelectedChangedEvent, this.OrganizationSelectedChangedEventHandler);
            PubSub<SearchResultModel>.RegisterEvent(EventNames.OrganizationSelectedAllChangedEvent, this.OrganizationSelectedAllChangedEventHandler);

            this.GetContactsCommand = new RelayCommand<OrganizationModel>(this.GetContactsCommandHandler);
            this.CopyToClipboardPlainTextCommand = new RelayCommand(this.CopyToClipboardPlainTextHandler);
            this.CopyToClipboardExcelFormatCommand = new RelayCommand(this.CopyToClipboardExcelFormatHandler);
        }

        /// <summary>
        /// Gets or sets the SearchResult model
        /// </summary>
        public new SearchResultModel Model { get; set; }

        /// <summary>
        /// Gets the GetContacts command
        /// </summary>
        public RelayCommand<OrganizationModel> GetContactsCommand { get; private set; }

        /// <summary>
        /// Gets the CopyToClipboardPlainText command
        /// </summary>
        public ICommand CopyToClipboardPlainTextCommand { get; private set; }

        /// <summary>
        /// Gets the CopyToClipboardExcelFormat command
        /// </summary>
        public ICommand CopyToClipboardExcelFormatCommand { get; private set; }

        /// <summary>
        /// Handler for SearchResultReceived event
        /// </summary>        
        /// <param name="sender">Sender object - not used in this context</param>
        /// <param name="args">Result collection</param>
        public void SearchResultReceivedEventHandler(object sender, PubSubEventArgs<ObservableCollection<OrganizationModel>> args)
        {
            this.logger.Debug("Handling search result received event.");
            this.Model.ResultCollection = args.Item;
            this.Model.SelectAllChecked = false;
            this.Model.IsBusy = false;
        }

        /// <summary>
        /// Copy to clipboard in plain text logic
        /// </summary>
        public void CopyToClipboardPlainTextHandler()
        {
            StringBuilder stringBuilder = new StringBuilder();
            IEnumerable<OrganizationModel> organizationsWithContacts = this.GetOrganizationsWithContacts(this.Model.ResultCollection.Where(x => x.IsSelected));

            foreach (OrganizationModel organizationModel in organizationsWithContacts)
            {
                stringBuilder.Append(organizationModel.Name + " " + organizationModel.Type + " " + organizationModel.OrganizationNumber + Environment.NewLine);

                if (organizationModel.OfficalContactsCollection.Count > 0)
                {
                    stringBuilder.Append(Resources.OfficialContactsTitle + ":" + Environment.NewLine);

                    foreach (OfficialContactModel officialContactModel in organizationModel.OfficalContactsCollection)
                    {
                        if (!string.IsNullOrEmpty(officialContactModel.EmailAddress) && !string.IsNullOrEmpty(officialContactModel.MobileNumber))
                        {
                            stringBuilder.Append(officialContactModel.EmailAddress + " " + officialContactModel.MobileNumber + Environment.NewLine);
                            continue;
                        }

                        if (!string.IsNullOrEmpty(officialContactModel.EmailAddress))
                        {
                            stringBuilder.Append(officialContactModel.EmailAddress + Environment.NewLine);
                            continue;
                        }

                        if (!string.IsNullOrEmpty(officialContactModel.MobileNumber))
                        {
                            stringBuilder.Append(officialContactModel.MobileNumber + Environment.NewLine);
                        }
                    }
                }

                stringBuilder.Append(Environment.NewLine);
            }

            Clipboard.SetText(stringBuilder.ToString());
        }

        /// <summary>
        /// Copy to clipboard in excel format logic
        /// </summary>
        public void CopyToClipboardExcelFormatHandler()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string separator = "\t";

            IEnumerable<OrganizationModel> organizationsWithContacts = this.GetOrganizationsWithContacts(this.Model.ResultCollection.Where(x => x.IsSelected));
            foreach (OrganizationModel organizationModel in organizationsWithContacts)
            {
                foreach (OfficialContactModel officialContactModel in organizationModel.OfficalContactsCollection)
                {
                    stringBuilder.Append(organizationModel.Name + " " + organizationModel.Type + separator + organizationModel.OrganizationNumber + separator);

                    string mobilNumber = !string.IsNullOrEmpty(officialContactModel.MobileNumber) ? officialContactModel.MobileNumber : string.Empty;
                    if (mobilNumber.StartsWith("0"))
                    {
                        mobilNumber = "=" + "\"" + mobilNumber + "\"";
                    }

                    stringBuilder.Append(officialContactModel.EmailAddress + separator + mobilNumber + Environment.NewLine);
                }

                if (organizationModel.OfficalContactsCollection.Count != 0)
                {
                    stringBuilder.AppendLine();
                }
            }

            Clipboard.SetText(stringBuilder.ToString());
        }

        private void OrganizationSelectedAllChangedEventHandler(object sender, PubSubEventArgs<SearchResultModel> e)
        {
            int selectedCount = this.Model.ResultCollection.Select(x => x.IsSelected).Count();
            if (selectedCount != this.Model.ResultCollection.Count)
            {
                return;
            }
            //// Set all items to the same selected value 
            foreach (OrganizationModel organizationModel in this.Model.ResultCollection)
            {
                organizationModel.SetIsSelected(this.Model.SelectAllChecked);
            }
        }

        private void EnvironmentChangedEventHandler(object sender, PubSubEventArgs<string> e)
        {
            this.logger.Debug("Handling environment changed received event.");
            this.restQuery = new RestQuery(ProxyConfigHelper.GetConfig(e.Item), this.logger);
            this.Model.ResultCollection = new ObservableCollection<OrganizationModel>();
            this.Model.EmptyMessageVisibility = false;
            this.Model.SelectAllChecked = false;
        }

        private void OrganizationSelectedChangedEventHandler(object sender, PubSubEventArgs<OrganizationModel> e)
        {
            int selectedCount = this.Model.ResultCollection.Select(x => x.IsSelected).Count();
            if ((selectedCount != this.Model.ResultCollection.Count) || !e.Item.IsSelected)
            {
                this.Model.SetSelectAllChecked(false);
            }
        }

        private IEnumerable<OrganizationModel> GetOrganizationsWithContacts(IEnumerable<OrganizationModel> orgs)
        {
            IEnumerable<OrganizationModel> organizationsWithContacts = orgs as IList<OrganizationModel> ?? orgs.ToList();
            foreach (OrganizationModel organizationModel in organizationsWithContacts)
            {
                this.GetContactsCommandHandler(organizationModel);
            }

            return organizationsWithContacts;
        }

        private void SearchStartedEventHandler(object sender, PubSubEventArgs<bool> e)
        {
            this.Model.IsBusy = true;
            this.Model.EmptyMessageVisibility = false;
        }

        private void GetContactsCommandHandler(OrganizationModel obj)
        {
            if (obj == null)
            {
                return;
            }

            if (obj.OfficalContactsCollection == null && !string.IsNullOrEmpty(obj.OfficialContacts))
            {
                IList<OfficialContact> officialContactDtoCollection = new List<OfficialContact>();

                try
                {
                    officialContactDtoCollection = this.restQuery.GetByLink<OfficialContact>(obj.OfficialContacts);
                }
                catch (RestClientException rex)
                {
                    this.logger.Error("Exception from the RestClient", rex);
                }

                obj.OfficalContactsCollection =
                    this.mapper.Map<ICollection<OfficialContact>, ObservableCollection<OfficialContactModel>>(
                        officialContactDtoCollection);
            }

            if (obj.PersonalContactsCollection != null || string.IsNullOrEmpty(obj.PersonalContacts))
            {
                return;
            }

            IList<PersonalContact> personalContactDtoCollecton = new List<PersonalContact>();

            try
            {
                personalContactDtoCollecton = this.restQuery.GetByLink<PersonalContact>(obj.PersonalContacts);
            }
            catch (RestClientException rex)
            {
                this.logger.Error("Exception from the RestClient", rex);
            }

            obj.PersonalContactsCollection =
                this.mapper.Map<ICollection<PersonalContact>, ObservableCollection<PersonalContactModel>>(
                    personalContactDtoCollecton);
        }
    }
}
