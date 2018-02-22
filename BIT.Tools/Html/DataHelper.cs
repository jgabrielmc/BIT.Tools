using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;

namespace BIT.Tools.Html
{
    public class DataHelper<TModel>
    {
        private AppWebViewPage<TModel> dataWebViewPage { get; set; }

        public DataHelper(AppWebViewPage<TModel> dataWebViewPage)
        {
            if (dataWebViewPage == null)
            {
                throw new ArgumentNullException("dataWebViewPage");
            }
            this.dataWebViewPage = dataWebViewPage;
        }

        #region AjaxLink
        public MvcHtmlString AjaxLink(String actionName, AjaxOptions ajaxOptions)
        {
            return AjaxLink(actionName, null, null, ajaxOptions, null);
        }

        public MvcHtmlString AjaxLink(String actionName, object routeValues, AjaxOptions ajaxOptions)
        {
            return AjaxLink(actionName, null, routeValues, ajaxOptions, null);
        }

        public MvcHtmlString AjaxLink(String actionName, String controllerName, AjaxOptions ajaxOptions)
        {
            return AjaxLink(actionName, controllerName, null, ajaxOptions, null);
        }

        public MvcHtmlString AjaxLink(String actionName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            return AjaxLink(actionName, null, routeValues, ajaxOptions, htmlAttributes);
        }

        public MvcHtmlString AjaxLink(String actionName, String controllerName, object routeValues, AjaxOptions ajaxOptions)
        {
            return AjaxLink(actionName, controllerName, routeValues, ajaxOptions, null);
        }

        public MvcHtmlString AjaxLink(String actionName, String controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var actionLink = dataWebViewPage.Ajax.ActionLink("#", actionName, controllerName, routeValues, ajaxOptions, htmlAttributes).ToString();
            actionLink = actionLink.Replace("data-ajax=\"true\"", "data-ajax-link=\"true\"");
            return MvcHtmlString.Create(actionLink.Substring(3, actionLink.Length - 9));
        }

        #endregion
        /// <summary>
        /// Si el Request contiene el parámetro UpdateTargetId, entonces genera los atributos necesarion por el Unobstrusive Ajax:
        /// data-ajax="true="
        /// data-update-id="Valor del UpdateTargetId"
        /// </summary>
        /// <returns></returns>
        public MvcHtmlString CreateAutoTargetAjaxAttributes()
        {
            var updateTargetId = dataWebViewPage.Request.Params["UpdateTargetId"];
            
            if (dataWebViewPage.Request.IsAjaxRequest() && !String.IsNullOrEmpty(updateTargetId))
            {
                var actionLink = dataWebViewPage.Ajax.ActionLink("#", "AAA", "CCC", null, new AjaxOptions() { UpdateTargetId = updateTargetId }).ToString();
                actionLink = actionLink.Replace("href=\"/CCC/AAA\"", "");
                return MvcHtmlString.Create(actionLink.Substring(3, actionLink.Length - 9));
            }
            return MvcHtmlString.Empty;
        }

        /*
         * data-type="modal-link" data-source-url="@Url.Action("EditAutor", new { ExperienciaExitosaId = Model.ExperienciaExitosaId, AutorId = item.AutorId})"
         *  new  data-type="modal-link" data-source-url="@Url.Action("EditAutor", new { ExperienciaExitosaId = Model.ExperienciaExitosaId, AutorId = item.AutorId})"
         */

       
    }
}
