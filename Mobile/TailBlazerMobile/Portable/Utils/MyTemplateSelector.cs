using System.Collections.Generic;
using Xamarin.Forms;

namespace TailBlazerMobile.Portable.Utils
{
    public class MyTemplateSelector : DataTemplateSelector
    {
        List<TemplateRule> _templateRules;

        public List<TemplateRule> TemplateRules
        {
            get
            {
                if (this._templateRules == null)
                {
                    this._templateRules = new List<TemplateRule>();
                }

                return this._templateRules;
            }
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item != null)
            {
                foreach (TemplateRule rule in this.TemplateRules)
                {
                    // Select the appropriate template for each property.
                    if (rule.PropertyType.IsAssignableFrom(item.GetType()))
                    {
                        return rule.DataTemplate;
                    }
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
