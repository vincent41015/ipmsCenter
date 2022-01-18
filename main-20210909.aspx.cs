using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

public partial class main : System.Web.UI.Page
{
    

    DBAcess db = new DBAcess();

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        //loadData();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
                

        if (!IsPostBack)
        {
            DataTable dt = db.query(@"SELECT amount_1F as yline, amount_B1F as yline1, amount_B2F as yline2,amount_B3F as yline3,amount_B4F as yline4, amount_B4FVendor as yline5, amount_1FVendor as yline6 , dtime as xline 
            FROM Log_RedLineCount where dtime > DATEADD( day, -7, getdate() ) order by dtime");

            string script = "window.onload = function() { drawchart(" + JsonConvert.SerializeObject(dt, Formatting.Indented) + "  ); };";
            ClientScript.RegisterStartupScript(this.GetType(), "UpdateTime", script, true);
        }

        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JustAlert", "openModal1();", true);
        //月日顯示
        DateTime nowday = DateTime.Now;
        Li_Month.Text = nowday.ToString("MMM", new CultureInfo("en-US"));
        Li_Day.Text = nowday.ToString("dd");

    }

    
    

    protected void itemDataBoundRepeater_ItemDataBound(object source, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            //Button btn_del = (Button)e.Item.FindControl("Btn_del");
            //if (btn_del != null)
            //    btn_del.Attributes.Add("onclick", "javascript:return window.confirm('確認更改嗎?')");
            //btn_del.Attributes.Add("CommandArgument", "<%#Eval('licenseplate')%>");
        }
    }


    


   

    

    protected void Button1_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JustAlert", "<script>openModal1();</script>", true);
        //ClientScript.RegisterStartupScript(this.GetType(), "JustAlert", "<script>openModal1();</script>");
    }

    protected void Chart1_Load1(object sender, EventArgs e)
    {
//        DataTable dt = db.query(@"SELECT amount_1F as yline, amount_B1F as yline1, amount_B2F as yline2,amount_B3F as yline3,amount_B4F as yline4, cast (dtime as varchar) as xline 
//            FROM Log_RedLineCount where dtime > DATEADD( day, -7, getdate() ) order by dtime");

        

        //foreach (DataRow dr in dt.Rows)
        //{
        //    dr["xline"] = DateTime.Parse(dr["xline"].ToString()).ToString("MM/dd");

        //}

		/*
		DataTable dt = db.query(@"SELECT amount_1F as yline, amount_B1F as yline1, amount_B2F as yline2,amount_B3F as yline3,amount_B4F as yline4, dtime as xline 
            FROM Log_RedLineCount where dtime > DATEADD( day, -7, getdate() ) order by dtime");
        Chart1.Titles["Title1"].Text = "樓層停放數量記錄";
        Chart1.DataSource = dt;


        Chart1.DataBind();*/
    }



}
