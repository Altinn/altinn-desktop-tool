using AltinnDesktopTool.Utils.PubSub;

namespace AltinnDesktopTool.Model
{
    public class RoleModel : ModelBase
    {
        private bool isSelected;

        /// <summary>
        /// Gets or sets the role type
        /// </summary>
        public string RoleType { get; set; }

        /// <summary>
        /// Gets or sets the role definition id
        /// </summary>
        public string RoleDefinitionId { get; set; }

        /// <summary>
        /// Gets or sets the role name
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the role description
        /// </summary>
        public string RoleDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an item is selected
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.isSelected = value;
                this.RaisePropertyChanged(() => this.IsSelected);
                PubSub<RoleModel>.RaiseEvent(EventNames.RoleSelectedChangedEvent, this, new PubSubEventArgs<RoleModel>(this));
            }
        }

        /// <summary>
        /// Sets IsSelected
        /// </summary>
        /// <param name="value">boolean value</param>
        public void SetIsSelected(bool value)
        {
            this.isSelected = value;
            this.RaisePropertyChanged(() => this.IsSelected);
        }


    }
}
