using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class data8 : System.Web.UI.Page
{
    DBAcess db = new DBAcess();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            db.setSystemOperate(Session["LoginName"] + "", "瀏覽", "系統設定");
        }


        //initContent();
    }

    private void initContent()
    {
        querydata();

    }

    protected void querydata()
    {
        //string LoginTime = txt_startdate.Text;
        //string LogoutTime = txt_enddate.Text;
        //string queryString = string.Empty;
        //DataTable dt = new DataTable();


        //queryString = " select distinct top 500 a.no,a.stype,a.tagid,a.dtime,a.floor,c.area,a.readerid,a.antid,b.name,b.licenseplate,b.id from Socket_temp a left join v_people_tag_licenseplate b on a.tagid=b.tagid LEFT OUTER JOIN MaxSeatSet_map AS c ON a.readerid = c.in_reader_id AND  a.antid = c.in_ant_id  where a.tagid !=''  ";




        //queryString += " and a.stype='depart' group by   a.no, a.stype, a.tagid, a.floor, c.area, a.readerid, a.dtime, a.antid, b.name, b.licenseplate, b.id  order by  a.dtime desc  ,a.no desc";

        //dt = db.query(queryString);


        //Repeater1.DataSource = dt;
        //Repeater1.DataBind();

        //dt.Dispose();

    }
}
