using System.Web.Mvc;

namespace BIT.Tools.Html
{
    public class AppWebViewPage<TModel> : WebViewPage<TModel>
    {
        public DataHelper<TModel> Data { get; set; }

        public override void InitHelpers()
        {
            base.InitHelpers();
            Data = new DataHelper<TModel>(this);
        }

        public override void Execute()
        {
        }
    }
}
