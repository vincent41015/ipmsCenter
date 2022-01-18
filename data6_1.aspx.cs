using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class data6_1 : System.Web.UI.Page
{
    
    DBAcess dbaccess = new DBAcess();
    //checkPermissions check = new checkPermissions();
   
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            queryrole();
            querydata();

            dbaccess.setSystemOperate("Web", Session["LoginName"] + "", "瀏覽", "權限管理/使用者管理", "");

        }
        else {
            string target = Request["__EVENTTARGET"]; // parameter
            string parameter = Request["__EVENTARGUMENT"]; // parameter

            if (target == "Del") { 
                // do Delete 
                doDel(parameter);
            }
            else if (target == "Edit") { 
                //do Edit
                
                doEdit(parameter);
            }
        }        
        

    }
    
    protected void querydata()
    {
        DataTable dt = new DataTable();
        int r_level = (int)dbaccess.executeScalar("SELECT r_level FROM  s_Role where r_id = '"+Session["r_id"].ToString()+"' ");


        dt = dbaccess.query("SELECT p.*, r.* FROM s_Permissions p  LEFT JOIN s_Role r ON p.r_id = r.r_id where r.r_level <  '" + r_level + "' or p.Account = '"+Session["Account"]+"'");
        //GridView1.DataSource = dt;
        //GridView1.DataBind();


        Repeater1.DataSource = dt;
        Repeater1.DataBind();

        dt.Dispose();
    }


    protected void doDel(string p_id) {

        dbaccess.excute("DELETE FROM s_Permissions WHERE p_id = '"+p_id+"'");

        querydata();

        dbaccess.setSystemOperate("Web", Session["LoginName"] + "", "刪除使用者", "權限管理/使用者管理", "id="+p_id);
    }

    protected void doEdit(string p_id)
    {

        
        DataTable dt = dbaccess.query("SELECT Account, Password, r_id, username, remark FROM s_Permissions WHERE p_id = '" + p_id + "'");
        //Response.Write(dt.Rows.Count);

        if (dt != null && dt.Rows.Count > 0) {
            //ViewState["id"] = p_id;
            //txt_account.Text = dt.Rows[0]["Account"].ToString().Trim();
            //txt_account.Enabled = false;
            //txt_UserName.Text = dt.Rows[0]["username"].ToString().Trim();
            //txt_remark.Text = dt.Rows[0]["remark"].ToString().Trim();
            //queryrole();
            //ListItem li_role = ddl_role.Items.FindByValue(dt.Rows[0]["r_id"].ToString().Trim());
            //if (li_role != null)
            //{
            //    li_role.Selected = true;
            //}

            ViewState["id"] = p_id;
            txt_account.Text = dt.Rows[0]["Account"].ToString().Trim();
            txt_account.Enabled = false;
            txt_UserName.Text = dt.Rows[0]["username"].ToString().Trim();
            txt_remark.Text = dt.Rows[0]["remark"].ToString().Trim();

            txt_Password.Attributes.Add("value", dt.Rows[0]["Password"].ToString().Trim());// = dt.Rows[0]["Password"].ToString().Trim();
            txt_Password2.Attributes.Add("value", dt.Rows[0]["Password"].ToString().Trim());// = dt.Rows[0]["Password"].ToString().Trim();

            //queryrole();
            ListItem li_role = ddl_role.Items.FindByValue(dt.Rows[0]["r_id"].ToString().Trim());
            if (li_role != null)
            {
                li_role.Selected = true;
            }

            btn_save.Visible = true;
            btn_add.Visible = false;
            ddl_role.Enabled = false;
        }

    }

    protected void queryrole()
    {
        int r_level = (int)dbaccess.executeScalar("SELECT r_level FROM  s_Role where r_id = '" + Session["r_id"].ToString() + "' ");

        ddl_role.Items.Clear();
        DataTable dt = new DataTable();
        dt = dbaccess.query("SELECT * FROM s_Role where r_level < '"+r_level+"'");
        if (dt.Rows.Count > 0)
        {
            ddl_role.DataTextField = "r_name";
            ddl_role.DataValueField = "r_id";
            ddl_role.DataSource = dt;
            ddl_role.DataBind();
        }
        ddl_role.Items.Insert(0, new ListItem("請選擇", ""));
        dt.Dispose();
    }
    

    protected void btn_save_Click(object sender, EventArgs e)
    {
        
        string id_1 = string.Empty;
        if (ViewState["id"] != null)
            id_1 = ViewState["id"].ToString().Trim();
        //db.update_Permissions(ddl_role.SelectedValue, txt_account.Text.Trim(), txt_UserName.Text.Trim(), id_1, txt_remark.Text.Trim(), txt_Password.Text.Trim());

        dbaccess.excute( "UPDATE s_Permissions SET  username='"+txt_UserName.Text+"', remark = '"+txt_remark.Text+"', r_id = '"+ddl_role.SelectedValue+"'  WHERE p_id = '"+id_1+"' " );

        dbaccess.setSystemOperate( "Web" ,Session["LoginName"] + "", "Edit", "權限管理/使用者編輯", "P_id:" + id_1 + ",role:" + ddl_role.SelectedValue + ",username:" + txt_UserName.Text + ",remark:" + txt_remark.Text);

        //queryrole();
        txt_account.Text = string.Empty;
        txt_UserName.Text = string.Empty;
        txt_remark.Text = string.Empty;
        txt_Password.Text = string.Empty;
        txt_Password2.Text = string.Empty;
        querydata();
        btn_save.Visible = false;
        btn_add.Visible = true;
        txt_account.Enabled = true;
    }
    
    protected void btn_add_Click(object sender, EventArgs e)
    {
        //db.setSystemOperate(Session["LoginName"] + "", "新增", "權限管理");
        //db.innsert_Permissions(ddl_role.SelectedValue, txt_account.Text.Trim(), txt_UserName.Text.Trim(), txt_remark.Text.Trim(), txt_Password.Text.Trim());

        //檢查帳號是否存在
        if ((int)dbaccess.executeScalar("SELECT count( account ) FROM s_Permissions WHERE account = '" + txt_account.Text + "'") == 1) {
            string info = "<script>alert(' Account Exist ');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "info", info);
            return;
        }


        dbaccess.excute("INSERT INTO  s_Permissions( Account, Password, username, remark, r_id, utime ) VALUES( '" + txt_account.Text + "', '" + txt_Password.Text + "', '" + txt_UserName.Text + "', '" + txt_remark.Text + "', '" + ddl_role.SelectedValue + "', getdate() ) ");


        dbaccess.setSystemOperate("Web", Session["LoginName"] + "", "INSERT", "權限管理/使用者編輯", "account:" + txt_account.Text +",role:" + ddl_role.SelectedValue + ",username:" + txt_UserName.Text + ",remark:" + txt_remark.Text);
        queryrole();
        txt_account.Text = string.Empty;
        txt_UserName.Text = string.Empty;
        txt_remark.Text = string.Empty;
        txt_Password.Text = string.Empty;
        txt_Password2.Text = string.Empty;

        querydata();
    }


    protected void itemDataBoundRepeater_ItemDataBound(object source, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            //Button btn_del = (Button)e.Item.FindControl("Btn_del");
            //btn_del.Attributes.Add("onclick", "javascript:return window.confirm('確認更改嗎?')");
            //btn_del.Attributes.Add("CommandArgument", "<%#Eval('m_id')%>");


        }
    }

    protected void Repeater1_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        //string licenseplate = e.CommandArgument.ToString();
        //dbaccess.excute("UPDATE CarList SET dtime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',  area = CASE area WHEN '2P' THEN 'OUT' ELSE 'OUT-7P' END WHERE licenseplate = '" + licenseplate + "' ");
        //loaddata();
    }
}
