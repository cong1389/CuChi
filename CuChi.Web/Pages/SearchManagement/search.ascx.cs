using Cb.BLL.Product;
using Cb.DBUtility;
using Cb.Model.Products;
using Cb.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Cb.Web.Common;
using Cb.Localization;

namespace Cb.Web.Pages.SearchManagement
{
    public partial class search : DGCUserControl
    {
        #region Parameter

        protected string template_path, pageName, cid, cidsub, id, records, hot = string.Empty, feature = string.Empty, categoryID = string.Empty;
        int total;

        protected int currentPageIndex
        {
            get
            {
                if (ViewState["CurrentPageIndex"] != null)
                    return int.Parse(ViewState["CurrentPageIndex"].ToString());
                else
                    return 1;
            }
            set
            {
                ViewState["CurrentPageIndex"] = value;
            }
        }

        #endregion

        #region Common

        private void InitPage()
        {
            template_path = WebUtils.GetWebPath();
            pageName = Utils.GetParameter("page", "home");
            cid = Utils.GetParameter("cid", string.Empty).Replace("/", "");
            cidsub = Utils.GetParameter("cidsub", string.Empty).Replace("/", "");
            id = Utils.GetParameter("id", string.Empty).Replace("/", "");

            //GetProductCategory();
            GetProduct();

        }

        private void GetProduct()
        {
            try
            {
                ProductBLL pcBll = new ProductBLL();
                IList<PNK_Product> lst = null;
                if (cid != string.Empty)
                {
                    lst = pcBll.GetListSearch(LangInt, cidsub.Replace("/", ""), "1", string.Empty, cid.Replace("/", ""), string.Empty, string.Empty, string.Empty, string.Empty, currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCate"]), out  total);
                    //lst = pcBll.GetListSearch(LangInt, cid, "1" string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCate"]), out  total);
                    if (total > 0)
                    {
                        block_category.lstSearch = lst;
                        block_category.TotalSearch = total;
                    }
                }


            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("block_category", "GetProduct", ex.Message);
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