// =============================================
// Author:		Congtt
// Create date: 22/09/2014
// Description:	danh sach booking
// =============================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cb.Utility;
using Cb.DBUtility;
using Cb.BLL;
using Cb.Localization;
using System.Configuration;
using Cb.Model;
using System.Globalization;

namespace Cb.Web.Admin.Pages.Booking
{
    public partial class admin_booking : System.Web.UI.UserControl
    {
        #region Fields

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
        protected string show_msg;
        protected string l_search;
        protected string records;
        protected string msg_no_selected_item;
        protected string msg_confirm_delete_item;
        private string action;
        int total;
        private BookingBLL pcBll
        {
            get
            {
                if (ViewState["pcBll"] != null)
                    return (BookingBLL)ViewState["pcBll"];
                else return new BookingBLL();
            }
            set
            {
                ViewState["pcBll"] = value;
            }
        }

        private Generic<PNK_Booking> genericBLL
        {
            get
            {
                if (ViewState["genericBLLget"] != null)
                    return (Generic<PNK_Booking>)ViewState["genericBLLget"];
                else return new Generic<PNK_Booking>();
            }
            set
            {
                ViewState["genericBLLget"] = value;
            }
        }

        private Generic2C<PNK_Booking, PNK_BookingDesc> generic2CBLL
        {
            get
            {
                if (ViewState["generic2CBLL"] != null)
                    return (Generic2C<PNK_Booking, PNK_BookingDesc>)ViewState["generic2CBLL"];
                else return new Generic2C<PNK_Booking, PNK_BookingDesc>();
            }
            set
            {
                ViewState["generic2CBLL"] = value;
            }
        }

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

        #endregion

        #endregion

        #region Common

        /// <summary>
        /// Init page
        /// </summary>
        private void InitPage()
        {
            pcBll = new BookingBLL();
            genericBLL = new Generic<PNK_Booking>();
            generic2CBLL = new Generic2C<PNK_Booking, PNK_BookingDesc>();
            this.template_path = WebUtils.GetWebPath();
            msg_confirm_delete_item = LocalizationUtility.GetText("mesConfirmDelete");
            msg_no_selected_item = LocalizationUtility.GetText("mesSelectItem");
            LocalizationUtility.SetValueControl(this);

            GetMessage();

            //Set default datetime
            DateTime now = DateTime.Now;
            txtFromDate.Text = string.Format("{0:dd/MM/yyyy}", new DateTime(now.Year, now.Month, 1));
            txtToDate.Text = string.Format("{0:dd/MM/yyyy}", new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month)));

