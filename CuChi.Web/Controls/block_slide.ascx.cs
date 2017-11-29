using Cb.BLL;
using Cb.BLL.Product;
using Cb.DBUtility;
using Cb.Localization;
using Cb.Model;
using Cb.Model.Products;
using Cb.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Cb.Web.Controls
{
    public partial class block_slide : DGCUserControl
    {
        #region Parameter

        protected string title, metaDescription, metaKeyword, h1, h2, h3, analytic;
        int total;

        #endregion

        #region Common

        private void InitPage()
        {
            GetSlogan();
            GetBanner();
        }

        private void GetBanner()
        {
            BannerBLL bannerBLL = new BannerBLL();
            IList<PNK_Banner> lst = bannerBLL.GetList(DBConvert.ParseInt(ConfigurationManager.AppSettings["slideId"]), string.Empty, "1", 1, 100, out total);
            if (total > 0)
            {
                this.rptResult.DataSource = lst;
                this.rptResult.DataBind();
            }
        }

        private void GetSlogan()
        {
            ContentStaticBLL pcBll = new ContentStaticBLL();
            IList<PNK_ContentStatic> lst = pcBll.GetList(LangInt, string.Empty, ConfigurationManager.AppSettings["contentStatic_Slogan"], string.Empty, 1, 10000, out total);
            if (total > 0)
            {
                ltrSlogan.Text = lst[0].ContentStaticDesc.Brief;
            }
        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitPage();
            }
        }

        protected void rptResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PNK_Banner data = e.Item.DataItem as PNK_Banner;

                HtmlImage img = e.Item.FindControl("img") as HtmlImage;
                string src = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["SliderUpload"], data.Image);
                img.Src = src;
                img.Attributes.Add("data-src", src);

                Literal ltrName = e.Item.FindControl("ltrName") as Literal;
                ltrName.Text = img.Alt = data.Name;

                Literal ltrDetail = e.Item.FindControl("ltrDetail") as Literal;
                ltrDetail.Text = data.Detail;



                HtmlAnchor hypLink = e.Item.FindControl("hypLink") as HtmlAnchor;
                hypLink.HRef = data.LinkUrl;

                //Literal ltrLink = e.Item.FindControl("ltrLink") as Literal;
                //ltrLink.Text = LocalizationUtility.GetText("ltrLink", Ci);
            }
        }

        #endregion
    }
}