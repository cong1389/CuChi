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
using System.Text;

namespace Cb.Web.Controls
{
    public partial class block_category_left : DGCUserControl
    {
        #region Parameter

        protected string template_path, pageName, cid, cidsub, id, records, hot = string.Empty, feature = string.Empty, categoryID = string.Empty, parentID = string.Empty;
        int total, level = 0;

        string categoryIdByPass = string.Empty;
        public string CategoryIdByPass
        {
            get
            {
                if (CategoryIdByPass != string.Empty)
                    return categoryIdByPass;
                else
                    return string.Empty;
            }
            set
            {
                categoryIdByPass = value;
            }
        }

        int totalSearch;
        public int TotalSearch { get; set; }

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

        ProductCategoryBLL pcBll = new ProductCategoryBLL();
        IList<PNK_Product> _lstSearch;
        public IList<PNK_Product> lstSearch { set; get; }

        #endregion

        #region Common

        private void InitPage()
        {
            template_path = WebUtils.GetWebPath();
            pageName = Utils.GetParameter("page", "home");
            cid = Utils.GetParameter("cid", string.Empty);
            cidsub = Utils.GetParameter("cidsub", string.Empty);
            id = Utils.GetParameter("id", string.Empty);

            if (Session["level"] != null)
            {
                level = DBConvert.ParseInt(Session["level"].ToString());
            }

            if (pageName == "tim-kiem" || pageName == "search")
            {
                this.records = DBConvert.ParseString(totalSearch);
                this.pager.PageSize = DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCate"]);
                this.pager.ItemCount = totalSearch;

                this.rptResult.DataSource = lstSearch;
                this.rptResult.DataBind();

                //Get left menu category
                GetCateLeft("search");
            }
            else
            {
                string pathUsc = pageName;
                switch (pageName)
                {
                    case "home":
                    case "trang-chu":
                        divBoxTop.Style.Add("display", "none");
                        break;
                    default:
                        divBoxTop.Style.Add("display", "block");
                        break;
                }
                GetProductCategory();
                // GetProduct(string.Empty);

                //Get left menu category
                GetCateLeft(Session["level"].ToString());
            }
        }

