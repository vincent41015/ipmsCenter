using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Functions_Function_1 : System.Web.UI.Page
{
    //DBAcess db = new DBAcess();
    DbTest db = new DbTest();

    protected void Page_Load(object sender, EventArgs e)
    {
        string str = "";
        string strClassement = "";
        string strSens = "";
        string strSensInverse = "";
        string sSearch;

        string strRequeteA = "";
        string strRequeteC = "";
        string strRequeteB_1 = "";
        string strRequeteB_2 = "";
        string strRequeteB_3 = "";

        int year = 0;
        int month = 0;
        string stock;

        if (Request.QueryString["year"] != null) {
            year = int.Parse(Request.QueryString["year"].ToString());
        }
        if (Request.QueryString["month"] != null)
        {
            month = int.Parse(Request.QueryString["month"].ToString());
        }
        if (Request.QueryString["stock"] != null)
        {
            stock = Request.QueryString["stock"].ToString();
        }

        if (year == 0 || month == 0) {
            Response.Write("{\"sEcho\": 1,\"iTotalRecords\": \"0\",\"iTotalDisplayRecords\": \"0\",\"aaData\": []}");

            return;
        }


        int iDisplayStart = Request.QueryString["iDisplayStart"] != null ? Convert.ToInt32(Request.QueryString["iDisplayStart"]) : 0;
        int iDisplayLength = Request.QueryString["iDisplayLength"] != null ? Convert.ToInt32(Request.QueryString["iDisplayLength"]) : 10;
        int sEcho = Request.QueryString["sEcho"] != null ? Convert.ToInt32(Request.QueryString["sEcho"]) : 1;
        int iSortCol_0 = Request.QueryString["iSortCol_0"] != null ? Convert.ToInt32(Request.QueryString["iSortCol_0"]) : 0;
        sSearch = Request.QueryString["sSearch"];


        switch (iSortCol_0) { 
            case 0 :
                strClassement = "engine";
            break;
            case 1:
            strClassement = "browser";
            break;
            case 2:
            strClassement = "platform";
            break;
            case 3:
            strClassement = "version";
            break;
            case 4:
            strClassement = "grade";
            break;
            default:
            strClassement = "engine";
            break;
        }

        switch (Request.QueryString["sSortDir_0"]) { 
            case "asc":
                strSens = " asc";
                strSensInverse = " desc";
            break;
            case "desc":
            strSens = " desc";
            strSensInverse = " asc";
            break;
        }


        strRequeteA += "SELECT * FROM (";
        strRequeteA += "SELECT TOP ";
        strRequeteA += iDisplayLength;
        strRequeteA += " * FROM (";
        strRequeteB_1 = "SELECT ";
        strRequeteB_2 = "TOP " + (iDisplayStart + iDisplayLength);
        strRequeteB_3 += " _id, engine, browser, platform, version, grade  ";
        strRequeteB_3 += "FROM ajax  ";

        //strRequeteB_3 += ") as t1 order by dtime desc)  as t2";
        //strRequeteB_3 += " order by dtime asc ";
        strRequeteB_3 += " WHERE 1=1";

        #region user Defined condition 
        
        #endregion


        #region search condition
        if (sSearch != null && sSearch.Trim() != "") {
            
            strRequeteB_3 += " AND (";
            
            strRequeteB_3 += "engine LIKE '%" + sSearch + "%'";
            strRequeteB_3 += " OR ";
            strRequeteB_3 += "browser LIKE '%" + sSearch + "%'";
            strRequeteB_3 += " OR ";
            strRequeteB_3 += "platform LIKE '%" + sSearch + "%'";
            strRequeteB_3 += " OR ";
            strRequeteB_3 += "version LIKE '%" + sSearch + "%'";
            strRequeteB_3 += " OR ";
            strRequeteB_3 += "grade LIKE '%" + sSearch + "%'";

            strRequeteB_3 += ")";
        }
        #endregion


        strRequeteC += " ORDER BY ";
        strRequeteC += strClassement;
        strRequeteC += strSens;
 
        strRequeteC += ") AS foo ORDER BY ";
        strRequeteC += strClassement;
        strRequeteC += strSensInverse;
 
        strRequeteC += ") AS bar ORDER BY ";
        strRequeteC += strClassement;
        strRequeteC += strSens;


      DataTable dt = db.query(strRequeteA + strRequeteB_1 + strRequeteB_2 + strRequeteB_3 + strRequeteC);
        int totalCount = (int)db.executeScalar( "SELECT COUNT(_id) FROM (" + strRequeteB_1  + strRequeteB_3 + ") AS P1");

        str = "{";
        str += "\"sEcho\": " + sEcho + ",";
        str += "\"iTotalRecords\": " + totalCount + ",";
        str += "\"iTotalDisplayRecords\": " + totalCount + ",";
        str += "\"aaData\": [";

        int nbRowANePasAfficher = 0;
        if ((iDisplayStart + iDisplayLength) > totalCount)
        {
            nbRowANePasAfficher = (iDisplayStart + iDisplayLength) - totalCount;
        }
        if (iDisplayStart == 0)
        {
            nbRowANePasAfficher = 0;
        }

        bool autre = false;

        foreach (DataRow row in dt.Rows)
        {
            if (nbRowANePasAfficher > 0)
            {
                nbRowANePasAfficher -= 1;
                continue;
            }
            if (autre == true)
            {
                str += ",";
            }
            str += "[";

            str += "\"" + row["_id"].ToString() + "\",";
            str += "\"" + row["engine"].ToString() + "\",";
            str += "\"" + row["browser"].ToString() + "\",";
            str += "\"" + row["platform"].ToString() + "\",";
            str += "\"" + row["version"].ToString() + "\",";
            str += "\"" + row["grade"].ToString() + "\"";

            str += "]";
            autre = true;
        }

        str += "]";
        str += "}";

        Response.Write(str);


    }
}