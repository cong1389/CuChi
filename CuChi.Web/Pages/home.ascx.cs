using System;
using System.Collections.Generic;
using System.Web.UI;
using Cb.BLL;
using Cb.Model;
using Cb.Utility;
using System.Configuration;

namespace Cb.Web.Pages
{
    public partial class home : DGCUserControl
    {
        #region Parameter

        protected string categoryID, title, metaDescription, metaKeyword, h1, h2, h3, analytic, pageName, cid, cidsub, id;
        private string activeClass = "";
        int total;


        #endregion

        #region Common

        private void InitPage()
        {
            GetCount();
            GetSEO();

            //Big group
            block_category_Daily.CategoryParentIdByPass = ConfigurationManager.AppSettings["parentIdDailyCate"];
            block_category_Daily.CategoryIdByPass = ConfigurationManager.AppSettings["parentIdDaily"];

            //Luxy group
            block_category_Packages.CategoryParentIdByPass = ConfigurationManager.AppSettings["parentIdPackagesCate"];
            block_category_Packages.CategoryIdByPass = ConfigurationManager.AppSettings["parentIdPackages"];

            //Private group
            block_category_Tips.CategoryParentIdByPass = ConfigurationManager.AppSettings["parentIdTipsCate"];
            block_category_Tips.CategoryIdByPass = ConfigurationManager.AppSettings["parentIdTips"];

            //Package
            block_category_package.CategoryParentIdByPass = ConfigurationManager.AppSettings["parentIdPackageCate"];
            block_category_package.CategoryIdByPass = ConfigurationManager.AppSettings["parentIdPackage"];
        }

        /// <summary>
        /// Get count
        /// </summary>
        private void GetCount()
        {
            ContentStaticBLL pcBll = new ContentStaticBLL();
            IList<PNK_ContentStatic> lst = pcBll.GetList(LangInt, string.Empty, ConfigurationManager.AppSettings["contentStatic_WebsiteHot"], string.Empty, 1, 1, out total);
            if (total > 0)
            {
                ltrWellCome.Text = lst[0].ContentStaticDesc.Brief;
            }
        }

        private void GetSEO()
        {
            ConfigurationBLL pcBll = new ConfigurationBLL();
            IList<PNK_Configuration> lst = pcBll.GetList();
            if (lst != null && lst.Count > 0)
            {
                foreach (PNK_Configuration item in lst)
                {
                    if (item.Key_name == Constant.Configuration.config_title)
                    {
                        title = item.Value_name;
                    }
                    else if (item.Key_name == Constant.Configuration.config_metaDescription)
                    {
                        metaDescription = item.Value_name;
                    }
                    else if (item.Key_name == Constant.Configuration.config_metaKeyword)
                    {
                        metaKeyword = item.Value_name;
                    }

                    else if (item.Key_name == Constant.Configuration.config_h1)
                    {
                        h1 = item.Value_name;
                    }
                    else if (item.Key_name == Constant.Configuration.config_h2)
                    {
                        h2 = item.Value_name;
                    }
                    else if (item.Key_name == Constant.Configuration.config_h3)
                    {
                        h3 = item.Value_name;
                    }
                    else if (item.Key_name == Constant.Configuration.config_analytic)
                    {
                        analytic = item.Value_name;
                    }
                }

                WebUtils.SeoPage(title, metaDescription, metaKeyword, this.Page);
                WebUtils.SeoTagH(h1, h2, h3, Controls);

                //Google Analytic
                WebUtils.IncludeJSScript(this.Page, analytic);
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