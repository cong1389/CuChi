// =============================================
// Author:		Congtt
// Create date: 22/09/2014
// Description:	danh sach sản phẩm
// =============================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cb.Model;
using Cb.DBUtility;
using Cb.BLL;
using Cb.Localization;
using Cb.Model.Products;
using Cb.BLL.Product;
using System.IO;
using System.Configuration;
using Cb.Utility;

namespace Cb.Web.Admin.Pages.Gallerys
{
    public partial class admin_gallery : System.Web.UI.UserControl
    {
        #region Parameter

        private ProductBLL pcBll
        {
            get
            {
                if (ViewState["pcBll"] != null)
                    return (ProductBLL)ViewState["pcBll"];
                else return new ProductBLL();
            }
            set
            {
                ViewState["pcBll"] = value;
            }
        }
        private Generic<PNK_Product> genericBLL
        {
            get
            {
                if (ViewState["genericBLLget"] != null)
                    return (Generic<PNK_Product>)ViewState["genericBLLget"];
                else return new Generic<PNK_Product>();
            }
            set
            {
                ViewState["genericBLLget"] = value;
            }
        }
        private Generic2C<PNK_Product, PNK_ProductDesc> generic2CBLL
        {
            get
            {
                if (ViewState["generic2CBLL"] != null)
                    return (Generic2C<PNK_Product, PNK_ProductDesc>)ViewState["generic2CBLL"];
                else return new Generic2C<PNK_Product, PNK_ProductDesc>();
            }
            set
            {
                ViewState["generic2CBLL"] = value;
            }
        }

        private int indexItem = 0;
        protected string template_path
        {
            get
            {
                if (ViewState["template_path"] != null)
                    return ViewState["template_path"].ToString();
                else
                    return null;
            }
            set
            {
                ViewState["template_path"] = value;
            }
        }
        protected string show_msg, action, l_search, records, msg_no_selected_item, msg_confirm_delete_item;

        string getLinkByPageName = string.Empty;

        #region Viewstate

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

