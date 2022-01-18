using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;
using System.IO;
using System.Web.Security;

public partial class schecklogin : System.Web.UI.Page
{
    DBAcess db = new DBAcess();
    public class myData
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public bool logged { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {


        //if (Session["login_status"].ToString() == "wait_for_check")
        if(true)
        {
            string login = Request.QueryString["login"];
            string pass = Request.QueryString["pass"];
            //string querystring = "select * from Permissions";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var responseEntities = new List<myData>()
            {
                new myData{ logged=true}
            };


            DataTable dt = new DataTable();
            try
            {
                dt = db.query("select * from s_Permissions where Account ='" + login + "' and PassWord='" + pass + "'");
                if (dt.Rows.Count > 0)
                {
                    //string rid = "";
                    Session["LoginName"] = dt.Rows[0]["username"].ToString();
                    Session["Account"] = dt.Rows[0]["Account"].ToString();
                    Session["r_id"] = dt.Rows[0]["r_id"].ToString();

                    

                    //string strPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "SHA1");

                    //Session["r_level"] = (int)db.executeScalar("SELECT r_level FROM  Role where r_id = '" + Session["r_id"].ToString() + "' ");
                    //Session["r_type"] = (string)db.executeScalar("SELECT r_type FROM  Role where r_id = '" + Session["r_id"].ToString() + "' ");

                    //rid = dt.Rows[0]["r_id"].ToString();
                    //dt = db.query("SELECT r_level FROM Role WHERE r_id = '"+rid+"'");
                    //Session["r_level"] = dt.Rows[0]["r_level"].ToString();
                    //Session["Account"] = dt.Rows[0]["Account"].ToString();

                    db.setSystemOperate(Session["LoginName"] + "", "登入", "登入系統成功");
                }
                else
                {
                    responseEntities = new List<myData>()
                    {
                        new myData{ logged=false}
                    };

                    db.setSystemOperate("", "登入", "登入系統失敗");
                }
            }
            catch( Exception ex )
            {

                File.AppendAllText( "error.txt.", ex.ToString() );
                responseEntities = new List<myData>()
                {
                    new myData{ logged=false}
                };

                db.setSystemOperate("", "登入", "登入系統失敗");

            }
            finally
            {
                dt.Dispose();
            }

            var result = serializer.Serialize(responseEntities);
            Response.Write(result);
            Response.End();

        }
        else
        {

        }

    }
}
