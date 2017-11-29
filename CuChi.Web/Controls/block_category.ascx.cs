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
    public partial class block_category : DGCUserControl
    {
        #region Parameter

        protected string template_path, pageName, cid, cidsub, id, records, hot = string.Empty, feature = string.Empty, categoryID = string.Empty, parentID = string.Empty;
        int total;

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

        string categoryParentIdByPass = string.Empty;
        public string CategoryParentIdByPass
        {
            get
            {
                if (categoryParentIdByPass != string.Empty)
                    return categoryParentIdByPass;
                else
                    return string.Empty;
            }
            set
            {
                categoryParentIdByPass = value;
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


            if (pageName == "tim-kiem" || pageName == "search")
            {
                this.records = DBConvert.ParseString(totalSearch);
                this.pager.PageSize = DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCate"]);
                this.pager.ItemCount = totalSearch;

                this.rptResult.DataSource = lstSearch;
                this.rptResult.DataBind();

                ////Set header
                //ProductCategoryBLL pcBll = new ProductCategoryBLL();
                //IList<PNK_ProductCategory> lst = pcBll.GetList(LangInt, string.Empty, "1", DBConvert.ParseInt(ConfigurationManager.AppSettings["parentIdTemplate"]), int.MinValue, false, string.Empty, 1, 1, out  total);
                //if (total > 0)
                //{
                //    //ltrCateNameTitle.Text = lst[0].ProductCategoryDesc.Name;
                //    // ltrCategoryBrief.Text = lst[0].ProductCategoryDesc.Brief;
                //}
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

                GetProduct();
            }
        }

        private void GetProduct()
        {
            try
            {
                string categoryID = null;
                categoryID = categoryIdByPass == string.Empty ? GetProductCategory() : categoryIdByPass;
                if (categoryIdByPass != string.Empty)
                {
                    GetCateName(categoryIdByPass);
                }

                ProductBLL pcBll = new ProductBLL();
                IList<PNK_Product> lst = null;
                if (cidsub != string.Empty && cid != string.Empty)
                {
                    lst = pcBll.GetList(LangInt, cidsub, "1", string.Empty, string.Empty, currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCate"]), out total);
                }
                else if (cidsub == string.Empty && cid != string.Empty && categoryID != string.Empty)
                {
                    DataTable dtb = DBHelper.ExcuteFromCmd("SELECT * FROM dbo.fc_GetAllChildProductCategory(" + categoryID + ",1)", null);
                    string[] array = dtb.AsEnumerable()
                                        .Select(row => row.Field<Int32>("id").ToString())
                                        .ToArray();
                    string idFirst = string.Join(",", array);
                    lst = pcBll.GetList(LangInt, string.Empty, "1", idFirst, string.Empty, currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCate"]), out total);

                }
                else if (cid == string.Empty && categoryID != string.Empty)
                {
                    DataTable dtb = DBHelper.ExcuteFromCmd("SELECT * FROM dbo.fc_GetAllChildProductCategory(" + categoryID + ",1)", null);
                    if (dtb != null && dtb.Rows.Count > 0)
                    {
                        string[] array = dtb.AsEnumerable()
                                .Select(row => row.Field<Int32>("id").ToString())
                                .ToArray();
                        string idFirst = string.Join(",", array);
                        if (pageName == "trang-chu" || pageName == "home")
                        {
                            lst = pcBll.GetList(LangInt, string.Empty, "1", idFirst, string.Empty, string.Empty, "1", string.Empty, string.Empty, currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCate"]), out total);
                            pager.Visible = false;
                        }
                        else
                        {
                            lst = pcBll.GetList(LangInt, string.Empty, "1", idFirst, string.Empty, currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCate"]), out total);
                        }
                    }
                }

                //total = lst.Count;
                if (total > 0)
                {
                    this.records = DBConvert.ParseString(total);
                    this.pager.PageSize = DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCate"]);
                    this.pager.ItemCount = total;

                    this.rptResult.DataSource = lst;
                    this.rptResult.DataBind();

                }
            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("block_category", "GetProduct", ex.Message);
            }
        }

        private void GetCateName(string categoryIdByPass)
        {
            ProductCategoryBLL pcBll = new ProductCategoryBLL();
            IList<PNK_ProductCategory> lst = pcBll.GetListTree(LangInt,string.Empty, string.Empty, DBConvert.ParseInt(CategoryParentIdByPass), int.MinValue, string.Empty
            , 1, true, string.Empty, 1, 1, out  total);
            if (lst.Count > 0)
            {
                ltrCateName.Text = lst[0].ProductCategoryDesc.Name;

                string link = Utils.CombineUrl(Template_path, UtilityLocal.AppendLanguage(lst[0].ProductCategoryDesc.TreeNameUrlDesc, LangId));
                hypCateName.HRef = link;
            }
        }

        private string GetProductCategory()
        {
            ProductCategoryBLL pcBll = new ProductCategoryBLL();
            IList<PNK_ProductCategory> lst = null;
            IList<PNK_ProductCategory> lst2 = null;

            if (cidsub != string.Empty && cid != string.Empty && id != string.Empty)
            {
                lst = pcBll.GetList(LangInt, cid, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out total);
                //ltrCateNameTitle.Text = lst[0].ProductCategoryDesc.Name;
                categoryID = lst[0].ProductCategoryDesc.Id.ToString();

                WebUtils.SeoPage(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaDecription, lst[0].ProductCategoryDesc.MetaKeyword, this.Page);
                WebUtils.SeoTagH(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, Controls);
            }
            else if (cidsub != string.Empty && cid != string.Empty)
            {
                lst = pcBll.GetList(LangInt, cid, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out total);
                if (total > 0)
                {
                    //lần 2 truyền parentID vào để lấy ds con
                    lst = pcBll.GetList(LangInt, string.Empty, string.Empty, lst[0].Id, false, "p.ordering", 1, 1000, out total);
                    WebUtils.SeoPage(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaDecription, lst[0].ProductCategoryDesc.MetaKeyword, this.Page);
                    WebUtils.SeoTagH(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, Controls);

                    //Chỉ có 1 menu cha
                    if (total > 0)
                    {
                        //Gen html image category
                        ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);

                        //ltrCateNameTitle.Text = lst[0].ProductCategoryDesc.Name;
                        categoryID = lst[0].ProductCategoryDesc.Id.ToString();
                    }
                    else
                        categoryID = null;
                }
            }
            else if (Session["level"] != null)
            {
                string level = Session["level"].ToString();
                switch (level)
                {
                    case "1":
                        lst = pcBll.GetList(LangInt, pageName, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out total);
                        if (total > 0)
                        {
                            lst2 = pcBll.GetList(LangInt, cid, string.Empty, lst[0].Id, true, "p.ordering", 1, 1000, out total);
                            if (lst2.Count > 0)
                            {
                                //Gen html image category
                                ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);

                                categoryID = lst2[0].ProductCategoryDesc.Id.ToString();
                            }
                            else
                                //Nếu không cấp con của cấp 1 thì lấy luôn cấp 1
                                categoryID = lst[0].ProductCategoryDesc.Id.ToString();
                        }
                        break;
                    case "2":
                        lst = pcBll.GetList(LangInt, pageName, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out total);
                        if (total > 0)
                        {
                            lst2 = pcBll.GetList(LangInt, cid, string.Empty, lst[0].Id, true, "p.ordering", 1, 1000, out total);
                            if (lst2.Count > 0)
                            {
                                //Gen html image category
                                ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);

                                categoryID = lst2[0].ProductCategoryDesc.Id.ToString();

                            }
                        }
                        break;
                }
            }
            else if (cidsub == string.Empty && cid != string.Empty)
            {
                lst = pcBll.GetList(LangInt, cid, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out total);
                lst = lst.Where(m => m.ProductCategoryDesc.NameUrl == cid).ToList();

                WebUtils.SeoPage(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaDecription, lst[0].ProductCategoryDesc.MetaKeyword, this.Page);
                WebUtils.SeoTagH(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, Controls);
                IList<PNK_ProductCategory> lstSub = pcBll.GetList(LangInt, string.Empty, string.Empty, lst[0].Id, false, "p.ordering", 1, 1000, out total);

                if (lstSub.Count > 0)
                {
                    //Gen html image category
                    ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);

                    //ltrCateNameTitle.Text = lst[0].ProductCategoryDesc.Name;
                    categoryID = lst[0].ProductCategoryDesc.Id.ToString();

                }
                else
                    categoryID = null;
            }
            else if (cid == string.Empty)
            {
                //lần đầu lấy parentID
                //Xài chung với home, nên set
                if (pageName == "trang-chu" || pageName == "home")
                {
                    lst = pcBll.GetList(LangInt, string.Empty, string.Empty, DBConvert.ParseInt(ConfigurationManager.AppSettings["parentIdChoose"]), int.MinValue, true, string.Empty, 1, 1000, out total);
                }
                else
                {
                    lst = pcBll.GetList(LangInt, pageName, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out total);
                    WebUtils.SeoPage(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaDecription, lst[0].ProductCategoryDesc.MetaKeyword, this.Page);
                    WebUtils.SeoTagH(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, Controls);
                }

                //lần 2 truyền parentID vào để lấy ds con
                lst2 = pcBll.GetList(LangInt, string.Empty, string.Empty, lst[0].Id, false, "p.ordering", 1, 1000, out total);
                //lst = lst2.Count > 0 ? lst2 : lst;
                if (lst.Count > 0)
                {
                    //Gen html image category
                    ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);

                    //ltrCateNameTitle.Text = lst[0].ProductCategoryDesc.Name;
                    categoryID = lst[0].Id.ToString();
                }
                else
                {
                    //Gen html image category
                    ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);

                    //ltrCateNameTitle.Text = lst2[0].ProductCategoryDesc.Name;
                    categoryID = lst2[0].ParentId.ToString();
                    parentID = lst2[0].ParentId.ToString();
                }
            }

            return categoryID;
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
                //img.Attributes.Add("data-lazysrc", WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductUpload"], data.Image));

                Literal ltrTitle = e.Item.FindControl("ltrTitle") as Literal;
                hypImg.Title = ltrTitle.Text = img.Alt = img.Attributes["title"] = data.ProductDesc.Title;

                Literal ltrBrief = e.Item.FindControl("ltrBrief") as Literal;
                ltrBrief.Text = data.ProductDesc.Brief;

                StringBuilder sbPrice = new StringBuilder();
                StringBuilder sbPriceFrom = new StringBuilder();
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

                    //sbPriceFrom.Append("<div class=\"package-info last-minute\"><div class=\"package-info-inner\"><span class=\"discount-price\"><i class=\"icon-tag\"></i>Price from $USD " + data.Post + "</span></div></div>");
                }
                Literal ltrPrice = e.Item.FindControl("ltrPrice") as Literal;
                ltrPrice.Text = sbPrice.ToString();
                Literal ltrDiscountPercent = e.Item.FindControl("ltrDiscountPercent") as Literal;
                ltrDiscountPercent.Text = sbDiscountPercent.ToString();

                Literal ltrPriceFrom = e.Item.FindControl("ltrPriceFrom") as Literal;
                //ltrPriceFrom.Text = sbPriceFrom.ToString();

                //Literal ltrPriceNormal = e.Item.FindControl("ltrPriceNormal") as Literal;
                //ltrPriceNormal.Text = data.Website;

                //Literal ltrPriceDiscount = e.Item.FindControl("ltrPriceDiscount") as Literal;
                //ltrPriceDiscount.Text = data.Latitude;

                Literal ltrDate = e.Item.FindControl("ltrDate") as Literal;
                ltrDate.Text = data.Area == "" ? "Available all year round" : string.Format("{0}; {1}", data.Area, data.Code);
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