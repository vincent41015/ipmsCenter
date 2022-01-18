using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class data6 : System.Web.UI.Page
{
    DBAcess db = new DBAcess();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            db.setSystemOperate("Web", Session["LoginName"] + "", "瀏覽", "權限管理", "");

        }    
        
        
    }
}
