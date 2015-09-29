using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingWeb.CustomHtmlHelper
{
    public static class HtmlImageHelper
    {
        public static IHtmlString Image(this HtmlHelper helper, string src, string style="",string alt="")
        {
            // Build <img> tag
            TagBuilder tb = new TagBuilder("img");
            // Add "src" attribute
            tb.Attributes.Add("src", VirtualPathUtility.ToAbsolute("~/Image/" + src));
            // Add "alt" attribute
            tb.Attributes.Add("alt", alt);
            // return MvcHtmlString. This class implements IHtmlString
            // interface. IHtmlStrings will not be html encoded.
            tb.Attributes.Add("style", style);
            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
        }
    }
}