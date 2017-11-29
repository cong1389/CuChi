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
using Cb.BLL;
using Cb.Model;
using System.Collections;


namespace Cb.Web.Pages.GalleryManagement
{
    public partial class Picture : DGCUserControl
    {
        #region Parameter

        protected string template_path, pageName, cid, cidsub, id, records, hot = string.Empty, feature = string.Empty, categoryID = string.Empty;
        int total;

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

            GetProduct();

        }

        private void GetProduct()
        {
            try
            {
                string categoryID = null;
                categoryID = GetProductCategory();

                ProductBLL pcBll = new ProductBLL();
                IList<PNK_Product> lst2 = null;
                IList<PNK_Product> lst3 = null;
                string level = string.Empty;

                if (Session["level"] != null)
                {
                    level = Session["level"].ToString();
                    switch (level)
                    {
                        case "1":
                            lst2 = pcBll.GetList(LangInt, pageName, string.Empty, string.Empty, string.Empty, null, string.Empty, 1, 9999, out total);
                            break;
                        case "2":
                            lst2 = pcBll.GetList(LangInt, cid, string.Empty, string.Empty, string.Empty, null, string.Empty, 1, 9999, out total);
                            GetListImage(lst2[0].Id.ToString(), rptImg);

                            ltrHeader.Text = lst2[0].ProductDesc.Title;
                            break;
                        case "3":
                        default:
                            lst2 = pcBll.GetList(LangInt, cid, string.Empty, string.Empty, string.Empty, null, string.Empty, 1, 9999, out total);
                            lst3 = lst2.Where(m => m.ProductDesc.TitleUrl == id).ToList();
                            GetListImage(lst3[0].Id.ToString(), rptImg);

                            ltrHeader.Text = lst3[0].ProductDesc.Title;
                            break;
                    }
                }

                if (total > 0)
                {
                    this.records = DBConvert.ParseString(total);
                    this.pager.PageSize = DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeCate"]);
                    this.pager.ItemCount = total;

                    this.rptResult.DataSource = lst2;
                    this.rptResult.DataBind();


                }
            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("block_category", "GetProduct", ex.Message);
            }
        }

        private string GetProductCategory()
        {
            ProductCategoryBLL pcBll = new ProductCategoryBLL();
            IList<PNK_ProductCategory> lst = null;
            IList<PNK_ProductCategory> lst2 = null;

            if (cidsub != string.Empty && cid != string.Empty && id != string.Empty)
            {
                lst = pcBll.GetList(LangInt, cid, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out  total);
                //ltrCateName.Text = lst[0].ProductCategoryDesc.Name;
                categoryID = lst[0].ProductCategoryDesc.Id.ToString();

                WebUtils.SeoPage(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaDecription, lst[0].ProductCategoryDesc.MetaKeyword, this.Page);
                WebUtils.SeoTagH(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, Controls);

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
                    WebUtils.SeoPage(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaDecription, lst[0].ProductCategoryDesc.MetaKeyword, this.Page);
                    WebUtils.SeoTagH(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, Controls);

                    //Gen html image category
                    ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);

                    //Chỉ có 1 menu cha
                    if (total > 0)
                    {
                        categoryID = lst[0].ProductCategoryDesc.Id.ToString();
                    }
                    else
                        categoryID = null;
                }
            }
            else if (cidsub == string.Empty && cid != string.Empty)
            {
                lst = pcBll.GetList(LangInt, cid, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out  total);
                if (total > 0)
                {
                    WebUtils.SeoPage(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaDecription, lst[0].ProductCategoryDesc.MetaKeyword, this.Page);
                    WebUtils.SeoTagH(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, Controls);
                    //IList<PNK_ProductCategory> lstSub = pcBll.GetList(LangInt, string.Empty, string.Empty, lst[0].Id, false, "p.ordering", 1, 1000, out  total);

                    //Gen html image category
                    ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);

                    if (lst.Count > 1)
                    {

                        //ltrCateNameTitle.Text = lst[0].ProductCategoryDesc.Name;
                        categoryID = lst[0].ProductCategoryDesc.Id.ToString();
                    }
                    else if (lst.Count == 1)
                    {

                        //ltrCateNameTitle.Text = lst[0].ProductCategoryDesc.Name;
                        categoryID = lst[0].ProductCategoryDesc.Id.ToString();
                    }
                    else
                        categoryID = null;
                }
            }
            else if (cid == string.Empty)
            {
                //lần đầu lấy parentID
                lst = pcBll.GetList(LangInt, pageName, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out  total);
                if (total > 0)
                {
                    WebUtils.SeoPage(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaDecription, lst[0].ProductCategoryDesc.MetaKeyword, this.Page);
                    WebUtils.SeoTagH(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, Controls);

                    //Gen html image category
                    ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);

                    if (lst.Count > 0)
                    {  //lần 2 truyền parentID vào để lấy ds con
                        lst2 = pcBll.GetList(LangInt, string.Empty, string.Empty, lst[0].Id, false, "p.ordering", 1, 1000, out  total);


                        //ltrCateNameTitle.Text = lst[0].ProductCategoryDesc.Name;
                        //ltrCategoryBrief.Text = lst[0].ProductCategoryDesc.Brief;
                        categoryID = lst[0].Id.ToString();
                    }
                    else
                    {
                        //lần 2 truyền parentID vào để lấy ds con
                        lst2 = pcBll.GetList(LangInt, string.Empty, string.Empty, lst[0].Id, false, "p.ordering", 1, 1000, out  total);


                        //ltrCateNameTitle.Text = lst2[0].ProductCategoryDesc.Name;
                        //ltrCategoryBrief.Text = lst[0].ProductCategoryDesc.Brief;
                        categoryID = lst2[0].ParentId.ToString();
                    }
                }
            }

            return categoryID;
        }

        private void GetListImage(string productID, Repeater rptImg)
        {
            UploadImageBLL bll = new UploadImageBLL();
            IList<PNK_UploadImage> lst = bll.GetList(string.Empty, productID, "1", 1, 100, out  total);
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

        protected void rptResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PNK_Product data = e.Item.DataItem as PNK_Product;

                HtmlAnchor hypImg = e.Item.FindControl("hypImg") as HtmlAnchor;
                HtmlAnchor hypTitle = e.Item.FindControl("hypTitle") as HtmlAnchor;

                Dictionary<string, object> dic = UtilityLocal.GetHRefByLevel(data, LangInt, LangId, Ci);
                hypTitle.HRef = hypImg.HRef = dic["HRef"].ToString();

                HtmlImage img = e.Item.FindControl("img") as HtmlImage;
                img.Src = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductUpload"], data.Image);

                Literal ltrTitle = e.Item.FindControl("ltrTitle") as Literal;
                ltrTitle.Text = data.ProductDesc.Title;
            }
        }

        protected void rptImg_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PNK_UploadImage data = e.Item.DataItem as PNK_UploadImage;

                HtmlImage img = e.Item.FindControl("img") as HtmlImage;
                HtmlImage imgThumb = e.Item.FindControl("imgThumb") as HtmlImage;
                HtmlAnchor hypImgThumb = e.Item.FindControl("hypImgThumb") as HtmlAnchor;
              
                string src = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductUpload"], data.Name);
                hypImgThumb.HRef = img.Src = imgThumb.Src = src;

                Literal ltrTitle = e.Item.FindControl("ltrTitle") as Literal;
                img.Alt = img.Attributes["title"] = imgThumb.Alt = imgThumb.Attributes["title"] = hypImgThumb.Title = ltrTitle.Text = WebUtils.GetFileName(data.Name);
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