using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class data4 : System.Web.UI.Page
{
    
   
    DBAcess dbaccess = new DBAcess();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.Title = "IPMS 2.0 智慧型停車場管理系統 - 車輛停放位置查詢";
        

        if (!IsPostBack) {
            //db.setSystemOperate(Session["LoginName"] + "", "瀏覽", "車位查詢");
            dbaccess.setSystemOperate( "web", Session["LoginName"].ToString(), "瀏覽", "停車系統", "");
        }
        
    }

    
}
