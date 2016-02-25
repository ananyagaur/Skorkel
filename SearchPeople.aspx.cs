using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Web.UI;
using System.Net;


public partial class SearchPeople : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    DO_Registrationdetails objdoreg = new DO_Registrationdetails();
    DA_Registrationdetails objdareg = new DA_Registrationdetails();

    DO_Scrl_UserRecommendation objRecmndDO = new DO_Scrl_UserRecommendation();
    DA_Scrl_UserRecommendation objRecmndDA = new DA_Scrl_UserRecommendation();

    DO_Scrl_UserPostUpdateTbl objpost = new DO_Scrl_UserPostUpdateTbl();
    DA_Scrl_UserPostUpdateTbl objpostDB = new DA_Scrl_UserPostUpdateTbl();

    DA_Profile ObjDAprofile = new DA_Profile();
    DO_Profile objDoProfile = new DO_Profile();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_WallMessage WallMessageDO = new DO_WallMessage();
    DA_WallMessage WallMessageDA = new DA_WallMessage();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];
    string UserTypeId = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divConnDisPopup.Style.Add("display", "none");
            divMessPopup.Style.Add("display", "none");

            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            if (Request.QueryString["Id"] != "" && Request.QueryString["Id"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Request.QueryString["Id"]);
            }

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "People";

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "10";

            if (Request.QueryString["ViewAll"] == "1")
            {
                ViewState["ViewAll"] = Request.QueryString["ViewAll"];
                BindSingleData();
            }
            else
            {
                BinduserSearch();
            }
            GetUserType();
        }

        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];
        if (eventTarget == "PostBackFromOnclick_btnOpen")
        {

        }
    }

    protected void GetUserType()
    {
        if (Convert.ToString(ViewState["FlagUser"]) == "1")
            UserTypeId = "S";
        else if (Convert.ToString(ViewState["FlagUser"]) == "2")
            UserTypeId = "P";
        else if (Convert.ToString(ViewState["FlagUser"]) == "3")
            UserTypeId = "LI";
        else if (Convert.ToString(ViewState["FlagUser"]) == "4")
            UserTypeId = "L";
        else if (Convert.ToString(ViewState["FlagUser"]) == "5")
            UserTypeId = "J";
        else if (Convert.ToString(ViewState["FlagUser"]) == "6")
            UserTypeId = "TP";
        else if (Convert.ToString(ViewState["FlagUser"]) == "7")
            UserTypeId = "LF";
    }

    #region Search

    protected void BindSingleData()
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            objpost.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
            objpost.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
            if (hdnTempUserId.Value != "" && hdnTempUserId.Value != null)
                objpost.intUserType = hdnTempUserId.Value;
            objpost.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
            dt = objpostDB.GetDataTable(objpost, DA_Scrl_UserPostUpdateTbl.Scrl_UserPostUpdateTbl.GetConnectionList);

            if (dt.Rows.Count > 0)
            {
                lstPostUpdates.DataSource = dt;
                lstPostUpdates.DataBind();
                dvPage.Visible = true;
                BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));
            }
            else
            {
                lstPostUpdates.DataSource = null;
                lstPostUpdates.DataBind();
                dvPage.Visible = false;
            }
        }
    }

    protected void BinduserSearch()
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            objpost.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
            objpost.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
            objpost.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
            if (hdnTempUserId.Value != "" && hdnTempUserId.Value != null)
                objpost.intUserType = hdnTempUserId.Value;
            objpost.strSearch = "";
            dt = objpostDB.GetDataTable(objpost, DA_Scrl_UserPostUpdateTbl.Scrl_UserPostUpdateTbl.GetAllStudentDetails);
            if (dt.Rows.Count > 0)
            {
                lstPostUpdates.DataSource = dt;
                lstPostUpdates.DataBind();
                dvPage.Visible = true;
                BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));
            }
            else
            {
                lstPostUpdates.DataSource = null;
                lstPostUpdates.DataBind();
                dvPage.Visible = false;
            }
        }
    }

    protected void lstPostUpdates_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        HiddenField hdnintUserTypeID = (HiddenField)e.Item.FindControl("hdnintUserTypeID");
        HiddenField hdnstrOthers = (HiddenField)e.Item.FindControl("hdnstrOthers");
        LinkButton btnsendreq = dataItem.FindControl("btnsendreq") as LinkButton;
        LinkButton lnkDisConnect = (LinkButton)e.Item.FindControl("lnkDisConnect");
        LinkButton btnRecmnd = dataItem.FindControl("btnRecmnd") as LinkButton;
        HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
        Label lblFriends = (Label)e.Item.FindControl("lblFriends");
        Label lblName = (Label)e.Item.FindControl("lblName");
        HiddenField hdnimgprofile = (HiddenField)e.Item.FindControl("hdnimgprofile");

        if (imgprofile.Src == "" || imgprofile.Src == null || imgprofile.Src == "CroppedPhoto/")
        {
            imgprofile.Src = "images/comment-profile.jpg";
        }
        else
        {
            string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + hdnimgprofile.Value);
            if (File.Exists(imgPathPhysical))
            {
            }
            else
            {
                imgprofile.Src = "images/comment-profile.jpg";
            }
        }
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            if (hdnintUserTypeID.Value == "6")
                lblName.Text = Convert.ToString(hdnstrOthers.Value);

            if (Convert.ToString(ViewState["UserID"]) == Convert.ToString(Session["ExternalUserId"]))
            {
                objdoreg.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
                objdoreg.InvitedUserId = Convert.ToInt32(hdnRegistrationId.Value);
                DataTable dtReq = new DataTable();
                dtReq = objdareg.GetExistsRequest(objdoreg, DA_Registrationdetails.RegistrationDetails.SingleRecord);
                if (dtReq.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtReq.Rows[0]["IsAccepted"]) == 0)
                    {
                        btnsendreq.Visible = true;
                        lnkDisConnect.Visible = false;
                        btnRecmnd.Visible = false;
                        if (dtReq.Rows[0]["Accepts"].ToString() == "2")
                            btnsendreq.Text = "Accept Invitation";

                    }
                    else if (Convert.ToInt32(dtReq.Rows[0]["IsAccepted"]) == 1)
                    {
                        btnsendreq.Visible = false;
                        lnkDisConnect.Visible = true;
                        btnRecmnd.Visible = true;
                    }
                    else if (Convert.ToInt32(dtReq.Rows[0]["IsAccepted"]) == 2)
                    {
                        btnsendreq.Visible = true;
                        lnkDisConnect.Visible = false;
                        btnRecmnd.Visible = false;
                    }
                }
                else
                {
                    btnRecmnd.Visible = false;
                    lnkDisConnect.Visible = false;
                }
                DataTable dtfrnd = new DataTable();
                objDoProfile.RegistrationId = Convert.ToInt32(hdnRegistrationId.Value);
                dtfrnd = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetConnCount);
                if (dtfrnd.Rows.Count > 0)
                {
                    lblFriends.Text = Convert.ToString(dtfrnd.Rows[0]["Accepted"]);
                }
                else
                {
                    lblFriends.Text = "0";
                }
            }
            else if (Convert.ToInt32(Session["ExternalUserId"]) == Convert.ToInt32(hdnRegistrationId.Value))
            {
                btnsendreq.Visible = false;
                btnRecmnd.Visible = false;
                lnkDisConnect.Visible = false;

                DataTable dtfrnd = new DataTable();
                objDoProfile.RegistrationId = Convert.ToInt32(hdnRegistrationId.Value);
                dtfrnd = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetConnCount);
                if (dtfrnd.Rows.Count > 0)
                {
                    lblFriends.Text = Convert.ToString(dtfrnd.Rows[0]["Accepted"]);
                }
                else
                {
                    lblFriends.Text = "0";
                }
            }
            else
            {
                objdoreg.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
                objdoreg.InvitedUserId = Convert.ToInt32(hdnRegistrationId.Value);
                DataTable dtReq = new DataTable();
                dtReq = objdareg.GetExistsRequest(objdoreg, DA_Registrationdetails.RegistrationDetails.SingleRecord);
                if (dtReq.Rows.Count > 0)
                {
                    objdoreg.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
                    objdoreg.InvitedUserId = Convert.ToInt32(hdnRegistrationId.Value);
                    DataTable dt = new DataTable();
                    dt = objdareg.GetExistsRequest(objdoreg, DA_Registrationdetails.RegistrationDetails.SingleRecord);

                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[0]["IsAccepted"]) == 0)
                        {
                            btnsendreq.Visible = true;
                            lnkDisConnect.Visible = false;
                            btnRecmnd.Visible = false;
                        }
                        else if (Convert.ToInt32(dt.Rows[0]["IsAccepted"]) == 1)
                        {
                            btnsendreq.Visible = false;
                            lnkDisConnect.Visible = true;
                            btnRecmnd.Visible = true;

                        }
                        else if (Convert.ToInt32(dt.Rows[0]["IsAccepted"]) == 2)
                        {
                            btnsendreq.Visible = true;
                            lnkDisConnect.Visible = false;
                            btnRecmnd.Visible = false;
                        }
                    }
                    else
                    {
                        btnsendreq.Visible = true;
                        lnkDisConnect.Visible = false;
                        btnRecmnd.Visible = false;
                    }
                }
                else
                {
                    btnRecmnd.Visible = false;
                    lnkDisConnect.Visible = false;
                }
                DataTable dtfrnd = new DataTable();
                objDoProfile.RegistrationId = Convert.ToInt32(hdnRegistrationId.Value);
                dtfrnd = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetConnCount);
                if (dtfrnd.Rows.Count > 0)
                {
                    lblFriends.Text = Convert.ToString(dtfrnd.Rows[0]["Accepted"]);
                }
                else
                {
                    lblFriends.Text = "0";
                }
            }
        }
    }

    protected void lstPostUpdates_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        LinkButton lblPostlink = (LinkButton)e.Item.FindControl("lblPostlink");
        Label lblEmailId = (Label)e.Item.FindControl("lblEmailId");
        Label lblName = (Label)e.Item.FindControl("lblName");
        hdnfullname.Value = lblName.Text;
        hdnEmailId.Value = lblEmailId.Text;

        ViewState["RegId"] = Convert.ToInt32(hdnRegistrationId.Value);

        divSuccess.Style.Add("display", "none");
        if (e.CommandName == "Details")
        {
            Response.Redirect("Home.aspx?RegId=" + hdnRegistrationId.Value);
        }
        else
        if (e.CommandName == "Connect" || e.CommandName == "DisConnect")
        {
            divMessPopup.Style.Add("display", "none");
            ViewState["CommandName"] = e.CommandName;

            if (e.CommandName == "Connect")
            {
                lblConnDisconn.Text = "Do you want to Connect ?";
            }
            else
            {
                lblConnDisconn.Text = "Do you want to DisConnect ?";
            }
            divConnDisPopup.Style.Add("display", "block");
        }
        else if (e.CommandName == "Recommendation")
        {
            divConnDisPopup.Style.Add("display", "none");
            divMessPopup.Style.Add("display", "block");
        }
        else if (e.CommandName == "Ask Recommendation")
        {
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                objRecmndDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }
            else
            {
                return;
            }
            objRecmndDO.intInvitedUserId = Convert.ToInt32(hdnRegistrationId.Value);
            objRecmndDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objRecmndDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (objRecmndDO.strIpAddress == null)
                objRecmndDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.Add);
            lblMessage.Text = "Ask for recommendation send successfully.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }
        else if (e.CommandName == "CancelRecom")
        {
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trRecommend");
            tr.Visible = false;
        }
        else if (e.CommandName == "Close")
        {
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trRecommend");
            tr.Visible = false;
        }
    }

    protected void lnkConnDisconn_Click(object sender, EventArgs e)
    {
        divConnDisPopup.Style.Add("display", "none");
        string name = "", emailId = "";
        int accept = 0;
        int InvitedUserId = Convert.ToInt32(ViewState["RegId"]);
        objdoreg.InvitedUserId = Convert.ToInt32(ViewState["RegId"]);
        objdoreg.RegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());

        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            objdoreg.RegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objdoreg.InvitedUserId = Convert.ToInt32(ViewState["RegId"]);
            DataTable dtReq = new DataTable();
            dtReq = objdareg.GetExistsRequest(objdoreg, DA_Registrationdetails.RegistrationDetails.SingleRecord);
            if (dtReq.Rows.Count > 0)
            {
                name = Convert.ToString(dtReq.Rows[0]["Name"]);
                emailId = Convert.ToString(dtReq.Rows[0]["vchrUserName"]);
                accept = Convert.ToInt32(dtReq.Rows[0]["IsAccepted"]);

                if (Convert.ToInt32(dtReq.Rows[0]["IsAccepted"]) == 0)
                {
                    if (Convert.ToInt32(dtReq.Rows[0]["Accepts"]) != 2)
                    {
                        divConnDisPopup.Style.Add("display", "none");
                        divSuccess.Style.Add("display", "block");
                        lblSuccess.Text = "You have already send the request.";
                        lblSuccess.ForeColor = System.Drawing.Color.Red;
                        divSuccess.Style.Add("display", "block");
                        return;
                    }
                    else
                    {
                        objdoreg.intRequestInvitaionId = Convert.ToInt32(dtReq.Rows[0]["intRequestInvitaionId"]);
                        objdareg.AddEditDel_Request(objdoreg, DA_Registrationdetails.RegistrationDetails.Update);
                        divConnDisPopup.Style.Add("display", "none");
                        BinduserSearch();
                        return;
                    }
                }
            }
        }
        else
        {
            return;
        }
        objdoreg.ConnectRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
        objdoreg.AddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
        objdoreg.IpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (objdoreg.IpAddress == null)
            objdoreg.IpAddress = Request.ServerVariables["REMOTE_ADDR"];

        if (Convert.ToString(ViewState["CommandName"]).Trim() == "DisConnect")
        {
            objdareg.AddEditDel_Request(objdoreg, DA_Registrationdetails.RegistrationDetails.UpdateConnection);

            if (ISAPIURLACCESSED == "1")
            {
                try
                {
                    String url = APIURL + "disconnectUser.action?" +
                                 "uidFirstUser=" + UserTypeId + objdoreg.RegistrationId +
                                 "&uidSecondUser=" + UserTypeId + objdoreg.InvitedUserId;

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                    myRequest1.Method = "GET";
                    if (ISAPIResponse == "1")
                    {
                        WebResponse myResponse1 = myRequest1.GetResponse();
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.strURL = url;
                        objAPILogDO.strAPIType = "Search People Disconnect User";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = objdoreg.IpAddress;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }

            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindSingleData();
            }
            else
            {
                BinduserSearch();
            }
            lblSuccess.Text = "Successfully removed from the friendlist.";
            lblSuccess.ForeColor = System.Drawing.Color.Green;
            divSuccess.Style.Add("display", "block");
            return;
        }
        else
        {
            objdareg.AddEditDel_Request(objdoreg, DA_Registrationdetails.RegistrationDetails.Add);

            objdoreg.RegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objdoreg.InvitedUserId = Convert.ToInt32(ViewState["RegId"]);
            DataTable dtReqs = new DataTable();
            dtReqs = objdareg.GetExistsRequest(objdoreg, DA_Registrationdetails.RegistrationDetails.SingleRecord);
            if (dtReqs.Rows.Count > 0)
            {
                name = Convert.ToString(dtReqs.Rows[0]["Name"]);
                emailId = Convert.ToString(dtReqs.Rows[0]["vchrUserName"]);
                accept = Convert.ToInt32(dtReqs.Rows[0]["IsAccepted"]);
            }

            if (ISAPIURLACCESSED == "1")
            {
                try
                {
                    String url = APIURL + "connectUser.action?" +
                                 "uidFirstUser=" + UserTypeId + objdoreg.RegistrationId +
                                 "&uidSecondUser=" + UserTypeId + objdoreg.InvitedUserId;

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                    myRequest1.Method = "GET";
                    if (ISAPIResponse == "1")
                    {
                        WebResponse myResponse1 = myRequest1.GetResponse();
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.strURL = url;
                        objAPILogDO.strAPIType = "Search People Connect User";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = objdoreg.IpAddress;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }

                }
                catch { }

            }
            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindSingleData();
            }
            else
            {
                BinduserSearch();
            }
            SendMailConnections(emailId, name, accept);
            lblSuccess.Text = "Request Invitation and a mail has been send to the person.";
            lblSuccess.ForeColor = System.Drawing.Color.Green;

        }
        divConnDisPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "block");
    }

    private void SendMailConnections(string mailid, string name, int accept)
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
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];
            string MailSSL = ConfigurationManager.AppSettings["MailSSL"];
            string MailTo = mailid;
            string Mailbody = "";

            NetworkCredential cre = new NetworkCredential(username, Password);
            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = true;
            clientip.Credentials = cre;
            if (MailSSL != "0")
                clientip.EnableSsl = true;
            try
            {

                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom);
                Rmm2.Body = Mailbody.ToString();
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("");

                htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Request Invitation</b>"
                  + "<br><br>" + "Dear " + name + " "
                  + "<br><br> You have a freind request sent by " + Session["LoginName"] + "<br><br>"
                  + "<br>Regards," + "<br>" + "Skorkel Team<br><br>"
                  + "****This is a system generated Email. Kindly do not reply****", null, "text/html");
                Rmm2.To.Clear();
                Rmm2.To.Add(MailTo);
                Rmm2.Subject = "Skorkel Request Invitation.";
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

    protected void lnkSendMess_Click(object sender, EventArgs e)
    {
        int MessId = Convert.ToInt32(ViewState["RegId"]);
        WallMessageDO.intInvitedUserId = Convert.ToInt32(ViewState["RegId"]);
        WallMessageDO.striInvitedUserId = Convert.ToString(ViewState["RegId"]);
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            WallMessageDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
        }
        else
        {
            return;
        }

        WallMessageDO.StrRecommendation = txtBody.InnerText.Trim().Replace("'", "''").Replace("\n", "<br>");
        WallMessageDO.strSubject = txtSubject.Text.Trim().Replace("'", "''").Replace("\n", "<br>");
        WallMessageDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
        WallMessageDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (WallMessageDO.strIpAddress == null)
            WallMessageDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
        WallMessageDA.Scrl_AddEditDelWallMessage(WallMessageDO, DA_WallMessage.WallMessage.AddSearchPeoplemsg);

        txtSubject.Text = "";
        txtBody.InnerText = "";
        divMessPopup.Style.Add("display", "none");
        try
        {
            string UserURL = "";
            if (ISAPIURLACCESSED == "1")
            {
                UserURL = APIURL + "massageToUser.action?" +
                           "messageByUserId=USR" + WallMessageDO.intRegistrationId +
                           "&messageToUserId=USR" + WallMessageDO.striInvitedUserId +
                           "&message=" + WallMessageDO.StrRecommendation;

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                myRequest1.Method = "GET";
                if (ISAPIResponse == "1")
                {
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();
                    objAPILogDO.strURL = UserURL;
                    objAPILogDO.strAPIType = "User Message";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDO.strIPAddress = WallMessageDO.strIpAddress;
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
            }
        }
        catch { }

        divSuccess.Style.Add("display", "block");
        lblSuccess.Text = "Message send successfully.";
        lblSuccess.ForeColor = System.Drawing.Color.Green;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    #endregion

    #region Checkbox details
    protected void Students_CheckedChange(object sender, EventArgs e)
    {
        CheckedPeople();
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }

    }

    protected void Professors_CheckedChange(object sender, EventArgs e)
    {
        CheckedPeople();
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    protected void Lawyers_CheckedChange(object sender, EventArgs e)
    {
        CheckedPeople();
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    protected void Judges_CheckedChange(object sender, EventArgs e)
    {
        CheckedPeople();
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    protected void Others_CheckedChange(object sender, EventArgs e)
    {
        CheckedPeople();
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    public void CheckedPeople()
    {
        divConnDisPopup.Style.Add("display", "none");
        divMessPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");

        hdnTempUserId.Value = "";
        hdnCurrentPage.Value = "1";

        if (ChkStudents.Checked == true)
            hdnTempUserId.Value = ",1";
        if (ChkProfessors.Checked == true)
            hdnTempUserId.Value += ",2";
        if (ChkLawyers.Checked == true)
            hdnTempUserId.Value += ",4";
        if (ChkJudges.Checked == true)
            hdnTempUserId.Value += ",5";
        if (ChkOthers.Checked == true)
            hdnTempUserId.Value += ",6";

        if (ChkStudents.Checked == false && ChkProfessors.Checked == false && ChkLawyers.Checked == false && ChkJudges.Checked == false && ChkOthers.Checked == false)
            hdnTempUserId.Value = "";
    }

    protected void imgReset_Click(object sender, ImageClickEventArgs e)
    {
        divConnDisPopup.Style.Add("display", "none");
        divMessPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");

        ChkStudents.Checked = false;
        ChkProfessors.Checked = false;
        ChkLawyers.Checked = false;
        ChkJudges.Checked = false;
        ChkOthers.Checked = false;

        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "10";
        hdnTempUserId.Value = "";

        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    #endregion

    #region Paging For All

    protected void BindRptPager(Int64 PageSize, Int64 CurrentPage, Int64 MaxCount)
    {

        if (MaxCount > 0 && CurrentPage > 0 && PageSize > 0)
        {
            Int64 DisplayPage = 10;

            Int64 totalPage = (MaxCount / PageSize) + ((MaxCount % PageSize) == 0 ? 0 : 1);

            Int64 StartPage = (((CurrentPage / DisplayPage) - ((CurrentPage % DisplayPage) == 0 ? 1 : 0)) * DisplayPage) + 1;    // ((40 /10) - 1) * 10

            Int64 EndPage = ((CurrentPage / DisplayPage) + ((CurrentPage % DisplayPage) == 0 ? 0 : 1)) * DisplayPage;

            hdnNextPage.Value = (CurrentPage + 1).ToString();
            hdnPreviousPage.Value = (CurrentPage - 1).ToString();
            hdnEndPage.Value = EndPage.ToString();
            hdnEndPage.Value = totalPage.ToString();

            if (totalPage < EndPage)
            {
                if (totalPage != StartPage)
                {
                    EndPage = totalPage;
                    hdnEndPage.Value = EndPage.ToString();
                }
                else
                {
                    StartPage = StartPage - DisplayPage;
                    StartPage++;
                    EndPage = totalPage;
                    hdnEndPage.Value = EndPage.ToString();
                }
            }
            else
            {
                if (Convert.ToInt32(hdnNextPage.Value) == totalPage)
                {
                    StartPage++;
                    EndPage = totalPage;
                    hdnEndPage.Value = EndPage.ToString();
                }
                
            }

            if (totalPage == 1)
            {
                lnkPrevious.Visible = false;
                lnkNext.Visible = false;
                rptDvPage.DataSource = null;
                rptDvPage.DataBind();
            }
            else
            {
                DataTable dtPage = new DataTable();
                DataColumn PageNo = new DataColumn();
                PageNo.DataType = System.Type.GetType("System.String");
                PageNo.ColumnName = "intPageNo";
                dtPage.Columns.Add(PageNo);

                for (Int64 i = StartPage; i <= EndPage; i++)
                {
                    dtPage.Rows.Add(i.ToString());
                    hdnLastPage.Value = i.ToString();
                }

                rptDvPage.DataSource = dtPage;
                rptDvPage.DataBind();


                if (CurrentPage > DisplayPage)
                {
                    lnkPrevious.Visible = true;
                    hdnPreviousPage.Value = (CurrentPage - 1).ToString();
                }
                else
                {
                    lnkPrevious.Visible = true;
                }
                if (totalPage > EndPage)
                {
                    lnkNext.Visible = true;
                    //hdnNextPage.Value = (EndPage + 1).ToString();
                    hdnLastPage.Value = totalPage.ToString();
                }
                else
                {
                    lnkNext.Visible = true;
                }
            }
        }

    }

    protected void lnkNext_Click(object sender, EventArgs e)
    {
        divMessPopup.Style.Add("display", "none");
        divConnDisPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");
        if (Convert.ToInt32(hdnEndPage.Value) >= Convert.ToInt32(hdnNextPage.Value))
        {
            imgPaging.Style.Add("opacity", "1.2");
            hdnCurrentPage.Value = hdnNextPage.Value;

            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindSingleData();
            }
            else
            {
                BinduserSearch();
            }
        }
        else
        {
            imgPaging.Style.Add("opacity", "1.2");
            hdnCurrentPage.Value = hdnNextPage.Value;

            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindSingleData();
            }
            else
            {
                BinduserSearch();
            }
        }
    }

    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        divMessPopup.Style.Add("display", "none");
        divConnDisPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");
        hdnCurrentPage.Value = "1";
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    protected void lnkLast_Click(object sender, EventArgs e)
    {
        divMessPopup.Style.Add("display", "none");
        divConnDisPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");
        hdnCurrentPage.Value = hdnLastPage.Value;
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        divMessPopup.Style.Add("display", "none");
        divConnDisPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");
        if (hdnPreviousPage.Value != "0")
        {
            hdnCurrentPage.Value = hdnPreviousPage.Value;

            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindSingleData();
            }
            else
            {
                BinduserSearch();
            }
        }

    }

    protected void rptDvPage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "PageLink")
        {
            divMessPopup.Style.Add("display", "none");
            divConnDisPopup.Style.Add("display", "none");
            divSuccess.Style.Add("display", "none");
            LinkButton lnkPageLink = (LinkButton)e.Item.FindControl("lnkPageLink");
            if (lnkPageLink != null)
            {
                hdnCurrentPage.Value = lnkPageLink.Text;
                lnkPageLink.Style.Add("color", "#141414 !important");
                lnkPageLink.Style.Add("text-decoration", "none !important");

                if (lnkPageLink.Text == "")
                {
                    hdnCurrentPage.Value = "1";
                }
                if (lnkPageLink.Text != "1")
                {
                    imgPaging.Style.Add("opacity", "1.2");
                }
                else
                {
                    imgPaging.Style.Add("opacity", "0.2");
                }

                hdnCurrentPage.Value = lnkPageLink.Text;
                lnkPageLink.Enabled = false;
                if (Convert.ToString(ViewState["ViewAll"]) == "1")
                {
                    BindSingleData();
                }
                else
                {
                    BinduserSearch();
                }
            }
        }
    }

    protected void rptDvPage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkPageLink = (LinkButton)e.Item.FindControl("lnkPageLink");
            if (lnkPageLink != null)
            {
                if (hdnCurrentPage.Value == lnkPageLink.Text)
                {
                    lnkPageLink.Enabled = false;
                    if (ViewState["lnkPageLink"] != null)
                    {
                        if (lnkPageLink.Text == "1")
                        {
                            ViewState["lnkPageLink"] = null;
                        }
                    }
                }
                else
                {
                    lnkPageLink.Enabled = true;
                }
                if (hdnCurrentPage.Value == "1")
                {
                    ViewState["lnkPageLink"] = "PageCount";
                }

            }


        }
    }

    #endregion

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string SaveComment1(string[] array)
    {
        return "1";
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string BindPeopleSearch(string[] array)
    {
        return "1";
    }
}