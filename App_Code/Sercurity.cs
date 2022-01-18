using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Sercurity 的摘要描述
/// </summary>
public static class Sercurity
{
    static DBAcess db = new DBAcess();

    //public Sercurity()
    //{
    //    //
    //    // TODO: 在此加入建構函式的程式碼
    //    //

        
    //}

    public static DataTable LeftMenu(string rid)
    {
        DataTable dt = new DataTable();

        return dt;
    }

    public static DataTable LeftMenu(string rid, string currentMenuId)
    {
        DataTable dt = new DataTable();

        return dt;
    }

    public static DataTable RightMenu(string rid)
    {
        DataTable dt = new DataTable();

        return dt;
    }

    public static bool CheckPermission( string rid, string Category ) {
        bool ret = false;

        #region 檢查權限
        DataTable tempDT = db.query("select distinct b.FGroup from s_RF_mapping a left join  s_Functions b ON a.f_id = b.FId where operate_enable = 1 AND a.r_id = '"+rid+"'");

        foreach (DataRow row in tempDT.Rows) {
            if (Category.Equals(row["FGroup"].ToString())) {
                ret = true;
                break;
            }
        }        
        #endregion

        return ret;
    }

    public static bool CheckPermission2(string rid, string Category)
    {
        bool ret = false;

        #region 檢查權限
        DataTable tempDT = db.query("select f_id, r_id from s_RF_mapping  where operate_enable = 1 AND r_id = '" + rid + "' AND f_id = '"+Category+"'");

        if (tempDT.Rows.Count > 0) {
            ret = true;
        }

        #endregion

        return ret;
    } 



}