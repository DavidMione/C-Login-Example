using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.IO;
using System.Text;

namespace Login_Code_Example.AppData
{
    public class DataLayer
    {
        //this datalayer will ustilize data set, data adapter, sql connection, sql command
        SqlDataAdapter dapt;
        SqlCommand cmd;
        DataSet ds;
        SqlConnection conn;

        public DataLayer()
        {
            Initialize();
        }

        public void Initialize()
        {
            //make sure of database connectivity
            //opportunity to troubleshoot issues with the database connection using Exception
            try
            {
                string DBString = ConfigurationManager.ConnectionStrings["connectionStringName"].ConnectionString;
                conn = new SqlConnection(DBString);
            }

            catch (Exception ex)
            {
                return;
            }
        }

        //checks to see if the user exists
        public DataSet Verify_User_Login(string email, string pswd)
        {
            dapt = new SqlDataAdapter("Verify_User_Login", conn);
            dapt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dapt.SelectCommand.Parameters.AddWithValue("emailaddress", email);
            dapt.SelectCommand.Parameters.AddWithValue("password", pswd);
            ds = new DataSet();
            try
            {
                dapt.SelectCommand.Prepare();
                dapt.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {                    
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        //calls up the minimal user information as a dataset
        public DataSet Get_UserID_by_Email(string email)
        {
            dapt = new SqlDataAdapter("Get_UserID_by_Email", conn);
            dapt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dapt.SelectCommand.Parameters.AddWithValue("@email", email);
            ds = new DataSet();
            try
            {
                dapt.SelectCommand.Prepare();
                dapt.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        //this will create the user session record. 
        //The user session is deleted after use by a database stored procedure.
        public int User_Session_Create(int userid, string sess)
        {
            int recordsAffected = 0;
            cmd = new SqlCommand("User_Session_Insert", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@userSession", sess);
            try
            {
                conn.Open();
                cmd.Prepare();
                recordsAffected = (int)cmd.ExecuteScalar();
                if (recordsAffected > 0)
                {
                    return recordsAffected;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        //this will load the user info you want to use for display purposes in the user or admin section
        //you can display some key info to give a verification to the user
        public DataSet Load_Complete_User_by_ID(int userid)
        {
            dapt = new SqlDataAdapter("Load_Complete_User_by_ID", conn);
            dapt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dapt.SelectCommand.Parameters.AddWithValue("@userid", userid);
            ds = new DataSet();
            try
            {
                dapt.SelectCommand.Prepare();
                dapt.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        //this is a check to see if the user and session exist
        public int Get_User_By_Session(string sess)
        {
            DataSet ds = new DataSet();
            string useridString = string.Empty;
            try
            {
                if (sess.Length > 10)
                {
                    ds = Load_User_By_Session(sess);
                    if (ds != null)
                    {
                        useridString = ds.Tables[0].Rows[0]["UserID"].ToString();
                        return Convert.ToInt32(useridString);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return -2;
                }
            }
            catch (Exception ex)
            {
                return -5;
            }
        }

        //this will load the user with session info for verification and redirection
        public DataSet Load_User_By_Session(string sess)
        {
            dapt = new SqlDataAdapter("Load_User_By_Session", conn);
            dapt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dapt.SelectCommand.Parameters.AddWithValue("@sess", sess);
            ds = new DataSet();
            try
            {
                dapt.SelectCommand.Prepare();
                dapt.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        //a check to verify that the user exists in our records
        public string Verify_User_Belongs(int userid, int role)
        {
            DataSet ds = new DataSet();
            string email = string.Empty;
            try
            {
                ds = Verify_User(userid, role);
                if (ds != null)
                {
                    email = ds.Tables[0].Rows[0]["emailAddress"].ToString();
                    return email;
                }
                else
                {
                    return "Unknown";
                }
            }
            catch
            {
                return "Unknown";
            }
        }

        //a check on the role of the user
        public DataSet Verify_User(int userid, int role)
        {
            dapt = new SqlDataAdapter("Verify_User", conn);
            dapt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dapt.SelectCommand.Parameters.AddWithValue("userid", userid);
            dapt.SelectCommand.Parameters.AddWithValue("role", role);

            ds = new DataSet();
            try
            {
                dapt.SelectCommand.Prepare();
                dapt.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ex = null;
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        //this function will close the user session and logout the user
        //the user can then be redirected and will have to log in again
        public bool User_Logout_(string sess)
        {
            int recordsAffected = 0;
            cmd = new SqlCommand("User_Logout", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@usersession", sess);
            try
            {
                conn.Open();
                cmd.Prepare();
                recordsAffected = (int)cmd.ExecuteNonQuery();
                if (recordsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}