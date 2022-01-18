using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class data7 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["login_status"] == null)
        {
            Response.Redirect("login.aspx");
        }

        initContent();
    }

    private void initContent()
    {
       

    }
}
