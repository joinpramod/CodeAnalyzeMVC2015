

using System.Web.UI.WebControls;
using System;
using System.Drawing;


    public partial class Info : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            LinkButton btnInfo = (LinkButton)this.Master.FindControl("btnInfo");
            // btnInfo.Font.Size = 24;
            btnInfo.ForeColor = Color.Yellow;
            // Panel pnlWebDetails = (Panel)this.Master.FindControl("pnlWebDetails");
            //  pnlWebDetails.Visible = false;
        }

    }