        private void GetProduct(string categoryID)
        {
            try
            {
                categoryID = categoryIdByPass == string.Empty ? categoryID : categoryIdByPass;

                ProductBLL pcBll = new ProductBLL();
                Dictionary<string, object> dic = Common.UtilityLocal.GetProduct(pageName, categoryID, level, LangInt, currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCate"]));
                if (dic != null && dic.Count > 0)
                {
                    IList<PNK_Product> lst = dic["DictProduct"] as IList<PNK_Product>;
                    if (lst.Count > 0)
                    {
                        this.records = dic["DictProduct_Total"].ToString();
                        this.pager.PageSize = DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCate"]);
                        this.pager.ItemCount = DBConvert.ParseDouble(dic["DictProduct_Total"].ToString());

                        this.rptResult.DataSource = lst;
                        this.rptResult.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("block_category", "GetProduct", ex.Message);
            }
        }

        private string GetProductCategory()
        {
            Dictionary<string, object> dic = Common.UtilityLocal.GetProductCategory(pageName, cid, cidsub, id, LangInt, Session["level"].ToString());
            if (dic != null && dic.Count > 0)
            {
                IList<PNK_ProductCategory> lst = dic["DictProductCategory"] as IList<PNK_ProductCategory>;
                if (lst.Count > 0)
                {
                    //Gen html image category
                    ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);
                    GetProduct(lst[0].Id.ToString());

                    WebUtils.SeoPage(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaDecription, lst[0].ProductCategoryDesc.MetaKeyword, this.Page);
                    WebUtils.SeoTagH(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, Controls);
                }
            }

            return categoryID;
        }

        //Get left menu category
        private void GetCateLeft(string level)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            ProductCategoryBLL pcBll = new ProductCategoryBLL();
            IList<PNK_ProductCategory> lstResult = null;
            IList<PNK_ProductCategory> lst = null;
            IList<PNK_ProductCategory> lst2 = null;
            IList<PNK_ProductCategory> lst3 = null;
            string cateName = Common.UtilityLocal.GetCateNameByLevel(pageName, cid, cidsub, id);
            switch (level)
            {
                case "1":
                    lst = pcBll.GetList(LangInt, cateName, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out  total);
                    if (lst.Count > 0)
                    {
                        lstResult = pcBll.GetList(LangInt, string.Empty, string.Empty, lst[0].Id, false, "p.ordering", 1, 1000, out  total);
                    }
                    break;
                case "2":
                    lst = pcBll.GetList(LangInt, pageName, string.Empty, int.MinValue, false, "p.ordering", 1, 1000, out  total);
                    if (lst.Count > 0)
                    {
                        lst2 = pcBll.GetList(LangInt, cid, string.Empty, lst[0].Id, false, "p.ordering", 1, 1000, out  total);
                        if (lst2.Count > 0)
                        {
                            lstResult = pcBll.GetList(LangInt, string.Empty, string.Empty, lst2[0].Id, false, "p.ordering", 1, 1000, out  total);
                        }
                    }
                    break;
                case "3":
                    lst = pcBll.GetList(LangInt, pageName, string.Empty, int.MinValue, false, "p.ordering", 1, 1000, out  total);
                    if (lst.Count > 0)
                    {
                        lst2 = pcBll.GetList(LangInt, cid, string.Empty, int.MinValue, lst[0].Id, 1, true, "p.ordering", 1, 1000, out  total);
                        //lst2 = pcBll.GetList(LangInt, cid, string.Empty, lst[0].Id, false, "p.ordering", 1, 1000, out  total);
                        if (lst2.Count > 0)
                        {
                            lst2 = pcBll.GetList(LangInt, cidsub, string.Empty, int.MinValue, lst2[0].Id, 1, true, "p.ordering", 1, 1000, out  total);
                            if (lst2.Count > 0)
                            {
                                lstResult = pcBll.GetList(LangInt, string.Empty, string.Empty, int.MinValue, lst2[0].Id, 1, true, "p.ordering", 1, 1000, out  total);
                            }
                        }
                    }
                    break;
                case "4":
                    lst = pcBll.GetList(LangInt, pageName, string.Empty, int.MinValue, false, "p.ordering", 1, 1000, out  total);
                    if (lst.Count > 0)
                    {
                        lst2 = pcBll.GetList(LangInt, cid, string.Empty, lst[0].Id, false, "p.ordering", 1, 1000, out  total);
                        if (lst2.Count > 0)
                        {
                            lst2 = pcBll.GetList(LangInt, cidsub, "1", int.MinValue, lst2[0].Id, 1, true, "p.ordering", 1, 1000, out  total);
                            if (lst2.Count > 0)
                            {
                                lstResult = pcBll.GetList(LangInt, string.Empty, "1", int.MinValue, lst2[0].Id, 1, true, "p.ordering", 1, 1000, out  total);
                                //DataTable dtb = DBHelper.ExcuteFromCmd("SELECT * FROM dbo.fc_GetAllChildProductCategory(" + lst2[0].Id + ",1)", null);
                                //if (dtb != null && dtb.Rows.Count > 0)
                                //{
                                //    string[] array = dtb.AsEnumerable()
                                //            .Select(row => row.Field<Int32>("id").ToString())
                                //            .ToArray();                                 
                                //    string idFirst = string.Join(",", array);
                                //    //lst2 = lst.Where(

                                //}
                            }
                        }

                    }
                    break;
                case "tim-kiem":
                case "search":
                    lstResult = pcBll.GetList(LangInt, cid, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out  total);
                    //if (lst.Count > 0)
                    //{
                    //    lst2 = pcBll.GetList(LangInt, cid, string.Empty, lst[0].Id, false, "p.ordering", 1, 1000, out  total);
                    //}
                    break;
            }

            if (lstResult.Count > 0)
            {
                this.rptLeftCate.DataSource = lstResult;
                this.rptLeftCate.DataBind();
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
                HtmlAnchor hypImg = e.Item.FindControl("hypImg") as HtmlAnchor;
                HtmlAnchor hypTitle = e.Item.FindControl("hypTitle") as HtmlAnchor;

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
                    //hypTitle.HRef = hypImg.HRef = LinkHelper.GetLink(pageName, LangId, cid, data.CategoryUrlDesc, data.ProductDesc.TitleUrl);
                }
                else if (level == 4)
                {
                    hypTitle.HRef = hypImg.HRef = LinkHelper.GetLink(link, data.ProductDesc.TitleUrl);
                }

                HtmlImage img = e.Item.FindControl("img") as HtmlImage;
                img.Src = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductUpload"], data.Image);

                Literal ltrTitle = e.Item.FindControl("ltrTitle") as Literal;
                hypImg.Title = ltrTitle.Text = img.Alt = img.Attributes["title"] = data.ProductDesc.Title;

                Literal ltrBrief = e.Item.FindControl("ltrBrief") as Literal;
                ltrBrief.Text = data.ProductDesc.Brief;

                StringBuilder sbPrice = new StringBuilder();
                StringBuilder sbDiscountPercent = new StringBuilder();
                //Nếu không có giá giảm
                if (string.IsNullOrWhiteSpace(data.Post))
                {
                    string normalPrice = data.Website == "" ? "Call" : string.Format("$USD {0}", data.Website);
                    sbPrice.Append("<div class=\"package-info\"><i class=\"icon-tag\"></i><span class=\"package-price\">" + normalPrice + "</span></div>");
                    sbDiscountPercent.Append("<div class=\"package-ribbon-wrapper\"><div class=\"hidden package-type normal-type\">Learn More</div><div class=\"clear\"></div><div class=\"hidden package-type-gimmick\"></div><div class=\"clear\"></div></div>");
                }
                //Nếu có giá giảm
                else
                {
                    string normalPrice = data.Website == "" ? "Call" : string.Format("$USD {0}", data.Website);
                    decimal discountPercent = ((DBConvert.ParseDecimal(data.Website) - DBConvert.ParseDecimal(data.Post)) / DBConvert.ParseDecimal(data.Website)) * 100;
                    sbPrice.Append("<div class=\"package-info last-minute\"><div class=\"package-info-inner\"><span class=\"discount-price\"><i class=\"icon-tag\"></i> $USD " + data.Post + "</span><span class=\"normal-price\">$USD " + data.Website + "</span></div></div>");
                    sbDiscountPercent.Append("<div class=\"package-ribbon-wrapper\"><div class=\"hidden package-type last-minute\"><span class=\"head\">Last Minute</span><span class=\"discount-text\">" + discountPercent + " Off</span></div><div class=\"clear\"></div><div class=\"hidden package-type-gimmick\"></div><div class=\"clear\"></div></div>");
                }
                Literal ltrPrice = e.Item.FindControl("ltrPrice") as Literal;
                ltrPrice.Text = sbPrice.ToString();
                Literal ltrDiscountPercent = e.Item.FindControl("ltrDiscountPercent") as Literal;
                ltrDiscountPercent.Text = sbDiscountPercent.ToString();

                Literal ltrDate = e.Item.FindControl("ltrDate") as Literal;
                ltrDate.Text = data.Area == "" ? "Available all year round" : string.Format("{0}; {1}", data.Area, data.Code);
            }
        }

