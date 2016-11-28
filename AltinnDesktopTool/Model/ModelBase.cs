***REMOVED***
using System.Collections;
***REMOVED***
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using GalaSoft.MvvmLight;

namespace AltinnDesktopTool.Model
***REMOVED***
***REMOVED***
    /// Base Class for Data Models used by Views and View Models.
***REMOVED***
    public class ModelBase : ObservableObject, INotifyDataErrorInfo
    ***REMOVED***
    ***REMOVED***
        /// Dictionary of ValidationErrors
    ***REMOVED***
        public readonly Dictionary<string, ICollection<string>> ValidationErrors = new Dictionary<string, ICollection<string>>();        

        private bool isBusy;

    ***REMOVED***
        /// Event handler for change of errors.
    ***REMOVED***
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    ***REMOVED***
        /// True if there are errors.
    ***REMOVED***
        public bool HasErrors => this.ValidationErrors.Count > 0;

    ***REMOVED***
        /// Gets or sets a value indicating whether there is a background operation running
    ***REMOVED***
        public bool IsBusy
        ***REMOVED***
            get
            ***REMOVED***
                return this.isBusy;
    ***REMOVED***

            set
            ***REMOVED***
                this.isBusy = value;
                this.RaisePropertyChanged(() => this.IsBusy);
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Get the error messages for a property
    ***REMOVED***
        /// <param name="propertyName">The property name</param>
        /// <returns>The error messages or null if not found</returns>
        public IEnumerable GetErrors(string propertyName)
        ***REMOVED***
            return string.IsNullOrEmpty(propertyName) || !this.ValidationErrors.ContainsKey(propertyName) ? null : this.ValidationErrors[propertyName];
***REMOVED***

    ***REMOVED***
        /// Validates a property of the model using the defined validators
    ***REMOVED***
        /// <param name="value">The property value</param>
        /// <param name="propertyName">The property name, which is a valid property of this model</param>
        protected void ValidateModelProperty(object value, string propertyName)
        ***REMOVED***
            if (this.ValidationErrors.ContainsKey(propertyName))
            ***REMOVED***
                this.ValidationErrors.Remove(propertyName);
    ***REMOVED***

            ICollection<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(this, null, null) ***REMOVED*** MemberName = propertyName ***REMOVED***;

            if (!Validator.TryValidateProperty(value, validationContext, validationResults))
            ***REMOVED***
                this.ValidationErrors.Add(propertyName, new List<string>());
                foreach (ValidationResult validationResult in validationResults)
                ***REMOVED***
                    this.ValidationErrors[propertyName].Add(validationResult.ErrorMessage);
        ***REMOVED***
    ***REMOVED***

            this.RaiseErrorsChanged(propertyName);
***REMOVED***

    ***REMOVED***
        /// Validates the model, meaning all the properties
    ***REMOVED***
        protected void ValidateModel()
        ***REMOVED***
            this.ValidationErrors.Clear();

            ICollection<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(this, null, null);

            if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
            ***REMOVED***
                foreach (ValidationResult validationResult in validationResults)
                ***REMOVED***
                    string property = validationResult.MemberNames.ElementAt(0);
                    if (this.ValidationErrors.ContainsKey(property))
                    ***REMOVED***
                        this.ValidationErrors[property].Add(validationResult.ErrorMessage);
            ***REMOVED***
                    else
                    ***REMOVED***
                        this.ValidationErrors.Add(property, new List<string> ***REMOVED*** validationResult.ErrorMessage ***REMOVED***);
            ***REMOVED***
        ***REMOVED***
    ***REMOVED***

            // Raise the ErrorsChanged for all properties explicitly
            this.RaiseErrorsChanged("Username");
            this.RaiseErrorsChanged("Name");
***REMOVED***

        private void RaiseErrorsChanged(string propertyName)
        ***REMOVED***
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
***REMOVED***
***REMOVED***
***REMOVED***