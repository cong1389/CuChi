using Cb.BLL;
using Cb.BLL.Product;
using Cb.DBUtility;
using Cb.Localization;
using Cb.Model;
using Cb.Model.Products;
using Cb.Utility;
using Cb.Web.Common;
using Cb.Web.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Cb.Web.Pages.CategoryManagement
{
    public partial class CategoryDetail : DGCUserControl
    {
        #region Parameter

        private string title, metaDescription, metaKeyword, h1, h2, h3, analytic, background;
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
            GetConfig();
            GetHowToBook();
            GetCustomizeTours();

        }

        private void GetDetail()
        {
            ProductBLL pcBll = new ProductBLL();
            IList<PNK_Product> lst = null;
            string level = string.Empty;

            if (Session["level"] != null)
            {
                switch (level)
                {
                    case "1":
                        lst = pcBll.GetList(LangInt, pageName, string.Empty, string.Empty, string.Empty, null, string.Empty, 1, 9999, out total);
                        break;
                    case "2":
                        lst = pcBll.GetList(LangInt, cid, string.Empty, string.Empty, string.Empty, null, string.Empty, 1, 9999, out total);
                        break;
                    case "3":
                        lst = pcBll.GetList(LangInt, cidsub, string.Empty, string.Empty, string.Empty, null, string.Empty, 1, 9999, out total);
                        break;
                    case "4":
                    default:
                        if (cid == LocalizationUtility.GetText("linkCate", Ci))
                        {
                            lst = pcBll.GetList(LangInt, pageName, string.Empty, string.Empty, id, null, string.Empty, 1, 9999, out total);
                        }
                        else if (cidsub == LocalizationUtility.GetText("linktmp", Ci))
                        {
                            lst = pcBll.GetList(LangInt, cid, string.Empty, string.Empty, id, null, string.Empty, 1, 9999, out total);
                        }
                        else
                        {
                            lst = pcBll.GetList(LangInt, cidsub, string.Empty, string.Empty, id, null, string.Empty, 1, 9999, out total);
                        }
                        break;
                }
            }

            if (total > 0)
            {
                Session["booking_categoryID"] = lst[0].CategoryId;

                string linkPage = Common.UtilityLocal.GetCateNameByLevel(pageName, cid, cidsub, id);
                //string rawUrl = Request.RawUrl;
                //if (LangInt == 1)
                //{
                //    string[] sep = { "vn" };
                //    string[] test = rawUrl.Split(sep, 0);
                //}
                hypBookingNow.HRef = LinkHelper.GetLink("booking", LangId, linkPage);

                ////set price                
                //block_booking booking = new block_booking();
                //booking.Title = lst[0].ProductDesc.Title;

                //Get img slide
                GetListImage(lst[0].Id.ToString());

                //set price                
                block_bookingprice.ProductId = DBConvert.ParseInt(lst[0].Id);

                //set map                
                //block_googlemap_detail.ProductId = lst[0].Id.ToString();

                //set programtour
                block_programtour.ProductId = lst[0].Id.ToString();
                //if (!string.IsNullOrWhiteSpace(lst[0].Latitude) || !string.IsNullOrWhiteSpace(lst[0].Longitude))
                //{
                //    //block_googlemap_detail.Visible = true;

                //    //block_googlemap_detail.Latitude = lst[0].Latitude;
                //    //block_googlemap_detail.Longitude = lst[0].Longitude;
                //    //block_googlemap_detail.CompanyName = lst[0].ProductDesc.Title;
                //}

                StringBuilder sbPrice = new StringBuilder();
                StringBuilder sbDiscountPercent = new StringBuilder();
                StringBuilder sbPriceFrom = new StringBuilder();

                //Ẩn các div k có giá trị bên trong
                if (string.IsNullOrWhiteSpace(lst[0].Status))
                {
                    string hidden = string.IsNullOrWhiteSpace(lst[0].Status) == true ? "display:none" : "display:block";
                    //divDuration.Attributes.Add("style", hidden);
                }
                if (string.IsNullOrWhiteSpace(lst[0].ProductDesc.Position))
                {
                    string hidden = string.IsNullOrWhiteSpace(lst[0].Status) == true ? "display:none" : "display:block";
                    //divTab.Attributes.Add("style", hidden);
                }

                //Nếu không có giá giảm
                if (string.IsNullOrWhiteSpace(lst[0].Post))
                {
                    sbPrice.Append("<div class=\"package-info\"><i class=\"icon-tag\"></i><span class=\"head\">Price: </span>USD " + lst[0].Website + "</div>");
                    string learnHidden = string.IsNullOrWhiteSpace(lst[0].Website) == true ? "hidden" : "";
                    sbDiscountPercent.Append("<div class=\"package-type normal-type " + learnHidden + " \">Learn More</div><div class=\"clear\"></div><div class=\"package-type-gimmick " + learnHidden + "\"></div><div class=\"clear\"></div>");
                    sbPriceFrom.Append("<div class=\"package-info last-minute\"><div class=\"package-info-inner\"><span class=\"discount-price\"><i class=\"icon-tag\"></i>Price from USD " + lst[0].Post + "</span></div></div>");

                }

                //Nếu có giá giảm
                else
                {
                    //if (string.IsNullOrWhiteSpace(lst[0].Website) && string.IsNullOrWhiteSpace(lst[0].Post))
                    //{
                        decimal discountPercent = ((DBConvert.ParseDecimal(lst[0].Website) - DBConvert.ParseDecimal(lst[0].Post)) / DBConvert.ParseDecimal(lst[0].Website)) * 100;
                        sbPrice.Append("<div class=\"package-info\"><i class=\"icon-tag\"></i><span class=\"head\">Price: </span><span class=\"normal-price\">USD " + lst[0].Website + "</span><span class=\"discount-text\">" + discountPercent + " Off</span><span class=\"separator\"> : </span><span class=\"discount-price\">USD " + lst[0].Post + "</span></div>");
                        sbDiscountPercent.Append("<div class=\"package-type last-minute\"><span class=\"head\">Last Minute</span><span class=\"discount-text\"> " + discountPercent + "  Off</span></div><div class=\"clear\"></div><div class=\"package-type-gimmick\"></div><div class=\"clear\"></div>");
                        sbPriceFrom.Append("<div class=\"package-info last-minute\"><div class=\"package-info-inner\"><span class=\"discount-price\"><i class=\"icon-tag fa-fw\"></i>Price from USD " + lst[0].Post + "</span></div></div>");
                    //}
                }
                
                ltrPriceFrom.Text = sbPriceFrom.ToString();

                ltrTourCode.Text = lst[0].Status;
                ltrLength.Text = lst[0].Area;
                ltrStartFrom.Text = lst[0].Bedroom;
                ltrTourType.Text = lst[0].Province;
                ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);

                //chi tiết
                ltrDetail.Text = lst[0].ProductDesc.Detail;

                //Detailed Itinerary
                ltrDetailedItinerary.Text = lst[0].ProductDesc.Utility;
                //Prices and Services
                ltrPriceServices.Text = lst[0].ProductDesc.Design;

                //View and print
                if (lst[0].Bathroom != "" && lst[0].Bathroom != string.Empty)
                {
                    divPdf.Style.Add("display", "block");
                    lbnViewFile.CommandArgument = lst[0].Bathroom;
                    PrintFile(lst[0].Bathroom);
                }

                //Video
                string embed = UtilityLocal.GetVideoList(lst[0].Id);
                if (embed != string.Empty)
                {
                    ifrVideo.Attributes.Add("src", "//www.youtube.com/embed/" + embed + "?rel=0&amp;autoplay=0");
                }
                else
                    ifrVideo.Visible = false;

                WebUtils.SeoPage(lst[0].ProductDesc.MetaTitle, lst[0].ProductDesc.Metadescription, lst[0].ProductDesc.MetaKeyword, this.Page);
                WebUtils.SeoTagH(lst[0].ProductDesc.H1, lst[0].ProductDesc.H2, lst[0].ProductDesc.H3, this.Controls);

                //Get Count review
                string oldCount = string.IsNullOrEmpty(lst[0].District) == true ? "0" : lst[0].District;
                ltrViewCount.Text = string.Format("{0} Reviews", oldCount);

                //Update count view after user forcus detail                
                int intOldCount = DBConvert.ParseInt(oldCount);
                int intNewCount = intOldCount + 1;

                Generic<PNK_Product> genericBLL = new Generic<PNK_Product>();
                PNK_Product productCat = new PNK_Product();
                productCat.Id = DBConvert.ParseInt(lst[0].Id);
                productCat = genericBLL.Load(productCat, new string[] { "Id" });
                productCat.Ordering = DBConvert.ParseInt(lst[0].Ordering);
                productCat.District = intNewCount.ToString();
                if (productCat.Ordering > 0)
                {
                    genericBLL.Update(productCat, productCat, new string[] { "Id" });
                }
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
                ltrHeaderCategory.Text = lst.Count > 0 ? Common.UtilityLocal.ImagePathByFont(lst[0]) : "";
            }
            else if (cidsub != string.Empty && cid != string.Empty)
            {
                lst = pcBll.GetList(LangInt, cid, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out  total);
                if (total > 0)
                {
                    //lần 2 truyền parentID vào để lấy ds con
                    lst = pcBll.GetList(LangInt, string.Empty, string.Empty, lst[0].Id, false, "p.ordering", 1, 1000, out  total);

                    //Gen html image category
                    ltrHeaderCategory.Text = total > 0 ? Common.UtilityLocal.ImagePathByFont(lst[0]) : "";
                }
            }
            else if (cidsub == string.Empty && cid != string.Empty)
            {
                lst = pcBll.GetList(LangInt, cid, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out  total);
                if (total > 0)
                {
                    //Gen html image category
                    //   ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);
                    ltrHeaderCategory.Text = total > 0 ? Common.UtilityLocal.ImagePathByFont(lst[0]) : "";

                }
            }
            else if (cid == string.Empty)
            {
                //lần đầu lấy parentID
                lst = pcBll.GetList(LangInt, pageName, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out  total);
                if (total > 0)
                {
                    //Gen html image category
                    // ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);
                    ltrHeaderCategory.Text = total > 0 ? Common.UtilityLocal.ImagePathByFont(lst[0]) : "";
                }
            }

            //Get top destination
            if (lst.Count > 0)
            {
                IList<PNK_ProductCategory> lstTop = pcBll.GetList(LangInt, string.Empty, "1", int.MinValue, true, "p.ordering", 1, 5, out  total);
                if (lstTop.Count > 0)
                {
                    lstTop = lstTop.Where(m => m.SmallImage == "1").ToList();
                    if (lstTop.Count > 0)
                    {
                        divTop.Attributes.Add("style", "block");
                        rptTopDetination.DataSource = lstTop;
                        rptTopDetination.DataBind();
                    }
                }
            }

            return string.Empty;
        }

        private void GetListImage(string productID)
        {
            UploadImageBLL bll = new UploadImageBLL();
            IList<PNK_UploadImage> lst = bll.GetList(string.Empty, productID, "1", 1, 100, out  total);
            if (total > 0)
            {
                rptResult.DataSource = lst;
                rptResult.DataBind();
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
                    if (LangInt == 1)
                    {
                        //if (item.Key_name == Constant.Configuration.config_address_vi)
                        //{
                        //    ltrAddressValue.Text = item.Value_name;
                        //}
                        if (item.Key_name == Constant.Configuration.phone)
                        {
                            ltrPhone.Text = ltrPhoneValue.Text = item.Value_name;
                        }
                        else if (item.Key_name == Constant.Configuration.email)
                        {
                            ltrEmail.Text = item.Value_name;
                            hypEmail.HRef = string.Format("mailto:{0}", item.Value_name);
                        }
                    }
                    else
                    {
                        if (item.Key_name == Constant.Configuration.phone)
                        {
                            ltrPhone.Text = ltrPhoneValue.Text = item.Value_name;
                        }
                        else if (item.Key_name == Constant.Configuration.email)
                        {
                            ltrEmail.Text = item.Value_name;
                            hypEmail.HRef = string.Format("mailto:{0}", item.Value_name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get count
        /// </summary>
        private void GetHowToBook()
        {
            ContentStaticBLL pcBll = new ContentStaticBLL();
            IList<PNK_ContentStatic> lst = pcBll.GetList(LangInt, string.Empty, ConfigurationManager.AppSettings["contentStatic_HowToBook"], string.Empty, 1, 1, out total);
            if (total > 0)
            {
                ltrHowToBook.Text = lst[0].ContentStaticDesc.Brief;
            }
        }

        /// <summary>
        /// Get count
        /// </summary>
        private void GetCustomizeTours()
        {
            ContentStaticBLL pcBll = new ContentStaticBLL();
            IList<PNK_ContentStatic> lst = pcBll.GetList(LangInt, string.Empty, ConfigurationManager.AppSettings["contentStatic_CustomTour"], string.Empty, 1, 1, out total);
            if (total > 0)
            {
                divCustomTour.Attributes.Add("style", "block");
                ltrCustomTours.Text = lst[0].ContentStaticDesc.Brief;
            }
        }

        private void PrintFile(string fileName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("function PrintFile() {");
            sb.AppendFormat("var printWin = window.open('{0}{1}/{2}', '', 'width=700,height=500,toolbar=0,menubar=0,location=1,status=1,scrollbars=1,resizable=1,left=0,top=0');", template_path, ConfigurationManager.AppSettings["ProductUpload"], fileName);
            sb.Append("printWin.focus();printWin.print();");
            sb.Append("}");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>" + sb.ToString() + "</script>");
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

        protected void lbnViewFile_Command(object sender, CommandEventArgs e)
        {
            UtilityLocal.ReadFile(e.CommandArgument.ToString().Trim(), Request, Response, this.Page);
        }

        protected void rptResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PNK_UploadImage data = e.Item.DataItem as PNK_UploadImage;

                HtmlImage img = e.Item.FindControl("img") as HtmlImage;
                HtmlImage imgThumb = e.Item.FindControl("imgThumb") as HtmlImage;

                imgThumb.Src = img.Src = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductUpload"], data.Name);

                img.Alt = img.Attributes["title"] = data.Name;
            }
        }

        protected void rptTopDetination_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PNK_ProductCategory data = e.Item.DataItem as PNK_ProductCategory;
                HtmlAnchor hypTitle = e.Item.FindControl("hypTitle") as HtmlAnchor;
                HtmlAnchor hypImg = e.Item.FindControl("hypImg") as HtmlAnchor;

                cid = Utils.GetParameter("cid", string.Empty);
                string link = UtilityLocal.GetPathTreeNameUrl(data.Id, LangInt, LangId);
                int level = link.Count(i => i.Equals('/'));
                if (level == 1)
                {
                    hypTitle.HRef = hypImg.HRef = LinkHelper.GetLink(data.ProductCategoryDesc.NameUrl, LangId);
                }
                else if (level == 2)
                {
                    hypTitle.HRef = hypImg.HRef = LinkHelper.GetLink(link, LocalizationUtility.GetText("linktmp", Ci), data.ProductCategoryDesc.NameUrl);
                }
                else if (level == 3)
                {
                    hypTitle.HRef = hypImg.HRef = LinkHelper.GetLink(link);
                }
                else if (level == 4)
                {
                    hypTitle.HRef = hypImg.HRef = LinkHelper.GetLink(link);
                }

                HtmlImage img = e.Item.FindControl("img") as HtmlImage;
                img.Src = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductCategoryUpload"], data.BaseImage);

                Literal ltrTitle = e.Item.FindControl("ltrTitle") as Literal;
                ltrTitle.Text = hypTitle.Title = hypImg.Title = img.Alt = img.Attributes["title"] = data.ProductCategoryDesc.Name;
            }
        }

        #endregion

    }
}