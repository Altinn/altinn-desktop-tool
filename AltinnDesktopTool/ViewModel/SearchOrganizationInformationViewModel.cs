using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

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
    /// ViewModel for SearchOrganizationInformation view
    /// </summary>    
    public sealed class SearchOrganizationInformationViewModel : AltinnViewModelBase
    {
        private readonly ILog logger;
        private readonly IMapper mapper;
        private IRestQuery query;

        /// <summary>
        /// Initializes a new instance of the SearchOrganizationInformationViewModel class.
        /// </summary>
        /// <param name="logger">The logger to be used by the instance.</param>
        /// <param name="mapper">The AutoMapper instance to use by the view model.</param>
        /// <param name="query">The query proxy to use in the actual searches.</param>
        public SearchOrganizationInformationViewModel(ILog logger, IMapper mapper, IRestQuery query)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.query = query;

            this.Model = new SearchOrganizationInformationModel();
            this.SearchCommand = new RelayCommand<SearchOrganizationInformationModel>(this.SearchCommandHandler);

            PubSub<ObservableCollection<OrganizationModel>>.AddEvent(EventNames.SearchResultReceivedEvent, this.SearchResultReceivedEventHandler);
            PubSub<bool>.AddEvent(EventNames.SearchStartedEvent, this.SearchStartedEventHandler);
            PubSub<string>.RegisterEvent(EventNames.EnvironmentChangedEvent, this.EnvironmentChangedEventHandler);
        }

        /// <summary>
        /// Occurs when a search has been started.
        /// </summary>
        public event PubSubEventHandler<bool> SearchStartedEventHandler;

        /// <summary>
        /// Occurs when the search in complete.
        /// </summary>
        public event PubSubEventHandler<ObservableCollection<OrganizationModel>> SearchResultReceivedEventHandler;

        /// <summary>
        /// Gets or sets the command to run when search is triggered.
        /// </summary>
        public RelayCommand<SearchOrganizationInformationModel> SearchCommand { get; set; }

        /// <summary>
        /// Gets or sets the model used by the search view.
        /// </summary>
        public new SearchOrganizationInformationModel Model { get; set; }

        /// <summary>
        /// Event handler for when the environment has changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">An object that contains the name of the new configuration.</param>
        public void EnvironmentChangedEventHandler(object sender, PubSubEventArgs<string> args)
        {
            this.logger.Debug("Handling environment changed received event.");
            this.query = new RestQuery(ProxyConfigHelper.GetConfig(args.Item), this.logger);

            this.SearchCommand.Execute(this.Model);
        }

        private static SearchType IdentifySearchType(string searchText)
        {
            if (searchText.IndexOf("@", StringComparison.InvariantCulture) > 0)
            {
                return SearchType.EMail;
            }

            if (searchText.Length == 9 && searchText.All(char.IsDigit))
            {
                return SearchType.OrganizationNumber;
            }

            return SearchType.PhoneNumber;
        }

        private async void SearchCommandHandler(SearchOrganizationInformationModel obj)
        {
            this.logger.Debug(this.GetType().FullName + " Searching for: " + obj.SearchText + ", " + obj.SearchType);

            obj.LabelText = string.Empty;
            obj.LabelBrush = Brushes.Green;

            // Removing all whitespaces from the search string.
            string searchText = new string(obj.SearchText.Where(c => !char.IsWhiteSpace(c)).ToArray());

            if (string.IsNullOrEmpty(searchText))
            {
                obj.LabelText = Resources.SearchLabelEmptySearch;
                obj.LabelBrush = Brushes.Red;

                // Preventing an empty search. It takes a lot of time and the result is useless. 
                return;
            }

            PubSub<bool>.RaiseEvent(EventNames.SearchStartedEvent, this, new PubSubEventArgs<bool>(true));

            // After having removed the radio buttons where the user could select search type, search is always Smart, but the check
            // is kept in case the radio buttons comes back in a future release. For example as advanced search.
            SearchType searchType = obj.SearchType == SearchType.Smart ? IdentifySearchType(searchText) : obj.SearchType;

            IList<Organization> organizations = new List<Organization>();

            try
            {
                switch (searchType)
                {
                    case SearchType.EMail:
                        obj.LabelText = string.Format(Resources.SearchLabelResultat, Resources.EMail + " " + searchText);
                        organizations = await this.GetOrganizations(searchType, searchText);
                        break;
                    case SearchType.PhoneNumber:
                        obj.LabelText = string.Format(Resources.SearchLabelResultat, Resources.PhoneNumber + " " + searchText);
                        organizations = await this.GetOrganizations(searchType, searchText);
                        break;
                    case SearchType.OrganizationNumber:
                        obj.LabelText = string.Format(Resources.SearchLabelResultat, Resources.OrganizationNumber + " " + searchText);
                        Organization organization = await this.GetOrganization(searchText);
                        organizations.Add(organization);
                        break;
                    case SearchType.Smart:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (RestClientException rex)
            {
                this.logger.Error("Exception from the RestClient", rex);

                obj.LabelBrush = Brushes.Red;

                switch (rex.ErrorCode)
                {
                    case RestClientErrorCodes.RemoteApiReturnedStatusBadRequest:
                        obj.LabelText = searchType == SearchType.OrganizationNumber
                            ? Resources.SearchLabelErrorOrganizationNotFound
                            : Resources.SearchLabelErrorGeneralError;
                        break;
                    case RestClientErrorCodes.RemoteApiReturnedStatusUnauthorized:
                        obj.LabelText = Resources.SearchLabelErrorUnauthorized;
                        break;
                    case RestClientErrorCodes.RemoteApiReturnedStatusForbidden:
                        obj.LabelText = Resources.SearchLabelErrorForbidden;
                        break;
                    case RestClientErrorCodes.RemoteApiReturnedStatusNotFound:
                        obj.LabelText = Resources.SearchLabelErrorOrganizationNotFound;
                        break;
                    default:
                        obj.LabelText = Resources.SearchLabelErrorGeneralError;
                        break;
                }
            }

            ObservableCollection<OrganizationModel> orgmodellist = organizations != null
                         ? this.mapper.Map<ICollection<Organization>, ObservableCollection<OrganizationModel>>(organizations)
                         : new ObservableCollection<OrganizationModel>();

            PubSub<ObservableCollection<OrganizationModel>>.RaiseEvent(
                EventNames.SearchResultReceivedEvent, this, new PubSubEventArgs<ObservableCollection<OrganizationModel>>(orgmodellist));
        }

        private async Task<Organization> GetOrganization(string orgnb)
        {
            return await Task.Run(() => this.query.Get<Organization>(orgnb));
        }

        private async Task<IList<Organization>> GetOrganizations(SearchType type, string searchText)
        {
            return await Task.Run(() => this.query.Get<Organization>(new KeyValuePair<string, string>(type.ToString(), searchText)));
        }
    }
}
