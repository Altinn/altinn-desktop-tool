using AltinnDesktopTool.Utils.PubSub;
using System.Collections.ObjectModel;

namespace AltinnDesktopTool.Model
{
    /// <summary>
    /// Roles Search Result Model
    /// </summary>
    public class RolesSearchResultModel : ModelBase
    {
        private ObservableCollection<RoleModel> resultCollection;

        private bool emptyMessageVisibility;

        private string infoText = string.Empty;
        private bool rolesSelectAllChecked;

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
        /// Gets or sets the Result collection presented in the role grid
        /// </summary>
        public ObservableCollection<RoleModel> ResultCollection
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
        public bool RolesSelectAllChecked
        {
            get
            {
                return this.rolesSelectAllChecked;
            }

            set
            {
                this.rolesSelectAllChecked = value;
                this.RaisePropertyChanged(() => this.RolesSelectAllChecked);
                PubSub<RolesSearchResultModel>.RaiseEvent(EventNames.RoleSelectedAllChangedEvent, this, new PubSubEventArgs<RolesSearchResultModel>(this));
            }
        }

        /// <summary>
        /// Sets RolesSelectAllChecked
        /// </summary>
        /// <param name="value">boolean value</param>
        public void SetSelectAllChecked(bool value)
        {
            this.rolesSelectAllChecked = false;
            this.RaisePropertyChanged(() => this.RolesSelectAllChecked);
        }
    }
}
