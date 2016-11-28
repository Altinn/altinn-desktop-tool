using System.Collections.ObjectModel;

using AltinnDesktopTool.Utils.PubSub;

namespace AltinnDesktopTool.Model
{
    /// <summary>
    /// Search Result Model
    /// </summary>
    public class SearchResultModel : ModelBase
    {
        private ObservableCollection<OrganizationModel> resultCollection;

        private bool emptyMessageVisibility;

        private string infoText = string.Empty;
        private bool selectAllChecked;

        /// <summary>
        /// Gets or sets a value indicating whether the Info text is visible in the result grid
        /// </summary>
        public bool EmptyMessageVisibility
        {
            get
            {
                return this.emptyMessageVisibility;
            }

            set
            {
                this.emptyMessageVisibility = value;
                this.RaisePropertyChanged(() => this.EmptyMessageVisibility);
                this.InfoText = value ? View.Resources.NoDataText : string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the Info text shown in the result grid
        /// </summary>
        public string InfoText
        {
            get
            {
                return this.infoText;
            }

            set
            {
                this.infoText = value;
                this.RaisePropertyChanged(() => this.InfoText);
            }
        }

        /// <summary>
        /// Gets or sets the Result collection presented in the Organization grid
        /// </summary>
        public ObservableCollection<OrganizationModel> ResultCollection
        {
            get
            {
                return this.resultCollection;
            }

            set
            {
                this.resultCollection = value;
                this.RaisePropertyChanged(() => this.ResultCollection);
                this.EmptyMessageVisibility = (this.resultCollection == null) || (this.resultCollection.Count == 0);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether all items are checked or not
        /// </summary>
        public bool SelectAllChecked
        {
            get
            {
                return this.selectAllChecked;
            }

            set
            {
                this.selectAllChecked = value;
                this.RaisePropertyChanged(() => this.SelectAllChecked);
                PubSub<SearchResultModel>.RaiseEvent(EventNames.OrganizationSelectedAllChangedEvent, this, new PubSubEventArgs<SearchResultModel>(this));
            }
        }

        /// <summary>
        /// Sets SelectAllChecked
        /// </summary>
        /// <param name="value">boolean value</param>
        public void SetSelectAllChecked(bool value)
        {
            this.selectAllChecked = false;
            this.RaisePropertyChanged(() => this.SelectAllChecked);
        }
    }
}