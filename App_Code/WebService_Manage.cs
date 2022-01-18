using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using System.Web.Script.Services;
using System.Net;
using System.IO;
using ClosedXML.Excel;
using System.ComponentModel;
using System.Net.Sockets;

/// <summary>
/// WebService_Ventilation 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
[System.Web.Script.Services.ScriptService]
public class WebService_Manage : System.Web.Services.WebService
{

    DBAcess db = new DBAcess();
    public WebService_Manage()
    {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetView()
    {

        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var cat = HttpContext.Current.Request.Params["cat"].ToString(CultureInfo.CurrentCulture);

        var db = new DBAcess();
        DataTable dt = new DataTable();

        var str = "SELECT * FROM [v_Device_Point_Map] where [Category] = '" + cat + "' order by Category,subCategory,_name,_name2";

        dt = db.query(str);

        var records = new List<View>();

        try
        {
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var rows = new View();
                rows.deviceID = dt.Rows[i]["deviceID"].ToString();
                rows.Category = dt.Rows[i]["Category"].ToString();
                rows.subCategory = dt.Rows[i]["subCategory"].ToString();
                rows._name = dt.Rows[i]["_name"].ToString();
                rows._name2 = dt.Rows[i]["_name2"].ToString();
                rows.sub_id = dt.Rows[i]["sub_id"].ToString();
                rows.floor = dt.Rows[i]["floor"].ToString();
                rows._operation = dt.Rows[i]["_operation"].ToString();
                rows._status = dt.Rows[i]["_status"].ToString();
                rows.isSend = dt.Rows[i]["isSend"].ToString();
                rows.PLC_box = dt.Rows[i]["PLC_box"].ToString();
                rows.PLC_id = dt.Rows[i]["PLC_id"].ToString();
                rows.PLC_sub = dt.Rows[i]["PLC_sub"].ToString();
                rows.CA = dt.Rows[i]["CA"].ToString();
                rows.IO_type = dt.Rows[i]["IO_type"].ToString();
                rows.comefrom = dt.Rows[i]["comefrom"].ToString();
                rows._floor = dt.Rows[i]["_floor"].ToString();
                rows._area = dt.Rows[i]["_area"].ToString();
                rows._x = dt.Rows[i]["_x"].ToString();
                rows._y = dt.Rows[i]["_y"].ToString();
                rows.MapId = dt.Rows[i]["MapId"].ToString();
                rows.MapName = dt.Rows[i]["MapName"].ToString();
                rows.MapURI = dt.Rows[i]["MapURI"].ToString();
                rows.MapType = dt.Rows[i]["MapType"].ToString();
                rows.Expr1 = dt.Rows[i]["Expr1"].ToString();
                rows.pattern_id = dt.Rows[i]["pattern_id"].ToString();
                rows.pattern_name = dt.Rows[i]["name"].ToString();
                rows._width = dt.Rows[i]["_width"].ToString();
                rows._height = dt.Rows[i]["_height"].ToString();
                rows._url = dt.Rows[i]["_url"].ToString();
                rows._color = dt.Rows[i]["_color"].ToString();
                rows.note = dt.Rows[i]["note"].ToString();

                records.Add(rows);
            }
        }
        catch
        {

        }


        dt.Dispose();

        /*
        if (!records.Any())
        {
            return string.Empty;
        }
        */

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            sb.Append("\"" + result.deviceID + "\",");
            sb.Append("\"" + result.subCategory + "\",");
            sb.Append("\"" + result._name + "\",");
            sb.Append("\"" + result._name2 + "\",");
            sb.Append("\"" + result.sub_id + "\",");
            sb.Append("\"" + result.floor + "\",");
            sb.Append("\"" + result._operation + "\",");
            sb.Append("\"" + result._status + "\",");
            sb.Append("\"" + result.isSend + "\",");
            sb.Append("\"" + result.PLC_box + "\",");
            sb.Append("\"" + result.PLC_id + "\",");
            sb.Append("\"" + result.PLC_sub + "\",");
            sb.Append("\"" + result.CA + "\",");
            sb.Append("\"" + result.IO_type + "\",");
            sb.Append("\"" + result.comefrom + "\",");
            sb.Append("\"" + result._floor + "\",");
            sb.Append("\"" + result._area + "\",");
            sb.Append("\"" + result._x + "\",");
            sb.Append("\"" + result._y + "\",");
            sb.Append("\"" + result.MapId + "\",");
            sb.Append("\"" + result.MapName + "\",");
            sb.Append("\"" + result.MapURI + "\",");
            sb.Append("\"" + result.MapType + "\",");
            sb.Append("\"" + result.Expr1 + "\",");
            sb.Append("\"" + result.pattern_id + "\",");
            sb.Append("\"" + result.pattern_name + "\",");
            sb.Append("\"" + result._width + "\",");
            sb.Append("\"" + result._height + "\",");
            sb.Append("\"" + result._url + "\",");
            sb.Append("\"" + result._color + "\",");
            sb.Append("\"" + result.note + "\",");

            sb.Append("\"" + "<button class='editButton button icon-save ' value='" + result.deviceID + "' >儲存</button> <button class='delButton button icon-trash' value = '" + result.deviceID + "' >刪除</button>" + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");
        return sb.ToString();
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetDevices()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var cat = HttpContext.Current.Request.Params["cat"].ToString(CultureInfo.CurrentCulture);

        var db = new DBAcess();
        DataTable dt = new DataTable();

        var str = "SELECT * FROM [s_DeviceSetting] where [Category] = '" + cat + "' and [valid] = '1' order by subCategory, _sort, floor, _name, sub_id";

        dt = db.query(str);

        var records = new List<View>();

        try
        {
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var rows = new View();
                rows.deviceID = dt.Rows[i]["_id"].ToString();
                rows.Category = dt.Rows[i]["Category"].ToString();
                rows.subCategory = dt.Rows[i]["subCategory"].ToString();
                rows._name = dt.Rows[i]["_name"].ToString();
                rows._name2 = dt.Rows[i]["_name2"].ToString();
                rows.sub_id = dt.Rows[i]["sub_id"].ToString();
                rows.d_comefrom = dt.Rows[i]["comefrom"].ToString();
                rows.room_id = dt.Rows[i]["room_id"].ToString();
                rows._ip = dt.Rows[i]["_ip"].ToString();
                rows._floor = dt.Rows[i]["floor"].ToString();
                rows._sort = dt.Rows[i]["_sort"].ToString();

                records.Add(rows);
            }
        }
        catch
        {

        }


        dt.Dispose();

        /*
        if (!records.Any())
        {
            return string.Empty;
        }
        */

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            sb.Append("\"" + result.subCategory + "\",");
            sb.Append("\"" + result._name + "\",");
            sb.Append("\"" + result.sub_id + "\",");
            sb.Append("\"" + result._floor + "\",");
            sb.Append("\"" + result.room_id + "\",");
            sb.Append("\"" + result._ip + "\",");
            sb.Append("\"" + result._sort + "\",");
            sb.Append("\"" + "<div class='button icon-eye' style='cursor:pointer;' onclick='show_group(" + result.deviceID + ")'>檢視群組</div>" + "\",");
            
            sb.Append("\"" + "<div style='width:170px;'>" +
                             "<a class='button icon-pencil' href='/Center/System_PointManagement/PointManagement_Device_Update.aspx?deviceID=" + result.deviceID + "' style='width:50px;'>編輯</a>" +
                             "<div class='button icon-trash' onclick='del_confirm(" + result.deviceID + ")' style='width:50px;margin-left:10px;'>刪除</div>" +
                             "</div>" + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");
        return sb.ToString();
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetDevicesByGroup()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var group = HttpContext.Current.Request.Params["group"].ToString(CultureInfo.CurrentCulture);

        var dt = db.query("select a.* from [s_DeviceSetting] a left join [s_DeviceGroupList] b on a.[_id] = b.[DeviceID] left join [s_DeviceGroup] c on b.[GroupID] = c.[_id] where c._id = '" + group + "' and a.[valid] = '1'");

        var records = new List<View>();

        try
        {
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var rows = new View();
                rows.deviceID = dt.Rows[i]["_id"].ToString();
                rows.Category = dt.Rows[i]["Category"].ToString();
                rows.subCategory = dt.Rows[i]["subCategory"].ToString();
                rows._name = dt.Rows[i]["_name"].ToString();
                rows._name2 = dt.Rows[i]["_name2"].ToString();
                rows.sub_id = dt.Rows[i]["sub_id"].ToString();
                rows.d_comefrom = dt.Rows[i]["comefrom"].ToString();
                rows.room_id = dt.Rows[i]["room_id"].ToString();
                rows._ip = dt.Rows[i]["_ip"].ToString();
                rows._floor = dt.Rows[i]["floor"].ToString();
                rows._sort = dt.Rows[i]["_sort"].ToString();

                records.Add(rows);
            }
        }
        catch
        {

        }


        dt.Dispose();

        /*
        if (!records.Any())
        {
            return string.Empty;
        }
        */

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            sb.Append("\"" + result.subCategory + "\",");
            sb.Append("\"" + result._name + "\",");
            sb.Append("\"" + result.sub_id + "\",");
            sb.Append("\"" + result._floor + "\",");
            sb.Append("\"" + result.room_id + "\",");
            sb.Append("\"" + result._ip + "\",");
            sb.Append("\"" + result._sort + "\",");
            sb.Append("\"" + "<div class='button icon-trash' style='cursor:pointer;' onclick='openConfirm(" + result.deviceID + "," + group + ")'>移出群組</div>" + "\",");
            
            sb.Append("\"" + "<div style='width:170px;'>" +
                             "<a class='button icon-pencil' href='/Center/System_PointManagement/PointManagement_Device_Update.aspx?deviceID=" + result.deviceID + "' style='width:50px;'>編輯</a>" +
                             "<div class='button icon-trash' onclick='del_confirm(" + result.deviceID + ")' style='width:50px;margin-left:10px;'>刪除</div>" +
                             "</div>" + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");
        return sb.ToString();
    }

    [WebMethod]
    public object get_device_title(string d_id)
    {
        var str = "SELECT * FROM [s_DeviceSetting] where [_id] = '" + d_id + "'";
        var dt = db.query(str);

        return new
        {
            _id = dt.Rows[0]["_id"].ToString(),
            Category = dt.Rows[0]["Category"].ToString(),
            subCategory = dt.Rows[0]["subCategory"].ToString(),
            _name = dt.Rows[0]["_name"].ToString(),
            _name2 = dt.Rows[0]["_name2"].ToString(),
            sub_id = dt.Rows[0]["sub_id"].ToString(),
            room_id = dt.Rows[0]["room_id"].ToString(),
            floor = dt.Rows[0]["floor"].ToString(),
            comefrom = dt.Rows[0]["comefrom"].ToString(),
            _ip = dt.Rows[0]["_ip"].ToString(),
            _sort = dt.Rows[0]["_sort"].ToString()
        };
    }

    [WebMethod]
    public object get_device_view(string d_id)
    {
        var str = "SELECT * FROM [v_DeviceSetting] where [_id] = '" + d_id + "'";
        var dt = db.query(str);

        return new
        {
            _id = dt.Rows[0]["_id"].ToString(),
            Category = dt.Rows[0]["Category"].ToString(),
            subCategory = dt.Rows[0]["subCategory"].ToString(),
            _name = dt.Rows[0]["_name"].ToString()
        };
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string get_device_details()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var d_id = HttpContext.Current.Request.Params["d_id"].ToString(CultureInfo.CurrentCulture);

        var db = new DBAcess();

        var str = "SELECT * FROM [s_DeviceIOPoint] where [deviceID] = '" + d_id + "' order by [_id]";

        var dt = db.query(str);

        var records = new List<View>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var rows = new View();
            rows.point_id = dt.Rows[i]["_id"].ToString();
            rows._operation = dt.Rows[i]["_operation"].ToString();
            rows.operationname = dt.Rows[i]["operationname"].ToString();
            rows._status = dt.Rows[i]["_status"].ToString();
            rows.isSend = dt.Rows[i]["isSend"].ToString();
            rows.PLC_box = dt.Rows[i]["PLC_box"].ToString();
            rows.PLC_id = dt.Rows[i]["PLC_id"].ToString();
            rows.PLC_sub = dt.Rows[i]["PLC_sub"].ToString();
            rows.CA = dt.Rows[i]["CA"].ToString();
            rows.IO_type = dt.Rows[i]["IO_type"].ToString();
            rows.comefrom = dt.Rows[i]["comefrom"].ToString();
            rows._ip = dt.Rows[i]["_ip"].ToString();
            rows.c_id = dt.Rows[i]["c_id"].ToString();
            rows._sort = dt.Rows[i]["_sort"].ToString();

            records.Add(rows);
        }



        dt.Dispose();

        /*
        if (!records.Any())
        {
            return string.Empty;
        }
        */

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");
            /*
            sb.Append("\"" + "<input type='text' class='input' value='" + result._operation + "' />" + "\",");
            sb.Append("\"" + "<input type='text' class='input' value='" + result._status + "' />" + "\",");
            sb.Append("\"" + "<input type='text' class='input' value='" + result.isSend + "' />" + "\",");
            sb.Append("\"" + "<input type='text' class='input' value='" + result.PLC_id + "' />" + "\",");
            sb.Append("\"" + "<input type='text' class='input' value='" + result.PLC_sub + "' />" + "\",");
            sb.Append("\"" + "<input type='text' class='input' value='" + result.CA + "' />" + "\",");
            sb.Append("\"" + "<input type='text' class='input' value='" + result.IO_type + "' />" + "\",");
            sb.Append("\"" + "<input type='text' class='input' value='" + result.c_id + "' />" + "\",");
            sb.Append("\"" + "<input type='text' class='input' value='" + result._sort + "' />" + "\",");
            */
            sb.Append("\"" + "<div class='io_operation'>" + result._operation + "</div>" + "\",");
            sb.Append("\"" + "<div class='io_status'>" + result._status + "</div>" + "\",");
            sb.Append("\"" + "<div class='io_isSend'>" + result.isSend + "</div>" + "\",");
            sb.Append("\"" + "<div class='io_PLC_id'>" + result.PLC_id + "</div>" + "\",");
            sb.Append("\"" + "<div class='io_PLC_sub'>" + result.PLC_sub + "</div>" + "\",");
            sb.Append("\"" + "<div class='io_CA'>" + result.CA + "</div>" + "\",");
            sb.Append("\"" + "<div class='io_IO_type'>" + result.IO_type + "</div>" + "\",");
            sb.Append("\"" + "<div class='io_c_id'>" + result.c_id + "</div>" + "\",");
            sb.Append("\"" + "<div class='io_sort'>" + result._sort + "</div>" + "\",");

            sb.Append("\"" + "<div style='width:160px;margin:auto;'><a id='" + result.point_id + "' class='button icon-pencil' onclick='point_edit(this)' style='width:50px;'>編輯</a> <a id='" + result.point_id + "' class='button icon-trash' onclick='del_confirm(this)' style='width:50px;'>刪除</a></div>" + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");
        return sb.ToString();
    }

    public class View
    {
        public string _id { get; set; }

        public string kind { get; set; }
        public string deviceID { get; set; }
        public string Category { get; set; }
        public string subCategory { get; set; }
        public string _name { get; set; }
        public string _name2 { get; set; }
        public string sub_id { get; set; }
        public string floor { get; set; }
        public string room_id { get; set; }
        public string d_comefrom { get; set; }
        public string _ip { get; set; }
        public string _sort { get; set; }
        public string point_id { get; set; }
        public string _operation { get; set; }
        public string operationname { get; set; }
        public string _status { get; set; }
        public string isSend { get; set; }
        public string PLC_box { get; set; }
        public string PLC_id { get; set; }
        public string PLC_sub { get; set; }
        public string CA { get; set; }
        public string IO_type { get; set; }
        public string comefrom { get; set; }
        public string c_id { get; set; }
        public string i_id { get; set; }
        public string _floor { get; set; }
        public string _area { get; set; }
        public string _x { get; set; }
        public string _y { get; set; }
        public string sort { get; set; }
        public string MapId { get; set; }
        public string _onMap { get; set; }
        public string f_x { get; set; }
        public string f_y { get; set; }
        public string f_MapId { get; set; }
        public string _group { get; set; }
        public string MapName { get; set; }
        public string MapURI { get; set; }
        public string MapType { get; set; }
        public string MapCat { get; set; }
        public string Expr1 { get; set; }
        public string z_index { get; set; }
        public string pattern_id { get; set; }
        public string pattern_name { get; set; }
        public string _width { get; set; }
        public string _height { get; set; }
        public string _url { get; set; }
        public string _transparent { get; set; }
        public string _animation { get; set; }
        public string _color { get; set; }
        public string note { get; set; }
        public string clip_path { get; set; }
        public string _checked { get; set; }
        public string _type { get; set; }
        public string _value { get; set; }
        public string location { get; set; }
        public string new_pattern { get; set; }
        public string _priority { get; set; }
        public string off_width { get; set; }
        public string off_height { get; set; }
        public string off_url { get; set; }
        public string off_transparent { get; set; }
        public string off_animation { get; set; }
        public string off_color { get; set; }
        public string error_width { get; set; }
        public string error_height { get; set; }
        public string error_url { get; set; }
        public string error_transparent { get; set; }
        public string error_animation { get; set; }
        public string error_color { get; set; }
        public string pattern_id_off { get; set; }
        public string pattern_name_off { get; set; }
        public string pattern_id_error { get; set; }
        public string pattern_name_error { get; set; }
        public string s_status { get; set; }
        public string v_status { get; set; }
        public string detected { get; set; }
        public string operation_type { get; set; }
        public string _onMap_operation { get; set; }
        public string _onMap_status { get; set; }
    }

    [WebMethod]
    public object delete_device(string r_id)
    {
        var _cat = db.query("select [Category] From v_DeviceSetting WHERE _id='" + r_id + "'");
        var result = db.excute("update [s_DeviceSetting] set [valid] = '0' WHERE [_id] = '" + r_id + "'");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return new { _cat = _cat.Rows[0]["Category"].ToString() };
    }

    [WebMethod]
    public object create_device(string _cat, string _subCat, string _name, string _sub_id, string _floor, string room_id, string _ip, string _sort)
    {
        var result = db.excute("insert into [s_DeviceSetting] ([Category], [subCategory], [_name], [_name2], [sub_id], [floor], [room_id], [_ip], [valid], [_sort], _type, category_type ) Values ('" + _cat + "', '" + _subCat + "', '" + _name + "', '" + _name + "', '" + _sub_id + "', '" + _floor + "', " + room_id + ", '" + _ip + "', '1', '" + _sort + "', '" + _name + "', '" + _cat + "')");
        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return new
            {
                _cat
            };
    }

    [WebMethod]
    public string update_device(string _id, string Category, string subCategory, string _name, string sub_id, string floor, string room_id, string _ip, string _sort)
    {
        var result = db.excute("update [s_DeviceSetting] set [Category] = '" + Category + "',[subCategory] = '" + subCategory + "', [_name] = '" + _name + "', [sub_id] = '" + sub_id + "', [room_id] = " + room_id + ", [floor] = '" + floor + "', [_ip] = '" + _ip + "', [_sort] = '" + _sort + "' where [_id] = '" + _id + "'");
        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public string create_IOPoint(string d_id, string _operation, string _status, string isSend, string PLC_id, string PLC_sub, string CA, string IO_type, string c_id, string _sort)
    {
        var result = db.excute("insert into [s_DeviceIOPoint] ([deviceID], [_operation], [operationname], [_type], [_status], [isSend], [PLC_id], [PLC_sub], [CA], [IO_type], [c_id], [_sort], [utime], [confirm], [isUpdated]) Values ('" + d_id + "', '" + _operation + "', '" + _operation + "', '" + _operation + "', '" + _status + "', '" + isSend + "', '" + PLC_id + "', '" + PLC_sub + "', '" + CA + "', '" + IO_type + "', " + c_id + ", '" + _sort + "', getdate(), '1', '2')");
        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public string update_IOPoint(string _id, string _operation, string _status, string isSend, string PLC_id, string PLC_sub, string CA, string IO_type, string c_id, string _sort)
    {
        var result = db.excute("update [s_DeviceIOPoint] set [_operation] = '" + _operation + "', [_status] = '" + _status + "', [isSend] = '" + isSend + "', [PLC_id] = '" + PLC_id + "', [PLC_sub] = '" + PLC_sub + "', [CA] = '" + CA + "', [IO_type] = '" + IO_type + "', [c_id] = " + c_id + ", [_sort] = '" + _sort + "', [utime] = getdate() where [_id] = '" + _id + "'");
        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public string delete_IOPoint(string _id)
    {
        var result = db.excute("delete [s_DeviceIOPoint] where [_id] = '" + _id + "'");
        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetMaps()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var mcat = HttpContext.Current.Request.Params["mcat"].ToString(CultureInfo.CurrentCulture);
        var cat = HttpContext.Current.Request.Params["cat"].ToString(CultureInfo.CurrentCulture);

        var db = new DBAcess();
        DataTable dt = new DataTable();

        var str = "SELECT * FROM [u_def_Map] where [Category] = '" + mcat + "' and [MapType] = '" + cat + "' order by [sort]";

        dt = db.query(str);

        var records = new List<View>();

        try
        {
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var rows = new View();
                rows.MapId = dt.Rows[i]["_id"].ToString();
                rows.MapName = dt.Rows[i]["MapName"].ToString();
                rows.MapURI = dt.Rows[i]["MapURI"].ToString();
                rows._width = dt.Rows[i]["defaultWidth"].ToString();
                rows._x = dt.Rows[i]["offset_x"].ToString();
                rows._y = dt.Rows[i]["offset_y"].ToString();
                rows.sort = dt.Rows[i]["sort"].ToString();
                rows.deviceID = dt.Rows[i]["device_id"].ToString();

                records.Add(rows);
            }
        }
        catch
        {

        }


        dt.Dispose();

        /*
        if (!records.Any())
        {
            return string.Empty;
        }
        */

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            sb.Append("\"" + "<div class='m_MapName'>" + result.MapName + "</div>" + "\",");
            sb.Append("\"" + "<div class='m_MapURI'>" + result.MapURI + "</div>" + "\",");
            sb.Append("\"" + "<div class='m_width'>" + result._width + "</div>" + "\",");
            sb.Append("\"" + "<div class='m_x'>" + result._x + "</div>" + "\",");
            sb.Append("\"" + "<div class='m_y'>" + result._y + "</div>" + "\",");

            var sort = "";
            switch (result.sort)
            {
                case "0":
                    sort = "地下BF";
                    break;
                case "1":
                    sort = "樓層F";
                    break;
                case "2":
                    sort = "頂樓RF";
                    break;
            }
            sb.Append("\"" + "<div class='m_sort'>" + result.sort + "</div>" + "\",");
            sb.Append("\"" + "<div class='m_deviceID'>" + result.deviceID + "</div>" + "\",");

            sb.Append("\"" + "<div style='width:160px;'><div class='button icon-pencil' onclick='map_edit(" + result.MapId + ",this)' style='width:50px;cursor:pointer;'>編輯</div> <div class='button icon-trash' onclick='del_confirm(" + result.MapId + ")' style='width:50px;cursor:pointer;'>刪除</div></div>" + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");
        return sb.ToString();
    }

    [WebMethod]
    public object get_map_title(string m_id)
    {
        var str = "SELECT * FROM [u_def_Map] where [_id] = '" + m_id + "'";
        var dt = db.query(str);

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<string> row = new List<string>();

        return new
        {
            _id = dt.Rows[0]["_id"].ToString(),
            MapName = dt.Rows[0]["MapName"].ToString(),
            MapURI = dt.Rows[0]["MapURI"].ToString(),
            MapType = dt.Rows[0]["MapType"].ToString(),
            Category = dt.Rows[0]["Category"].ToString(),
            _width = dt.Rows[0]["defaultWidth"].ToString(),
            _x = dt.Rows[0]["offset_x"].ToString(),
            _y = dt.Rows[0]["offset_y"].ToString(),
            sort = dt.Rows[0]["sort"].ToString()
        };
    }

    [WebMethod]
    public object get_map_main_cat()
    {
        var str = "SELECT [Category] FROM [u_def_Map] group by [Category]";
        var dt = db.query(str);

        List<string> row = new List<string>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            row.Add(dt.Rows[i]["Category"].ToString());
        }

        return new
        {
            Category = row
        };
    }

    [WebMethod]
    public object get_map_cat(string cat)
    {
        var str = "SELECT [MapType] FROM [u_def_Map] where [Category] = '" + cat + "' group by [MapType]";
        var dt = db.query(str);

        List<string> row = new List<string>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            row.Add(dt.Rows[i]["MapType"].ToString());
        }

        return new
        {
            MapType = row
        };
    }

    [WebMethod]
    public object get_map_name_edit(string cat, string scat)
    {
        var dt = db.query("select * from ( SELECT a.[_id], case when c._name is not null then c._name else a.[MapName] end as MapName, a.[sort] FROM [u_def_Map] a left join [s_DeviceMapInfo] b on a.[_id] = b.[MapId] left join v_DeviceSetting c on a.device_id = c._id where a.[Category] = '" + cat + "' and a.[MapType] = '" + scat + "' ) x group by [_id], [MapName], [sort] order by [sort] , [MapName]");


        List<string> row = new List<string>();
        List<string> row2 = new List<string>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            row.Add(dt.Rows[i]["MapName"].ToString());
            row2.Add(dt.Rows[i]["_id"].ToString());
        }

        return new
        {
            MapName = row,
            _id = row2
        };
    }

    [WebMethod]
    public object get_map_name(string cat, string scat)
    {
        var dt = db.query("select * from ( SELECT a.[_id], case when d._name is not null then d._name else a.[MapName] end as MapName, a.[sort] FROM [u_def_Map] a inner join [s_DeviceMapInfo] b on a.[_id] = b.[MapId] inner join [s_MapIO_mapping] c on Convert(nvarchar(50),b.[_id]) = c.[DeviceMapInfo_id] left join v_DeviceSetting d on a.device_id = d._id where a.[Category] = '" + cat + "' and a.[MapType] = '" + scat + "' and c.[checked] = 'true' ) x group by [_id], [MapName], [sort] order by [sort], [MapName]");

        List<string> row = new List<string>();
        List<string> row2 = new List<string>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            row.Add(dt.Rows[i]["MapName"].ToString());
            row2.Add(dt.Rows[i]["_id"].ToString());
        }

        return new
        {
            MapName = row,
            _id = row2
        };
    }

    [WebMethod]
    public object get_map_name_by_device_cat(string cat, string scat, string dcat)
    {
        var str = "SELECT a.[_id], a.[MapName], a.[sort] FROM [u_def_Map] a inner join [s_DeviceMapInfo] b on a.[_id] = b.[MapId] inner join [s_MapIO_mapping] c on Convert(nvarchar(50),b.[_id]) = c.[DeviceMapInfo_id] left join [v_DeviceSetting] d on b.[device_ID] = d.[_id] where a.[Category] = '" + cat + "' and a.[MapType] = '" + scat + "' and d.[Category] = '" + dcat + "' group by a.[_id], a.[MapName], a.[sort] order by a.[sort] desc, a.[MapName] desc";
        var dt = db.query(str);

        List<string> row = new List<string>();
        List<string> row2 = new List<string>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            row.Add(dt.Rows[i]["MapName"].ToString());
            row2.Add(dt.Rows[i]["_id"].ToString());
        }

