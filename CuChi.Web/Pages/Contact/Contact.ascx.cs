using Cb.BLL;
using Cb.BLL.Product;
using Cb.DBUtility;
using Cb.Localization;
using Cb.Model;
using Cb.Model.Products;
using Cb.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.XPath;

namespace Cb.Web.Pages.Contact
{
    public partial class Contact : DGCUserControl
    {
        #region Parameter

        protected string pageName, template_path = string.Empty, id = string.Empty, cid = string.Empty, cidsub = string.Empty;
        private int total;

        #endregion

        #region Common

        private void InitPage()
        {
            template_path = WebUtils.GetWebPath();
            pageName = Utils.GetParameter("page", "home");
            cid = Utils.GetParameter("cid", string.Empty);
            cidsub = Utils.GetParameter("cidsub", string.Empty);
            id = Utils.GetParameter("id", string.Empty);

            GetConfig();
            SetHeader();
            GetCount();

            btnSend.Text = LocalizationUtility.GetText("ltrSend", Ci);
            GetOrder();
        }

        private void SetHeader()
        {
            //Set header
            ProductCategoryBLL pcBll = new ProductCategoryBLL();
            IList<PNK_ProductCategory> lst = pcBll.GetList(LangInt, pageName, string.Empty, int.MinValue, int.MinValue, false, string.Empty, 1, 1, out  total);
            if (total > 0)
            {
                //Gen html image category
                ltrHeaderCategory.Text = Common.UtilityLocal.ImagePathByFont(lst[0]);

                //ltrCateNameTitle.Text = lst[0].ProductCategoryDesc.Name;
                //ltrCategoryBrief.Text = lst[0].ProductCategoryDesc.Brief;

                WebUtils.SeoPage(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaDecription, lst[0].ProductCategoryDesc.MetaKeyword, this.Page);
                WebUtils.SeoTagH(lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, lst[0].ProductCategoryDesc.MetaTitle, Controls);
            }
        }

        private void GetConfig()
        {
            //ConfigurationBLL pcBll = new ConfigurationBLL();
            //IList<PNK_Configuration> lst = pcBll.GetList();
            //if (lst != null && lst.Count > 0)
            //{
            //    foreach (PNK_Configuration item in lst)
            //    {
            //        if (LangInt == 1)
            //        {
            //            if (item.Key_name == Constant.Configuration.config_address_vi)
            //            {
            //                ltrAddressValue.Text = item.Value_name;
            //            }
            //            else if (item.Key_name == Constant.Configuration.phone)
            //            {
            //                ltrPhoneValue.Text = item.Value_name;
            //            }
            //            else if (item.Key_name == Constant.Configuration.email)
            //            {
            //                ltrEmail.Text = item.Value_name;
            //            }
            //        }
            //        else
            //        {
            //            if (item.Key_name == Constant.Configuration.config_address1_vi)
            //            {
            //                ltrAddressValue.Text = item.Value_name;
            //            }
            //            else if (item.Key_name == Constant.Configuration.phone)
            //            {
            //                ltrPhoneValue.Text = item.Value_name;
            //            }
            //            else if (item.Key_name == Constant.Configuration.email)
            //            {
            //                ltrEmail.Text = item.Value_name;
            //            }
            //        }
            //    }
            //}
        }

        private void GetCount()
        {
            ContentStaticBLL pcBll = new ContentStaticBLL();
            IList<PNK_ContentStatic> lst = pcBll.GetList(LangInt, string.Empty, ConfigurationManager.AppSettings["contentStatic_Count"], string.Empty, 1, 1, out total);
            if (total > 0)
            {
                ltrAddress.Text = lst[0].ContentStaticDesc.Brief;
            }
        }

