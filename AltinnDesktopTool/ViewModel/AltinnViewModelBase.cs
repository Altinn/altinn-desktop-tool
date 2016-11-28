using AltinnDesktopTool.Model;
using GalaSoft.MvvmLight;

namespace AltinnDesktopTool.ViewModel
{
    /// <summary>
    /// Base class for ViewModels
    /// </summary>
    public class AltinnViewModelBase : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the Model object of the ViewModel class
        /// </summary>
        public virtual ModelBase Model { get; set; }
    }
}
