using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using Cb.BLL;
using Cb.Model;
using Cb.Utility;
using Cb.BLL.Product;
using Cb.Model.Products;
using Cb.DBUtility;
using System.Text;
using System.Configuration;
using System.Collections;
using Cb.Localization;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.ComponentModel;
using System.Reflection;

namespace Cb.Web.Common
{
    public static class UtilityLocal
    {
        #region Parameter

        static int total = int.MinValue;

        public enum List
        {
            ProductDivision = 1,//Ngành hàng
            ProductType = 2,//Loại hàng
            ProductGroup = 3,//Nhóm hàng
            Brand = 4,//Hãng sản xuất
            Province = 5,//Tỉnh
            UnitMeasure = 6,//Đơn vị đo lường
            SalesPriceType = 7,//Loại giá
            VatGroup = 8//Loại giá
        }

        #endregion

        public static DataTable ObjectToData(object o)
        {
            DataTable dt = new DataTable("OutputData");

            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);

            o.GetType().GetProperties().ToList().ForEach(f =>
            {
                try
                {
                    f.GetValue(o, null);
                    dt.Columns.Add(f.Name, f.PropertyType);
                    dt.Rows[0][f.Name] = f.GetValue(o, null);
                }
                catch { }
            });
            return dt;
        }

        public static void PrintWebControl(Control ctrl, string Script)
        {
            StringWriter stringWrite = new StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            if (ctrl is WebControl)
            {
                Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
            }
            Page pg = new Page();
            pg.EnableEventValidation = false;
            if (Script != string.Empty)
            {
                pg.ClientScript.RegisterStartupScript(pg.GetType(), "PrintJavaScript", Script);
            }
            HtmlForm frm = new HtmlForm();
            pg.Controls.Add(frm);
            frm.Attributes.Add("runat", "server");
            frm.Controls.Add(ctrl);
            pg.DesignerInitialize();
            pg.RenderControl(htmlWrite);
            string strHTML = stringWrite.ToString();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(strHTML);
            HttpContext.Current.Response.Write("<script>window.print();</script>");
            HttpContext.Current.Response.End();
        }

