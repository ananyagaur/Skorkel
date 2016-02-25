using System;
using System.Web;
using System.Data;
using DA_SKORKEL;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Text;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;
using System.Collections.Specialized;
using Matlus.FederatedIdentity;
using System.IO;

public partial class SignUp1 : System.Web.UI.Page
{
    private oAuthLinkedIn _oauth = new oAuthLinkedIn();
    DO_Registrationdetails objRegistration = new DO_Registrationdetails();
    DA_Registrationdetails objRegistrationDB = new DA_Registrationdetails();

    DataTable dt = new DataTable();
    DataRow row;

    DA_Login objLoginDB = new DA_Login();
    DO_Login objLogin = new DO_Login();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];

    string GoogleClientId = ConfigurationManager.AppSettings["GoogleClientId"];
    string GoogleClientSecret = ConfigurationManager.AppSettings["GoogleClientSecret"];
    string GoogleRedirectUrl = ConfigurationManager.AppSettings["GoogleRedirectUrl"];
    string GoogleMailUrl = ConfigurationManager.AppSettings["GoogleMailUrl"];

    protected void Page_Load(object sender, EventArgs e)
    {
        string oauth_token = Request.QueryString["oauth_token"];
        string oauth_verifier = Request.QueryString["oauth_verifier"];

        if (oauth_token != null && oauth_verifier != null)
        {
            Application["oauth_token"] = oauth_token;
            Application["oauth_verifier"] = oauth_verifier;
            Response.Redirect("LinkedInAccountDetails.aspx?oauth_verifier=" + oauth_verifier + "");
        }

        if (!IsPostBack)
        {
            form1.Action = QueryStringModule.Encrypt(Request.Url.Query.ToString().Replace("?", ""));
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != null)
            {
                //After signup activate user account
                string RegOutId = Request.QueryString["id"];
                ViewState["RegOutId"] = RegOutId;
                activate();
                GetUserEmailId();
            }
        }

    }

    private void activate()
    {
        if (Convert.ToInt32(ViewState["RegOutId"]) != 0)
            objRegistration.RegistrationId = Convert.ToInt32(ViewState["RegOutId"]);

        objRegistrationDB.AddEditDel_Profile(objRegistration, DA_Registrationdetails.RegistrationDetails.InstituteNameStuProfList);
    }

    protected void GetUserEmailId()
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        dt = new DataTable();
        objLogin.intRegistartionID = Convert.ToInt32(ViewState["RegOutId"]);
        dt = objLoginDB.GetDataTable(objLogin, DA_SKORKEL.DA_Login.Login_1.UserDetails);

        if (dt.Rows.Count > 0)
        {
            UserSession.UserInfo UInfo = new UserSession.UserInfo();
            string LoginName = Convert.ToString(dt.Rows[0]["LoginName"]);
            UInfo.UserName = Convert.ToString(dt.Rows[0]["vchrUserName"]);
            UInfo.UserID = Convert.ToInt64(dt.Rows[0]["intRegistrationId"]);
            int TypeId = Convert.ToInt32(dt.Rows[0]["intUserTypeID"]);

            if (ISAPIURLACCESSED == "1")
            {
                String UserURL = APIURL + "registerUser.action?" +
                                        "uid=" + Convert.ToString(dt.Rows[0]["intRegistrationId"]) +
                                        "&userId=" + Convert.ToString(dt.Rows[0]["vchrUserName"]) +
                                        "&password=" + Convert.ToString(dt.Rows[0]["vchrPassword"]) +
                                        "&firstName=" + Convert.ToString(dt.Rows[0]["vchrFirstName"]) +
                                        "&lastName=" + Convert.ToString(dt.Rows[0]["vchrLastName"]) +
                                        "&userType=STUDENT" +
                                        "&userContextIds=" + null +
                                        "&friendUserIds=" + null +
                                        "&lawRelated=" + null;
                try
                {

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();

                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();

                    objAPILogDO.strURL = UserURL;
                    objAPILogDO.strAPIType = "Student";
                    objAPILogDO.strResponse = result;

                    if (ip == null)
                        objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
                catch (Exception ex)
                { ex.Message.ToString(); }
            }
            Session.Add("UserTypeId", TypeId);
            Session.Add("UInfo", UInfo);
            Session.Add("LoginName", LoginName);
            Session.Add("ExternalUserId", Convert.ToString(dt.Rows[0]["intRegistrationId"]));
            Response.Redirect("Home.aspx");
        }
    }

    protected void BindDataTable()
    {
        dt = new DataTable();

        DataColumn UserTypeId = new DataColumn();
        UserTypeId.DataType = System.Type.GetType("System.String");
        UserTypeId.ColumnName = "UserTypeId";
        dt.Columns.Add(UserTypeId);

        DataColumn intLawRelated = new DataColumn();
        intLawRelated.DataType = System.Type.GetType("System.String");
        intLawRelated.ColumnName = "intLawRelated";
        dt.Columns.Add(intLawRelated);

        DataColumn vchrRegistartionType = new DataColumn();
        vchrRegistartionType.DataType = System.Type.GetType("System.String");
        vchrRegistartionType.ColumnName = "vchrRegistartionType";
        dt.Columns.Add(vchrRegistartionType);

        DataColumn strOthers = new DataColumn();
        strOthers.DataType = System.Type.GetType("System.String");
        strOthers.ColumnName = "strOthers";
        dt.Columns.Add(strOthers);

        DataColumn InstituteName = new DataColumn();
        InstituteName.DataType = System.Type.GetType("System.String");
        InstituteName.ColumnName = "InstituteName";
        dt.Columns.Add(InstituteName);

        DataColumn FirstName = new DataColumn();
        FirstName.DataType = System.Type.GetType("System.String");
        FirstName.ColumnName = "FirstName";
        dt.Columns.Add(FirstName);

        DataColumn MiddleName = new DataColumn();
        MiddleName.DataType = System.Type.GetType("System.String");
        MiddleName.ColumnName = "MiddleName";
        dt.Columns.Add(MiddleName);

        DataColumn LastName = new DataColumn();
        LastName.DataType = System.Type.GetType("System.String");
        LastName.ColumnName = "LastName";
        dt.Columns.Add(LastName);

        DataColumn UserName = new DataColumn();
        UserName.DataType = System.Type.GetType("System.String");
        UserName.ColumnName = "UserName";
        dt.Columns.Add(UserName);

        DataColumn Password = new DataColumn();
        Password.DataType = System.Type.GetType("System.String");
        Password.ColumnName = "Password";
        dt.Columns.Add(Password);

        DataColumn ConPassword = new DataColumn();
        ConPassword.DataType = System.Type.GetType("System.String");
        ConPassword.ColumnName = "ConPassword";
        dt.Columns.Add(ConPassword);

        ViewState.Add("UserDetails", dt);

    }

    protected void lnlNext_Click(object sender, EventArgs e)
    {
        CompareValidator1.Validate();
        lblMsgs.Text = "";

        dt = new DataTable();
        dt = (DataTable)ViewState["UserDetails"];
        if (txtUname.Text.Trim().Replace("'", "''") == "")
        {
            return;
        }
        if (txtFirstName.Text.Trim().Replace("'", "''") == ""|| txtFirstName.Text.Trim().Replace("'", "''") == "First Name")
        {
            lblMsgs.Text = "First name is required.";
            return;
        }
        if (txtLastName.Text.Trim().Replace("'", "''") == "" || txtLastName.Text.Trim().Replace("'", "''") == "Last Name")
        {
            lblMsgs.Text = "Last name is required.";
            return;
        }

        if (txtPassword.Text.Trim() == "")
        {
            return;
        }
        if (txtConPassword.Text.Trim() == "")
        {
            lblMsgs.Text = "Confirm password is required.";
            return;
        }
        if (chkIAgree.Checked == false)
        {
            lblMsgs.Text = "Please check Terms & Conditions.";
            return;
        }

        if (txtConPassword.Text.Trim() != txtPassword.Text.Trim())
        {
            lblMsgs.Text = "Passwords do not match.";
            return;
        }

        objLogin.Username = txtUname.Text.Trim().Replace("'", "''");
        dt = objLoginDB.GetDataSet(objLogin, DA_SKORKEL.DA_Login.Login_1.GmailFBLogin);
        if (dt.Rows.Count > 0)
        {
            lblMsgs.Text = "Email id already exist.";
            txtUname.Text = "";
            return;
        }

        CryptoGraphy objEncrypt = new CryptoGraphy();
        string Password = objEncrypt.Encrypt(txtPassword.Text.Trim());
        string middleName="";

        BindDataTable();
        row = dt.NewRow();
        row["FirstName"] = txtFirstName.Text.Trim(); ;
        row["MiddleName"] = middleName;
        row["LastName"] = txtLastName.Text.Trim();
        row["UserName"] = txtUname.Text.Trim();
        row["Password"] = Password;
        row["ConPassword"] = txtConPassword.Text.Trim();
        row["UserTypeId"] = 1;                     //  For students
        dt.Rows.Add(row);
        Session.Add("UserDetails", dt);
        Response.Redirect("~/SignUp2.aspx");
    }

    protected void btnLinkedInLogin_Click(object sender, EventArgs e)
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        string authLink = _oauth.AuthorizationLinkGet();
        Application["reuqestToken"] = _oauth.Token;
        Application["reuqestTokenSecret"] = _oauth.TokenSecret;
        Application["oauthLink"] = authLink;
        Session["Session_OToken"] = _oauth.Token;
        Session["Session_OTokenSecret"] = _oauth.TokenSecret;
        Response.Redirect(authLink);
    }

    #region Gmail login Code

    protected void btnGoogleLogin_Click(object sender, EventArgs e)
    {
        string AuthenticationUrl = "https://accounts.google.com/o/oauth2/auth?redirect_uri=" + GoogleRedirectUrl
            + "&response_type=code&client_id=" + GoogleClientId 
            + "&scope=https://www.googleapis.com/auth/plus.login+https://www.googleapis.com/auth/plus.me+https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile&approval_prompt=force&access_type=offline";
        
        Response.Redirect(AuthenticationUrl);
    }

    protected void HandleOpneIdProviderResponse()
    {
        OpenIdRelyingParty rp = new OpenIdRelyingParty();

        var response = rp.GetResponse();
        if (response != null)
        {
            switch (response.Status)
            {
                case AuthenticationStatus.Authenticated:

                    Session["GoogleIdentifier"] = response.ClaimedIdentifier.ToString();
                    var fetchResponse = response.GetExtension<FetchResponse>();
                    Session["FetchResponse"] = fetchResponse;

                    var response2 = Session["FetchResponse"] as FetchResponse;

                    GetUserDetails(response2);
                    break;
                case AuthenticationStatus.Canceled:
                    Session["GoogleIdentifier"] = "Cancelled.";
                    break;
                case AuthenticationStatus.Failed:
                    Session["GoogleIdentifier"] = "Login Failed.";
                    break;
            }
        }
    }

    protected void GetUserDetails(FetchResponse response)
    {
        string fname = response.GetAttributeValue(WellKnownAttributes.Name.First) ?? "";
        string mname = response.GetAttributeValue(WellKnownAttributes.Name.Middle) ?? "";
        string lname = response.GetAttributeValue(WellKnownAttributes.Name.Last) ?? "";
        string Email = response.GetAttributeValue(WellKnownAttributes.Contact.Email) ?? "";
        string password = string.Empty;
        DA_Registrationdetails objRegistrationDB = new DA_Registrationdetails();
        DO_Registrationdetails objRegistration = new DO_Registrationdetails();

        //DA_OutLaetMaster.DA_OutLaetMaster outlat = new DA_OutLaetMaster.DA_OutLaetMaster();
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
        {
            ip = Request.ServerVariables["REMOTE_ADDR"];
        }


        DataTable dt = new DataTable();
        objLogin.Username = Email.Trim(); ;
        objLogin.Password = password.Trim();

        dt = objLoginDB.GetDataSet(objLogin, DA_SKORKEL.DA_Login.Login_1.GmailFBLogin);
        if (dt.Rows.Count == 0)
        {
            Session["GmailLogin"] = "";
            password = GeneratePassword();

            objRegistration.FirstName = fname;
            objRegistration.MiddleName = mname;
            objRegistration.LastName = lname;
            objRegistration.UserName = Email;
            objRegistration.Password = password;
            objRegistration.UserTypeId = 1;

            Session["GmailLogin"] = objRegistration;
            string gpass = "Confirm";
            string strmsg = string.Empty;
            byte[] encode = new byte[gpass.Length];
            encode = Encoding.UTF8.GetBytes(gpass);
            strmsg = Convert.ToBase64String(encode);

            objRegistrationDB.AddEditDel_RegistrationDetails(objRegistration, DA_Registrationdetails.RegistrationDetails.Add);
            objRegistration.RegistrationId = objRegistration.RegOutId;

            CustomerLogin(Email, password);
            Response.Redirect("Home.aspx");

            Response.Redirect("SignUp2.aspx?flg=SKL&ID=" + strmsg + "", true);
        }
        else
        {
            CustomerLogin(Email, "");
            Response.Redirect("Home.aspx");
        }
    }

    protected String GeneratePassword()
    {
        int minPassSize = 6;
        int maxPassSize = 12;
        StringBuilder stringBuilder = new StringBuilder();
        char[] charArray = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()".ToCharArray();
        int newPassLength = new Random().Next(minPassSize, maxPassSize);
        char character;
        Random rnd = new Random(DateTime.Now.Millisecond);
        for (int i = 0; i < newPassLength; i++)
        {
            character = charArray[rnd.Next(0, (charArray.Length - 1))];
            stringBuilder.Append(character);

        }

        return stringBuilder.ToString();
    }

    private void SendMail(string To, string UName, string CustPassword)
    {
        try
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

            string mailfrom = ConfigurationManager.AppSettings["mailFrom"];
            string mailServer = ConfigurationManager.AppSettings["mailServer"];
            string username = ConfigurationManager.AppSettings["UserName"];
            string Password = ConfigurationManager.AppSettings["Password"];
            string Port = ConfigurationManager.AppSettings["Port"];
            string MailSSL = ConfigurationManager.AppSettings["MailSSL"];
            string MailTo = To;
            //  MailTo = "ychoudhari@myuberall.com";
            SmtpClient clientip = new SmtpClient(mailServer);

            clientip.Port = Convert.ToInt32(Port);
            NetworkCredential cre = new NetworkCredential(username, Password);
            clientip.UseDefaultCredentials = false;
            if (MailSSL != "0")
                clientip.EnableSsl = true;

            clientip.Credentials = cre;
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];

            try
            {
                string Topic = string.Empty;
                string Description = string.Empty;
                string empid = string.Empty;

                string str = "<p> Hi , </p>";
                str += "<p> We have created a new Skorkel Account for you. </p>";

                str += "<p> Your account details are: <br /> Email id for login: " + UName + " <br /> Password:" + CustPassword + "</p>";

                str += "<br /> <br /><p><a href='http://115.124.123.151/Landing.aspx' >www.skorkel.com</a> </p>";
                System.Net.Mail.MailMessage mm2 = new System.Net.Mail.MailMessage();

                mm2.From = new System.Net.Mail.MailAddress(mailfrom, DisplayName);
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(str, null, "text/html");


                mm2.To.Clear();
                mm2.CC.Clear();
                mm2.Bcc.Clear();
                mm2.To.Add(MailTo);
                mm2.Subject = "Skorkel - Account created";
                mm2.AlternateViews.Add(htmlView);
                mm2.IsBodyHtml = true;
                clientip.Send(mm2);
                mm2.To.Clear();
                mm2.CC.Clear();
                mm2.Bcc.Clear();


            }
            catch (FormatException ex)
            {
                ex.Message.ToString();
                return;
            }
            catch (SmtpException ex)
            {
                ex.Message.ToString();
                return;
            }
            finally
            {
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected bool CustomerLogin(string login, string pass)
    {
        DA_Login Log = new DA_Login();

        DataTable dt = new DataTable();
        objLogin.Username = login;
        objLogin.Password = pass;

        dt = objLoginDB.GetDataSet(objLogin, DA_SKORKEL.DA_Login.Login_1.GmailFBLogin);
        if (dt.Rows.Count > 0)
        {
            UserSession.UserInfo UInfo = new UserSession.UserInfo();
            string LoginName = Convert.ToString(dt.Rows[0]["LoginName"]);
            UInfo.UserName = Convert.ToString(dt.Rows[0]["vchrUserName"]);
            UInfo.UserID = Convert.ToInt64(dt.Rows[0]["intRegistrationId"]);
            int TypeId = Convert.ToInt32(dt.Rows[0]["intUserTypeID"]);
            Session.Add("UserTypeId", TypeId);

            Session.Add("UInfo", UInfo);
            Session.Add("LoginName", LoginName);
            Session.Add("ExternalUserId", Convert.ToString(dt.Rows[0]["intRegistrationId"]));
            return true;
        }
        else
        {
            return false;

        }

    }

    private static string GetFullname(string first, string last)
    {
        var _first = first ?? "";
        var _last = last ?? "";
        if (string.IsNullOrEmpty(_first) || string.IsNullOrEmpty(_last))
            return "";
        return _first + " " + _last;
    }

    NameValueCollection StringToNameValueCollection(string queryString)
    {
        NameValueCollection queryParameters = new NameValueCollection();
        string[] querySegments = queryString.Split('&');
        foreach (string segment in querySegments)
        {
            string[] parts = segment.Split('=');
            if (parts.Length > 0)
            {
                string key = parts[0].Trim(new char[] { '?', ' ' });
                string val = parts[1].Trim();

                queryParameters.Add(key, val);
            }
        }
        return queryParameters;
    }

    void PersistRequestToken(RequestToken requestToken)
    {
        Session["RequestToken"] = requestToken;
    }

    RequestToken GetRequesttoken()
    {
        var requestToken = (RequestToken)Session["RequestToken"];
        Session.Remove("RequestToken");
        return requestToken;
    }

    string GetRouteableUrlFromRelativeUrl(string relativeUrl)
    {
        var url = HttpContext.Current.Request.Url;
        return url.Scheme + "://" + url.Host + ":" + url.Port + VirtualPathUtility.ToAbsolute("~/" + relativeUrl);
    }

    #endregion

}