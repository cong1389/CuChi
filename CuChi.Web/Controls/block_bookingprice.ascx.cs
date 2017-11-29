using Cb.DBUtility;
using Cb.Utility;
using System;
using System.Data;
using System.Text;
using System.Web.UI;

namespace Cb.Web.Controls
{
    public partial class block_bookingprice : DGCUserControl
    {
        #region Parameter
        /// <summary>
        /// Type default của Standart price=1;
        /// </summary>
        static int type = 1;

        protected string template_path, pageName, cid, cidsub, id;
        int total;

        private int productId = int.MinValue;
        public int ProductId
        {
            get
            {
                if (productId != int.MinValue)
                    return productId;
                else
                    return int.MinValue;
            }
            set
            {
                productId = value;
            }
        }

        #endregion

        #region Common

        private void InitPage()
        {
            template_path = WebUtils.GetWebPath();
            pageName = Utils.GetParameter("page", "home");
            cid = Utils.GetParameter("cid", string.Empty);
            cidsub = Utils.GetParameter("cidsub", string.Empty);
            id = Utils.GetParameter("id", string.Empty);

            BindData();
        }

        private void BindData()
        {
            StringBuilder sbHeader = new StringBuilder();
            StringBuilder sbColumn = new StringBuilder();
            StringBuilder sbRow = new StringBuilder();

            DGCParameter[] param = new DGCParameter[2];
            param[0] = new DGCParameter("@productId", DbType.Int32, ProductId); ;
            param[1] = new DGCParameter("@total", DbType.Int32, total); ;
            DataTable dtb = DBHelper.ExcuteFromStore("BookingPrice_Table_Get", param);

            if (dtb != null && dtb.Rows.Count > 0)
            {
                foreach (DataColumn column in dtb.Columns)
                {
                    sbHeader.AppendFormat("<th class='text-center'>{0}</th>", column.ColumnName);
                }
                ltrHeader.Text = sbHeader.ToString();

                for (int r = 0; r < dtb.Rows.Count; r++)
                {
                    sbColumn = new StringBuilder();
                    for (int c = 0; c < dtb.Columns.Count; c++)
                    {
                        string price = dtb.Rows[r][c].ToString() == string.Empty ? "Call" : string.Format("${0}", dtb.Rows[r][c].ToString());
                        string td = c == 0 ? string.Format("<td class='text-center'>{0}</td>", dtb.Rows[r][c].ToString()) : string.Format("<td class='text-center'>{0}</td>", price);
                        sbColumn.AppendFormat(td);
                    }
                    sbRow.AppendFormat("<tr class='text-center'>{0}</tr>", sbColumn);
                }
                ltrRows.Text = sbRow.ToString();

                //foreach (DataRow row in dtb.Rows)
                //{
                //    c = 0;
                //    foreach (DataColumn column in dtb.Columns)
                //    {
                //        sbHeader.AppendFormat("<th class='text-center'>{0}</th>", column.ColumnName);
                //        string dola = c > 0 ? string.Format("<td class='text-center'>$ {0}</td>", row[column].ToString()) : string.Format("<td class='text-center'>{0}</td>", row[column].ToString());
                //        sbRow.AppendFormat(dola);
                //        c++;
                //    }
                //    sbRows.Append(sbRow);
                //}
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

        #endregion
    }
}