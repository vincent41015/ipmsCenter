using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Globalization;

/// <summary>
/// myWebService 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
[System.Web.Script.Services.ScriptService]
public class myWebService : System.Web.Services.WebService
{

    DBAcess dbaccess = new DBAcess();

    public myWebService()
    {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod]
    public List<object> getLineChartData(string date1)
    {
        List<object> iData = new List<object>();
        List<string> labels = new List<string>();

        string query1 = "Select distinct( DateName( month , DateAdd( month , DATEPART(MONTH,orders_date) , -1 ) )) as month_name, ";
        query1 += " DATEPART(MONTH,orders_date) as month_number from mobile  where DATEPART(YEAR,orders_date)='2016'  ";
        query1 += " order by month_number;";

        DataTable dtLabels = dbaccess.query(query1);
        foreach (DataRow drow in dtLabels.Rows)
        {
            labels.Add(drow["month_name"].ToString());
        }


        iData.Add(labels);

        string query_DataSet_1 = " select DATENAME(MONTH,DATEADD(MONTH,month(orders_date),-1 )) as month_name, month(orders_date) as month_number ,sum ";
        query_DataSet_1 += " (orders_quantity) as total_quantity  from mobile  ";
        query_DataSet_1 += " where YEAR(orders_date)='2016'  ";
        query_DataSet_1 += " group by   month(orders_date) ";
        query_DataSet_1 += " order by  month_number  ";

        DataTable dtDataItemsSets_1 = dbaccess.query(query_DataSet_1);
        List<int> lst_dataItem_1 = new List<int>();
        foreach (DataRow dr in dtDataItemsSets_1.Rows)
        {
            lst_dataItem_1.Add(Convert.ToInt32(dr["total_quantity"].ToString()));
        }


        iData.Add(lst_dataItem_1);


        return iData;
    }
    public DataTable commonFuntionGetData(string strQuery)
    {
        DBAcess db = new DBAcess();

        return db.query(strQuery);

        //SqlDataAdapter dap = new SqlDataAdapter(strQuery, cn);
        //DataSet ds = new DataSet();
        //dap.Fill(ds);
        //return ds.Tables[0];
    }

    [WebMethod]
    public List<object> getTotalData()
    {
        List<object> iData = new List<object>();
        List<object> dataSet = new List<object>();

        string query1 = "Select dtime, licenseplate FROM OutEnterData_tmp";
        DataTable dt = dbaccess.query(query1);


        foreach (DataRow dr in dt.Rows)
        {
            dataSet.Add(new objData(dr["licenseplate"].ToString(), Convert.ToDateTime(dr["dtime"])));
        }


        iData.Add(dataSet);


        return iData;
    }


    public class objData
    {

        DateTime dtime;
        String licenseplate;
        public objData(string licenseplate, DateTime dtime)
        {
            this.licenseplate = licenseplate;
            this.dtime = dtime;
        }
    }

    [WebMethod]
    public List<object> GetCarAmount()
    {
        List<object> iData = new List<object>();
        List<object> CarStatus = new List<object>();

        string query = "SELECT area, amount, totalamount FROM RedLineCount ";
        DataTable dtLabels = dbaccess.query(query);

        int eTagCar = 0;//(int)dbaccess.executeScalar("SELECT Count(*) FROM eTagCarArea WHERE ReaderID = 5");
        int eTagMoto = (int)dbaccess.executeScalar("SELECT Count(*) FROM eTagMotoArea WHERE ReaderID = 1");


        foreach (DataRow drow in dtLabels.Rows)
        {
            if (drow["area"].ToString().Contains("Vendor"))
            {
                if (drow["area"].ToString() == "B4FVendor")
                {
                    CarStatus.Add(new CarAmountObj(drow["area"].ToString(), eTagCar.ToString(), drow["totalamount"].ToString()));
                }
                else
                {
                    CarStatus.Add(new CarAmountObj(drow["area"].ToString(), eTagMoto.ToString(), drow["totalamount"].ToString()));
                }

            }
            else if (drow["area"].ToString().Contains("B4F"))
            {
                CarStatus.Add(new CarAmountObj(drow["area"].ToString(), (int.Parse(drow["amount"].ToString()) - eTagCar).ToString(), drow["totalamount"].ToString()));
            }
            else
            {
                CarStatus.Add(new CarAmountObj(drow["area"].ToString(), drow["amount"].ToString(), drow["totalamount"].ToString()));
            }

        }

        iData.Add(CarStatus);

        return iData;
    }

    [WebMethod]
    public List<object> SetCarAmount(string area, string value)
    {
        List<object> iData = new List<object>();
        List<object> DataSet1 = new List<object>();

        string query = "UPDATE RedLineCount set amount = '" + value + "' WHERE area = '" + area + "'";
        dbaccess.excute(query);
        DataSet1.Add(new MessageObj("Set " + area + "Seat :" + value));
        iData.Add(DataSet1);
        return iData;
    }

