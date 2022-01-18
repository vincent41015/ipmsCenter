using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using Newtonsoft.Json;

/// <summary>
/// WebService_Power 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
[System.Web.Script.Services.ScriptService]
public class WebService_Power : System.Web.Services.WebService {


    DBAcess db = new DBAcess();
    public WebService_Power () {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    public class PowerReportData
    {
        public string dtime;
        public string _value1;

        public PowerReportData( string dtime, string _value1 )
        {
            this.dtime = dtime;
            this._value1 = _value1;            
        }
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod(EnableSession = true)]
    public List<object> GetPowerReport()
    {
        List<object> iData = new List<object>();

        List<string> labelSets = new List<string>();
        List<float>  dataSets = new List<float>();
        List<float> dataSets2 = new List<float>();
        
        string query1 = "select * FROM (SELECT top 10 dtime, _value1 FROM Power_Report_1 order by dtime desc ) as t order by dtime  ";
        
        DataTable dt = db.query(query1);
        foreach (DataRow drow in dt.Rows)
        {
            labelSets.Add( drow["dtime"].ToDateTime().ToString( "yyyy-MM-dd HH" ) );

            float ff= drow["_value1"].ToSingle();

            if (ff > 100)
            {
                dataSets2.Add(100);
                dataSets.Add(ff - 100);
            }
            else {
                dataSets2.Add(ff);
                dataSets.Add(0);
            }
            
            
        }
        iData.Add(labelSets);
        iData.Add(dataSets);
        iData.Add(dataSets2);
        return iData;

        
    }


}
