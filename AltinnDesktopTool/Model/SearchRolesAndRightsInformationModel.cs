using System.Windows.Media;

namespace AltinnDesktopTool.Model
{
    public class SearchRolesAndRightsInformationModel : ModelBase
    {
        private string subjectSearchText;
        private string reporteeSearchText;
        private string labelText;
        private Brush labelBrush;

        /// <summary>
        /// Gets or sets the text to be displayed as feedback from the search.
        /// </summary>
        public string LabelText
        {
            get
            {
                return this.labelText;
            }

            set
            {
                this.labelText = value;
                this.RaisePropertyChanged(() => this.LabelText);
            }
        }

        /// <summary>
        /// Gets or sets the brush used to define the look of the label foreground color.
        /// </summary>
        public Brush LabelBrush
        {
            get
            {
                return this.labelBrush;
            }

            set
            {
                this.labelBrush = value;
                this.RaisePropertyChanged(() => this.LabelBrush);
            }
        }

        /// <summary>
        /// Gets or sets the subject search text.
        /// </summary>
        public string SubjectSearchText
        {
            get
            {
                return this.subjectSearchText;
            }

            set
            {
                this.subjectSearchText = value;
                this.RaisePropertyChanged(() => this.SubjectSearchText);
            }
        }

        /// <summary>
        /// Gets or sets the reportee search text.
        /// </summary>
        public string ReporteeSearchText
        {
            get
            {
                return this.reporteeSearchText;
            }

            set
            {
                this.reporteeSearchText = value;
                this.RaisePropertyChanged(() => this.ReporteeSearchText);
            }
        }
    }
}
