using Cb.BLL;
using Cb.DBUtility;
using Cb.Model;
using Cb.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cb.Web.Admin.Pages.TourPriceClass
{
    public partial class admin_tourpriceclass : DGCUserControl
    {
        #region Parameter

        IList<PNK_PriceClass> lst;

        public string ProductId
        {
            get
            {
                if (ViewState["CategoryId"] != null)
                    return ViewState["CategoryId"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["CategoryId"] = value;
            }
        }
        int total;


        #endregion

        #region Common

        private void InitPage()
        {

            BindData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="type">==NULL mặc định hình</param>
        /// <param name="type">==1:map</param>
        private void BindData()
        {
            PriceClassBLL bll = new PriceClassBLL();
            lst = bll.GetList();
            if (lst.Count() > 0)
            {
                grdTourPriceClass.DataSource = lst;
                grdTourPriceClass.DataBind();

                //grdTourPriceClass.Columns[4].Visible = true;
            }
            else
            {
                PNK_PriceClass pnk = new PNK_PriceClass();
                DataTable dt = Common.UtilityLocal.ObjectToData(pnk);
                grdTourPriceClass.DataSource = dt;
                grdTourPriceClass.DataBind();

                //grdTourPriceClass.Columns[4].Visible = false;
                foreach (GridViewRow row in grdTourPriceClass.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        LinkButton lb = ((LinkButton)row.FindControl("lnkRemove"));
                        if (lb != null)
                        {
                            lb.Visible = false;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Save location
        /// </summary>
        private int Save(string id, string name)
        {
            int priceId = DBConvert.ParseInt(id);
            PNK_PriceClass productcatObj = new PNK_PriceClass();
            Generic<PNK_PriceClass> genericBLL = new Generic<PNK_PriceClass>();

            if (priceId == int.MinValue)
            {
                productcatObj.Name = name;   

                //excute             
                genericBLL.Insert(productcatObj);
            }
            else
            {
                productcatObj.Name = name;              
                productcatObj.ID = priceId;      

                //excute
                genericBLL.Update(productcatObj, productcatObj, new string[] { "Id" });
                grdTourPriceClass.EditIndex = -1;
            }

            BindData();

            return priceId;
        }

        #endregion

        #region Event

        protected void AddNewCustomer(object sender, EventArgs e)
        {
            string name = ((TextBox)grdTourPriceClass.FooterRow.FindControl("txtName")).Text;     
            Save(string.Empty, name);
        }

        protected void grdTourPriceClass_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindData();
            grdTourPriceClass.PageIndex = e.NewPageIndex;
            grdTourPriceClass.DataBind();
        }

        protected void grdTourPriceClass_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdTourPriceClass.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void grdTourPriceClass_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdTourPriceClass.EditIndex = -1;
            BindData();
        }

        protected void grdTourPriceClass_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string priceId = ((Label)grdTourPriceClass.Rows[e.RowIndex].FindControl("lbID")).Text;          
            string name = ((TextBox)grdTourPriceClass.Rows[e.RowIndex].FindControl("txtName")).Text;           

            Save(priceId, name);
        }

        protected void grdTourPriceClass_RowDeleted(object sender, GridViewDeleteEventArgs e)
        {
            string priceId = grdTourPriceClass.DataKeys[e.RowIndex].Value.ToString();
            Generic<PNK_PriceClass> genericBLL = new Generic<PNK_PriceClass>();
            genericBLL.Delete(priceId);

            BindData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        #endregion
        
    }
}