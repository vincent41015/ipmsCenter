using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class data6_2 : System.Web.UI.Page
{
    DBAcess db = new DBAcess();
    //checkPermissions check = new checkPermissions();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            db.setSystemOperate("Web", Session["LoginName"] + "", "瀏覽", "權限管理/角色編輯", "");
        
            querydata();
            checkbutton();
        }
        else
        {
            string target = Request["__EVENTTARGET"]; // parameter
            string parameter = Request["__EVENTARGUMENT"]; // parameter

            if (target == "Del")
            {
                // do Delete 
            }
            else if (target == "Edit")
            {
                //do Edit

                doEdit(parameter);
            }
            else if (target == "") { 
            
            }
        }        

    }

    protected void doEdit(string r_id) {
        Response.Redirect( "~/data6_3.aspx?id="+r_id );
    }

    protected void checkbutton()
    {
        //    if (check.check("角色編輯", "add_enable", "Pony")) //新增
        //    {
        //        btn_add.Visible = true;
        //    }
        //    else
        //    {
        //        btn_add.Visible = false;
        //    }
        //    if (check.check("角色編輯", "edit_enable", "Pony")) //編輯
        //    {
        //        GridView3.Columns[2].Visible = true;
        //        GridView3.Columns[3].Visible = true;
        //    }
        //    else
        //    {
        //        GridView3.Columns[2].Visible = false;
        //        GridView3.Columns[3].Visible = false;
        //    }
    }
    protected void querydata()
    {
        
        //dt = db.query(db.Select_string("Role"));

        DataTable dt = new DataTable();
        int r_level = (int)db.executeScalar("SELECT r_level FROM  s_Role where r_id = '" + Session["r_id"].ToString() + "' ");

        for (int i = 1; i < r_level; i++)
        {
            ddl_rlevel.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }



        //dt = db.query("SELECT p.*, r.* FROM member p  LEFT JOIN Role r ON p.m_role = r.r_id where r.r_level <=  '" + r_level + "'");


        dt = db.query("SELECT * FROM s_Role WHERE r_level < '" + r_level + "' ");
        GridView3.DataSource = dt;
        GridView3.DataBind();

        Repeater1.DataSource = dt;
        Repeater1.DataBind();

        dt.Dispose();
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RoleEdit")
        {
            
            int index = Convert.ToInt32(e.CommandArgument);
            ViewState["id"] = GridView3.DataKeys[index].Value.ToString().Trim();
            txt_RoleName.Text = GridView3.Rows[index].Cells[1].Text.ToString().Trim();
            txt_Describe.Text = GridView3.Rows[index].Cells[2].Text.ToString().Trim();
            ddl_rtype.SelectedValue = GridView3.Rows[index].Cells[0].Text.ToString();


            if (Session["r_level"].ToString() == GridView3.Rows[index].Cells[3].Text.ToString().Trim())
            {
                ddl_rlevel.Visible = false;
                Lb_rlevel.Visible = true;
                Lb_rlevel.Text = GridView3.Rows[index].Cells[2].Text.ToString().Trim();
            }
            else
            {
                ddl_rlevel.Visible = true;
                Lb_rlevel.Visible = false;
                ddl_rlevel.SelectedIndex = int.Parse(GridView3.Rows[index].Cells[3].Text.ToString().Trim()) - 1;
            }

            btn_save.Visible = true;
            btn_add.Visible = false;
        }
        else if (e.CommandName == "PermisEdit")
        {
            //db.setSystemOperate(Session["LoginName"] + "", "編輯", "角色編輯");
            int index = Convert.ToInt32(e.CommandArgument);
            string id = GridView3.DataKeys[index].Value.ToString().Trim();
            Response.Redirect("data6_3.aspx?id=" + id);
        }
        else if (e.CommandName == "del")
        {
            db.setSystemOperate(Session["LoginName"] + "", "刪除", "角色編輯");
            string id = e.CommandArgument.ToString();
            //db.Del(db.Del_string("Role", "r_id", id));
            db.excute("DELETE FROM Role where r_id = '" + id + "'");
            db.excute("DELETE FROM RF_mapping where r_id = '" + id + "'");
            db.setSystemOperate("Web", Session["LoginName"] + "", "Delete", "權限管理/角色編輯", "Rid:" + id);
            querydata();
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        //db.setSystemOperate(Session["LoginName"] + "", "修改", "角色編輯");
        string id_1 = string.Empty;
        btn_save.Visible = false;
        btn_add.Visible = true;
        if (ViewState["id"] != null)
            id_1 = ViewState["id"].ToString().Trim();

        if (ddl_rtype.SelectedValue == "admin") {
            if (int.Parse(Session["r_level"].ToString()) <= int.Parse(ddl_rlevel.SelectedValue)) { 
                //alter error
                return;
            }
        }


        if (ddl_rlevel.Visible == true)
        {
            db.excute("UPDATE Role SET r_name = '" + txt_RoleName.Text + "', r_describe = '" + txt_Describe.Text + "', r_level = '" + ddl_rlevel.SelectedValue + "' WHERE r_id = '" + id_1 + "'");

            db.setSystemOperate("Web", Session["LoginName"] + "", "Edit", "權限管理/角色編輯", "Rid:" + id_1 + ",r_name:"+txt_RoleName.Text + ", r_describe:" + txt_Describe.Text + ",r_level:" + ddl_rlevel.SelectedValue);

        }
        else
        {
            //db.updateRole(id_1, txt_RoleName.Text, txt_Describe.Text);
        }

        txt_RoleName.Text = string.Empty;
        txt_Describe.Text = string.Empty;
        querydata();

        ddl_rlevel.Visible = true;
        Lb_rlevel.Visible = false;
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        //db.setSystemOperate(Session["LoginName"] + "", "新增", "角色編輯");
        string r_name = txt_RoleName.Text;
        string r_describe = txt_Describe.Text;
        Guid r_id = Guid.NewGuid();
        //db.innsertRole(r_id, r_name, r_describe);
        db.excute("INSERT INTO Role( [r_id],[r_name],[r_describe],[r_level] ) VALUES( '" + r_id + "', '" + r_name + "', '" + r_describe + "', '" + ddl_rlevel.SelectedValue + "' )");
        db.setSystemOperate("Web", Session["LoginName"] + "", "INSERT", "權限管理/角色編輯", "Rid:" + r_id + ",r_name:" + txt_RoleName.Text + ", r_describe:" + txt_Describe.Text + ",r_level:" + ddl_rlevel.SelectedValue);
        txt_RoleName.Text = string.Empty;
        txt_Describe.Text = string.Empty;
        querydata();
    }

    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        querydata();
    }

}
