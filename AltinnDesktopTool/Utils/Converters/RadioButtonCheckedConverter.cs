using System;
using System.Globalization;
using System.Windows.Data;

namespace AltinnDesktopTool.Utils.Converters
{
    /// <summary>
    /// This is a <see cref="IValueConverter"/> class that can convert an enum value to a string or boolean and back.
    /// The intended use is related to binding of an enum to a set of radio buttons and their checked property.
    /// </summary>
    public class RadioButtonCheckedConverter : IValueConverter
    {
        /// <summary>
        /// Convert from an enum to a boolean or string.
        /// </summary>
        /// <param name="value">The value to be converted.</param>
        /// <param name="targetType">The type to convert to. Limited to boolean and string.</param>
        /// <param name="parameter">The parameter value to compare with. Used if target type is a boolean, not for string.</param>
        /// <param name="culture">Input is not used. String compare is using <see cref="StringComparison.InvariantCultureIgnoreCase"/> instead.</param>
        /// <returns>A boolean or string with with the input value converted.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!targetType.IsAssignableFrom(typeof(bool)) && !targetType.IsAssignableFrom(typeof(string)))
            {
                throw new ArgumentException("RadioButtonCheckedConverter can only convert to boolean or string.");
            }

            if (targetType == typeof(string))
            {
                return value.ToString();
            }

            return string.Compare(value.ToString(), (string)parameter, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        /// <summary>
        /// Convert a string or boolean back into an enum.
        /// </summary>
        /// <param name="value">The value to convert into an enum.</param>
        /// <param name="targetType">The enum type to convert into.</param>
        /// <param name="parameter">The value to compare with if input type is a boolean. Used to determine if value should be true or false.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>The value converted into an enum of the given type.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!targetType.IsAssignableFrom(typeof(bool)) && !targetType.IsAssignableFrom(typeof(string)))
            {
                throw new ArgumentException("RadioButtonCheckedConverter can only convert back value from a string or a boolean.");
            }

            if (!targetType.IsEnum)
            {
                throw new ArgumentException("RadioButtonCheckedConverter can only convert value to an Enum Type.");
            }

            string s = value as string;
            if (s != null)
            {
                return Enum.Parse(targetType, s, true);
            }

            // We have a boolean, as for binding to a checkbox. we use parameter
            if ((bool)value)
            {
                return Enum.Parse(targetType, (string)parameter, true);
            }

            return Binding.DoNothing;
        }
    }
}
