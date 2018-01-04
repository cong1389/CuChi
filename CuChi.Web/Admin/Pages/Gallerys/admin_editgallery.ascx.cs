// =============================================
// Author:		Congtt
// Create date: 22/09/2014
// Description:	Edit danh sach sản phẩm
//    Cột Area lưu file name PDF
//    Cột Hot lưu sach hay nhat
//    Cột Feature lưu sach nổi bật
//    Cột Design lưu tagName
//    Cột Utility lưu tagUrl
//    Cột Bathroom lưu hiển thị sản phẩm trang chủ
// =============================================

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Cb.DBUtility;
using Cb.Model;
using Cb.BLL;
using Cb.Localization;
using System.IO;
using Cb.Model.Products;
using Cb.BLL.Product;
using System.Net;
using Cb.Utility;

namespace Cb.Web.Admin.Pages.Gallerys
{
    public partial class admin_editgallery : DGCUserControl
    {
        #region Parameter

        private Generic<PNK_Product> genericBLL;
        private Generic<PNK_ProductDesc> genericDescBLL;
        private Generic2C<PNK_Product, PNK_ProductDesc> generic2CBLL;

        protected int gallerycategoryId = int.MinValue;
        string galleryCategoryName = string.Empty;
        protected string template_path;

        private string getLinkByPageName
        {
            get
            {
                if (ViewState["getLinkByPageName"] != null)
                    return ViewState["getLinkByPageName"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["getLinkByPageName"] = value;
            }
        }

        private string filenameUpload
        {
            get
            {
                if (ViewState["filenameUpload"] != null)
                    return ViewState["filenameUpload"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["filenameUpload"] = value;
            }
        }

        private string categoryId
        {
            get
            {
                if (ViewState["categoryId"] != null)
                    return ViewState["categoryId"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["categoryId"] = value;
            }
        }

        private string id
        {
            get
            {
                if (ViewState["id"] != null)
                    return ViewState["id"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["id"] = value;
            }
        }

        #endregion

        #region Common

        /// <summary>
        /// Init page
        /// </summary>
        private void InitPage()
        {
            LocalizationUtility.SetValueControl(this);
            ltrPageDetail.Text = LocalizationUtility.GetText(ltrPageDetail.ID);

            //swich page
            switch (Utils.GetParameter("page", "home"))
            {
                case Constant.UI.edit_picture_page:
                    getLinkByPageName = Constant.UI.picture_page;
                    break;
                case Constant.UI.edit_video_page:
                    getLinkByPageName = Constant.UI.video_page;
                    break;
            }

            this.ltrAdminApply.Text = Constant.UI.admin_apply;
            this.ltrAdminCancel.Text = Constant.UI.admin_cancel;
            this.ltrAdminDelete.Text = Constant.UI.admin_delete;
            this.ltrAdminSave.Text = Constant.UI.admin_save;
            //this.ltrAminName.Text = Constant.UI.admin_name;
            this.ltrAminLangVi.Text = Constant.UI.admin_lang_Vi;
            this.ltrAminLangEn.Text = Constant.UI.admin_lang_En;
            //this.ltrAminName_En.Text = Constant.UI.admin_name_en;

            reqv_txtNameVi.Text = Constant.UI.alert_empty_name_outsite;
            reqv_txtNameVi.ErrorMessage = Constant.UI.alert_empty_name;

            //load category
            GetDataDropDownCategory(this.drpCategory);

            BindCost();
            SetRoleMenu();
        }

        /// <summary>
        /// GetId
        /// </summary>
        private void GetId()
        {
            txtToDate.Text = DateTime.Today.ToString();

            #region Set thuoc tinh cho block_baseimage

            block_baseimage.ImagePath = block_uploadimage.ImagePath = ConfigurationManager.AppSettings["ProductUpload"];
            block_baseimage.MinWidth = ConfigurationManager.AppSettings["minWidthItem"];
            block_baseimage.MinHeigh = ConfigurationManager.AppSettings["minHeightItem"];
            block_baseimage.MaxWidth = ConfigurationManager.AppSettings["maxWidthItem"];
            block_baseimage.MaxHeight = ConfigurationManager.AppSettings["maxHeightItem"];
            block_baseimage.MaxWidthBox = ConfigurationManager.AppSettings["maxWidthBoxItem"];
            block_baseimage.MaxHeightBox = ConfigurationManager.AppSettings["maxHeightBoxItem"];

            block_uploadimage.CategoryId = categoryId.ToString();
            block_uploadimage.Id = id;

            #endregion

            genericBLL = new Generic<PNK_Product>();
            generic2CBLL = new Generic2C<PNK_Product, PNK_ProductDesc>();
            genericDescBLL = new Generic<PNK_ProductDesc>();
            categoryId = Utils.GetParameter("cid", string.Empty);
            id = Utils.GetParameter("id", string.Empty);
            gallerycategoryId = id == string.Empty ? int.MinValue : DBConvert.ParseInt(id);
            template_path = WebUtils.GetWebPath();

            //Show tab upload file khi đã cập nhật vào bảng
            if (gallerycategoryId == int.MinValue)
            {
                block_bookingprice.Visible = false;
                ucMessageEmty9.MessageContent = LocalizationUtility.GetText("MessageEmty");

                divProgramTour.Visible = false;
                ucMessageEmty8.MessageContent = LocalizationUtility.GetText("MessageEmty");

                block_mapsmulti.Visible = false;
                ucMessageEmty7.MessageContent = LocalizationUtility.GetText("MessageEmty");

                block_uploadfile.Visible = false;
                ucMessageEmty6.MessageContent = LocalizationUtility.GetText("MessageEmty");

                block_uploadvideo.Visible = false;
                ucMessageEmty5.MessageContent = LocalizationUtility.GetText("MessageEmty");

                block_uploadimage.Visible = false;
                ucMessageEmty4.MessageContent = LocalizationUtility.GetText("MessageEmty");

                block_baseimage.Visible = false;
                ucMessageEmty3.MessageContent = LocalizationUtility.GetText("MessageEmty");
            }

            //Set link chương trình tourr
            hypProgramTour.HRef = string.Format("/adm/programtour/{0}", id);

            block_bookingprice.ProductId = id;
        }

        /// <summary>
        /// Xem PDF file
        /// </summary>
        /// <param name="fileName"></param>
        private void ViewPdf(string fileName)
        {
            //string path = Request.PhysicalApplicationPath;            
            //string url = Path.Combine(ConfigurationManager.AppSettings["ProductUpload"], fileName).Replace("/","\\");
            //url = Utils.CombineUrl(path, url);

            string url = string.Format("{0}/{1}", ConfigurationManager.AppSettings["ProductUpload"], fileName);
            url = Utils.CombineUrl(WebUtils.GetBaseUrl(), url);

            WebClient wc = new WebClient();
            Byte[] buffer = wc.DownloadData(url);
            if (buffer != null)
            {
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(buffer);
                Response.Flush();
                Response.End();
            }
        }

        /// <summary>
        /// UploadMp3
        /// </summary>
        /// <param name="fu"></param>
        /// <returns></returns>
        private string UploadMp3(FileUpload fu)
        {
            //File giữ nguyen định dạng
            string fileNameMp3 = string.Format("{0}{1}.{2}", WebUtils.GetFileName(fu.PostedFile.FileName.Split('.')[0]), DateTime.Now.ToString("ddMMyyyyhhmmss"), WebUtils.GetFileExtension(fu.FileName));
            string pathNameMp3 = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["ProductUpload"]), fileNameMp3);
            string fileNameOgg = pathNameMp3.Replace("mp3", "ogg").Replace("MP3", "ogg");
            //if (File.Exists(fileNameMp3))
            //{
            fu.SaveAs(pathNameMp3);
            fu.SaveAs(fileNameOgg);
            //}
            return fileNameMp3;
        }

        /// <summary>
        /// ViewMusic
        /// </summary>
        /// <param name="fu"></param>
        /// <returns></returns>
        private void ViewMusic(string fileName)
        {
            string path = Request.PhysicalApplicationPath;
            string url = string.Format("{0}/{1}", ConfigurationManager.AppSettings["ProductUpload"], fileName);
            url = Utils.CombineUrl(path, url);
            WebClient wc = new WebClient();
            Byte[] buffer = wc.DownloadData(url);
            if (buffer != null)
            {
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(buffer);
                Response.Flush();
                Response.End();
            }
        }

        /// <summary>
        /// Phân quyền tài khoản Congtt full quyền, những tk còn lại k có quyền xóa và edit
        /// </summary>
        private void SetRoleMenu()
        {
            PNK_User lst_user = (PNK_User)Session[Global.SESS_USER];
            if (lst_user.Username != "congtt")
            {
                divPage.Attributes.Add("class", "hidden");
            }
        }

        /// <summary>
        /// ShowProduct
        /// </summary>
        private void ShowProduct()
        {
            if (this.gallerycategoryId != int.MinValue)
            {
                PNK_Product gallerycatObj = new PNK_Product();
                string[] fields = { "Id" };
                gallerycatObj.Id = this.gallerycategoryId;
                gallerycatObj = generic2CBLL.Load(gallerycatObj, fields, Constant.DB.LangId);
                chkPublished.Checked = gallerycatObj.Published == "1" ? true : false;
                chkPublishedHot.Checked = gallerycatObj.Hot == "1" ? true : false;
                chkPublishedFeature.Checked = gallerycatObj.Feature == "1" ? true : false;
                // chkNewInHome.Checked = gallerycatObj.Bathroom == "1" ? true : false;

                txtBedRoom.Value = DBConvert.ParseString(gallerycatObj.Bedroom);

                txtToDate.Text = gallerycatObj.Code;
                txtStatus.Value = gallerycatObj.Status;
                try
                {
                    this.drpProvince.SelectedValue = gallerycatObj.Province == string.Empty ? "" : drpProvince.Items.FindByText(gallerycatObj.Province).Value;
                    //this.drpDistrict.SelectedValue = gallerycatObj.District == string.Empty ? "" : drpDistrict.Items.FindByText(gallerycatObj.District).Value;
                }
                catch (Exception)
                {
                    drpProvince.SelectedIndex = 0;
                }
                this.drpCategory.SelectedValue = gallerycatObj.CategoryId.ToString();

                chkProjectNew.Checked = gallerycatObj.Price == "1" ? true : false;

                drpCost.SelectedValue = DBConvert.ParseString(gallerycatObj.Cost);
                txtWebsite.Text = gallerycatObj.Website;
                txtPost.Text = gallerycatObj.Post;

                txtLatitude.Value = gallerycatObj.Latitude;
                txtLongitude.Value = gallerycatObj.Longitude;
                txtPage.Text = gallerycatObj.Page == "" ? ConfigurationManager.AppSettings["pagePathProductDetail"] : gallerycatObj.Page;

                #region Set image

                block_baseimage.ImageName = gallerycatObj.Image;

                if (gallerycatObj.ImageType == 1 || gallerycatObj.ImageType == null)
                {
                    HtmlControl rdImage = block_baseimage.FindControl("rdImage") as HtmlControl;
                    rdImage.Attributes["checked"] = "checked";
                }
                if (gallerycatObj.ImageType == 2)
                {
                    HtmlControl txtFontName = block_baseimage.FindControl("txtFontName") as HtmlControl;
                    txtFontName.Attributes["value"] = gallerycatObj.ImageFont;

                    HtmlControl rdImageFont = block_baseimage.FindControl("rdImageFont") as HtmlControl;
                    rdImageFont.Attributes["checked"] = "checked";
                }

                try
                {
                    cboArea.SelectedValue = gallerycatObj.Area == string.Empty ? "1" : cboArea.Items.FindByText(gallerycatObj.Area).Value;
                }
                catch (Exception)
                {
                    cboArea.SelectedIndex = 0;
                }
                //cboArea.Items.FindByText(gallerycatObj.Area).Selected = true;

                #endregion

                block_uploadfile.ImageName = gallerycatObj.Bathroom;

                IList<PNK_ProductDesc> lst = genericDescBLL.GetAllBy(new PNK_ProductDesc(), string.Format(" where mainid = {0}", this.gallerycategoryId), null);
                foreach (PNK_ProductDesc item in lst)
                {
                    switch (item.LangId)
                    {
                        case 1:
                            this.txtName.Value = item.Title;
                            this.txtIntro.Text = item.Brief;
                            this.txtDetailVi.Text = Server.HtmlDecode(item.Detail);
                            this.txtPositionVi.Text = item.Position;
                            this.txtUtilityVi.Text = item.Utility;
                            this.txtPicturesVi.Text = item.Pictures;
                            this.txtDesignVi.Text = item.Design;
                            this.txtPaymentVi.Text = item.Payment;
                            this.txtContactVi.Text = item.Contact;
                            this.txtMetaTitle.Text = item.MetaTitle;
                            this.txtMetaDescription.Text = item.Metadescription;
                            this.txtMetaKeyword.Text = item.MetaKeyword;
                            this.txtH1.Text = item.H1;
                            this.txtH2.Text = item.H2;
                            this.txtH3.Text = item.H3;
                            break;
                        case 2:
                            this.txtNameEng.Value = item.Title;
                            this.txtIntroEng.Text = item.Brief;
                            this.txtDetailEng.Text = item.Detail;
                            this.txtUtilityEng.Text = item.Utility;
                            this.txtPicturesEng.Text = item.Pictures;
                            this.txtDesignEng.Text = item.Design;
                            this.txtPaymentEng.Text = item.Payment;
                            this.txtContactEng.Text = item.Contact;
                            this.txtMetaTitleEng.Text = item.MetaTitle;
                            this.txtMetaTitleEng.Text = item.Metadescription;
                            this.txtMetaTitleEng.Text = item.MetaKeyword;
                            this.txtMetaTitleEng.Text = item.MetaKeyword;
                            this.txtH1Eng.Text = item.H1;
                            this.txtH2Eng.Text = item.H2;
                            this.txtH3Eng.Text = item.H3;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// BindCost
        /// </summary>
        private void BindCost()
        {
            drpCost.Items.Clear();
            drpCost.Items.Add(new ListItem(LocalizationUtility.GetText("strSelAItem"), string.Empty));
            string full;
            Type t = typeof(enuCostId);
            Array arr = Enum.GetValues(t);
            foreach (enuCostId enu in arr)
            {
                if (enu.ToString("d") != int.MinValue.ToString())
                {
                    full = string.Format("{0}_{1}", t.Name, enu.ToString());
                    drpCost.Items.Add(new ListItem(LocalizationUtility.GetText(full), enu.ToString("d")));
                }
            }
            drpCost.SelectedIndex = 1;
        }

        /// <summary>
        /// get data for insert update
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        private PNK_Product GetDataObjectParent(PNK_Product gallerycatObj)
        {
            try
            {
                gallerycatObj.Published = chkPublished.Checked ? "1" : "0";
                gallerycatObj.Hot = chkPublishedHot.Checked ? "1" : "0";
                gallerycatObj.Feature = chkPublishedFeature.Checked ? "1" : "0";
                gallerycatObj.Price = chkProjectNew.Checked ? "1" : "0";

                //File upload
                //   gallerycatObj.Bathroom = fileUpload1.FileName;

                gallerycatObj.Cost = drpCost.SelectedValue;
                //gallerycatObj.District = drpDistrict.SelectedItem == null ? string.Empty : drpDistrict.SelectedItem.Text;
                gallerycatObj.Bedroom = txtBedRoom.Value;

                //gallerycatObj.Code = txtToDate.Text;
                gallerycatObj.Area = cboArea.SelectedItem.Text;

                //nguyên giá
                gallerycatObj.Website = txtWebsite.Text;
                //giá khuyến mãi
                gallerycatObj.Post = txtPost.Text;
                //Điểm đến
                gallerycatObj.Status = txtStatus.Value;

                gallerycatObj.Province = drpProvince.SelectedItem == null ? string.Empty : drpProvince.SelectedItem.Text;
                gallerycatObj.UpdateDate = DateTime.Now;
                gallerycatObj.CategoryId = DBConvert.ParseInt(drpCategory.SelectedValue);


                gallerycatObj.Longitude = txtLongitude.Value;//Kinh do   
                gallerycatObj.Latitude = txtLatitude.Value;
                gallerycatObj.Page = txtPage.Text.Trim();

                //update by
                if (Session[Global.SESS_USER] != null)
                {
                    PNK_User user = (PNK_User)Session[Global.SESS_USER];
                    gallerycatObj.UpdateBy = user.Username;
                }

                #region Get image

                HtmlControl txtFontName = block_baseimage.FindControl("txtFontName") as HtmlControl;
                gallerycatObj.ImageFont = string.IsNullOrEmpty(txtFontName.Attributes["value"]) == true ? string.Empty : txtFontName.Attributes["value"];

                HtmlControl rdImageFont = block_baseimage.FindControl("rdImageFont") as HtmlControl;
                if (rdImageFont != null && rdImageFont.Attributes["checked"] == "checked")
                    gallerycatObj.ImageType = DBConvert.ParseInt(rdImageFont.Attributes["value"]);
                else
                    gallerycatObj.ImageType = 1;

                HtmlControl hddImageName = block_baseimage.FindControl("hddImageName") as HtmlControl;
                if (hddImageName != null && hddImageName.Attributes["value"] != null)
                {
                    gallerycatObj.Image = hddImageName.Attributes["value"].ToString();
                }
                else
                {
                    gallerycatObj.Image = "";
                }

                #endregion

                HtmlControl hddNameFileUpload = block_uploadfile.FindControl("hddNameFileUpload") as HtmlControl;
                if (hddNameFileUpload != null && hddNameFileUpload.Attributes["value"] != null)
                {
                    gallerycatObj.Bathroom = hddNameFileUpload.Attributes["value"].ToString();
                }
                else
                {
                    gallerycatObj.Bathroom = "";
                }

            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("GetDataObjectParent", "admin_editgallery", ex.Message);
            }

            return gallerycatObj;
        }

        /// <summary>
        /// get data child for insert update
        /// </summary>
        /// <param name="contdescObj"></param>
        /// <returns></returns>
        private PNK_ProductDesc GetDataObjectChild(PNK_ProductDesc gallerycatdescObj, int lang)
        {
            switch (lang)
            {
                case 1:
                    gallerycatdescObj.MainId = this.gallerycategoryId;
                    gallerycatdescObj.LangId = Constant.DB.LangId;
                    gallerycatdescObj.Title = SanitizeHtml.Sanitize(txtName.Value);
                    gallerycatdescObj.TitleUrl = Utils.RemoveUnicode(SanitizeHtml.Sanitize(txtName.Value));
                    gallerycatdescObj.Brief = txtIntro.Text;
                    gallerycatdescObj.Detail = txtDetailVi.Text;
                    gallerycatdescObj.Position = txtPositionVi.Text;
                    gallerycatdescObj.Utility = txtUtilityVi.Text;
                    gallerycatdescObj.Pictures = txtPicturesVi.Text;
                    gallerycatdescObj.Design = txtDesignVi.Text;
                    gallerycatdescObj.Payment = txtPaymentVi.Text;
                    gallerycatdescObj.Contact = txtContactVi.Text;
                    gallerycatdescObj.MetaTitle = txtMetaTitle.Text;
                    gallerycatdescObj.Metadescription = txtMetaDescription.Text;
                    gallerycatdescObj.MetaKeyword = txtMetaKeyword.Text;
                    gallerycatdescObj.H1 = txtH1.Text;
                    gallerycatdescObj.H2 = txtH2.Text;
                    gallerycatdescObj.H3 = txtH3.Text;
                    break;
                case 2:
                    gallerycatdescObj.MainId = this.gallerycategoryId;
                    gallerycatdescObj.LangId = Constant.DB.LangId_En;
                    string title = string.IsNullOrEmpty(txtNameEng.Value) ? SanitizeHtml.Sanitize(txtName.Value) : SanitizeHtml.Sanitize(txtNameEng.Value);
                    gallerycatdescObj.Title = title;
                    gallerycatdescObj.TitleUrl = Utils.RemoveUnicode(title);
                    gallerycatdescObj.Brief = string.IsNullOrEmpty(txtIntroEng.Text) ? txtIntro.Text : txtIntroEng.Text;
                    gallerycatdescObj.Detail = string.IsNullOrEmpty(txtDetailVi.Text) ? txtDetailVi.Text : txtDetailEng.Text;

                    gallerycatdescObj.Position = string.IsNullOrEmpty(txtPositionEng.Text) ? txtPositionVi.Text : txtPositionEng.Text;

                    gallerycatdescObj.Design = string.IsNullOrEmpty(txtDesignEng.Text) ? txtDesignVi.Text : txtDesignEng.Text;
                    gallerycatdescObj.Pictures = string.IsNullOrEmpty(txtPicturesEng.Text) ? txtPicturesVi.Text : txtPicturesEng.Text;
                    gallerycatdescObj.Payment = string.IsNullOrEmpty(txtPaymentEng.Text) ? txtPaymentVi.Text : txtPaymentEng.Text;
                    gallerycatdescObj.Contact = string.IsNullOrEmpty(txtContactEng.Text) ? txtContactVi.Text : txtContactEng.Text;

                    gallerycatdescObj.MetaTitle = string.IsNullOrEmpty(txtMetaTitleEng.Text) ? txtMetaTitle.Text : txtMetaTitleEng.Text;
                    gallerycatdescObj.Metadescription = string.IsNullOrEmpty(txtMetaDescriptionEng.Text) ? txtMetaDescription.Text : txtMetaDescriptionEng.Text;
                    gallerycatdescObj.MetaKeyword = string.IsNullOrEmpty(txtMetaKeywordEng.Text) ? txtMetaKeyword.Text : txtMetaKeywordEng.Text;
                    gallerycatdescObj.H1 = string.IsNullOrEmpty(txtH1Eng.Text) ? txtH1.Text : txtH1Eng.Text;
                    gallerycatdescObj.H2 = string.IsNullOrEmpty(txtH2Eng.Text) ? txtH2.Text : txtH2Eng.Text;
                    gallerycatdescObj.H3 = string.IsNullOrEmpty(txtH3Eng.Text) ? txtH3.Text : txtH3Eng.Text;
                    break;
            }
            return gallerycatdescObj;
        }

        /// <summary>
        /// Save location
        /// </summary>
        private void SaveProduct()
        {
            //Xoá cache trước khi lưu
            CacheHelper.ClearAll();

            PNK_Product gallerycatObj = new PNK_Product();
            PNK_ProductDesc gallerycatObjVn = new PNK_ProductDesc();
            PNK_ProductDesc gallerycatObjEn = new PNK_ProductDesc();
            if (this.gallerycategoryId == int.MinValue)
            {
                //get data insert
                gallerycatObj = this.GetDataObjectParent(gallerycatObj);
                gallerycatObj.PostDate = DateTime.Now;
                gallerycatObj.Ordering = genericBLL.getOrdering();
                gallerycatObjVn = this.GetDataObjectChild(gallerycatObjVn, Constant.DB.LangId);
                gallerycatObjEn = this.GetDataObjectChild(gallerycatObjEn, Constant.DB.LangId_En);

                List<PNK_ProductDesc> lst = new List<PNK_ProductDesc>();
                lst.Add(gallerycatObjVn);
                lst.Add(gallerycatObjEn);
                //excute
                this.gallerycategoryId = generic2CBLL.Insert(gallerycatObj, lst);
            }
            else
            {
                string[] fields = { "Id" };
                gallerycatObj.Id = this.gallerycategoryId;
                gallerycatObj = genericBLL.Load(gallerycatObj, fields);

                //get data update
                gallerycatObj = this.GetDataObjectParent(gallerycatObj);
                gallerycatObjVn = this.GetDataObjectChild(gallerycatObjVn, Constant.DB.LangId);
                gallerycatObjEn = this.GetDataObjectChild(gallerycatObjEn, Constant.DB.LangId_En);
                List<PNK_ProductDesc> lst = new List<PNK_ProductDesc>();
                lst.Add(gallerycatObjVn);
                lst.Add(gallerycatObjEn);
                //excute
                generic2CBLL.Update(gallerycatObj, lst, fields);
                //neu ve Published oo thay doi thi chay ham ChangeWithTransaction de doi Published cac con va cac gallery
                //if (publisheddOld != gallerycatObj.Published)
                //    PNK_Product.ChangeWithTransaction(DBConvert.ParseString(this.gallerycategoryId), gallerycatObj.Published);
            }

        }

        /// <summary>
        /// Delete Image In Folder
        /// </summary>
        private bool DeleteImage()
        {
            bool result = false;
            if (!string.IsNullOrEmpty(filenameUpload))
            {
                string f = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["ProductUpload"]), filenameUpload);
                if (File.Exists(f))
                {
                    try
                    {
                        File.Delete(f);
                        filenameUpload = null;
                        SetVisibleImg(true, string.Empty);
                        result = true;
                    }
                    catch
                    {
                        result = false;
                    }
                }
                else
                {
                    filenameUpload = null;
                    SetVisibleImg(true, string.Empty);
                }
            }
            return result;
        }

        /// <summary>
        /// Delete image in folder and database
        /// </summary>
        /// <param name="cid"></param>
        private void DeleteProduct(string cid)
        {
            //swich page
            switch (Utils.GetParameter("page", "home"))
            {
                case Constant.UI.edit_picture_page:
                    getLinkByPageName = Constant.UI.picture_page;
                    break;
                case Constant.UI.edit_video_page:
                    getLinkByPageName = Constant.UI.video_page;
                    break;
            }

            //Xoá cache trước khi lưu
            CacheHelper.ClearAll();

            string link, url;

            if (generic2CBLL.Delete(cid) && DeleteImage())
                link = LinkHelper.GetAdminLink(getLinkByPageName, categoryId, "delete");
            else
                link = LinkHelper.GetAdminLink(getLinkByPageName, categoryId, "delfail");
            url = Utils.CombineUrl(template_path, link);
            Response.Redirect(url);
        }

        /// <summary>
        /// Cancel content
        /// </summary>
        private void CancelProduct()
        {
            //swich page
            switch (Utils.GetParameter("page", "home"))
            {
                case Constant.UI.edit_picture_page:
                    getLinkByPageName = Constant.UI.picture_page;
                    break;
                case Constant.UI.edit_video_page:
                    getLinkByPageName = Constant.UI.video_page;
                    break;
            }

            string url = LinkHelper.GetAdminLink(getLinkByPageName);
            Response.Redirect(url);
        }

        /// <summary>
        /// getDataDropDownCategory
        /// </summary>
        /// <param name="_drp"></param>
        public static void GetDataDropDownCategory(DropDownList _drp)
        {
            int totalrow;
            string strTemp;
            _drp.Items.Clear();
            _drp.Items.Add(new ListItem(Constant.UI.admin_Category, Constant.DSC.IdRootProductCategory.ToString()));
            ProductCategoryBLL ncBll = new ProductCategoryBLL();
            IList<PNK_ProductCategory> lst = ncBll.GetList(Constant.DB.LangId, string.Empty, 1, 300, out totalrow);

            //Loại bỏ những galleryCategory thuộc nhóm Gallery có Id=64
            lst = lst.Where(x => x.ParentId == DBConvert.ParseInt(ConfigurationManager.AppSettings["parentIdContact"])
            || x.ProductCategoryDesc.Id == DBConvert.ParseInt(ConfigurationManager.AppSettings["parentIdContact"])).ToList();

            if (lst != null && lst.Count > 0)
            {
                foreach (PNK_ProductCategory item in lst)
                {
                    strTemp = Utils.GetScmplit(item.ProductCategoryDesc.Name, item.PathTree);
                    _drp.Items.Add(new ListItem(strTemp, DBConvert.ParseString(item.Id)));
                }
            }
            //_drp.SelectedIndex = _drp.Items.IndexOf(_drp.Items.FindByValue(ConfigurationManager.AppSettings["parentIdLeture"]));
        }

        /// <summary>
        ///Hien thi o upload hinh anh( true: chua upload hinh) 
        /// </summary>
        /// <param name="isShowUplImg"></param>
        /// <param name="filename"></param>
        private void SetVisibleImg(bool isShowUplImg, string filename)
        {
            //if (isShowUplImg)
            //{
            //    fuImage.Visible = btnUploadImage.Visible = true;
            //    lbnView.Visible = lbnDelete.Visible = false;
            //}
            //else
            //{
            //    fuImage.Visible = btnUploadImage.Visible = false;
            //    //lbnView.Attributes["href"] = filename;
            //    lbnView.Visible = lbnDelete.Visible = true;
            //}
        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(admin_txtnews), this.Page);
            btn_Delete.Attributes["onclick"] = string.Format("javascript:return confirm('{0}');", Constant.UI.admin_msg_confirm_delete_item);
            GetId();
            if (!IsPostBack)
            {
                InitPage();
                ShowProduct();
            }
        }

        /// <summary>
        /// btnSave_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveProduct();

                //swich page
                switch (Utils.GetParameter("page", "home"))
                {
                    case Constant.UI.edit_picture_page:
                        getLinkByPageName = Constant.UI.picture_page;
                        break;
                    case Constant.UI.edit_video_page:
                        getLinkByPageName = Constant.UI.video_page;
                        break;
                }

                string url = LinkHelper.GetAdminMsgLink(getLinkByPageName, categoryId, "save");
                Response.Redirect(url);
            }
        }

        /// <summary>
        /// btnApply_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveProduct();
                //string url = LinkHelper.GetAdminLink("edit_gallery", categoryId, gallerycategoryId.ToString());
                //Response.Redirect(url);
            }
        }

        /// <summary>
        /// btnDelete_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteProduct(DBConvert.ParseString(this.gallerycategoryId));
        }

        /// <summary>
        /// btnCancel_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CancelProduct();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alert"></param>
        private void Alert(string alert)
        {
            string script = string.Format("alert('{0}')", alert);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertgallerycategory", script, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void csv_drpCategory_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //args.IsValid = !CheckParentIsThisOrChild();
            //if (!args.IsValid)
            //    Alert(Constant.UI.alert_invalid_parent_gallerycategory);
        }

        #endregion
    }
}