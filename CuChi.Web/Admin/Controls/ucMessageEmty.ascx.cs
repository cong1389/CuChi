using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cb.Web.Admin.Controls
{
    public partial class ucMessageEmty : System.Web.UI.UserControl
    {
        private string messageContent;

        public string MessageContent
        {
            get { return messageContent; }
            set { messageContent = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ltrContent.Text = messageContent;
        }
    }
}