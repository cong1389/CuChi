using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Globalization;

namespace Cb.Utility
{
    public class DGCUserControl : UserControl
    {
        #region Field
        private string template_path;
        private string langId;
        private int langInt;
        private CultureInfo ci;
        #endregion

        #region Properties

        public CultureInfo Ci
        {
            get { return ci; }
            set { ci = value; }
        }

        public int LangInt
        {
            get { return langInt; }
            set { langInt = value; }
        }


        public string LangId
        {
            get { return langId; }
            set { langId = value; }
        }

        public string Template_path
        {
            get { return template_path; }
            set { template_path = value; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            Page p = this.Page;
            //template_path = Utils.GetValueProperties(p, "Template_path").ToString();
            //langId = Utils.GetValueProperties(p, "LangId").ToString();
            //LangInt = Convert.ToInt32(Utils.GetValueProperties(p, "LangInt"));
            //ci = Utils.GetValueProperties(p, "Ci") as CultureInfo;

            template_path = WebUtils.GetBaseUrl();
            langId = Utils.GetParameter("langid", Constant.DB.langVn);
            this.ci = WebUtils.getResource(langId);
            langInt = langId == Constant.DB.langVn ? 1 : 1;


            //template_path = WebUtils.GetBaseUrl();
            //langId = Constant.DB.langVn;
            //this.ci = WebUtils.getResource(langId);
            //langInt = langId == Constant.DB.langVn ? 1 : 2;

            //langId = Utils.GetParameter("langid", Constant.DB.langVn);
            //this.ci = WebUtils.getResource(langId);
            //langInt = langId == Constant.DB.langVn ? 1 : 2;
        }
    }
}