        protected void rptLeftCate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PNK_ProductCategory data = e.Item.DataItem as PNK_ProductCategory;
                RadioButton rd = e.Item.FindControl("rdTrip1") as RadioButton;
                rd.Text = data.ProductCategoryDesc.Name.Trim();
            }
        }

        protected void chk_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdTrip = (RadioButton)sender;
            if (rdTrip.Checked)
            {
                pageName = Utils.GetParameter("page", "home");
                cid = Utils.GetParameter("cid", string.Empty);
                cidsub = Utils.GetParameter("cidsub", string.Empty);
                id = Utils.GetParameter("id", string.Empty);

                //string cateName = Common.UtilityLocal.GetCateNameByLevel(pageName, cid, cidsub, id);
                string chkID = Utils.RemoveUnicode(rdTrip.Text).ToLower();
                string level = id != string.Empty ? "3" : Session["level"].ToString();
                Dictionary<string, object> dic = Common.UtilityLocal.GetProductCategory(pageName, cid, cidsub, string.Empty, LangInt, level);
                if (dic != null && dic.Count > 0)
                {
                    IList<PNK_ProductCategory> lst = dic["DictProductCategory"] as IList<PNK_ProductCategory>;
                    if (lst.Count > 0)
                    {
                        //lst là danh sách category đến cấp hiện tại, nhưng khi click chk cần lấy hết cấp con của cấp hiện tại
                        ProductCategoryBLL pcBllCate = new ProductCategoryBLL();
                        IList<PNK_ProductCategory> lst1 = pcBllCate.GetList(LangInt, string.Empty, string.Empty, lst[0].Id, true, "p.ordering", 1, 1000, out  total);
                        IList<PNK_ProductCategory> lst2 = lst1.Where(m => m.ProductCategoryDesc.NameUrl == chkID).ToList();

                        //Gen html image category
                        ltrHeaderCategory.Text = lst2.Count > 0 ? Common.UtilityLocal.ImagePathByFont(lst2[0]) : string.Empty;
                        categoryID = lst2.Count > 0 ? lst2[0].Id.ToString() : string.Empty;

                        //Get product by categoryID when checked
                        if (categoryID != string.Empty)
                        {
                            DataTable dtb = DBHelper.ExcuteFromCmd("SELECT * FROM dbo.fc_GetAllChildProductCategory(" + categoryID + ",1)", null);
                            string[] array = dtb.AsEnumerable()
                                                .Select(row => row.Field<Int32>("id").ToString())
                                                .ToArray();
                            string idFirst = string.Join(",", array);

                            total = 0;
                            ProductBLL pcBll = new ProductBLL();
                            IList<PNK_Product> lstProduct = pcBll.GetList(LangInt, string.Empty, "1", idFirst, string.Empty, currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCate"]), out total);
                            this.records = DBConvert.ParseString(total);
                            this.pager.PageSize = DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCate"]);
                            this.pager.ItemCount = total;
                            this.rptResult.DataSource = lstProduct;
                            this.rptResult.DataBind();

                            WebUtils.SeoPage(lst2[0].ProductCategoryDesc.MetaTitle, lst2[0].ProductCategoryDesc.MetaDecription, lst2[0].ProductCategoryDesc.MetaKeyword, this.Page);
                        }
                        else
                        {
                            this.records = DBConvert.ParseString(total);
                            this.pager.PageSize = DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCate"]);
                            this.pager.ItemCount = total;
                            this.rptResult.DataSource = lst2;
                            this.rptResult.DataBind();
                        }

                    }
                }
            }
        }

        public void pager_Command(object sender, CommandEventArgs e)
        {
            this.currentPageIndex = Convert.ToInt32(e.CommandArgument);
            pager.CurrentIndex = this.currentPageIndex;
            InitPage();
        }

        #endregion
    }
}