    [WebMethod]
    public List<object> SetCarTotalAmount(string area, string value)
    {
        List<object> iData = new List<object>();
        List<object> DataSet1 = new List<object>();

        string query = "UPDATE RedLineCount set totalamount = '" + value + "' WHERE area = '" + area + "'";
        dbaccess.excute(query);
        DataSet1.Add(new MessageObj("Set " + area + "Max Seat :" + value));
        iData.Add(DataSet1);
        return iData;
    }

    public class CarStatusObj
    {
        public string _id;
        public string _x, _y;
        public string _status;
        public string _name;

        public CarStatusObj(string _id, string x, string y, string _status, string p_name)
        {
            this._id = _id;
            this._x = x;
            this._y = y;
            this._status = _status;
            this._name = p_name;
        }
    }

    public class CarAmountObj
    {
        public string floor;
        public string amount;
        public string totalamount;


        public CarAmountObj(string floor, string amount, string totalamount)
        {
            this.floor = floor;
            this.amount = amount;
            this.totalamount = totalamount;

        }
    }

    public class MessageObj
    {
        public string Message;
        public MessageObj(string Message)
        {
            this.Message = Message;
        }
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string BData(string sWORD,string txt_startdate, string txt_enddate, string tx_Vendor, string tx_Name, string tx_BlueCard, string DDL_Location, string DDL_Action,int page,int DisplayLength,string sorting)
    {
        DateTime startDate, endDate;
        int echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        int displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        int displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);
        var sortOrder = HttpContext.Current.Request.Params["sSortDir_0"].ToString(CultureInfo.CurrentCulture);
        var sSearch = HttpContext.Current.Request.Params["sSearch"].ToString(CultureInfo.CurrentCulture);
        sWORD = sWORD.Trim();
        string cmd_order = " order by ";
        string[] sortset = sorting.Split(',');
        switch (sortset[0])
        {
            case "0":
                cmd_order += "dtime ";
                break;
            case "1":
                cmd_order += "VendorName ";
                break;
            case "2":
                cmd_order += "MemberName ";
                break;
            case "3":
                cmd_order += "blueCard ";
                break;
            case "4":
                cmd_order += "CardNo ";
                break;
            case "5":
                cmd_order += "deviceName ";
                break;
            case "6":
                cmd_order += "actName ";
                break;
        }
        cmd_order += sortset[1];

        string cmd = "FROM (select row_number() over ("+cmd_order+") as RowNum, * FROM v_Vendor_InOut_Log WHERE 1=1 ";
        if (string.IsNullOrEmpty(txt_startdate.Trim()) && string.IsNullOrEmpty(txt_enddate.Trim()))
        {
            cmd += " AND dtime between '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "' ";
        }

        if (!string.IsNullOrEmpty(txt_startdate.Trim()))
        {
            bool r = DateTime.TryParse(txt_startdate.Trim(), out startDate);
            if (r)
            {
                cmd += " AND dtime > '" + startDate.ToString("yyyy-MM-dd") + "' ";
            }

        }

        if (!string.IsNullOrEmpty(txt_enddate.Trim()))
        {
            bool r = DateTime.TryParse(txt_enddate.Trim(), out endDate);
            if (r)
            {
                cmd += " AND dtime < '" + endDate.AddDays(1).ToString("yyyy-MM-dd") + "' ";
            }

        }
        if(!string.IsNullOrEmpty(sSearch))
        {
            cmd += " AND (dtime like '%"+ sSearch.Trim() + "%' OR actName like '%" + sSearch.Trim() + "%' OR VendorName like '%" + sSearch.Trim() + "%' OR MemberName like '%" + sSearch.Trim() + "%' OR blueCard like '%" + sSearch.Trim() + "%' OR CardNo like '%" + sSearch.Trim() + "%' OR deviceName like '%" + sSearch.Trim() + "%' )";
        }
        //if (!string.IsNullOrEmpty(txt_condition.Text)) {
        //    cmd += " AND (VendorName like '%" + txt_condition.Text + "%'  OR MemberName like '%" + txt_condition.Text + "%') ";
        //}

        if (!string.IsNullOrEmpty(tx_Vendor.Trim()))
        {
            cmd += " AND VendorName like '%" + tx_Vendor.Trim() + "%' ";
        }
        if (!string.IsNullOrEmpty(tx_Name.Trim()))
        {
            cmd += " AND MemberName like '%" + tx_Name.Trim() + "%' ";
        }
        if (!string.IsNullOrEmpty(tx_BlueCard.Trim()))
        {
            cmd += " AND blueCard = '" + tx_BlueCard.Trim() + "' ";
        }

