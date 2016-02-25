using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using DA_SKORKEL;


public partial class ForgetPassword : System.Web.UI.Page
{
    DA_Login objLoginDB = new DA_Login();
    DO_Login objLogin = new DO_Login();
    CryptoGraphy objEncrypt = new CryptoGraphy();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserName.Text = "";            
        }
    }

    protected void lnkGetPassword_Click(object sender, EventArgs e)
    {
        if (UserName.Text.Trim() != "")
        {
            lblMessage.Text = "";
            objLogin.Username = UserName.Text.Trim();
            if (objLogin.Username.Length <= 0)
                return;
            lnkGetPassword.Visible = false;
            DataTable dt = new DataTable();
            dt = objLoginDB.GetDataSet(objLogin, DA_Login.Login_1.ForgotPassword);

            if (dt.Rows.Count > 0)
            {
                lnkGetPassword.Visible = false;
                UserName.Visible = false;
                lblErrorMsg.Text = "";
                if (Convert.ToString(dt.Rows[0]["intUserTypeID"]).Trim() != "3")
                {
                    SendMail(UserName.Text.Trim().ToString(), dt.Rows[0]["strSrcklID"].ToString(), dt.Rows[0]["LoginName"].ToString(), 'a', Convert.ToString(objEncrypt.Decrypt(dt.Rows[0]["vchrPassword"].ToString())), "");
                    UserName.Text = "";
                }
                else
                {
                    SendMail(UserName.Text.Trim().ToString(), dt.Rows[0]["strSrcklID"].ToString(), dt.Rows[0]["InstituteName"].ToString(), 'a', Convert.ToString(objEncrypt.Decrypt(dt.Rows[0]["vchrPassword"].ToString())), "");
                    UserName.Text = "";
                }

            }
            else
            {
                lnkGetPassword.Visible = true;
                UserName.Visible = true;
                lblErrorMsg.Text = "Invalid Login Id.";
                lblErrorMsg.CssClass = "RedErrormsg";
            }
        }
        else
        {
            lblErrorMsg.Text = "Please enter email-Id.";
            lblErrorMsg.CssClass = "RedErrormsg";
        }
    }

    private void SendMail(string To, string Link, string Name, char flag, string Details, string DistName)
    {
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            string mailfrom = ConfigurationManager.AppSettings["mailfrom"];
            string mailServer = ConfigurationManager.AppSettings["mailServer"];
            string username = ConfigurationManager.AppSettings["UserName"];
            string Password = ConfigurationManager.AppSettings["Password"];
            string Port = ConfigurationManager.AppSettings["Port"];
            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailSSL = ConfigurationManager.AppSettings["MailSSL"];
            string MailTo = To;
            SmtpClient clientip = new SmtpClient(mailServer);

            clientip.Port = Convert.ToInt32(Port);
            NetworkCredential cre = new NetworkCredential(username, Password);
            clientip.UseDefaultCredentials = false;
            if(MailSSL!="0")
            clientip.EnableSsl = true;
            clientip.Credentials = cre;
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];
            try
            {
                string Topic = string.Empty;
                string Description = string.Empty;
                string empid = string.Empty;
                string strDetails = string.Empty;
                strDetails = "<tr><td><br>Account Details</td></tr>";

                string ReportString = "";
                string str = " <table style='font-family:Calibri; font-size:16px' border='0'>";
                ReportString += "<tr><td >Hi " + Name + ", <br><br> You recently requested for a password.<br>" +
                "Please keep the following information safely.<br></td></tr>" +
                "<tr><td><b><br> Login Information</b><br><br></td></tr>" +
                "<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;Password is : " + Details + "<br></td></tr> " +
                "<tr><td><br>Regards, <br>Skorkel Team</td></tr> ";// +
                ReportString += "<tr><td><br>****This is a system generated message. Kindly do not reply****</td></tr>";
                ReportString += "</table>";
                MailMessage mm2 = new MailMessage();
                mm2.From = new System.Net.Mail.MailAddress(mailfrom, DisplayName);
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(str + ReportString, null, "text/html");
                mm2.To.Clear();
                mm2.To.Add(MailTo);
                mm2.CC.Clear();
                mm2.Subject = "Your Skorkel Account Details";
                mm2.AlternateViews.Add(htmlView);
                mm2.IsBodyHtml = true;
                clientip.Send(mm2);
                mm2.To.Clear();
                pMsg.Style.Add("display", "block");
                lblMessage.Text = "Your account details has been mailed to you.";
                lblMessage.CssClass = "GreenErrormsg";
            }
            catch (SmtpException ex)
            {
                pMsg.Style.Add("display", "block");
                lblMessage.Text = ex.Message;
                lblMessage.CssClass = "RedErrormsg";
                return;
            }
            finally
            {
                conn.Close();
            }
    }
}