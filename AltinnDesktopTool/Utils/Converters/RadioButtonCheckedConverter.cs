***REMOVED***
using System.Globalization;
using System.Windows.Data;

namespace AltinnDesktopTool.Utils.Converters
***REMOVED***
***REMOVED***
    /// This is a <see cref="IValueConverter"/> class that can convert an enum value to a string or boolean and back.
    /// The intended use is related to binding of an enum to a set of radio buttons and their checked property.
***REMOVED***
    public class RadioButtonCheckedConverter : IValueConverter
    ***REMOVED***
    ***REMOVED***
        /// Convert from an enum to a boolean or string.
    ***REMOVED***
        /// <param name="value">The value to be converted.</param>
        /// <param name="targetType">The type to convert to. Limited to boolean and string.</param>
        /// <param name="parameter">The parameter value to compare with. Used if target type is a boolean, not for string.</param>
        /// <param name="culture">Input is not used. String compare is using <see cref="StringComparison.InvariantCultureIgnoreCase"/> instead.</param>
        /// <returns>A boolean or string with with the input value converted.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        ***REMOVED***
            if (!targetType.IsAssignableFrom(typeof(bool)) && !targetType.IsAssignableFrom(typeof(string)))
            ***REMOVED***
                throw new ArgumentException("RadioButtonCheckedConverter can only convert to boolean or string.");
    ***REMOVED***

            if (targetType == typeof(string))
            ***REMOVED***
                return value.ToString();
    ***REMOVED***

            return string.Compare(value.ToString(), (string)parameter, StringComparison.InvariantCultureIgnoreCase) == 0;
***REMOVED***

    ***REMOVED***
        /// Convert a string or boolean back into an enum.
    ***REMOVED***
        /// <param name="value">The value to convert into an enum.</param>
        /// <param name="targetType">The enum type to convert into.</param>
        /// <param name="parameter">The value to compare with if input type is a boolean. Used to determine if value should be true or false.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>The value converted into an enum of the given type.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        ***REMOVED***
            if (!targetType.IsAssignableFrom(typeof(bool)) && !targetType.IsAssignableFrom(typeof(string)))
            ***REMOVED***
                throw new ArgumentException("RadioButtonCheckedConverter can only convert back value from a string or a boolean.");
    ***REMOVED***

            if (!targetType.IsEnum)
            ***REMOVED***
                throw new ArgumentException("RadioButtonCheckedConverter can only convert value to an Enum Type.");
    ***REMOVED***

            string s = value as string;
            if (s != null)
            ***REMOVED***
                return Enum.Parse(targetType, s, true);
    ***REMOVED***

            // We have a boolean, as for binding to a checkbox. we use parameter
            if ((bool)value)
            ***REMOVED***
                return Enum.Parse(targetType, (string)parameter, true);
    ***REMOVED***

            return Binding.DoNothing;
***REMOVED***
***REMOVED***
***REMOVED***