            //btnSearchDatetime_ServerClick(null,null);
        }

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private int GetList(byte langid, string content, DateTime fromDate, DateTime toDate, string cateId, int begin, int end)
        {
            IList<PNK_Booking> lst = pcBll.GetList(langid, content, fromDate, toDate, begin, end, out total);
            this.records = DBConvert.ParseString(lst.Count);
            this.pager.PageSize = DBConvert.ParseInt(ConfigurationManager.AppSettings["PageSizeAdminCate"]);
            this.pager.ItemCount = total;
            this.rptResult.DataSource = lst;
            this.rptResult.DataBind();
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
                    string url = LinkHelper.GetAdminLink("booking");
                    Response.Redirect(url);
                    break;
                case "search":
                    pager.CurrentIndex = 1;
                    this.currentPageIndex = 1;
                    Search();
                    break;
            }
        }

        private void Add()
        {
            string url = LinkHelper.GetAdminLink("edit_booking");
            Response.Redirect(url);
        }

        private void Edit(string cid)
        {
            if (cid == null) return;
            string link, url;
            string[] arrStr;
            if (cid.IndexOf(',') >= 0)
            {
                arrStr = cid.Split(',');
                link = LinkHelper.GetAdminLink("edit_booking", arrStr[0]);
                //link = string.Format(SiteNavigation.link_adminPage_editproductcategory, arrStr[0]);
            }
            else
                link = LinkHelper.GetAdminLink("edit_booking", cid);
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
        /// delete
        /// </summary>
        private void Delete(string cid)
        {
            if (cid != null)
            {

                string link, url;

                if (generic2CBLL.Delete(cid))
                    link = LinkHelper.GetAdminMsgLink("booking", "delete");
                else
                    link = LinkHelper.GetAdminMsgLink("booking", "delfail");
                url = Utils.CombineUrl(template_path, link);
                Response.Redirect(url);

            }
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
                    PNK_Booking productCat = new PNK_Booking();
                    productCat.Id = DBConvert.ParseInt(btId.Value);
                    productCat = genericBLL.Load(productCat, new string[] { "Id" });
                    HtmlInputText txtOrder = (HtmlInputText)item.FindControl("txtOrder");
                    if (txtOrder != null)
                    {
                        try
                        {
                            productCat.Ordering = DBConvert.ParseInt(txtOrder.Value);
                            if (productCat.Ordering > 0)
                            {
                                genericBLL.Update(productCat, productCat, new string[] { "Id" });
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

        private void Search()
        {
            GetList(1, string.Empty, DateTime.Now.Date, DateTime.Now.Date, string.Empty, this.currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["PageSizeAdminCate"]));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveOrder();
            string url = Utils.CombineUrl(template_path, LinkHelper.GetAdminLink("booking"));
            Response.Redirect(url);
        }

        private string GetAllChildCategory()
        {
            string arrId = "";
            //BookingCategoryBLL newsCateBll = new BookingCategoryBLL();
            //IList<PNK_BookingCategory> lst = newsCateBll.GetAllChild(DBConvert.ParseInt(drpNewsCategory.SelectedValue), true);
            ////if (lst != null && lst.Count > 0)
            ////{
            ////    foreach (sd_NewsCategory obj in lst)
            ////    {
            ////        arrId += obj.Id + ",";
            ////    }
            ////}
            ////arrId = arrId.EndsWith(",") ? arrId.Remove(arrId.Length - 1, 1) : arrId;
            ////return arrId;
            //arrId = Utils.ArrayToString<PNK_BookingCategory>((List<PNK_BookingCategory>)lst, "Id", ",");
            return arrId;// !string.IsNullOrEmpty(arrId) ? arrId : "-1011";
        }

        #endregion

        #region Event

        /// <summary>
        /// init component
        /// </summary>
        override protected void OnInit(EventArgs e)
        {
            //InitializeComponent();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GetAction();
            if (!IsPostBack)
            {
                InitializeComponent();
                Search();
            }
        }

        private void InitializeComponent()
        {
            InitPage();
        }

        protected void btnFullName_ServerClick(object sender, EventArgs e)
        {
            string strSearch = Request.Form[txtFullName.ClientID.Replace('_', '$')];
            strSearch = strSearch == null ? string.Empty : Utils.RemoveUnicode(SanitizeHtml.Sanitize(strSearch));

            IList<PNK_Booking> lst = pcBll.GetList(1, string.Empty, DateTime.MinValue, DateTime.MinValue, 1, 9999, out total);
            lst = lst.Where(m => m.FirstName.ToLower().Contains(strSearch.ToLower()) || m.LastName.ToLower().Contains(strSearch.ToLower())).ToList();

            this.records = DBConvert.ParseString(lst.Count);
            this.pager.PageSize = DBConvert.ParseInt(ConfigurationManager.AppSettings["PageSizeAdmin"]);
            this.pager.ItemCount = lst.Count;
            this.rptResult.DataSource = lst;
            this.rptResult.DataBind();
        }

        protected void btnSearchPhone_ServerClick(object sender, EventArgs e)
        {
            string strSearch = Request.Form[txtSearchPhone.ClientID.Replace('_', '$')];
            strSearch = strSearch == null ? string.Empty : Utils.RemoveUnicode(SanitizeHtml.Sanitize(strSearch));

            IList<PNK_Booking> lst = pcBll.GetList(1, string.Empty, DateTime.MinValue, DateTime.MinValue, 1, 9999, out total);
            lst = lst.Where(m => m.PhoneNumber == strSearch).ToList();

            this.records = DBConvert.ParseString(lst.Count);
            this.pager.PageSize = DBConvert.ParseInt(ConfigurationManager.AppSettings["PageSizeAdmin"]);
            this.pager.ItemCount = lst.Count;
            this.rptResult.DataSource = lst;
            this.rptResult.DataBind();
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
                    PNK_Booking data = (PNK_Booking)e.Item.DataItem;

                    //Role
                    Literal ltr = null;
                    ltr = (Literal)e.Item.FindControl("ltrchk");
                    ltr.Text = string.Format(@"<INPUT class='txt' TYPE='checkbox' ID='cb{0}' NAME='cid[]' value='{1}' onclick='isChecked(this.checked);' >",
                                                e.Item.ItemIndex, data.Id);

                    //ltrNewsCategory
                    ltr = (Literal)e.Item.FindControl("ltrNewsCategory");

                    //image
                    if (data.Published == "1")
                    {
                        img = "tick.png";
                        alt = LocalizationUtility.GetText(ltrAdminPublish.Text);
                        publishedTask = "unpublish";
                    }
                    else
                    {
                        img = "publish_x.png";
                        alt = LocalizationUtility.GetText(ltrAdminPublish.Text);
                        publishedTask = "publish";
                    }

                    //Order
                    txt = (HtmlInputText)e.Item.FindControl("txtOrder");
                    txt.Value = DBConvert.ParseString(data.Ordering);

                    //Id
                    HtmlInputButton btId = (HtmlInputButton)e.Item.FindControl("btId");
                    btId.Value = DBConvert.ParseString(data.Id);

                    //set link
                    HyperLink hdflink = new HyperLink();
                    hdflink = (HyperLink)e.Item.FindControl("hdflink");
                    hdflink.NavigateUrl = template_path + LinkHelper.GetAdminLink("edit_booking", data.Id);
                    ImageButton imgctr = (ImageButton)e.Item.FindControl("btnPublish");
                    imgctr.ImageUrl = string.Format("/Admin/images/{0}", img);
                    imgctr.Attributes.Add("alt", alt);
                    HtmlTableCell btn = (HtmlTableCell)e.Item.FindControl("tdbtn");
                    btn.Attributes.Add("onclick", string.Format(" return listItemTask('cb{0}', '{1}')", e.Item.ItemIndex, publishedTask));

                    //Name
                    ltr = (Literal)e.Item.FindControl("ltrName");
                    ltr.Text = string.IsNullOrWhiteSpace(data.FirstName) || string.IsNullOrWhiteSpace(data.LastName) ? "[No name]" : string.Format("{0} {1}", data.FirstName, data.LastName);// data.FullName;

                    //Phone
                    ltr = (Literal)e.Item.FindControl("txtPhone");
                    ltr.Text = data.PhoneNumber;
                }
                catch { }

            }
        }

        protected void btnSearchDatetime_ServerClick(object sender, EventArgs e)
        {
            DateTime fromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string strFromDate = fromDate.ToString("MM/dd/yyyy");

            DateTime toDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string strToDate = toDate.ToString("MM/dd/yyyy");

            GetList(1, string.Empty, DBConvert.ParseDateTime(strFromDate, "MM/dd/yyyy"), DBConvert.ParseDateTime(strToDate, "MM/dd/yyyy"), string.Empty, this.currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["PageSizeAdminCate"]));
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