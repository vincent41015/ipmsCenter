using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Web.SessionState;

/// <summary>
/// WebService_Alarm 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
[System.Web.Script.Services.ScriptService]
public class WebService_Alarm : System.Web.Services.WebService {

    DBAcess dbaccess = new DBAcess();
    public WebService_Alarm () {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public List<object> getAlarmDataMap(string _id)
    {
        List<object> iData = new List<object>();
        List<AlarmData> AlarmSets = new List<AlarmData>();

        string query1 = "Select _id, _x, _y, _status , MapURI FROM [v_Device_Alarm] WHERE _id = '" + _id + "' ";
        
        DataTable dt = dbaccess.query(query1);
        foreach (DataRow drow in dt.Rows)
        {
            AlarmSets.Add(new AlarmData(drow["_id"].ToString(), drow["_x"].ToString(), drow["_y"].ToString(), drow["MapURI"].ToString(), drow["_status"].ToString()));
        }


        iData.Add(AlarmSets);

        return iData;
    }

    public class AlarmData
    {
        public string _id;
        public string _x, _y;
        public string MapURI;
        public string _status;
        public AlarmData( string _id, string _x, string _y, string MapURI, string _status )
        {
            this._id = _id;
            this._x = _x;
            this._y = _y;
            this.MapURI = MapURI;
            this._status = _status;
        }
    }

    [WebMethod( EnableSession=true )]
    public List<object> getAlarmDeviceByFloor( string floor )
    {
        List<object> iData = new List<object>();
        List<AlarmData> AlarmSets = new List<AlarmData>();

        string rid = Session["r_id"].ToString();
        

        AlarmMapData MapData;
        //string query1 = "Select a._id, _x, _y, a._status , b.MapURI FROM Alarm_DeviceSetting a left join u_def_Map b on a.MapID = b._id WHERE a._floor = '" + floor + "' ";
        //string query1 = "Select a._id, _x, _y, a._status , b.MapURI FROM s_RDeviceGroup_maping c Join Alarm_DeviceSetting a ON c.deviceGroup = a._group AND c.deviceType = 'Alarm' left join u_def_Map b on a.MapID = b._id WHERE a._floor = '" + floor + "' AND c.r_id = '"+rid+"' ";
        string query1 = @"SELECT [_status],[_ID],_x,_y,_name,MapURI,Category
  FROM [TennisCenter].[dbo].[v_Device_Alarm] 
  WHERE _floor = '" + floor + "'  ";



        DataTable dt = dbaccess.query(query1);
        foreach (DataRow drow in dt.Rows)
        {
            AlarmSets.Add(new AlarmData(drow["_id"].ToString(), drow["_x"].ToString(), drow["_y"].ToString(), drow["MapURI"].ToString(), drow["_status"].ToString()));
        }
        iData.Add(AlarmSets);


        query1 = "Select MapName, MapURI, MapType FROM  u_def_Map  WHERE MapName = '"+ floor+"' AND MapType = 'Alarm' ";
        dt = dbaccess.query(query1);
        foreach (DataRow drow in dt.Rows)
        {
            MapData = new AlarmMapData(drow["MapName"].ToString(), drow["MapURI"].ToString(), drow["MapType"].ToString());
            iData.Add(MapData);
        }
       

        return iData;
    }


    public class AlarmMapData
    {
        public string MapName;        
        public string MapURI;
        public string MapType;
        public AlarmMapData (string _name, string _uri, string _type)
        {
            this.MapName = _name;
            this.MapURI = _uri;
            this.MapType = _type;            
        }
    }


}
