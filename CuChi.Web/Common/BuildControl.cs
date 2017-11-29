using Cb.BLL;
using Cb.DBUtility;
using Cb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Cb.Web.Common
{
    public static class BuildControl
    {
        static int total;

        /// <summary>
        /// Bind dropdownlist
        /// </summary>
        /// <param name="langId">Id ngôn ngữ</param>
        /// <param name="drp"></param>
        /// <param name="type">enum loại drp </param>
        /// <param name="isFirstRow">có cần thêm dòng đầu tiên không</param>
        /// <param name="emptyFirstRow">text dòng đầu</param>
        /// <param name="param">Biến điều kiện</param>
        public static void BuildDropDownList(int langId, DropDownList drp, Common.UtilityLocal.List type, bool isFirstRow, string emptyFirstRow, string param)
        {
            switch (type)
            {
                //VAT
                case Common.UtilityLocal.List.VatGroup:
                    VatGroupBLL vatGroupBLL = new VatGroupBLL();
                    drp.Items.Clear();
                    if (isFirstRow)
                    {
                        drp.Items.Add(new ListItem(emptyFirstRow, "-1"));
                    }
                    IList<PNK_VatGroup> lstVatGroup = vatGroupBLL.GetVatGroupAll();

                    if (lstVatGroup != null && lstVatGroup.Count > 0)
                    {
                        foreach (PNK_VatGroup item in lstVatGroup)
                        {
                            drp.Items.Add(new ListItem(item.Name, DBConvert.ParseString(item.Id)));
                        }
                    }
                    break;
            }
        }
    }
}