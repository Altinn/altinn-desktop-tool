using AltinnDesktopTool.Model;
using AltinnDesktopTool.Utils.Helpers;
using AltinnDesktopTool.Utils.PubSub;
using AltinnDesktopTool.View;
using AutoMapper;
using Common.Logging;
using GalaSoft.MvvmLight.Command;
using RestClient;
using RestClient.DTO;
using RestClient.Resources;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AltinnDesktopTool.ViewModel
{
    public sealed class SearchRolesAndRightsInformationViewModel : AltinnViewModelBase
    {
        private readonly ILog logger;
        private readonly IMapper mapper;
        private IRestQuery query;

        /// <summary>
        /// Initializes a new instance of the SearchRolesAndRightsInformationViewModel class.
        /// </summary>
        /// <param name="logger">The logger to be used by the instance.</param>
        /// <param name="mapper">The AutoMapper instance to use by the view model.</param>
        /// <param name="query">The query proxy to use in the actual searches.</param>
        public SearchRolesAndRightsInformationViewModel(ILog logger, IMapper mapper, IRestQuery query)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.query = query;

            this.Model = new SearchRolesAndRightsInformationModel();
            this.SearchCommand = new RelayCommand<SearchRolesAndRightsInformationModel>(this.SearchCommandHandler);

            PubSub<ObservableCollection<RoleModel>>.AddEvent(EventNames.RoleSearchResultReceivedEvent, this.RoleSearchResultReceivedEventHandler);
            PubSub<bool>.AddEvent(EventNames.RoleSearchStartedEvent, this.RoleSearchStartedEventHandler);
            PubSub<string>.RegisterEvent(EventNames.EnvironmentChangedEvent, this.EnvironmentChangedEventHandler);
        }

        /// <summary>
        /// Occurs when a search has been started.
        /// </summary>
        public event PubSubEventHandler<bool> RoleSearchStartedEventHandler;

        ///// <summary>
        ///// Occurs when the search in complete.
        ///// </summary>
        public event PubSubEventHandler<ObservableCollection<RoleModel>> RoleSearchResultReceivedEventHandler;

        /// <summary>
        /// Gets or sets the command to run when search is triggered.
        /// </summary>
        public RelayCommand<SearchRolesAndRightsInformationModel> SearchCommand { get; set; }

        /// <summary>
        /// Gets or sets the model used by the search view.
        /// </summary>
        public new SearchRolesAndRightsInformationModel Model { get; set; }

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

        private async void SearchCommandHandler(SearchRolesAndRightsInformationModel obj)
        {
            this.logger.Debug(this.GetType().FullName + " Searching for subject " + obj.SubjectSearchText + " og reportee " + obj.ReporteeSearchText);

            obj.LabelText = string.Empty;
            obj.LabelBrush = Brushes.Green;

            // Removing all whitespaces from the search strings.
            string subjectSearchText = new string(obj.SubjectSearchText.Where(c => !char.IsWhiteSpace(c)).ToArray());
            string reporteeSearchText = new string(obj.ReporteeSearchText.Where(c => !char.IsWhiteSpace(c)).ToArray());

            if (string.IsNullOrEmpty(subjectSearchText) || string.IsNullOrEmpty(reporteeSearchText))
            {
                obj.LabelText = Resources.SearchLabelEmptySearch;
                obj.LabelBrush = Brushes.Red;

                // Preventing an empty search. It takes a lot of time and the result is useless. 
                return;
            }

            PubSub<bool>.RaiseEvent(EventNames.RoleSearchStartedEvent, this, new PubSubEventArgs<bool>(true));
            SearchType subjectSearchType = IdentifySearchType(subjectSearchText);
            SearchType reporteeSearchType = IdentifySearchType(reporteeSearchText);

            IList<Role> roles = new List<Role>();

            try
            {
                if (subjectSearchType == SearchType.Unknown || reporteeSearchType == SearchType.Unknown)
                {
                    obj.LabelBrush = Brushes.Red;
                    obj.LabelText = Resources.SearchLabelErrorInvalidInput;
                }
                else
                {
                    roles = await this.GetRoles(subjectSearchText, reporteeSearchText);
                }
            }
            catch (RestClientException rex)
            {
                this.logger.Error("Exception from the RestClient", rex);

                obj.LabelBrush = Brushes.Red;

                switch (rex.ErrorCode)
                {
                    case RestClientErrorCodes.RemoteApiReturnedStatusBadRequest:
                        obj.LabelText = Resources.SearchLabelErrorGeneralError;
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

            ObservableCollection<RoleModel> rolesmodellist = roles != null
                         ? this.mapper.Map<ICollection<Role>, ObservableCollection<RoleModel>>(roles)
                         : new ObservableCollection<RoleModel>();

            PubSub<ObservableCollection<RoleModel>>.RaiseEvent(
                EventNames.RoleSearchResultReceivedEvent, this, new PubSubEventArgs<ObservableCollection<RoleModel>>(rolesmodellist));
        }

        private static SearchType IdentifySearchType(string searchText)
        {
            if (searchText.Length == 9 && searchText.All(char.IsDigit))
            {
                return SearchType.OrganizationNumber;
            }
            else if(searchText.Length == 11 && searchText.All(char.IsDigit))
            {
                return SearchType.SSN;
            }
            else
            {
                return SearchType.Unknown;
            }
        }

        private async Task<IList<Role>> GetRoles(string subjectSearchText, string reporteeSearchText)
        {
            return await Task.Run(() => this.query.Get<Role>(
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Subject", subjectSearchText),
                    new KeyValuePair<string, string>("Reportee", reporteeSearchText)
                }));
        }
    }
}
