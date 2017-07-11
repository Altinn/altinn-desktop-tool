using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using GalaSoft.MvvmLight;

namespace AltinnDesktopTool.Model
{
    /// <summary>
    /// Base Class for Data Models used by Views and View Models.
    /// </summary>
    public class ModelBase : ObservableObject, INotifyDataErrorInfo
    {
        /// <summary>
        /// Dictionary of ValidationErrors
        /// </summary>
        public readonly Dictionary<string, ICollection<string>> ValidationErrors = new Dictionary<string, ICollection<string>>();        

        private bool isBusy;

        /// <summary>
        /// Event handler for change of errors.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// True if there are errors.
        /// </summary>
        public bool HasErrors => this.ValidationErrors.Count > 0;

        /// <summary>
        /// Gets or sets a value indicating whether there is a background operation running
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            set
            {
                this.isBusy = value;
                this.RaisePropertyChanged(() => this.IsBusy);
            }
        }

        /// <summary>
        /// Get the error messages for a property
        /// </summary>
        /// <param name="propertyName">The property name</param>
        /// <returns>The error messages or null if not found</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            return string.IsNullOrEmpty(propertyName) || !this.ValidationErrors.ContainsKey(propertyName) ? null : this.ValidationErrors[propertyName];
        }

        /// <summary>
        /// Validates a property of the model using the defined validators
        /// </summary>
        /// <param name="value">The property value</param>
        /// <param name="propertyName">The property name, which is a valid property of this model</param>
        protected void ValidateModelProperty(object value, string propertyName)
        {
            if (this.ValidationErrors.ContainsKey(propertyName))
            {
                this.ValidationErrors.Remove(propertyName);
            }

            ICollection<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(this, null, null) { MemberName = propertyName };

            if (!Validator.TryValidateProperty(value, validationContext, validationResults))
            {
                this.ValidationErrors.Add(propertyName, new List<string>());
                foreach (ValidationResult validationResult in validationResults)
                {
                    this.ValidationErrors[propertyName].Add(validationResult.ErrorMessage);
                }
            }

            this.RaiseErrorsChanged(propertyName);
        }

        /// <summary>
        /// Validates the model, meaning all the properties
        /// </summary>
        protected void ValidateModel()
        {
            this.ValidationErrors.Clear();

            ICollection<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(this, null, null);

            if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    string property = validationResult.MemberNames.ElementAt(0);
                    if (this.ValidationErrors.ContainsKey(property))
                    {
                        this.ValidationErrors[property].Add(validationResult.ErrorMessage);
                    }
                    else
                    {
                        this.ValidationErrors.Add(property, new List<string> { validationResult.ErrorMessage });
                    }
                }
            }

            // Raise the ErrorsChanged for all properties explicitly
            this.RaiseErrorsChanged("Username");
            this.RaiseErrorsChanged("Name");
        }

        private void RaiseErrorsChanged(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}