using System;
using Xamarin.Forms;

namespace TailBlazerMobile.Portable.Utils
{
    public class TemplateRule
    {
        private Type propertyType;
        private DataTemplate dataTemplate;

        public Type PropertyType
        {
            get
            {
                return this.propertyType;
            }
            set
            {
                this.propertyType = value;
            }
        }

        public DataTemplate DataTemplate
        {
            get
            {
                return this.dataTemplate;
            }
            set
            {
                this.dataTemplate = value;
            }
        }
    }
}