        return new
        {
            MapName = row,
            _id = row2
        };
    }

    [WebMethod]
    public object get_device_group()
    {
        var dt = db.query("SELECT * FROM [s_DeviceGroup]");

        var group = new List<id_and_name>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new id_and_name();

            row._id = dt.Rows[i]["_id"].ToString();
            row._name = dt.Rows[i]["_groupName"].ToString();

            group.Add(row);
        }

        return new
        {
            group
        };
    }

    [WebMethod]
    public object get_device_cat()
    {
        var str = "SELECT [Category] FROM [v_DeviceSetting] where [valid] = 1 group by [Category] order by [Category] ";
        var dt = db.query(str);

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<string> row = new List<string>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            row.Add(dt.Rows[i]["Category"].ToString());
        }

        return new
        {
            Category = row
        };
    }

    [WebMethod]
    public object get_device_name(string cat)
    {
        var str = "SELECT [_id],[_name] FROM [v_DeviceSetting] where [Category] = '" + cat + "' and valid = 1 order by [_name] ";
        var dt = db.query(str);

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<string> row1 = new List<string>();
        List<string> row2 = new List<string>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            row1.Add(dt.Rows[i]["_name"].ToString());
            row2.Add(dt.Rows[i]["_id"].ToString());
        }

        return new
        {
            _name = row1,
            _id = row2
        };
    }

    [WebMethod]
    public object get_device_name_by_supcat(string cat)
    {
        var str = "SELECT [_id],[_name] FROM [v_DeviceSetting] where [subCategory] = '" + cat + "' order by [_name] ";
        var dt = db.query(str);

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<string> row1 = new List<string>();
        List<string> row2 = new List<string>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            row1.Add(dt.Rows[i]["_name"].ToString());
            row2.Add(dt.Rows[i]["_id"].ToString());
        }

        return new
        {
            _name = row1,
            _id = row2
        };
    }

    [WebMethod]
    public object get_device_name_by_supcat_and_operation_on_off(string cat)
    {
        var str = "SELECT a.[_id], a.[_name] FROM [v_DeviceSetting] a inner join s_DeviceIOPoint b on a.[_id] = b.[deviceID] where [subCategory] = '" + cat + "' and b._operation in ('啟/停機','電源開關','警報開關') order by [_name] ";
        var dt = db.query(str);

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<string> row1 = new List<string>();
        List<string> row2 = new List<string>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            row1.Add(dt.Rows[i]["_name"].ToString());
            row2.Add(dt.Rows[i]["_id"].ToString());
        }

        return new
        {
            _name = row1,
            _id = row2
        };
    }

    [WebMethod]
    public object get_device_name_by_double_cat(string cat, string scat)
    {
        var str = "SELECT [_id],[_name] FROM [v_DeviceSetting] where [Category] = '" + cat + "' and [subCategory] = '" + scat + "' and valid = 1 order by [_name] ";
        var dt = db.query(str);

        var device = new List<id_and_name>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new id_and_name();
            row._id = dt.Rows[i]["_id"].ToString();
            row._name = dt.Rows[i]["_name"].ToString();

            device.Add(row);
        }

        return new
        {
            device
        };
    }

    [WebMethod]
    public object get_io_name_by_device_scat_and_name(string d_name, string scat)
    {
        var str = "";

        if(d_name == "%")
        {
            str = "SELECT b.[_operation] FROM [v_DeviceSetting] a inner join [s_DeviceIOPoint] b on a._id = b.deviceID and b.isSend = 1 where a.[_name] like '" + d_name + "' and a.subcategory = '" + scat + "' group by b.[_operation] ";
        }
        else
        {
            str = "SELECT b.[_id], b.[_operation] FROM [v_DeviceSetting] a inner join [s_DeviceIOPoint] b on a._id = b.deviceID and b.isSend = 1 where a.[_name] like '" + d_name + "' and a.subcategory = '" + scat + "'";
        }

        var dt = db.query(str);

        var io_name = new List<id_and_name>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new id_and_name();
            //row._id = dt.Rows[i]["_id"].ToString();
            row._name = dt.Rows[i]["_operation"].ToString();

            io_name.Add(row);
        }

        return new
        {
            io_name
        };
    }

    [WebMethod]
    public object get_io_name_by_device_cat_scat_and_name(string d_name, string scat, string cat)
    {
        var str = "";

        if (d_name == "%")
        {
            str = "SELECT b.[_operation] FROM [v_DeviceSetting] a inner join [s_DeviceIOPoint] b on a._id = b.deviceID where a.[_name] like '" + d_name + "' and a.subcategory = '" + scat + "' and a.category = '" + cat + "' group by b.[_operation] ";
        }
        else
        {
            str = "SELECT b.[_id], b.[_operation] FROM [v_DeviceSetting] a inner join [s_DeviceIOPoint] b on a._id = b.deviceID where a.[_name] like '" + d_name + "' and a.subcategory = '" + scat + "' and a.category = '" + cat + "'";
        }

        var dt = db.query(str);

        var io_name = new List<id_and_name>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new id_and_name();
            //row._id = dt.Rows[i]["_id"].ToString();
            row._name = dt.Rows[i]["_operation"].ToString();

            io_name.Add(row);
        }

        return new
        {
            io_name
        };
    }

    [WebMethod]
    public object create_map(string _cat, string _name, string _mapURI, string _subCat, string _width, string _x, string _y, string sort, string device_id)
    {
        var result = db.excute("insert into [u_def_Map] ([Category], [MapName], [MapURI], [MapType], [defaultWidth], [offset_x], [offset_y], [sort], [device_id] ) Values ('" + _cat + "', '" + _name + "', '" + _mapURI + "', '" + _subCat + "', '" + _width + "', '" + _x + "', '" + _y + "', '" + sort + "', " + device_id + " )");
        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return new
            {
                _cat = _cat,
                _subCat = _subCat
            };
    }

    [WebMethod]
    public string update_map(string _id, string _cat, string _subCat, string _name, string _mapURI, string _width, string _x, string _y, string sort, string device_id)
    {
        var result = db.excute("update [u_def_Map] set [MapName] = '" + _name + "', [MapURI] = '" + _mapURI + "', [MapType] = '" + _subCat + "', [Category] = '" + _cat + "', [defaultWidth] = '" + _width + "', [offset_x] = '" + _x + "', [offset_y] = '" + _y + "', [sort] = '" + sort + "', [device_id] = " + device_id + " where [_id] = '" + _id + "'");
        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object delete_map(string r_id)
    {
        var _cat = db.query("select [Category], [MapType] From [u_def_Map] WHERE _id='" + r_id + "'");
        var result = db.excute("DELETE From [u_def_Map] WHERE _id='" + r_id + "'");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return new
            {
                m_cat = _cat.Rows[0]["Category"].ToString(),
                _cat = _cat.Rows[0]["MapType"].ToString()
            };
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string get_deviceinfo()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var m_id = HttpContext.Current.Request.Params["m_id"].ToString(CultureInfo.CurrentCulture);

        var db = new DBAcess();

        var str = "SELECT a.*,c._name FROM [s_DeviceMapInfo] a left join [v_DeviceSetting] c on a.device_ID = c._id where a.[MapId] = '" + m_id + "' order by a.[device_ID]";

        var dt = db.query(str);
        var dt2 = db.query("select a.[_id] as 'i_id' , b.[_id], b.[_operation] From [s_DeviceMapInfo] a left join [s_DeviceIOPoint] b on a.[device_ID] = b.[deviceID] where a.[MapId] = '" + m_id + "' and b.[_id] is not null ");
        var dt3 = db.query("select b.[_id] From [s_DeviceMapInfo] a left join [s_DeviceIOPoint] b on a.[device_ID] = b.[deviceID] inner join [s_MapIO_mapping] c on a.[_id] = c.[DeviceMapInfo_id] and b.[_id] = c.[DeviceIOPoint_id] where a.[MapId] = '" + m_id + "'");

        var records = new List<View>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var rows = new View();
            rows._id = dt.Rows[i]["_id"].ToString();
            rows._name = dt.Rows[i]["_name"].ToString();
            rows._area = dt.Rows[i]["_area"].ToString();
            rows._x = dt.Rows[i]["_x"].ToString();
            rows._y = dt.Rows[i]["_y"].ToString();
            rows._onMap = dt.Rows[i]["_onMap"].ToString();
            rows.f_x = dt.Rows[i]["f_x"].ToString();
            rows.f_y = dt.Rows[i]["f_y"].ToString();
            rows.f_MapId = dt.Rows[i]["f_MapId"].ToString();
            rows._group = dt.Rows[i]["_group"].ToString();
            rows.pattern_id = dt.Rows[i]["pattern_id"].ToString();

            records.Add(rows);
        }



        dt.Dispose();

        /*
        if (!records.Any())
        {
            return string.Empty;
        }
        */

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            sb.Append("\"" + result._name + "\",");
            sb.Append("\"" + "<input type='text' class='input' value='" + result._area + "' />" + "\",");
            sb.Append("\"" + "<input type='text' class='input' value='" + result._x + "' oninput='onlyNumber(this)' />" + "\",");
            sb.Append("\"" + "<input type='text' class='input' value='" + result._y + "' oninput='onlyNumber(this)' />" + "\",");
            sb.Append("\"" + "<input type='checkbox' " + ((result._onMap == "True") ? "checked" : "") + " />" + "\",");
            sb.Append("\"" + "<input type='text' class='input' value='" + result.f_x + "' oninput='onlyNumber(this)' />" + "\",");
            sb.Append("\"" + "<input type='text' class='input' value='" + result.f_y + "' oninput='onlyNumber(this)' />" + "\",");
            sb.Append("\"" + "<input type='text' class='input' value='" + result.f_MapId + "' />" + "\",");
            sb.Append("\"" + "<input type='text' class='input' value='" + result._group + "' />" + "\",");
            sb.Append("\"" + "<input type='hidden' class='input' value='" + result.pattern_id + "' />" + "\",");

            var checkBoxes = "<div style='width:200px; text-align:left; margin:auto; border:1px gray solid; border-radius:5px; padding:5px;'>";
            var next = false;
            for (var i = 0; i < dt2.Rows.Count; i++)
            {
                if (dt2.Rows[i]["i_id"].ToString() == result._id)
                {
                    for (var j = 0; j < dt3.Rows.Count; j++)
                    {
                        if (dt3.Rows[j]["_id"].ToString() == dt2.Rows[i]["_id"].ToString())
                        {
                            checkBoxes += "<div><input type='checkbox' checked value='" + dt2.Rows[i]["_id"].ToString() + "'>" + dt2.Rows[i]["_operation"].ToString() + "</div>";
                            next = true;
                        }
                    }
                    if (!next)
                        checkBoxes += "<div><input type='checkbox' value='" + dt2.Rows[i]["_id"].ToString() + "'>" + dt2.Rows[i]["_operation"].ToString() + "</div>";
                    else
                        next = false;
                }
            }



            if (checkBoxes == "<div style='width:200px; text-align:left; margin:auto; border:1px gray solid; border-radius:5px; padding:5px;'>")
                checkBoxes += "無點位";

            checkBoxes += "</div>";

            sb.Append("\"" + checkBoxes + "\",");

            sb.Append("\"" + "<a id='" + result._id + "' class='button icon-save ' onclick='point_update(this)'> 儲存</a> <a id='" + result._id + "' class='button icon-trash ' onclick='del_confirm(this)'>刪除</a>" + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");
        return sb.ToString();
    }

    [WebMethod]
    public string create_deviceinfo(string m_id, string d_id)
    {
        var result = db.excute("insert into [s_DeviceMapInfo] ([device_ID], [MapId]) Values ('" + d_id + "', '" + m_id + "')");
        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public string create_deviceinfo_x_y(string m_id, string d_id, string _x, string _y)
    {
        var result = db.excute("insert into [s_DeviceMapInfo] ([device_ID], [MapId], [_x], [_y]) Values ('" + d_id + "', '" + m_id + "', '" + _x + "', '" + _y + "')");
        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public string delete_deviceinfo(string _id)
    {
        var result = db.excute("delete From [s_DeviceMapInfo] where [_id] = '" + _id + "'");
        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public string update_deviceinfo(string _id, string _area, string _x, string _y, string _onMap, string _onMap_operation, string _onMap_status, string f_x, string f_y, string f_MapId, string _group, string pattern_id, string pattern_id_off, string pattern_id_error, string z_index, string clip_path, string width, string height, string IO_id, string point_checked, string _priority)
    {
        var result = db.excute("update [s_DeviceMapInfo] set [_area] = '" + _area + "', [_x] = '" + _x + "', [_y] = '" + _y + "', [_onMap] = '" + _onMap + "', [_onMap_operation] = '" + _onMap_operation + "', [_onMap_status] = '" + _onMap_status + "', [f_x] = '" + f_x + "', [f_y] = '" + f_y + "', [f_MapId] = '" + f_MapId + "', [_group] = '" + _group + "', [utime] = getdate(), [pattern_id] = '" + pattern_id + "', [pattern_id_off] = '" + pattern_id_off + "', [pattern_id_error] = '" + pattern_id_error + "', [z_index] = '" + z_index + "', [clip_path] = '" + clip_path + "', [width] = '" + width + "', [height] = '" + height + "', [_priority] = '" + _priority + "' where [_id] = '" + _id + "'");
        
        /*
        var result2 = 0;

        var io = IO_id.Split(',');
        var check = point_checked.Split(',');

        
         * var count = 0;
        foreach (var i in io)
        {
            var isnull = db.query("select * from [s_MapIO_mapping] where [DeviceMapInfo_id] = '" + _id + "' and [DeviceIOPoint_id] = '" + i + "'").Rows.Count;
            if (isnull > 0)
            {
                if (check[count] == "false")
                {
                    result2 = db.excute("delete [s_MapIO_mapping] where [DeviceMapInfo_id] = '" + _id + "' and [DeviceIOPoint_id] = '" + i + "'");
                }
            }
            else
            {
                if (check[count] == "true")
                {
                    result2 = db.excute("insert into [s_MapIO_mapping] ([DeviceMapInfo_id], [DeviceIOPoint_id]) values ( '" + _id + "', '" + i + "')");
                }
            }
            count++;
        }
        */

        if (result != 0)
        {
            return JsonConvert.SerializeObject(true);
        }
        else
            return JsonConvert.SerializeObject(false);
    }

    [WebMethod]
    public string update_io_mapping(string i_id, string io_info)
    {
        var result = 0;
        var io = io_info.Split(',');

        for (var i = 0; i < (io.Length / 4); i++) 
        {
            var isnull = db.query("select * from [s_MapIO_mapping] where [DeviceMapInfo_id] = '" + i_id + "' and [DeviceIOPoint_id] = '" + io[i * 4] + "'").Rows.Count;
            if (isnull > 0)
            {
                result = db.excute("update [s_MapIO_mapping] set [checked] = '" + io[i * 4 + 1] + "', [_type] = '" + io[i * 4 + 2] + "', [_value] = '" + io[i * 4 + 3] + "' where [DeviceMapInfo_id] = '" + i_id + "' and [DeviceIOPoint_id] = '" + io[i * 4] + "' ");
            }
            else
            {
                result = db.excute("insert into [s_MapIO_mapping] ([DeviceMapInfo_id], [DeviceIOPoint_id], [checked], [_type], [_value]) values ( '" + i_id + "', '" + io[i * 4] + "', '" + io[i * 4 + 1] + "', '" + io[i * 4 + 2] + "', '" + io[i * 4 + 3] + "')");
            }
        }
        

        if (result != 0)
        {
            return JsonConvert.SerializeObject(true);
        }
        else
            return JsonConvert.SerializeObject(false);
    }

    [WebMethod]
    public object get_pattern()
    {
        var str = "SELECT [_id], [name] FROM [u_def_Pattern] order by [name]";
        var dt = db.query(str);

        List<string> row1 = new List<string>();
        List<string> row2 = new List<string>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            row1.Add(dt.Rows[i]["name"].ToString());
            row2.Add(dt.Rows[i]["_id"].ToString());
        }

        return new
        {
            name = row1,
            _id = row2
        };
    }

    [WebMethod]
    public object get_single_pattern(string p_id)
    {
        var dt = db.query("SELECT [_id], [name], [_width], [_height], [_url], [_transparent], [_color], [animation], [note] FROM [u_def_Pattern] WHERE _id = '" + p_id + "'");

        var pattern = new View();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            pattern.pattern_id = dt.Rows[i]["_id"].ToString();
            pattern.pattern_name = dt.Rows[i]["name"].ToString();
            pattern._width = dt.Rows[i]["_width"].ToString();
            pattern._height = dt.Rows[i]["_height"].ToString();
            pattern._url = dt.Rows[i]["_url"].ToString();
            pattern._transparent = dt.Rows[i]["_transparent"].ToString();
            pattern._color = dt.Rows[i]["_color"].ToString();
            pattern._animation = dt.Rows[i]["animation"].ToString();
            pattern.note = dt.Rows[i]["note"].ToString();
        }

        return new
        {
            pattern
        };
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public object GetPatterns()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var db = new DBAcess();

        var str = "SELECT * FROM [TSMCDB].[dbo].[u_def_Pattern] order by [name]";

        var dt = db.query(str);

        var records = new List<View>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var rows = new View();
            rows.pattern_id = dt.Rows[i]["_id"].ToString();
            rows.pattern_name = dt.Rows[i]["name"].ToString();
            rows._width = dt.Rows[i]["_width"].ToString();
            rows._height = dt.Rows[i]["_height"].ToString();
            rows._url = dt.Rows[i]["_url"].ToString();
            rows._color = dt.Rows[i]["_color"].ToString();
            rows._transparent = dt.Rows[i]["_transparent"].ToString();
            rows._animation = dt.Rows[i]["animation"].ToString();
            rows.note = dt.Rows[i]["note"].ToString();

            records.Add(rows);
        }

        dt.Dispose();

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            sb.Append("\"" + "<div id='" + result.pattern_id + "' class='button icon-eye ' style='width:50px;cursor:pointer;' onclick='point_view(this)'>預覽</div>" + "\",");
            sb.Append("\"" + "<div class='p_pattern_name'>" + result.pattern_name + "</div>" + "\",");
            sb.Append("\"" + "<div class='p_width'>" + result._width + "</div>" + "\",");
            sb.Append("\"" + "<div class='p_height'>" + result._height + "</div>" + "\",");
            sb.Append("\"" + "<div class='p_url'>" + result._url + "</div>" + "\",");
            sb.Append("\"" + "<div class='p_color p_minicolor'>" + result._color + "</div>" + "\",");
            sb.Append("\"" + "<div class='p_transparent'>" + result._transparent + "</div>" + "\",");
            sb.Append("\"" + "<div class='p_animation'>" + ((result._animation == "rotate_aa") ? "順時針旋轉" : "") + ((result._animation == "rotate_bb") ? "逆時針旋轉" : "") + ((result._animation == "alarm_light") ? "閃爍" : "") + "</div>" + "\",");
            sb.Append("\"" + "<div class='p_note'>" + result.note + "</div>" + "\",");

            sb.Append("\"" + "<div><div class='button icon-pencil ' style='width:50px;cursor:pointer;margin: 0 10px;' onclick='point_update(" + result.pattern_id + ",this)'>編輯</div><div class='button icon-trash ' style='width:50px;cursor:pointer;margin: 0 10px;' onclick='del_confirm(" + result.pattern_id + ",this)'>刪除</div></div>" + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");
        return new { str = sb.ToString() };
    }

    [WebMethod]
    public string delete_pattern(string _id)
    {
        var result = db.excute("delete From [u_def_Pattern] where [_id] = '" + _id + "'");
        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public string update_pattern(string _id, string pattern_name, string _width, string _height, string _url, string _color, string _transparent, string _animation, string _note)
    {
        var result = db.excute("update [u_def_Pattern] set [name] = '" + pattern_name + "', [_width] = '" + _width + "', [_height] = '" + _height + "', [_url] = '" + _url + "', [_color] = '" + _color + "', [_transparent] = '" + _transparent + "', [animation] = '" + _animation + "', [note] = '" + _note + "' where [_id] = '" + _id + "'");
        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public string create_pattern(string _name, string _width, string _height, string _url, string _color, string _transparent, string _animation, string _note)
    {
        var result = db.excute("insert into [u_def_Pattern] ([name], [_width], [_height], [_url], [_color], [_transparent], [animation], [note]) Values ('" + _name + "', '" + _width + "', '" + _height + "', '" + _url + "', '" + _color + "', '" + _transparent + "', '" + _animation + "', '" + _note + "') ");
        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object map_back_type(string _id)
    {
        var dt = db.query("select [Category], [MapType] From [u_def_Map] where [_id] = '" + _id + "'");
        if (dt.Rows.Count > 0)
        {
            return new
            {
                m_cat = dt.Rows[0]["Category"].ToString(),
                _cat = dt.Rows[0]["MapType"].ToString()
            };
        }
        else
            return JsonConvert.SerializeObject(false);
    }

    [WebMethod]
    public object device_back_type(string _id)
    {
        var dt = db.query("select [Category] From [v_DeviceSetting] where [_id] = '" + _id + "'");
        if (dt.Rows.Count > 0)
        {
            return new
            {
                _cat = dt.Rows[0]["Category"].ToString()
            };
        }
        else
            return JsonConvert.SerializeObject(false);
    }

    [WebMethod(EnableSession =true)]
    public string save_imgs(string _url, string type)
    {
        var fileUrl = _url.Split(',');
        var mimeType = _url.Replace("data:", "").Split(';');

        var fileExtension = GetMIMESupportedExt(mimeType[0]).FirstOrDefault();

        Guid g = Guid.NewGuid();

        var web_path = get_local_webPath();

        string fileNameWithPath = @"D:\Share\WEB\Center\upload\" + type + @"\" + g + fileExtension;

        using (FileStream fs = new FileStream(fileNameWithPath, FileMode.Create))
        {
            using (BinaryWriter bw = new BinaryWriter(fs))

            {
                byte[] data = Convert.FromBase64String(fileUrl[1]);
                bw.Write(data);
                bw.Close();
            }
        }

        //var newPath = fileNameWithPath.Replace("D:\\Images\\", "/Center/upload/");
        string newPath = @"/Center/upload/" + type + @"/" + g + fileExtension;

        return JsonConvert.SerializeObject(newPath);
    }

    public string get_local_webPath()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                switch (ip.ToString()) {
                    case "10.30.3.251":
                        return Session["webPath_Ollie"].ToString();
                    default:
                        return Session["webPath"].ToString();
                } 
            }
        }
        throw new Exception(Session["webPath"].ToString());
    }

    private static IEnumerable<string> GetMIMESupportedExt(string mime)
    {
        var linq = from item in Microsoft.Win32.Registry.ClassesRoot.GetSubKeyNames()
                   let key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(item)
                   let value = key.GetValue("Content Type")
                   where value != null && value.ToString().Equals(mime, StringComparison.CurrentCultureIgnoreCase)
                   select item;
        return linq;
    }

    [WebMethod]
    public object get_single_DeviceMapInfo(string i_id)
    {

        var dt = db.query("select a.*, b.[name] as d_name, e.[_name], b._url as d_url,b._transparent as d_transparent, b._color as d_color, b.animation as d_animation, c._url as off_url,c._transparent as off_transparent, c._color as off_color, c.animation as off_animation, c.name as off_name, d._url as error_url, d._transparent as error_transparent, d._color as error_color, d.animation as error_animation, d.name as error_name From [s_DeviceMapInfo] a left join [u_def_Pattern] b on a.[pattern_id] = b.[_id] left join [u_def_Pattern] c on a.[pattern_id_off] = c.[_id] left join [u_def_Pattern] d on a.[pattern_id_error] = d.[_id] inner join [v_DeviceSetting] e on a.[device_ID] = e.[_id] where a.[_id] = '" + i_id + "'");
        var dt2 = db.query("select b.[_id], b.[_operation] From [s_DeviceMapInfo] a left join [s_DeviceIOPoint] b on a.device_ID = b.deviceID where a.[_id] = '" + i_id + "'");

        if (dt.Rows.Count != 0)
        {
            var info = new View();

            info.i_id = dt.Rows[0]["_id"].ToString();
            info._id = dt.Rows[0]["device_ID"].ToString();
            info._name = dt.Rows[0]["_name"].ToString();
            info._floor = dt.Rows[0]["_floor"].ToString();
            info._area = dt.Rows[0]["_area"].ToString();
            info._x = dt.Rows[0]["_x"].ToString();
            info._y = dt.Rows[0]["_y"].ToString();
            info.MapId = dt.Rows[0]["MapId"].ToString();
            info._onMap = dt.Rows[0]["_onMap"].ToString();
            info._onMap_operation = dt.Rows[0]["_onMap_operation"].ToString();
            info._onMap_status = dt.Rows[0]["_onMap_status"].ToString();
            info.f_x = dt.Rows[0]["f_x"].ToString();
            info.f_y = dt.Rows[0]["f_y"].ToString();
            info.f_MapId = dt.Rows[0]["f_MapId"].ToString();
            info._group = dt.Rows[0]["_group"].ToString();
            info.z_index = dt.Rows[0]["z_index"].ToString();

            info.pattern_id = dt.Rows[0]["pattern_id"].ToString();
            info.pattern_name = dt.Rows[0]["d_name"].ToString();
            info._url = dt.Rows[0]["d_url"].ToString();
            info._transparent = dt.Rows[0]["d_transparent"].ToString();
            info._color = dt.Rows[0]["d_color"].ToString();
            info._animation = dt.Rows[0]["d_animation"].ToString();

            info.pattern_id_off = dt.Rows[0]["pattern_id_off"].ToString();
            info.pattern_name_off = dt.Rows[0]["off_name"].ToString();
            info.off_url = dt.Rows[0]["off_url"].ToString();
            info.off_transparent = dt.Rows[0]["off_transparent"].ToString();
            info.off_color = dt.Rows[0]["off_color"].ToString();
            info.off_animation = dt.Rows[0]["off_animation"].ToString();

            info.pattern_id_error = dt.Rows[0]["pattern_id_error"].ToString();
            info.pattern_name_error = dt.Rows[0]["error_name"].ToString();
            info.error_url = dt.Rows[0]["error_url"].ToString();
            info.error_transparent = dt.Rows[0]["error_transparent"].ToString();
            info.error_color = dt.Rows[0]["error_color"].ToString();
            info.error_animation = dt.Rows[0]["error_animation"].ToString();

            info.clip_path = dt.Rows[0]["clip_path"].ToString();
            info._width = dt.Rows[0]["width"].ToString();
            info._height = dt.Rows[0]["height"].ToString();
            info._priority = dt.Rows[0]["_priority"].ToString();

            var IO_id = new List<string>();
            var operations = new List<string>();
            for (var i = 0; i < dt2.Rows.Count; i++)
            {
                IO_id.Add(dt2.Rows[i]["_id"].ToString());
                operations.Add(dt2.Rows[i]["_operation"].ToString());
            }

            return new
            {
                info,
                IO_id,
                operations
            };
        }
        else
            return JsonConvert.SerializeObject(false);

    }

    [WebMethod]
    public object get_IOmapping(string i_id)
    {
        var dt = db.query("SELECT * FROM [s_MapIO_mapping] where [DeviceMapInfo_id] = '" + i_id + "'");

        var io = new List<io>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var rows = new io();

            rows.DeviceIOPoint_id = dt.Rows[i]["DeviceIOPoint_id"].ToString();
            rows._type = dt.Rows[i]["_type"].ToString();
            rows._value = dt.Rows[i]["_value"].ToString();

            io.Add(rows);
        }

        return new
        {
            io
        };
    }

    public class io
    {
        public string _id { get; set; }
        public string DeviceMapInfo_id { get; set; }
        public string DeviceIOPoint_id { get; set; }
        public string _type { get; set; }
        public string _value { get; set; }
    }

    [WebMethod]
    public object get_map_IOmapping(string m_id)
    {
        var dt = db.query("SELECT b.[device_ID], d.[_operation] FROM [s_MapIO_mapping] a inner join [s_DeviceMapInfo] b on a.[DeviceMapInfo_id] = b.[_id] inner join [u_def_Map] c on b.[MapId] = c.[_id] inner join [s_DeviceIOPoint] d on a.DeviceIOPoint_id = d._id where c.[_id] = '" + m_id + "'");

        var io = new List<string>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            io.Add(dt.Rows[i]["DeviceIOPoint_id"].ToString());
        }

        return new
        {
            io
        };
        
    }

    [WebMethod]
    public object get_map_IOmapping_by_iid(string i_id)
    {
        var dt = db.query("SELECT b.[_id], b.[_operation], b.[IO_type], b.[_status], d.[checked], d.[_type], d.[_value] FROM [s_DeviceMapInfo] a inner join [s_DeviceIOPoint] b on a.[device_ID] = b.[deviceID] and b.isSend = 1 left join [s_MapIO_mapping] d on Convert(nvarchar(50),b.[_id]) = d.[DeviceIOPoint_id] and Convert(nvarchar(50),a.[_id]) = d.[DeviceMapInfo_id] where a.[_id] = '" + i_id + "' order by b._sort, b._operation");

        var io = new List<View>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new View();

            row._id = dt.Rows[i]["_id"].ToString();
            row.operationname = dt.Rows[i]["_operation"].ToString();
            row.IO_type = dt.Rows[i]["IO_type"].ToString();
            row._status = dt.Rows[i]["_status"].ToString();
            row._checked = dt.Rows[i]["checked"].ToString();
            row._type = dt.Rows[i]["_type"].ToString();
            row._value = dt.Rows[i]["_value"].ToString();

            io.Add(row);
        }

        return new
        {
            io
        };
    }

    [WebMethod]
    public object get_map_and_device(string _id,string type)
    {
        var map = new View();
        var DeviceMapInfo = new List<View>();
        var oprations = new List<View>();
        var patterns = new List<View>();

        if ((type != "single") && (type != "add"))
        {
            var dt = db.query("select * From [u_def_Map] where [_id] = '" + _id + "'");
            if (dt.Rows.Count == 0)
            {
                return new { error = "無此地圖" };
            }

            map._url = dt.Rows[0]["MapURI"].ToString();
            map.MapType = dt.Rows[0]["MapType"].ToString();
            map._width = dt.Rows[0]["defaultWidth"].ToString();
            map._x = dt.Rows[0]["offset_x"].ToString();
            map._y = dt.Rows[0]["offset_y"].ToString();
        }

        var new_type = type.Split('_');

        var dt2 = new DataTable();
        switch (new_type[0])
        {
            case "edit":
                dt2 = db.query("SELECT a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, '' as detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id where a.MapId = '" + _id + "' and a.[valid] = 1 and b.[valid] = 1 group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height]");
                break;
            case "single":
                dt2 = db.query("SELECT a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, '' as detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id where a._id = '" + _id + "' and a.[valid] = 1 and b.[valid] = 1 group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height]");
                break;
            case "add":
                dt2 = db.query("SELECT top(1) a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, '' as detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id where a.MapId = '" + _id + "' and a.[valid] = 1 and b.[valid] = 1 group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height] order by a._id desc");
                break;
            case "normal":
                dt2 = db.query("SELECT a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, h.detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id inner join [s_MapIO_mapping] g on convert(nvarchar(50),a.[_id]) = g.[DeviceMapInfo_id] and convert(nvarchar(50),c.[_id]) = g.[DeviceIOPoint_id] left join Alarm_Detect h on h.deviceID = a.device_ID and h.detected = 1 where a.MapId = '" + _id + "' and a.[valid] = 1 and b.[valid] = 1 and g.checked = 'true' group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height], h.[detected]");
                break;
            case "dc":
                dt2 = db.query("SELECT a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, h.detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id inner join [s_MapIO_mapping] g on convert(nvarchar(50),a.[_id]) = g.[DeviceMapInfo_id] and convert(nvarchar(50),c.[_id]) = g.[DeviceIOPoint_id] left join Alarm_Detect h on h.deviceID = a.device_ID and h.detected = 1 where a.MapId = '" + _id + "' and a.[valid] = 1 and b.Category = '" + new_type[1] + "' and b.[valid] = 1 and g.checked = 'true' group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height], h.[detected]");
                break;
            case "aid":
                dt2 = db.query("SELECT a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, h.detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id inner join [s_MapIO_mapping] g on convert(nvarchar(50),a.[_id]) = g.[DeviceMapInfo_id] and convert(nvarchar(50),c.[_id]) = g.[DeviceIOPoint_id] left join Alarm_Detect h on h.deviceID = a.device_ID and h.detected = 1 where a.MapId = '" + _id + "' and a.[valid] = 1 and b._id = '" + new_type[1] + "' and b.[valid] = 1 and g.checked = 'true' group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height], h.[detected]");
                break;
        }

        if (dt2.Rows.Count != 0)
        {
            for (var i = 0; i < dt2.Rows.Count; i++)
            {
                var row = new View();

                row.i_id = dt2.Rows[i]["i_id"].ToString();
                row._id = dt2.Rows[i]["_id"].ToString();
                row._name = dt2.Rows[i]["_name"].ToString();
                row.Category = dt2.Rows[i]["Category"].ToString();
                row._x = dt2.Rows[i]["_x"].ToString();
                row._y = dt2.Rows[i]["_y"].ToString();
                row._onMap = dt2.Rows[i]["_onMap"].ToString();
                row._onMap_operation = dt2.Rows[i]["_onMap_operation"].ToString();
                row._onMap_status = dt2.Rows[i]["_onMap_status"].ToString();
                row.f_x = dt2.Rows[i]["f_x"].ToString();
                row.f_y = dt2.Rows[i]["f_y"].ToString();
                row.z_index = dt2.Rows[i]["z_index"].ToString();
                row._url = dt2.Rows[i]["_url"].ToString();
                row._color = dt2.Rows[i]["_color"].ToString();
                row._transparent = dt2.Rows[i]["_transparent"].ToString();
                row._animation = dt2.Rows[i]["animation"].ToString();
                row._width = dt2.Rows[i]["_width"].ToString();
                row._height = dt2.Rows[i]["_height"].ToString();
                row.off_url = dt2.Rows[i]["off_url"].ToString();
                row.off_color = dt2.Rows[i]["off_color"].ToString();
                row.off_transparent = dt2.Rows[i]["off_transparent"].ToString();
                row.off_animation = dt2.Rows[i]["off_animation"].ToString();
                row.off_width = dt2.Rows[i]["off_width"].ToString();
                row.off_height = dt2.Rows[i]["off_height"].ToString();
                row.error_url = dt2.Rows[i]["error_url"].ToString();
                row.error_color = dt2.Rows[i]["error_color"].ToString();
                row.error_transparent = dt2.Rows[i]["error_transparent"].ToString();
                row.error_animation = dt2.Rows[i]["error_animation"].ToString();
                row.error_width = dt2.Rows[i]["error_width"].ToString();
                row.error_height = dt2.Rows[i]["error_height"].ToString();
                row.detected = dt2.Rows[i]["detected"].ToString();

                DeviceMapInfo.Add(row);
            }
        }

        var dt3 = new DataTable();
        switch (new_type[0])
        {
            case "edit":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[_status], e.[_type], e.[_value], '' as s_status, '' as operation_type, f.detected FROM [s_DeviceMapInfo] a inner join [v_DeviceIOPoint] c on a.device_ID = c.deviceID inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c._id) = e.DeviceIOPoint_id) and e.[checked] = 'true' left join Alarm_Detect f on c._id = f.io_id where a.MapId = '" + _id + "' and c.isSend = 1 order by c.deviceID, c._sort, c._operation ");
                break;
            case "single":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[_status], e.[_type], e.[_value], '' as s_status, '' as operation_type, f.detected FROM [s_DeviceMapInfo] a inner join [v_DeviceIOPoint] c on a.device_ID = c.deviceID inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c._id) = e.DeviceIOPoint_id) and e.[checked] = 'true' left join Alarm_Detect f on c._id = f.io_id where a._id = '" + _id + "' and c.isSend = 1 order by c.deviceID, c._sort, c._operation ");
                break;
            case "add":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[_status], e.[_type], e.[_value], '' as s_status, '' as operation_type, f.detected FROM (select top(1) * from [s_DeviceMapInfo] where MapId = '" + _id + "' order by _id desc ) a inner join [v_DeviceIOPoint] c on a.device_ID = c.deviceID inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c._id) = e.DeviceIOPoint_id) and e.[checked] = 'true' left join Alarm_Detect f on c._id = f.io_id where c.isSend = 1 order by c.deviceID, c._sort, c._operation");
                break;
            case "normal":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[v_status] as _status, c.[_status] as s_status, c.[_type] as operation_type, e.[_type], e.[_value], f.detected FROM [s_DeviceMapInfo] a inner join [s_DeviceIOPoint] c on a.device_ID = c.deviceID inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c.[_id]) = e.DeviceIOPoint_id) left join Alarm_Detect f on c._id = f.io_id where a.MapId = '" + _id + "' and c.isSend = 1 and e.[checked] = 'true' order by c.deviceID, c._sort, c._operation");
                break;
            case "dc":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[v_status] as _status, c.[_status] as s_status, c.[_type] as operation_type, e.[_type], e.[_value], f.detected FROM [s_DeviceMapInfo] a inner join [s_DeviceIOPoint] c on a.device_ID = c.deviceID inner join s_DeviceSetting d on a.device_ID = d._id inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c.[_id]) = e.DeviceIOPoint_id) left join Alarm_Detect f on c._id = f.io_id where a.MapId = '" + _id + "' and c.isSend = 1  and d.category_type in ('" + new_type[1] + "') and e.[checked] = 'true' order by c._id, c._sort, c._operation");
                break;
            case "aid":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[v_status] as _status, c.[_status] as s_status, c.[_type] as operation_type, e.[_type], e.[_value], f.detected FROM [s_DeviceMapInfo] a inner join [s_DeviceIOPoint] c on a.device_ID = c.deviceID inner join s_DeviceSetting d on a.device_ID = d._id inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c.[_id]) = e.DeviceIOPoint_id) left join Alarm_Detect f on c._id = f.io_id where a.MapId = '" + _id + "' and c.isSend = 1  and d._id in ('" + new_type[1] + "') and e.[checked] = 'true' order by c._sort, c._operation");
                break;
        }

        if (dt3.Rows.Count != 0)
        {
            for (var i = 0; i < dt3.Rows.Count; i++)
            {
                var row = new View();

                row.i_id = dt3.Rows[i]["i_id"].ToString();
                row._id = dt3.Rows[i]["_id"].ToString();
                row.operationname = dt3.Rows[i]["_operation"].ToString();
                row._status = dt3.Rows[i]["_status"].ToString();
                row.s_status = dt3.Rows[i]["s_status"].ToString();
                row.operation_type = dt3.Rows[i]["operation_type"].ToString();
                row._type = dt3.Rows[i]["_type"].ToString();
                row._value = dt3.Rows[i]["_value"].ToString();
                row.detected = dt3.Rows[i]["detected"].ToString();

                oprations.Add(row);
            }
        }

        var dt4 = new DataTable();
        switch (new_type[0])
        {
            case "aid":
                dt4 = db.query("SELECT b.[subCategory], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height FROM [s_DeviceMapInfo] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id inner join [s_MapIO_mapping] g on convert(nvarchar(50),a.[_id]) = g.[DeviceMapInfo_id] and convert(nvarchar(50),c.[_id]) = g.[DeviceIOPoint_id] where a.MapId = '" + _id + "' and b.[valid] = 1 and g.checked = 'true' and b._id = '" + new_type[1] + "' group by b.[subCategory], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height]");
                break;
            default:
                dt4 = db.query("SELECT b.[subCategory], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height FROM [s_DeviceMapInfo] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id inner join [s_MapIO_mapping] g on convert(nvarchar(50),a.[_id]) = g.[DeviceMapInfo_id] and convert(nvarchar(50),c.[_id]) = g.[DeviceIOPoint_id] where a.MapId = '" + _id + "' and b.[valid] = 1 and g.checked = 'true' group by b.[subCategory], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height]");
                break;
        }

        
        if (dt4.Rows.Count != 0)
        {
            for (var i = 0; i < dt4.Rows.Count; i++)
            {
                var row = new View();

                row.subCategory = dt4.Rows[i]["subCategory"].ToString();
                row._url = dt4.Rows[i]["_url"].ToString();
                row._color = dt4.Rows[i]["_color"].ToString();
                row._transparent = dt4.Rows[i]["_transparent"].ToString();
                row._animation = dt4.Rows[i]["animation"].ToString();
                row._width = dt4.Rows[i]["_width"].ToString();
                row._height = dt4.Rows[i]["_height"].ToString();
                row.off_url = dt4.Rows[i]["off_url"].ToString();
                row.off_color = dt4.Rows[i]["off_color"].ToString();
                row.off_transparent = dt4.Rows[i]["off_transparent"].ToString();
                row.off_animation = dt4.Rows[i]["off_animation"].ToString();
                row.off_width = dt4.Rows[i]["off_width"].ToString();
                row.off_height = dt4.Rows[i]["off_height"].ToString();
                row.error_url = dt4.Rows[i]["error_url"].ToString();
                row.error_color = dt4.Rows[i]["error_color"].ToString();
                row.error_transparent = dt4.Rows[i]["error_transparent"].ToString();
                row.error_animation = dt4.Rows[i]["error_animation"].ToString();
                row.error_width = dt4.Rows[i]["error_width"].ToString();
                row.error_height = dt4.Rows[i]["error_height"].ToString();

                patterns.Add(row);
            }
        }

        return new
        {
            map,
            DeviceMapInfo,
            oprations,
            patterns
        };
    }

    [WebMethod]
    public object get_map_and_device_alarm(string _id, string type)
    {
        var map = new View();
        var DeviceMapInfo = new List<View>();
        var oprations = new List<View>();
        var patterns = new List<View>();

        if ((type != "single") && (type != "add"))
        {
            var dt = db.query("select * From [u_def_Map] where [_id] = '" + _id + "'");
            if (dt.Rows.Count == 0)
            {
                return new { error = "無此地圖" };
            }

            map._url = dt.Rows[0]["MapURI"].ToString();
            map.MapType = dt.Rows[0]["MapType"].ToString();
            map._width = dt.Rows[0]["defaultWidth"].ToString();
            map._x = dt.Rows[0]["offset_x"].ToString();
            map._y = dt.Rows[0]["offset_y"].ToString();
        }

        var new_type = type.Split('_');

        var dt2 = new DataTable();
        switch (new_type[0])
        {
            case "edit":
                dt2 = db.query("SELECT a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, '' as detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo_alarm] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id where a.MapId = '" + _id + "' and a.[valid] = 1 and b.[valid] = 1 group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height]");
                break;
            case "single":
                dt2 = db.query("SELECT a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, '' as detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo_alarm] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id where a._id = '" + _id + "' and a.[valid] = 1 and b.[valid] = 1 group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height]");
                break;
            case "add":
                dt2 = db.query("SELECT top(1) a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, '' as detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo_alarm] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id where a.MapId = '" + _id + "' and a.[valid] = 1 and b.[valid] = 1 group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height] order by a._id desc");
                break;
            case "normal":
                dt2 = db.query("SELECT a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, h.detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo_alarm] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id inner join [s_MapIO_mapping] g on convert(nvarchar(50),a.[_id]) = g.[DeviceMapInfo_id] and convert(nvarchar(50),c.[_id]) = g.[DeviceIOPoint_id] left join Alarm_Detect h on h.deviceID = a.device_ID and h.detected = 1 where a.MapId = '" + _id + "' and a.[valid] = 1 and b.[valid] = 1 and g.checked = 'true' group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height], h.[detected]");
                break;
            case "dc":
                dt2 = db.query("SELECT a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, h.detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo_alarm] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id inner join [s_MapIO_mapping] g on convert(nvarchar(50),a.[_id]) = g.[DeviceMapInfo_id] and convert(nvarchar(50),c.[_id]) = g.[DeviceIOPoint_id] left join Alarm_Detect h on h.deviceID = a.device_ID and h.detected = 1 where a.MapId = '" + _id + "' and a.[valid] = 1 and b.Category = '" + new_type[1] + "' and b.[valid] = 1 and g.checked = 'true' group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height], h.[detected]");
                break;
            case "aid":
                dt2 = db.query("SELECT a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, h.detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo_alarm] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id inner join [s_MapIO_mapping] g on convert(nvarchar(50),a.[_id]) = g.[DeviceMapInfo_id] and convert(nvarchar(50),c.[_id]) = g.[DeviceIOPoint_id] left join Alarm_Detect h on h.deviceID = a.device_ID and h.detected = 1 where a.MapId = '" + _id + "' and a.[valid] = 1 and b._id = '" + new_type[1] + "' and b.[valid] = 1 and g.checked = 'true' group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height], h.[detected]");
                break;
        }

        if (dt2.Rows.Count != 0)
        {
            for (var i = 0; i < dt2.Rows.Count; i++)
            {
                var row = new View();

                row.i_id = dt2.Rows[i]["i_id"].ToString();
                row._id = dt2.Rows[i]["_id"].ToString();
                row._name = dt2.Rows[i]["_name"].ToString();
                row.Category = dt2.Rows[i]["Category"].ToString();
                row._x = dt2.Rows[i]["_x"].ToString();
                row._y = dt2.Rows[i]["_y"].ToString();
                row._onMap = dt2.Rows[i]["_onMap"].ToString();
                row._onMap_operation = dt2.Rows[i]["_onMap_operation"].ToString();
                row._onMap_status = dt2.Rows[i]["_onMap_status"].ToString();
                row.f_x = dt2.Rows[i]["f_x"].ToString();
                row.f_y = dt2.Rows[i]["f_y"].ToString();
                row.z_index = dt2.Rows[i]["z_index"].ToString();
                row._url = dt2.Rows[i]["_url"].ToString();
                row._color = dt2.Rows[i]["_color"].ToString();
                row._transparent = dt2.Rows[i]["_transparent"].ToString();
                row._animation = dt2.Rows[i]["animation"].ToString();
                row._width = dt2.Rows[i]["_width"].ToString();
                row._height = dt2.Rows[i]["_height"].ToString();
                row.off_url = dt2.Rows[i]["off_url"].ToString();
                row.off_color = dt2.Rows[i]["off_color"].ToString();
                row.off_transparent = dt2.Rows[i]["off_transparent"].ToString();
                row.off_animation = dt2.Rows[i]["off_animation"].ToString();
                row.off_width = dt2.Rows[i]["off_width"].ToString();
                row.off_height = dt2.Rows[i]["off_height"].ToString();
                row.error_url = dt2.Rows[i]["error_url"].ToString();
                row.error_color = dt2.Rows[i]["error_color"].ToString();
                row.error_transparent = dt2.Rows[i]["error_transparent"].ToString();
                row.error_animation = dt2.Rows[i]["error_animation"].ToString();
                row.error_width = dt2.Rows[i]["error_width"].ToString();
                row.error_height = dt2.Rows[i]["error_height"].ToString();
                row.detected = dt2.Rows[i]["detected"].ToString();

                DeviceMapInfo.Add(row);
            }
        }

        var dt3 = new DataTable();
        switch (new_type[0])
        {
            case "edit":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[_status], e.[_type], e.[_value], '' as s_status, '' as operation_type, f.detected FROM [s_DeviceMapInfo_alarm] a inner join [v_DeviceIOPoint] c on a.device_ID = c.deviceID inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c._id) = e.DeviceIOPoint_id) and e.[checked] = 'true' left join Alarm_Detect f on c._id = f.io_id where a.MapId = '" + _id + "' and c.isSend = 1 order by c.deviceID, c._sort, c._operation ");
                break;
            case "single":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[_status], e.[_type], e.[_value], '' as s_status, '' as operation_type, f.detected FROM [s_DeviceMapInfo_alarm] a inner join [v_DeviceIOPoint] c on a.device_ID = c.deviceID inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c._id) = e.DeviceIOPoint_id) and e.[checked] = 'true' left join Alarm_Detect f on c._id = f.io_id where a._id = '" + _id + "' and c.isSend = 1 order by c.deviceID, c._sort, c._operation ");
                break;
            case "add":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[_status], e.[_type], e.[_value], '' as s_status, '' as operation_type, f.detected FROM (select top(1) * from [s_DeviceMapInfo_alarm] where MapId = '" + _id + "' order by _id desc ) a inner join [v_DeviceIOPoint] c on a.device_ID = c.deviceID inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c._id) = e.DeviceIOPoint_id) and e.[checked] = 'true' left join Alarm_Detect f on c._id = f.io_id where c.isSend = 1 order by c.deviceID, c._sort, c._operation");
                break;
            case "normal":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[v_status] as _status, c.[_status] as s_status, c.[_type] as operation_type, e.[_type], e.[_value], f.detected FROM [s_DeviceMapInfo_alarm] a inner join [s_DeviceIOPoint] c on a.device_ID = c.deviceID inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c.[_id]) = e.DeviceIOPoint_id) left join Alarm_Detect f on c._id = f.io_id where a.MapId = '" + _id + "' and c.isSend = 1 and e.[checked] = 'true' order by c.deviceID, c._sort, c._operation");
                break;
            case "dc":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[v_status] as _status, c.[_status] as s_status, c.[_type] as operation_type, e.[_type], e.[_value], f.detected FROM [s_DeviceMapInfo_alarm] a inner join [s_DeviceIOPoint] c on a.device_ID = c.deviceID inner join s_DeviceSetting d on a.device_ID = d._id inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c.[_id]) = e.DeviceIOPoint_id) left join Alarm_Detect f on c._id = f.io_id where a.MapId = '" + _id + "' and c.isSend = 1  and d.category_type in ('" + new_type[1] + "') and e.[checked] = 'true' order by c._id, c._sort, c._operation");
                break;
            case "aid":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[v_status] as _status, c.[_status] as s_status, c.[_type] as operation_type, e.[_type], e.[_value], f.detected FROM [s_DeviceMapInfo_alarm] a inner join [s_DeviceIOPoint] c on a.device_ID = c.deviceID inner join s_DeviceSetting d on a.device_ID = d._id inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c.[_id]) = e.DeviceIOPoint_id) left join Alarm_Detect f on c._id = f.io_id where a.MapId = '" + _id + "' and c.isSend = 1  and d._id in ('" + new_type[1] + "') and e.[checked] = 'true' order by c._sort, c._operation");
                break;
        }

        if (dt3.Rows.Count != 0)
        {
            for (var i = 0; i < dt3.Rows.Count; i++)
            {
                var row = new View();

                row.i_id = dt3.Rows[i]["i_id"].ToString();
                row._id = dt3.Rows[i]["_id"].ToString();
                row.operationname = dt3.Rows[i]["_operation"].ToString();
                row._status = dt3.Rows[i]["_status"].ToString();
                row.s_status = dt3.Rows[i]["s_status"].ToString();
                row.operation_type = dt3.Rows[i]["operation_type"].ToString();
                row._type = dt3.Rows[i]["_type"].ToString();
                row._value = dt3.Rows[i]["_value"].ToString();
                row.detected = dt3.Rows[i]["detected"].ToString();

                oprations.Add(row);
            }
        }

        var dt4 = new DataTable();
        switch (new_type[0])
        {
            case "aid":
                dt4 = db.query("SELECT b.[subCategory], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height FROM [s_DeviceMapInfo_alarm] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id inner join [s_MapIO_mapping] g on convert(nvarchar(50),a.[_id]) = g.[DeviceMapInfo_id] and convert(nvarchar(50),c.[_id]) = g.[DeviceIOPoint_id] where a.MapId = '" + _id + "' and b.[valid] = 1 and g.checked = 'true' and b._id = '" + new_type[1] + "' group by b.[subCategory], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height]");
                break;
            default:
                dt4 = db.query("SELECT b.[subCategory], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height FROM [s_DeviceMapInfo_alarm] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id inner join [s_MapIO_mapping] g on convert(nvarchar(50),a.[_id]) = g.[DeviceMapInfo_id] and convert(nvarchar(50),c.[_id]) = g.[DeviceIOPoint_id] where a.MapId = '" + _id + "' and b.[valid] = 1 and g.checked = 'true' group by b.[subCategory], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height]");
                break;
        }


        if (dt4.Rows.Count != 0)
        {
            for (var i = 0; i < dt4.Rows.Count; i++)
            {
                var row = new View();

                row.subCategory = dt4.Rows[i]["subCategory"].ToString();
                row._url = dt4.Rows[i]["_url"].ToString();
                row._color = dt4.Rows[i]["_color"].ToString();
                row._transparent = dt4.Rows[i]["_transparent"].ToString();
                row._animation = dt4.Rows[i]["animation"].ToString();
                row._width = dt4.Rows[i]["_width"].ToString();
                row._height = dt4.Rows[i]["_height"].ToString();
                row.off_url = dt4.Rows[i]["off_url"].ToString();
                row.off_color = dt4.Rows[i]["off_color"].ToString();
                row.off_transparent = dt4.Rows[i]["off_transparent"].ToString();
                row.off_animation = dt4.Rows[i]["off_animation"].ToString();
                row.off_width = dt4.Rows[i]["off_width"].ToString();
                row.off_height = dt4.Rows[i]["off_height"].ToString();
                row.error_url = dt4.Rows[i]["error_url"].ToString();
                row.error_color = dt4.Rows[i]["error_color"].ToString();
                row.error_transparent = dt4.Rows[i]["error_transparent"].ToString();
                row.error_animation = dt4.Rows[i]["error_animation"].ToString();
                row.error_width = dt4.Rows[i]["error_width"].ToString();
                row.error_height = dt4.Rows[i]["error_height"].ToString();

                patterns.Add(row);
            }
        }

        return new
        {
            map,
            DeviceMapInfo,
            oprations,
            patterns
        };
    }

    [WebMethod]
    public object get_map_and_device_color(string _id, string type)
    {
        var map = new View();
        var DeviceMapInfo = new List<View>();
        var oprations = new List<View>();
        var patterns = new List<View>();

        if ((type != "single") && (type != "add"))
        {
            var dt = db.query("select * From [u_def_Map] where [_id] = '" + _id + "'");
            if (dt.Rows.Count == 0)
            {
                return new { error = "無此地圖" };
            }

            map._url = dt.Rows[0]["MapURI"].ToString();
            map.MapType = dt.Rows[0]["MapType"].ToString();
            map._width = dt.Rows[0]["defaultWidth"].ToString();
            map._x = dt.Rows[0]["offset_x"].ToString();
            map._y = dt.Rows[0]["offset_y"].ToString();
        }

        var new_type = type.Split('_');

        var dt2 = new DataTable();
        switch (new_type[0])
        {
            case "edit":
                dt2 = db.query("SELECT a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, '' as detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo_color] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id where a.MapId = '" + _id + "' and a.[valid] = 1 and b.[valid] = 1 group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height]");
                break;
            case "single":
                dt2 = db.query("SELECT a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, '' as detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo_color] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id where a._id = '" + _id + "' and a.[valid] = 1 and b.[valid] = 1 group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height]");
                break;
            case "add":
                dt2 = db.query("SELECT top(1) a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, '' as detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo_color] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id where a.MapId = '" + _id + "' and a.[valid] = 1 and b.[valid] = 1 group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height] order by a._id desc");
                break;
            case "normal":
                dt2 = db.query("SELECT a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, h.detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo_color] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id inner join [s_MapIO_mapping] g on convert(nvarchar(50),a.[_id]) = g.[DeviceMapInfo_id] and convert(nvarchar(50),c.[_id]) = g.[DeviceIOPoint_id] left join Alarm_Detect h on h.deviceID = a.device_ID and h.detected = 1 where a.MapId = '" + _id + "' and a.[valid] = 1 and b.[valid] = 1 and g.checked = 'true' group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height], h.[detected]");
                break;
            case "dc":
                dt2 = db.query("SELECT a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, h.detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo_color] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id inner join [s_MapIO_mapping] g on convert(nvarchar(50),a.[_id]) = g.[DeviceMapInfo_id] and convert(nvarchar(50),c.[_id]) = g.[DeviceIOPoint_id] left join Alarm_Detect h on h.deviceID = a.device_ID and h.detected = 1 where a.MapId = '" + _id + "' and a.[valid] = 1 and b.Category = '" + new_type[1] + "' and b.[valid] = 1 and g.checked = 'true' group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height], h.[detected]");
                break;
            case "aid":
                dt2 = db.query("SELECT a.[_id] as i_id, b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[z_index], a.[f_y], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height, h.detected FROM [TSMCDB].[dbo].[s_DeviceMapInfo_color] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id inner join [s_MapIO_mapping] g on convert(nvarchar(50),a.[_id]) = g.[DeviceMapInfo_id] and convert(nvarchar(50),c.[_id]) = g.[DeviceIOPoint_id] left join Alarm_Detect h on h.deviceID = a.device_ID and h.detected = 1 where a.MapId = '" + _id + "' and a.[valid] = 1 and b._id = '" + new_type[1] + "' and b.[valid] = 1 and g.checked = 'true' group by a.[_id] , b.[_id], b.[_name], b.[Category], a.[_x], a.[_y], a.[_onMap], a.[_onMap_operation], a.[_onMap_status], a.[f_x], a.[f_y], a.[z_index], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height], h.[detected]");
                break;
        }

        if (dt2.Rows.Count != 0)
        {
            for (var i = 0; i < dt2.Rows.Count; i++)
            {
                var row = new View();

                row.i_id = dt2.Rows[i]["i_id"].ToString();
                row._id = dt2.Rows[i]["_id"].ToString();
                row._name = dt2.Rows[i]["_name"].ToString();
                row.Category = dt2.Rows[i]["Category"].ToString();
                row._x = dt2.Rows[i]["_x"].ToString();
                row._y = dt2.Rows[i]["_y"].ToString();
                row._onMap = dt2.Rows[i]["_onMap"].ToString();
                row._onMap_operation = dt2.Rows[i]["_onMap_operation"].ToString();
                row._onMap_status = dt2.Rows[i]["_onMap_status"].ToString();
                row.f_x = dt2.Rows[i]["f_x"].ToString();
                row.f_y = dt2.Rows[i]["f_y"].ToString();
                row.z_index = dt2.Rows[i]["z_index"].ToString();
                row._url = dt2.Rows[i]["_url"].ToString();
                row._color = dt2.Rows[i]["_color"].ToString();
                row._transparent = dt2.Rows[i]["_transparent"].ToString();
                row._animation = dt2.Rows[i]["animation"].ToString();
                row._width = dt2.Rows[i]["_width"].ToString();
                row._height = dt2.Rows[i]["_height"].ToString();
                row.off_url = dt2.Rows[i]["off_url"].ToString();
                row.off_color = dt2.Rows[i]["off_color"].ToString();
                row.off_transparent = dt2.Rows[i]["off_transparent"].ToString();
                row.off_animation = dt2.Rows[i]["off_animation"].ToString();
                row.off_width = dt2.Rows[i]["off_width"].ToString();
                row.off_height = dt2.Rows[i]["off_height"].ToString();
                row.error_url = dt2.Rows[i]["error_url"].ToString();
                row.error_color = dt2.Rows[i]["error_color"].ToString();
                row.error_transparent = dt2.Rows[i]["error_transparent"].ToString();
                row.error_animation = dt2.Rows[i]["error_animation"].ToString();
                row.error_width = dt2.Rows[i]["error_width"].ToString();
                row.error_height = dt2.Rows[i]["error_height"].ToString();
                row.detected = dt2.Rows[i]["detected"].ToString();

                DeviceMapInfo.Add(row);
            }
        }

        var dt3 = new DataTable();
        switch (new_type[0])
        {
            case "edit":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[_status], e.[_type], e.[_value], '' as s_status, '' as operation_type, f.detected,e.location FROM [s_DeviceMapInfo_color] a inner join [v_DeviceIOPoint] c on a.device_ID = c.deviceID inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c._id) = e.DeviceIOPoint_id) and e.[checked] = 'true' left join Alarm_Detect f on c._id = f.io_id where a.MapId = '" + _id + "' and c.isSend = 1 order by c.deviceID, c._sort, c._operation ");
                break;
            case "single":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[_status], e.[_type], e.[_value], '' as s_status, '' as operation_type, f.detected,e.location FROM [s_DeviceMapInfo_color] a inner join [v_DeviceIOPoint] c on a.device_ID = c.deviceID inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c._id) = e.DeviceIOPoint_id) and e.[checked] = 'true' left join Alarm_Detect f on c._id = f.io_id where a._id = '" + _id + "' and c.isSend = 1 order by c.deviceID, c._sort, c._operation ");
                break;
            case "add":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[_status], e.[_type], e.[_value], '' as s_status, '' as operation_type, f.detected,e.location FROM (select top(1) * from [s_DeviceMapInfo_color] where MapId = '" + _id + "' order by _id desc ) a inner join [v_DeviceIOPoint] c on a.device_ID = c.deviceID inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c._id) = e.DeviceIOPoint_id) and e.[checked] = 'true' left join Alarm_Detect f on c._id = f.io_id where c.isSend = 1 order by c.deviceID, c._sort, c._operation");
                break;
            case "normal":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[v_status] as _status, c.[_status] as s_status, c.[_type] as operation_type, e.[_type], e.[_value], f.detected,e.location  FROM [s_DeviceMapInfo_color] a inner join [s_DeviceIOPoint] c on a.device_ID = c.deviceID inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c.[_id]) = e.DeviceIOPoint_id) left join Alarm_Detect f on c._id = f.io_id where a.MapId = '" + _id + "' and c.isSend = 1 and e.[checked] = 'true' order by c.deviceID, c._sort, c._operation");
                break;
            case "dc":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[v_status] as _status, c.[_status] as s_status, c.[_type] as operation_type, e.[_type], e.[_value], f.detected,e.location FROM [s_DeviceMapInfo_color] a inner join [s_DeviceIOPoint] c on a.device_ID = c.deviceID inner join s_DeviceSetting d on a.device_ID = d._id inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c.[_id]) = e.DeviceIOPoint_id) left join Alarm_Detect f on c._id = f.io_id where a.MapId = '" + _id + "' and c.isSend = 1  and d.category_type in ('" + new_type[1] + "') and e.[checked] = 'true' order by c._id, c._sort, c._operation");
                break;
            case "aid":
                dt3 = db.query("SELECT a.[_id] as i_id, c.[_id], c.[_operation], c.[v_status] as _status, c.[_status] as s_status, c.[_type] as operation_type, e.[_type], e.[_value], f.detected,e.location  FROM [s_DeviceMapInfo_color] a inner join [s_DeviceIOPoint] c on a.device_ID = c.deviceID inner join s_DeviceSetting d on a.device_ID = d._id inner join [s_MapIO_mapping] e on (Convert(nvarchar(50),a.[_id]) = e.[DeviceMapInfo_id] and Convert(nvarchar(50),c.[_id]) = e.DeviceIOPoint_id) left join Alarm_Detect f on c._id = f.io_id where a.MapId = '" + _id + "' and c.isSend = 1  and d._id in ('" + new_type[1] + "') and e.[checked] = 'true' order by c._sort, c._operation");
                break;
        }

        if (dt3.Rows.Count != 0)
        {
            for (var i = 0; i < dt3.Rows.Count; i++)
            {
                var row = new View();

                row.i_id = dt3.Rows[i]["i_id"].ToString();
                row._id = dt3.Rows[i]["_id"].ToString();
                row.operationname = dt3.Rows[i]["_operation"].ToString();
                row._status = dt3.Rows[i]["_status"].ToString();
                row.s_status = dt3.Rows[i]["s_status"].ToString();
                row.operation_type = dt3.Rows[i]["operation_type"].ToString();
                row._type = dt3.Rows[i]["_type"].ToString();
                row._value = dt3.Rows[i]["_value"].ToString();
                row.location = dt3.Rows[i]["location"].ToString();
                row.detected = dt3.Rows[i]["detected"].ToString();
                oprations.Add(row);
            }
        }

        var dt4 = new DataTable();
        switch (new_type[0])
        {
            case "aid":
                dt4 = db.query("SELECT b.[subCategory], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height FROM [s_DeviceMapInfo_color] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id inner join [s_MapIO_mapping] g on convert(nvarchar(50),a.[_id]) = g.[DeviceMapInfo_id] and convert(nvarchar(50),c.[_id]) = g.[DeviceIOPoint_id] where a.MapId = '" + _id + "' and b.[valid] = 1 and g.checked = 'true' and b._id = '" + new_type[1] + "' group by b.[subCategory], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height]");
                break;
            default:
                dt4 = db.query("SELECT b.[subCategory], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url] as off_url, e.[_color] as off_color, e.[_transparent] as off_transparent, e.[animation] as off_animation, e.[_width] as off_width, e.[_height] as off_height, f.[_url] as error_url, f.[_color] as error_color, f.[_transparent] as error_transparent, f.[animation] as error_animation, f.[_width] as error_width, f.[_height] as error_height FROM [s_DeviceMapInfo_color] a inner join [v_DeviceSetting] b on a.device_ID = b._id left join [s_DeviceIOPoint] c on a.device_ID = c.deviceID left join [u_def_Pattern] d on a.pattern_id = d._id left join [u_def_Pattern] e on a.pattern_id_off = e._id left join [u_def_Pattern] f on a.pattern_id_error = f._id inner join [s_MapIO_mapping] g on convert(nvarchar(50),a.[_id]) = g.[DeviceMapInfo_id] and convert(nvarchar(50),c.[_id]) = g.[DeviceIOPoint_id] where a.MapId = '" + _id + "' and b.[valid] = 1 and g.checked = 'true' group by b.[subCategory], d.[_url], d.[_color], d.[_transparent], d.[animation], d.[_width], d.[_height], e.[_url], e.[_color], e.[_transparent], e.[animation], e.[_width], e.[_height], f.[_url], f.[_color], f.[_transparent], f.[animation], f.[_width], f.[_height]");
                break;
        }


        if (dt4.Rows.Count != 0)
        {
            for (var i = 0; i < dt4.Rows.Count; i++)
            {
                var row = new View();

                row.subCategory = dt4.Rows[i]["subCategory"].ToString();
                row._url = dt4.Rows[i]["_url"].ToString();
                row._color = dt4.Rows[i]["_color"].ToString();
                row._transparent = dt4.Rows[i]["_transparent"].ToString();
                row._animation = dt4.Rows[i]["animation"].ToString();
                row._width = dt4.Rows[i]["_width"].ToString();
                row._height = dt4.Rows[i]["_height"].ToString();
                row.off_url = dt4.Rows[i]["off_url"].ToString();
                row.off_color = dt4.Rows[i]["off_color"].ToString();
                row.off_transparent = dt4.Rows[i]["off_transparent"].ToString();
                row.off_animation = dt4.Rows[i]["off_animation"].ToString();
                row.off_width = dt4.Rows[i]["off_width"].ToString();
                row.off_height = dt4.Rows[i]["off_height"].ToString();
                row.error_url = dt4.Rows[i]["error_url"].ToString();
                row.error_color = dt4.Rows[i]["error_color"].ToString();
                row.error_transparent = dt4.Rows[i]["error_transparent"].ToString();
                row.error_animation = dt4.Rows[i]["error_animation"].ToString();
                row.error_width = dt4.Rows[i]["error_width"].ToString();
                row.error_height = dt4.Rows[i]["error_height"].ToString();

                patterns.Add(row);
            }
        }        
        return new
        {
            map,
            DeviceMapInfo,
            oprations,
            patterns
        };
    }

    [WebMethod]
    public object show_io_control(string i_id)
    {
        var dt = db.query("select * from (SELECT b.[_id], b.[_operation], b.[_status], a.[_type], a.[_value], c.detected, b._sort,a.kind FROM [s_MapIO_mapping] a inner join [v_DeviceIOPoint] b on a.[DeviceIOPoint_id] = Convert(nvarchar(50),b.[_id]) left join Alarm_Detect c on b._id = c.io_id and c.detected = 1 where a.[DeviceMapInfo_id] = '" + i_id + "' and a.checked = 'true' and b.isSend = 1 UNION SELECT b.[_id], b.[operationname], b.[_status], a.[_type], a.[_value], null as detected, b._sort,a.kind FROM [s_MapIO_mapping] a left join [s_DeviceIOPoint] b on a.[DeviceIOPoint_id] = Convert(nvarchar(50),b.[_id]) where a.[DeviceMapInfo_id] = '" + i_id + "' and a._type = 'camera' ) x order by x._sort, x.[_operation]");

        var io = new List<View>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new View();

            row._id = dt.Rows[i]["_id"].ToString();
            row.operationname = dt.Rows[i]["_operation"].ToString();
            row._status = dt.Rows[i]["_status"].ToString();
            row._type = dt.Rows[i]["_type"].ToString();
            row._value = dt.Rows[i]["_value"].ToString();
            row.detected = dt.Rows[i]["detected"].ToString();  
            row.kind = dt.Rows[i]["kind"].ToString();
            io.Add(row);
        }

        return new
        {
            io
        };
    }

    [WebMethod]
    public object show_io_control_color(string i_id)
    {
        var dt = db.query("select * from (SELECT b.[_id], b.[_operation], b.[_status], a.[_type], a.[_value], c.detected, b._sort,a.location FROM [s_MapIO_mapping] a inner join [v_DeviceIOPoint] b on a.[DeviceIOPoint_id] = Convert(nvarchar(50),b.[_id]) left join Alarm_Detect c on b._id = c.io_id and c.detected = 1 where a.[DeviceMapInfo_id] = '" + i_id + "' and a.checked = 'true' and b.isSend = 1 UNION SELECT b.[_id], b.[operationname], b.[_status], a.[_type], a.[_value], null as detected, b._sort,a.location FROM [s_MapIO_mapping] a left join [s_DeviceIOPoint] b on a.[DeviceIOPoint_id] = Convert(nvarchar(50),b.[_id]) where a.[DeviceMapInfo_id] = '" + i_id + "' and a._type = 'camera' ) x order by x._sort, x.[_operation]");

        var io = new List<View>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new View();

            row._id = dt.Rows[i]["_id"].ToString();
            row.operationname = dt.Rows[i]["_operation"].ToString();
            row._status = dt.Rows[i]["_status"].ToString();
            row._type = dt.Rows[i]["_type"].ToString();
            row._value = dt.Rows[i]["_value"].ToString();
            row.detected = dt.Rows[i]["detected"].ToString();
            row.location = dt.Rows[i]["location"].ToString();

            io.Add(row);
        }

        return new
        {
            io
        };
    }

    [WebMethod]
    public object get_vertical_devices(string cat)
    {
        var dt = db.query("SELECT c.[_id], c.[_name], c.[subCategory], d.[MapName], b.[_status] FROM [s_DeviceMapInfo] a left join [v_DeviceIOPoint] b on a.[device_ID] = b.[deviceID] and b.[_operation] = '運轉狀態' left join [v_DeviceSetting] c on a.[device_ID] = c.[_id] inner join [u_def_Map] d on a.[MapId] = d.[_id] where c.Category = '" + cat + "' and d.[sort] > 0 order by d.[sort] desc, d.[MapName] desc");
        var dt2 = db.query("SELECT c.[_id], c.[_name], c.[subCategory], d.[MapName], b.[_status] FROM [s_DeviceMapInfo] a left join [v_DeviceIOPoint] b on a.[device_ID] = b.[deviceID] and b.[_operation] = '運轉狀態' left join [v_DeviceSetting] c on a.[device_ID] = c.[_id] inner join [u_def_Map] d on a.[MapId] = d.[_id] where c.Category = '" + cat + "' and d.[sort] = 0 order by d.[sort] desc, d.[MapName] asc");

        var devices = new List<View>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new View();

            row._id = dt.Rows[i]["_id"].ToString();
            row._name = dt.Rows[i]["_name"].ToString();
            row.subCategory = dt.Rows[i]["subCategory"].ToString();
            row.MapName = dt.Rows[i]["MapName"].ToString();
            row._status = (dt.Rows[i]["_status"] == DBNull.Value) ? "Null" : dt.Rows[i]["_status"].ToString();

            devices.Add(row);
        }
        for (var i = 0; i < dt2.Rows.Count; i++)
        {
            var row = new View();

            row._id = dt2.Rows[i]["_id"].ToString();
            row._name = dt2.Rows[i]["_name"].ToString();
            row.subCategory = dt2.Rows[i]["subCategory"].ToString();
            row.MapName = dt2.Rows[i]["MapName"].ToString();

            devices.Add(row);
        }

        return new
        {
            devices
        };
    }

    [WebMethod]
    public object get_all_vertical_devices()
    {
        var dt = db.query("SELECT c.[_id], c.[_name], c.[Category], c.[subCategory], d.[MapName] FROM [s_DeviceMapInfo] a left join [v_DeviceSetting] c on a.[device_ID] = c.[_id] inner join [u_def_Map] d on a.[MapId] = d.[_id] where d.[sort] > 0 order by d.[sort] desc, d.[MapName] desc");
        var dt2 = db.query("SELECT c.[_id], c.[_name], c.[Category], c.[subCategory], d.[MapName] FROM [s_DeviceMapInfo] a left join [v_DeviceSetting] c on a.[device_ID] = c.[_id] inner join [u_def_Map] d on a.[MapId] = d.[_id] where d.[sort] = 0 order by d.[sort] desc, d.[MapName] asc");

        var devices = new List<View>();
        for (var i = 0; i < dt.Rows.Count ; i++)
        {
            var row = new View();

            row._id = dt.Rows[i]["_id"].ToString();
            row._name = dt.Rows[i]["_name"].ToString();
            row.Category = dt.Rows[i]["Category"].ToString();
            row.subCategory = dt.Rows[i]["subCategory"].ToString();
            row.MapName = dt.Rows[i]["MapName"].ToString();
            
            devices.Add(row);
        }
        for (var i = 0; i < dt2.Rows.Count; i++)
        {
            var row = new View();

            row._id = dt2.Rows[i]["_id"].ToString();
            row._name = dt2.Rows[i]["_name"].ToString();
            row.Category = dt2.Rows[i]["Category"].ToString();
            row.subCategory = dt2.Rows[i]["subCategory"].ToString();
            row.MapName = dt2.Rows[i]["MapName"].ToString();

            devices.Add(row);
        }

        return new
        {
            devices
        };
    }

    [WebMethod]
    public object get_device_supcat(string cat)
    {
        var dt = db.query("SELECT [subCategory] FROM [v_DeviceSetting] where [Category] = '" + cat + "' and valid = 1 group by [subCategory]");

        var supcat = new List<string>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            supcat.Add(dt.Rows[i]["subCategory"].ToString());
        }

        return new
        {
            supcat
        };
    }

    [WebMethod]
    public object get_operationname(string mcat, string cat)
    {
        var dt = db.query("SELECT b.[_operation] FROM [v_DeviceSetting] a left join [s_DeviceIOPoint] b on a.[_id] = b.[deviceID] where a.[Category] = '" + mcat + "' and a.[subCategory] = '" + cat + "' and a.[valid] = 1 and b.[isSend] = 1 group by b.[_sort], b.[_operation] order by b.[_sort], b.[_operation]");

        var operationname = new List<string>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            operationname.Add(dt.Rows[i]["_operation"].ToString());
        }

        return new
        {
            operationname
        };
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public object GetIObySupcat()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var cat = HttpContext.Current.Request.Params["cat"].ToString(CultureInfo.CurrentCulture);

        var dt1 = db.query("SELECT b.[_operation] FROM [v_DeviceSetting] a left join [s_DeviceIOPoint] b on a.[_id] = b.[deviceID] where a.[subCategory] = '" + cat + "' group by b.[_operation]");
        var dt2 = db.query("SELECT a.[_id], a.[_name] FROM [v_DeviceSetting] a left join [s_DeviceIOPoint] b on a.[_id] = b.[deviceID] where a.[subCategory] = '" + cat + "' group by a.[_id], a.[_name]");
        var dt = db.query("SELECT a.[_id], a.[_name], b.[_operation], b.[_status] FROM [v_DeviceSetting] a left join [s_DeviceIOPoint] b on a.[_id] = b.[deviceID] where a.[subCategory] = '" + cat + "' order by a.[_name], b.[_sort], b.[_operation]");

        var records = new List<string[]>();
        dt1.Rows.Add("default_name");
        dt1.Rows.Add("default_id");
        var rows = new string[dt1.Rows.Count];
        var last_device = dt.Rows[0]["_id"].ToString();
        var row_count = 0;

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["_id"].ToString() != last_device)
            {
                last_device = dt.Rows[i]["_id"].ToString();
                rows[0] = dt2.Rows[row_count]["_name"].ToString();
                rows[1] = dt2.Rows[row_count]["_id"].ToString();
                records.Add(rows);
                row_count++;
                rows = new string[dt1.Rows.Count];
            }

            for (var j = 0; j < dt1.Rows.Count; j++)
            {
                if (dt.Rows[i]["_operation"].ToString() == dt1.Rows[j]["_operation"].ToString())
                {
                    rows[j + 2] = dt.Rows[i]["_status"].ToString();
                }
            }

        }

        rows[0] = dt2.Rows[row_count]["_name"].ToString();
        rows[1] = dt2.Rows[row_count]["_id"].ToString();
        records.Add(rows);

        dt.Dispose();

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            sb.Append("\"" + "<div>" + result[0] + "</div>" + "\",");
            for (var j = 2; j < dt1.Rows.Count; j++)
            {
                sb.Append("\"" + "<div class='_status'>" + result[j] + "</div>" + "\",");
            }
            sb.Append("\"" + "<div><a id='" + result[1] + "' class='button icon-play ' style='width:50px;' onclick='device_on(this.id)'>啟動</a><a id='" + result[1] + "' class='button icon-stop ' style='width:50px;' onclick='device_on(this.id)'>停止</a><a id='" + result[1] + "' class='button icon-gear ' style='width:80px;' onclick='device_timeset(this.id)'>排程控制</a></div>" + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            for (var j = 0; j < dt1.Rows.Count - 1; j++)
            {
                sb.Append("\"" + "\",");
            }
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");

        return new
        {
            str = sb.ToString()
        };
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetGroups()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var fn = HttpContext.Current.Request.Params["fn"].ToString(CultureInfo.CurrentCulture);

        var dt = db.query("SELECT * FROM [s_DeviceGroup]");

        var records = new List<id_and_name>();

        try
        {
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var rows = new id_and_name();

                rows._id = dt.Rows[i]["_id"].ToString();
                rows._name = dt.Rows[i]["_groupName"].ToString();

                records.Add(rows);
            }
        }
        catch
        {

        }


        dt.Dispose();

        /*
        if (!records.Any())
        {
            return string.Empty;
        }
        */

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            sb.Append("\"" + result._id + "\",");
            sb.Append("\"" + result._name + "\",");
            sb.Append("\"" + "<div id='" + result._id + "' class='button icon-eye' onclick='view_member(this.id)' style='width:50px;margin:0 10px;'>檢視</div>" + "\",");

            if(fn == "management")
                sb.Append("\"" + "<div id='" + result._id + "_" + result._name + "' class='button icon-pencil' onclick='g_edit(this.id)' style='width:50px;margin:0 10px;'>編輯</div><div id='" + result._id + "' class='button icon-trash' onclick='del_confirm(this.id)' style='width:50px;margin:0 10px;'>刪除</div>" + "\"");
            if (fn == "control")
                sb.Append("\"" + "<div id='" + result._id + "' class='button icon-play' onclick=''>啟動</div><div id='" + result._id + "' class='button icon-stop' onclick=''>停止</div>" + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");
        return sb.ToString();
    }

    public class id_and_name
    {
        public string _id { get; set; }
        public string _name { get; set; }
    }

    [WebMethod]
    public object create_group(string _name)
    {
        var result = db.excute("insert into [s_DeviceGroup] ( [_groupName] ) Values ( '" + _name + "' )");
        
        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else 
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object delete_group(string _id)
    {
        var result = db.excute("delete [s_DeviceGroup] where [_id] = '" + _id + "'");
        result += db.excute("delete [s_DeviceGroupList] where [GroupID] = '" + _id + "'");

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object update_group(string _id, string _name)
    {
        var result = db.excute("update [s_DeviceGroup] set [_groupName] = '" + _name + "' where [_id] = '" + _id + "'");

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object get_group_member(string _id)
    {
        var dt = db.query("SELECT b.[_id], b.[Category], b.[subCategory], b.[_name] FROM [s_DeviceGroupList] a inner join [v_DeviceSetting] b on a.[DeviceID] = b.[_id] where [GroupID] = '" + _id + "' and b.[valid] = 1 order by b.[Category], b.[subCategory], b.[_name]");

        var member = new List<View>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new View();

            row.Category = dt.Rows[i]["Category"].ToString();
            row.subCategory = dt.Rows[i]["subCategory"].ToString();
            row._name = dt.Rows[i]["_name"].ToString();
            row._id = dt.Rows[i]["_id"].ToString();

            member.Add(row);
        }

        return new { member };
    }

    [WebMethod]
    public object add_member(string _id, string g_id)
    {
        var isDup = db.query("select * from [s_DeviceGroupList] where [GroupID] = '" + g_id + "' and [DeviceID] = '" + _id + "'").Rows.Count;
        var result = 0;
        if (isDup == 0)
        {
            result = db.excute("insert into [s_DeviceGroupList] ( [GroupID], [DeviceID] ) Values ( '" + g_id + "', '" + _id + "' )");
        }

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object remove_member(string _id, string g_id)
    {
        var result = db.excute("delete [s_DeviceGroupList] where [GroupID] = '" + g_id + "' and [DeviceID] = '" + _id + "' ");

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object get_single_device_group(string _id)
    {
        var dt = db.query("SELECT c.[_id], c.[_groupName] FROM [v_DeviceSetting] a left join [s_DeviceGroupList] b on a.[_id] = b.[DeviceID] left join [s_DeviceGroup] c on b.[GroupID] = c.[_id] where a.[_id] = '" + _id + "' order by c.[_groupName]");

        var groups = new List<id_and_name>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new id_and_name();

            row._id = dt.Rows[i]["_id"].ToString();
            row._name = dt.Rows[i]["_groupName"].ToString();

            groups.Add(row);
        }

        return new { groups };
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetGroupTimeSettings()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var t_id = HttpContext.Current.Request.Params["t_id"].ToString(CultureInfo.CurrentCulture);
        var t_type = HttpContext.Current.Request.Params["t_type"].ToString(CultureInfo.CurrentCulture);

        var dt = db.query("SELECT a.[_id], a.[stime], a.[etime], b.[_groupName], a.[DayofWeekList], a.[valid], a.[note] FROM [s_TimeSetting_Group] a inner join [s_DeviceGroup] b on a.[groupID] = b.[_id] order by a.[stime]");

        var records = new List<TimeSetting>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var rows = new TimeSetting();

            rows._id = dt.Rows[i]["_id"].ToString();
            rows.s_time = dt.Rows[i]["stime"].ToString();
            rows.e_time = dt.Rows[i]["etime"].ToString();
            rows.group_list = dt.Rows[i]["_groupName"].ToString();
            rows.day_of_week = dt.Rows[i]["DayofWeekList"].ToString();
            rows.valid = dt.Rows[i]["valid"].ToString();
            rows.note = dt.Rows[i]["note"].ToString();

            records.Add(rows);
        }

        dt.Dispose();

        /*
        if (!records.Any())
        {
            return string.Empty;
        }
        */

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            sb.Append("\"" + "<input id='g_" + result._id + "' type='checkbox' class='checkbox " + ((t_id == result._id && t_type == "Device") ? "default_row" : "") + "' value='" + result._id + "' onclick='set_valid(this)' " + ((result.valid == "True") ?"checked":"") + "/>" + "\",");
            sb.Append("\"" + result.s_time + "\",");
            sb.Append("\"" + result.e_time + "\",");
            sb.Append("\"" + result.group_list + "\",");

            var week = result.day_of_week.Split(',');
            var response = "";
            foreach (var i in week)
            {
                switch (i)
                {
                    case "1":
                        response += "星期日";
                        break;
                    case "2":
                        response += "星期一";
                        break;
                    case "3":
                        response += "星期二";
                        break;
                    case "4":
                        response += "星期三";
                        break;
                    case "5":
                        response += "星期四";
                        break;
                    case "6":
                        response += "星期五";
                        break;
                    case "7":
                        response += "星期六";
                        break;
                    case "0":
                        response += "&emsp;&emsp;&emsp;";
                        break;
                }
                response += " ";
            }
            sb.Append("\"" + response + "\",");
            sb.Append("\"" + result.note + "\",");

            sb.Append("\"" + "<div id='" + result._id + "' class='button icon-pencil' onclick='group_set_edit(this.id)'>編輯</div><div id='" + result._id + "' class='button icon-trash' onclick='del_group_set_confirm(this.id)'>刪除</div>" + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");
        return sb.ToString();
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetDeviceTimeSettings()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var t_id = HttpContext.Current.Request.Params["t_id"].ToString(CultureInfo.CurrentCulture);
        var t_type = HttpContext.Current.Request.Params["t_type"].ToString(CultureInfo.CurrentCulture);

        var dt = db.query("SELECT a.[_id], a.[stime], a.[etime], b.[_id] as deviceID, b.[_name], a.[DayofWeekList], a.[valid], a.[note] FROM [s_TimeSetting_Device] a inner join [v_DeviceSetting] b on a.[deviceID] = b.[_id] order by a.[stime]");

        var records = new List<TimeSetting>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var rows = new TimeSetting();

            rows._id = dt.Rows[i]["_id"].ToString();
            rows.s_time = dt.Rows[i]["stime"].ToString();
            rows.e_time = dt.Rows[i]["etime"].ToString();
            rows.deviceID = dt.Rows[i]["deviceID"].ToString();
            rows._name = dt.Rows[i]["_name"].ToString();
            rows.day_of_week = dt.Rows[i]["DayofWeekList"].ToString();
            rows.valid = dt.Rows[i]["valid"].ToString();
            rows.note = dt.Rows[i]["note"].ToString();

            records.Add(rows);
        }

        dt.Dispose();

        /*
        if (!records.Any())
        {
            return string.Empty;
        }
        */

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            sb.Append("\"" + "<input id='d_" + result._id + "' type='checkbox' class='checkbox " + ((t_id == result._id && t_type == "Device") ? "default_row":"") + "' value='" + result.valid + "' onclick='set_valid(this)' " + ((result.valid == "True") ? "checked" : "") + "/>" + "\",");
            sb.Append("\"" + result.s_time + "\",");
            sb.Append("\"" + result.e_time + "\",");
            sb.Append("\"" + result._name + "\",");

            var week = result.day_of_week.Split(',');
            var response = "";
            foreach (var i in week)
            {
                switch (i)
                {
                    case "1":
                        response += "星期日";
                        break;
                    case "2":
                        response += "星期一";
                        break;
                    case "3":
                        response += "星期二";
                        break;
                    case "4":
                        response += "星期三";
                        break;
                    case "5":
                        response += "星期四";
                        break;
                    case "6":
                        response += "星期五";
                        break;
                    case "7":
                        response += "星期六";
                        break;
                    case "0":
                        response += "&emsp;&emsp;&emsp;";
                        break;
                }
                response += " ";
            }
            sb.Append("\"" + response + "\",");
            sb.Append("\"" + result.note + "\",");

            sb.Append("\"" + "<div id='" + result._id + "' class='button icon-pencil ' onclick='device_set_edit(this.id)'>編輯</div><div id='" + result._id + "' class='button icon-trash' onclick='del_device_set_confirm(this.id)'>刪除</div>" + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");
        return sb.ToString();
    }

    public class TimeSetting
    {
        public string _id { get; set; }
        public string s_time { get; set; }
        public string e_time { get; set; }
        public string group_list { get; set; }
        public string Category { get; set; }
        public string subCategory { get; set; }
        public string _name { get; set; }
        public string deviceID { get; set; }
        public string day_of_week { get; set; }
        public string valid { get; set; }
        public string note { get; set; }

    }

    [WebMethod]
    public object create_device_timeset(string stime, string etime, string d_id, string weeklist, string note)
    {
        var result = 0;
        if (stime == "不設定")
            result = db.excute("insert into [s_TimeSetting_Device] ([stime], [etime], [deviceID], [DayofWeekList], [note]) values ( null, '" + etime + "', '" + d_id + "', '" + weeklist + "', '" + note + "')");
        else
        {
            if (etime == "不設定")
                result = db.excute("insert into [s_TimeSetting_Device] ([stime], [etime], [deviceID], [DayofWeekList], [note]) values ('" + stime + "', null, '" + d_id + "', '" + weeklist + "', '" + note + "')");
            else
                result = db.excute("insert into [s_TimeSetting_Device] ([stime], [etime], [deviceID], [DayofWeekList], [note]) values ('" + stime + "', '" + etime + "', '" + d_id + "', '" + weeklist + "', '" + note + "')");
        }

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object create_group_timeset(string stime, string etime, string g_id, string weeklist, string note)
    {
        var result = 0;
        if (stime == "不設定")
            result = db.excute("insert into [s_TimeSetting_Group] ([stime], [etime], [groupID], [DayofWeekList], [note]) values (null, '" + etime + "', '" + g_id + "', '" + weeklist + "', '" + note + "')");
        else
        {
            if (etime == "不設定")
                result = db.excute("insert into [s_TimeSetting_Group] ([stime], [etime], [groupID], [DayofWeekList], [note]) values ('" + stime + "', null, '" + g_id + "', '" + weeklist + "', '" + note + "')");
            else
                result = db.excute("insert into [s_TimeSetting_Group] ([stime], [etime], [groupID], [DayofWeekList], [note]) values ('" + stime + "', '" + etime + "', '" + g_id + "', '" + weeklist + "', '" + note + "')");
        }

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object delete_device_set(string _id)
    {
        var result = db.excute("delete [s_TimeSetting_Device] where [_id] = '" + _id + "' ");

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object delete_group_set(string _id)
    {
        var result = db.excute("delete [s_TimeSetting_Group] where [_id] = '" + _id + "' ");

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object get_single_device_set(string _id)
    {
        var dt = db.query("SELECT a.[_id], a.[stime], a.[etime], b.[Category], b.[subCategory], a.[deviceID], a.[DayofWeekList], a.[note] FROM [s_TimeSetting_Device] a left join [v_DeviceSetting] b on a.[deviceID] = b._id where a.[_id] = '" + _id + "'");

        var device_set = new List<TimeSetting>();

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new TimeSetting();

            row._id = dt.Rows[i]["_id"].ToString();
            row.s_time = dt.Rows[i]["stime"].ToString();
            row.e_time = dt.Rows[i]["etime"].ToString();
            row.Category = dt.Rows[i]["Category"].ToString();
            row.subCategory = dt.Rows[i]["subCategory"].ToString();
            row.deviceID = dt.Rows[i]["deviceID"].ToString();
            row.day_of_week = dt.Rows[i]["DayofWeekList"].ToString();
            row.note = dt.Rows[i]["note"].ToString();

            device_set.Add(row);
        }

        return new { device_set };
    }

    [WebMethod]
    public object get_single_group_set(string _id)
    {
        var dt = db.query("SELECT [_id], [stime], [etime], [groupID], [DayofWeekList], [note] FROM [s_TimeSetting_Group] where [_id] = '" + _id + "'");

        var group_set = new List<TimeSetting>();
            
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new TimeSetting();

            row._id = dt.Rows[i]["_id"].ToString();
            row.s_time = dt.Rows[i]["stime"].ToString();
            row.e_time = dt.Rows[i]["etime"].ToString();
            row.group_list = dt.Rows[i]["groupID"].ToString();
            row.day_of_week = dt.Rows[i]["DayofWeekList"].ToString();
            row.note = dt.Rows[i]["note"].ToString();

            group_set.Add(row);
        }

        return new { group_set };
    }

    [WebMethod]
    public object update_device_timeset(string _id, string stime, string etime, string d_id, string weeklist, string note)
    {
        var result = 0;
        if(stime == "不設定")
            result = db.excute("update [s_TimeSetting_Device] set [stime] = null, [etime] = '" + etime + "', [deviceID] = '" + d_id + "', [DayofWeekList] =  '" + weeklist + "', [note] =  '" + note + "' where [_id] = '" + _id + "'");
        else
        {
            if (etime == "不設定")
                result = db.excute("update [s_TimeSetting_Device] set [stime] = '" + stime + "', [etime] = null, [deviceID] = '" + d_id + "', [DayofWeekList] =  '" + weeklist + "', [note] =  '" + note + "' where [_id] = '" + _id + "'");
            else
                result = db.excute("update [s_TimeSetting_Device] set [stime] = '" + stime + "', [etime] = '" + etime + "', [deviceID] = '" + d_id + "', [DayofWeekList] =  '" + weeklist + "', [note] =  '" + note + "' where [_id] = '" + _id + "'");
        }

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object update_group_timeset(string _id, string stime, string etime, string g_id, string weeklist, string note)
    {
        var result = 0;
        if (stime == "不設定")
            result = db.excute("update [s_TimeSetting_Group] set [stime] = null, [etime] = '" + etime + "', [groupID] = '" + g_id + "', [DayofWeekList] =  '" + weeklist + "', [note] =  '" + note + "' where [_id] = '" + _id + "'");
        else
        {
            if (etime == "不設定")
                result = db.excute("update [s_TimeSetting_Group] set [stime] = '" + stime + "', [etime] = null, [groupID] = '" + g_id + "', [DayofWeekList] =  '" + weeklist + "', [note] =  '" + note + "' where [_id] = '" + _id + "'");
            else
                result = db.excute("update [s_TimeSetting_Group] set [stime] = '" + stime + "', [etime] = '" + etime + "', [groupID] = '" + g_id + "', [DayofWeekList] =  '" + weeklist + "', [note] =  '" + note + "' where [_id] = '" + _id + "'");
        }

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object get_single_device_cat_and_scat(string _id)
    {
        var dt = db.query("SELECT [Category], [subCategory] FROM [v_DeviceSetting] where [_id] = '" + _id + "'");

        var device = new List<View>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new View();

            row.Category = dt.Rows[i]["Category"].ToString();
            row.subCategory = dt.Rows[i]["subCategory"].ToString();

            device.Add(row);
        }

        return new
        {
            device
        };
    }

    [WebMethod]
    public object Get_IO_bySupcat(string mcat, string cat, string datatype)
    {
        var dt1 = db.query("SELECT b.[_operation], b.[IO_type] FROM [v_DeviceSetting] a left join [s_DeviceIOPoint] b on a.[_id] = b.[deviceID] where a.[Category] = '" + mcat + "' and a.[subCategory] = '" + cat + "' and a.[valid] = 1 and b.[isSend] = 1 group by b.[_operation], b.[IO_type], b.[_sort] order by b.[_sort], b.[_operation]");
        var dt2 = db.query("SELECT a.[_id], a.[_name] FROM [v_DeviceSetting] a left join [s_DeviceIOPoint] b on a.[_id] = b.[deviceID] left join [s_DeviceSetting] c on a.[_id] = c.[_id] where a.[Category] = '" + mcat + "' and a.[subCategory] = '" + cat + "' and a.[valid] = 1 " + ((dt1.Rows[0]["_operation"].ToString() == "對講機編號") ? "group by a.[_id], a.[_name], b.[_status] order by b.[_status]" : "group by a.[_id], a.[_name], a.[floor], a.[sub_id], c._sort,c.[_name], c.[floor], c.[sub_id], c.room_id order by c._sort,c.[_name], c.[floor], c.[sub_id], c.room_id"));
        var dt = db.query("SELECT a.[_id], a.[_name], b.[_operation], b.[_status], b.[_id] as io_id, c.[_type], c.[_value], f.detected FROM [v_DeviceSetting] a left join [v_DeviceIOPoint] b on a.[_id] = b.[deviceID] left join [s_MapIO_mapping] c on replace(c.[DeviceMapInfo_id],'_DeviceList','') = a.[_id] and Convert(nvarchar(50),b.[_id]) = c.[DeviceIOPoint_id] left join [s_DeviceSetting] d on a.[_id] = d.[_id] left join u_def_Room e on d.room_id = e.room_id left join Alarm_Detect f on b._id = f.io_id where a.[Category] = '" + mcat + "' and a.[subCategory] = '" + cat + "' and a.[valid] = 1 and b.[isSend] = 1 order by " + ((dt1.Rows[0]["_operation"].ToString() == "對講機編號") ? "b.[_status]" : "d._sort,d.[_name], d.[floor], d.[sub_id], e.room_id, b.[_sort], b.[_operation]") );

        var records = new List<string[]>();
        dt1.Rows.Add("default_name");
        dt1.Rows.Add("default_id");
        dt1.Rows.Add("default_io_id");
        dt1.Rows.Add("default_type");
        dt1.Rows.Add("default_value");
        var rows = new string[dt1.Rows.Count + 2];
        var last_device = dt.Rows[0]["_id"].ToString();
        var last_io_position = 0;
        var row_count = 0;
        var io_id = "";
        var io_type = "";
        var io_value = "";
        var is_AI = "";
        var detected = "";
        for (var i=0;i< dt1.Rows.Count; i++)
        {
            if(dt1.Rows[i]["IO_type"].ToString() == "AI")
                is_AI = "T";
        }

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["_id"].ToString() != last_device)
            {
                if (io_id.Split('-').Length != dt1.Rows.Count - 5) 
                {
                    if (io_id != "")
                        io_id += "-";
                    io_id += "none";
                    if (io_type != "")
                        io_type += "-";
                    io_type += "none";
                    if (io_value != "")
                        io_value += "-";
                    io_value += "none";
                    if (detected != "")
                        detected += "-";
                    detected += "none";
                }

                last_device = dt.Rows[i]["_id"].ToString();
                rows[0] = dt2.Rows[row_count]["_name"].ToString();
                rows[1] = dt2.Rows[row_count]["_id"].ToString();
                rows[2] = io_id;
                rows[3] = io_type;
                rows[4] = io_value;
                rows[dt1.Rows.Count +1] = is_AI;
                rows[dt1.Rows.Count] = detected;
                records.Add(rows);
                row_count++;
                last_io_position = 0;
                io_id = "";
                io_type = "";
                io_value = "";
                detected = "";
                rows = new string[dt1.Rows.Count + 2];
            }

            for (var j = last_io_position; j < dt1.Rows.Count; j++)
            {
                if (dt.Rows[i]["_operation"].ToString() == dt1.Rows[j]["_operation"].ToString())
                {
                    rows[j + 5] = dt.Rows[i]["_status"].ToString();
                    if (io_id != "")
                        io_id += "-";
                    io_id += dt.Rows[i]["io_id"].ToString();
                    if (io_type != "")
                        io_type += "-";
                    if (dt.Rows[i]["_type"].ToString() != "")
                        io_type += dt.Rows[i]["_type"].ToString();
                    else
                        io_type += "none";
                    if (io_value != "")
                        io_value += "-";
                    io_value += (dt.Rows[i]["_value"].ToString() == "") ? "none" : dt.Rows[i]["_value"].ToString();
                    if (detected != "")
                        detected += "-";
                    if(dt.Rows[i]["detected"].ToString() == "")
                        detected += "False";
                    else
                        detected += dt.Rows[i]["detected"].ToString();
                    last_io_position = j + 1;
                    break;
                }
                else
                {
                    if (io_id != "")
                        io_id += "-";
                    io_id += "none";
                    if (io_type != "")
                        io_type += "-";
                    io_type += "none";
                    if (io_value != "")
                        io_value += "-";
                    io_value += "none";
                    if (detected != "")
                        detected += "-";
                    detected += "none";
                }
            }

        }

        rows[0] = dt2.Rows[row_count]["_name"].ToString();
        rows[1] = dt2.Rows[row_count]["_id"].ToString();
        rows[2] = io_id;
        rows[3] = io_type;
        rows[4] = io_value;
        rows[dt1.Rows.Count +1] = is_AI;
        rows[dt1.Rows.Count] = detected;
        records.Add(rows);

        var row = new StringBuilder();
        var name_row = new StringBuilder();
        var row_content = new string[dt2.Rows.Count, dt1.Rows.Count - 5];

        for (var i = 0; i < dt2.Rows.Count ; i++)
        {
            name_row.Append("<tr class='point_data_list'>");
            name_row.Append("<td class='name' style='vertical-align:middle;'><div>" + dt2.Rows[i]["_name"].ToString() + "</div></td>");
            name_row.Append("</tr>");
        }

        var row_content_count = 0;
        foreach (var element in records)
        {
            var _type = element[3].Split('-');
            var _value = element[4].Split('-');
            var count = 0;
            var col_content_count = 0;

            row.Append("<tr class='point_data_list'>");

            //row.Append("<td class='name'>" + element[0] + "</td>");
            //name_row.Append("<tr class='point_data_list'>");
            //name_row.Append("<td class='name' style='vertical-align:middle;'><div>" + element[0] + "</div></td>");
            //name_row.Append("</tr>");

            for (var i = 5; i < element.Length - 2 ; i++)
            {
                var status = ((element[i] != null) ? element[i] : "");

                var type_switch = status;

                if (element[i] != null)
                {
                    if (datatype == "Free")
                        _type[i-5] = "none";

                    row_content[row_content_count, col_content_count] = status + ((element[element.Length - 2].Split('-')[i - 5] == "True") ? "_1" : "_0") + "_" + element[2].Split('-')[i - 5];
                    col_content_count += 1;

                    switch (_type[i-5])
                    {
                        case "":
                            type_switch = status;
                            break;
                        case "none":
                            type_switch = status;
                            break;
                        case "readonly":
                            type_switch = status;
                            break;
                        case "on_off":
                            type_switch = "<span id='" + element[2].Split('-')[i - 5] + "' onclick='confirm_on_off_switch(this," + element[2].Split('-')[i - 5] + ")' class='switch mid-margin-right replacement u_on_off " + (((status == "啟動") || (status == "運轉中(C)")) ? "checked" : "") + "' tabindex='0'><span class='switch-on'><span>ON</span></span><span class='switch-off'><span>OFF</span></span><span class='switch-button'></span><input type='checkbox' name='switch' id='switch' class='' value='1' tabindex='-1'></span>";
                            break;
                        case "select":
                            type_switch = "<select id='" + element[2].Split('-')[i - 5] + "' class='select u_select' style='width:95px;' onchange='confirm_selection_change(this," + element[2].Split('-')[i - 5] + ")'>";
                            foreach(var element_2 in _value[count].Split('_')){
                                type_switch += "<option value='" + element_2 + "' " + ((element_2 == status) ? "selected" : "") + ">" + element_2 + "</option>";
                            };
                            type_switch += "</select>";
                            break;
                        case "text":
                            type_switch = "<input id='" + element[2].Split('-')[i - 5] + "' class='input u_input' style='text-align:center;width:95px;' type='text' value='" + status + "' onclick='show_input_modal(this," + element[2].Split('-')[i - 5] + ")' readonly />";
                            break;
                        case "hyperlink":
                            type_switch = "<a class='status_link' href='" + _value[count] + "' target='_blank'>" + status + "</a>";
                            break;
                        case "alarm_reset":
                            type_switch = status + ((status != "正常") ? "<div class='button icon-tick' onclick='DL_alarm_reset(" + element[2].Split('-')[i - 5] + ",this)' style='width:40px;'>復歸</div>" : "");
                            break;
                        case "on&off":
                            type_switch = "<div class='button icon-cycle green-gradient' style='width:8px;height:30px;border-radius:16px;margin:0 10px;line-height:30px;' onclick='switch_on(" + element[2].Split('-')[i - 5] + ")'></div><div class='button icon-forbidden red-gradient' style='width:8px;height:30px;border-radius:16px;margin:0 10px;line-height:30px;' onclick='switch_off(" + element[2].Split('-')[i - 5] + ")'></div>";
                            break;
                    }

                    count++;
                }

                row.Append("<td class='" + "data_" + _type[i - 5] + " _status" + ((element[element.Length - 2].Split('-')[i - 5] == "True") ? " red" : "") + "' ><div>" + type_switch + "</div></td>");
            }
            //row += "<td class='ctrl'><div><a id='" + element[1] + "' class='button icon-pencil' style='width:80px;' onclick='device_view(this)'>顯示方式</a><input type='hidden' value='" + element[2] + "' /><a id='" + element[1] + "' class='button icon-gear' style='width:80px; margin:0 5px;' onclick='device_timeset(this.id)'>排程控制</a></div></td>";
            //row += "<td class='ctrl'><div><a id='" + element[1] + "' class='button icon-eye' style='width:40px; margin:0 5px;' onclick='go_map(this.id)'>地圖</a><a id='" + element[1] + "' class='button icon-outbox' style='width:80px; margin:0 5px;' onclick='confirm_to_control(this)'>送出控制</a></div></td>";

            row_content_count += 1;

            var power_map = (element[element.Length - 1] == "T") ? "<div class='button icon-line-graph' style='width:45px; margin:0 5px;' onclick='go_power_map(" + element[1] + ")'>圖表</div>" : "";
            var map = "<div class='button icon-eye' style='width:45px; margin:0 5px;' onclick='go_map(" + element[1] + ")'>地圖</div>";
            var log = "<div class='button icon-pages' style='width:45px; margin:0 5px;' onclick='go_log(" + element[1] + ")'>紀錄</div>";
            var show = "<div id='" + element[1] + "' class='button icon-pencil' style='width:80px;' onclick='device_view(this)'>顯示方式<input type='hidden' value='" + element[2] + "' /></div>";
            var timeset = "<div id='" + element[1] + "' class='button icon-gear' style='width:80px; margin:0 5px;' onclick='device_timeset(this.id)'>排程控制</div>";

            switch (datatype)
            {
                case "Edit":
                    row.Append("<td class='ctrl'>" + show + timeset + "</td>");
                    break;
                default:
                    row.Append("<td class='ctrl'>" + map + power_map + log + "</td>");
                    break;
            }
            
            row.Append("</tr>");
        }

        return new
        {
            row_content,
            row = row.ToString(),
            row_count = row_count + 1,
            name_row = name_row.ToString()
        };
    }

    /*
    [WebMethod]
    public object Get_IO_by Supcat_Edit(string mcat, string cat)
    {
        var dt1 = db.query("SELECT b.[operationname] FROM [v_DeviceSetting] a left join [s_DeviceIOPoint] b on a.[_id] = b.[deviceID] where a.[Category] = '" + mcat + "' and a.[subCategory] = '" + cat + "' and a.[valid] = 1 and b.[isSend] = 1 group by b.[operationname]");
        var dt2 = db.query("SELECT a.[_id], a.[_name] FROM [v_DeviceSetting] a left join [s_DeviceIOPoint] b on a.[_id] = b.[deviceID] left join [s_DeviceSetting] c on a.[_id] = c.[_id] where a.[Category] = '" + mcat + "' and a.[subCategory] = '" + cat + "' and a.[valid] = 1 " + ((dt1.Rows[0]["operationname"].ToString() == "對講機編號") ? "group by a.[_id], a.[_name], b.[_status] order by b.[_status]" : "group by a.[_id], a.[_name], a.[floor], a.[sub_id], c.[_name] order by c.[_name], a.[floor], a.[sub_id]"));
        var dt = db.query("SELECT a.[_id], a.[_name], b.[operationname], b.[_status], b.[_id] as io_id, c.[_type], c.[_value] FROM [v_DeviceSetting] a left join [v_DeviceIOPoint] b on a.[_id] = b.[deviceID] left join [s_MapIO_mapping] c on replace(c.[DeviceMapInfo_id],'_DeviceList','') = a.[_id] and Convert(nvarchar(50),b.[_id]) = c.[DeviceIOPoint_id] left join [s_DeviceSetting] d on a.[_id] = d.[_id] where a.[Category] = '" + mcat + "' and a.[subCategory] = '" + cat + "' and a.[valid] = 1 and b.[isSend] = 1 order by " + ((dt1.Rows[0]["operationname"].ToString() == "對講機編號") ? "b.[_status]" : "d.[_name], a.[floor], a.[sub_id], b.[operationname]"));

        var records = new List<string[]>();
        dt1.Rows.Add("default_name");
        dt1.Rows.Add("default_id");
        dt1.Rows.Add("default_io_id");
        dt1.Rows.Add("default_type");
        dt1.Rows.Add("default_value");
        var rows = new string[dt1.Rows.Count];
        var last_device = dt.Rows[0]["_id"].ToString();
        var row_count = 0;
        var io_id = "";
        var io_type = "";
        var io_value = "";

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["_id"].ToString() != last_device)
            {
                last_device = dt.Rows[i]["_id"].ToString();
                rows[0] = dt2.Rows[row_count]["_name"].ToString();
                rows[1] = dt2.Rows[row_count]["_id"].ToString();
                rows[2] = io_id;
                rows[3] = io_type;
                rows[4] = io_value;
                records.Add(rows);
                row_count++;
                io_id = "";
                io_type = "";
                io_value = "";
                rows = new string[dt1.Rows.Count];
            }

            for (var j = 0; j < dt1.Rows.Count; j++)
            {
                if (dt.Rows[i]["operationname"].ToString() == dt1.Rows[j]["operationname"].ToString())
                {
                    rows[j + 5] = dt.Rows[i]["_status"].ToString();
                    if (io_id != "")
                        io_id += "-";
                    io_id += dt.Rows[i]["io_id"].ToString();
                    if (io_type != "")
                        io_type += "-";
                    if (dt.Rows[i]["_type"].ToString() != "")
                        io_type += dt.Rows[i]["_type"].ToString();
                    else
                        io_type += "none";
                    if (io_value != "")
                        io_value += "-";
                    io_value += (dt.Rows[i]["_value"].ToString() == "") ? "none" : dt.Rows[i]["_value"].ToString();
                }
            }

        }

        rows[0] = dt2.Rows[row_count]["_name"].ToString();
        rows[1] = dt2.Rows[row_count]["_id"].ToString();
        rows[2] = io_id;
        rows[3] = io_type;
        rows[4] = io_value;
        records.Add(rows);

        return new
        {
            DeviceList = records
        };
    }*/

    [WebMethod]
    public object get_operation_type(string io_id, string d_id, string i_id)
    {
        var dt = db.query("SELECT a.[DeviceIOPoint_id], b.[_operation], b.[IO_type], b.[_status], a.[checked], a.[_type], a.[_value] FROM [s_MapIO_mapping] a inner join [s_DeviceIOPoint] b on a.[DeviceIOPoint_id] = b.[_id] where [DeviceMapInfo_id] = '" + i_id + "' and b.[operationname] is not null and isSend <> 0 order by b.[_sort], b.[_operation]");
        var operation = db.query("SELECT [_id] FROM s_DeviceIOPoint where [deviceID] = '" + d_id + "' and isSend <> 0 order by [_sort], [_operation] ");
        var some_thing_wrong = false;
        var io = io_id.Split('-');
        var records = new List<View>();
        var dt_pool = dt.AsEnumerable().Select(r => r.Field<string>("DeviceIOPoint_id")).ToArray();

        if ( dt.Rows.Count != operation.Rows.Count)
        {
            var _index = 0;
            //db.excute("delete [s_MapIO_mapping] where [DeviceMapInfo_id] = '" + i_id + "' ");
            for (var i = 0; i < operation.Rows.Count; i++) 
            {
                if (dt_pool.Contains(operation.Rows[i]["_id"].ToString()))
                {
                    //var result = db.excute("insert into [s_MapIO_mapping] ( [DeviceMapInfo_id], [DeviceIOPoint_id], [checked], [_type], [_value] ) Values ('" + i_id + "', '" + operation.Rows[i]["_id"].ToString() + "', '" + dt.Rows[_index]["checked"].ToString() + "', '" + dt.Rows[_index]["_type"].ToString() + "', '" + dt.Rows[_index]["_value"].ToString() + "') ");
                    //if (result == 0) some_thing_wrong = true;
                    _index += 1;
                }
                else
                {
                    var result = db.excute("insert into [s_MapIO_mapping] ( [DeviceMapInfo_id], [DeviceIOPoint_id], [checked], [_type], [_value] ) Values ('" + i_id + "', '" + operation.Rows[i]["_id"].ToString() + "', 'true', 'none', 'NULL') ");
                    if (result == 0) some_thing_wrong = true;
                }
                
            }
            
            dt = db.query("SELECT a.[DeviceIOPoint_id], b.[_operation], b.[IO_type], b.[_status], a.[_type], a.[_value] FROM [s_MapIO_mapping] a left join [s_DeviceIOPoint] b on a.[DeviceIOPoint_id] = b.[_id] where [DeviceMapInfo_id] = '" + i_id + "' order by b.[_sort], b.[_operation] ");
        }

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new View();

            row._id = dt.Rows[i]["DeviceIOPoint_id"].ToString();
            row.operationname = dt.Rows[i]["_operation"].ToString();
            row.IO_type = dt.Rows[i]["IO_type"].ToString();
            row._status = dt.Rows[i]["_status"].ToString();
            row._type = dt.Rows[i]["_type"].ToString();
            row._value = dt.Rows[i]["_value"].ToString();

            records.Add(row);
        }

        if (some_thing_wrong)
        {
            return new
            {
                records = JsonConvert.SerializeObject(false)
            };
        }
        else
        {
            return new
            {
                records
            };
        }
        
    }

    [WebMethod(EnableSession = true)]
    public string io_control_led(string io_id, string _status, string _user)
    {
        var result = db.excute("update s_MapIO_mapping set location='"+ _status + "' where DeviceIOPoint_id = '"+ io_id + "' ");

        if ((result == 0))
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod(EnableSession =true)]
    public string io_control(string io_id, string _status, string _user)
    {
        var dt = db.query("select [c_id] from [s_DeviceIOPoint] where [_id] = '" + io_id + "'");
        var c_id = dt.Rows[0]["c_id"];
        var username = Session["LoginName"];

        if(c_id != DBNull.Value)
        {
            io_id = c_id.ToString();
        }

        var result = db.excute("update [s_DeviceIOPoint] set [_status] = '" + _status + "', [utime] = getdate(), [confirm] = '0', isUpdated = 0 where [_id] = '" + io_id + "'");
        var result2 = db.excute("insert into [CenterLog].[dbo].[Log_Device_Control] ( [io_id], [_status], [time], [_user]) Values ( '" + io_id + "', '" + _status + "', getdate(), '" + username + "')");

        if ((result == 0) || (result2 == 0))
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod(EnableSession = true)]
    public string led_control(string io_id, string _status, string _user)
    {
        var username = Session["LoginName"];

        var result = db.excute("update [s_DeviceIOPoint] set [_status] = '" + _status + "', [utime] = getdate() where [_id] = '" + io_id + "'");
        var result2 = db.excute("insert into [CenterLog].[dbo].[Log_Device_Control] ( [io_id], [_status], [time], [_user]) Values ( '" + io_id + "', '" + _status + "', getdate(), '" + username + "')");

        if ((result == 0) || (result2 == 0))
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object get_io_o_for_timeset(string d_id)
    {
        var dt = db.query("SELECT b.[_id], b.[_operation], c.[_type], c.[_value] FROM [v_DeviceSetting] a left join [s_DeviceIOPoint] b on a.[_id] = b.[deviceID] left join [s_MapIO_mapping] c on replace(c.[DeviceMapInfo_id],'_DeviceList','') = a.[_id] and b.[_id] = c.[DeviceIOPoint_id] where b.[IO_type] like '%O' and a.[_id] = '" + d_id + "'");

        var io = new List<View>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new View();

            row._id = dt.Rows[i]["_id"].ToString();
            row.operationname = dt.Rows[i]["_operation"].ToString();
            row._type = dt.Rows[i]["_type"].ToString();
            row._value = dt.Rows[i]["_value"].ToString();

            io.Add(row);
        }

        return new
        {
            io
        };
    }

    [WebMethod]
    public object get_alarm_info(string d_id)
    {
        var dt = db.query("select a.[Category] as 'device_cat', a.[subCategory] as 'device_subcat', a.[_name] as 'device_name', b.[_x] as 'device_x', b.[_y] as 'device_y', c.[Category] as 'map_cat', c.[MapType] as 'map_subcat', c.[MapName] as 'map_name', c.[MapURI] as 'map_url', c.[defaultWidth] as 'map_width', d.[_url] as 'pattern_url', d.[_width] as 'pattern_width', d.[_height] as 'pattern_height', d.[_color] as 'pattern_color', d.[_transparent] as 'pattern_opacity', f.[_ip] as 'cctv_ip' from [v_DeviceSetting] a left join [s_DeviceMapInfo] b on a.[_id] = b.[device_ID] left join [u_def_Map] c on b.[MapId] = c.[_id] left join [u_def_Pattern] d on b.[pattern_id] = d.[_id] left join [s_MapIO_mapping] e on (Convert(nvarchar(50),b.[_id]) + '_camera') = e.[DeviceIOPoint_id] left join [v_DeviceSetting] f on e.[_value] = f.[_id] where a.[_id] = '" + d_id + "' ");

        var alarm = new List<Alarm_Info>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new Alarm_Info();

            row.device_cat = dt.Rows[i]["device_cat"].ToString();
            row.device_subcat = dt.Rows[i]["device_subcat"].ToString();
            row.device_name = dt.Rows[i]["device_name"].ToString();
            row.device_x = dt.Rows[i]["device_x"].ToString();
            row.device_y = dt.Rows[i]["device_y"].ToString();
            row.map_cat = dt.Rows[i]["map_cat"].ToString();
            row.map_subcat = dt.Rows[i]["map_subcat"].ToString();
            row.map_name = dt.Rows[i]["map_name"].ToString();
            row.map_url = dt.Rows[i]["map_url"].ToString();
            row.map_width = dt.Rows[i]["map_width"].ToString();
            row.pattern_url = dt.Rows[i]["pattern_url"].ToString();
            row.pattern_width = dt.Rows[i]["pattern_width"].ToString();
            row.pattern_height = dt.Rows[i]["pattern_height"].ToString();
            row.pattern_color = dt.Rows[i]["pattern_color"].ToString();
            row.pattern_opacity = dt.Rows[i]["pattern_opacity"].ToString();
            row.cctv_ip = dt.Rows[i]["cctv_ip"].ToString();

            alarm.Add(row);
        }

        return new
        {
            alarm
        };
    }

    public class Alarm_Info
    {
        public string device_cat { get; set; }
        public string device_subcat { get; set; }
        public string device_name { get; set; }
        public string cctv_ip { get; set; }
        public string device_x { get; set; }
        public string device_y { get; set; }
        public string map_cat { get; set; }
        public string map_subcat { get; set; }
        public string map_name { get; set; }
        public string map_url { get; set; }
        public string map_width { get; set; }
        public string pattern_url { get; set; }
        public string pattern_width { get; set; }
        public string pattern_height { get; set; }
        public string pattern_color { get; set; }
        public string pattern_opacity { get; set; }
    }

    [WebMethod]
    public object get_camera()
    {
        var dt = db.query("SELECT * FROM [v_DeviceSetting] where [category_type] = 'CCTV' ");

        var camera = new List<View>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new View();

            row._id = dt.Rows[i]["_id"].ToString();
            row._name = dt.Rows[i]["_name"].ToString();

            camera.Add(row);
        }

        return new
        {
            camera
        };
    }

    [WebMethod]
    public object get_mapinfo_camera(string i_id)
    {
        var io_id = i_id + "_camera";
        var dt = db.query("SELECT * FROM [s_MapIO_mapping] where [DeviceMapInfo_id] = '" + i_id + "' and [DeviceIOPoint_id] = '" + io_id + "' ");

        if(dt.Rows.Count == 0)
        {
            db.excute("insert into [s_MapIO_mapping] ([DeviceMapInfo_id],[DeviceIOPoint_id],[_type],[_value]) Values ('" + i_id + "','" + io_id + "','camera','none') ");
            dt = db.query("SELECT * FROM [s_MapIO_mapping] where [DeviceMapInfo_id] = '" + i_id + "' and [DeviceIOPoint_id] = '" + io_id + "' ");
        }

        var camera = dt.Rows[0]["_value"].ToString();

        return new
        {
            camera
        };
    }

    [WebMethod]
    public object save_mapinfo_camera(string camera_id, string io_id)
    {
        var result = db.excute("update [s_MapIO_mapping] set [_value] = '" + camera_id + "' where [DeviceIOPoint_id] = '" + io_id + "' ");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetRatio()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var db = new DBAcess();
        DataTable dt = new DataTable();

        var str = "SELECT [_id], [Category], [subCategory], [_operation], [status_min], [status_max], [value_min], [value_max], [unit], [value_0], [value_1] FROM [s_DeviceIOPoint_Ratio] order by [Category], [subCategory], [_operation]";

        dt = db.query(str);

        var records = new List<Ratio>();

        try
        {
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var rows = new Ratio();
                rows._id = dt.Rows[i]["_id"].ToString();
                rows.Category = dt.Rows[i]["Category"].ToString();
                rows.subCategory = dt.Rows[i]["subCategory"].ToString();
                rows._operation = dt.Rows[i]["_operation"].ToString();
                rows.status_min = dt.Rows[i]["status_min"].ToString();
                rows.status_max = dt.Rows[i]["status_max"].ToString();
                rows.value_min = dt.Rows[i]["value_min"].ToString();
                rows.value_max = dt.Rows[i]["value_max"].ToString();
                rows.unit = dt.Rows[i]["unit"].ToString();
                rows.value_0 = dt.Rows[i]["value_0"].ToString();
                rows.value_1 = dt.Rows[i]["value_1"].ToString();

                records.Add(rows);
            }
        }
        catch
        {

        }


        dt.Dispose();

        /*
        if (!records.Any())
        {
            return string.Empty;
        }
        */

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            sb.Append("\"" + result.Category + "\",");
            sb.Append("\"" + result.subCategory + "\",");
            sb.Append("\"" + result._operation + "\",");
            sb.Append("\"" + result.status_min + "\",");
            sb.Append("\"" + result.status_max + "\",");
            sb.Append("\"" + result.value_min + "\",");
            sb.Append("\"" + result.value_max + "\",");
            sb.Append("\"" + result.unit + "\",");
            sb.Append("\"" + result.value_0 + "\",");
            sb.Append("\"" + result.value_1 + "\",");

            sb.Append("\"" + "<div class='button icon-pencil ' onclick='edit_ratio(" + result._id + ")' style='width:50px;'>編輯</div><div class='button icon-trash' onclick='del_confirm(" + result._id + ")' style='width:50px;'>刪除</div>" + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");
        return sb.ToString();
    }

    public class Ratio
    {
        public string _id { get; set; }
        public string Category { get; set; }
        public string subCategory { get; set; }
        public string _operation { get; set; }
        public string status_min { get; set; }
        public string status_max { get; set; }
        public string value_min { get; set; }
        public string value_max { get; set; }
        public string unit { get; set; }
        public string value_0 { get; set; }
        public string value_1 { get; set; }
    }

    [WebMethod]
    public object get_single_ratio(string _id)
    {
        var dt = db.query("SELECT [_id], [Category], [subCategory], [_operation], [status_min], [status_max], [value_min], [value_max], [unit], [value_0], [value_1] FROM [s_DeviceIOPoint_Ratio] where [_id] = '" + _id + "'");

        var rows = new Ratio();
        rows._id = dt.Rows[0]["_id"].ToString();
        rows.Category = dt.Rows[0]["Category"].ToString();
        rows.subCategory = dt.Rows[0]["subCategory"].ToString();
        rows._operation = dt.Rows[0]["_operation"].ToString();
        rows.status_min = dt.Rows[0]["status_min"].ToString();
        rows.status_max = dt.Rows[0]["status_max"].ToString();
        rows.value_min = dt.Rows[0]["value_min"].ToString();
        rows.value_max = dt.Rows[0]["value_max"].ToString();
        rows.unit = dt.Rows[0]["unit"].ToString();
        rows.value_0 = dt.Rows[0]["value_0"].ToString();
        rows.value_1 = dt.Rows[0]["value_1"].ToString();

        return new { rows };
    }

    [WebMethod]
    public object update_ratio(string _id, string status_min, string status_max, string value_min, string value_max, string unit, string value_0, string value_1)
    {
        var result = db.excute(" update [s_DeviceIOPoint_Ratio] set [status_min] = '" + status_min + "', [status_max] = '" + status_max + "', [value_min] = '" + value_min + "', [value_max] = '" + value_max + "', [unit] = '" + unit + "', [value_0] = '" + value_0 + "', [value_1] = '" + value_1 + "' where [_id] = '" + _id + "' ");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object delete_ratio(string _id)
    {
        var result = db.excute(" delete [s_DeviceIOPoint_Ratio] where [_id] = '" + _id + "' ");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object create_ratio(string Category, string subCategory, string _operation, string status_min, string status_max, string value_min, string value_max, string unit, string value_0, string value_1)
    {
        var result = db.excute(" insert into [s_DeviceIOPoint_Ratio] ( [Category], [subCategory], [_operation], [status_min], [status_max], [value_min], [value_max], [unit], [value_0], [value_1] ) values ('" + Category + "', '" + subCategory + "', '" + _operation + "', '" + status_min + "', '" + status_max + "', '" + value_min + "', '" + value_max + "', '" + unit + "', '" + value_0 + "', '" + value_1 + "' ) ");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetCombine()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var db = new DBAcess();
        DataTable dt = new DataTable();

        var str = "SELECT [_id], [PLC], [Modbus], [Modbus_Group], [Carry], [Value] FROM [PLC_Value_combin] order by [PLC], [Modbus]";

        dt = db.query(str);

        var records = new List<Combine>();

        try
        {
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var rows = new Combine();
                rows._id = dt.Rows[i]["_id"].ToString();
                rows.PLC = dt.Rows[i]["PLC"].ToString();
                rows.Modbus = dt.Rows[i]["Modbus"].ToString();
                rows.Modbus_Group = dt.Rows[i]["Modbus_Group"].ToString();
                rows.Carry = dt.Rows[i]["Carry"].ToString();
                rows.Value = dt.Rows[i]["Value"].ToString();

                records.Add(rows);
            }
        }
        catch
        {

        }


        dt.Dispose();

        /*
        if (!records.Any())
        {
            return string.Empty;
        }
        */

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            sb.Append("\"" + result.PLC + "\",");
            sb.Append("\"" + result.Modbus + "\",");
            sb.Append("\"" + result.Modbus_Group + "\",");
            sb.Append("\"" + result.Carry + "\",");
            sb.Append("\"" + result.Value + "\",");

            sb.Append("\"" + "<div class='button icon-pencil ' onclick='edit_combine(" + result._id + ")' style='width:50px;'>編輯</div><div class='button icon-trash' onclick='del_confirm(" + result._id + ")' style='width:50px;'>刪除</div>" + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");
        return sb.ToString();
    }

    public class Combine
    {
        public string _id { get; set; }
        public string PLC { get; set; }
        public string Modbus { get; set; }
        public string Modbus_Group { get; set; }
        public string Carry { get; set; }
        public string Value { get; set; }

    }

    [WebMethod]
    public object get_single_combine(string _id)
    {
        var dt = db.query("SELECT [_id], [PLC], [Modbus], [Modbus_Group], [Carry], [Value] FROM [PLC_Value_combin] where [_id] = '" + _id + "'");

        var rows = new Combine();
        rows._id = dt.Rows[0]["_id"].ToString();
        rows.PLC = dt.Rows[0]["PLC"].ToString();
        rows.Modbus = dt.Rows[0]["Modbus"].ToString();
        rows.Modbus_Group = dt.Rows[0]["Modbus_Group"].ToString();
        rows.Carry = dt.Rows[0]["Carry"].ToString();
        rows.Value = dt.Rows[0]["Value"].ToString();

        return new { rows };
    }

    [WebMethod]
    public object update_combine(string _id, string PLC, string Modbus, string Modbus_Group, string Carry, string Value)
    {
        var result = db.excute(" update [PLC_Value_combin] set [PLC] = '" + PLC + "', [Modbus] = '" + Modbus + "', [Modbus_Group] = '" + Modbus_Group + "', [Carry] = '" + Carry + "', [Value] = '" + Value + "' where [_id] = '" + _id + "' ");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object delete_combine(string _id)
    {
        var result = db.excute(" delete [PLC_Value_combin] where [_id] = '" + _id + "' ");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object create_combine( string PLC, string Modbus, string Modbus_Group, string Carry, string Value)
    {
        var result = db.excute(" insert into [PLC_Value_combin] ( [PLC], [Modbus], [Modbus_Group], [Carry], [Value] ) values ('" + PLC + "', '" + Modbus + "', '" + Modbus_Group + "', '" + Carry + "', '" + Value + "' ) ");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object get_map_by_device_id(string d_id)
    {
        var dt = db.query("SELECT b.[Category], b.[MapType], b.[MapName], a._priority FROM [s_DeviceMapInfo] a inner join [u_def_Map] b ON a.[MapId] = b.[_id] inner join (select MIN([_priority]) as _priority from [s_DeviceMapInfo] WHERE [device_ID] = '" + d_id + "' group by device_ID) c on a._priority = c._priority WHERE a.[device_ID] = '" + d_id + "'");

        var rows = new View();

        if(dt.Rows.Count > 0)
        {
            rows.Category = dt.Rows[0]["Category"].ToString();
            rows.MapType = dt.Rows[0]["MapType"].ToString();
            rows.MapName = dt.Rows[0]["MapName"].ToString();

            return new { 
                rows,
                failed = "false"
            };
        }
        else
        {
            return new {
                failed = "true"
            };
        }

        
    }

    [WebMethod]
    public object send_control(string io_id, string value)
    {
        var io = io_id.Split(',');
        var values = value.Split(',');
        var result = 0;

        for (var i = 0; i < io.Length; i++)
        {
            result += db.excute("update [s_DeviceIOPoint] set [_status] = '" + values[i] + "', confirm = 0 where [_id] = '" + io[i] + "'");
        }

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object io_ratio(string io_id, string _status)
    {
        var result = 0.0;
        var error_result = "";

        var dt = db.query("SELECT c.[status_min],c.[status_max],c.[value_min],c.[value_max],c.[unit],c.[value_0],c.[value_1] FROM [s_DeviceIOPoint] a left join [v_DeviceSetting] b on a.[deviceID] = b.[_id] inner join [s_DeviceIOPoint_Ratio] c on c.[Category] = b.[Category] and c.[subCategory] = b.[subCategory] and c.[_operation] = a.[_operation] where a._id = '" + io_id + "'");

        var rows = new Ratio();
        rows.status_min = dt.Rows[0]["status_min"].ToString();
        rows.status_max = dt.Rows[0]["status_max"].ToString();
        rows.value_min = dt.Rows[0]["value_min"].ToString();
        rows.value_max = dt.Rows[0]["value_max"].ToString();
        rows.unit = dt.Rows[0]["unit"].ToString();
        rows.value_0 = dt.Rows[0]["value_0"].ToString();
        rows.value_1 = dt.Rows[0]["value_1"].ToString();

        if (rows.unit != "ON/OFF")
        {
            if (!_status.Contains(rows.unit))
            {
                error_result = "請輸入正確的值";
                return new { error_result };
            }

            _status = _status.Replace(rows.unit, "");
            if (_status == "")
            {
                error_result = "請輸入範圍內的值";
                return new { error_result };
            }

            if (Convert.ToDouble(rows.value_max) > Convert.ToDouble(rows.value_min))
            {
                if ((Convert.ToDouble(_status) > Convert.ToDouble(rows.value_max)) || (Convert.ToDouble(_status) < Convert.ToDouble(rows.value_min)))
                {
                    error_result = "請輸入範圍內的值";
                    return new { error_result };
                }

                result = Math.Round((Convert.ToDouble(_status) / (Convert.ToDouble(rows.value_max) - Convert.ToDouble(rows.value_min))) * (Convert.ToDouble(rows.status_max) - Convert.ToDouble(rows.status_min)) + Convert.ToDouble(rows.status_min));
            }
            else
            {
                if ((Convert.ToDouble(_status) < Convert.ToDouble(rows.value_max)) || (Convert.ToDouble(_status) > Convert.ToDouble(rows.value_min)))
                {
                    error_result = "請輸入範圍內的值";
                    return new { error_result };
                }

                result = Math.Round((Convert.ToDouble(_status) / (Convert.ToDouble(rows.value_max) - Convert.ToDouble(rows.value_min))) * (Convert.ToDouble(rows.status_max) - Convert.ToDouble(rows.status_min)) + Convert.ToDouble(rows.status_max));
            }
            
        }
        else
        {
            if (_status == rows.value_0)
            {
                result = 0;
            }
            else if (_status == rows.value_1)
            {
                result = 65535;
            }
            else
            {
                error_result = "請輸入正確的值";
            }
        }

        if(error_result!="")
        {
            return new { error_result };
        }
        else
        {
            return new { result };
        }
        
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetLogs_Alarm()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var _name = HttpContext.Current.Request.Params["_name"].ToString(CultureInfo.CurrentCulture);
        var _operation = HttpContext.Current.Request.Params["_operation"].ToString(CultureInfo.CurrentCulture);
        var datetime = HttpContext.Current.Request.Params["datetime"].ToString(CultureInfo.CurrentCulture);
        var old_value = HttpContext.Current.Request.Params["old_value"].ToString(CultureInfo.CurrentCulture);
        var new_value = HttpContext.Current.Request.Params["new_value"].ToString(CultureInfo.CurrentCulture);

        var db = new DBAcess();
        DataTable dt = new DataTable();

        var str = "SELECT [_name], [_operation], convert(varchar,[datetime],120) as datetime, [old_value], [new_value] FROM [v_Log_DeviceIOPoint_Alarm] where [_name] like '%" + _name + "%' and [_operation] like '%" + _operation + "%' and convert(varchar,[datetime],120) like '%" + datetime + "%' and [old_value] like '%" + old_value + "%' and [new_value] like '%" + new_value + "%' ";

        dt = db.query(str);

        var records = new List<IO_Log>();

        try
        {
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var rows = new IO_Log();
                rows._name = dt.Rows[i]["_name"].ToString();
                rows._operation = dt.Rows[i]["_operation"].ToString();
                rows.datetime = Convert.ToDateTime(dt.Rows[i]["datetime"]).ToString("yyyy-MM-dd HH:mm:ss");
                rows.old_value = dt.Rows[i]["old_value"].ToString();
                rows.new_value = dt.Rows[i]["new_value"].ToString();

                records.Add(rows);
            }
        }
        catch
        {

        }


        dt.Dispose();

        /*
        if (!records.Any())
        {
            return string.Empty;
        }
        */

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            sb.Append("\"" + result._name + "\",");
            sb.Append("\"" + result._operation + "\",");
            sb.Append("\"" + result.datetime + "\",");
            sb.Append("\"" + result.old_value + "\",");
            sb.Append("\"" + result.new_value + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");
        return sb.ToString();
    }

    public class IO_Log
    {
        public string _name { get; set; }
        public string _operation { get; set; }
        public string datetime { get; set; }
        public string old_value { get; set; }
        public string new_value { get; set; }
        public string _value { get; set; }
        public string _status { get; set; }
        public string condition { get; set; }
        public string alarm_info { get; set; }
        public string detect_info { get; set; }
        public string _user { get; set; }
        public string mail { get; set; }
        public string t_id { get; set; }
        public string t_type { get; set; }

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetLogs_History()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var cat = HttpContext.Current.Request.Params["cat"].ToString(CultureInfo.CurrentCulture);
        var scat = HttpContext.Current.Request.Params["scat"].ToString(CultureInfo.CurrentCulture);
        var device = HttpContext.Current.Request.Params["device"].ToString(CultureInfo.CurrentCulture);
        var io = HttpContext.Current.Request.Params["io"].ToString(CultureInfo.CurrentCulture);
        var s_d = HttpContext.Current.Request.Params["s_d"].ToString(CultureInfo.CurrentCulture);
        var e_d = HttpContext.Current.Request.Params["e_d"].ToString(CultureInfo.CurrentCulture);
        var _value = HttpContext.Current.Request.Params["_value"].ToString(CultureInfo.CurrentCulture);
        var _count = HttpContext.Current.Request.Params["_count"].ToString(CultureInfo.CurrentCulture);
        var t = HttpContext.Current.Request.Params["t"].ToString(CultureInfo.CurrentCulture);
        var _condition = HttpContext.Current.Request.Params["_condition"].ToString(CultureInfo.CurrentCulture);
        var alarm_type = HttpContext.Current.Request.Params["alarm_type"].ToString(CultureInfo.CurrentCulture);
        var _status = HttpContext.Current.Request.Params["_status"].ToString(CultureInfo.CurrentCulture);
        var action = HttpContext.Current.Request.Params["action"].ToString(CultureInfo.CurrentCulture);
        var _user = HttpContext.Current.Request.Params["_user"].ToString(CultureInfo.CurrentCulture);
        var _receiver = HttpContext.Current.Request.Params["_receiver"].ToString(CultureInfo.CurrentCulture); 

        var db = new DBAcess();
        DataTable dt = new DataTable();

        var s_y = Convert.ToDateTime(s_d).Year;
        var s_m = Convert.ToDateTime(s_d).Month;
        var e_y = Convert.ToDateTime(e_d).Year;
        var e_m = Convert.ToDateTime(e_d).Month;
        var n_y = DateTime.Now.Year;
        var n_m = DateTime.Now.Month;

        var log_table = "";
        var str = "";
        var union = "";
        switch (t)
        {
            case "History":
                var category_type = db.query("select category_type from TSMCDB.dbo.s_devicesetting where Category = '" + cat + "' group by category_type ").Rows[0]["category_type"].ToString();

                switch (category_type)
                {
                    case "空調系統":
                        union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Air");
                        break;
                    case "計費系統":
                        union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Toll");
                        break;
                    case "消防系統":
                        union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Fire");
                        break;
                    default:
                        union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Other");
                        break;
                }
                
                log_table = "SELECT a._id, a.datetime, a._value, a._status, a.io_id FROM ( " + union + " ) AS a ";
                str = "SELECT TOP " + _count + " c.[_name], b.[_operation], convert(varchar,a.[datetime],120) as datetime, a.[_status] FROM ( " + log_table + " ) a inner join [TSMCDB].[dbo].[s_DeviceIOPoint] b on a.io_id = b._id and b._operation like '" + io + "' and b.isSend = 1 inner join [TSMCDB].[dbo].[v_DeviceSetting] c on c._id = b.deviceID and c.valid = 1 and c._name like '" + device + "' and c.subcategory = '" + scat + "' and c.category like '" + cat + "' where a.[datetime] >= convert(datetime,'" + s_d + "') and a.[datetime] <= convert(datetime,'" + e_d + "') and a.[_status] like '%" + _value + "%' order by datetime desc";
                dt = db.query(str);
                break;
            case "Alarm":

                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_Alarm");

                log_table = "SELECT a._id, a._status, a.condition, a.datetime, a.alarm_info, a.io_id FROM ( " + union + " ) AS a ";
                str = "SELECT TOP " + _count + " c.[_name], b.[_operation], convert(varchar,a.[datetime],120) as datetime, a.[_status], a.condition, a.alarm_info FROM ( " + log_table + " ) a inner join [TSMCDB].[dbo].[s_DeviceIOPoint] b on a.io_id = b._id and b._operation like '" + io + "' and b.isSend = 1 inner join [TSMCDB].[dbo].[v_DeviceSetting] c on c._id = b.deviceID and c.valid = 1 and c._name like '" + device + "' and c.subcategory like '" + scat + "' and c.category like '" + cat + "' where a.[datetime] >= convert(datetime,'" + s_d + "') and a.[datetime] <= convert(datetime,'" + e_d + "') and a.[_status] like '%" + _value + "%' and a.[condition] like '%" + _condition + "%' and a.[alarm_info] like '%" + alarm_type + "%' order by datetime desc";
                dt = db.query(str);
                break;
            case "Detect":

                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_Detect");

                log_table = "SELECT a._id, a._status, a._value, a.detect_time, a.detect_info, a.io_id FROM ( " + union + " ) AS a ";
                str = "SELECT TOP " + _count + " c.[_name], b.[_operation], convert(varchar,a.[detect_time],120) as datetime, a.[_status], a.[_value], a.[detect_info] FROM ( " + log_table + " ) a inner join [TSMCDB].[dbo].[s_DeviceIOPoint] b on a.io_id = b._id and b._operation like '" + io + "' inner join [TSMCDB].[dbo].[v_DeviceSetting] c on c._id = b.deviceID and c.valid = 1 and c._name like '" + device + "' and c.subcategory like '" + scat + "' and c.category like '" + cat + "' where a.[detect_time] >= convert(datetime,'" + s_d + "') and a.[detect_time] <= convert(datetime,'" + e_d + "') and a.[_status] like '%" + _value + "%' and a.[_value] like '%" + _status + "%' and a.[detect_info] like '%" + action + "%' order by datetime desc";
                dt = db.query(str);
                break;

            case "Control":

                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_Device_Control");

                db.excute("insert into TSMCDB.dbo.s_DeviceIOPoint_Short ([io_id], [_status], [datetime]) (select io_id, _status, time from (" + union + ") x where [time] >= convert(datetime,'" + s_d + "') and [time] <= convert(datetime,'" + e_d + "') )");
                log_table = "SELECT a._id, a.time, a._user, a.io_id FROM ( " + union + " ) AS a ";
                str = "SELECT TOP " + _count + " c.[_name], b.[_operation], convert(varchar,a.[time],120) as datetime, d.[_status], a.[_user] FROM ( " + log_table + " ) a inner join [TSMCDB].[dbo].[s_DeviceIOPoint] b on a.io_id = b._id and b._operation like '" + io + "' inner join [TSMCDB].[dbo].[v_DeviceSetting] c on c._id = b.deviceID and c.valid = 1 and c._name like '" + device + "' and c.subcategory like '" + scat + "' and c.category like '" + cat + "' LEFT JOIN TSMCDB.dbo.v_DeviceIOPoint_Calculate_V2 d on a.io_id = d.io_id and a.time = d.datetime where a.[time] >= convert(datetime,'" + s_d + "') and a.[time] <= convert(datetime,'" + e_d + "') and d.[_status] like '%" + _value + "%' and a.[_user] like '%" + _user + "%' order by datetime desc";
                dt = db.query(str);
                db.excute("delete a from TSMCDB.dbo.s_DeviceIOPoint_Short a inner join (select io_id, _status, time from (" + union + ") x ) b on a.io_id = b.io_id and a.datetime = b.time");
                break;

            case "Mail_Msg":

                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_Alarm");

                db.excute("insert into TSMCDB.dbo.s_DeviceIOPoint_Short ([io_id], [_status], [datetime]) (select io_id, _status, datetime from (" + union + ") x where [datetime] >= convert(datetime,'" + s_d + "') and [datetime] <= convert(datetime,'" + e_d + "') )");
                log_table = "SELECT a._id, a.datetime , a.mail, b.io_id, b.datetime as alarm_time FROM CenterLog.dbo.Log_Mail a inner join ( " + union + " ) b on a.log_id = b._id ";
                str = "SELECT TOP " + _count + " c.[_name], b.[_operation], convert(varchar,a.[datetime],120) as datetime, d.[_status], a.[mail] FROM ( " + log_table + " ) a inner join [TSMCDB].[dbo].[s_DeviceIOPoint] b on a.io_id = b._id and b._operation like '" + io + "' inner join [TSMCDB].[dbo].[v_DeviceSetting] c on c._id = b.deviceID and c.valid = 1 and c._name like '" + device + "' and c.subcategory like '" + scat + "' and c.category like '" + cat + "' LEFT JOIN TSMCDB.dbo.v_DeviceIOPoint_Calculate_V2 d on a.io_id = d.io_id and a.alarm_time = d.datetime where a.[datetime] >= convert(datetime,'" + s_d + "') and a.[datetime] <= convert(datetime,'" + e_d + "') and d.[_status] like '%" + _value + "%' and a.[mail] like '%" + _receiver + "%' order by datetime desc";
                dt = db.query(str);
                db.excute("delete a from TSMCDB.dbo.s_DeviceIOPoint_Short a inner join (select io_id, _status, datetime from (" + union + ") x ) b on a.io_id = b.io_id and a.datetime = b.datetime");
                break;

            case "TimeSetting":

                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_TimeSetting");

                log_table = "SELECT a._id, a._status, a.datetime, a.timeset_id, a.[table], a.io_id FROM ( " + union + " ) AS a ";
                str = "SELECT TOP " + _count + " c.[_name], b.[_operation], convert(varchar,a.[datetime],120) as datetime, a.[_status], a.[timeset_id], a.[table] FROM ( " + log_table + " ) a inner join [TSMCDB].[dbo].[s_DeviceIOPoint] b on a.io_id = b._id and b._operation like '" + io + "' inner join [TSMCDB].[dbo].[v_DeviceSetting] c on c._id = b.deviceID and c.valid = 1 and c._name like '" + device + "' and c.subcategory like '" + scat + "' and c.category like '" + cat + "' where a.[datetime] >= convert(datetime,'" + s_d + "') and a.[datetime] <= convert(datetime,'" + e_d + "') and a.[_status] like '%" + _value + "%' order by datetime desc";
                dt = db.query(str);
                break;

        }

        var records = new List<IO_Log>();

        switch (t)
        {
            case "History":
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var rows = new IO_Log();
                    rows._name = dt.Rows[i]["_name"].ToString();
                    rows._operation = dt.Rows[i]["_operation"].ToString();
                    rows.datetime = Convert.ToDateTime(dt.Rows[i]["datetime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    rows._value = dt.Rows[i]["_status"].ToString();

                    records.Add(rows);
                }
                break;
            case "Alarm":
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var rows = new IO_Log();
                    rows._name = dt.Rows[i]["_name"].ToString();
                    rows._operation = dt.Rows[i]["_operation"].ToString();
                    rows.datetime = Convert.ToDateTime(dt.Rows[i]["datetime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    rows._status = dt.Rows[i]["_status"].ToString();
                    rows.condition = dt.Rows[i]["condition"].ToString();
                    rows.alarm_info = dt.Rows[i]["alarm_info"].ToString();

                    records.Add(rows);
                }
                break;
            case "Detect":
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var rows = new IO_Log();
                    rows._name = dt.Rows[i]["_name"].ToString();
                    rows._operation = dt.Rows[i]["_operation"].ToString();
                    rows.datetime = Convert.ToDateTime(dt.Rows[i]["datetime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    rows._status = dt.Rows[i]["_status"].ToString();
                    rows._value = dt.Rows[i]["_value"].ToString();
                    rows.detect_info = dt.Rows[i]["detect_info"].ToString();

                    records.Add(rows);
                }
                break;
            case "Control":
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var rows = new IO_Log();
                    rows._name = dt.Rows[i]["_name"].ToString();
                    rows._operation = dt.Rows[i]["_operation"].ToString();
                    rows.datetime = Convert.ToDateTime(dt.Rows[i]["datetime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    rows._status = dt.Rows[i]["_status"].ToString();
                    rows._user = dt.Rows[i]["_user"].ToString();

                    records.Add(rows);
                }
                break;
            case "Mail_Msg":
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var rows = new IO_Log();
                    rows._name = dt.Rows[i]["_name"].ToString();
                    rows._operation = dt.Rows[i]["_operation"].ToString();
                    rows.datetime = Convert.ToDateTime(dt.Rows[i]["datetime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    rows._status = dt.Rows[i]["_status"].ToString();
                    rows.mail = dt.Rows[i]["mail"].ToString();

                    records.Add(rows);
                }
                break;
            case "TimeSetting":
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var rows = new IO_Log();
                    rows._name = dt.Rows[i]["_name"].ToString();
                    rows._operation = dt.Rows[i]["_operation"].ToString();
                    rows.datetime = Convert.ToDateTime(dt.Rows[i]["datetime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    rows._status = dt.Rows[i]["_status"].ToString();
                    rows.t_id = dt.Rows[i]["timeset_id"].ToString();
                    rows.t_type = dt.Rows[i]["table"].ToString();

                    records.Add(rows);
                }
                break;
        }
        


        dt.Dispose();

        /*
        if (!records.Any())
        {
            return string.Empty;
        }
        */

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            switch (t)
            {
                case "History":
                    sb.Append("\"" + result._name + "\",");
                    sb.Append("\"" + result._operation + "\",");
                    sb.Append("\"" + result.datetime + "\",");
                    sb.Append("\"" + result._value + "\"");
                    break;
                case "Alarm":
                    sb.Append("\"" + result._name + "\",");
                    sb.Append("\"" + result._operation + "\",");
                    sb.Append("\"" + result.datetime + "\",");
                    sb.Append("\"" + result._status + "\",");
                    sb.Append("\"" + result.condition + "\",");
                    sb.Append("\"" + result.alarm_info + "\"");
                    break;
                case "Detect":
                    sb.Append("\"" + result._name + "\",");
                    sb.Append("\"" + result._operation + "\",");
                    sb.Append("\"" + result.datetime + "\",");
                    sb.Append("\"" + result._status + "\",");
                    sb.Append("\"" + result._value + "\",");
                    sb.Append("\"" + result.detect_info + "\"");
                    break;
                case "Control":
                    sb.Append("\"" + result._name + "\",");
                    sb.Append("\"" + result._operation + "\",");
                    sb.Append("\"" + result.datetime + "\",");
                    sb.Append("\"" + result._status + "\",");
                    sb.Append("\"" + result._user + "\"");
                    break;
                case "Mail_Msg":
                    sb.Append("\"" + result._name + "\",");
                    sb.Append("\"" + result._operation + "\",");
                    sb.Append("\"" + result.datetime + "\",");
                    sb.Append("\"" + result._status + "\",");
                    sb.Append("\"" + result.mail + "\"");
                    break;
                case "TimeSetting":
                    sb.Append("\"" + result._name + "\",");
                    sb.Append("\"" + result._operation + "\",");
                    sb.Append("\"" + result.datetime + "\",");
                    sb.Append("\"" + result._status + "\",");
                    sb.Append("\"" + "<a href='http://localhost/Center/System_TimeSetting/TimeSetting.aspx?t_id=" + result.t_id + "&t_type=" + ((result.t_type == "") ? "Device" : result.t_type) + "' class='button icon-eye' style='width:50px;' target='_blank'>排程</a>" + "\"");
                    break;
            }
            

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            switch (t)
            {
                case "History":
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\"");
                    break;
                case "Alarm":
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\"");
                    break;
                case "Detect":
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\"");
                    break;
                case "Control":
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\"");
                    break;
                case "Mail_Msg":
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\"");
                    break;
                case "TimeSetting":
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\",");
                    sb.Append("\"" + "\"");
                    break;
            }
            

            sb.Append("]");
        }

        sb.Append("]}");
        return sb.ToString();
    }

    [WebMethod(EnableSession = true)]
    public object save_as_excel_for_log( string cat, string scat, string device, string io, string s_d, string e_d, string _value, string _count, string t, string _condition, string alarm_type, string _status, string action, string _user, string _receiver )
    {
        var db = new DBAcess();
        DataTable dt = new DataTable();

        var s_y = Convert.ToDateTime(s_d).Year;
        var s_m = Convert.ToDateTime(s_d).Month;
        var e_y = Convert.ToDateTime(e_d).Year;
        var e_m = Convert.ToDateTime(e_d).Month;
        var n_y = DateTime.Now.Year;
        var n_m = DateTime.Now.Month;

        var log_table = "";
        var str = "";
        var union = "";

        switch (t)
        {
            case "History":
                var category_type = db.query("select category_type from TSMCDB.dbo.s_devicesetting where Category = '" + cat + "' group by category_type ").Rows[0]["category_type"].ToString();

                switch (category_type)
                {
                    case "空調系統":
                        union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Air");
                        break;
                    case "計費系統":
                        union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Toll");
                        break;
                    case "消防系統":
                        union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Fire");
                        break;
                    default:
                        union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Other");
                        break;
                }

                log_table = "SELECT a._id, a.datetime, a._value, a._status, a.io_id FROM ( " + union + " ) AS a ";
                str = "SELECT TOP " + _count + " c.[_name], b.[_operation], convert(varchar,a.[datetime],120) as datetime, a.[_status] FROM ( " + log_table + " ) a inner join [TSMCDB].[dbo].[s_DeviceIOPoint] b on a.io_id = b._id and b._operation like '" + io + "' and b.isSend = 1 inner join [TSMCDB].[dbo].[v_DeviceSetting] c on c._id = b.deviceID and c.valid = 1 and c._name like '" + device + "' and c.subcategory = '" + scat + "' and c.category like '" + cat + "' where a.[datetime] >= convert(datetime,'" + s_d + "') and a.[datetime] <= convert(datetime,'" + e_d + "') and a.[_status] like '%" + _value + "%' order by datetime desc";
                dt = db.query(str);
                break;
            case "Alarm":

                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_Alarm");

                log_table = "SELECT a._id, a._status, a.condition, a.datetime, a.alarm_info, a.io_id FROM ( " + union + " ) AS a ";
                str = "SELECT TOP " + _count + " c.[_name], b.[_operation], convert(varchar,a.[datetime],120) as datetime, a.[_status], a.condition, a.alarm_info FROM ( " + log_table + " ) a inner join [TSMCDB].[dbo].[s_DeviceIOPoint] b on a.io_id = b._id and b._operation like '" + io + "' and b.isSend = 1 inner join [TSMCDB].[dbo].[v_DeviceSetting] c on c._id = b.deviceID and c.valid = 1 and c._name like '" + device + "' and c.subcategory like '" + scat + "' and c.category like '" + cat + "' where a.[datetime] >= convert(datetime,'" + s_d + "') and a.[datetime] <= convert(datetime,'" + e_d + "') and a.[_status] like '%" + _value + "%' and a.[condition] like '%" + _condition + "%' and a.[alarm_info] like '%" + alarm_type + "%' order by datetime desc";
                dt = db.query(str);
                break;
            case "Detect":

                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_Detect");

                log_table = "SELECT a._id, a._status, a._value, a.detect_time, a.detect_info, a.io_id FROM ( " + union + " ) AS a ";
                str = "SELECT TOP " + _count + " c.[_name], b.[_operation], convert(varchar,a.[detect_time],120) as datetime, a.[_status], a.[_value], a.[detect_info] FROM ( " + log_table + " ) a inner join [TSMCDB].[dbo].[s_DeviceIOPoint] b on a.io_id = b._id and b._operation like '" + io + "' inner join [TSMCDB].[dbo].[v_DeviceSetting] c on c._id = b.deviceID and c.valid = 1 and c._name like '" + device + "' and c.subcategory like '" + scat + "' and c.category like '" + cat + "' where a.[detect_time] >= convert(datetime,'" + s_d + "') and a.[detect_time] <= convert(datetime,'" + e_d + "') and a.[_status] like '%" + _value + "%' and a.[_value] like '%" + _status + "%' and a.[detect_info] like '%" + action + "%' order by datetime desc";
                dt = db.query(str);
                break;

            case "Control":

                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_Device_Control");

                db.excute("insert into TSMCDB.dbo.s_DeviceIOPoint_Short ([io_id], [_status], [datetime]) (select io_id, _status, time from (" + union + ") x where [time] >= convert(datetime,'" + s_d + "') and [time] <= convert(datetime,'" + e_d + "') )");
                log_table = "SELECT a._id, a.time, a._user, a.io_id FROM ( " + union + " ) AS a ";
                str = "SELECT TOP " + _count + " c.[_name], b.[_operation], convert(varchar,a.[time],120) as datetime, d.[_status], a.[_user] FROM ( " + log_table + " ) a inner join [TSMCDB].[dbo].[s_DeviceIOPoint] b on a.io_id = b._id and b._operation like '" + io + "' inner join [TSMCDB].[dbo].[v_DeviceSetting] c on c._id = b.deviceID and c.valid = 1 and c._name like '" + device + "' and c.subcategory like '" + scat + "' and c.category like '" + cat + "' LEFT JOIN TSMCDB.dbo.v_DeviceIOPoint_Calculate_V2 d on a.io_id = d.io_id and a.time = d.datetime where a.[time] >= convert(datetime,'" + s_d + "') and a.[time] <= convert(datetime,'" + e_d + "') and d.[_status] like '%" + _value + "%' and a.[_user] like '%" + _user + "%' order by datetime desc";
                dt = db.query(str);
                db.excute("delete a from TSMCDB.dbo.s_DeviceIOPoint_Short a inner join (select io_id, _status, time from (" + union + ") x ) b on a.io_id = b.io_id and a.datetime = b.time");
                break;

            case "Mail_Msg":

                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_Alarm");

                db.excute("insert into TSMCDB.dbo.s_DeviceIOPoint_Short ([io_id], [_status], [datetime]) (select io_id, _status, datetime from (" + union + ") x where [datetime] >= convert(datetime,'" + s_d + "') and [datetime] <= convert(datetime,'" + e_d + "') )");
                log_table = "SELECT a._id, a.datetime , a.mail, b.io_id, b.datetime as alarm_time FROM CenterLog.dbo.Log_Mail a inner join ( " + union + " ) b on a.log_id = b._id ";
                str = "SELECT TOP " + _count + " c.[_name], b.[_operation], convert(varchar,a.[datetime],120) as datetime, d.[_status], a.[mail] FROM ( " + log_table + " ) a inner join [TSMCDB].[dbo].[s_DeviceIOPoint] b on a.io_id = b._id and b._operation like '" + io + "' inner join [TSMCDB].[dbo].[v_DeviceSetting] c on c._id = b.deviceID and c.valid = 1 and c._name like '" + device + "' and c.subcategory like '" + scat + "' and c.category like '" + cat + "' LEFT JOIN TSMCDB.dbo.v_DeviceIOPoint_Calculate_V2 d on a.io_id = d.io_id and a.alarm_time = d.datetime where a.[datetime] >= convert(datetime,'" + s_d + "') and a.[datetime] <= convert(datetime,'" + e_d + "') and d.[_status] like '%" + _value + "%' and a.[mail] like '%" + _receiver + "%' order by datetime desc";
                dt = db.query(str);
                db.excute("delete a from TSMCDB.dbo.s_DeviceIOPoint_Short a inner join (select io_id, _status, datetime from (" + union + ") x ) b on a.io_id = b.io_id and a.datetime = b.datetime");
                break;

            case "TimeSetting":

                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_TimeSetting");

                log_table = "SELECT a._id, a._status, a.datetime, a.timeset_id, a.[table], a.io_id FROM ( " + union + " ) AS a ";
                str = "SELECT TOP " + _count + " c.[_name], b.[_operation], convert(varchar,a.[datetime],120) as datetime, a.[_status], a.[timeset_id], a.[table] FROM ( " + log_table + " ) a inner join [TSMCDB].[dbo].[s_DeviceIOPoint] b on a.io_id = b._id and b._operation like '" + io + "' inner join [TSMCDB].[dbo].[v_DeviceSetting] c on c._id = b.deviceID and c.valid = 1 and c._name like '" + device + "' and c.subcategory like '" + scat + "' and c.category like '" + cat + "' where a.[datetime] >= convert(datetime,'" + s_d + "') and a.[datetime] <= convert(datetime,'" + e_d + "') and a.[_status] like '%" + _value + "%' order by datetime desc";
                dt = db.query(str);
                break;

        }

        //xlsx 檔案位置
        string FilePath = Session["webPath"].ToString() + Session["webName"].ToString() + @"\download\" + DateTime.Now.ToString("yyyyMMddHHmmss") + @".xlsx";
        //FilePath = @"D:\Ollie\台科大\Center\Center\download\" + DateTime.Now.ToString("yyyyMMddHHmmss") + @".xlsx";

        //建立 xlxs 轉換物件
        Export.XSLXHelper helper = new Export.XSLXHelper();
        //取得轉為 xlsx 的物件
        var xlsx = new XLWorkbook();

        switch (t)
        {
            case "History":
                var records_History = new List<Log_History_to_Excel>();
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var rows = new Log_History_to_Excel();
                    rows._name = dt.Rows[i]["_name"].ToString();
                    rows._operation = dt.Rows[i]["_operation"].ToString();
                    rows.datetime = Convert.ToDateTime(dt.Rows[i]["datetime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    rows._value = dt.Rows[i]["_status"].ToString();

                    records_History.Add(rows);
                }
                xlsx = helper.Export(records_History);
                break;
            case "Alarm":
                var records_Alarm = new List<Log_Alarm_to_Excel>();
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var rows = new Log_Alarm_to_Excel();
                    rows._name = dt.Rows[i]["_name"].ToString();
                    rows._operation = dt.Rows[i]["_operation"].ToString();
                    rows.datetime = Convert.ToDateTime(dt.Rows[i]["datetime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    rows._status = dt.Rows[i]["_status"].ToString();
                    rows.condition = dt.Rows[i]["condition"].ToString();
                    rows.alarm_info = dt.Rows[i]["alarm_info"].ToString();

                    records_Alarm.Add(rows);
                }
                xlsx = helper.Export(records_Alarm);
                break;
            case "Detect":
                var records_Detect = new List<Log_Detect_to_Excel>();
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var rows = new Log_Detect_to_Excel();
                    rows._name = dt.Rows[i]["_name"].ToString();
                    rows._operation = dt.Rows[i]["_operation"].ToString();
                    rows.datetime = Convert.ToDateTime(dt.Rows[i]["datetime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    rows._status = dt.Rows[i]["_status"].ToString();
                    rows._value = dt.Rows[i]["_value"].ToString();
                    rows.detect_info = dt.Rows[i]["detect_info"].ToString();

                    records_Detect.Add(rows);
                }
                xlsx = helper.Export(records_Detect);
                break;
            case "Control":
                var records_Control = new List<Log_Control_to_Excel>();
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var rows = new Log_Control_to_Excel();
                    rows._name = dt.Rows[i]["_name"].ToString();
                    rows._operation = dt.Rows[i]["_operation"].ToString();
                    rows.datetime = Convert.ToDateTime(dt.Rows[i]["datetime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    rows._status = dt.Rows[i]["_status"].ToString();
                    rows._user = dt.Rows[i]["_user"].ToString();

                    records_Control.Add(rows);
                }
                xlsx = helper.Export(records_Control);
                break;
            case "Mail_Msg":
                var records_Mail_Msg = new List<Log_Mail_Msg_to_Excel>();
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var rows = new Log_Mail_Msg_to_Excel();
                    rows._name = dt.Rows[i]["_name"].ToString();
                    rows._operation = dt.Rows[i]["_operation"].ToString();
                    rows.datetime = Convert.ToDateTime(dt.Rows[i]["datetime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    rows._status = dt.Rows[i]["_status"].ToString();
                    rows.mail = dt.Rows[i]["mail"].ToString();

                    records_Mail_Msg.Add(rows);
                }
                xlsx = helper.Export(records_Mail_Msg);
                break;
            case "TimeSetting":
                var records_TimeSetting = new List<Log_TimeSetting_to_Excel>();
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var rows = new Log_TimeSetting_to_Excel();
                    rows._name = dt.Rows[i]["_name"].ToString();
                    rows._operation = dt.Rows[i]["_operation"].ToString();
                    rows.datetime = Convert.ToDateTime(dt.Rows[i]["datetime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    rows._status = dt.Rows[i]["_status"].ToString();

                    records_TimeSetting.Add(rows);
                }
                xlsx = helper.Export(records_TimeSetting);
                break;
        }

        dt.Dispose();

        //存檔至指定位置
        xlsx.SaveAs(FilePath);

        //string path = @"D:\NTUST\CenterWEB\Center\Center\download\";
        string FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + @".xlsx";

        return new
        {
            FilePath
        };
    }

    public class Log_History_to_Excel
    {
        [Description("設備名稱")]
        public string _name { get; set; }
        [Description("點位名稱")]
        public string _operation { get; set; }
        [Description("時間")]
        public string datetime { get; set; }
        [Description("狀態")]
        public string _value { get; set; }
    }
    public class Log_Alarm_to_Excel
    {
        [Description("設備名稱")]
        public string _name { get; set; }
        [Description("點位名稱")]
        public string _operation { get; set; }
        [Description("時間")]
        public string datetime { get; set; }
        [Description("值")]
        public string _status { get; set; }
        [Description("判斷式")]
        public string condition { get; set; }
        [Description("類別")]
        public string alarm_info { get; set; }
    }
    public class Log_Detect_to_Excel
    {
        [Description("設備名稱")]
        public string _name { get; set; }
        [Description("點位名稱")]
        public string _operation { get; set; }
        [Description("時間")]
        public string datetime { get; set; }
        [Description("值")]
        public string _value { get; set; }
        [Description("狀態")]
        public string _status { get; set; }
        [Description("動作")]
        public string detect_info { get; set; }
    }
    public class Log_Control_to_Excel
    {
        [Description("設備名稱")]
        public string _name { get; set; }
        [Description("點位名稱")]
        public string _operation { get; set; }
        [Description("時間")]
        public string datetime { get; set; }
        [Description("狀態")]
        public string _status { get; set; }
        [Description("使用者名稱")]
        public string _user { get; set; }
    }
    public class Log_Mail_Msg_to_Excel
    {
        [Description("設備名稱")]
        public string _name { get; set; }
        [Description("點位名稱")]
        public string _operation { get; set; }
        [Description("時間")]
        public string datetime { get; set; }
        [Description("狀態")]
        public string _status { get; set; }
        [Description("收件者")]
        public string mail { get; set; }
    }
    public class Log_TimeSetting_to_Excel
    {
        [Description("設備名稱")]
        public string _name { get; set; }
        [Description("點位名稱")]
        public string _operation { get; set; }
        [Description("時間")]
        public string datetime { get; set; }
        [Description("狀態")]
        public string _status { get; set; }
    }


    public string log_table_str_loop(int s_y, int s_m, int e_y, int e_m, int n_y, int n_m, string union, string table)
    {
        for (var i = s_y; i <= e_y; i++)
        {
            if (i == s_y && i != e_y)
            {
                for (var j = s_m; j <= 12; j++)
                {
                    union = log_table_str(i, j, n_y, n_m, union, table);
                }
            }
            if (i != s_y && i != e_y)
            {
                for (var j = 1; j <= 12; j++)
                {
                    union = log_table_str(i, j, n_y, n_m, union, table);
                }
            }
            if (i != s_y && i == e_y)
            {
                for (var j = 1; j <= e_m; j++)
                {
                    union = log_table_str(i, j, n_y, n_m, union, table);
                }
            }
            if (i == s_y && i == e_y)
            {
                for (var j = s_m; j <= e_m; j++)
                {
                    union = log_table_str(i, j, n_y, n_m, union, table);
                }
            }
        }

        return union;
    }

    public string log_table_str(int y, int m, int n_y, int n_m, string union, string table)
    {
        var union_str = "";
        var db_name = "CenterLog_" + y + "_" + ((m < 10) ? "0" + m.ToString() : m.ToString()) + "";
        var table_name = table + "_" + y + "_" + ((m < 10) ? "0" + m.ToString() : m.ToString());

        if (y == n_y && m == n_m)
            union_str += " select * from [CenterLog].[dbo].[" + table + "] ";
        else
        {
            var is_db_created = db.query("use master if EXISTS(SELECT * FROM sysdatabases WHERE name='" + db_name + "') select 1 as x else select 0 as x").Rows[0]["x"].ToString();
            if (is_db_created == "1")
            {
                var is_table_created = db.query("use " + db_name + " IF EXISTS(SELECT * FROM sysobjects WHERE name='" + table_name + "') select 1 as x else select 0 as x").Rows[0]["x"].ToString();
                if (is_table_created == "1")
                {
                    union_str += " select * from " + db_name + ".[dbo]." + table_name;
                }
            }
        }

        if (union != "" && union_str != "")
        {
            union += " union ";
        }

        union += union_str;

        return union;
    }

    [WebMethod]
    public object get_three_step_setting()
    {
        var dt = db.query("select * from v_PLC_Memory_Editor where _name = '三階段卸載'");

        var rows = new List<View>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var row = new View();
            row._operation = dt.Rows[i]["_operation"].ToString();
            row._status = dt.Rows[i]["_status"].ToString();

            rows.Add(row);
        }
        
        return new { rows };
    }

    [WebMethod]
    public object update_three_steps(string new_m,string new_s1, string new_s2, string new_s3 )
    {
        var result = db.excute("update s_DeviceIOPoint set _status = '" + new_m + "' from s_DeviceIOPoint where _operation = '契約電量'");
        result = db.excute("update s_DeviceIOPoint set _status = '" + new_s1 + "' from s_DeviceIOPoint where _operation = '第一階段電量'");
        result = db.excute("update s_DeviceIOPoint set _status = '" + new_s2 + "' from s_DeviceIOPoint where _operation = '第二階段電量'");
        result = db.excute("update s_DeviceIOPoint set _status = '" + new_s3 + "' from s_DeviceIOPoint where _operation = '第三階段電量'");

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object three_steps_control_s3()
    {
        var result = db.excute("update s_DeviceIOPoint set _status = '65535', confirm = 0 from s_DeviceIOPoint where _operation = '第三階段連動'");

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object three_steps_control_s2()
    {
        var result = db.excute("update s_DeviceIOPoint set _status = '65535', confirm = 0 from s_DeviceIOPoint where _operation = '第二階段連動'");

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object three_steps_control_s1()
    {
        var result = db.excute("update s_DeviceIOPoint set _status = '65535', confirm = 0 from s_DeviceIOPoint where _operation = '第一階段連動'");

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object set_timeset_on_off(string _id, string valid)
    {
        var result = 0;
        var type = _id.Substring(0, 2);
        var id = _id.Substring(2, _id.Length-2);

        if (type == "g_")
            result = db.excute("update [s_TimeSetting_Group] set [valid] = '" + valid + "' where [_id] = '" + id + "'");
        if (type == "d_")
            result = db.excute("update [s_TimeSetting_Device] set [valid] = '" + valid + "' where [_id] = '" + id + "'");


        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object alarm_reset(string io_id)
    {
        var result = db.excute("update s_DeviceIOPoint set _status = '0', plc_sub_0 = 0, isUpdated = 0 where _id = '" + io_id + "'");
        var _status = db.query("select _status from v_DeviceIOPoint_Calculate where _id = '" + io_id + "'").Rows[0]["_status"].ToString();

        if (result == 0)
            return JsonConvert.SerializeObject(false);
        else
            return new
            {
                _status
            };
    }

    [WebMethod(EnableSession = true)]
    public List<object> GetPowerReport(string io_id, string s, string e, string t)
    {
        List<object> iData = new List<object>();

        List<string> labelSets = new List<string>();
        List<float> dataSets = new List<float>();
        List<float> dataSets2 = new List<float>();
        List<float> dataSets3 = new List<float>();

        var dt0 = db.query("select a._type, b.category_type from TSMCDB.dbo.s_deviceiopoint a inner join TSMCDB.dbo.s_devicesetting b on a.deviceid = b._id where a._id = '" + io_id + "'");
        var category_type = dt0.Rows[0]["category_type"].ToString();
        var io_type = dt0.Rows[0]["_type"].ToString();

        var log_table = "";
        var union = "";

        var s_y = Convert.ToDateTime(s).Year;
        var s_m = Convert.ToDateTime(s).Month;
        var e_y = Convert.ToDateTime(e).Year;
        var e_m = Convert.ToDateTime(e).Month;
        var n_y = DateTime.Now.Year;
        var n_m = DateTime.Now.Month;

        switch (category_type)
        {
            case "空調系統":
                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Air");
                break;
            case "計費系統":
                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Toll");
                break;
            case "消防系統":
                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Fire");
                break;
            default:
                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Other");
                break;
        }

        log_table = "SELECT a._id, c._name, b._operation, a.datetime, a._value, a._status, b._id AS io_id FROM ( " + union + " ) AS a LEFT OUTER JOIN TSMCDB.dbo.s_DeviceIOPoint AS b ON a.io_id = b._id LEFT OUTER JOIN TSMCDB.dbo.v_DeviceSetting AS c ON b.deviceID = c._id LEFT OUTER JOIN TSMCDB.dbo.s_DeviceIOPoint_Ratio AS d ON c.Category = d.Category AND c.subCategory = d.subCategory AND b._operation = d._operation";
        
        var dt = new DataTable();
        var dt2 = db.query("select c.unit from s_DeviceIOPoint a inner join s_DeviceSetting b on a.deviceID = b._id inner join s_DeviceIOPoint_Ratio c on a._operation = c._operation and b.Category = c.Category and b.subCategory = c.subCategory where a._id = '" + io_id + "' ");

        switch (t)
        {
            case "year":
                dt = db.query("select x._id, datetime, _status from ( " + log_table + " ) x inner join (select max(_id) as _id, Y from ( select _id, DATEPART(YEAR,datetime) as Y from ( " + log_table + " ) x where io_id = '" + io_id + "' and datetime >= convert(date,'" + s +"') and datetime <= convert(datetime,'" + e + "') and _status is not null and _status <> 'N/A') x group by Y) y on x._id = y._id and DATEPART(YEAR,x.datetime) = y.Y order by datetime");
                break;
            case "month":
                dt = db.query("select x._id, datetime, _status from ( " + log_table + " ) x inner join (select max(_id) as _id, Y, M from ( select _id, DATEPART(YEAR,datetime) as Y, DATEPART(MONTH,datetime) as M from ( " + log_table + " ) x where io_id = '" + io_id + "' and datetime >= convert(date,'" + s + "') and datetime <= convert(datetime,'" + e + "') and _status is not null and _status <> 'N/A') x group by Y, M) y on x._id = y._id and DATEPART(YEAR,x.datetime) = y.Y and DATEPART(MONTH,x.datetime) = y.M order by datetime");
                break;
            case "week":
                dt = db.query("select x._id, datetime, _status from ( " + log_table + " ) x inner join (select max(_id) as _id, Y, W from ( select _id, DATEPART(YEAR,datetime) as Y, DATEPART(WEEK,datetime) as W from ( " + log_table + " ) x where io_id = '" + io_id + "' and datetime >= convert(date,'" + s + "') and datetime <= convert(datetime,'" + e + "') and _status is not null and _status <> 'N/A') x group by Y, W) y on x._id = y._id and DATEPART(YEAR,x.datetime) = y.Y and DATEPART(WEEK,x.datetime) = y.W order by datetime");
                break;
            case "day":
                dt = db.query("select x._id, datetime, _status from ( " + log_table + " ) x inner join (select max(_id) as _id, Y, M, D from ( select _id, DATEPART(YEAR,datetime) as Y, DATEPART(MONTH,datetime) as M, DATEPART(DAY,datetime) as D from ( " + log_table + " ) x where io_id = '" + io_id + "' and datetime >= convert(date,'" + s + "') and datetime <= convert(datetime,'" + e + "') and _status is not null and _status <> 'N/A') x group by Y, M, D) y on x._id = y._id and DATEPART(YEAR,x.datetime) = y.Y and DATEPART(MONTH,x.datetime) = y.M and DATEPART(DAY,x.datetime) = y.D order by datetime");
                break;
            case "hour":
                dt = db.query("select x._id, datetime, _status from ( " + log_table + " ) x inner join (select max(_id) as _id, Y, M, D, H from ( select _id, DATEPART(YEAR,datetime) as Y, DATEPART(MONTH,datetime) as M, DATEPART(DAY,datetime) as D, DATEPART(HOUR,datetime) as H from ( " + log_table + " ) x where io_id = '" + io_id + "' and datetime >= convert(date,'" + s + "') and datetime <= convert(datetime,'" + e + "') and _status is not null and _status <> 'N/A') x group by Y, M, D, H) y on x._id = y._id and DATEPART(YEAR,x.datetime) = y.Y and DATEPART(MONTH,x.datetime) = y.M and DATEPART(DAY,x.datetime) = y.D and DATEPART(HOUR,x.datetime) = y.H order by datetime");
                break;
            case "minute":
                dt = db.query("select x._id, datetime, _status from ( " + log_table + " ) x inner join (select max(_id) as _id, Y, M, D, H, mm from ( select _id, DATEPART(YEAR,datetime) as Y, DATEPART(MONTH,datetime) as M, DATEPART(DAY,datetime) as D, DATEPART(HOUR,datetime) as H, DATEPART(MINUTE,datetime) as mm from ( " + log_table + " ) x where io_id = '" + io_id + "' and datetime >= convert(date,'" + s + "') and datetime <= convert(datetime,'" + e + "') and _status is not null and _status <> 'N/A') x group by Y, M, D, H, mm) y on x._id = y._id and DATEPART(YEAR,x.datetime) = y.Y and DATEPART(MONTH,x.datetime) = y.M and DATEPART(DAY,x.datetime) = y.D and DATEPART(HOUR,x.datetime) = y.H and DATEPART(MINUTE,x.datetime) = y.mm order by datetime");
                break;
        }

        foreach (DataRow drow in dt.Rows)
        {
            var ff = (dt2.Rows[0]["unit"].ToString() != "") ? float.Parse(drow["_status"].ToString().Replace(dt2.Rows[0]["unit"].ToString(), "")) : float.Parse(drow["_status"].ToString());
            dataSets.Add(ff);

            switch (t)
            {
                case "year":
                    var d_y = Convert.ToDateTime(drow["datetime"].ToString()).ToString("yyyy");
                    labelSets.Add(d_y);
                    break;
                case "month":
                    var d_m = Convert.ToDateTime(drow["datetime"].ToString()).ToString("yyyy/MM");
                    labelSets.Add(d_m);
                    break;
                case "week":
                    var d_w = Convert.ToDateTime(drow["datetime"].ToString()).ToString("yyyy/MM/dd dddd");
                    labelSets.Add(d_w);
                    break;
                case "day":
                    var d_d = Convert.ToDateTime(drow["datetime"].ToString()).ToString("yyyy/MM/dd");
                    labelSets.Add(d_d);
                    break;
                case "hour":
                    var d_h = Convert.ToDateTime(drow["datetime"].ToString()).ToString("yyyy/MM/dd HH:00:00");
                    labelSets.Add(d_h);
                    break;
                case "minute":
                    var d_mm = Convert.ToDateTime(drow["datetime"].ToString()).ToString("yyyy/MM/dd HH:mm:00");
                    labelSets.Add(d_mm);
                    break;
            }
        }

        //var WM_and_EM = new string[6]{ "電力度數計WHSUM", "電力度數計WHA", "電力度數計WHB", "電力度數計WHC", "水表度數計", "電力度數累計" };
        var isWMEM = "F";
        //if(WM_and_EM.Contains(io_type))
        //{
            isWMEM = "T";
            for (var i =0;i< dataSets.Count; i++)
            {
                if (i != 0)
                {
                    dataSets3.Add(dataSets[i] - dataSets[i-1]);
                }
                else
                {
                    dataSets3.Add(0);
                }
            }
        //}

        var max = dataSets.Max();

        iData.Add(labelSets);
        iData.Add(dataSets);
        iData.Add(dataSets2);
        iData.Add(dt2.Rows[0]["unit"].ToString());
        iData.Add(max);
        iData.Add(isWMEM);
        iData.Add(dataSets3);
        return iData;

    }

    [WebMethod]
    public object get_name_and_operations_by_id(string d_id)
    {
        var dt = db.query("select a._name, b._id, b._operation from v_DeviceSetting a inner join s_DeviceIOPoint b on a._id = b.deviceID where a._id = '" + d_id + "' and b.IO_type = 'AI' and b.isSend = 1");

        var _name = dt.Rows[0]["_name"].ToString();
        var io_id = new List<string>();
        var _operation = new List<string>();
        
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            io_id.Add(dt.Rows[i]["_id"].ToString());
            _operation.Add(dt.Rows[i]["_operation"].ToString());
        }

        return new
        {
            _name,
            io_id,
            _operation
        };
    }

    [WebMethod]
    public object get_range(string io_id)
    {
        var dt = new DataTable();
        var dt2 = new DataTable();

        dt = db.query("SELECT TOP (1) datepart(YEAR,convert(date,[datetime])) as _year ,datepart(MONTH,convert(date,[datetime])) as _month ,datepart(DAY,convert(date,[datetime])) as _day FROM [CenterLog].[dbo].[Log_History_First] WHERE io_id = '" + io_id + "'");
        dt2 = db.query("SELECT TOP (1) datepart(YEAR,convert(date,[datetime])) as _year ,datepart(MONTH,convert(date,[datetime])) as _month ,datepart(DAY,convert(date,[datetime])) as _day FROM [CenterLog].[dbo].[Log_History_Last] WHERE io_id = '" + io_id + "'");

        var s_y = "";
        var s_m = "";
        var s_d = "";

        var e_y = "";
        var e_m = "";
        var e_d = "";

        if (dt.Rows.Count > 0)
        {
            s_y = dt.Rows[0]["_year"].ToString();
            s_m = dt.Rows[0]["_month"].ToString();
            s_d = dt.Rows[0]["_day"].ToString();
        }
        else
        {
            s_y = DateTime.Now.Year.ToString();
            s_m = DateTime.Now.Month.ToString();
            s_d = DateTime.Now.Day.ToString();
        }

        if (dt.Rows.Count > 0)
        {
            e_y = dt2.Rows[0]["_year"].ToString();
            e_m = dt2.Rows[0]["_month"].ToString();
            e_d = dt2.Rows[0]["_day"].ToString();
        }
        else
        {
            e_y = DateTime.Now.Year.ToString();
            e_m = DateTime.Now.Month.ToString();
            e_d = DateTime.Now.Day.ToString();
        }


        return new
        {
            s_y,
            s_m,
            s_d,
            e_y,
            e_m,
            e_d
        };
    }

    [WebMethod]
    public object save_as_excel_for_power(string io_id, string s, string e, string t)
    {
        var dt0 = db.query("select a._type, b.category_type from TSMCDB.dbo.s_deviceiopoint a inner join TSMCDB.dbo.s_devicesetting b on a.deviceid = b._id where a._id = '" + io_id + "'");
        var category_type = dt0.Rows[0]["category_type"].ToString();
        List<string> labelSets = new List<string>();
        var export_log = new List<Log>();
        //export_log = new List<Log2>();

        var log_table = "";
        var union = "";

        var s_y = Convert.ToDateTime(s).Year;
        var s_m = Convert.ToDateTime(s).Month;
        var e_y = Convert.ToDateTime(e).Year;
        var e_m = Convert.ToDateTime(e).Month;
        var n_y = DateTime.Now.Year;
        var n_m = DateTime.Now.Month;

        switch (category_type)
        {
            case "空調系統":
                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Air");
                break;
            case "計費系統":
                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Toll");
                break;
            case "消防系統":
                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Fire");
                break;
            default:
                union = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union, "Log_History_Other");
                break;
        }

        log_table = "SELECT a._id, c._name, b._operation, a.datetime, a._value, a._status, b._id AS io_id FROM ( " + union + " ) AS a LEFT OUTER JOIN TSMCDB.dbo.s_DeviceIOPoint AS b ON a.io_id = b._id LEFT OUTER JOIN TSMCDB.dbo.v_DeviceSetting AS c ON b.deviceID = c._id LEFT OUTER JOIN TSMCDB.dbo.s_DeviceIOPoint_Ratio AS d ON c.Category = d.Category AND c.subCategory = d.subCategory AND b._operation = d._operation";

        var dt = new DataTable();

        switch (t)
        {
            case "year":
                dt = db.query("select [_name],[_operation],[datetime],[_value],[_status]  from ( " + log_table + " ) x inner join (select max(_id) as _id, Y from ( select _id, DATEPART(YEAR,datetime) as Y from ( " + log_table + " ) x where io_id = '" + io_id + "' and datetime >= convert(date,'" + s + "') and datetime <= convert(datetime,'" + e + "') and _status is not null and _status <> 'N/A') x group by Y) y on x._id = y._id and DATEPART(YEAR,x.datetime) = y.Y order by datetime");
                break;
            case "month":
                dt = db.query("select [_name],[_operation],[datetime],[_value],[_status]  from ( " + log_table + " ) x inner join (select max(_id) as _id, Y, M from ( select _id, DATEPART(YEAR,datetime) as Y, DATEPART(MONTH,datetime) as M from ( " + log_table + " ) x where io_id = '" + io_id + "' and datetime >= convert(date,'" + s + "') and datetime <= convert(datetime,'" + e + "') and _status is not null and _status <> 'N/A') x group by Y, M) y on x._id = y._id and DATEPART(YEAR,x.datetime) = y.Y and DATEPART(MONTH,x.datetime) = y.M order by datetime");
                break;
            case "week":
                dt = db.query("select [_name],[_operation],[datetime],[_value],[_status]  from ( " + log_table + " ) x inner join (select max(_id) as _id, Y, W from ( select _id, DATEPART(YEAR,datetime) as Y, DATEPART(WEEK,datetime) as W from ( " + log_table + " ) x where io_id = '" + io_id + "' and datetime >= convert(date,'" + s + "') and datetime <= convert(datetime,'" + e + "') and _status is not null and _status <> 'N/A') x group by Y, W) y on x._id = y._id and DATEPART(YEAR,x.datetime) = y.Y and DATEPART(WEEK,x.datetime) = y.W order by datetime");
                break;
            case "day":
                dt = db.query("select [_name],[_operation],[datetime],[_value],[_status]  from ( " + log_table + " ) x inner join (select max(_id) as _id, Y, M, D from ( select _id, DATEPART(YEAR,datetime) as Y, DATEPART(MONTH,datetime) as M, DATEPART(DAY,datetime) as D from ( " + log_table + " ) x where io_id = '" + io_id + "' and datetime >= convert(date,'" + s + "') and datetime <= convert(datetime,'" + e + "') and _status is not null and _status <> 'N/A') x group by Y, M, D) y on x._id = y._id and DATEPART(YEAR,x.datetime) = y.Y and DATEPART(MONTH,x.datetime) = y.M and DATEPART(DAY,x.datetime) = y.D order by datetime");
                break;
            case "hour":
                dt = db.query("select [_name],[_operation],[datetime],[_value],[_status]  from ( " + log_table + " ) x inner join (select max(_id) as _id, Y, M, D, H from ( select _id, DATEPART(YEAR,datetime) as Y, DATEPART(MONTH,datetime) as M, DATEPART(DAY,datetime) as D, DATEPART(HOUR,datetime) as H from ( " + log_table + " ) x where io_id = '" + io_id + "' and datetime >= convert(date,'" + s + "') and datetime <= convert(datetime,'" + e + "') and _status is not null and _status <> 'N/A') x group by Y, M, D, H) y on x._id = y._id and DATEPART(YEAR,x.datetime) = y.Y and DATEPART(MONTH,x.datetime) = y.M and DATEPART(DAY,x.datetime) = y.D and DATEPART(HOUR,x.datetime) = y.H order by datetime");
                break;
            case "minute":
                dt = db.query("select [_name],[_operation],[datetime],[_value],[_status]  from ( " + log_table + " ) x inner join (select max(_id) as _id, Y, M, D, H, mm from ( select _id, DATEPART(YEAR,datetime) as Y, DATEPART(MONTH,datetime) as M, DATEPART(DAY,datetime) as D, DATEPART(HOUR,datetime) as H, DATEPART(MINUTE,datetime) as mm from ( " + log_table + " ) x where io_id = '" + io_id + "' and datetime >= convert(date,'" + s + "') and datetime <= convert(datetime,'" + e + "') and _status is not null and _status <> 'N/A') x group by Y, M, D, H, mm) y on x._id = y._id and DATEPART(YEAR,x.datetime) = y.Y and DATEPART(MONTH,x.datetime) = y.M and DATEPART(DAY,x.datetime) = y.D and DATEPART(HOUR,x.datetime) = y.H and DATEPART(MINUTE,x.datetime) = y.mm order by datetime");
                break;
        }

        var logs = new List<Log>();
        foreach (DataRow drow in dt.Rows)
        {
            var log = new Log();
            log._name = drow["_name"].ToString();
            log._operation = drow["_operation"].ToString();
            log._value = drow["_value"].ToString();
            log._status = drow["_status"].ToString();

            switch (t)
            {
                case "year":
                    log.datetime = Convert.ToDateTime(drow["datetime"].ToString()).ToString("yyyy");
                    break;
                case "month":
                    log.datetime = Convert.ToDateTime(drow["datetime"].ToString()).ToString("yyyy/MM");
                    break;
                case "week":
                    log.datetime = Convert.ToDateTime(drow["datetime"].ToString()).ToString("MM/dd dddd");
                    break;
                case "day":
                    log.datetime = Convert.ToDateTime(drow["datetime"].ToString()).ToString("MM/dd");
                    break;
                case "hour":
                    log.datetime = Convert.ToDateTime(drow["datetime"].ToString()).ToString("MM/dd HH:00:00");
                    break;
                case "minute":
                    log.datetime = Convert.ToDateTime(drow["datetime"].ToString()).ToString("HH:mm:00");
                    break;
            }

            logs.Add(log);
        }

        //xlsx 檔案位置
        string FilePath = @"C:\inetpub\wwwroot\Center\download\" + DateTime.Now.ToString("yyyyMMddHHmmss") + @".xlsx";
        FilePath = @"D:\Ollie\台科大\Center\Center\download\" + DateTime.Now.ToString("yyyyMMddHHmmss") + @".xlsx";

        //建立 xlxs 轉換物件
        Export.XSLXHelper helper = new Export.XSLXHelper();
        //取得轉為 xlsx 的物件
        var xlsx = helper.Export(logs);
        //存檔至指定位置
        xlsx.SaveAs(FilePath);
        
        //string path = @"D:\NTUST\CenterWEB\Center\Center\download\";
        string FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + @".xlsx";
        
        return new
        {
            FilePath
        };
    }

    public class Log
    {
        [Description("設備名稱")]
        public string _name { get; set; }
        [Description("點位名稱")]
        public string _operation { get; set; }
        [Description("時間")]
        public string datetime { get; set; }
        [Description("原始值")]
        public string _value { get; set; }
        [Description("狀態")]
        public string _status { get; set; }
    }

    [WebMethod]
    public void set_operation_sort(string cat, string scat, string io_list)
    {
        var io_sort = io_list.Split(',');

        for (var i = 0; i < io_sort.Length; i++) 
        {
            var io = io_sort[i];
            db.excute("update a set a._sort = " + i + " from s_deviceiopoint a inner join s_devicesetting b on a.deviceid = b._id where b.Category = '" + cat + "' and b.subCategory = '" + scat + "' and b.valid = 1 and a._operation = '" + io + "'");
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetRole()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var db = new DBAcess();
        DataTable dt = new DataTable();

        var str = "SELECT r_id, r_name, r_describe, r_level FROM s_Role where valid = 1";

        dt = db.query(str);

        var records = new List<Role>();

        try
        {
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var rows = new Role();
                rows.r_id = dt.Rows[i]["r_id"].ToString();
                rows.r_name = dt.Rows[i]["r_name"].ToString();
                rows.r_describe = dt.Rows[i]["r_describe"].ToString();
                rows.r_level = dt.Rows[i]["r_level"].ToString();

                records.Add(rows);
            }
        }
        catch
        {

        }

        dt.Dispose();

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            sb.Append("\"" + "<div class='r_name'>" + result.r_name + "</div>" + "\",");
            sb.Append("\"" + "<div class='r_level'>" + result.r_level + "</div>" + "\",");
            sb.Append("\"" + "<div class='r_describe'>" + result.r_describe + "</div>" + "\",");

            sb.Append("\"" + "<div class='button icon-pencil' onclick='edit_role(this)' style='width:50px;margin:0px 10px;'>編輯</div><div class='button icon-flow-cascade' onclick='edit_permission(this)' style='width:80px;margin:0px 10px;'>權限設定</div><div class='button icon-trash' onclick='del_confirm(this)' style='width:50px;margin:0px 10px;'>刪除</div><input class='r_id' type='hidden' value='" + result.r_id + "'>" + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");
        return sb.ToString();
    }

    public class Role
    {
        public string r_id { get; set; }
        public string r_name { get; set; }
        public string r_describe { get; set; }
        public string r_level { get; set; }

        public Role()
        {

        }

        public Role(string _r_id, string _r_name, string _r_describe, string _r_level)
        {
            r_id = _r_id;
            r_name = _r_name;
            r_describe = _r_describe;
            r_level = _r_level;
        }
    }

    [WebMethod]
    public object create_role(string r_name, string r_level, string r_describe)
    {
        var result = db.excute(" insert into [s_Role] ( [r_name], [r_level], [r_describe], [create_time], [valid]) values ('" + r_name + "', '" + r_level + "', '" + r_describe + "', getdate(), 1 ) ");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object delete_role(string r_id)
    {
        var result = db.excute(" update [s_Role] set valid = 0, del_time = getdate() where r_id = '" + r_id + "' ");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object update_role(string r_id, string r_name, string r_level, string r_describe)
    {
        var result = db.excute(" update [s_Role] set [r_name] = '" + r_name + "', [r_level] = '" + r_level + "', [r_describe] = '" + r_describe + "' where r_id = '" + r_id + "' ");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object getFunctions(string r_id)
    {
        var dt = db.query("SELECT a.Fid, a.FName, a.FGroup, a.FDescription, a.FParentID, a.Flevel, b.operate_enable as _enable FROM s_Functions a left join s_RF_mapping b on a.Fid = b.f_id and b.r_id = '" + r_id + "' order by a.Fid");

        var functions = new List<webFunction>();
        for (var i = 0; i < dt.Rows.Count; i++) 
        {
            var row = new webFunction();
            row.Fid = dt.Rows[i]["Fid"].ToString();
            row.FName = dt.Rows[i]["FName"].ToString();
            row.FGroup = dt.Rows[i]["FGroup"].ToString();
            row.FDescription = dt.Rows[i]["FDescription"].ToString();
            row.FParentID = dt.Rows[i]["FParentID"].ToString();
            row.Flevel = dt.Rows[i]["Flevel"].ToString();
            row._enable = dt.Rows[i]["_enable"].ToString();

            functions.Add(row);
        }

        return new
        {
            functions
        };
    }

    public class webFunction
    {
        public string Fid { get; set; }
        public string FName { get; set; }
        public string FGroup { get; set; }
        public string FDescription { get; set; }
        public string FParentID { get; set; }
        public string Flevel { get; set; }
        public string _enable { get; set; }
    }

    [WebMethod]
    public object update_permission(string r_id, string[] fids)
    {
        var result = 0;
        var len = fids.Length;

        foreach (var i in fids)
        {
            var f_id = i.Split('-')[0];
            var check = i.Split('-')[1];

            var dt = db.query(" select [operate_enable] from [s_RF_mapping] where [r_id] = '" + r_id + "' and f_id = '" + f_id + "' ");
            if (dt.Rows.Count == 0)
            {
                if (check == "true")
                {
                    result += db.excute("insert into [s_RF_mapping] ([r_id], [f_id], [operate_enable]) Values ( '" + r_id + "', '" + f_id + "', 1)");
                }
                if (check == "false")
                {
                    result += db.excute("insert into [s_RF_mapping] ([r_id], [f_id], [operate_enable]) Values ( '" + r_id + "', '" + f_id + "', 0)");
                }
            }
            else
            {
                if (dt.Rows[0]["operate_enable"].ToString() == "True")
                {
                    if (check == "true")
                    {
                        result += 1;
                    }
                    if (check == "false")
                    {
                        result += db.excute("update [s_RF_mapping] set [operate_enable] = 0 where [r_id] = '" + r_id + "' and [f_id] = '" + f_id + "'");
                    }
                }
                if (dt.Rows[0]["operate_enable"].ToString() == "False")
                {
                    if (check == "true")
                    {
                        result += db.excute("update [s_RF_mapping] set [operate_enable] = 1 where [r_id] = '" + r_id + "' and [f_id] = '" + f_id + "'");
                    }
                    if (check == "false")
                    {
                        result += 1;
                    }
                }
            }
        }

        if (result == len)
        {
            return JsonConvert.SerializeObject(true);
        }
        else
            return JsonConvert.SerializeObject(false);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetAccount()
    {
        var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
        var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
        var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);

        var db = new DBAcess();
        DataTable dt = new DataTable();

        var str = "SELECT a.[p_id], a.[username], a.[Account], a.[PassWord], a.[r_id], b.[r_name], a.[phone], a.[email], a.[Remark], a.[Receive_Mail], a.[Receive_Message] FROM [TSMCDB].[dbo].[s_Permissions] a inner join s_Role b on a.r_id = b.r_id where a.valid = 1";

        dt = db.query(str);

        var records = new List<AccountInfomation>();

        try
        {
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var rows = new AccountInfomation();
                rows.p_id = dt.Rows[i]["p_id"].ToString();
                rows.username = dt.Rows[i]["username"].ToString();
                rows.Account = dt.Rows[i]["Account"].ToString();
                rows.PassWord = dt.Rows[i]["PassWord"].ToString();
                rows.r_id = dt.Rows[i]["r_id"].ToString();
                rows.r_name = dt.Rows[i]["r_name"].ToString();
                rows.phone = dt.Rows[i]["phone"].ToString();
                rows.email = dt.Rows[i]["email"].ToString();
                rows.Remark = dt.Rows[i]["Remark"].ToString();
                rows.Receive_Mail = dt.Rows[i]["Receive_Mail"].ToString();
                rows.Receive_Message = dt.Rows[i]["Receive_Message"].ToString();

                records.Add(rows);
            }
        }
        catch
        {

        }

        dt.Dispose();

        var itemsToSkip = displayStart == 0
                          ? 0
                          : displayStart;
        var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();
        var hasMoreRecords = false;

        var sb = new StringBuilder();
        sb.Append(@"{" + "\"sEcho\": " + echo + ",");
        sb.Append("\"recordsTotal\": " + records.Count + ",");
        sb.Append("\"recordsFiltered\": " + records.Count + ",");
        sb.Append("\"iTotalRecords\": " + records.Count + ",");
        sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
        sb.Append("\"aaData\": [");

        foreach (var result in pagedResults)
        {
            if (hasMoreRecords)
            {
                sb.Append(",");
            }

            sb.Append("[");

            var hidden_password = "";
            for (var i = 0; i < result.PassWord.Length; i++) 
            {
                hidden_password += "*";
            }

            sb.Append("\"" + "<div class='a_username'>" + result.username + "</div>" + "\",");
            sb.Append("\"" + "<div class='a_Account'>" + result.Account + "</div>" + "\",");
            sb.Append("\"" + "<div class='a_PassWord'>" + hidden_password + "<input type='hidden' value='" + result.PassWord +  "' /></div>" + "\",");
            sb.Append("\"" + "<div class='a_r_name'>" + result.r_name + "<input type='hidden' value='" + result.r_id + "' /></div>" + "\",");
            sb.Append("\"" + "<div class='a_phone'>" + result.phone + "<input id='Receive_Message' type='hidden' value='" + result.Receive_Message + "' /></div>" + "\",");
            sb.Append("\"" + "<div class='a_email'>" + result.email + "<input id='Receive_Mail' type='hidden' value='" + result.Receive_Mail + "' /></div>" + "\",");
            sb.Append("\"" + "<div class='a_Remark'>" + result.Remark + "</div>" + "\",");

            sb.Append("\"" + "<div class='button icon-pencil' onclick='edit_Account(this)' style='width:50px;margin:0px 10px;'>編輯</div><div class='button icon-mail' onclick='edit_Receive(this)' style='width:80px;margin:0px 10px;'>接收設定</div><div class='button icon-trash' onclick='del_confirm(this)' style='width:50px;margin:0px 10px;'>刪除</div><input class='r_id' type='hidden' value='" + result.p_id + "'>" + "\"");

            sb.Append("]");
            hasMoreRecords = true;
        }

        if (pagedResults.Count < 1)
        {
            sb.Append("[");

            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\",");
            sb.Append("\"" + "\"");

            sb.Append("]");
        }

        sb.Append("]}");
        return sb.ToString();
    }

    public class AccountInfomation
    {
        public string p_id { get; set; }
        public string username { get; set; }
        public string Account { get; set; }
        public string PassWord { get; set; }
        public string r_id { get; set; }
        public string r_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string Remark { get; set; }
        public string Receive_Mail { get; set; }
        public string Receive_Message { get; set; }
    }

    [WebMethod]
    public object get_roles()
    {
        var dt = db.query("SELECT r_id, r_name, r_describe, r_level FROM s_Role where valid = 1");

        var roles = new List<Role>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var rows = new Role();
            rows.r_id = dt.Rows[i]["r_id"].ToString();
            rows.r_name = dt.Rows[i]["r_name"].ToString();
            rows.r_describe = dt.Rows[i]["r_describe"].ToString();
            rows.r_level = dt.Rows[i]["r_level"].ToString();

            roles.Add(rows);
        }

        return new
        {
            roles
        };
    }

    [WebMethod]
    public object check_Account(string Account)
    {
        var dt = db.query("SELECT * FROM s_Permissions where Account = '" + Account + "' and valid = 1 ");

        if (dt.Rows.Count == 0)
        {
            return JsonConvert.SerializeObject(true);
        }
        else
            return JsonConvert.SerializeObject(false);
    }

    [WebMethod]
    public object create_Account(string username, string Account, string PassWord, string r_id, string phone, string mail, string Remark)
    {
        var result = db.excute(" insert into [s_Permissions] ( [username], [Account], [PassWord], [r_id], [phone], [email], [Remark], [Ctime], [valid]) values ('" + username + "', '" + Account + "', '" + PassWord + "', '" + r_id + "', '" + phone + "', '" + mail + "', '" + Remark + "', getdate(), 1 ) ");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object delete_Account(string p_id)
    {
        var result = db.excute(" update [s_Permissions] set valid = 0, Dtime = getdate() where p_id = '" + p_id + "' ");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object update_Account(string p_id, string username, string PassWord, string r_id, string phone, string mail, string Remark)
    {
        var result = db.excute(" update [s_Permissions] set [username] = '" + username + "', [PassWord] = '" + PassWord + "', [r_id] = '" + r_id + "', [phone] = '" + phone + "', [email] = '" + mail + "', [Remark] = '" + Remark + "' where p_id = '" + p_id + "' ");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object update_Receive(string p_id, string Receive_Mail, string Receive_Message)
    {
        var result = db.excute(" update [s_Permissions] set [Receive_Mail] = '" + Receive_Mail + "', [Receive_Message] = '" + Receive_Message + "' where p_id = '" + p_id + "' ");

        if (result == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
            return JsonConvert.SerializeObject(true);
    }

    [WebMethod]
    public object get_meter_log_date()
    {
        var dt_meter_first = db.query(" SELECT TOP (1) datetime FROM [CenterLog].[dbo].[Log_History_First] a inner join TSMCDB.dbo.s_DeviceIOPoint b on a.io_id = b._id inner join TSMCDB.dbo.s_DeviceSetting c on b.deviceID = c._id where c.subCategory = '洗衣房' or c.subCategory = '商店及各樓層水錶' and room_id is not null and room_id <> '0' order by a.datetime ");
        var dt_meter_last = db.query("SELECT TOP (1) a.datetime FROM [CenterLog].[dbo].[Log_History_Last] a inner join TSMCDB.dbo.s_DeviceIOPoint b on a.io_id = b._id inner join TSMCDB.dbo.s_DeviceSetting c on b.deviceID = c._id where c.subCategory = '洗衣房' or c.subCategory = '商店及各樓層水錶' and room_id is not null and room_id <> '0' order by a.datetime desc");

        if (dt_meter_first.Rows.Count == 0 || dt_meter_last.Rows.Count == 0)
        {
            return JsonConvert.SerializeObject(false);
        }
        else
        {
            var f_year = dt_meter_first.Rows[0]["datetime"].ToString().Split('/')[0];
            var f_month = dt_meter_first.Rows[0]["datetime"].ToString().Split('/')[1];
            var l_year = dt_meter_last.Rows[0]["datetime"].ToString().Split('/')[0];
            var l_month = dt_meter_last.Rows[0]["datetime"].ToString().Split('/')[1];

            return new
            {
                f_year,
                f_month,
                l_year,
                l_month
            };
        }
            
    }

    [WebMethod]
    public object get_meter_report(string m_type, string y, string m)
    {
        var s_y = Convert.ToInt32(y);
        var s_m = Convert.ToInt32(m);
        var e_y = Convert.ToInt32(y);
        var e_m = Convert.ToInt32(m);
        var n_y = DateTime.Now.Year;
        var n_m = DateTime.Now.Month;
        var union_min = "";
        var union_max = "";
        var where_str = "";
        var where_str_sub = "";

        union_min = log_table_str_loop(s_y, s_m - 1, e_y, e_m - 1, n_y, n_m, union_min, "Log_History_Toll");
        union_max = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union_max, "Log_History_Toll");
        
        switch (m_type)
        {
            case "wm":
                where_str = "b._type = '水表度數計'";
                where_str_sub = "a._name like '%水錶%自助洗衣房%'";
                break;
            case "em":
                where_str = "b._type = '電力度數計WHSUM'";
                where_str_sub = "a._name like '%電表%衣%'";
                break;
        }

        var dt_min_str = "SELECT a._name,max(b.maxValue) as maxValue FROM TSMCDB.dbo.v_DeviceSetting a " +
                        "Left join( " +
                        "SELECT c._id, a.io_id, c._name, b._operation, max(a.datetime) as datetime, a._value, a._status as maxValue " +
                        "FROM( " + union_min + " ) AS a " +
                        "LEFT OUTER JOIN TSMCDB.dbo.s_DeviceIOPoint AS b ON a.io_id = b._id " +
                        "LEFT OUTER JOIN TSMCDB.dbo.v_DeviceSetting AS c ON b.deviceID = c._id " +
                        "LEFT OUTER JOIN TSMCDB.dbo.s_DeviceIOPoint_Ratio AS d ON c.Category = d.Category AND c.subCategory = d.subCategory AND b._operation = d._operation " +
                        "inner join( " +
                        "SELECT a.io_id, max(convert(float, a._value)) as maxValue " +
                        "FROM( " + union_min + " ) AS a " +
                        "group by a.io_id " +
                        ") e on a.io_id = e.io_id and a._value = e.maxValue " +
                        "where a._status is not null and " + where_str + " " +
                        "group by c._id, a.io_id, c._name, b._operation, a._value, a._status " +
                        ") b on a._id = b._id " +
                        "where " + where_str_sub + " " +
                        "group by a._name order by a._name ";

        var dt_max_str = "SELECT a._name,max(b.maxValue) as maxValue FROM TSMCDB.dbo.v_DeviceSetting a " +
                        "Left join( " +
                        "SELECT c._id, a.io_id, c._name, b._operation, max(a.datetime) as datetime, a._value, a._status as maxValue " +
                        "FROM( " + union_max + " ) AS a " +
                        "LEFT OUTER JOIN TSMCDB.dbo.s_DeviceIOPoint AS b ON a.io_id = b._id " +
                        "LEFT OUTER JOIN TSMCDB.dbo.v_DeviceSetting AS c ON b.deviceID = c._id " +
                        "LEFT OUTER JOIN TSMCDB.dbo.s_DeviceIOPoint_Ratio AS d ON c.Category = d.Category AND c.subCategory = d.subCategory AND b._operation = d._operation " +
                        "inner join( " +
                        "SELECT a.io_id, max(convert(float, a._value)) as maxValue " +
                        "FROM( " + union_max + " ) AS a " +
                        "group by a.io_id " +
                        ") e on a.io_id = e.io_id and a._value = e.maxValue " +
                        "where a._status is not null and " + where_str + " " +
                        "group by c._id, a.io_id, c._name, b._operation, a._value, a._status " +
                        ") b on a._id = b._id " +
                        "where " + where_str_sub + " " +
                        "group by a._name order by a._name ";

        var dt_min = db.query(dt_min_str);
        var dt_max = db.query(dt_max_str);

        var report = new List<Report_Meter>();

        for (var i = 0; i < dt_min.Rows.Count; i++) 
        {
            var row = new Report_Meter();
            row._name = dt_min.Rows[i]["_name"].ToString();
            row.minValue = dt_min.Rows[i]["maxValue"].ToString();
            row.maxValue = dt_max.Rows[i]["maxValue"].ToString();

            var minValue = Convert.ToDouble(row.minValue.Replace("度", "").Replace("m3", ""));
            var maxValue = Convert.ToDouble(row.maxValue.Replace("度", "").Replace("m3", ""));
            if (maxValue < minValue)
            {
                row.maxValue = row.minValue;
                maxValue = minValue;
            }

            row.diffValue = Math.Round(maxValue - minValue, 2).ToString() + ((m_type == "wm") ? "m3" : "度");
            report.Add(row);
        }

        return report;
    }

    [WebMethod(EnableSession = true)]
    public object get_meter_report_excel(string m_type, string y, string m)
    {
        var s_y = Convert.ToInt32(y);
        var s_m = Convert.ToInt32(m);
        var e_y = Convert.ToInt32(y);
        var e_m = Convert.ToInt32(m);
        var n_y = DateTime.Now.Year;
        var n_m = DateTime.Now.Month;
        var union_min = "";
        var union_max = "";
        var where_str = "";
        var where_str_sub = "";

        union_min = log_table_str_loop(s_y, s_m - 1, e_y, e_m - 1, n_y, n_m, union_min, "Log_History_Toll");
        union_max = log_table_str_loop(s_y, s_m, e_y, e_m, n_y, n_m, union_max, "Log_History_Toll");

        switch (m_type)
        {
            case "wm":
                where_str = "b._type = '水表度數計'";
                where_str_sub = "a._name like '%水錶%自助洗衣房%'";
                break;
            case "em":
                where_str = "b._type = '電力度數計WHSUM'";
                where_str_sub = "a._name like '%電表%衣%'";
                break;
        }

        var dt_min_str = "SELECT a._name,max(b.maxValue) as maxValue FROM TSMCDB.dbo.v_DeviceSetting a " +
                        "Left join( " +
                        "SELECT c._id, a.io_id, c._name, b._operation, max(a.datetime) as datetime, a._value, a._status as maxValue " +
                        "FROM( " + union_min + " ) AS a " +
                        "LEFT OUTER JOIN TSMCDB.dbo.s_DeviceIOPoint AS b ON a.io_id = b._id " +
                        "LEFT OUTER JOIN TSMCDB.dbo.v_DeviceSetting AS c ON b.deviceID = c._id " +
                        "LEFT OUTER JOIN TSMCDB.dbo.s_DeviceIOPoint_Ratio AS d ON c.Category = d.Category AND c.subCategory = d.subCategory AND b._operation = d._operation " +
                        "inner join( " +
                        "SELECT a.io_id, max(convert(float, a._value)) as maxValue " +
                        "FROM( " + union_min + " ) AS a " +
                        "group by a.io_id " +
                        ") e on a.io_id = e.io_id and a._value = e.maxValue " +
                        "where a._status is not null and " + where_str + " " +
                        "group by c._id, a.io_id, c._name, b._operation, a._value, a._status " +
                        ") b on a._id = b._id " +
                        "where " + where_str_sub + " " +
                        "group by a._name order by a._name ";

        var dt_max_str = "SELECT a._name,max(b.maxValue) as maxValue FROM TSMCDB.dbo.v_DeviceSetting a " +
                        "Left join( " +
                        "SELECT c._id, a.io_id, c._name, b._operation, max(a.datetime) as datetime, a._value, a._status as maxValue " +
                        "FROM( " + union_max + " ) AS a " +
                        "LEFT OUTER JOIN TSMCDB.dbo.s_DeviceIOPoint AS b ON a.io_id = b._id " +
                        "LEFT OUTER JOIN TSMCDB.dbo.v_DeviceSetting AS c ON b.deviceID = c._id " +
                        "LEFT OUTER JOIN TSMCDB.dbo.s_DeviceIOPoint_Ratio AS d ON c.Category = d.Category AND c.subCategory = d.subCategory AND b._operation = d._operation " +
                        "inner join( " +
                        "SELECT a.io_id, max(convert(float, a._value)) as maxValue " +
                        "FROM( " + union_max + " ) AS a " +
                        "group by a.io_id " +
                        ") e on a.io_id = e.io_id and a._value = e.maxValue " +
                        "where a._status is not null and " + where_str + " " +
                        "group by c._id, a.io_id, c._name, b._operation, a._value, a._status " +
                        ") b on a._id = b._id " +
                        "where " + where_str_sub + " " +
                        "group by a._name order by a._name ";

        var dt_min = db.query(dt_min_str);
        var dt_max = db.query(dt_max_str);

        var report = new List<Report_Meter>();

        for (var i = 0; i < dt_min.Rows.Count; i++)
        {
            var row = new Report_Meter();
            row._name = dt_min.Rows[i]["_name"].ToString();
            row.minValue = dt_min.Rows[i]["maxValue"].ToString();
            row.maxValue = dt_max.Rows[i]["maxValue"].ToString();

            var minValue = Convert.ToDouble(row.minValue.Replace("度", "").Replace("m3", ""));
            var maxValue = Convert.ToDouble(row.maxValue.Replace("度", "").Replace("m3", ""));
            if (maxValue < minValue)
            {
                row.maxValue = row.minValue;
                maxValue = minValue;
            }

            row.diffValue = Math.Round(maxValue - minValue, 2).ToString() + ((m_type == "wm") ? "m3" : "度");
            report.Add(row);
        }

        //xlsx 檔案位置
        string FilePath = Session["webPath"].ToString() + Session["webName"].ToString() + @"\download\" + DateTime.Now.ToString("yyyyMMddHHmmss") + @".xlsx";
        //FilePath = @"D:\NTUST\CenterWEB\Center\Center\download\" + DateTime.Now.ToString("yyyyMMddHHmmss") + @".xlsx";

        //建立 xlxs 轉換物件
        Export.XSLXHelper helper = new Export.XSLXHelper();
        //取得轉為 xlsx 的物件
        var xlsx = new XLWorkbook();

        xlsx = helper.Export(report);

        //存檔至指定位置
        xlsx.SaveAs(FilePath);

        //string path = @"D:\NTUST\CenterWEB\Center\Center\download\";
        string FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + @".xlsx";

        return new
        {
            FilePath
        };
    }

    public class Report_Meter
    {
        [Description("設備名稱")]
        public string _name { get; set; }
        [Description("上月紀錄")]
        public string minValue { get; set; }
        [Description("本月紀錄")]
        public string maxValue { get; set; }
        [Description("本月累計")]
        public string diffValue { get; set; }

    }

    [WebMethod]
    public List<Role> xml_test()
    {
        var dt = db.query("SELECT r_id, r_name, r_describe, r_level FROM s_Role where valid = 1");

        var roles = new List<Role>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var rows = new Role();
            rows.r_id = dt.Rows[i]["r_id"].ToString();
            rows.r_name = dt.Rows[i]["r_name"].ToString();
            rows.r_describe = dt.Rows[i]["r_describe"].ToString();
            rows.r_level = dt.Rows[i]["r_level"].ToString();

            roles.Add(rows);
        }

        //Book book = new Book("xml", 1);

        // 建立儲存的檔案
        //FileStream fs = new FileStream(@"D:\Ollie\DateTime.XML", FileMode.Create);

        // 以序列化物件的型別為參數，建立 XmlSerializer 物件
        //XmlSerializer xSerializer = new XmlSerializer(typeof(List<Role>));

        // 執行 XmlSerializer.Serialize
        //xSerializer.Serialize(fs, roles);

        return roles;
    }

    public class Book
    {
        public string title;
        public int No;
        public Book(string title_, int no_)
        {
            title = title_;
            No = no_;
        }
        public Book()
        {
        }
    }

    [WebMethod]
    public string change_sensor_mode_kind(string io_id, string mode, string kind)
    {
        var disconnected = db.query("select b._id from s_DeviceIOPoint a join Alarm_Detect b on a.deviceID = b.deviceID where a._id = " + io_id + " and b.detected = 1");
        if (disconnected.Rows.Count > 0)
            return "感測器連線中斷，請修復連線後再試";

        var command_send = 0;
        command_send = db.excute("update s_MapIO_mapping set kind = " + kind + " where DeviceIOPoint_id = " + io_id);

        if (command_send > 0)
            return "指令送出成功！";

        return "指令送出失敗，請聯繫管理員";
    }

    [WebMethod]
    public string change_sensor_mode(string io_id, string mode,string kind)
    {
        var disconnected = db.query("select b._id from s_DeviceIOPoint a join Alarm_Detect b on a.deviceID = b.deviceID where a._id = " + io_id + " and b.detected = 1");
        if (disconnected.Rows.Count > 0)
            return "感測器連線中斷，請修復連線後再試";

        var command_send = 0;
        command_send += db.excute("update s_DeviceIOPoint set _status = " + mode + ", [confirm] = 0, utime = getdate() where _id = " + io_id);
        if (command_send > 0)
            command_send += db.excute("update s_MapIO_mapping set _value = " + mode + " where DeviceIOPoint_id = " + io_id);

        if (command_send > 1)
            return "指令送出成功！";

        return "指令送出失敗，請聯繫管理員";
    }

    [WebMethod]
    public string change_led_enable(string io_id, string color,string status)
    {
        var disconnected = db.query("select b._id from s_DeviceIOPoint a join Alarm_Detect b on a.deviceID = b.deviceID where a._id = " + io_id + " and b.detected = 1");
        if (disconnected.Rows.Count > 0)
            return "感測器連線中斷，請修復連線後再試";
        var command_send = 0;
        command_send = db.excute("update s_LED_Group_Mapping set _enable=1 where _group like '"+ color + "%' and io_id = " + io_id);
        command_send +=db.excute("update s_MapIO_mapping set location='" + status + "' where DeviceIOPoint_id = '" + io_id + "' ");
        if (command_send > 0)
            return "指令送出成功！";
        return "指令送出失敗，請聯繫管理員";
    }

    [WebMethod]
    public string change_led_disable(string io_id, string color)
    {
        var disconnected = db.query("select b._id from s_DeviceIOPoint a join Alarm_Detect b on a.deviceID = b.deviceID where a._id = " + io_id + " and b.detected = 1");
        if (disconnected.Rows.Count > 0)
            return "感測器連線中斷，請修復連線後再試";
        var command_send = 0;
        command_send = db.excute("update s_LED_Group_Mapping set _enable=0 where _group like '" + color + "%' and io_id = " + io_id);
        if (command_send > 0)
            return "指令送出成功！";
        return "指令送出失敗，請聯繫管理員";
    }

    [WebMethod]
    public string change_count_group_color(string io_id, string _status)
    {
        var disconnected = db.query("select b._id from s_DeviceIOPoint a join Alarm_Detect b on a.deviceID = b.deviceID where a._id = " + io_id + " and b.detected = 1");
        if (disconnected.Rows.Count > 0)
            return "感測器連線中斷，請修復連線後再試";

        var groups = new string[13] { "不分區", "紅區標準", "紅區彈性", "黃區標準", "黃區彈性", "綠區標準", "綠區彈性", "婦女車位", "無障礙車位", "不計數", "主管車位", "藍區標準", "藍區彈性" };
        var _group = "";

        for (var i = 0; i < groups.Count(); i++)
        {
            if (i.ToString() == _status)
                _group = groups[i];
        }

        var command_send = 0;
        command_send += db.excute("update s_DeviceIOPoint set _status = " + _status + ", v_status = " + _status + ", utime = getdate() where _id = " + io_id);
        if (command_send > 0)
            command_send += db.excute("update b set b._group = '" + _group + "', b.utime = getdate() from s_DeviceIOPoint a join s_DeviceIOPoint b on a.deviceID = b.deviceID and b._type = '運轉狀態' where a._id = " + io_id);
        String pattern_id = "";
        String group_id = "";
        String sql = "";
        var dt = db.query("select a.sub_id, b._id, b.deviceid, c.pattern_id, c.pattern_id_error, c.pattern_id_off, C._group " +
                          "from s_DeviceSetting a join s_DeviceIOPoint b on a._id = b.deviceID join s_DeviceMapInfo_color c on c._id = b.deviceID " +
                          "where a.valid = 1 and b._id =" + io_id);
        var roles = new List<Role>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            group_id = dt.Rows[i]["_group"].ToString();
            //1為橫向車位，2為直向車位
            if (group_id.Equals("1"))
            {
                sql = "select _id,name from u_def_Pattern where name like '" + _group.SubString(0, 2) + "(橫)" + "%' ";
                var dt1 = db.query(sql);
                if (dt1.Rows.Count > 0) { 
                    pattern_id = dt1.Rows[0]["_id"].ToString();
                }
            }
            else
            {
                sql = "select _id,name from u_def_Pattern where name like '" + _group.SubString(0, 2) + "(直)" + "%' ";
                var dt1 = db.query(sql);
                if (dt1.Rows.Count > 0)
                {
                    pattern_id = dt1.Rows[0]["_id"].ToString();
                }
            }

        }
        if (pattern_id != "")
        {
            command_send += db.excute("update c set c.pattern_id='" + pattern_id + "',c.pattern_id_error='" + pattern_id + "',c.pattern_id_off='" + pattern_id + "' from s_DeviceSetting a join s_DeviceIOPoint b on a._id = b.deviceID join s_DeviceMapInfo_color c on c._id = b.deviceID where a.valid = 1 and b._operation = '計數群組' and b._id = " + io_id);
        }
        if (command_send > 1)
            return "指令送出成功！";

        return "指令送出失敗，請聯繫管理員";
    }

    [WebMethod]
    public string change_count_group(string io_id, string _status)
    {
        var disconnected = db.query("select b._id from s_DeviceIOPoint a join Alarm_Detect b on a.deviceID = b.deviceID where a._id = " + io_id + " and b.detected = 1");
        if (disconnected.Rows.Count > 0)
            return "感測器連線中斷，請修復連線後再試";

        var groups = new string[13] { "不分區", "紅區標準", "紅區彈性", "黃區標準", "黃區彈性", "綠區標準", "綠區彈性", "婦女車位", "無障礙車位", "不計數","主管車位","藍區標準","藍區彈性"};
        var _group = "";

        for (var i = 0; i < groups.Count(); i++)
        {
            if (i.ToString() == _status)
                _group = groups[i];
        }

        var command_send = 0;
        command_send += db.excute("update s_DeviceIOPoint set _status = " + _status + ", v_status = " + _status + ", utime = getdate() where _id = " + io_id);
        if (command_send > 0)
            command_send += db.excute("update b set b._group = '" + _group + "', b.utime = getdate() from s_DeviceIOPoint a join s_DeviceIOPoint b on a.deviceID = b.deviceID and b._type = '運轉狀態' where a._id = " + io_id);
        String pattern_id = "";
        String group_id = "";
        String sql = "";
        var dt = db.query("select a.sub_id, b._id, b.deviceid, c.pattern_id, c.pattern_id_error, c.pattern_id_off, C._group " +
                          "from s_DeviceSetting a join s_DeviceIOPoint b on a._id = b.deviceID join s_DeviceMapInfo_color c on c._id = b.deviceID " +
                          "where a.valid = 1 and b._id =" + io_id);
        var roles = new List<Role>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            group_id = dt.Rows[i]["_group"].ToString();
            //1為橫向車位，2為直向車位
            if (group_id.Equals("1"))
            {
                sql = "select _id,name from u_def_Pattern where name like '" + _group.SubString(0, 2) + "(橫)" + "%' ";
                var dt1 = db.query(sql);
                if (dt1.Rows.Count > 0)
                {
                    pattern_id = dt1.Rows[0]["_id"].ToString();
                }
            }
            else
            {
                sql = "select _id,name from u_def_Pattern where name like '" + _group.SubString(0, 2) + "(直)" + "%' ";
                var dt1 = db.query(sql);
                if (dt1.Rows.Count > 0)
                {
                    pattern_id = dt1.Rows[0]["_id"].ToString();
                }
            }

        }
        if (pattern_id != "")
        {
            command_send += db.excute("update c set c.pattern_id='" + pattern_id + "',c.pattern_id_error='" + pattern_id + "',c.pattern_id_off='" + pattern_id + "' from s_DeviceSetting a join s_DeviceIOPoint b on a._id = b.deviceID join s_DeviceMapInfo_color c on c._id = b.deviceID where a.valid = 1 and b._operation = '計數群組' and b._id = " + io_id);
        }
        if (command_send > 1)
            return "指令送出成功！";

        return "指令送出失敗，請聯繫管理員";
    }

    [WebMethod]
    public object GetCarAmount()
    {
        var main = db.query("SELECT [floor], [_space], [total] FROM [v_Main_Page]");
        var data = new List<MainPageData>();

        for (var i = 0; i < main.Rows.Count; i++)
        {
            var d = new MainPageData();
            d.floor = main.Rows[i]["floor"].ToString();
            d.amount = main.Rows[i]["_space"].ToString();
            d.totalamount = main.Rows[i]["total"].ToString();

            data.Add(d);
        }

        return data;
    }

    public class MainPageData
    {
        public string floor { get; set; }
        public string amount { get; set; }
        public string totalamount { get; set; }
    }
}