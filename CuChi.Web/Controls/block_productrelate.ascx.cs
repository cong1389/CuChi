using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Utility;
using Cb.Model;
using System.Web.UI.HtmlControls;
using Cb.BLL.Product;
using Cb.Model.Products;
using Cb.DBUtility;
using System.Configuration;
using Cb.BLL;
using Cb.Localization;
using Cb.Web.Common;
using System.Data;

namespace Cb.Web.Controls
{
    public partial class block_productrelate : DGCUserControl
    {
        #region Parameter

        protected string template_path, pageName, nameurl, url, cid, cidsub, id;
        int total;

        #endregion

        #region Common

        private void InitPage()
        {
            template_path = WebUtils.GetWebPath();
            pageName = Utils.GetParameter("page", "home");
            cid = Utils.GetParameter("cid", string.Empty);
            cidsub = Utils.GetParameter("cidsub", string.Empty);
            id = Utils.GetParameter("id", string.Empty);

            GetDetail();

        }

        /// <summary>
        /// ishHot=true,cateid=sanphamID
        /// </summary>
        private void GetDetail()
        {
            ProductBLL pcBll = new ProductBLL();
            IList<PNK_Product> lst = pcBll.GetListRelate(LangInt, pageName, string.Empty, id, 1, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCateDetail"]), out  total);

            if (total > 0)
            {
                this.rptResult.DataSource = lst;
                this.rptResult.DataBind();
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
                PNK_Product data = e.Item.DataItem as PNK_Product;
                HtmlAnchor hypTitle = e.Item.FindControl("hypTitle") as HtmlAnchor;
                HtmlAnchor hypImg = e.Item.FindControl("hypImg") as HtmlAnchor;

                cid = Utils.GetParameter("cid", string.Empty);
                string link = UtilityLocal.GetPathTreeNameUrl(data.CategoryId, LangInt, LangId);
                int level = link.Count(i => i.Equals('/'));
                if (level == 1)
                {
                    hypTitle.HRef = hypImg.HRef = LinkHelper.GetLink(UtilityLocal.GetPathTreeNameUrl(data.CategoryId, LangInt, LangId), LocalizationUtility.GetText("linkCate", Ci), LocalizationUtility.GetText("linktmp", Ci), data.ProductDesc.TitleUrl);
                }
                else if (level == 2)
                {
                    hypTitle.HRef = hypImg.HRef = LinkHelper.GetLink(link, LocalizationUtility.GetText("linktmp", Ci), data.ProductDesc.TitleUrl);
                }
                else if (level == 3)
                {
                    hypTitle.HRef = hypImg.HRef = LinkHelper.GetLink(link, data.ProductDesc.TitleUrl);
                }
                else if (level == 4)
                {
                    hypTitle.HRef = hypImg.HRef = LinkHelper.GetLink(link, data.ProductDesc.TitleUrl);
                }

                HtmlImage img = e.Item.FindControl("img") as HtmlImage;
                img.Src = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductUpload"], data.Image);

                Literal ltrTitle = e.Item.FindControl("ltrTitle") as Literal;
                ltrTitle.Text = hypTitle.Title = hypImg.Title = img.Alt = img.Attributes["title"] = data.ProductDesc.Title;
            }
        }

        #endregion
    }
}