using Microsoft.AspNetCore.Razor.TagHelpers;

namespace prueba.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "ajax-link")]
    [HtmlTargetElement("form", Attributes = "ajax-link")]
    public class AjaxAction : TagHelper
    {
        [HtmlAttributeName("ajax-link")]
        public bool ajaxlink { get; set; }

        [HtmlAttributeName("ajax-update")]
        public string Update { get; set; }

        [HtmlAttributeName("ajax-method")]
        public string method { get; set; }

        [HtmlAttributeName("ajax-confirm")]
        public string confirm { get; set; }

        [HtmlAttributeName("ajax-success")]
        public string success { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ajaxlink)
            {

                output.Attributes.SetAttribute("data-ajax", "true");
                output.Attributes.SetAttribute("data-ajax-loading", ".E-loading");
                output.Attributes.SetAttribute("data-ajax-update", Update);
                if (!string.IsNullOrEmpty(method)) output.Attributes.SetAttribute("data-ajax-method", method);
                if (!string.IsNullOrEmpty(confirm)) output.Attributes.SetAttribute("data-ajax-confirm", confirm);
                if (!string.IsNullOrEmpty(success)) output.Attributes.SetAttribute("data-ajax-success", success);


            }


            base.Process(context, output);
        }
    }
}