        public static string GetCateNameByLevel(string pageName, string cid, string cidsub, string id)
        {
            string name = string.Empty;

            //level 1
            if ((cid == string.Empty && id == string.Empty) || (pageName == "tim-kiem" || pageName == "search"))
            {
                name = pageName;
            }
            //level 2
            else if (cid != string.Empty && cidsub == string.Empty && id == string.Empty)
            {
                name = cid;
            }
            //level 3
            else if (cid != string.Empty && cidsub != string.Empty && id == string.Empty)
            {
                name = cidsub;
            }
            //level 4
            else if (cid != string.Empty && cidsub != string.Empty && id != string.Empty)
            {
                name = id;
            }

            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="cid"></param>
        /// <param name="cidsub"></param>
        /// <param name="id"></param>
        /// <param name="LangInt"></param>
        /// <param name="level"></param>
        /// <returns>Dictionary<string, object> ProductCategoryBLL</returns>
        public static Dictionary<string, object> GetProductCategory(string pageName, string cid, string cidsub, string id, int LangInt, string level)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            ProductCategoryBLL pcBll = new ProductCategoryBLL();
            IList<PNK_ProductCategory> lst = null;
            IList<PNK_ProductCategory> lst2 = null;
            IList<PNK_ProductCategory> lst3 = null;
            if (level != null)
            {
                string cateName = Common.UtilityLocal.GetCateNameByLevel(pageName, cid, cidsub, id);
                switch (level)
                {
                    case "1":
                        lst = pcBll.GetList(LangInt, cateName, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out  total);
                        if (lst.Count > 0)
                        {
                            lst2 = lst;
                            //lst2 = pcBll.GetList(LangInt, cid, string.Empty, lst[0].Id, false, "p.ordering", 1, 1000, out  total);
                        }
                        break;
                    case "2":
                        lst = pcBll.GetList(LangInt, pageName, string.Empty, int.MinValue, false, "p.ordering", 1, 1000, out  total);
                        if (lst.Count > 0)
                        {
                            lst2 = pcBll.GetList(LangInt, cid, "1", int.MinValue, lst[0].Id, 1, true, "p.ordering", 1, 1000, out  total);
                            //lst2 = pcBll.GetList(LangInt, cid, string.Empty, lst[0].Id, true, "p.ordering", 1, 1000, out  total);
                            //if (lst2.Count > 0)
                            //{
                            //    lst2 = pcBll.GetList(LangInt, string.Empty, string.Empty, lst2[0].Id, true, "p.ordering", 1, 1000, out  total);
                            //}

                        }
                        break;
                    case "3":
                        lst = pcBll.GetList(LangInt, pageName, string.Empty, int.MinValue, false, "p.ordering", 1, 1000, out  total);
                        if (lst.Count > 0)
                        {
                            lst2 = pcBll.GetList(LangInt, cid, string.Empty, lst[0].Id, true, "p.ordering", 1, 1000, out  total);
                            if (lst2.Count > 0)
                            {
                                int parentID = lst2[0].Id;
                                lst2 = pcBll.GetList(LangInt, cidsub, "1", int.MinValue, parentID, 1, true, "p.ordering", 1, 1000, out  total);
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
                                    lst2 = pcBll.GetList(LangInt, id, "1", int.MinValue, lst2[0].Id, 1, true, "p.ordering", 1, 1000, out  total);
                                }
                            }

                        }
                        break;
                    case "tim-kiem":
                    case "search":
                        lst2 = pcBll.GetList(LangInt, cid, string.Empty, int.MinValue, true, "p.ordering", 1, 1000, out  total);
                        //if (lst.Count > 0)
                        //{
                        //    lst2 = pcBll.GetList(LangInt, cid, string.Empty, lst[0].Id, false, "p.ordering", 1, 1000, out  total);
                        //}
                        break;
                }
            }
            dic.Add("DictProductCategory", lst2);
            dic.Add("DictProductCategory_Total", total);
            return dic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="cid"></param>
        /// <param name="cidsub"></param>
        /// <param name="id"></param>
        /// <param name="LangInt"></param>
        /// <param name="level"></param>
        /// <returns>Dictionary<string, object> ProductCategoryBLL</returns>
        public static Dictionary<string, object> GetProduct(string pageName, string categoryID, int level, int langInt, int pageIndex, int pageSize)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            ProductBLL pcBll = new ProductBLL();
            IList<PNK_Product> lst = null;

            switch (level.ToString())
            {
                case "1":
                case "2":
                case "3":
                case "4":
                    DataTable dtb = DBHelper.ExcuteFromCmd("SELECT * FROM dbo.fc_GetAllChildProductCategory(" + categoryID + ",1)", null);
                    if (dtb != null && dtb.Rows.Count > 0)
                    {
                        string[] array = dtb.AsEnumerable()
                                .Select(row => row.Field<Int32>("id").ToString())
                                .ToArray();
                        string idFirst = string.Join(",", array);
                        if (pageName == "trang-chu" || pageName == "home")
                        {
                            lst = pcBll.GetList(langInt, string.Empty, "1", idFirst, string.Empty, string.Empty, "1", string.Empty, string.Empty, pageIndex, pageSize, out total);
                        }
                        else
                        {
                            lst = pcBll.GetList(langInt, string.Empty, "1", idFirst, string.Empty, pageIndex, pageSize, out total);
                        }
                    }
                    break;
                case "tim-kiem":
                case "search":

                    break;
            }