        private string categoryId
        {
            get
            {
                if (ViewState["CategoryID"] != null)
                    return ViewState["CategoryID"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["CategoryID"] = value;
            }
        }

        #endregion

        #endregion

        #region Common

        /// <summary>
        /// Init page
        /// </summary>
        private void InitPage()
        {
            categoryId = Utils.GetParameter("cid", string.Empty);

            //switch page
            string pageName = Utils.GetParameter("page", "home");
            switch (pageName)
            {
                case "picture":
                    getLinkByPageName = Constant.UI.edit_picture_page;
                    break;
                case "video":
                    getLinkByPageName = Constant.UI.edit_video_page;
                    break;
            }


            pcBll = new ProductBLL();
            genericBLL = new Generic<PNK_Product>();
            generic2CBLL = new Generic2C<PNK_Product, PNK_ProductDesc>();
            this.template_path = WebUtils.GetWebPath();
            msg_confirm_delete_item = LocalizationUtility.GetText("mesConfirmDelete");
            msg_no_selected_item = LocalizationUtility.GetText("mesSelectItem");
            LocalizationUtility.SetValueControl(this);

            BindNewsCategory();
            GetMessage();
        }

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="cateId">Mảng cái ID con</param>
        /// <param name="content">Đây là ID trong GetList Bill, Where theo TitlUrl bảng Product</param>
        /// <returns></returns>
        private int GetList(byte langid, string content, string cateId, int begin, int end)
        {
            int total;
            IList<PNK_Product> lst = pcBll.GetList(1, string.Empty, string.Empty, cateId, content, null, string.Empty, begin, end, out total);

            if (lst != null && lst.Count > 0)
            {
                this.records = DBConvert.ParseString(lst.Count);
                this.pager.PageSize = DBConvert.ParseInt(ConfigurationManager.AppSettings["PageSizeAdmin"]);
                this.pager.ItemCount = total;
                this.rptResult.DataSource = lst;
                this.rptResult.DataBind();

            }

            return total;
        }

        /// <summary>
        /// action
        /// </summary>
        private void GetAction()
        {
            

            this.action = Request.Form["task"];
            string cid = Request.Form["cid[]"];
            switch (action)
            {
                case "new":
                    Add();
                    break;
                case "edit":
                    Edit(cid);
                    break;
                case "publish":
                    Change(cid, "1");
                    break;
                case "unpublish":
                    Change(cid, "0");
                    break;
                case "delete":
                    Delete(cid);
                    break;
                case "save":
                    SaveOrder();
                    string url = LinkHelper.GetAdminLink("news");
                    Response.Redirect(url);
                    break;
                case "search":
                    pager.CurrentIndex = 1;
                    this.currentPageIndex = 1;
                    Search();
                    break;
                case "orderup":
                    Order(cid, 1);
                    break;
                case "orderdown":
                    Order(cid, -1);
                    break;
                    //default:
                    //    show();
                    //    break;
            }
        }

        /// <summary>
        ///Thêm Item
        /// </summary>
        private void Add()
        {
            //switch page
            string pageName = Utils.GetParameter("page", "home");
            switch (pageName)
            {
                case Constant.UI.picture_page:
                    getLinkByPageName = Constant.UI.edit_picture_page;
                    break;
                case Constant.UI.video_page:
                    getLinkByPageName = Constant.UI.edit_video_page;
                    break;
            }

            string url = LinkHelper.GetAdminLink(getLinkByPageName);
            Response.Redirect(url);
        }

        /// <summary>
        /// Edit Item
        /// </summary>
        /// <param name="cid"></param>
        private void Edit(string cid)
        { 
            //switch page
            string pageName = Utils.GetParameter("page", "home");
            switch (pageName)
            {
                case Constant.UI.picture_page:
                    getLinkByPageName = Constant.UI.edit_picture_page;
                    break;
                case Constant.UI.video_page:
                    getLinkByPageName = Constant.UI.edit_video_page;
                    break;
            }

            if (cid == null) return;
            string link, url;
            string[] arrStr;
            if (cid.IndexOf(',') >= 0)
            {
                arrStr = cid.Split(',');
                link = LinkHelper.GetAdminLink(getLinkByPageName, arrStr[0]);
                //link = string.Format(SiteNavigation.link_adminPage_editgallerycategory, arrStr[0]);
            }
            else
                link = LinkHelper.GetAdminLink(getLinkByPageName, cid);
            Response.Redirect(link);
        }

        /// <summary>
        /// change
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="state"></param>
        private void Change(string cid, string state)
        {
            if (cid != null)
            {
                genericBLL.ChangeWithTransaction(cid, state);
                Search();
            }
        }

        /// <summary>
        /// Delete image in folder and database
        /// </summary>
        private void Delete(string cid)
        {
            if (cid != null)
            {
                PNK_Product galleryCatObj = new PNK_Product();
                string[] fields = { "Id" };
                galleryCatObj.Id = DBConvert.ParseInt(cid);
                galleryCatObj = genericBLL.Load(galleryCatObj, new string[] { "Id" });
                //string f = Path.Combine(Server.MapPath(Constant.DSC.ProductUploadFolder), strHeaderProduct.Text.Trim().Remove(0, 5), galleryCatObj.Image);
                //if (File.Exists(f))
                //{
                //    try
                //    {
                //        File.Delete(f);
                //    }
                //    catch { }
                //}

                //switch page
                string pageName = Utils.GetParameter("page", "home");
                switch (pageName)
                {
                    case Constant.UI.picture_page:
                        getLinkByPageName = Constant.UI.picture_page;
                        break;
                    case Constant.UI.video_page:
                        getLinkByPageName = Constant.UI.video_page;
                        break;
                }

                string link, url;
                if (generic2CBLL.Delete(cid))
                {
                    link = LinkHelper.GetAdminMsgLink(getLinkByPageName, cid, "delete");
                }
                else
                    link = LinkHelper.GetAdminMsgLink(getLinkByPageName, cid, "delfail");
                url = Utils.CombineUrl(template_path, link);
                Response.Redirect(url);
            }
        }

        /// <summary>
        /// sort
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="inc"></param>
        private void Order(string cid, int inc)
        {
            if (cid != null)
            {
                string[] fields = { "Id" };
                PNK_Product Obj = new PNK_Product();
                Obj.Id = DBConvert.ParseInt(cid);
                Obj = genericBLL.Load(Obj, fields);
                genericBLL.Move(Obj, inc);
            }
            Search();
        }

        /// <summary>
        /// saveOrder
        /// </summary>
        private void SaveOrder()
        {
            foreach (RepeaterItem item in rptResult.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlInputButton btId = (HtmlInputButton)item.FindControl("btId");
                    PNK_Product galleryCat = new PNK_Product();
                    galleryCat.Id = DBConvert.ParseInt(btId.Value);
                    galleryCat = genericBLL.Load(galleryCat, new string[] { "Id" });
                    HtmlInputText txtOrder = (HtmlInputText)item.FindControl("txtOrder");
                    if (txtOrder != null)
                    {
                        try
                        {
                            galleryCat.Ordering = DBConvert.ParseInt(txtOrder.Value);
                            if (galleryCat.Ordering > 0)
                            {
                                genericBLL.Update(galleryCat, galleryCat, new string[] { "Id" });
                            }
                        }
                        catch { }
                    }
                }
            }
        }

