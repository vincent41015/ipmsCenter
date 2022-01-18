using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class data3 : System.Web.UI.Page
{
    DBAcess db = new DBAcess();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }


    protected string FormatDate(object date)
    {
        if (date == DBNull.Value)
        {
            return "n/a";
        }
        try
        {
            return ((DateTime)date).ToDateTimeSortString();
        }
        catch
        {
            return "n/a";
        }
    }
    protected string FormatMoney(object amount)
    {
        if (amount == DBNull.Value)
        {
            return String.Format("{0:C}", 0);
        }
        return String.Format("{0:C}", amount);
    }
    protected string FormatArea(object area)
    {
        if (area == DBNull.Value)
        {
            return String.Format("{0:C}", 0);
        }
        return String.Format("{0:C}", area);
    }


    

    
}
