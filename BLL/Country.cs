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
using Cb.Utility;

namespace Cb.BLL
{
    [Serializable]
    public class CountryBLL
    {
        private static IGeneric2C<PNK_Country, PNK_CountryDesc> dal_2C;

        private string prefixParam;

        public CountryBLL()
        {
            Type t = typeof(Cb.SQLServerDAL.Generic2C<PNK_Country, PNK_CountryDesc>);
            dal_2C = DataAccessGeneric2C<PNK_Country, PNK_CountryDesc>.CreateSession(t.FullName);

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

        public IList<PNK_Country> GetList(int langId, string name, string Id, string newsCateId, int pageIndex, int pageSize, out int total)
        {
            IList<PNK_Country> lst = new List<PNK_Country>();
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

            if (!string.IsNullOrEmpty(newsCateId))
                param[4] = new DGCParameter(string.Format("{0}cateId", prefixParam), DbType.String, newsCateId);
            else
                param[4] = new DGCParameter(string.Format("{0}cateId", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(Id))
                param[5] = new DGCParameter(string.Format("{0}Id", prefixParam), DbType.String, Id);
            else
                param[5] = new DGCParameter(string.Format("{0}Id", prefixParam), DbType.String, DBNull.Value);

            string[] keyArr = param.Select(x => x.Value.ToString()).ToArray();
            string key = string.Format("Country_Get_{0}_{1}", WebUtils.CurrentUserIP, string.Join("_", keyArr));
            Dictionary<string, object> dic = null;
            total = 0;

            ////Get cache
            if (!CacheHelper.Get(key, out lst))
            {
                lst = dal_2C.GetList("Country_Get", param, out total);
                CacheHelper.Add(lst, key);
            }
            else
            {
                total = lst.Count();
            }

            ////Get cache
            //if (!CacheHelper.Get(key, out dic))
            //{
            //    lst = dal_2C.GetList("Country_Get", param, out total);

            //    dic = new Dictionary<string, object>();
            //    dic.Add("Dict_Country_Get", lst);
            //    dic.Add("Dict_Country_Get_Total", total);
            //    CacheHelper.Add(dic, key);
            //}
            //if (dic != null && dic.Count > 0)
            //{
            //    lst = dic["Dict_Country_Get"] as IList<PNK_Country>;
            //    total = DBConvert.ParseInt(dic["Dict_Country_Get_Total"]);
            //}
            
            return lst;
        }
    }
}
