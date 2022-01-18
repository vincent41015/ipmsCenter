using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class data6_3 : System.Web.UI.Page
{
    DBAcess db = new DBAcess();
    //checkPermissions api = new checkPermissions();
    string id = string.Empty;
    int xx = 0;
    //TSMC_dll.DataManage.PermissionsEdit_role dll_string = new TSMC_dll.DataManage.PermissionsEdit_role();
    protected void Page_Load(object sender, EventArgs e)
    {
        //ViewState["f_id"] = "1";

        //Session["fname"] = "權限管理";
        //Session["f_id"] = 1;

        //if (Session["login_status"] == null || Session["LoginName"] == null || Session["r_id"] == null || Session["r_level"] == null)
        //{
        //    Response.Redirect("login.aspx");
        //}


        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"].ToString().Trim();
                querydata( id );
                //db.excute("INSERT INTO [ITRI].[dbo].[SystemOperate]([datetime],[add_user],[instruction],[function_name], parmList ) VALUES( getdate(), '" + Session["LoginName"] + "', '瀏覽', '權限管理/角色編輯/功能設定', 'RoleID=" + id + "'  )");
                db.setSystemOperate("Web", Session["LoginName"] + "", "瀏覽", "權限管理/角色編輯/功能設定", "R_id:" + id);
            }
        }
    }

    protected void querydata( string rid )
    {
        xx = 0;
        DataTable dt = new DataTable();
        //dt = db.query("SELECT * FROM [Function]b left join [RF_mapping]a on b.f_id=a.f_id where a.r_id='" + id + "'");
        //if (dt.Rows.Count <= 0)
        //{
        //Response.Write(db.Select_string("Function"));
        //dt = db.query(db.Select_string("Function"));
        object rlevel = db.executeScalar( "SELECT r_level FROM s_Role WHERE r_id = '"+ rid +"'" );

        dt = db.query("SELECT * FROM [s_Functions] WHERE Fid>0 AND Flevel <= '"+rlevel+"' and isEnable = 1 ");
        //    xx = 1;
        //}


       
        #region TreeNode

        //TreeNode node = new TreeNode();
        CreateTree(dt, 0);

        //TreeView1.Nodes.Add(node);

        #endregion

        /*
        GridView2.DataSource = dt;
        GridView2.DataBind();
        */
        /*
        DataTable dt2 = new DataTable();
        dt2 = db.query(db.Select_string("RF_mapping", "r_id", id));
        foreach (GridViewRow gr in GridView2.Rows)
        {
            string f_id = GridView2.DataKeys[gr.RowIndex].Value.ToString().Trim();
            CheckBox cb_operate_enable = (CheckBox)gr.FindControl("cb_operate_enable");
            for (int x = 0; x < dt2.Rows.Count; x++)
            {
                if (dt2.Rows[x]["f_id"] + "" == f_id)
                {
                    cb_operate_enable.Checked = Convert.ToBoolean(dt2.Rows[x]["operate_enable"] + "");
                }
            }

        }
        dt2.Dispose();
        */
        dt.Dispose();
        
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (xx == 0)
        //    {

        //        CheckBox cb_operate_enable = (CheckBox)e.Row.FindControl("cb_operate_enable");
        //        cb_operate_enable.Checked = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "operate_enable"));
        //    }
        //}
    }

    private TreeNode CreateTree(TreeNode node, DataTable TreeDT, int fid)
    {

        TreeNode ChildNode;
        foreach (DataRow row in TreeDT.Rows)
        {
            ChildNode = new TreeNode();
            ChildNode.Text = row["Fname"].ToString(); //父的顯示名稱
            ChildNode.Value = row["Fid"].ToString();  //這邊VALUE自己看要設啥
            ChildNode.SelectAction = TreeNodeSelectAction.None;
            ChildNode.ShowCheckBox = true;

            DataTable check = db.query("SELECT * FROM s_RF_mapping WHERE r_id = '" + id + "' AND f_id = '" + row["Fid"] + "' ");
            if (check.Rows.Count > 0)
                ChildNode.Checked = Convert.ToBoolean(check.Rows[0]["operate_enable"].ToString());

            //DataTable temp = db.query("SELECT * FROM [Function] WHERE f_enable = 1 AND f_parent = " + row["f_id"] + " AND f_level <= '" + Session["r_level"].ToString() + "'");
            //CreateTree(ChildNode, temp, int.Parse(row["f_id"].ToString()));


            node.ChildNodes.Add(ChildNode);
        }



        return node;
    }

    private void CreateTree(DataTable TreeDT, int fid)
    {

        TreeNode ChildNode;
        foreach (DataRow row in TreeDT.Rows)
        {
            ChildNode = new TreeNode();
            ChildNode.Text = row["Fname"].ToString(); //父的顯示名稱
            ChildNode.Value = row["Fid"].ToString();  //這邊VALUE自己看要設啥
            ChildNode.SelectAction = TreeNodeSelectAction.None;
            ChildNode.ShowCheckBox = true;

            DataTable check = db.query("SELECT * FROM s_RF_mapping WHERE r_id = '" + id + "' AND f_id = '" + row["fid"] + "' ");
            if (check.Rows.Count > 0)
                ChildNode.Checked = Convert.ToBoolean(check.Rows[0]["operate_enable"].ToString());

            //DataTable temp = db.query("SELECT * FROM [s_Functions] WHERE  FGroup = " + row["Fid"] + " ");
            //CreateTree(ChildNode, temp, int.Parse(row["Fid"].ToString()));


            TreeView1.Nodes.Add(ChildNode);
        }




    }

    private void SaveTree(TreeNode tn)
    {
        //DataTable dt = new DataTable();
        //dt = db.query("SELECT * FROM RF_mapping WHERE r_id = '" + id + "' AND f_id = '" + tn.Value + "'");
        //if (dt.Rows.Count > 0)
        //{
        //    //db.updateRF_mapping(id, tn.Value, tn.Checked);
        //    if (tn.Checked)
        //        db.excute("UPDATE RF_mapping SET operate_enable = 1 WHERE r_id = '" + id + "' AND f_id = '" + tn.Value + "'");
        //    else
        //        db.excute("UPDATE RF_mapping SET operate_enable = 0 WHERE r_id = '" + id + "' AND f_id = '" + tn.Value + "'");
        //}
        //else {
        //    //db.insertRF_mapping(id, tn.Value, tn.Checked);
        //    if (tn.Checked)
        //    {
        //        db.excute("INSERT INTO RF_mapping( [r_id],[operate_enable],[f_id] ) VALUES( '" + id + "', 1, '"+tn.Value+"' )");
        //    }
        //    else {
        //        db.excute("INSERT INTO RF_mapping( [r_id],[operate_enable],[f_id] ) VALUES( '" + id + "', 0, '"+tn.Value+"' )");
        //    }
            
        //}

        if (tn.Checked)
        {
            db.excute("INSERT INTO s_RF_mapping( [r_id],[operate_enable],[f_id] ) VALUES( '" + id + "', 1, '" + tn.Value + "' )");
        }


        db.setSystemOperate("Web", Session["LoginName"] + "", "Edit", "權限管理/角色編輯/功能設定", "Rid:"+id+",f_id:"+tn.Value + ",enable:" + tn.Checked);

        if (tn.ChildNodes.Count > 0)
        {
            foreach (TreeNode n in tn.ChildNodes)
            {
                SaveTree(n);
            }
        }
    }

    private void checkAll(TreeNode tn) {

        tn.Checked = true;

        if (tn.ChildNodes.Count > 0)
        {
            foreach (TreeNode n in tn.ChildNodes)
            {
                checkAll(n);
            }
        }
    }

    private void CancelAll(TreeNode tn)
    {
        tn.Checked = false;
        if (tn.ChildNodes.Count > 0)
        {
            foreach (TreeNode n in tn.ChildNodes)
            {
                CancelAll(n);
            }
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        db.setSystemOperate(Session["LoginName"] + "", "修改", "權限編輯/功能設定");
        if (Request.QueryString["id"] != null)
        {
            id = Request.QueryString["id"].ToString().Trim();
        }

        db.query("DELETE FROM s_RF_mapping WHERE r_id = '" + id + "' AND f_id <> 0 ");

        foreach (TreeNode tn in TreeView1.Nodes)
        {

            SaveTree(tn);
        }


        //Session["LoginName"] = null;
        //Session["UserName"] = null;
        Response.Redirect("~/data6.aspx");
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/data6_2.aspx");
    }
    protected void btn_selectAll_Click(object sender, EventArgs e)
    {
        /*
        foreach (GridViewRow gr in GridView2.Rows)
        {

            CheckBox cb_operate_enable = (CheckBox)gr.FindControl("cb_operate_enable");

            cb_operate_enable.Checked = true;

        }
        */

        foreach (TreeNode tn in TreeView1.Nodes)
        {

            checkAll(tn);
        }

    }
    protected void btn_disselectAll_Click(object sender, EventArgs e)
    {
        /*
        foreach (GridViewRow gr in GridView2.Rows)
        {

            CheckBox cb_operate_enable = (CheckBox)gr.FindControl("cb_operate_enable");
            cb_operate_enable.Checked = false;

        }
        */

        foreach (TreeNode tn in TreeView1.Nodes)
        {

            CancelAll(tn);
        }
    }

    protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {

    }
}
