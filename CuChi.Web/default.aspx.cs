using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.BLL;
using Cb.Model;
using Cb.DBUtility;
using Cb.Model.Products;
using Cb.BLL.Product;
using Cb.Localization;
using System.Configuration;
using Cb.Utility;
using System.Globalization;
using System.Threading;

namespace Cb.Web
{
    public partial class _default : DGCPage
    {
        #region Parameter

        protected string cid, cidsub, pagePath, id, template_path = string.Empty, langId = string.Empty;
        private int total;

        UserControl contentView = new UserControl();
        #endregion

        #region Common

        private void GetPageName(string pageName)
        {
            try
            {
                cid = Utils.GetParameter("cid", string.Empty);
                cidsub = Utils.GetParameter("cidsub", string.Empty);
                id = Utils.GetParameter("id", string.Empty);

                if (pageName == "booking")
                {
                    pagePath = "Pages/BookingManagement/Booking.ascx";
                    Session["level"] = 1;
                }

                else
                {
                    string cateName = string.Empty;
                    ProductCategoryBLL pcBllCate = new ProductCategoryBLL();
                    cateName = Common.UtilityLocal.GetCateNameByLevel(pageName, cid, cidsub, id);
                    IList<PNK_ProductCategory> lstCate = pcBllCate.GetList(LangInt, cateName, string.Empty, int.MinValue, false, "p.ordering", 1, 9999, out  total);
                    if (total > 0)
                    {
                        pagePath = lstCate[0].Page;
                        Session["level"] = lstCate[0].PathTree.Count(i => i.Equals('.'));
                    }

                    if (lstCate == null || total == 0)
                    {
                        ProductBLL pcBll = new ProductBLL();
                        cid = cid != LocalizationUtility.GetText("linkCate", Ci) ? cid : string.Empty;
                        cidsub = cidsub != LocalizationUtility.GetText("linktmp", Ci) ? cidsub : string.Empty;
                        IList<PNK_Product> lst = pcBll.GetList(LangInt, cidsub, string.Empty, string.Empty, id, null, string.Empty, 1, 9999, out total);
                        if (total > 0)
                        {
                            //set page đặc biệt khi level=4 mà thiếu cid, cidsub
                            if (cid == LocalizationUtility.GetText("linkCate", Ci) || cid == LocalizationUtility.GetText("linktmp", Ci))
                            {
                                lst = lst.Where(p => p.ProductDesc.TitleUrl == id && p.CategoryUrlDesc == pageName).ToList();
                            }
                            else
                            {
                                lst = lst.Where(p => p.ProductDesc.TitleUrl == id).ToList();
                            }
                            if (lst.Count > 0)
                            {
                                string pagePathProduct = Common.UtilityLocal.GetPathTreeNameUrl(lst[0].Id, LangInt, LangId);
                                pagePath = lst[0].Page;
                                Session["level"] = 3;
                            }
                        }
                    }
                }
                contentView = (UserControl)Page.LoadControl(pagePath);
                phdContent.Controls.Add(contentView);
            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("default.aspx", "GetPageName", ex.Message + "/" + cid + "/" + cidsub + "/" + id);
            }
        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //pagePath = Utils.GetParameter("page", "home");
            GetPageName(Utils.GetParameter("page", "home"));

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //}
        }

        #endregion
    }
}