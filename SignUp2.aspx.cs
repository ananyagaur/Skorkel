using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using DA_SKORKEL;
using System.Configuration;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Xml;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;

public partial class SignUp2 : System.Web.UI.Page
{
    DO_Registrationdetails objRegistration = new DO_Registrationdetails();
    DA_Registrationdetails objRegistrationDB = new DA_Registrationdetails();

    DA_Login objLoginDB = new DA_Login();
    DO_Login objLogin = new DO_Login();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_Scrl_UserExperienceTbl objDO = new DO_Scrl_UserExperienceTbl();
    DA_Scrl_UserExperienceTbl objDA = new DA_Scrl_UserExperienceTbl();

    DO_Scrl_UserEducationTbl objDOEdu = new DO_Scrl_UserEducationTbl();
    DA_Scrl_UserEducationTbl objDAEdu = new DA_Scrl_UserEducationTbl();
    private oAuthLinkedIn _oauth = new oAuthLinkedIn();
    CryptoGraphy objEncrypt = new CryptoGraphy();

    DataTable dtUser = new DataTable();
    DataTable dtExp = new DataTable();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];
    string GoogleClientId = ConfigurationManager.AppSettings["GoogleClientId"];
    string GoogleClientSecret = ConfigurationManager.AppSettings["GoogleClientSecret"];
    string GoogleRedirectUrl = ConfigurationManager.AppSettings["GoogleRedirectUrl"];
    string GoogleMailUrl = ConfigurationManager.AppSettings["GoogleMailUrl"];
    string UserTypeId = string.Empty, CurrentlyWork = string.Empty;
    string accessToken = "";
    string accessTokenSecret = "";
    string gettoken = null;
    string getotokensecret = null;
    string getverifier = null;
    string Acesss_token = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["flg"] != null && Request.QueryString["flg"] == "SKL")
            {
                BindUserDataTable();
                GetLinkedInDetails();
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                GetGmailLogInDetails();
            }

            LoadUserexpYear();
            if (Session["UserDetails"] != null)
            {
                string ukey1 = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "");
                ViewState["UnqKey"] = ukey1;
            }
            else
            {
                if (Session["ExternalUserId"] != null)
                {
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    Response.Redirect("Landing.aspx");
                }
            }
        }
    }

    protected void GetGmailLogInDetails()
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        string code = Request.QueryString["code"];
        var token = "https://www.googleapis.com/oauth2/v3/token?code=" + code
            + "&redirect_uri=" + GoogleRedirectUrl + "&client_id=" + GoogleClientId
            + "&scope=&client_secret=" + GoogleClientSecret
            + "&grant_type=authorization_code";

        HttpClient client = new HttpClient();
        var request = WebRequest.Create(token);
        request.Headers.Add("Authorization", "");
        request.Method = "POST";
        try
        {
            var requestStream = request.GetRequestStream();
            var responses = request.GetResponse();
            using (var responseStream = responses.GetResponseStream())
            {
                var reader = new StreamReader(responseStream);
                var responseText = reader.ReadToEnd();

                AccessTok m = JsonConvert.DeserializeObject<AccessTok>(responseText);
                Acesss_token = m.Access_Token;
            }
            Response.Close();
        }
        catch (WebException ex)
        {
            if (Session["ExternalUserId"] != null)
            {
                Response.Redirect("Home.aspx");
            }
            ex.Message.ToString();
        }

        var GplusUserInfo = "https://www.googleapis.com/plus/v1/people/me?access_token=" + Acesss_token;
        try
        {
            if (GplusUserInfo != string.Empty)
            {
                HttpWebRequest requests = (HttpWebRequest)WebRequest.Create(GplusUserInfo);
                requests.Method = "GET";
                requests.ContentLength = 0;
                HttpWebResponse response = (HttpWebResponse)requests.GetResponse();

                if (response.StatusCode.ToString().ToLower() == "ok")
                {
                    string contentType = response.ContentType;
                    Stream content = response.GetResponseStream();
                    if (content != null)
                    {
                        StreamReader contentReader = new StreamReader(content);
                        Response.ContentType = contentType;
                        var responseText = contentReader.ReadToEnd();
                        GoogleProfile Gplus = JsonConvert.DeserializeObject<GoogleProfile>(responseText);
                        string gender = Gplus.gender;
                        string Email = Gplus.Emails.Find(email => email.Type == "account").Value;

                        string name = Gplus.displayName;
                        string[] words = name.Split(' ');
                        string FirstName = words[0];
                        string Lastname = words[1];

                        DA_Registrationdetails objRegistrationDB = new DA_Registrationdetails();
                        DO_Registrationdetails objRegistration = new DO_Registrationdetails();
                        DataTable dt = new DataTable();
                        objLogin.Username = Email;

                        dt = objLoginDB.GetDataSet(objLogin, DA_SKORKEL.DA_Login.Login_1.GmailFBLogin);
                        if (dt.Rows.Count == 0)
                        {
                            string password = GeneratePassword();
                            objRegistration.FirstName = FirstName;
                            objRegistration.LastName = Lastname;
                            objRegistration.UserName = Email;
                            objRegistration.Password = password;
                            objRegistration.UserTypeId = 1;
                            if (gender == "male")
                            {
                                objRegistration.Sex = "M";
                            }
                            else
                            {
                                objRegistration.Sex = "F";
                            }
                            objRegistrationDB.AddEditDel_RegistrationDetails(objRegistration, DA_Registrationdetails.RegistrationDetails.Add);

                            UserSession.UserInfo UInfo = new UserSession.UserInfo();
                            UInfo.UserName = Convert.ToString(objRegistration.UserName);
                            UInfo.UserID = Convert.ToInt64(objRegistration.RegOutId);
                            int TypeId = Convert.ToInt32(objRegistration.UserTypeId);
                            Session.Add("UserTypeId", TypeId);
                            Session.Add("UInfo", UInfo);
                            Session.Add("LoginName", name);
                            Session.Add("ExternalUserId", Convert.ToString(objRegistration.RegOutId));

                            if (ISAPIURLACCESSED == "1")
                            {
                                String UserURL = APIURL + "registerUser.action?" +
                                                        "uid=" + objRegistration.RegOutId +
                                                        "&userId=" + objRegistration.UserName +
                                                        "&password=" + objRegistration.Password +
                                                        "&firstName=" + objRegistration.FirstName +
                                                        "&lastName=" + objRegistration.LastName +
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
                                {
                                    ex.Message.ToString();
                                }
                            }

                            activate();
                        }
                        else
                        {
                            UserSession.UserInfo UInfo = new UserSession.UserInfo();
                            string LoginName = Convert.ToString(dt.Rows[0]["LoginName"]);
                            UInfo.UserName = Convert.ToString(dt.Rows[0]["vchrUserName"]);
                            UInfo.UserID = Convert.ToInt64(dt.Rows[0]["intRegistrationId"]);
                            int TypeId = Convert.ToInt32(dt.Rows[0]["intUserTypeID"]);
                            //vchrPassword
                            Session.Add("UserTypeId", TypeId);
                            Session.Add("UInfo", UInfo);
                            Session.Add("LoginName", LoginName);
                            Session.Add("ExternalUserId", Convert.ToString(dt.Rows[0]["intRegistrationId"]));

                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (Session["ExternalUserId"] != null)
            {
                Response.Redirect("Home.aspx");
            }
        }
    }

    protected void GetLinkedInDetails()
    {

        try
        {
            gettoken = Session["Session_OToken"].ToString();
            getotokensecret = Session["Session_OTokenSecret"].ToString();
            getverifier = Request.QueryString["oauth_verifier"];

            try
            {
                //Get Access Token
                _oauth.Token = gettoken;
                _oauth.TokenSecret = getotokensecret;
                _oauth.Verifier = getverifier;

                _oauth.AccessTokenGet(gettoken);
                accessToken = _oauth.Token;
                accessTokenSecret = _oauth.TokenSecret;

                _oauth.Token = accessToken;
                _oauth.TokenSecret = accessTokenSecret;
                _oauth.Verifier = getverifier;
            }
            catch
            {
                Session.Abandon();
                Response.Redirect("Landing.aspx");
            }

            if (Request.QueryString["flg"] != null && Request.QueryString["flg"] != "")
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                string response = _oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/people/~/", null);

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(response); // suppose that myXmlString contains "<Names>...</Names>"

                XmlNodeList xnList = xml.SelectNodes("/person");

                foreach (XmlNode xn in xnList)
                {
                    string firstName = xn["first-name"].InnerText;
                    ViewState["firstName"] = firstName;
                    string lastName = xn["last-name"].InnerText;
                    ViewState["lastName"] = lastName;
                    string CompanyName = xn["headline"].InnerText;
                }


                XmlNodeList xnList1 = xml.SelectNodes("/");
                string response2 = _oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/people/~/email-address", null);
                xml.LoadXml(response2);
                foreach (XmlNode Xnn in xnList1)
                {
                    string emailAddress = Xnn["email-address"].InnerText;
                    ViewState["emailAddress"] = emailAddress;
                }
                LoadLinkedIndata();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void BindUserDataTable()
    {
        DataTable dt = new DataTable();

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

    protected void LoadLinkedIndata()
    {
        string password = string.Empty;
        DA_Registrationdetails objRegistrationDB = new DA_Registrationdetails();
        DO_Registrationdetails objRegistration = new DO_Registrationdetails();

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
        {
            ip = Request.ServerVariables["REMOTE_ADDR"];
        }

        DataTable dt = new DataTable();
        objLogin.Username = Convert.ToString(ViewState["emailAddress"]);
        objLogin.Password = password.Trim();
        dt = objLoginDB.GetDataSet(objLogin, DA_SKORKEL.DA_Login.Login_1.GmailFBLogin);

        if (dt.Rows.Count == 0)
        {
            password = GeneratePassword();
            dt = new DataTable();
            dt = (DataTable)ViewState["UserDetails"];

            DataRow row = dt.NewRow();
            row["FirstName"] = Convert.ToString(ViewState["firstName"]);
            row["LastName"] = Convert.ToString(ViewState["lastName"]);
            row["UserName"] = Convert.ToString(ViewState["emailAddress"]);
            row["Password"] = password;
            row["ConPassword"] = password;
            row["UserTypeId"] = 1;
            dt.Rows.Add(row);
            Session.Add("UserDetails", dt);
            ViewState.Add("FromLinkedIn", 1);
        }
        else
        {
            CustomerLogin(Convert.ToString(ViewState["emailAddress"]), "");
            Response.Redirect("Home.aspx");
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

    protected void LoadUserexpYear()
    {
        try
        {
            DataTable dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.LoadYear);
            if (dt.Rows.Count > 0)
            {
                txtFromYear.DataSource = dt;
                txtFromYear.DataTextField = "intYear";
                txtFromYear.DataValueField = "intYear";
                txtFromYear.DataBind();
                txtFromYear.Items.Insert(0, new ListItem("Year"));

                txtToYear.DataSource = dt;
                txtToYear.DataTextField = "intYear";
                txtToYear.DataValueField = "intYear";
                txtToYear.DataBind();
                txtToYear.Items.Insert(0, new ListItem("Year"));
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    public void SaveUserDetails()
    {
        dtUser = (DataTable)Session["UserDetails"];
        if (dtUser.Rows.Count > 0)
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];

            objRegistration.IpAddress = ip;
            objRegistration.FirstName = Convert.ToString(dtUser.Rows[0]["FirstName"]);
            objRegistration.LastName = Convert.ToString(dtUser.Rows[0]["LastName"]);
            objRegistration.UserName = Convert.ToString(dtUser.Rows[0]["UserName"]);
            objRegistration.UserTypeId = Convert.ToInt32(dtUser.Rows[0]["UserTypeId"]);
            objRegistration.Password = Convert.ToString(dtUser.Rows[0]["Password"]);
            lblEmail.Text = Convert.ToString(dtUser.Rows[0]["UserName"]);
            objRegistrationDB.AddEditDel_RegistrationDetails(objRegistration, DA_Registrationdetails.RegistrationDetails.Add);
            Session["ExternalUserId"] = objRegistration.RegOutId;

            if (Convert.ToString(ViewState["FromLinkedIn"]) == "1")
            {
                UserSession.UserInfo UInfo = new UserSession.UserInfo();
                string LoginName = Convert.ToString(dtUser.Rows[0]["FirstName"] + " " + dtUser.Rows[0]["LastName"]);
                UInfo.UserName = Convert.ToString(dtUser.Rows[0]["UserName"]);
                UInfo.UserID = Convert.ToInt64(Session["ExternalUserId"]);
                int TypeId = Convert.ToInt32(dtUser.Rows[0]["UserTypeId"]);
                Session.Add("UserTypeId", TypeId);
                Session.Add("UInfo", UInfo);
                Session.Add("LoginName", LoginName);
                Session.Add("ExternalUserId", Convert.ToString(Session["ExternalUserId"]));

                if (ISAPIURLACCESSED == "1")
                {
                    String UserURL = APIURL + "registerUser.action?" +
                                            "uid=" + Convert.ToString(Session["ExternalUserId"]) +
                                            "&userId=" + dtUser.Rows[0]["UserName"] +
                                            "&password=" + dtUser.Rows[0]["Password"] +
                                            "&firstName=" + dtUser.Rows[0]["FirstName"] +
                                            "&lastName=" + dtUser.Rows[0]["LastName"] +
                                            "&userType=STUDENT" +
                                            "&userContextIds=" + null +
                                            "&friendUserIds=" + null +
                                            "&lawRelated=" + null;
                    try
                    {

                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                        myRequest1.Method = "GET";
                        WebResponse myResponse1 = myRequest1.GetResponse();
                        if (ISAPIResponse != "0")
                        {
                            StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                            String result = sr.ReadToEnd();
                            objAPILogDO.strURL = UserURL;
                            objAPILogDO.strAPIType = "Student";
                            objAPILogDO.strResponse = result;
                            if (ip == null)
                                objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                            objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                }

                activate();
                if (txtInstituteName.Text == "")
                {
                    Response.Redirect("Home.aspx");
                }
            }
            else
            {
                SendMail(objRegistration);
            }
        }

    }

    private void activate()
    {
        if (Convert.ToInt32(Session["ExternalUserId"]) != 0)
            objRegistration.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);

        objRegistrationDB.AddEditDel_Profile(objRegistration, DA_Registrationdetails.RegistrationDetails.InstituteNameStuProfList);
    }

    protected void lnlNext_Click(object sender, EventArgs e)
    {
        SaveSignup();
    }

    protected void lnlSkip_Click(object sender, EventArgs e)
    {
        txtToYear.SelectedIndex = 0;
        txtInstituteName.Text = "";
        txtFromYear.SelectedIndex = 0;
        txtDescription.InnerText = "";
        txtDegree.Text = "";
        fromMonth.Value = "Month";
        toMonth.SelectedIndex = 0;
        chkAtPresent.Checked = false;
        lstAreaExpert.DataSource = null;
        lstAreaExpert.DataBind();
        lstAreaExpert.Visible = false;
        ViewState["UnqKey"] = null;
        SaveSignup();
    }

    protected void SaveSignup()
    {
        if (txtInstituteName.Text != "")
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            SaveUserDetails();
            if (txtDegree.Text != "")
            {
                objDOEdu.strDegree = txtDegree.Text.Trim().Replace("'", "''");
                objDOEdu.intDegreeId = objDAEdu.GetintDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetDeegreeid);
            }

            if (txtInstituteName.Text != "")
            {
                objDOEdu.strInstituteName = txtInstituteName.Text.Trim().Replace("'", "''");
                objDOEdu.intInstituteId = objDAEdu.GetintDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetInstituteid);
            }
            if (txtFromYear.Text.Trim() != "Year")
            {
                objDOEdu.intYear = Convert.ToInt32(txtFromYear.Text.Trim());
            }
            if (txtToYear.Text.Trim() != "Year")
            {
                objDOEdu.intToYear = Convert.ToInt32(txtToYear.Text.Trim());
            }

            if (fromMonth.Value == "Jan")
            {
                objDOEdu.intMonth = 1;
            }
            else if (fromMonth.Value == "Feb")
            {
                objDOEdu.intMonth = 2;
            }
            else if (fromMonth.Value == "Mar")
            {
                objDOEdu.intMonth = 3;
            }
            else if (fromMonth.Value == "Apr")
            {
                objDOEdu.intMonth = 4;
            }
            else if (fromMonth.Value == "May")
            {
                objDOEdu.intMonth = 5;
            }
            else if (fromMonth.Value == "Jun")
            {
                objDOEdu.intMonth = 6;
            }
            else if (fromMonth.Value == "Jul")
            {
                objDOEdu.intMonth = 7;
            }
            else if (fromMonth.Value == "Aug")
            {
                objDOEdu.intMonth = 8;
            }
            else if (fromMonth.Value == "Sep")
            {
                objDOEdu.intMonth = 9;
            }
            else if (fromMonth.Value == "Oct")
            {
                objDOEdu.intMonth = 10;
            }
            else if (fromMonth.Value == "Nov")
            {
                objDOEdu.intMonth = 11;
            }
            else if (fromMonth.Value == "Dec")
            {
                objDOEdu.intMonth = 12;
            }

            if (toMonth.SelectedValue == "Jan")
            {
                objDOEdu.intToMonth = 1;
            }
            else if (toMonth.SelectedValue == "Feb")
            {
                objDOEdu.intToMonth = 2;
            }
            else if (toMonth.SelectedValue == "Mar")
            {
                objDOEdu.intToMonth = 3;
            }
            else if (toMonth.SelectedValue == "Apr")
            {
                objDOEdu.intToMonth = 4;
            }
            else if (toMonth.SelectedValue == "May")
            {
                objDOEdu.intToMonth = 5;
            }
            else if (toMonth.SelectedValue == "Jun")
            {
                objDOEdu.intToMonth = 6;
            }
            else if (toMonth.SelectedValue == "Jul")
            {
                objDOEdu.intToMonth = 7;
            }
            else if (toMonth.SelectedValue == "Aug")
            {
                objDOEdu.intToMonth = 8;
            }
            else if (toMonth.SelectedValue == "Sep")
            {
                objDOEdu.intToMonth = 9;
            }
            else if (toMonth.SelectedValue == "Oct")
            {
                objDOEdu.intToMonth = 10;
            }
            else if (toMonth.SelectedValue == "Nov")
            {
                objDOEdu.intToMonth = 11;
            }
            else if (toMonth.SelectedValue == "Dec")
            {
                objDOEdu.intToMonth = 12;
            }

            if (chkAtPresent.Checked == true)
            {
                objDOEdu.intToMonth = DateTime.Now.Month;
            }

            objDOEdu.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
            objDOEdu.strSpclLibrary = txtDescription.InnerHtml;
            objDOEdu.strIpAddress = ip;

            if (chkAtPresent.Checked == true)
            {
                objDOEdu.intToMonth = DateTime.Now.Month;
            }
            objDOEdu.strEducationAchievement = "Education";
            objDOEdu.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
            objDOEdu.dtDate = DateTime.Now;
            objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.Insert);

            objDO.dtFromDate = DateTime.Now;
            objDO.dtToDate = DateTime.Now;
            objDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
            objDO.strUnqId = ViewState["UnqKey"].ToString();
            objDA.AddEditDel_Scrl_UserExperienceTbl(objDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.UpdateUserSkill);

            if (ISAPIURLACCESSED == "1")
            {
                try
                {
                    String url = APIURL + "updateWorkExpDetails.action?" +
                                "uid=" + UserTypeId + Convert.ToInt32(ViewState["UserID"]) +
                                "&expId=" + objDO.intOutId +
                                "&designation=" + objDO.strDesignation +
                                "&company=" + objDO.strCompanyName +
                                "&workFrom=" + objDO.dtFromDate +
                                "&workTo=" + CurrentlyWork +
                                "&details=" + objDO.strDescription;

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = url;
                        objAPILogDO.strAPIType = "Profile Work Experience";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = ip;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }

                catch { }
            }

            if (Convert.ToString(ViewState["FromLinkedIn"]) == "1")
            {
                Response.Redirect("Home.aspx");
            }
            lnlNext.Visible = false;
            lnkSkipbtn.Visible = false;
            lnlNext2.Visible = true;
            lnkSkipbtn2.Visible = true;
            Clear();
            divWelcome.Style.Add("display", "block");
        }
        else
        {
            SaveUserDetails();
            objDO.dtFromDate = DateTime.Now;
            objDO.dtToDate = DateTime.Now;
            objDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
            objDO.strUnqId =Convert.ToString(ViewState["UnqKey"]);
            objDA.AddEditDel_Scrl_UserExperienceTbl(objDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.UpdateUserSkill);

            divWelcome.Style.Add("display", "block");
            lnlNext.Visible = false;
            lnkSkipbtn.Visible = false;
            lnlNext2.Visible = true;
            lnkSkipbtn2.Visible = true;
            Clear();
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlCommand com = new SqlCommand())
            {
                //com.CommandText = "select strInstituteName from Scrl_InstituteNameTbl where strInstituteName like % '" + prefixText+"'%";
                com.CommandText = "select strInstituteName from Scrl_InstituteNameTbl where " + "strInstituteName like @Search + '%'";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> InstituteName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        InstituteName.Add(sdr["strInstituteName"].ToString());
                    }
                }
                con.Close();
                return InstituteName;

            }

        }

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetDegreeList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlCommand com = new SqlCommand())
            {
                //com.CommandText = "select strInstituteName from Scrl_InstituteNameTbl where strInstituteName like % '" + prefixText+"'%";
                com.CommandText = "select strDegreeName from Scrl_InstituteDegreeTbl where " + "strDegreeName like @Search + '%'";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> InstituteName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        InstituteName.Add(sdr["strDegreeName"].ToString());
                    }
                }
                con.Close();
                return InstituteName;

            }

        }

    }

    protected void chkAtPresent_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAtPresent.Checked == true)
        {
            txtToYear.Text = DateTime.Now.Year.ToString();
            txtToYear.Enabled = false;
            toMonth.SelectedIndex = DateTime.Now.Month;
            toMonth.Enabled = false;

        }
        else
        {
            txtToYear.SelectedIndex = 0;
            txtToYear.Enabled = true;
            toMonth.SelectedIndex = 0;
            toMonth.Enabled = true;
        }
        //LoadUserAreaExp();
    }

    protected void BindDataTable()
    {
        DataColumn intRegistrationId = new DataColumn();
        intRegistrationId.DataType = System.Type.GetType("System.String");
        intRegistrationId.ColumnName = "intRegistrationId";
        dtExp.Columns.Add(intRegistrationId);

        DataColumn strCompanyName = new DataColumn();
        strCompanyName.DataType = System.Type.GetType("System.String");
        strCompanyName.ColumnName = "strCompanyName";
        dtExp.Columns.Add(strCompanyName);

        DataColumn strDesignation = new DataColumn();
        strDesignation.DataType = System.Type.GetType("System.String");
        strDesignation.ColumnName = "strDesignation";
        dtExp.Columns.Add(strDesignation);

        DataColumn inFromtMonth = new DataColumn();
        inFromtMonth.DataType = System.Type.GetType("System.String");
        inFromtMonth.ColumnName = "inFromtMonth";
        dtExp.Columns.Add(inFromtMonth);

        DataColumn intFromYear = new DataColumn();
        intFromYear.DataType = System.Type.GetType("System.String");
        intFromYear.ColumnName = "intFromYear";
        dtExp.Columns.Add(intFromYear);

        DataColumn intToMonth = new DataColumn();
        intToMonth.DataType = System.Type.GetType("System.String");
        intToMonth.ColumnName = "intToMonth";
        dtExp.Columns.Add(intToMonth);

        DataColumn intToYear = new DataColumn();
        intToYear.DataType = System.Type.GetType("System.String");
        intToYear.ColumnName = "intToYear";
        dtExp.Columns.Add(intToYear);

        DataColumn strDescription = new DataColumn();
        strDescription.DataType = System.Type.GetType("System.String");
        strDescription.ColumnName = "strDescription";
        dtExp.Columns.Add(strDescription);

        DataColumn UnqKey = new DataColumn();
        UnqKey.DataType = System.Type.GetType("System.String");
        UnqKey.ColumnName = "UnqKey";
        dtExp.Columns.Add(UnqKey);


    }

    private void SendMail(DO_Registrationdetails objRegistration)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

            string mailfrom = ConfigurationManager.AppSettings["mailfrom"];
            string mailServer = ConfigurationManager.AppSettings["mailServer"];
            string username = ConfigurationManager.AppSettings["UserName"];
            string Password = ConfigurationManager.AppSettings["Password"];
            string Port = ConfigurationManager.AppSettings["Port"];
            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailSSL = ConfigurationManager.AppSettings["MailSSL"];
            string MailTo = objRegistration.UserName.Trim();
            string Mailbody = "";
            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = false;
            if (MailSSL != "0")
                clientip.EnableSsl = true;

            string id ="id=" + Session["ExternalUserId"];
            string encript = QueryStringModule.Encrypt(id.ToString());
            clientip.Credentials = new System.Net.NetworkCredential(username, Password);
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];

            try
            {
                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom);
                Rmm2.Body = Mailbody.ToString();
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Account verification</b>" + "<br><br>" +
                    "Dear" + " " + objRegistration.FirstName + " " + objRegistration.LastName + "," +
                    "<br><br>Thank you for creating Skorkel account.<br><br>Your username is your email address which you'll need when you log in.<br><br>Please click on the link below to activate your account."
                    + "<br><br>"
                    + "<a href='" + MailURL + "/SignUp1.aspx?id=" + Session["ExternalUserId"] + "'>" + MailURL
                    + "/SignUp1.aspx" + encript
                    + "</a>" + "<br><br><br>" + "Thanks," + "<br>"
                    + "Skorkel Team"
                    + "<br><br>****This is a system generated message. Kindly do not reply****", null, "text/html");
                Rmm2.To.Clear();
                Rmm2.To.Add(MailTo);
                Rmm2.Subject = "Skorkel new account information";
                Rmm2.AlternateViews.Add(htmlView);
                Rmm2.IsBodyHtml = true;
                clientip.Send(Rmm2);
                Rmm2.To.Clear();

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

    protected void Clear()
    {
        txtToYear.SelectedIndex = 0;
        txtInstituteName.Text = "";
        txtFromYear.SelectedIndex = 0;
        txtDescription.InnerText = "";
        txtDegree.Text = "";
        fromMonth.Value = "Month";
        toMonth.SelectedIndex = 0;
        chkAtPresent.Checked = false;
        lstAreaExpert.DataSource = null;
        lstAreaExpert.DataBind();
        lstAreaExpert.Visible = false;
        LoadUserexpYear();
    }

    protected void lstAreaExpert_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnintUserSkillId = (HiddenField)e.Item.FindControl("hdnintUserSkillId");
        //LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkdDelete");
        ImageButton lnkDelete = (ImageButton)e.Item.FindControl("lnkdDelete");
        if (e.CommandName == "DeleteExp")
        {
            objDO.dtFromDate = DateTime.Now;
            objDO.dtToDate = DateTime.Now;
            objDO.intUserSkillId = Convert.ToInt32(hdnintUserSkillId.Value);
            objDA.AddEditDel_Scrl_UserExperienceTbl(objDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.DeleteUserSkill);
            LoadUserAreaExp();
        }

    }

    protected void btnAreaExpSave_Click(object sender, EventArgs e)
    {
        if (txtAreaExpert.Text != "")
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objDO.dtFromDate = DateTime.Now;
            objDO.dtToDate = DateTime.Now;
            objDO.strSkillName = txtAreaExpert.Text.Trim();
            objDO.strIpAddress = ip;
            objDO.strUnqId = ViewState["UnqKey"].ToString();
            objDA.AddEditDel_Scrl_UserExperienceTbl(objDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.InsertUserSkill);
            txtAreaExpert.Text = "";
            btnAreaExpSave.Focus();
            LoadUserAreaExp();
        }
    }

    protected void LoadUserAreaExp()
    {
        objDO.strUnqId = ViewState["UnqKey"].ToString();
        DataTable dt = objDA.GetDataTable(objDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.GetUserSkill);
        if (dt.Rows.Count > 0)
        {
            lstAreaExpert.DataSource = dt;
            lstAreaExpert.DataBind();
        }
        else
        {
            lstAreaExpert.DataSource = null;
            lstAreaExpert.DataBind();
        }

    }

    #region GoogleLogin

    public class GoogleProfile
    {
        public string Id { get; set; }
        public string gender { get; set; }
        public string displayName { get; set; }
        public Image Image { get; set; }
        public List<Email> Emails { get; set; }
        public string ObjectType { get; set; }
        public string password { get; set; }
    }

    public class Email
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class name
    {
        public string familyName { get; set; }
        public string givenName { get; set; }
    }

    public class Image
    {
        public string Url { get; set; }
    }

    public class AccessTok
    {
        public string Access_Token { get; set; }
    }

    #endregion

}

