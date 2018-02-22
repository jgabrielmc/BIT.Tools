using BIT.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace BIT.Tools.Html
{
    public static class HtmlHelpersExtensions
    {
        public static MvcHtmlString HelpFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return HelpHelper(html, ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData), ExpressionHelper.GetExpressionText(expression));
        }

        internal static MvcHtmlString HelpHelper(System.Web.Mvc.HtmlHelper html, ModelMetadata metadata, string htmlFieldName)
        {
            string str = metadata.Description ?? "";
            if (string.IsNullOrEmpty(str))
            {
                return MvcHtmlString.Empty;
            }
            TagBuilder builder = new TagBuilder("p");
            builder.Attributes.Add("class", "help-block");
            builder.SetInnerText(str);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }
        #region datetextboxfor
        public static MvcHtmlString DateTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return DateTextBoxFor(htmlHelper, expression, null);
        }

        public static MvcHtmlString DateTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            var htmlAttributesDict = new RouteValueDictionary(htmlAttributes);

            if (!htmlAttributesDict.ContainsKey("class"))
                htmlAttributesDict.Add("class", "");
            htmlAttributesDict["class"] += " datepicker";

            if (metadata.Model.ToDateTime() == DateTime.MinValue)
                htmlAttributesDict.Add("Value", "");

            return htmlHelper.TextBoxFor(expression, "{0:dd/MM/yyyy}", htmlAttributesDict);
        }
#endregion
        #region select2for
        public static MvcHtmlString Select2For<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, String Action, String Controller, object htmlAttributes)
        {
            return Select2For(htmlHelper, expression, Action, Controller, htmlAttributes, null);
        }
        public static MvcHtmlString Select2For<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, String Action, String Controller, object htmlAttributes, params String[] parameters)
        {
            var htmlAttributesDict = new RouteValueDictionary(htmlAttributes);
            if (!htmlAttributesDict.ContainsKey("class"))
                htmlAttributesDict.Add("class", "");

            htmlAttributesDict["data-type"] += "select2-ajax";
            htmlAttributesDict["data-action"] += Action;
            htmlAttributesDict["data-controller"] += Controller;


            if (parameters.Length >= 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    htmlAttributesDict["data-params-" + i] += parameters[i];

                }
            }
            return htmlHelper.HiddenFor(expression, htmlAttributesDict);
        }

        public static MvcHtmlString Select2For<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            var htmlAttributesDict = new RouteValueDictionary(htmlAttributes);
            if (!htmlAttributesDict.ContainsKey("class"))
                htmlAttributesDict.Add("class", "");

            htmlAttributesDict["class"] += " select2";

            return htmlHelper.DropDownListFor(expression, selectList, "[--Seleccione--]", htmlAttributesDict);
        }
        #endregion
        #region ModalLink
        public static IHtmlString ModalLink(String action, String onCloasScript = "")
        {
            return ModalLink(action, null, null, onCloasScript);
        }

        public static IHtmlString ModalLink(String action, String controller, String onCloasScript = "")
        {
            return ModalLink(action, controller, null, onCloasScript);
        }

        public static IHtmlString ModalLink(String action, object routeValues, String onCloasScript = "")
        {
            return ModalLink(action, null, routeValues, onCloasScript);
        }

        public static IHtmlString ModalLink(String action, String controller, object routeValues, String onCloasScript = "")
        {
            var result = "data-type=\"modal-link\"  data-source-url=\"" + dataWebViewPage.Url.Action(action, controller, routeValues) + "\"";

            if (!String.IsNullOrEmpty(onCloasScript))
                result += " data-on-close=\"" + onCloasScript + "\"";

            return new HtmlString(result);
        }
#endregion
        public static IHtmlString Tooltip(String title, String position = "top", Boolean esHtml = false)
        {
            var result = "data-toggle=\"tooltip\" data-placement=\"" + position + "\" title=\"" + title.Replace("\"", "'") + "\"" + (esHtml ? " data-html=\"true\"" : "");
            return new HtmlString(result);
        }

    }
}