            dic.Add("DictProduct", lst);
            dic.Add("DictProduct_Total", total);
            return dic;
        }

        /// <summary>
        /// Get HReft theo từng level của Product
        /// </summary>
        /// <param name="data"></param>
        /// <param name="LangInt"></param>
        /// <param name="LangId"></param>
        /// <returns>ArrayList</returns>
        ///  item 0: HRef
        public static Dictionary<string, object> GetHRefByLevel(PNK_Product data, int LangInt, string LangId, CultureInfo Ci)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            try
            {
                string link = UtilityLocal.GetPathTreeNameUrl(data.CategoryId, LangInt, LangId);
                int level = link.Count(i => i.Equals('/'));

                if (level == 1)
                {
                    dic.Add("HRef", LinkHelper.GetLink(UtilityLocal.GetPathTreeNameUrl(data.CategoryId, LangInt, LangId), LocalizationUtility.GetText("linkCate", Ci), LocalizationUtility.GetText("linktmp", Ci), data.ProductDesc.TitleUrl));
                    // list.Add(LinkHelper.GetLink(UtilityLocal.GetPathTreeNameUrl(data.CategoryId, LangInt, LangId), LocalizationUtility.GetText("linkCate", Ci), LocalizationUtility.GetText("linktmp", Ci), data.ProductDesc.TitleUrl));
                }
                else if (level == 2)
                {
                    dic.Add("HRef", LinkHelper.GetLink(link, LocalizationUtility.GetText("linktmp", Ci), data.ProductDesc.TitleUrl));
                    //list.Add(LinkHelper.GetLink(link, LocalizationUtility.GetText("linktmp", Ci), data.ProductDesc.TitleUrl));
                }
                else if (level == 3)
                {
                    dic.Add("HRef", LinkHelper.GetLink(link, data.ProductDesc.TitleUrl));
                    //list.Add(LinkHelper.GetLink(link, data.ProductDesc.TitleUrl));
                }
                else if (level == 4)
                {
                    dic.Add("HRef", LinkHelper.GetLink(link, data.ProductDesc.TitleUrl));
                    //list.Add(LinkHelper.GetLink(link, data.ProductDesc.TitleUrl));
                }
            }
            catch (Exception ex)
            {

            }

            return dic;
        }

        public static string GetVideoList(int productID)
        {
            string result = string.Empty;
            UploadImageBLL bll = new UploadImageBLL();
            IList<PNK_UploadImage> lst = bll.GetList(string.Empty, productID.ToString(), "1", 1, 100, out  total);
            if (total > 0)
            {
                result = lst[0].ImagePath;
            }
            return result;
        }

        /// <summary>
        /// Get Image theo sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ImagePathByFont(PNK_Product data)
        {
            string result = string.Empty;
            string imagePath = data.Image == "" ? "breadcrumb-bg.jpg" : data.Image;
            int imageType = data.ImageType;
            switch (imageType)
            {
                case 1:
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<div class=\"page-header-wrapper gdl-container-color boxed-style gdl-border-x bottom\" style='background-image: url({0});text-align: center;background-size: cover;position: relative;color: #fff; height: 200px;' >", WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductUpload"], imagePath));
                    sb.Append("<div class=\"page-header-overlay\"></div><div class=\"page-header-container container\"> ");
                    sb.Append("<div class=\"page-header-title\">" + data.ProductDesc.Title + " </div>");
                    sb.Append("<div class=\"page-header-caption\">" + data.ProductDesc.Brief + "</div>");
                    sb.Append("<div class=\"clear\"></div>");
                    sb.Append(" </div></div>");
                    result = sb.ToString();
                    break;
                case 2:
                    result = data.ImageFont;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Get Image theo danh mục PNK_ProductCategory        
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ImagePathByFont(PNK_ProductCategory data)
        {
            string result = string.Empty;
            string imagePath = data.BaseImage == "" ? "breadcrumb-bg.jpg" : data.BaseImage;
            int imageType = data.ImageType;
            switch (imageType)
            {
                case 1:
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<div class=\"page-header-wrapper gdl-container-color boxed-style gdl-border-x bottom\" style='background-image: url({0});text-align: center;background-size: cover;position: relative;color: #fff; height: 200px;' >", WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductCategoryUpload"], imagePath));
                    sb.Append("<div class=\"page-header-overlay\"></div><div class=\"page-header-container container\"> ");
                    sb.Append("<div class=\"page-header-title\">" + data.ProductCategoryDesc.Name + " </div>");
                    sb.Append("<div class=\"page-header-caption\">" + data.ProductCategoryDesc.Brief + "</div>");
                    sb.Append("<div class=\"clear\"></div>");
                    sb.Append(" </div></div>");
                    result = sb.ToString();
                    break;
                case 2:
                    result = data.ImageFont;
                    break;
            }

            return result.ToString();
        }

        /// <summary>
        /// Get tên file trong bảng PNK_UploadImage
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static string GetFileNameByID(int productID)
        {
            string result = string.Empty;
            UploadImageBLL bll = new UploadImageBLL();
            IList<PNK_UploadImage> lst = bll.GetList(string.Empty, productID.ToString(), "1", 1, 100, out  total);
            if (total > 0)
            {
                result = lst[0].Name;
            }

            return result;
        }

        public static string GetPathTreeNameUrl(int categoryID, int langInt, string langId)
        {
            string result = string.Empty, id = string.Empty, pathTreeSub = string.Empty;

            try
            {
                ProductCategoryBLL pcBll = new ProductCategoryBLL();
                IList<PNK_ProductCategory> lst = pcBll.GetList(langInt, string.Empty, string.Empty, categoryID, int.MinValue, false, string.Empty, 1, 9999, out total);
                if (total > 0)
                {
                    string pathTree = lst[0].PathTree;
                    int level = pathTree.Count(i => i.Equals('.'));
                    for (int i = 1; i <= level; i++)
                    {
                        pathTreeSub = pathTree.Split('.')[i];
                        id = pathTreeSub.Split('-')[1];
                        lst = pcBll.GetList(langInt, string.Empty, string.Empty, int.Parse(id), int.MinValue, false, string.Empty, 1, 9999, out total);

                        result = result + (total > 0 ? lst[0].ProductCategoryDesc.NameUrl : string.Empty) + "/";

                        if (i == 1)
                        {
                            result = result + langId + "/";
                        }
                    }
                    result = result == "" ? "" : result.Remove(result.LastIndexOf('/'), 1);
                }
            }
            catch (Exception ex)
            {

                Write2Log.WriteLogs("Home Page", "GetPathTreeNameUrl", ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Đọc các loại file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="Request"></param>
        /// <param name="Response"></param>
        /// <param name="Page"></param>
        public static void ReadFile(string fileName, HttpRequest Request, HttpResponse Response, Page Page)
        {
            try
            {
                string path = Request.PhysicalApplicationPath;
                string url = Path.Combine("Resource", "Upload", "Products", fileName);
                url = Path.Combine(path, url);

                if (File.Exists(url))
                {
                    WebClient wc = new WebClient();
                    Byte[] buffer = wc.DownloadData(url);
                    if (buffer != null)
                    {
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        switch (WebUtils.GetFileExtension(fileName))
                        {
                            case "doc":
                                Response.ContentType = "application/msword";
                                break;
                            case "docx":
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                                break;
                            case "xls":
                                Response.ContentType = "application/vnd.ms-excel";
                                break;
                            case "xlsx":
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                break;
                            case "pdf":
                                Response.ContentType = "application/pdf";
                                break;
                        }
                        Response.BinaryWrite(buffer);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), string.Format("jAlert('Không có file !','Message',function(r) {{window.location='{0}'}});", Request.RawUrl), true);
            }
        }

        public static string AppendLanguage(string treeNameUrl, string langId)
        {
            string result = string.Empty;

            int firstFlag = treeNameUrl.IndexOf('/', 0);
            result = firstFlag > -1 ? treeNameUrl.Insert(firstFlag, string.Format("/{0}", langId)) : string.Format("{0}/{1}", treeNameUrl, langId);

            return result;
        }
    }
}