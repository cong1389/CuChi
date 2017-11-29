using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cb.IDAL;
using Cb.DALFactory;
using Cb.Model;
using Cb.DBUtility;
using System.Data;
using System.Configuration;

namespace Cb.BLL
{
    [Serializable]
    public class BookingPriceBLL
    {
        private static IGeneric<PNK_BookingPrice> dal;
        private string prefixParam;
        public BookingPriceBLL()
        {
            Type t = typeof(Cb.SQLServerDAL.Generic<PNK_BookingPrice>);
            dal = DataAccessGeneric<PNK_BookingPrice>.CreateSession(t.FullName);

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

        public IList<PNK_BookingPrice> GetList(int productId, out int total)
        {
            IList<PNK_BookingPrice> lst = new List<PNK_BookingPrice>();
            DGCParameter[] param = new DGCParameter[1];

            if (productId != int.MinValue)
                param[0] = new DGCParameter(string.Format("{0}productId", prefixParam), DbType.Int32, productId);
            else
                param[0] = new DGCParameter(string.Format("{0}productId", prefixParam), DbType.Int32, DBNull.Value);

            lst = dal.GetList("BookingPrice_Get", param, out total);
            return lst;
        }
    }
}
