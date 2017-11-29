using System;
using System.Collections.Generic;
using System.Linq;
using Cb.IDAL;
using Cb.DALFactory;
using Cb.Model;
using Cb.DBUtility;
using System.Data;
using System.Configuration;
using Cb.Utility;

namespace Cb.BLL
{
    [Serializable]
    public class BookingBLL
    {
        private static IGeneric2C<PNK_Booking, PNK_BookingDesc> dal_2C;

        private string prefixParam;

        public BookingBLL()
        {
            Type t = typeof(Cb.SQLServerDAL.Generic2C<PNK_Booking, PNK_BookingDesc>);
            dal_2C = DataAccessGeneric2C<PNK_Booking, PNK_BookingDesc>.CreateSession(t.FullName);

            switch (ConfigurationManager.AppSettings["Database"])
            {
                case "SQLServer":
                    prefixParam = "@";
                    break;
                case "MySQL":
                    prefixParam = "v_";
                    break;
            }
        }

        public IList<PNK_Booking> GetList(int langId, string name, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, out int total)
        {
            IList<PNK_Booking> lst = new List<PNK_Booking>();
            DGCParameter[] param = new DGCParameter[6];

            if (langId != int.MinValue)
                param[0] = new DGCParameter(string.Format("{0}langId", prefixParam), DbType.Int16, langId);
            else
                param[0] = new DGCParameter(string.Format("{0}langId", prefixParam), DbType.Int16, DBNull.Value);

            if (!string.IsNullOrEmpty(name))
                param[1] = new DGCParameter(string.Format("{0}name", prefixParam), DbType.String, name);
            else
                param[1] = new DGCParameter(string.Format("{0}name", prefixParam), DbType.String, DBNull.Value);

            if (pageIndex != int.MinValue)
                param[2] = new DGCParameter(string.Format("{0}pageIndex", prefixParam), DbType.Int32, pageIndex);
            else
                param[2] = new DGCParameter(string.Format("{0}pageIndex", prefixParam), DbType.Int32, DBNull.Value);

            if (pageSize != int.MinValue)
                param[3] = new DGCParameter(string.Format("{0}pageSize", prefixParam), DbType.Int32, pageSize);
            else
                param[3] = new DGCParameter(string.Format("{0}pageSize", prefixParam), DbType.Int32, DBNull.Value);

            if (fromDate !=DateTime.MinValue)
                param[4] = new DGCParameter(string.Format("{0}fromDate", prefixParam), DbType.DateTime, fromDate);
            else
                param[4] = new DGCParameter(string.Format("{0}fromDate", prefixParam), DbType.DateTime, DBNull.Value);

            if (toDate != DateTime.MinValue)
                param[5] = new DGCParameter(string.Format("{0}toDate", prefixParam), DbType.DateTime, toDate);
            else
                param[5] = new DGCParameter(string.Format("{0}toDate", prefixParam), DbType.DateTime, DBNull.Value);

            //string[] keyArr = param.Select(x => x.Value.ToString()).ToArray();
            //string key = string.Format("Booking_{0}_{1}", WebUtils.CurrentUserIP, string.Join("_", keyArr));            
            //total = 0;
            //////Get cache
            //if (!CacheHelper.Get(key, out lst))
            //{
                lst = dal_2C.GetList("Booking_Get", param, out total);
            //    CacheHelper.Add(lst, key);
            //}
            //else
            //{
            //    total = lst.Count();
            //}

           
            return lst;
        }
    }
}
