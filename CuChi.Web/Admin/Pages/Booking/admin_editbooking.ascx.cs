// =============================================
// Author:		Congtt
// Create date: 22/09/2014
// Description:	Edit danh sach booking
// =============================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Cb.DBUtility;
using Cb.Utility;
using Cb.BLL;
using Cb.Localization;
using System.IO;
using Microsoft.Security.Application;
using Cb.Model;

namespace Cb.Web.Admin.Pages.Booking
{
    public partial class admin_editbooking : DGCUserControl
    {
        #region Parameter

        protected int productcategoryId = int.MinValue;
        protected string template_path;

        private Generic<PNK_Booking> genericBLL;
        private Generic<PNK_BookingDesc> genericDescBLL;
        private Generic2C<PNK_Booking, PNK_BookingDesc> generic2CBLL;
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

        #endregion

        #region Common

        /// <summary>
        /// Init page
        /// </summary>booking
        private void InitPage()
        {
            LocalizationUtility.SetValueControl(this);
            ltrAdminApply.Text = Constant.UI.admin_apply;
            ltrAdminCancel.Text = Constant.UI.admin_cancel;
            ltrAdminDelete.Text = Constant.UI.admin_delete;
            ltrAdminSave.Text = Constant.UI.admin_save;

            //reqv_txtNameVi.Text = Constant.UI.alert_empty_name_outsite;
            //reqv_txtNameVi.ErrorMessage = Constant.UI.alert_empty_name;

            ltrAminLangVi.Text = Constant.UI.admin_lang_Vi;
            ltrAminLangEn.Text = Constant.UI.admin_lang_En;
        }

        private void GetId()
        {
            #region Set thuoc tinh cho block_baseimage

            block_baseimage.ImagePath = ConfigurationManager.AppSettings["BookingUpload"];
            block_baseimage.MinWidth = ConfigurationManager.AppSettings["minWidthCategory"];
            block_baseimage.MinHeigh = ConfigurationManager.AppSettings["minHeightCategory"];
            block_baseimage.MaxWidth = ConfigurationManager.AppSettings["maxWidthCategory"];
            block_baseimage.MaxHeight = ConfigurationManager.AppSettings["maxHeightCategory"];
            block_baseimage.MaxWidthBox = ConfigurationManager.AppSettings["maxWidthBoxCategory"];
            block_baseimage.MaxHeightBox = ConfigurationManager.AppSettings["maxHeightBoxCategory"];

            #endregion

            //get ID param          
            genericBLL = new Generic<PNK_Booking>();
            generic2CBLL = new Generic2C<PNK_Booking, PNK_BookingDesc>();
            genericDescBLL = new Generic<PNK_BookingDesc>();
            string strID = Utils.GetParameter("cid", string.Empty);
            this.productcategoryId = strID == string.Empty ? int.MinValue : DBConvert.ParseInt(strID);
            this.template_path = WebUtils.GetWebPath();
        }

        /// <summary>
        ///Hien thi o upload hinh anh( true: chua upload hinh) 
        /// </summary>
        /// <param name="isShowUplImg"></param>
        /// <param name="filename"></param>
        private void SetVisibleImg(bool isShowUplImg, string filename)
        {
            if (isShowUplImg)
            {
                fuImage.Visible = true;
                btnUploadImage.Visible = true;
                lbnView.Visible = false;
                lbnDelete.Visible = false;
            }
            else
            {
                fuImage.Visible = false;
                btnUploadImage.Visible = false;
                lbnView.Attributes["href"] = filename;
                lbnView.Visible = true;
                lbnDelete.Visible = true;
            }
        }

