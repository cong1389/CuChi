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
using System.Text;

namespace Cb.Web.Controls
{
    public partial class footer : DGCUserControl
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
            GetMenu();

            GetCount();
            GetConfig();
            GetPartner();
            GetWhy();
        }

        /// <summary>
        /// Get count
        /// </summary>
        private void GetCount()
        {
            ContentStaticBLL pcBll = new ContentStaticBLL();
            IList<PNK_ContentStatic> lst = pcBll.GetList(LangInt, string.Empty, ConfigurationManager.AppSettings["contentStatic_Help"], string.Empty, 1, 1, out total);
            if (total > 0)
            {
                ltrAddressFooter.Text = lst[0].ContentStaticDesc.Brief;
            }
        }

        /// <summary>
        /// Get count
        /// </summary>
        private void GetWhy()
        {
            ContentStaticBLL pcBll = new ContentStaticBLL();
            IList<PNK_ContentStatic> lst = pcBll.GetList(LangInt, string.Empty, ConfigurationManager.AppSettings["contentStatic_Why"], string.Empty, 1, 1, out total);
            if (total > 0)
            {
                ltrWhy.Text = lst[0].ContentStaticDesc.Brief;
            }
        }

        private void GetConfig()
        {
            ConfigurationBLL pcBll = new ConfigurationBLL();
            IList<PNK_Configuration> lst = pcBll.GetList();
            if (lst != null && lst.Count > 0)
            {
                foreach (PNK_Configuration item in lst)
                {
                    if (item.Key_name == Constant.Configuration.config_logoHeader)
                    {
                        imgLogo.Src = imgLogo.Src = WebUtils.GetUrlImage(Constant.DSC.AdvUploadFolder, item.Value_name);
                        hypImgHomePage.HRef = WebUtils.RedirectHomePage();

                    }
                    if (LangInt == 1)
                    {
                        //if (item.Key_name == Constant.Configuration.config_address_vi)
                        //{
                        //    ltrAddressValue.Text = item.Value_name;
                        //}
                        //else if (item.Key_name == Constant.Configuration.phone)
                        //{
                        //    ltrPhoneValue.Text = item.Value_name;
                        //}
                        //else
                        //if (item.Key_name == Constant.Configuration.config_fbfanpage)
                        //{
                        //   // ltrFBFP.Text = item.Value_name;
                        //}
                        //else if (item.Key_name == Constant.Configuration.yahooid)
                        //{
                        //    ltrFooterContact.Text = item.Value_name;
                        //}
                        //else if (item.Key_name == Constant.Configuration.config_googleplus)
                        //{
                        //    hypGooglePlus.HRef = item.Value_name;
                        //}
                        //else 
                        if (item.Key_name == Constant.Configuration.config_footer)
                        {
                            ltrFooter.Text = item.Value_name;
                        }
                        //else if (item.Key_name == Constant.Configuration.email)
                        //{
                        //    ltrEmail.Text = item.Value_name;
                        //}
                    }
                    else
                    {
                        //if (item.Key_name == Constant.Configuration.config_address1_vi)
                        //{
                        //    ltrAddressValue.Text = item.Value_name;
                        //}
                        //else if (item.Key_name == Constant.Configuration.phone)
                        //{
                        //    ltrPhoneValue.Text = item.Value_name;
                        //}
                        //else 
                        //if (item.Key_name == Constant.Configuration.config_fbfanpage)
                        //{
                        //    ltrFBFP.Text = item.Value_name;
                        //}
                        //else if (item.Key_name == Constant.Configuration.yahooid)
                        //{
                        //    ltrFooterContact.Text = item.Value_name;
                        //}
                        //else if (item.Key_name == Constant.Configuration.config_googleplus)
                        //{
                        //    hypGooglePlus.HRef = item.Value_name;
                        //}
                        //else
                        if (item.Key_name == Constant.Configuration.config_footer)
                        {
                            ltrFooter.Text = item.Value_name;
                        }
                        //else if (item.Key_name == Constant.Configuration.email)
                        //{
                        //    ltrEmail.Text = item.Value_name;
                        //}
                    }
                }
            }
        }

        private void GetMenu()
        {
            ProductCategoryBLL pcBll = new ProductCategoryBLL();
            lstAll = pcBll.GetList(LangInt, string.Empty, "1", int.MinValue, true, "p.ordering", 1, 1000, out total);

            if (total > 0)
            {
                lstParent = lstAll.Where(m => m.ParentId == 0                    
                    && m.Id != DBConvert.ParseInt(ConfigurationManager.AppSettings["parentIdHome"])
                    && m.ThumbnailImage == "1"
                    ).ToList();
                if (lstParent.Count() > 0)
                {
                    rptResult.DataSource = lstParent;
                    rptResult.DataBind();
                }
            }
        }

        private void GetSubMenu(string categoryId, Repeater rptSub)
        {
            ProductCategoryBLL pcBll = new ProductCategoryBLL();
            IList<PNK_ProductCategory> lst = pcBll.GetList(LangInt, string.Empty, "1", int.Parse(categoryId), false, "p.ordering", 1, 9999, out total);
            lst = lst.Where(m => m.ThumbnailImage == "1").ToList();
            if (lst.Count > 0)
            {
                rptSub.DataSource = lst;
                rptSub.DataBind();
            }
        }

        private void GetPartner()
        {
            BannerBLL bannerBLL = new BannerBLL();
            IList<PNK_Banner> lst = bannerBLL.GetList(DBConvert.ParseInt(ConfigurationManager.AppSettings["partnerId"]), string.Empty, "1", 1, 100, out total);
            if (total > 0)
            {
                this.rptPartner.DataSource = lst;
                this.rptPartner.DataBind();
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
                PNK_ProductCategory data = e.Item.DataItem as PNK_ProductCategory;

                string link = UtilityLocal.GetPathTreeNameUrl(data.Id, LangInt, LangId);
                HtmlAnchor hypName = e.Item.FindControl("hypName") as HtmlAnchor;
                hypName.HRef = LinkHelper.GetLink(link);

                Literal ltrName = e.Item.FindControl("ltrName") as Literal;
                hypName.Title = hypName.Title = ltrName.Text = data.ProductCategoryDesc.Name;

                Repeater rptSub = e.Item.FindControl("rptSub") as Repeater;
                GetSubMenu(data.Id.ToString(), rptSub);
                hddParentNameUrlFooter.Value = data.ProductCategoryDesc.NameUrl;

            }
        }

        protected void rptSub_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PNK_ProductCategory data = e.Item.DataItem as PNK_ProductCategory;

                string link = UtilityLocal.GetPathTreeNameUrl(data.Id, LangInt, LangId);
                HtmlAnchor hypName = e.Item.FindControl("hypName") as HtmlAnchor;
                hypName.HRef = LinkHelper.GetLink(link);

                hddParentNameUrlFooter.Value = data.ProductCategoryDesc.NameUrl;

                Literal ltrName = e.Item.FindControl("ltrName") as Literal;
                hypName.Title = ltrName.Text = data.ProductCategoryDesc.Name;

                //Sub menu con
                Repeater rptSub1 = e.Item.FindControl("rptSub1") as Repeater;
                GetSubMenu(data.Id.ToString(), rptSub1);
            }
        }

        protected void rptSub1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PNK_ProductCategory data = e.Item.DataItem as PNK_ProductCategory;

                string link = UtilityLocal.GetPathTreeNameUrl(data.Id, LangInt, LangId);
                HtmlAnchor hypName = e.Item.FindControl("hypName") as HtmlAnchor;
                hypName.HRef = LinkHelper.GetLink(link);

                Literal ltrName = e.Item.FindControl("ltrName") as Literal;
                hypName.Title = hypName.Title = ltrName.Text = data.ProductCategoryDesc.Name;

            }
        }

        protected void rptPartner_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PNK_Banner data = e.Item.DataItem as PNK_Banner;

                HtmlImage img = e.Item.FindControl("img") as HtmlImage;
                img.Src = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["SliderUpload"], data.Image);

                if (data.OutPage == 1)
                {
                    HtmlAnchor hypImg = e.Item.FindControl("hypImg") as HtmlAnchor;
                    hypImg.HRef = data.LinkUrl;
                }
            }
        }

        #endregion
    }
}