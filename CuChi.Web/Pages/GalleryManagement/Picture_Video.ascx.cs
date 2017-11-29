using Cb.BLL.Product;
using Cb.Model.Products;
using Cb.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cb.Web.Pages.GalleryManagement
{
    public partial class Picture_Video : DGCUserControl
    {
        #region Parameter

        private string title, metaDescription, metaKeyword, h1, h2, h3, analytic, background;
        protected string template_path, pageName, cid, id, records;
        int total;

        #endregion

        #region Common

        private void InitPage()
        {
            template_path = WebUtils.GetWebPath();
            pageName = Utils.GetParameter("page", "home");
            cid = Utils.GetParameter("cid", string.Empty);
            id = Utils.GetParameter("id", string.Empty);

            GetDetail();
        }

        private void GetDetail()
        {
            ProductBLL pcBll = new ProductBLL();
            IList<PNK_Product> lst = null;
            if (Session["level"] != null)
            {
                string level = Session["level"].ToString();
                switch (level)
                {
                    case "1":
                        lst = pcBll.GetList(LangInt, pageName, string.Empty, string.Empty, string.Empty, null, string.Empty, 1, 9999, out total);
                        break;
                    case "2":
                        lst = pcBll.GetList(LangInt, cid, string.Empty, string.Empty, string.Empty, null, string.Empty, 1, 9999, out total);
                        break;
                    case "3":
                    default:
                        lst = pcBll.GetList(LangInt, string.Empty, string.Empty, string.Empty, id, null, string.Empty, 1, 9999, out total);
                        break;
                }
            }

            if (total > 0)
            {

                ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);

                WebUtils.SeoPage(lst[0].ProductDesc.MetaTitle, lst[0].ProductDesc.Metadescription, lst[0].ProductDesc.MetaKeyword, this.Page);
                WebUtils.SeoTagH(lst[0].ProductDesc.H1, lst[0].ProductDesc.H2, lst[0].ProductDesc.H3, this.Controls);
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

        #endregion
    }
}