        /// <summary>
        /// Show location
        /// </summary>
        private void ShowProductcategory()
        {
            if (this.productcategoryId != int.MinValue)
            {
                PNK_Booking productcatObj = new PNK_Booking();
                string[] fields = { "Id" };
                productcatObj.Id = this.productcategoryId;
                productcatObj = generic2CBLL.Load(productcatObj, fields, Constant.DB.LangId);
                this.chkPublished.Checked = productcatObj.Published == "1" ? true : false;
                txtFistName.Value = productcatObj.FirstName;
                txtLastName.Value = productcatObj.LastName;
                txtPhoneNumber.Value = productcatObj.PhoneNumber;
                txtEmail.Value = productcatObj.Email;
                txtRequestTour.Value = productcatObj.RequestTour;                

                txtExpectedDepartureDate.Value = productcatObj.ExpectedDepartureDate;
                txtNumberOfAdults.Value = productcatObj.NumberOfAduts;
                txtNumberOfChildren.Value = productcatObj.NumberOfChildren;
                txtNumberOfInfant.Value = productcatObj.NumberOfInfant;
                txtTypeOfHotels.Value = productcatObj.HotelType;
                txtArrivalPort.Value = productcatObj.ArrivalPort;
                txtRoomType.Value = productcatObj.RoomType;
                txtOthersRoom.Value = productcatObj.RoomOther;
                txtBedType.Value = productcatObj.BedType;
                txtOtherBed.Value = productcatObj.BedOther;

                txtNeedVisaService.Value = productcatObj.VisaOfNeed;
                txtKnowThrought.Value = productcatObj.KnowThrough;
                txtPaymentMethod.Value = productcatObj.PaymentMethod;
                txtPaymentStatus.Value = productcatObj.PaymentStatus;

                txtDistance.Value = productcatObj.Distance;

                txtFlightArrivalNo.Text = productcatObj.FlightArrivalNo;
                txtFlightArrialTime.Text = productcatObj.FlightArrivaTime;
                txtFlightDepartureTime.Text = productcatObj.FlightDepartureTime;
                txtFlightArrivalDate.Text = productcatObj.FlightArrivalDate;
                txtFlightDepartureDate.Text = productcatObj.FlightDepartureDate;
                txtCustomerHeight.Text = productcatObj.CustomerHeight;

                txtHotelName.Text = productcatObj.HotelName;
                txtCustomerAge.Text = productcatObj.CustomerAge;
                txtHotelAddress.Text = productcatObj.HotelAddress;

                txtPickUp.Value = productcatObj.PickUpLocation;
                txtCity.Value =  productcatObj.City;
                txtCountry.Value = productcatObj.Country;

                IList<PNK_BookingDesc> lst = genericDescBLL.GetAllBy(new PNK_BookingDesc(), string.Format(" where mainid = {0}", this.productcategoryId), null);
                foreach (PNK_BookingDesc item in lst)
                {
                    switch (item.LangId)
                    {
                        case 1:
                            //productcatObj.MainId = 1;
                            //productcatObj.LangId = Constant.DB.LangId;
                            //productcatObj.Name = SanitizeHtml.Sanitize(txtFullName.Value);
                            break;
                        case 2:
                            //productcatObj.MainId = 1;
                            //productcatObj.LangId = Constant.DB.LangId_En;
                            //productcatObj.Name = SanitizeHtml.Sanitize(txtFullName.Value);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// get data for insert update
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        private PNK_Booking GetDataObjectParent(PNK_Booking productcatObj)
        {
            //productcatObj.CategoryId = 1;
            //productcatObj.Phone = this.txtPhone.Value;
            productcatObj.Published = chkPublished.Checked ? "1" : "0";
            productcatObj.UpdateDate = DateTime.Now;
            return productcatObj;
        }

        /// <summary>
        /// get data child for insert update
        /// </summary>
        /// <param name="contdescObj"></param>
        /// <returns></returns>
        private PNK_BookingDesc GetDataObjectChild(PNK_BookingDesc productcatdescObj, int lang)
        {
            switch (lang)
            {
                case 1:
                    productcatdescObj.MainId = this.productcategoryId;
                    productcatdescObj.LangId = Constant.DB.LangId;

                    break;
                case 2:
                    productcatdescObj.MainId = this.productcategoryId;
                    productcatdescObj.LangId = Constant.DB.LangId_En;


                    break;
            }
            return productcatdescObj;
        }

        /// <summary>
        /// Save location
        /// </summary>
        private void SaveBookingCategory()
        {
            PNK_Booking productcatObj = new PNK_Booking();
            PNK_BookingDesc productcatObjVn = new PNK_BookingDesc();
            PNK_BookingDesc productcatObjEn = new PNK_BookingDesc();
            if (this.productcategoryId == int.MinValue)
            {
                //get data insert
                productcatObj = this.GetDataObjectParent(productcatObj);
                productcatObj.PostDate = DateTime.Now;
                productcatObj.Ordering = genericBLL.getOrdering();
                productcatObjVn = this.GetDataObjectChild(productcatObjVn, Constant.DB.LangId);
                productcatObjEn = this.GetDataObjectChild(productcatObjEn, Constant.DB.LangId_En);

                List<PNK_BookingDesc> lst = new List<PNK_BookingDesc>();
                lst.Add(productcatObjVn);
                lst.Add(productcatObjEn);
                //excute
                this.productcategoryId = generic2CBLL.Insert(productcatObj, lst);
            }
            else
            {
                string[] fields = { "Id" };
                productcatObj.Id = this.productcategoryId;

                productcatObj = genericBLL.Load(productcatObj, fields);
                //string publisheddOld = productcatObj.Published;
                //get data update
                productcatObj = this.GetDataObjectParent(productcatObj);
                productcatObjVn = this.GetDataObjectChild(productcatObjVn, Constant.DB.LangId);
                productcatObjEn = this.GetDataObjectChild(productcatObjEn, Constant.DB.LangId_En);
                List<PNK_BookingDesc> lst = new List<PNK_BookingDesc>();
                lst.Add(productcatObjVn);
                lst.Add(productcatObjEn);
                //excute
                generic2CBLL.Update(productcatObj, lst, fields);
                //neu ve Published oo thay doi thi chay ham ChangeWithTransaction de doi Published cac con va cac product
                //if (publisheddOld != productcatObj.Published)
                //    PNK_Booking.ChangeWithTransaction(DBConvert.ParseString(this.productcategoryId), productcatObj.Published);
            }

        }

        /// <summary>
        /// delete location
        /// </summary>
        /// <param name="cid"></param>
        private void DeleteBookingCategory(string cid)
        {
            if (cid != null)
            {

                string link, url;

                if (generic2CBLL.Delete(cid))
                    link = LinkHelper.GetAdminLink("booking", "delete");
                else
                    link = LinkHelper.GetAdminLink("booking", "delfail");
                url = Utils.CombineUrl(template_path, link);
                Response.Redirect(url);

            }
        }

        /// <summary>
        /// Cancel content
        /// </summary>
        private void CancelBookingCategory()
        {
            string url = LinkHelper.GetAdminLink("booking");
            Response.Redirect(url);
        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            btn_Delete.Attributes["onclick"] = string.Format("javascript:return confirm('{0}');", Constant.UI.admin_msg_confirm_delete_item);
            GetId();
            if (!IsPostBack)
            {
                InitPage();
                ShowProductcategory();
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
                SaveBookingCategory();
                string url = LinkHelper.GetAdminMsgLink("booking", "save");
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
                SaveBookingCategory();
                string url = LinkHelper.GetAdminLink("edit_booking", this.productcategoryId);
                Response.Redirect(url);
            }
        }

        /// <summary>
        /// btnDelete_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteBookingCategory(DBConvert.ParseString(this.productcategoryId));
        }

        /// <summary>
        /// btnCancel_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CancelBookingCategory();
        }

        protected void btnUploadImage_Click(object sender, EventArgs e)
        {

            try
            {
                if (fuImage.HasFile)
                {

                    filenameUpload = string.Format("{0}.{1}", GenerateString.Generate(10), fuImage.FileName.Split('.')[1]);
                    //string str = Path.Combine(Request.PhysicalApplicationPath, Constant.DSC.NewsUploadFolder.Replace("/", "\\") + filenameUpload);
                    fuImage.SaveAs(Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["BookingUpload"]), filenameUpload));


                    //string strTemp = string.Format("<a class='zoom-image' href='{0}''>&nbsp;{1}</a>", Utils.CombineUrl(template_path, string.Format("{0}/{1}", Constant.DSC.NewsUploadFolder.Replace("\\", "/"), filename)), LocalizationUtility.GetText("strView"));
                    //strTemp += string.Format("<a href='{0}' >{1}</a>",LocalizationUtility.GetText("strDelete"));
                    //ltrImage.Text = strTemp;
                    SetVisibleImg(false, string.Format("{0}/{1}", ConfigurationManager.AppSettings["BookingUpload"], filenameUpload));
                }
            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("admin_editbooking", "btnUploadImage_Click", ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alert"></param>
        private void Alert(string alert)
        {
            string script = string.Format("alert('{0}')", alert);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertproductcategory", script, true);
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
            //    Alert(Constant.UI.alert_invalid_parent_productcategory);
        }

        protected void lbnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filenameUpload))
            {
                SetVisibleImg(true, string.Empty);

                filenameUpload = string.Empty; string f = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["BookingUpload"]), filenameUpload);
                if (File.Exists(f))
                {
                    try
                    {
                        File.Delete(f);
                    }
                    catch { }
                }
            }
        }

        #endregion
    }
}