using AltinnDesktopTool.Model;
using AltinnDesktopTool.Utils.Helpers;
using AltinnDesktopTool.Utils.PubSub;
using AutoMapper;
using GalaSoft.MvvmLight.Command;
using log4net;
using RestClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AltinnDesktopTool.ViewModel
{
    /// <summary>
    /// ViewModel for RolesSearchResult view
    /// </summary>
    public sealed class RolesSearchResultViewModel : AltinnViewModelBase
    {
        private readonly ILog logger;
        private readonly IMapper mapper;
        private IRestQuery restQuery;

        /// <summary>
        /// Initializes a new instance of the RolesSearchResultViewModel class.
        /// </summary>
        /// <param name="logger">The logger to be used by the instance.</param>
        /// <param name="mapper">The AutoMapper instance to use by the view model.</param>
        /// <param name="restQuery">The query proxy to use in the actual context.</param>
        public RolesSearchResultViewModel(ILog logger, IMapper mapper, IRestQuery restQuery)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.restQuery = restQuery;

            this.Model = new RolesSearchResultModel { ResultCollection = new ObservableCollection<RoleModel>() };
            this.Model.RolesSelectAllChecked = false;

            PubSub<ObservableCollection<RoleModel>>.RegisterEvent(EventNames.RoleSearchResultReceivedEvent, this.RoleSearchResultReceivedEventHandler);
            PubSub<bool>.RegisterEvent(EventNames.RoleSearchStartedEvent, this.RoleSearchStartedEventHandler);
            PubSub<string>.RegisterEvent(EventNames.EnvironmentChangedEvent, this.EnvironmentChangedEventHandler);
            PubSub<RoleModel>.RegisterEvent(EventNames.RoleSelectedChangedEvent, this.RoleSelectedChangedEventHandler);
            PubSub<RolesSearchResultModel>.RegisterEvent(EventNames.RoleSelectedAllChangedEvent, this.RoleSelectedAllChangedEventHandler);

            this.CopyToClipboardPlainTextCommand = new RelayCommand(this.CopyRolesToClipboardPlainTextHandler);
            this.CopyToClipboardExcelFormatCommand = new RelayCommand(this.CopyRolesToClipboardExcelFormatHandler);
        }

        /// <summary>
        /// Gets or sets the RolesSearchResult model
        /// </summary>
        public new RolesSearchResultModel Model { get; set; }

        /// <summary>
        /// Gets the CopyToClipboardPlainText command
        /// </summary>
        public ICommand CopyToClipboardPlainTextCommand { get; private set; }

        /// <summary>
        /// Gets the CopyToClipboardExcelFormat command
        /// </summary>
        public ICommand CopyToClipboardExcelFormatCommand { get; private set; }

        /// <summary>
        /// Handler for RoleSearchResultReceived event
        /// </summary>        
        /// <param name="sender">Sender object - not used in this context</param>
        /// <param name="args">Result collection</param>
        public void RoleSearchResultReceivedEventHandler(object sender, PubSubEventArgs<ObservableCollection<RoleModel>> args)
        {
            this.logger.Debug("Handling search result received event.");
            this.Model.ResultCollection = args.Item;
            this.Model.RolesSelectAllChecked = false;
            this.Model.IsBusy = false;
        }

        /// <summary>
        /// Copy to clipboard in plain text logic
        /// </summary>
        public void CopyRolesToClipboardPlainTextHandler()
        {
            StringBuilder stringBuilder = new StringBuilder();
            IEnumerable<RoleModel> roles = this.Model.ResultCollection.Where(x => x.IsSelected);

            foreach (RoleModel roleModel in roles)
            {
                stringBuilder.Append("RoleDefinitionId: " + roleModel.RoleDefinitionId + Environment.NewLine);
                stringBuilder.Append("RoleName: " + roleModel.RoleName + Environment.NewLine);
                stringBuilder.Append("RoleType: " + roleModel.RoleType + Environment.NewLine);
                stringBuilder.Append("RoleDescription: " + roleModel.RoleDescription + Environment.NewLine);
                stringBuilder.Append(Environment.NewLine);
            }

            Clipboard.SetText(stringBuilder.ToString());
        }

        /// <summary>
        /// Copy to clipboard in excel format logic
        /// </summary>
        public void CopyRolesToClipboardExcelFormatHandler()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string separator = "\t";
            IEnumerable<RoleModel> roles = this.Model.ResultCollection.Where(x => x.IsSelected);

            foreach (RoleModel roleModel in roles)
            {
                stringBuilder.Append(roleModel.RoleDefinitionId + separator + roleModel.RoleName + separator + roleModel.RoleType + separator + roleModel.RoleDescription + Environment.NewLine);
            }

            Clipboard.SetText(stringBuilder.ToString());
        }

        private void RoleSearchStartedEventHandler(object sender, PubSubEventArgs<bool> e)
        {
            this.Model.IsBusy = true;
            this.Model.EmptyMessageVisibility = false;
        }

        private void EnvironmentChangedEventHandler(object sender, PubSubEventArgs<string> e)
        {
            this.logger.Debug("Handling environment changed received event.");
            this.restQuery = new RestQuery(ProxyConfigHelper.GetConfig(e.Item), this.logger);
            this.Model.ResultCollection = new ObservableCollection<RoleModel>();
            this.Model.EmptyMessageVisibility = false;
            this.Model.RolesSelectAllChecked = false;
        }

        private void RoleSelectedAllChangedEventHandler(object sender, PubSubEventArgs<RolesSearchResultModel> e)
        {
            int selectedCount = this.Model.ResultCollection.Select(x => x.IsSelected).Count();
            if (selectedCount != this.Model.ResultCollection.Count)
            {
                return;
            }
            //// Set all items to the same selected value 
            foreach (RoleModel roleModel in this.Model.ResultCollection)
            {
                roleModel.SetIsSelected(this.Model.RolesSelectAllChecked);
            }
        }

        private void RoleSelectedChangedEventHandler(object sender, PubSubEventArgs<RoleModel> e)
        {
            int selectedCount = this.Model.ResultCollection.Select(x => x.IsSelected).Count();
            if ((selectedCount != this.Model.ResultCollection.Count) || !e.Item.IsSelected)
            {
                this.Model.SetSelectAllChecked(false);
            }
        }

    }
}
