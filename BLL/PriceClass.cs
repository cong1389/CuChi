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
    public class PriceClassBLL
    {
        private static IGeneric<PNK_PriceClass> dal;
        private string prefixParam;
        public PriceClassBLL()
        {
            Type t = typeof(Cb.SQLServerDAL.Generic<PNK_PriceClass>);
            dal = DataAccessGeneric<PNK_PriceClass>.CreateSession(t.FullName);

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

        public IList<PNK_PriceClass> GetList()
        {
            IList<PNK_PriceClass> lst = new List<PNK_PriceClass>();
            PNK_PriceClass bookingPrice = new PNK_PriceClass();
            lst = dal.GetAllBy(bookingPrice, "where 1=1", null);

            return lst;
        }
    }
}
