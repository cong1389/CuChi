using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Model;
using System.Data;
using Cb.DBUtility;
using Cb.Localization;
using System.Globalization;
using Cb.BLL;
using Cb.Utility;
using Cb.BLL.Product;
using Cb.Model.Products;
using System.Net;
using System.Text;
using System.IO;
using System.Xml.XPath;
using System.Security.Cryptography;

namespace Cb.Web
{
    public partial class Template : System.Web.UI.MasterPage
    {
        #region Parameter

        protected string pageName, template_path = string.Empty, cid, cidsub, id;
        int total;

        #endregion

        #region Common

        private void InitPage()
        {
            try
            {
                this.template_path = WebUtils.GetWebPath();
                pageName = Utils.GetParameter("page", "home");
                cid = Utils.GetParameter("cid", string.Empty);
                cidsub = Utils.GetParameter("cidsub", string.Empty);
                id = Utils.GetParameter("id", string.Empty);

                //string pathUsc = pageName;
                //switch (pageName)
                //{
                //    case "home":
                //    case "trang-chu":
                //        block_slide.Visible = true;
                //        divBoxTop.Attributes.Add("class", "header-outer-wrapper full-slider");
                //        break;
                //    default:
                //        divBoxTop.Attributes.Add("class", "header-outer-wrapper no-top-slider");
                //        break;

                //}
                //UserControl contentView = (UserControl)Page.LoadControl(pathUsc);
                //childContent.Controls.Add(contentView);

                //ProductCategoryBLL pcBllCate = new ProductCategoryBLL();
                //IList<PNK_ProductCategory> lstCate = pcBllCate.GetList(1, pageName, string.Empty, int.MinValue, false, "p.ordering", 1, 9999, out  total);
                //if (total > 0)
                //{
                //    string pagePath = lstCate[0].PageDetail.ToLower();
                //    if (pagePath.Contains("template") && id != string.Empty && cidsub != "page")
                //    {
                //        top_menu.Visible = footer.Visible = main.Visible = false;
                //    }
                //}
            }
            catch (Exception ex)
            {


            }

            //ConfigurationBLL pcBll = new ConfigurationBLL();
            //IList<PNK_Configuration> lst = pcBll.GetList();
            //if (lst != null && lst.Count > 0)
            //{
            //    foreach (PNK_Configuration item in lst)
            //    {
            //        if (item.Key_name == Constant.Configuration.config_vchat)
            //        {
            //            // WebUtils.IncludeJSScript(this.Page, item.Value_name);
            //        }
            //    }
            //}
        }

        public string POSTRequest()
        {
            string responseData = "", orderID = string.Empty, orderIDEncry = string.Empty, sessionID = string.Empty;

            try
            {
                string URL = "https://pgcard.agribank.com.vn:8380/Exec";
                string method = "POST";
                string postData = "<TKKPG>\n" +
                        "  <Request>\n" +
                        "   <Operation>CreateOrder</Operation>\n" +
                        "   <Language>EN</Language>\n" +
                        "   <Order>\n" +
                        "     <OrderType>Purchase</OrderType>\n" +
                        "     <Merchant>VBAPGMER07</Merchant>\n" +
                        "     <Amount>100000</Amount>\n" +
                        "     <Currency>704</Currency>\n" +
                        "     <Description>Thanh toan don han  test1</Description>\n" +
                        "     <ApproveURL>https://www.merchantagribank/Approve.jsp?TransactionID=123456</ApproveURL> " +
                        "     <CancelURL>https://www.merchantagribank/Cancel.jsp?TransactionID=123456</CancelURL>\n" +
                        "     <DeclineURL>https://www.merchantagribank/Decline.jsp?TransactionID=123456</DeclineURL>\n" +
                        "    </Order>\n" +
                        " </Request>\n" +
                        "</TKKPG>";

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
                            XPathNodeIterator nodes = xml.Select("/TKKPG/Response/Order/OrderID");
                            foreach (XPathNavigator item in nodes)
                            {
                                orderID = item.Value;
                            }

                            XPathNodeIterator nodesSession = xml.Select("/TKKPG/Response/Order/SessionID");
                            foreach (XPathNavigator item in nodesSession)
                            {
                                sessionID = item.Value;
                            }
                            reader.Close();
                        }
                    }

                    hwresponse.Close();
                }


            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("block_bookin", "POSTRequest", ex.Message);
            }

            //Goi ham ma hoa
            orderIDEncry = EncryptData(orderID, "eExLKznyBa5ABPQ6");
            string urlRedirect = string.Format("https://pgcard.agribank.com.vn/index.jsp?ORDERID={0}&SESSIONID={1}", orderIDEncry, sessionID);
            Response.Redirect(urlRedirect);

            return responseData;
        }

        public static string GetOrder8(string orderId)
        {
            string result = orderId + "        ".Substring(0, 8 - ((ToHex(orderId).Length % 16) >> 1));
            return result;
        }
        public static string ToHex(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                byte[] data = Encoding.UTF8.GetBytes(input);
                string hexData = BitConverter.ToString(data);
                return hexData.Replace("-", "");
            }
            else
            {
                return string.Empty;
            }
        }
        public static byte[] HexToByte(string hexString)
        {
            int numberChars = hexString.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                int high;
                try
                {
                    high = Convert.ToInt32(((char)hexString[i]).ToString(), 16);
                }
                catch (Exception ex)
                {
                    high = -1;
                }
                int low;
                try
                {
                    low = Convert.ToInt32(((char)hexString[i + 1]).ToString(), 16);
                }
                catch (Exception ex)
                {
                    low = -1;
                }
                int value = high << 4 | low;
                if (value > 127)
                {
                    value -= 256;
                }
                bytes[i / 2] = ((byte)value);
            }
            return bytes;
        }
        public static string EncryptData(string orderId, string key)
        {
            try
            {
                string order8 = GetOrder8(orderId);
                string hexData = ToHex(order8);
                var des = new DESCryptoServiceProvider();
                des.Padding = PaddingMode.None;
                des.Mode = CipherMode.ECB;
                var keyarr = HexToByte(key);
                des.Key = keyarr;
                des.IV = new byte[des.BlockSize / 8];
                ICryptoTransform ct = des.CreateEncryptor();
                byte[] input = HexToByte(hexData);
                var result = ct.TransformFinalBlock(input, 0, input.Length);
                return BitConverter.ToString(result).Replace("-", "");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            //  var keyarr = HexToByte1();

            //POSTRequest();
            // Response.Redirect("https://pgcard.agribank.com.vn/");
            InitPage();
        }

        #endregion
    }
}