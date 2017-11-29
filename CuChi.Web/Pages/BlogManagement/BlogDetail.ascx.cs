using Cb.BLL;
using Cb.BLL.Product;
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

namespace Cb.Web.Pages.BlogManagement
{
    public partial class BlogDetail : DGCUserControl
    {
        #region Parameter

        protected string title, metaDescription, metaKeyword, h1, h2, h3, analytic;
        protected string template_path, pageName, cid, id, cidsub, records;
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

            GetProductCategory();
            GetDetail();
        }

        private void GetDetail()
        {
            ProductBLL pcBll = new ProductBLL();
            IList<PNK_Product> lst = pcBll.GetList(LangInt, string.Empty, string.Empty, string.Empty, id, null, string.Empty, 1, 1, out total);
            if (total > 0)
            {
                ltrTitle.Text = lst[0].ProductDesc.Title;
                ltrDetail.Text = lst[0].ProductDesc.Detail;
                ltrDate.Text = lst[0].PostDate.ToString("dd/MM/yyyy");
                GetListImage(lst[0].Id);

                WebUtils.SeoPage(lst[0].ProductDesc.MetaTitle, lst[0].ProductDesc.Metadescription, lst[0].ProductDesc.MetaKeyword, this.Page);
                WebUtils.SeoTagH(lst[0].ProductDesc.H1, lst[0].ProductDesc.H2, lst[0].ProductDesc.H3, this.Controls);
            }
        }

        private string GetProductCategory()
        {
            ProductCategoryBLL pcBll = new ProductCategoryBLL();
            IList<PNK_ProductCategory> lst = null;

            if (cidsub != string.Empty && cid != string.Empty && id != string.Empty)
            {
                lst = pcBll.GetList(LangInt, pageName, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out  total);

                //Gen html image category
                ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);
            }
            else if (cidsub != string.Empty && cid != string.Empty)
            {
                lst = pcBll.GetList(LangInt, cid, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out  total);
                if (total > 0)
                {
                    //lần 2 truyền parentID vào để lấy ds con
                    lst = pcBll.GetList(LangInt, string.Empty, string.Empty, lst[0].Id, false, "p.ordering", 1, 1000, out  total);

                    //Gen html image category
                    ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);
                }
            }
            else if (cidsub == string.Empty && cid != string.Empty)
            {
                lst = pcBll.GetList(LangInt, cid, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out  total);
                if (total > 0)
                {
                    //Gen html image category
                    ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);

                }
            }
            else if (cid == string.Empty)
            {
                //lần đầu lấy parentID
                lst = pcBll.GetList(LangInt, pageName, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out  total);
                if (total > 0)
                {
                    //Gen html image category
                    ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);

                }
            }

            return string.Empty;
        }

        private void GetListImage(int productID)
        {
            UploadImageBLL bll = new UploadImageBLL();
            IList<PNK_UploadImage> lst = bll.GetList(string.Empty, productID.ToString(), "1", 1, 100, out  total);
            if (total > 0)
            {
                rptImg.DataSource = lst;
                rptImg.DataBind();
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

        protected void rptImg_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PNK_UploadImage data = e.Item.DataItem as PNK_UploadImage;

                HtmlImage img = e.Item.FindControl("img") as HtmlImage;

                img.Src = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductUpload"], data.Name);
                img.Alt = img.Attributes["title"] = data.Name;
            }
        }

        #endregion
    }
}