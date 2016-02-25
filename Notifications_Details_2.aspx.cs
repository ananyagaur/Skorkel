using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;

public partial class Notifications_Details_2 : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    DO_Registrationdetails objRegistration = new DO_Registrationdetails();
    DA_Registrationdetails objRegistrationDB = new DA_Registrationdetails();

    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();

    DO_Scrl_UserRecommendation objRecmndDO = new DO_Scrl_UserRecommendation();
    DA_Scrl_UserRecommendation objRecmndDA = new DA_Scrl_UserRecommendation();

    DO_Scrl_UserGroupJoin objGrpJoinDO = new DO_Scrl_UserGroupJoin();
    DA_Scrl_UserGroupJoin objGrpJoinDA = new DA_Scrl_UserGroupJoin();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "All Notifications";
            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "10";
            BindAllRequest();
        }
    }

    protected void BindAllRequest()
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            objdonetwork.CurrentPage = hdnCurrentPage.Value;
            objdonetwork.CurrentPageSize = hdnTotalItem.Value;
            objdonetwork.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
            DataTable dtSearch = objdanetwork.GetDatatable(objdonetwork, DA_Networks.NetworkDetails.GetNotificationDate);
            if (dtSearch.Rows.Count > 0)
            {
                ViewState["MaxCount"] = dtSearch.Rows[0]["MaxCount"];
                hdnMaxcount.Value = dtSearch.Rows[0]["Maxcount"].ToString();
                lstMainMyTag.DataSource = dtSearch;
                lstMainMyTag.DataBind();
                if (dtSearch.Rows.Count < 4)
                {
                    divheight.Style.Add("height", "400px");
                }
                else
                {
                    divheight.Style.Add("height", "auto");
                }
            }
            else
            {
                lstMainMyTag.DataSource = null;
                lstMainMyTag.DataBind();
                divheight.Style.Add("height", "400px");
            }
        }
        else
        {
            lstMainMyTag.DataSource = null;
            lstMainMyTag.DataBind();

        }
    }
      
    protected void lstMainMyTag_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //DataTable dtMain = new DataTable();
        DataTable dtchild = new DataTable();
        //HiddenField hdnId = (HiddenField)e.Item.FindControl("hdnId");
        Label lblAddedOn = (Label)e.Item.FindControl("lblAddedOn");       
        ListView lstChildMyTag = (ListView)e.Item.FindControl("lstChildMyTag");
        objdonetwork.NotificationDate =Convert.ToString(lblAddedOn.Text.Trim());
        objdonetwork.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);       
        dtchild = objdanetwork.GetDatatable(objdonetwork, DA_Networks.NetworkDetails.GetAllNotificationByDate);
        if (lblAddedOn.Text == DateTime.Today.ToString("dd MMM yyyy"))
        {
            lblAddedOn.Text = "Today";
        }
        else if (lblAddedOn.Text == DateTime.Today.AddDays(-1).ToString("dd MMM yyyy"))
        {
            lblAddedOn.Text = "Yesterday";
        }

        if (dtchild.Rows.Count > 0)
        {
            lstChildMyTag.DataSource = dtchild;
            lstChildMyTag.DataBind();
        }
        else
        {
            lstChildMyTag.DataSource = "";
            lstChildMyTag.DataBind();
        }
    }

    protected void lstMainMyTag_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        ListView lstChildMyTag = e.Item.FindControl("lstChildMyTag") as ListView;
        lstChildMyTag.ItemCommand += new EventHandler<ListViewCommandEventArgs>(lstChildMyTag_ItemCommand);
        lstChildMyTag.ItemDataBound += new EventHandler<ListViewItemEventArgs>(lstChildMyTag_ItemDataBound);
    }

    protected void lstChildMyTag_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnTableName = (HiddenField)e.Item.FindControl("hdnTableName");
        HiddenField hdnShareInvitee = (HiddenField)e.Item.FindControl("hdnShareInvitee");
        HiddenField hdnStrRecommendation = (HiddenField)e.Item.FindControl("hdnStrRecommendation");
        Label lblGroupName = (Label)e.Item.FindControl("lblGroupName");
        HiddenField hdnIsAccept = (HiddenField)e.Item.FindControl("hdnIsAccept");
        LinkButton lnkstrLink = (LinkButton)e.Item.FindControl("lnkstrLink");
        HiddenField hdnintIsAccept = (HiddenField)e.Item.FindControl("hdnintIsAccept");
        HtmlGenericControl SearchRept = (HtmlGenericControl)e.Item.FindControl("SearchRept");

        if (hdnTableName.Value == "Scrl_UserGroupJoiningTbl")
        {
            ViewState["lblGroupNames"] = lblGroupName.Text;
            ViewState["lnkName"] = lnkName.Text;
            lblnotificationname.Text = "Request to join" + " <b>" + lblGroupName.Text + " " + "group</b>";
            if (hdnIsAccept.Value == "2")
            {
                lnkConfirm.Visible = false;
                lnkCancel.Visible = false;
            }
            if (hdnIsAccept.Value == "1")
            {
                lnkConfirm.Visible = false;
                lnkCancel.Visible = false;
                lnkConnected.Visible = true;
            }
        }
        if (hdnTableName.Value == "Scrl_RequestGroupJoin")
        {
            if (ViewState["lblGroupNames"] != null)
            {
                if (ViewState["lblGroupNames"].ToString() == lblGroupName.Text && ViewState["lnkName"].ToString() == lnkName.Text)
                {
                    SearchRept.Style.Add("display", "none");
                }
            }
            if (hdnintIsAccept.Value == "1")
            {
                lnkConfirm.Visible = false;
                lnkCancel.Visible = false;
                lnkConnected.Visible = true;
            }
            lblnotificationname.Text = "Request to join" + " <b>" + lblGroupName.Text + " " + "group</b>";

        }
        else if (hdnTableName.Value == "Scrl_UserRequestInvitationTbl")
        {
            lblnotificationname.Text = "Request Invitation";
            if (hdnIsAccept.Value == "1")
            {
                lnkConfirm.Visible = false;
                lnkCancel.Visible = false;
                lnkConnected.Visible = true;
            }

        }
        else if (hdnTableName.Value == "Scrl_UserRecommendationTbl")
        {
            lblComment.Visible = true;
            lblComment.Text = Convert.ToString(hdnStrRecommendation.Value);
            lblnotificationname.Text = "Recommendation Skill";
            if (hdnIsAccept.Value == "2")
            {
                lnkConfirm.Visible = false;
                lnkCancel.Visible = false;
            }

            lnkConfirm.Visible = false;
            lnkCancel.Visible = false;
        }
        else if (hdnTableName.Value == "Scrl_UserRecommendationChildTbl")
        {
            lblnotificationname.Text = "Ask for Recommendation";
        }
        else if (hdnTableName.Value == "Scrl_OrgnisationGroupJoiningTbl")
        {
            lblnotificationname.Text = "Request to join" + " " + lblGroupName.Text + " " + "Orgnisation group";
        }

        else if (hdnTableName.Value == "Scrl_UserProfileWallTbl")
        {
            lblnotificationname.Text = "Wall Post";
        }
        else if (hdnTableName.Value == "Scrl_GroupShareTbl")
        {
            lnkstrLink.ToolTip = "View Group";
            DataTable dtShare = new DataTable();
            lnkConfirm.Style.Add("display", "none");
            lnkCancel.Style.Add("display", "none");

            lblnotificationname.Text = "Share group";
            lnkShareDetail.Text = lblGroupName.Text;

            String TotalShareInvitee = hdnShareInvitee.Value;
            string[] InviteeID = TotalShareInvitee.Split(',');
        }
        else if (hdnTableName.Value == "Scrl_UserPostQAReplyTbl")
        {
            DataTable dtShare = new DataTable();
            lnkConfirm.Style.Add("display", "none");
            lnkCancel.Style.Add("display", "none");

            lblnotificationname.Text = "Share QA";
            lnkShareDetail.Text = lblGroupName.Text;

        }
        else if (hdnTableName.Value == "Scrl_GrpShareUserStatusTbl")
        {
            DataTable dtShare = new DataTable();
            lnkConfirm.Style.Add("display", "none");
            lnkCancel.Style.Add("display", "none");

            lblnotificationname.Text = "Share group status link";
            lnkShareDetail.Text = lblGroupName.Text;

        }
        else if (hdnTableName.Value == "Scrl_MicrolTagLikeShareTbl")
        {
            DataTable dtShare = new DataTable();
            lnkConfirm.Style.Add("display", "none");
            lnkCancel.Style.Add("display", "none");

            lblnotificationname.Text = "Share Document";
            lnkShareDetail.Text = lblGroupName.Text;

        }
        else if (hdnTableName.Value == "Scrl_BlogHeadingLikeShareTbl")
        {
            DataTable dtShare = new DataTable();
            lnkConfirm.Style.Add("display", "none");
            lnkCancel.Style.Add("display", "none");

            lblnotificationname.Text = "Share Blog";
            lnkShareDetail.Text = lblGroupName.Text;
            lnkstrLink.ToolTip = "View Blog";
        }
    }

    protected void lstChildMyTag_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPkId = (HiddenField)e.Item.FindControl("hdnPkId");
        HiddenField hdnTableName = (HiddenField)e.Item.FindControl("hdnTableName");
        Label lblEmailId = (Label)e.Item.FindControl("lblEmailId");
        Label lblGroupName = (Label)e.Item.FindControl("lblGroupName");
        HiddenField hdnrequserid = (HiddenField)e.Item.FindControl("hdnrequserid");
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegID");
        LinkButton lnkstrLink = (LinkButton)e.Item.FindControl("lnkstrLink");
        HiddenField hdnShareInvitee = (HiddenField)e.Item.FindControl("hdnShareInvitee");
        ViewState["lblGroupName"] = lblGroupName.Text;
        hdnEmailId.Value = lblEmailId.Text;

        if (e.CommandName == "Details")
        {
            Response.Redirect(lnkstrLink.Text);
        }
        else
        if (e.CommandName == "ShareDetails")
        {
            if (hdnRegistrationId.Value != Convert.ToString(ViewState["UserID"]))
            {
                    Response.Redirect("Group-Home.aspx?GrpId=" + hdnrequserid.Value);
            }
        }
        else
        if (e.CommandName == "Confirm")
        {
            if (hdnTableName.Value == "Scrl_UserRequestInvitationTbl")
            {
                objRegistration.intRequestInvitaionId = Convert.ToInt32(hdnPkId.Value);
                objRegistration.AddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objRegistration.IpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objRegistration.IpAddress == null)
                    objRegistration.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objRegistrationDB.AddEditDel_Request(objRegistration, DA_Registrationdetails.RegistrationDetails.Update);
                SendMail(e.CommandName, hdnTableName.Value);
            }

            else if (hdnTableName.Value == "Scrl_UserGroupJoiningTbl")
            {
                objGrpJoinDO.intRequestJoinId = Convert.ToInt32(hdnPkId.Value);
                objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDO.isAccepted = 1;
                objGrpJoinDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objGrpJoinDO.strIpAddress == null)
                    objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Update);
                SendMail(e.CommandName, hdnTableName.Value);
            }

            else if (hdnTableName.Value == "Scrl_UserRecommendationTbl")
            {
                objRecmndDO.intRecommendationId = Convert.ToInt32(hdnPkId.Value);
                objRecmndDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objRecmndDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objRecmndDO.strIpAddress == null)
                    objRecmndDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.Update);

                SendMail(e.CommandName, hdnTableName.Value);
            }
            else if (hdnTableName.Value == "Scrl_RequestGroupJoin")
            {
                objgrp.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objgrp.inGroupId = Convert.ToInt32(hdnShareInvitee.Value);
                DataSet ds = new DataSet();
                ds = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetOtherGroupDetailsByGroupId);

                if (Convert.ToString(ds.Tables[0].Rows[0]["strAccess"]) == "A")
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "0")
                        {
                            objGrpJoinDO.isAccepted = 1;
                        }

                        else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "1")
                        {
                            objGrpJoinDO.isAccepted = 2;
                        }

                        else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "2")
                        {
                            objGrpJoinDO.isAccepted = 1;
                        }
                    }
                    else
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["strAccess"]) == "A")
                        {
                            objGrpJoinDO.isAccepted = 1;
                        }
                    }
                }

                else if (Convert.ToString(ds.Tables[0].Rows[0]["strAccess"]) == "R")
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "0")
                        {
                            objGrpJoinDO.isAccepted = 1;

                            lblSuccess.Text = "You have already send the group joining request.";
                            lblSuccess.ForeColor = System.Drawing.Color.Red;
                            divSuccess.Style.Add("display", "block");
                            return;
                        }

                        else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "1")
                        {
                            objGrpJoinDO.isAccepted = 2;
                        }

                        else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "2")
                        {
                            objGrpJoinDO.isAccepted = 0;
                        }
                    }
                }

                objGrpJoinDO.isAccepted = 1;
                objGrpJoinDO.inGroupId = Convert.ToInt32(hdnShareInvitee.Value);
                objGrpJoinDO.intInvitedUserId = Convert.ToInt32(ds.Tables[0].Rows[0]["intRegistrationId"]);

                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    ip = Request.ServerVariables["REMOTE_ADDR"];
                objGrpJoinDO.strIpAddress = ip;
                objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Insert);
                objGrpJoinDO.intRequestJoinId = Convert.ToInt32(hdnPkId.Value);
                objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.UpdateGroupMember);
            }

            BindAllRequest();
        }
        else
        if (e.CommandName == "Delete")
        {
            ViewState["hdnTableName"] = hdnTableName.Value;
            ViewState["intRequestInvitaionId"] = hdnPkId.Value;
            divSuccess.Style.Add("display", "block");
        }
    }

    protected void lnkCancels_Click(object sender, EventArgs e)
    {
        if (ViewState["hdnTableName"].ToString() == "Scrl_UserRequestInvitationTbl")
        {
            objRegistration.intRequestInvitaionId = Convert.ToInt32(ViewState["intRequestInvitaionId"]);
            objRegistration.AddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objRegistration.IpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (objRegistration.IpAddress == null)
                objRegistration.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objRegistrationDB.AddEditDel_Request(objRegistration, DA_Registrationdetails.RegistrationDetails.Delete);
        }
        else if (ViewState["hdnTableName"].ToString() == "Scrl_UserGroupJoiningTbl")
        {
            objGrpJoinDO.intRequestJoinId = Convert.ToInt32(ViewState["intRequestInvitaionId"]);
            objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objGrpJoinDO.isAccepted = 2;
            objGrpJoinDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (objGrpJoinDO.strIpAddress == null)
                objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Update);
        }

        else if (ViewState["hdnTableName"].ToString() == "Scrl_UserRecommendationTbl")
        {
            objRecmndDO.intRecommendationId = Convert.ToInt32(ViewState["intRequestInvitaionId"]);
            objRecmndDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objRecmndDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (objRecmndDO.strIpAddress == null)
                objRecmndDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.Delete);
        }
        else if (ViewState["hdnTableName"].ToString() == "Scrl_OrgnisationGroupJoiningTbl")
        {
            if (objGrpJoinDO.strIpAddress == null)
                objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objGrpJoinDO.intRequestJoinId = Convert.ToInt32(ViewState["intRequestInvitaionId"]);
            objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objGrpJoinDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objGrpJoinDO.isAccepted = 1;
            objGrpJoinDA.AddEditDel_Scrl_OrgnisationGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_OrgnisationGroupJoin.Update);

        }
        else if (ViewState["hdnTableName"].ToString() == "Scrl_RequestGroupJoin")
        {
            objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objGrpJoinDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (objGrpJoinDO.strIpAddress == null)
                objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objGrpJoinDO.intRequestJoinId = Convert.ToInt32(ViewState["intRequestInvitaionId"]);
            objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.DeleteGroupMember);
        }

        SendMail("Delete", ViewState["hdnTableName"].ToString());
        divSuccess.Style.Add("display", "none");
        BindAllRequest();

    }

    protected void lstChildMyTag_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {

    }

    protected void imgLoadMore_OnClick(object sender, EventArgs e)
    {
        int NextPage = 9, count = 0;

        for (int i = 0; i <= Convert.ToInt32(hdnTotalItem.Value); i++)
        {
            NextPage = NextPage + 1;
        }
        if (NextPage <= Convert.ToInt32(ViewState["MaxCount"]) || (Convert.ToInt32(ViewState["nextValue"]) < Convert.ToInt32(ViewState["MaxCount"])))
        {
            hdnTotalItem.Value = Convert.ToString(NextPage);
            ViewState["nextValue"] = hdnTotalItem.Value;
            BindAllRequest();
            count = NextPage - 10;
            if ((Convert.ToInt32(ViewState["nextValue"]) > Convert.ToInt32(ViewState["MaxCount"])))
            {
                pLoadMore.Style.Add("display", "none");
                lblNoMoreRslt.Visible = true;
            }
        }
        else
        {
            count = NextPage - 11;
            pLoadMore.Style.Add("display", "none");
            lblNoMoreRslt.Visible = true;

        }
    }

    private void SendMail(string status, string TableName)
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

            string MailTo = "";
            if (TableName == "Ask For Recommendation" && status == "Confirm")
                MailTo = Convert.ToString(ViewState["AskForRecommMailId"]);
            else
                MailTo = hdnEmailId.Value;

            string Mailbody = "";

            NetworkCredential cre = new NetworkCredential(username, Password);
            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = true;
            clientip.Credentials = cre;
            if (MailSSL != "0")
                clientip.EnableSsl = true;

            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];

            try
            {

                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom);
                Rmm2.Body = Mailbody.ToString();
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("");
                if (TableName == "Scrl_UserRequestInvitationTbl")
                {
                    if (status == "Confirm")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Request Invitation Status</b>"
                           + "<br><br>" + " "
                           + "Your Request Invitation has been accepted by " + Session["LoginName"] + ".<br><br><br>"
                           + "Regards," + "<br>" + "Skorkel Team"
                           + "<br><br>****This is a system generated Email. Kindly do not reply****", null, "text/html");
                    }
                    else if (status == "Delete")
                    {
                        return;
                    }
                    Rmm2.Subject = "Skorkel Request Invitation Status.";
                }
                else if (TableName == "Scrl_UserRecommendationTbl")
                {
                    if (status == "Confirm")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Recommendation Status</b>" + "<br><br>" + " " + "Your request for recommendation has been accepted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    }
                    else if (status == "Delete")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Recommendation Status</b>" + "<br><br>" + " " + "Your request for recommendation has not been accepted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    }
                    Rmm2.Subject = "Skorkel Recommendation Status";
                }

                else if (TableName == "Scrl_UserGroupJoiningTbl")
                {
                    if (status == "Confirm")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Group Joining Status</b>"
                           + "<br><br>" + " " + "Your request to join " + ViewState["lblGroupName"] + " group has been accepted by "
                           + Session["LoginName"] + "<br><br><br>" + "Thanks,"
                           + "<br>" + "Skorkel Team"
                           + "<br><br>****This is a system generated Email. Kindly do not reply**** ", null, "text/html");

                    }
                    else if (status == "Delete")
                    {
                        return;
                    }
                    Rmm2.Subject = "Skorkel Group Joining Status.";
                }
                else if (TableName == "Scrl_UserRecommendationChildTbl")
                {
                    if (status == "Confirm")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Asked Recommendation Status</b>" + "<br><br>" + " " + "Your request for recommendation has been accepted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    }
                    else if (status == "Delete")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Asked Recommendation Status</b>" + "<br><br>" + " " + "Your request for recommendation has not been accepted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    }
                    Rmm2.Subject = "Skorkel Asked Recommendation Status";
                }
                else if (TableName == "Ask For Recommendation")
                {
                    if (status == "Confirm")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Ask For Recommendation Status</b>" + "<br><br>" + " " + Convert.ToString(ViewState["AskForRecommName"]) + " " + "This is a replay for your asked recommendation request,which is accepted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    }
                    else if (status == "Delete")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Ask Recommendation Status</b>" + "<br><br>" + " " + "Your request for recommendation has not been accepted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    }
                    Rmm2.Subject = "Skorkel Asked Recommendation ";
                }
                else if (TableName == "Scrl_RequestGroupJoin")
                {
                    if (status == "Confirm")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Group Invitation Status</b>"
                            + "<br><br>Your invitation of " + ViewState["lblGroupName"] + " group joining has been accepted by "
                            + Session["LoginName"] + "<br><br>"

                            + "Regards," + "<br>" + "Skorkel Team"
                            + "<br><br>****This is a system generated Email. Kindly do not reply**** ", null, "text/html");
                    }
                    else if (status == "Delete")
                    {
                        return;
                    }
                    Rmm2.Subject = "Skorkel Group Invitation Status. ";
                }
                Rmm2.To.Clear();
                Rmm2.To.Add(MailTo);

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
}