        private void GetOrder()
        {
            if (Session["orderID_booking"] != null)
            {
                string URL = "https://pgcard.agribank.com.vn:8380/Exec";
                string postData = "<TKKPG>\n" +
                        "  <Request>\n" +
                        "   <Operation>GetOrderInformation</Operation>\n" +
                        "   <Language>EN</Language>\n" +
                        "   <Order>\n" +
                        "       <OrderType>Purchase</OrderType>\n" +
                        "       <Merchant>VBAPGMER07</Merchant>\n" +
                        "       <OrderID>724</OrderID>\n" +                                                                  
                        "    </Order>\n" +
                        "   <SessionID>605BA73863972B1E31C582F95CBC54AF</SessionID>\n" +
                        "   <ShowParams>true</ShowParams>\n" +
                        "   <ShowOperations>true</ShowOperations>\n" +
                        "   <ClassicView>true</ClassicView>\n" +
                        " </Request>\n" +
                        "</TKKPG>";
                string responseData = POSTRequest(URL, postData);
                txtMessage.InnerText = "abc:" + responseData;
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), string.Format("jAlert('" + responseData + "','Message',function(r) {{window.location='{0}'}});", Request.RawUrl), true);

            }

        }

        public string POSTRequest(string URL, string postData)
        {
            string responseData = "", orderID = string.Empty, orderIDEncry = string.Empty, sessionID = string.Empty;

            try
            {
                HttpWebRequest hwrequest = (HttpWebRequest)WebRequest.Create(URL);
                hwrequest.Timeout = 60000;
                hwrequest.Method = "POST";
                hwrequest.KeepAlive = false;

                hwrequest.ContentType = "application/x-www-form-urlencoded";

                UTF8Encoding encoding = new UTF8Encoding();
                byte[] postByteArray = encoding.GetBytes(postData);
                hwrequest.ContentLength = postByteArray.Length;
                Stream postStream = hwrequest.GetRequestStream();
                postStream.Write(postByteArray, 0, postByteArray.Length);
                postStream.Close();


                // Attempt to receive the WebResponse to the WebRequest.
                using (HttpWebResponse hwresponse = (HttpWebResponse)hwrequest.GetResponse())
                {
                    if (hwresponse != null)
                    { // If we have valid WebResponse then read it.
                        using (StreamReader reader = new StreamReader(hwresponse.GetResponseStream()))
                        {
                            XPathDocument doc = new XPathDocument(reader);
                            XPathNavigator xml = doc.CreateNavigator();
                            XPathNodeIterator nodes = xml.Select("/TKKPG/Response/Order/row/OrderParams/row");
                            foreach (XPathNavigator item in nodes)
                            {
                                //responseData = item.Value;
                                string sPARAMNAME = item.SelectSingleNode("PARAMNAME").Value;
                                string sVal = item.SelectSingleNode("VAL").Value;
                                if (sPARAMNAME.Equals("TWOResponseCode"))
                                {
                                    responseData = sVal;
                                }

                            }
                            reader.Close();
                        }
                    }

                    hwresponse.Close();
                }
            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("block_booking", "POSTRequest", ex.Message);
            }

            return responseData;
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

        protected void btnSend_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    bool result = false;
                    string path = Request.PhysicalApplicationPath;
                    string strHtml = WebUtils.GetMailTemplate(Path.Combine(path, "TemplateMail/Contact.txt"));
                    string body = string.Format(strHtml, "admin", txtFullName.Value, txtEmail.Value, txtMessage.Value);
                    result = WebUtils.SendEmail("Contact", txtEmail.Value, string.Empty, body);

                    if (result == true)
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), string.Format("jAlert('Send success.','Message',function(r) {{window.location='{0}'}});", Request.RawUrl), true);
                    else
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), string.Format("jAlert('Send fail.','Message',function(r) {{window.location='{0}'}});", Request.RawUrl), true);
                    txtEmail.Value = txtMessage.Value = txtFullName.Value = "";
                }
            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("btnSend_ServerClick.aspx", "Contact", ex.Message);
            }
        }

        #endregion
    }
}