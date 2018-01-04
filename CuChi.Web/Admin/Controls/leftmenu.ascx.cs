using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Model;
using Cb.Utility;
using Cb.BLL.Product;
using Cb.Model.Products;
using Cb.DBUtility;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace Cb.Web.Admin.Controls
{
    public partial class leftmenu : DGCUserControl
    {
        #region Parameter

        IList<PNK_ProductCategory> lstAll;

        protected string template_path
        {
            get
            {
                if (ViewState["template_path"] != null)
                    return ViewState["template_path"].ToString();
                return string.Empty;
            }
            set
            {
                ViewState["template_path"] = value;
            }
        }

        #endregion

        #region Common

        private void InitPage()
        {
            this.template_path = WebUtils.GetWebPath();

            SetLink();
            GetUserInfo();
        }

        private void GetUserInfo()
        {
            PNK_User lstUser = (PNK_User)Session[Global.SESS_USER];
            switch (lstUser.Username)
            {
                case "congtt":
                    hypSeo.Visible = true;
                    break;
            }
        }

        private void SetLink()
        {
            //Category
            hypManageCategories.HRef = LinkHelper.GetAdminLink("productcategory");
            hypManageItem.HRef = LinkHelper.GetAdminLink("product");
            hypSlide.HRef = LinkHelper.GetAdminLink("slider");

            hypManageBooking.HRef = LinkHelper.GetAdminLink("booking");

            //User
            hypManageUser.HRef = LinkHelper.GetAdminLink("user");

            //Setting
            hypPage.HRef = LinkHelper.GetAdminLink("page");
            hypConfiguration.HRef = LinkHelper.GetAdminLink("config");
            hypSeo.HRef = LinkHelper.GetAdminLink("seo");

            //Logo
            hypLogo.HRef = Utils.CombineUrl(Template_path, "admin");

            //ContentStatic
            hypContentStatic.HRef = LinkHelper.GetAdminLink("contentstatic");

            //Booking Price
            hypManageBookingPrice.HRef = LinkHelper.GetAdminLink("bookingprice");

            //Booking Group
            hypTourPriceClass.HRef = LinkHelper.GetAdminLink("tourpriceclass");
            hypManageBookingGroup.HRef = LinkHelper.GetAdminLink("bookinggroup");

            hypManageCountry.HRef = LinkHelper.GetAdminLink("country");

            hypExchageRate.HRef = LinkHelper.GetAdminLink("exchangerate");

            hypManageGallery.HRef = LinkHelper.GetAdminLink("gallery");

        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitPage();
                GenerateMenuProduct();
            }
        }

        #endregion

        private void GenerateMenuProduct()
        {
            IList<PNK_ProductCategory> lstParent;
            int total;
            ProductCategoryBLL ncBll = new ProductCategoryBLL();
            lstAll = ncBll.GetList(Constant.DB.LangId, string.Empty, 1, 300, out total);

            if (total > 0)
            {
                //Lấy danh sách danh mục cha có ParentID==0 gán vào menu cha         
                lstParent = lstAll.Where(m => m.ParentId == 0                   
                    && m.Id != 2229
                     && m.Id != 2222
                     && m.Id != 2220
                     && m.Id != 2207
                     && m.Id != 124
                     && m.Id != 73
                     && m.Id != 64
                     && m.Id != 64
                     && m.Id != 42

                    && m.Id != DBConvert.ParseInt(ConfigurationManager.AppSettings["parentIdHome"])
                    ).ToList();
                if (lstParent.Count() > 0)
                {
                    rptResult.DataSource = lstParent;
                    rptResult.DataBind();
                }
            }
        }

        protected void hypClearCache_ServerClick(object sender, EventArgs e)
        {
            CacheHelper.ClearAll();
        }

        protected void rptResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PNK_ProductCategory data = e.Item.DataItem as PNK_ProductCategory;

                HtmlAnchor hypName = e.Item.FindControl("hypName") as HtmlAnchor;
                hypName.HRef = LinkHelper.GetAdminLink("product", data.Id);

                Literal ltrName = e.Item.FindControl("ltrName") as Literal;
                hypName.Title = ltrName.Text = data.ProductCategoryDesc.Name;

                //Sub menu con              
                IList<PNK_ProductCategory> lstSub = lstAll.Where(m => m.ParentId == data.Id).ToList();
                if (lstSub.Count() > 0)
                {
                    Literal divIconSub = e.Item.FindControl("divIconSub") as Literal;
                    divIconSub.Text = "<span class=\"pull-right-container\" style=\"display: block\"><i class=\"fa fa-angle-left pull-right\"></i></span>";

                    HtmlControl divSub = e.Item.FindControl("divSub") as HtmlControl;
                    divSub.Visible = true;

                    Repeater rptResultSub = e.Item.FindControl("rptResultSub") as Repeater;
                    rptResultSub.DataSource = lstSub;
                    rptResultSub.DataBind();
                }
            }
        }

        protected void rptResultSub_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PNK_ProductCategory data = e.Item.DataItem as PNK_ProductCategory;

                HtmlAnchor hypName = e.Item.FindControl("hypName") as HtmlAnchor;
                hypName.HRef = LinkHelper.GetAdminLink("product", data.Id);

                Literal ltrName = e.Item.FindControl("ltrName") as Literal;
                hypName.Title = ltrName.Text = data.ProductCategoryDesc.Name;

                //Sub menu con              
                IList<PNK_ProductCategory> lstSub = lstAll.Where(m => m.ParentId == data.Id).ToList();
                if (lstSub.Count() > 0)
                {
                    Literal divIconSub1 = e.Item.FindControl("divIconSub1") as Literal;
                    divIconSub1.Text = "<span class=\"pull-right-container\" style=\"display: block\"><i class=\"fa fa-angle-left pull-right\"></i></span>";
                    
                    HtmlControl divSub1 = e.Item.FindControl("divSub1") as HtmlControl;
                    divSub1.Visible = true;

                    Repeater rptResultSub1 = e.Item.FindControl("rptResultSub1") as Repeater;
                    rptResultSub1.DataSource = lstSub;
                    rptResultSub1.DataBind();
                }
            }
        }

        protected void rptResultSub1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PNK_ProductCategory data = e.Item.DataItem as PNK_ProductCategory;

                HtmlAnchor hypName = e.Item.FindControl("hypName") as HtmlAnchor;
                hypName.HRef = LinkHelper.GetAdminLink("product", data.Id);

                Literal ltrName = e.Item.FindControl("ltrName") as Literal;
                hypName.Title = ltrName.Text = data.ProductCategoryDesc.Name;
            }
        }

    }
}