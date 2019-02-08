using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Security;
using System.Web.Security;

namespace Login_Code_Example
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //to ensure the user doesn't have to cursor over to the email textbox for convenience
            //if (!IsPostBack)
            //    SetFocus(txtEmailAddress);
        }

        //will clear the on screen textboxes
        //and reset the focus back to the email textbox
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtEmailAddress.Text = string.Empty;
            txtPassword.Text = string.Empty;
            SetFocus(txtEmailAddress);
        }

        //this function will use the user's email, create the sessionID, 
        //and interact to verify the validity of login information provided
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = string.Empty;
            email = txtEmailAddress.Text.Trim();

            string sess = string.Empty;
            sess = Page.Session.SessionID.ToString();

            int userid = 0;
            int userSessionID = 0;
            string path = string.Empty;
                        
            AppData.DataLayer DL = new AppData.DataLayer();

            //to make sure the user exists and is using the correct password before proceeding further
            if (Verify_User_Login())
            {
                //
                userid = Get_User_ID(email);
                if (userid > 0)
                {
                    userSessionID = DL.User_Session_Create(userid, sess);
                    if (userSessionID > 0)
                    {
                        Redirect_User(userid, sess);
                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx", false);
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx", false);
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx", false);
            }


        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SwitchViewSignUP();", true);
            
        }

        //this will use the variables email and password separately from the rest of the functions
        private bool Verify_User_Login()
        {
            AppData.DataLayer DL = new AppData.DataLayer();
            string email = string.Empty;
            string password = string.Empty;
            email = txtEmailAddress.Text.Trim();            
            password = txtPassword.Text.Trim();
            string encrypedpswrd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
            DataSet DS = new DataSet();
            try
            {
                DS = DL.Verify_User_Login(email, encrypedpswrd);
                if (DS != null)
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

        //after verificatoin the user information can be obtained
        //is a further wave of verification
        private int Get_User_ID(string email)
        {
            AppData.DataLayer DL = new AppData.DataLayer();
            DataSet DS = new DataSet();
            string stringUserID = string.Empty;
            int userid = 0;
            try
            {
                DS = DL.Get_UserID_by_Email(email);
                if (DS != null)
                {
                    stringUserID = DS.Tables[0].Rows[0]["UserID"].ToString();
                    userid = Convert.ToInt32(stringUserID);

                    return userid;
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

        //once the user's role is verified they are passed to the correct area with a session id
        private void Redirect_User(int userid, string sess)
        {
            DataSet DS = new DataSet();
            AppData.DataLayer DL = new AppData.DataLayer();
            string roleString = string.Empty;
            string path = string.Empty;
            int role = -1;
            try
            {
                DS = DL.Load_Complete_User_by_ID(userid);
                if (DS != null)
                {
                    roleString = DS.Tables[0].Rows[0]["Role"].ToString();
                    role = Convert.ToInt32(roleString);
                    if (role == 0)
                    {
                        path = "~/AdminZone/Default.aspx?SessionID=";
                        path = path + sess;
                    }
                    else if (role == 1)
                    {
                        path = "~UserZone/Default.aspx?SessionID=";
                        path = path + sess;
                    }
                    else
                    {
                        path = "~/Default.aspx";
                    }
                    Response.Redirect(path);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSubmitCreateAccount_Click(object sender, EventArgs e)
        {

        }        
    }
}