        /// <summary>
        /// get msg
        /// </summary>
        private void GetMessage()
        {
            string msg = Utils.GetParameter("msg", string.Empty);
            if (msg == string.Empty) return;
            if (msg == "save")
            {
                this.show_msg = string.Format("<div id=\"Cb-msg\"><div class=\"message\">{0}</div></div>", Constant.UI.admin_msg_save_success);
            }
            else if (msg == "delete")
            {
                this.show_msg = string.Format("<div id=\"Cb-msg\"><div class=\"message\">{0}</div></div>", Constant.UI.admin_msg_delete_success);
            }
        }

        /// <summary>
        /// Tìm kiếm
        /// </summary>
        private void Search()
        {
            string strSearch = Request.Form[search.ClientID.Replace('_', '$')];
            this.search.Value = strSearch;
            strSearch = strSearch == null ? string.Empty : Utils.RemoveUnicode(SanitizeHtml.Sanitize(strSearch));

            GetList(1, strSearch, GetAllChildCategory(), this.currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["PageSizeAdmin"]));
        }

        /// <summary>
        /// Gán dữ liệu cho drpCategory
        /// Vi Get du lieu theo PNK_ProductCategory, cid lay tu url xuong nên SelectedIndex theo cid        
        /// </summary>
        private void BindNewsCategory()
        {
            admin_editgallery.GetDataDropDownCategory(drpCategory);
            //drpCategory.SelectedIndex = 0;
            //drpCategory.SelectedIndex = drpCategory.Items.IndexOf(drpCategory.Items.FindByValue(ConfigurationManager.AppSettings["parentIdLeture  "]));
            //drpCategory.Attributes.Add("disabled", "disabled");
            //drpCategory.Style.Add("background-color", "#dddddd");
        }

        private string GetAllChildCategory()
        {
            int categoryId = int.MinValue;// = DBConvert.ParseInt(drpCategory.SelectedValue);
            string pageName = Utils.GetParameter("page", "home");
            switch (pageName)
            {
                case "picture":
                    categoryId = 112;
                    break;
                case "video":
                    categoryId = 113;
                    break;
            }

            //categoryId = categoryId == 0 ? int.MinValue : DBConvert.ParseInt(drpCategory.SelectedValue);
            string arrId = "";
            if (categoryId != int.MinValue)
            {
                ProductCategoryBLL newsCateBll = new ProductCategoryBLL();
                IList<PNK_ProductCategory> lst = newsCateBll.GetAllChild(categoryId, true);

                arrId = arrId + Utils.ArrayToString((List<PNK_ProductCategory>)lst, "Id", ",");

                drpCategory.SelectedValue = categoryId.ToString();
                drpCategory.Attributes.Add("readonly", "true");
            }
            else
            {
                ProductCategoryBLL newsCateBll = new ProductCategoryBLL();
                IList<PNK_ProductCategory> lst = newsCateBll.GetAllChild(DBConvert.ParseInt(ConfigurationManager.AppSettings["parentIdContact"]), true);

                arrId = arrId + Utils.ArrayToString<PNK_ProductCategory>((List<PNK_ProductCategory>)lst, "Id", ",");
            }
            return arrId;// !string.IsNullOrEmpty(arrId) ? arrId : "-1011";
        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            //Xoá cache trước khi lưu
            CacheHelper.ClearContains("Product");

            GetAction();
            BindNewsCategory();
            if (!IsPostBack)
            {
                InitializeComponent();
                Search();
            }
        }

        /// <summary>
        /// init component
        /// </summary>
        override protected void OnInit(EventArgs e)
        {
            //InitializeComponent();
            //  base.OnInit(e);
        }

        /// <summary>
        /// Nhiệm vụ: 1.Khởi tạo đối tượng; 2. Set text control.
        /// </summary>
        private void InitializeComponent()
        {
            InitPage();

            strHeaderGallery.Text = drpCategory.SelectedItem.Text;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveOrder();

            //switch page
            string pageName = Utils.GetParameter("page", "home");
            switch (pageName)
            {
                case Constant.UI.picture_page:
                    getLinkByPageName = Constant.UI.picture_page;
                    break;
                case Constant.UI.video_page:
                    getLinkByPageName = Constant.UI.video_page;
                    break;
            }

            string url = Utils.CombineUrl(template_path, LinkHelper.GetAdminLink(getLinkByPageName));
            Response.Redirect(url);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
            //rptResult.DataBind();
        }

        protected void drpCategory_onchange(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(3000);
            //    lblText.Text = "Xử lý hoàn tất.";
            Search();
        }

        /// <summary>
        /// ItemDataBound
        /// </summary>
        protected void rptResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string img, alt, publishedTask;
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trList");
                HtmlInputText txt = null;
                if (e.Item.ItemIndex % 2 == 0)
                {
                    tr.Attributes.Add("class", "even");
                }
                else
                {
                    tr.Attributes.Add("class", "old");
                }

