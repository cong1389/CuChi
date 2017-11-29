using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Localization;
using Cb.Model;
using System.Globalization;
using Cb.BLL;
using System.IO;
using Cb.Utility;
using Cb.BLL.Product;
using Cb.Model.Products;
using Cb.DBUtility;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data;
using System.Web.UI.HtmlControls;
using Cb.Web.Common;
using Cb.Model;
using System.Text;

namespace Cb.Web.Controls
{
    public partial class block_filter : DGCUserControl
    {
        #region Parameter

        protected string pageName, template_path = string.Empty;

        private int id, total;

        IList<PNK_ProductCategory> lstAll;
        IList<PNK_ProductCategory> lstParent;
        #endregion

        #region Common

        private void InitPage()
        {

        }

        private void GetMenu()
        {
            string strTemp = string.Empty;
            drpDestination.Items.Clear();
            drpDestination.Items.Add(new ListItem("Where would you like to go?", string.Empty));

            ProductCategoryBLL pcBll = new ProductCategoryBLL();
            lstAll = pcBll.GetList(LangInt, string.Empty, "1", int.Parse(ConfigurationManager.AppSettings["parentIdCyclingTour"]), false, "p.ordering", 1, 1000, out total);
            if (total > 0)
            {
                foreach (PNK_ProductCategory item in lstAll)
                {
                    drpDestination.Items.Add(new ListItem(item.ProductCategoryDesc.Name, item.ProductCategoryDesc.NameUrl));
                }
            }

        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            GetMenu();
            if (!Page.IsPostBack)
            {
                InitPage();
            }
        }

        protected void btnSearchTrip_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}