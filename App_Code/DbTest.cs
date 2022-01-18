using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// DBAcess 的摘要描述
/// </summary>
public class DbTest
{
    static string Web_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["WebConnectionStringTest"].ConnectionString;

    public DbTest()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    public DataTable query(string cmd)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Web_ConnectionString;
                SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public int excute(string sql)
    {
        int result = 0;
        try
        {
            using (SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = Web_ConnectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();

            }                        
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {            
        }
        return result;
    }


    public object executeScalar( string sql ) {
        object obj = null;
        try
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Web_ConnectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                obj = cmd.ExecuteScalar();
                cmd.Dispose();
                return obj;
            }
        }
        catch (Exception ex)
        {
            return obj;
        }
    }

    public string getRoleID(string username) {
        object result;
        string sqlcmd = "SELECT r_id FROM [Permissions] WHERE UserName = '" + username + "' ";
        try
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Web_ConnectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                result = cmd.ExecuteScalar();
                cmd.Dispose();

            }
        }
        catch (Exception ex)
        {
            return "" ;
        }
        finally
        {
        }

        return ((Guid)result).ToString();
        
    }


    public void setSystemOperate( string username, string action, string pageName ) {
        try
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Web_ConnectionString;
                conn.Open();
                string sql = "INSERT INTO SystemOperate ( add_user, instruction ,function_name ) VALUES( @Name, @Action, @FunctionName ) ";
                using (SqlCommand cmd = new SqlCommand(sql, conn)) {
                    cmd.Parameters.Add(new SqlParameter( "Name", username ));
                    cmd.Parameters.Add(new SqlParameter("Action", action));
                    cmd.Parameters.Add(new SqlParameter("FunctionName", pageName));
                    cmd.ExecuteNonQuery();
                }
                
                
            }
        }
        catch (Exception ex)
        {            
        }
        finally
        {
        }
    }

    public void setSystemOperate(string username, string action, string pageName, string par)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Web_ConnectionString;
                conn.Open();
                string sql = "INSERT INTO SystemOperate ( add_user, instruction ,function_name,parmList ) VALUES( @Name, @Action, @FunctionName, @Par ) ";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("Name", username));
                    cmd.Parameters.Add(new SqlParameter("Action", action));
                    cmd.Parameters.Add(new SqlParameter("FunctionName", pageName));
                    cmd.Parameters.Add(new SqlParameter("Par", par));
                    cmd.ExecuteNonQuery();
                }


            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }

    public void setSystemOperate( string system,  string username, string action, string pageName, string par)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Web_ConnectionString;
                conn.Open();
                string sql = "INSERT INTO SystemOperate ( sys,add_user, instruction ,function_name,parmList ) VALUES( @sys , @Name, @Action, @FunctionName, @Par ) ";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("sys", system));
                    cmd.Parameters.Add(new SqlParameter("Name", username));
                    cmd.Parameters.Add(new SqlParameter("Action", action));
                    cmd.Parameters.Add(new SqlParameter("FunctionName", pageName));
                    cmd.Parameters.Add(new SqlParameter("Par", par));
                    cmd.ExecuteNonQuery();
                }


            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }

    //警示車牌
    public int InsertAlarmPlate(string licenseplate, string Info1 )
    {
        int result = 0;

        try
        {
            using (SqlConnection connection = new SqlConnection(Web_ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(
                "INSERT INTO AlarmLicenseplate( Car_Licenseplate , Car_Info1 ) VALUES( @licenseplate, @Car_Info1 ) ", connection))
                {
                    //
                    // Add new SqlParameter to the command.
                    //
                    command.Parameters.Add(new SqlParameter("licenseplate", licenseplate));
                    command.Parameters.Add(new SqlParameter("Car_Info1", Info1));

                    result = command.ExecuteNonQuery();

                }
            }
        }
        catch (SqlException ex) {
            result = 0;
        }

        return result;
    }

    public int DelAlarmPlate(string licenseplate)
    {
        int result = 0;

        try
        {
            using (SqlConnection connection = new SqlConnection(Web_ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(
                "DELETE AlarmLicenseplate WHERE Car_Licenseplate = @licenseplate ", connection))
                {
                    //
                    // Add new SqlParameter to the command.
                    //
                    command.Parameters.Add(new SqlParameter("licenseplate", licenseplate));

                    result = command.ExecuteNonQuery();

                }
            }
        }
        catch (SqlException ex)
        {
            result = 0;
        }

        return result;
    }


    //RFID Card Number 
    public static bool checkCard(string CardNo) {
        bool ret = false;
        try
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Web_ConnectionString;
                conn.Open();
                string sql = "SELECT count(*) FRom RFID_Card WHERE CardNo = @CardNo";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("CardNo", CardNo));                    
                    int r = (int)cmd.ExecuteScalar();
                    if (r > 0)
                    {
                        ret = true;
                    }
                }
                

            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }

        return ret;
    }

    public static bool InsertCardNo(string CardNo) {

        int ret = 0;

        try
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Web_ConnectionString;
                conn.Open();
                string sql = "INSERT INTO RFID_Card( CardNo ) VALUES( @CardNo )";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("CardNo", CardNo));
                    ret = cmd.ExecuteNonQuery();
                    
                }


            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }

        if (ret > 0)
            return true;
        else
            return false;

    }

    public  bool checkPermission(string rid, string fid)
    {

        int ret = 0;

        try
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Web_ConnectionString;
                conn.Open();
                string sql = "SELECT count(*) FROM RF_mapping WHERE operate_enable = 1 AND f_id = @f_id AND r_id = @r_id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@f_id", fid));
                    cmd.Parameters.Add(new SqlParameter("@r_id", rid));
                    ret = (int)cmd.ExecuteScalar();

                }
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }

        if (ret > 0)
            return true;
        else
            return false;

    }

}