                try
                {
                    PNK_Product data = (PNK_Product)e.Item.DataItem;

                    //Role
                    Literal ltr = null;
                    ltr = (Literal)e.Item.FindControl("ltrchk");
                    ltr.Text = string.Format(@"<INPUT class='txt' TYPE='checkbox' ID='cb{0}' NAME='cid[]' value='{1}' onclick='isChecked(this.checked);' >",
                                                e.Item.ItemIndex, data.Id);

                    //ltrNewsCategory
                    ltr = (Literal)e.Item.FindControl("ltrNewsCategory");
                    ltr.Text = data.CategoryNameDesc;

                    //Sort
                    ltr = (Literal)e.Item.FindControl("ltrSort");
                    string strOrder = string.Empty;
                    string onclick = string.Empty;

                    //orderDown
                    if (indexItem < this.pager.ItemCount - 1)
                    {
                        onclick = string.Format("onclick=\"listItemTask('cb{0}', 'orderdown')\"", e.Item.ItemIndex);
                        strOrder += string.Format("<a title='{0}' {1} runat='server' class=\"center-block text-center\" style='cursor:pointer;color:#3C8DBC'><i class=\"fa fa-long-arrow-down\"></i></a> ", Constant.UI.admin_Down, onclick);
                    }
                    //orderUp
                    if (indexItem > 0)
                    {
                        onclick = string.Format("onclick=\"listItemTask('cb{0}', 'orderup')\"", e.Item.ItemIndex);
                        strOrder += string.Format("<a title='{0}' {1} runat='server' class=\"center-block text-center\" style='cursor:pointer;color:#3C8DBC'><i class=\"fa fa-long-arrow-up\"></i> </a> ", Constant.UI.admin_Up, onclick);
                    }
                    indexItem++;
                    ltr.Text = strOrder;

                    //publish
                    if (data.Published == "1")
                    {
                        img = "tick.png";
                        alt = LocalizationUtility.GetText(ltrAdminPublish.Text);
                        publishedTask = "unpublish";
                    }
                    else
                    {
                        img = "publish_x.png";
                        alt = LocalizationUtility.GetText(ltrAminUnPublish.Text);
                        publishedTask = "publish";
                    }

                    //Id
                    HtmlInputButton btId = (HtmlInputButton)e.Item.FindControl("btId");
                    btId.Value = DBConvert.ParseString(data.Id);

                    //Base img
                    HtmlImage baseImage = (HtmlImage)e.Item.FindControl("baseImage");
                    baseImage.Src = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductUpload"], data.Image);
                    HtmlAnchor hypBaseImage = (HtmlAnchor)e.Item.FindControl("hypBaseImage");

                    //set link
                    HyperLink hdflink = new HyperLink();
                    hdflink = (HyperLink)e.Item.FindControl("hdflink");
                    hypBaseImage.HRef = hdflink.NavigateUrl = template_path + LinkHelper.GetAdminLink(getLinkByPageName, data.CategoryId.ToString(), data.Id.ToString());
                    //HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("tdName");
                    //td.Attributes.Add("onclick", string.Format("listItemTask('cb{0}', 'edit')", e.Item.ItemIndex));
                    //td = (HtmlTableCell)e.Item.FindControl("trUpdateDate");
                    //td.Attributes.Add("onclick", string.Format("listItemTask('cb{0}', 'edit')", e.Item.ItemIndex));
                    ImageButton imgctr = (ImageButton)e.Item.FindControl("btnPublish");
                    imgctr.ImageUrl = string.Format("/Admin/images/{0}", img);
                    imgctr.Attributes.Add("alt", alt);
                    HtmlTableCell btn = (HtmlTableCell)e.Item.FindControl("tdbtn");
                    btn.Attributes.Add("onclick", string.Format(" return listItemTask('cb{0}', '{1}')", e.Item.ItemIndex, publishedTask));

                    //Name
                    ltr = (Literal)e.Item.FindControl("ltrName");
                    hypBaseImage.Attributes["title"] = baseImage.Alt = baseImage.Attributes["title"] = ltr.Text = data.ProductDesc.Title;
                    //Server.HtmlDecode(getScmplit(data.Lvl) + "&bull; | " + data.Lvl + " | " + data.ProductCategoryDesc.Name);
                }
                catch { }
            }
        }

        /// <summary>
        /// Pager
        /// <summary>
        public void pager_Command(object sender, CommandEventArgs e)
        {
            this.currentPageIndex = Convert.ToInt32(e.CommandArgument);
            pager.CurrentIndex = this.currentPageIndex;
            Search();
            //this.GetList(1, string.Empty, this.currentPageIndex, Constant.DSC.PageSize);
        }

        #endregion
    }
}