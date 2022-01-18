using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class data5 : System.Web.UI.Page
{
    DBAcess db = new DBAcess();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack) {
            db.setSystemOperate( "web",  Session["LoginName"].ToString() , "瀏覽", "報表管理", "");
            
        }        
    }   
}