        if (!string.IsNullOrEmpty(DDL_Location))
        {
            cmd += " AND device_ID = '" + DDL_Location + "' ";
        }
        if (!string.IsNullOrEmpty(DDL_Action))
        {
            cmd += " AND Act = '" + DDL_Action + "' ";
        }
        //cmd += cmd_order;
        //cmd += ") as a ";
        

        DataTable dt = dbaccess.query("SELECT COUNT(*) as count "+cmd+ ") as a ");
        var records = dt.Rows[0]["count"];
        //cmd += " WHERE RowNum >= '" + page + "' AND RowNum <= '" + (page + DisplayLength - 1) + "'";
        dt = dbaccess.query("SELECT * "+cmd +") as a  WHERE RowNum >= '" + (displayStart+1) + "' AND RowNum <= '" + (displayStart + DisplayLength ) + "'"+cmd_order);

        //cmd += " order by dtime desc ";
        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records + ",");
        sb.Append("\"recordsFiltered\": " + records + ",");
        sb.Append("\"iTotalRecords\": " + records + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records + ",");       
        sb.Append("\"aaData\": [");
        bool hasMoreRecords = false;
        //string rjson = JsonConvert.SerializeObject(dt);
        if (dt!=null&&dt.Rows.Count > 0)
        {
            foreach (DataRow result in dt.Rows)
            {
                if (hasMoreRecords)
                {
                    sb.Append(",");
                }

                sb.Append("{");
                sb.Append("\"dtime\":\"" + result["dtime"] + "\",");
                sb.Append("\"VendorName\":\"" + result["VendorName"] + "\",");
                sb.Append("\"MemberName\":\"" + result["MemberName"] + "\",");
                sb.Append("\"blueCard\":\"" + result["blueCard"] + "\",");
                sb.Append("\"CardNo\":\"" + result["CardNo"] + "\",");
                sb.Append("\"deviceName\":\"" + result["deviceName"] + "\",");
                sb.Append("\"actName\":\"" + result["actName"] + "\"");
                sb.Append("}");
                hasMoreRecords = true;
            }
        }
        sb.Append("]}");
        if (dt != null)
        {
            dt.Dispose();
        }
        return sb.ToString();
        //JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        //List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        //Dictionary<string, object> childRow;
        //foreach (DataRow row in dt.Rows)
        //{
        //    childRow = new Dictionary<string, object>();
        //    foreach (DataColumn col in dt.Columns)
        //    {
        //        childRow.Add(col.ColumnName, row[col]);
        //    }
        //    parentRow.Add(childRow);
        //}
        //string jsondata= jsSerializer.Serialize(parentRow);
        //return jsondata;

        //string rjson= JsonConvert.SerializeObject(dt);

        //return rjson;
    }

    public class BlueCardData
    {
        public string device_ID { get; set; }
        public string dtime { get; set; }
        public string actName { get; set; }
        public string VendorName { get; set; }
        public string MemberName { get; set; }
        public string blueCard { get; set; }
        public string deviceName { get; set; }
        public string _id { get; set; }
        public string Act { get; set; }
        public string CardNo { get; set; }

    }
    [WebMethod]
    public List<object> checkAlarm()
    {
        List<object> iData = new List<object>();
        List<object> dataSet = new List<object>();

        string query1 = "Select * FROM v_AlertDevice WHERE  isSend = 0";
        DataTable dt = dbaccess.query(query1);


        foreach (DataRow dr in dt.Rows)
        {
            //dataSet.Add(new AlarmStatusObj(dr["_id"].ToString(), dr["_x"].ToString(), dr["_y"].ToString(), dr["_status"].ToString(), dr["Message"].ToString() ) );                        
            dataSet.Add(new AlertObj(dr["DEviceID"].ToString(), dr["SName"].ToString(), dr["Category"].ToString(), dr["SubCategory"].ToString(), dr["_x"].ToString(), dr["_y"].ToString(), dr["_status"].ToString(), dr["Message"].ToString()));
        }
        dbaccess.excute("update Alert_Report SET isSend = 1 ");


        iData.Add(dataSet);



        return iData;
    }

    public class AlarmStatusObj
    {
        public string _id;
        public string x, y;
        public string _status;
        public string displayMessage;

        public AlarmStatusObj(string _id, string x, string y, string _status, string displayMessage)
        {
            this._id = _id;
            this.x = x;
            this.y = y;
            this._status = _status;
            this.displayMessage = displayMessage;
        }
    }


    public class AlertObj
    {
        public string _id;
        public string SName, Category, SubCategory;
        public string x, y;
        public string _status;
        public string displayMessage;

        public AlertObj(string _id, string SName, string Category, string SubCategory, string x, string y, string _status, string displayMessage)
        {
            this._id = _id;
            this.SName = SName;
            this.SubCategory = SubCategory;
            this._id = _id;
            this.x = x;
            this.y = y;
            this._status = _status;
            this.displayMessage = displayMessage;
        }
    }
}
