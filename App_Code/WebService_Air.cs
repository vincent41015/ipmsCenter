using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

/// <summary>
/// WebService_Air 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
[System.Web.Script.Services.ScriptService]
public class WebService_Air : System.Web.Services.WebService {

    DBAcess db = new DBAcess();

    public WebService_Air () {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }


    [WebMethod(EnableSession = true)]
    public List<object> GetTemperatureMain()
    {
        List<object> iData = new List<object>();

        List<string> labelSets = new List<string>();
        List<float> dataSets = new List<float>();        

        string query1 = @"SELECT * FROM (
SELECT top(100)  value1,value2,value3, dtime 
FROM Air_ThermometerRecord
WHERE ThermometerID = 4
order by dtime desc ) as temp 
order by dtime asc
  ";

        DataTable dt = db.query(query1);
        foreach (DataRow drow in dt.Rows)
        {
            labelSets.Add(drow["dtime"].ToDateTime().ToString("yyyy-MM-dd HH:mm:ss"));

            float ff = drow["value1"].ToSingle();
            dataSets.Add(ff);


            
        }
        iData.Add(labelSets);
        iData.Add(dataSets);
        
        return iData;


    }

    [WebMethod(EnableSession = true)]
    public List<object> GetPAH()
    {
        List<object> iData = new List<object>();

        
        List<PAHData> dataSets = new List<PAHData>();

        string query1 = @"SELECT  a.[_id],[_operation]
      ,[_status]
      ,[deviceID]      
      ,[ain_tempature]      
      ,b.f_x
      ,b.f_y
      ,b._name
,b.Category 
  FROM [TennisCenter].[dbo].[Air_Content_PAH] a
  left join Air_MapINfo b on a.deviceID = b._id
  ";

        DataTable dt = db.query(query1);
        foreach (DataRow drow in dt.Rows)
        {
            dataSets.Add(new PAHData(drow["deviceID"].ToString(), drow["f_x"].ToString(), drow["f_y"].ToString(), drow["_name"].ToString(), drow["_operation"].ToString(), drow["_status"].ToString(), drow["ain_tempature"].ToString(), drow["Category"].ToString()));
        }
        
        iData.Add(dataSets);

        return iData;


    }

    [WebMethod(EnableSession = true)]
    public List<object> GetAHU()
    {
        List<object> iData = new List<object>();


        List<AHUData> dataSets = new List<AHUData>();

        string query1 = @"SELECT  a.[_id],[_operation]
      ,[_status]
      ,[deviceID]      
       ,a.ain_tempature     
      ,b.f_x
      ,b.f_y
      ,b._name
        ,b.Category
  FROM [TennisCenter].[dbo].[Air_Content_AHU] a
  left join Air_MapINfo b on a.deviceID = b._id
  ";

        DataTable dt = db.query(query1);
        foreach (DataRow drow in dt.Rows)
        {
            dataSets.Add(new AHUData(drow["deviceID"].ToString(), drow["f_x"].ToString(), drow["f_y"].ToString(), drow["_name"].ToString(), drow["_operation"].ToString(), drow["_status"].ToString(), drow["ain_tempature"].ToString(), drow["Category"].ToString()));
        }

        iData.Add(dataSets);

        return iData;


    }

    [WebMethod(EnableSession = true)]
    public List<object> GetCH()
    {
        List<object> iData = new List<object>();


        List<CHData> dataSets = new List<CHData>();

        string query1 = @"SELECT  a.[_id],[_operation]
      ,[_status]
      ,[deviceID]      
       ,a.ain_tempature     
      ,b.f_x
      ,b.f_y
      ,b._name
      ,b.Category
  FROM [TennisCenter].[dbo].[Air_Content_CH] a
  left join Air_MapINfo b on a.deviceID = b._id
  ";

        DataTable dt = db.query(query1);
        foreach (DataRow drow in dt.Rows)
        {
            dataSets.Add(new CHData(drow["deviceID"].ToString(), drow["f_x"].ToString(), drow["f_y"].ToString(), drow["_name"].ToString(), drow["_operation"].ToString(), drow["_status"].ToString(), drow["Category"].ToString()));
        }

        iData.Add(dataSets);

        return iData;


    }



    public class AirData
    {
        public string _id;
        public string _x, _y;
        public string _name;
        public string MapURI;
        public string _operation;
        public string _status;
        public string Category;
        public AirData(string _id, string _name, string _x, string _y, string _operation, string _status, string MapURI, string Category)
        {
            this._id = _id;
            this._name = _name;
            this._x = _x;
            this._y = _y;
            this.MapURI = MapURI;
            this._operation = _operation;
            this._status = _status;
            this.Category = Category;
        }
    }

    public class MapData
    {
        public string MapName;
        public string MapURI;
        public string MapType;
        public MapData(string _name, string _uri, string _type)
        {
            this.MapName = _name;
            this.MapURI = _uri;
            this.MapType = _type;
        }
    }

    public class PAHData
    {
        public string _id;
        public string _x, _y;
        public string _name;
        public string _operation;
        public string _status;
        public string ain_tempature;
        public string Category;

        public PAHData(string _id, string _x, string _y, string _name, string _operation, string _status, string ain_tempature, string Category)
        {
            this._id = _id;
            this._x = _x;
            this._y = _y;
            this._name = _name;
            this._operation = _operation;
            this._status = _status;
            this.ain_tempature = ain_tempature;
            this.Category = Category;
            
        }
    }

    public class AHUData
    {
        public string _id;
        public string _x, _y;
        public string _name;
        public string _operation;
        public string _status;
        public string ain_tempature;
        public string Category;

        public AHUData(string _id, string _x, string _y, string _name, string _operation, string _status, string ain_tempature, string Category)
        {
            this._id = _id;
            this._x = _x;
            this._y = _y;
            this._name = _name;
            this._operation = _operation;
            this._status = _status;
            this.ain_tempature = ain_tempature;
            this.Category = Category;
        }
    }

    public class CHData
    {
        public string _id;
        public string _x, _y;
        public string _name;
        public string _operation;
        public string _status;
        public string ain_tempature;
        public string Category;

        public CHData(string _id, string _x, string _y, string _name, string _operation, string _status, string Category)
        {
            this._id = _id;
            this._x = _x;
            this._y = _y;
            this._name = _name;
            this._operation = _operation;
            this._status = _status;
            this.Category = Category;
        }
    }

    [WebMethod(EnableSession = true)]
    public List<object> GetDeviceByFloor(string floor)
    {
        List<object> iData = new List<object>();


        List<AirData> dataSets = new List<AirData>();
        MapData mapData;


        string query1 = @"SELECT [_operation]
      ,[_status]
      ,[_ID]                
      ,_x
      ,_y
      ,_name
,MapURI
,Category
  FROM [TennisCenter].[dbo].[v_Device_Air] 
  WHERE _floor = '" + floor+"'  ";

        DataTable dt = db.query(query1);
        foreach (DataRow drow in dt.Rows)
        {
            dataSets.Add(new AirData(drow["_ID"].ToString(), drow["_name"].ToString(), drow["_x"].ToString(), drow["_y"].ToString(), drow["_operation"].ToString(), drow["_status"].ToString(), drow["MapURI"].ToString(), drow["Category"].ToString()));
        }

        iData.Add(dataSets);

        query1 = "Select MapName, MapURI, MapType FROM  u_def_Map  WHERE MapName = '" + floor + "' AND MapType = 'Air' ";
        dt = db.query(query1);
        foreach (DataRow drow in dt.Rows)
        {
            mapData = new MapData(drow["MapName"].ToString(), drow["MapURI"].ToString(), drow["MapType"].ToString());
            iData.Add(mapData);
        }


        return iData;


    }


}
