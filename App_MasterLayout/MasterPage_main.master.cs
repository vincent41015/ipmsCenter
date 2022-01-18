using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class App_MasterLayout_MasterPage_Main : System.Web.UI.MasterPage
{
    DBAcess db = new DBAcess();
    protected void Page_Load(object sender, EventArgs e)
    {


        #region 取得該頁面所需權限碼
        string page = Request.FilePath.Replace(@"/ipmsCenter/", "").Replace(@"/ipmscenter/", "");
        DataTable dt = db.query("SELECT * FROM [s_WebDef] WHERE _url like '%" + page + "'");
        Session["Category"] = dt.Rows[0]["FunctionsId"].ToString();
        Session["MenuGroup"] = dt.Rows[0]["Menu_group"].ToString();

        //Session["f_id"] = dt.Rows[0]["f_id"].ToString();
        //Session["menuID"] = dt.Rows[0]["f_menuGroup"].ToString();

        #endregion
        
        if (Session["login_status"] == null || Session["LoginName"] == null || Session["r_id"] == null)
        {
            Response.Redirect(ResolveUrl("~/login.aspx"));
        }

        if (!IsPostBack)
        {
            #region 檢查權限
            /*
            if ( !Sercurity.CheckPermission(Session["r_id"].ToString(), Session["Category"].ToString())) {
                //沒有權限, 跳轉main.aspx
                string info = "<script>alert(' No Permission ');location.href='main.aspx';</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "info", info);
                return;
            } 
            */           
            #endregion

            if (Session["LoginName"] != null)
            {
                db.setSystemOperate("web", Session["LoginName"].ToString(), "瀏覽", page, "");
                User_name.Text = Session["LoginName"].ToString();
                try
                {
                    #region 左右選單                                        
                    GetLeftTree();
                    //GetRightTree();
                    #endregion
                }
                catch
                {
                    // Response.Write("ERROReeeeeeeeeeeeeeeeeee");
                }


                
            }
        }

        

    }

    private void GetLeftTree()
    {
        string r_id = Session["r_id"].ToString();

        //DataTable fs = db.query("SELECT f_id, f_url,f_class, f_name, f_menuGroup FROM [v_RF_TABLE] WHERE r_id = '" + r_id + "' AND f_enable = 1 AND operate_enable = 1 AND f_type = 0 ");

        DataTable fs = db.query("SELECT  _url,_class, _name, Menu_group FROM [v_MenuRole] WHERE r_id = '" + r_id + "' and _enable= 1  order by _sort ");

        Li1.Text = "";
        Li1.Text += "<ul id=\"shortcuts\" role=\"complementary\" class=\"children-tooltip tooltip-right\">";

        foreach (DataRow dr in fs.Rows)
        {
            string fname = dr["_name"].ToString();
            //string fid = dr["f_id"].ToString();

            string furl = dr["_url"].ToString();
            string aclass = dr["_class"].ToString();
            Li1.Text += "<li";

            if (dr["Menu_group"].ToString() == Session["MenuGroup"].ToString())
            {
                Li1.Text += " class=\"current\"";
            }

            

            Li1.Text += "><a href=\"" + ResolveUrl(furl) + "\"";
            if (!string.IsNullOrEmpty(aclass))
                Li1.Text += " class=\"" + aclass + "\"";
            Li1.Text += "\" title=\"" + fname + "\">" + fname + "</a></li>";
        }
        Li1.Text += " </ul>";
    }

    protected void GetRightTree()
    {
        DataTable dt = new DataTable();
        string cmdstring = string.Empty;
        string tagstring = string.Empty;

        string r_id = Session["r_id"].ToString();

        dt = db.query("SELECT * FROM [v_RF_TABLE] WHERE r_id = '" + r_id + "' AND f_enable = 1 AND operate_enable = 1 AND  f_type = 1");
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string fname = dr["f_name"].ToString();
                string furl = dr["f_url"].ToString();
                string fid = dr["f_id"].ToString();
                //檢查是否有子項目
                int getchildcount = checkchild(fid);
                if (getchildcount > 0)//有子節點
                {
                    tagstring += createchild(fid, getchildcount, fname);
                }
                else//單一連節
                {
                    tagstring += "<li><a href=\"" + ResolveUrl(furl) + "\">" + fname + "</a></li> ";
                }
            }

            Li2.Text = "<section class=\"navigable\"><ul class=\"big-menu\">" + tagstring + "</ul></section>";
        }
    }

    private int checkchild(string f_id)
    {
        DataTable dt = new DataTable();
        string cmdstring = string.Empty;
        cmdstring = "SELECT * FROM  [Function] WHERE f_parent = '" + f_id + "' ";
        dt = db.query(cmdstring);
        if (dt != null)
            return dt.Rows.Count;
        else
            return 0;
    }

    private string createchild(string f_id, int childcount, string f_name)
    {
        DataTable dt = new DataTable();
        string cmdstring = string.Empty;
        //cmdstring = "select c.f_name,c.f_url,c.f_id from Permissions a left join RF_mapping2 b on a.r_id=b.r_id left join [Function2] c on b.f_id=c.f_id where a.Account='" + Session["Account"].ToString() + "' and b.operate_enable='True' and c.f_type=1 and c.f_parent=" + f_id + " order by c.f_sort";
        cmdstring = "select * FROM  [Function] WHERE f_parent = '" + f_id + "' ";
        dt = db.query(cmdstring);
        string tagstring = string.Empty;
        tagstring = "<li class=\"with-right-arrow\"><span><span class=\"list-count\">" + childcount.ToString() + "</span>" + f_name + "</span><ul class=\"big-menu\">";
        foreach (DataRow dr in dt.Rows)
        {
            string fname = dr["f_name"].ToString();
            string furl = dr["f_url"].ToString();
            string fid = dr["f_id"].ToString();
            //檢查是否有子項目
            int getchildcount = checkchild(dr["f_id"].ToString());
            if (getchildcount > 0)//有子節點
            {
                tagstring += createchild(fid, getchildcount, fname);
            }
            else//單一連節
            {
                if (childcount > 0)
                {
                    tagstring += "<li><a href=\"" + ResolveUrl(furl) + "\">" + fname + "</a></li> ";
                }
            }
        }
        tagstring += "</ul></li>";
        return tagstring;
    }

    protected void Btn_Logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session["login_status"] = "wait_for_check";
            Response.Redirect(ResolveUrl("~/login.aspx"));
    }
    protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
    {
        if (e.Exception.Data["ExtraInfo"] != null)
        {
            ScriptManager1.AsyncPostBackErrorMessage =
                e.Exception.Message +
                e.Exception.Data["ExtraInfo"].ToString();
        }
        else
        {
            ScriptManager1.AsyncPostBackErrorMessage =
                "An unspecified error occurred.";
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        
    }
}
