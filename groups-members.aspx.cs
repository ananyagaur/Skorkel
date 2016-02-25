using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Web.UI;
using System.Web;
using System.IO;
using System.Net;

public partial class groups_members : System.Web.UI.Page
{

    DO_Scrl_UserRecommendation objRecmndDO = new DO_Scrl_UserRecommendation();
    DA_Scrl_UserRecommendation objRecmndDA = new DA_Scrl_UserRecommendation();

    DO_Scrl_UserGroupJoin objGrpJoinDO = new DO_Scrl_UserGroupJoin();
    DA_Scrl_UserGroupJoin objGrpJoinDA = new DA_Scrl_UserGroupJoin();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_UserGroupDetailTbl objDOG = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDAG = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_WallMessage WallMessageDO = new DO_WallMessage();
    DA_WallMessage WallMessageDA = new DA_WallMessage();

    DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();

    DataTable dt = new DataTable();
    string strusertypes = string.Empty;

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string UserTypeId = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //(this.Master as Main).getn();
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());
            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Groups";

            if (Request.QueryString["GrpId"] != "" && Request.QueryString["GrpId"] != null)
            {
                ViewState["intGroupId"] = Request.QueryString["GrpId"];
            }
            Session["SubmitTime"] = DateTime.Now.ToString();
            AccessModulePermisssion();
            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "12";
            BinduserSearch();

        }
        string eventArgument = Request.Form["__EVENTARGUMENT"];

        if (eventArgument != "" && eventArgument == "MyPage")
        {
            BinduserSearch();
        }
    }

    protected void AccessModulePermisssion()
    {
        objDOG.intUserId = Convert.ToInt32(ViewState["UserID"]);
        objDOG.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objgrp.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        DataSet dschk = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.ViewGrpAssignUser);
        DataTable dtRoleAP = objDAG.GetDataTable(objDOG, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpRoleRequestPermission);

        if (dschk.Tables[3].Rows.Count > 0)
        {
            hdnOwnerID.Value = Convert.ToString(dschk.Tables[3].Rows[0][0]);
            if (dschk.Tables[3].Rows[0][0].ToString() == ViewState["UserID"].ToString())
            {
                DivHome.Style.Add("display", "block");
                DivForumTab.Style.Add("display", "block");
                DivUploadTab.Style.Add("display", "block");
                DivPollTab.Style.Add("display", "block");
                DivEventTab.Style.Add("display", "block");
                DivMemberTab.Style.Add("display", "block");
                return;
            }
        }

        if (dtRoleAP.Rows.Count > 0)
        {
            if (dtRoleAP.Rows[0]["IsAccepted"].ToString() != "0" && dtRoleAP.Rows[0]["IsAccepted"].ToString() != "2")
            {
                DivHome.Style.Add("display", "block");
                DivForumTab.Style.Add("display", "block");
                DivUploadTab.Style.Add("display", "block");
                DivPollTab.Style.Add("display", "block");
                DivEventTab.Style.Add("display", "block");
                DivMemberTab.Style.Add("display", "block");
            }
            else
            {
                Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);
            }
        }
        else
        {
            Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);
        }


    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["SubmitTime"] = Session["SubmitTime"];
    }

   protected void BinduserSearch()
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            objgrp.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
            objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            DataSet dschk = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.ViewGrpAssignUser);
            if (dschk.Tables[3].Rows.Count > 0)
            {
                hdnOwnerID.Value = Convert.ToString(dschk.Tables[3].Rows[0][0]);
            }

            objGrpJoinDO.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
            objGrpJoinDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objGrpJoinDO.Currentpage = Convert.ToInt32(hdnCurrentPage.Value);
            objGrpJoinDO.PageSize = Convert.ToInt32(hdnTotalItem.Value);

            if (txtsearch.Text != "Search members")
                objGrpJoinDO.strSearch = txtsearch.Text.Trim().Replace("'", "''");
            if (Request.QueryString["searchTxt"] != "" && Request.QueryString["searchTxt"] != null)
            {
                objGrpJoinDO.strSearch = Request.QueryString["searchTxt"];
                dt.Clear();
                dt = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.SearchGroupMembers);
            }
            else
            {
                dt.Clear();
                dt = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.GroupMembers);
            }

            if (dt.Rows.Count > 0)
            {
                ViewState["MaxCount"] = dt.Rows.Count;
                hdnMaxcount.Value = dt.Rows.Count.ToString();
                if (Convert.ToInt32(ViewState["MaxCount"]) <= Convert.ToInt32(hdnTotalItem.Value))
                {
                    if (Convert.ToInt32(hdnTotalItem.Value) <= 10)
                    {
                        pLoadMore.Style.Add("display", "none");
                        lblNoMoreRslt.Visible = false;
                    }
                    else
                    {
                        pLoadMore.Style.Add("display", "none");
                        lblNoMoreRslt.Visible = true;
                    }
                }

               DataTable dtt = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.GroupMembersCount);
                lblTotalMember.Text = Convert.ToString(dtt.Rows.Count);
                lstPostUpdates.DataSource = dt;
                lstPostUpdates.DataBind();
            }
            else
            {
                lblTotalMember.Text = "0";
                lstPostUpdates.DataSource = null;
                lstPostUpdates.DataBind();
                dvPage.Visible = false;
                pLoadMore.Style.Add("display", "none");
                lblNoMoreRslt.Visible = true;
            }
        }
        else
        {
            lstPostUpdates.DataSource = null;
            lstPostUpdates.DataBind();
            dvPage.Visible = false;
        }
    }

    protected void lstPostUpdates_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        HiddenField hdnId = (HiddenField)e.Item.FindControl("hdnId");
        LinkButton lblPostlink = (LinkButton)e.Item.FindControl("lblPostlink");
        Label lblEmailId = (Label)e.Item.FindControl("lblEmailId");
        Label lblName = (Label)e.Item.FindControl("lblName");
        hdnfullname.Value = lblName.Text;
        hdnEmailId.Value = lblEmailId.Text;

        if (e.CommandName == "Accept")
        {
            int RegId = Convert.ToInt32(hdnRegistrationId.Value);
            ViewState["AccRegId"] = RegId;

            txtBody.Visible = false;
            divPopupMember.Style.Add("display", "block");
            lblMessageacc.Text = "Do you want to accept group joining request?";
            lblTitle.Text = "Connect group";
            txtBodyacc.Visible = false;
            ddlRoleDetails.Visible = false;
            tdddlRoles.Visible = false;
        }

        if (e.CommandName == "Reject")
        {
            divPopupMember.Style.Add("display", "block");
            int RegId = Convert.ToInt32(hdnRegistrationId.Value);
            ViewState["RegId"] = RegId;
            ViewState["RejRegId"] = RegId;
            txtBodyacc.Visible = false;
            lblMessageacc.Text = "Do you want to reject group joining request?";
            lblTitle.Text = "Reject Request";
            ddlRoleDetails.Visible = false;
            tdddlRoles.Visible = false;
          
        }

        if (e.CommandName == "Details")
        {
            Response.Redirect("Home.aspx?RegId=" + hdnRegistrationId.Value);
        }
        
        else if (e.CommandName == "Recommendation")
        {
            ViewState["InvitedUserId"] = hdnRegistrationId.Value;
            ViewState["intGroupId"] = Request.QueryString["GrpId"];
            dvPopup.Style.Add("display", "block");
            BinduserSearchMessage();
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
            SendMail("Ask Recommendation");
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

    protected void lstPostUpdates_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        LinkButton btnRecmnd = dataItem.FindControl("btnRecmnd") as LinkButton;
        HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");       
        LinkButton lnkAccept = (LinkButton)e.Item.FindControl("lnkAccept");
        LinkButton lnkConnected = (LinkButton)e.Item.FindControl("lnkConnected");
        HtmlTableRow tbl = (HtmlTableRow)e.Item.FindControl("tbl");
        HiddenField hdnimgprofile = e.Item.FindControl("hdnimgprofile") as HiddenField;
        LinkButton lnkRejected = (LinkButton)e.Item.FindControl("lnkRejected");

        objGrpJoinDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objGrpJoinDO.intRegistrationId = Convert.ToInt32(hdnRegistrationId.Value);
        DataTable dtConnected = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.GetConnected);
        if (dtConnected.Rows.Count > 0)
        {
            lnkConnected.Visible = true;
        }
        else
        {
            lnkConnected.Visible = false;
        }

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

        string UserID = ViewState["UserID"].ToString();
        string CreateGrpID = hdnOwnerID.Value;
       // string CreateGrpID = "570";
        if (UserID == CreateGrpID)
        {
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                objGrpJoinDO.intRegistrationId = Convert.ToInt32(hdnRegistrationId.Value);
                objGrpJoinDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
                objGrpJoinDO.PageSize = 100;
                objGrpJoinDO.Currentpage = 1;
                DataTable dtSearch = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.SingleRecord);

                if (dtSearch.Rows.Count > 0)
                {
                    if (Convert.ToString(dtSearch.Rows[0]["status"]) == "0")
                    {
                        lnkAccept.Visible = true;
                        //lnkConnected.Visible = false;
                        lnkRejected.Visible = true;
                        //btnsendreq.Visible = false;
                        btnRecmnd.Visible = false;
                    }
                    else if (Convert.ToString(dtSearch.Rows[0]["status"]) == "1")
                    {
                        lnkAccept.Visible = false;
                        //lnkConnected.Visible = true;
                        lnkRejected.Visible = true;
                        //btnsendreq.Visible = false;
                        btnRecmnd.Visible = true;
                    }
                    else if (Convert.ToString(dtSearch.Rows[0]["status"]) == "2")
                    {
                        lnkAccept.Visible = false;
                        //btnsendreq.Visible = true;
                        btnRecmnd.Visible = false;
                    }
                }
                else
                {
                    lnkAccept.Visible = false;
                    btnRecmnd.Visible = false;
                }
            }
        }
        else
        {
            objGrpJoinDO.intRegistrationId = Convert.ToInt32(hdnRegistrationId.Value);
            objGrpJoinDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objGrpJoinDO.PageSize = 100;
            objGrpJoinDO.Currentpage = 1;
            DataTable dtSearch = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.SingleRecord);

            if (dtSearch.Rows.Count > 0)
            {
                if (Convert.ToString(dtSearch.Rows[0]["status"]) == "0")
                {                    
                    tbl.Visible = false;
                }
            }
        }
    }

    #region AssignRole

    protected void GetAccessModuleDetails()
    {
        objgrp.intUserId = Convert.ToInt32(ViewState["UserID"]);
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        dt = objgrpDB.GetDataTable(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpModuleDetailsAcces);
        //Profile,Home=Wall,Uploads=Uploads,Events=Events,Discussion=Forum,Polls=Polls,Jobs=Jobs,Members=Members
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string ModuleName = Convert.ToString(dt.Rows[i]["strModuleName"]);

                switch (ModuleName)
                {
                    case "Wall": DivHome.Style.Add("display", "block");
                        break;
                    case "Uploads": DivUploadTab.Style.Add("display", "block");
                        break;
                    case "Events": DivEventTab.Style.Add("display", "block");
                        break;
                    case "Discussion": DivForumTab.Style.Add("display", "block");
                        break;
                    case "Polls": DivPollTab.Style.Add("display", "block");
                        break;                    
                    case "Members": DivMemberTab.Style.Add("display", "block");
                        break;
                }
            }

        }

    }

    #endregion

    #region Send Mails

    private void SendMail(string mailid, string name, int accept)
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
            string MailTo = mailid;
            string Mailbody = "";


            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = true;
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];

            try
            {

                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom);
                Rmm2.Body = Mailbody.ToString();
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("");
                if (accept == 1)
                {
                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel group joining request status</b>" + "<br><br>" + " " + name + " " + "Your group joining request has been accepted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                }
                else if (accept == 2)
                {
                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel group joining request status</b>" + "<br><br>" + " " + name + " " + "Your group joining request has been rejected by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                }
                Rmm2.To.Clear();
                Rmm2.To.Add(MailTo);
                Rmm2.Subject = "Skorkel group joining request status";
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

    private void SendGroupMail(string mailid, string name)
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
            //string MailTo = strEmail.Text + "," + "sjamadar@myuberall.com";
            string MailTo = mailid;
            string Mailbody = "";

            DataSet ds1 = new DataSet();
            ds1 = (DataSet)ViewState["GetOtherGroupDetailsByGroupId"];

            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = true;
            //clientip.Credentials = cre;
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];

            try
            {
                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom);
                Rmm2.Body = Mailbody.ToString();
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("");
                if (Convert.ToString(ds1.Tables[0].Rows[0]["strAccess"]) == "A")
                {
                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel group joining invitation</b>" + "<br><br>" + " " + name + " " + "Your group has been joined by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    Rmm2.Subject = "Skorkel group joining invitation";
                }
                else if (Convert.ToString(ds1.Tables[0].Rows[0]["strAccess"]) == "R")
                {
                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel group joining invitation</b>" + "<br><br>" + " " + name + " " + "You have a Skorkel group joining request posted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    Rmm2.Subject = "Skorkel group joining invitation";
                }
                else
                {
                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel group unjoining invitation</b>" + "<br><br>" + " " + name + " " + "Your group has been unjoined by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    Rmm2.Subject = "Skorkel group unjoining notification";
                }
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

    private void SendMail(string RequestType)
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
            //string MailTo = strEmail.Text + "," + "sjamadar@myuberall.com";

            string MailTo = hdnEmailId.Value;
            string Mailbody = "";


            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = true;
            //clientip.Credentials = cre;
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];

            try
            {
                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom);
                Rmm2.Body = Mailbody.ToString();
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("");
                if (RequestType == "Send Recommendation")
                {
                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Recommendation </b>" + "<br><br>" + " " + hdnfullname.Value + " " + "You have a Skorkel recommendation request posted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    Rmm2.Subject = "Skorkel Recommendation";
                }
                else if (RequestType == "Ask Recommendation")
                {
                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Ask For Recommendation </b>" + "<br><br>" + " " + hdnfullname.Value + " " + "You have been asked for a Skorkel recommendation request posted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    Rmm2.Subject = "Skorkel Recommendation";
                }
                else if (RequestType == "Send Request")
                {
                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Joining Request</b>" + "<br><br>" + " " + hdnfullname.Value + " " + "You have a Skorkel joining request posted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    Rmm2.Subject = "Skorkel Invitation";
                }
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

    #endregion

    #region Tabs

    protected void lnkProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);
    }
    protected void lnkHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Group-Home.aspx?GrpId=" + ViewState["intGroupId"]);
    }
    protected void lnkForumTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("forum-landing-page.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkUploadTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("uploads-docs-details.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkPollTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("Polls-Details.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkJobTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("jobs.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkEventTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("group-event-main.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkEventMemberTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("groups-members.aspx?GrpId=" + ViewState["intGroupId"]);
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

            if (totalPage < EndPage)
            {
                EndPage = totalPage;
            }

            if (totalPage == 1)
            {
                lnkPrevious.Visible = false;
                lnkFirst.Visible = false;

                lnkNext.Visible = false;
                lnkLast.Visible = false;

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
                }

                rptDvPage.DataSource = dtPage;
                rptDvPage.DataBind();


                if (CurrentPage > DisplayPage)
                {
                    lnkFirst.Visible = true;
                    lnkPrevious.Visible = true;
                    hdnPreviousPage.Value = (StartPage - 1).ToString();
                }
                else
                {
                    lnkPrevious.Visible = false;
                    lnkFirst.Visible = false;
                }
                if (totalPage > EndPage)
                {
                    lnkNext.Visible = true;
                    hdnNextPage.Value = (EndPage + 1).ToString();
                    hdnLastPage.Value = totalPage.ToString();
                    lnkLast.Visible = true;
                }
                else
                {
                    lnkNext.Visible = false;
                    lnkLast.Visible = false;
                }
            }
        }

    }

    protected void lnkNext_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = hdnNextPage.Value;
        BinduserSearch();
    }

    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = "1";
        BinduserSearch();
    }

    protected void lnkLast_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = hdnLastPage.Value;
        BinduserSearch();
    }

    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = hdnPreviousPage.Value;
        BinduserSearch();
    }

    protected void rptDvPage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "PageLink")
        {
            LinkButton lnkPageLink = (LinkButton)e.Item.FindControl("lnkPageLink");
            if (lnkPageLink != null)
            {
                hdnCurrentPage.Value = lnkPageLink.Text;
                lnkPageLink.Enabled = false;
                BinduserSearch();
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
                    // lnkPageLink.CssClass = "buttonPaging";
                    lnkPageLink.Enabled = false;
                }

                else
                {
                    //lnkPageLink.CssClass = "buttonActPaging";
                    lnkPageLink.Enabled = true;
                    //lnkPageLink.CssClass = lnkPageLink.CssClass.Replace("Paging", "");
                    //lnkPageLink.Attributes.Remove("class", "firstPaginFalse"); 

                }
            }
        }
    }

    #endregion

    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
       // Response.Redirect("groups-members.aspx?RejRegId=" + ViewState["RegId"] + "&GrpId=" + ViewState["intGroupId"] +"&Change=ch");
      
     
        BinduserSearch();
        string searchTxt = txtsearch.Text.Replace("'", "''");
        Response.Redirect("groups-members.aspx?Change=ch" + "&GrpId=" + ViewState["intGroupId"] + "&searchTxt=" + searchTxt);
       // Session["Change"] = "ch";
        //Response.Redirect("groups-members.aspx?Change=ch" + "&GrpId=" + ViewState["intGroupId"]);
       // Response.Redirect("groups-members.aspx?Change=ch");
    }

    protected void imgLoadMore_OnClick(object sender, EventArgs e)
    {
        int NextPage = 9, count = 0;

       for (int i = 0; i <= Convert.ToInt32(hdnTotalItem.Value); i++)
        {
            NextPage = NextPage + 1;
        }

        if (NextPage <= Convert.ToInt32(ViewState["MaxCount"]))
        {

        }

        if (NextPage <= Convert.ToInt32(ViewState["MaxCount"]) || (Convert.ToInt32(ViewState["nextValue"]) < Convert.ToInt32(ViewState["MaxCount"])))
        {
            hdnTotalItem.Value = Convert.ToString(NextPage);
            ViewState["nextValue"] = hdnTotalItem.Value;
            BinduserSearch();
            count = NextPage - 10;

            String ID = "ctl00_ContentPlaceHolder1_lstPostUpdates_ctrl" + count + "_lblPostlink";
            hdnLoader.Value = ID;

            if ((Convert.ToInt32(ViewState["nextValue"]) > Convert.ToInt32(ViewState["MaxCount"])))
            {
                pLoadMore.Style.Add("display", "none");
                lblNoMoreRslt.Visible = true;
            }
        }
        else
        {
            count = NextPage - 11;
            String ID = "ctl00_ContentPlaceHolder1_lstPostUpdates_ctrl" + count + "_lblPostlink";
            hdnLoader.Value = ID;
            pLoadMore.Style.Add("display", "none");
            lblNoMoreRslt.Visible = true;

        }
    }

    protected void lnkPopupOK_Click(object sender, EventArgs e)
    {
        WallMessageDO.intInvitedUserId = Convert.ToInt32(ViewState["InvitedUserId"]);
        WallMessageDO.striInvitedUserId = Convert.ToString(ViewState["InvitedUserId"]);
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            WallMessageDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
        }
        else
        {
            return;
        }

        WallMessageDO.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        WallMessageDO.StrRecommendation = txtBody.InnerHtml;
        WallMessageDO.strSubject = txtSubject.Text.Trim().Replace("'", "''");
        WallMessageDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
        WallMessageDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (WallMessageDO.strIpAddress == null)
            WallMessageDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
        WallMessageDA.Scrl_AddEditDelWallMessage(WallMessageDO, DA_WallMessage.WallMessage.Add);

        try
        {
            string UserURL = "";
            if (ISAPIURLACCESSED == "1")
            {
                UserURL = APIURL + "massageToUser.action?" +
                           "messageByUserId=USR" + ViewState["messageByUserId"] +
                           "&messageToUserId=USR" + ViewState["intRegistrationId"] +
                           "&message=" + WallMessageDO.StrRecommendation;

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                myRequest1.Method = "GET";
                WebResponse myResponse1 = myRequest1.GetResponse();

                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                String result = sr.ReadToEnd();

                objAPILogDO.strURL = UserURL;
                objAPILogDO.strAPIType = "Group Member";
                objAPILogDO.strResponse = result;
                objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objAPILogDO.strIPAddress = objGrpJoinDO.strIpAddress;
                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
            }
        }
        catch { }

        dvPopup.Style.Add("display", "none");
        Clear();
        //Response.Redirect("group-event-main.aspx?GrpId=" + ViewState["intGroupId"]);

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        dvPopup.Style.Add("display", "none");
    }

    protected void BinduserSearchMessage()
    {
        objGrpJoinDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objGrpJoinDO.intRegistrationId = Convert.ToInt32(ViewState["InvitedUserId"]);
        objGrpJoinDO.strSearch = "";

        dt.Clear();
        dt = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.GetPopupGroupMemberDtls);

        if (dt.Rows.Count > 0)
        {
            ViewState["messageByUserId"] = dt.Rows[0]["GroupOwnerRegId"];
            ViewState["messageToUserId"] = dt.Rows[0]["intRegistrationId"];
        }

    }

    protected void Clear()
    {
        txtSubject.Text = "";
        txtBody.InnerText = "";
    }

    String EmailId = "", Name = "", GroupName = "";

    protected void lnkPopupacc_Click(object sender, EventArgs e)
    {
        int intRequestJoinId = 0;
        if (ViewState["AccRegId"] != null)
        {

            objGrpJoinDO.intRegistrationId = Convert.ToInt32(ViewState["AccRegId"]);
            objGrpJoinDO.inGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
            objGrpJoinDO.PageSize = 100;
            objGrpJoinDO.Currentpage = 1;
            DataTable dtSearch = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.SingleRecord);

            if (dtSearch.Rows.Count > 0)
            {
                intRequestJoinId = Convert.ToInt32(dtSearch.Rows[0]["JoinId"]);

                if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
                {
                    objGrpJoinDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intRequestJoinId = intRequestJoinId;
                    objGrpJoinDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    objGrpJoinDO.isAccepted = 1;
                    if (objGrpJoinDO.strIpAddress == null)
                        objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                    objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Update);
                    //SaveMemberRole();
                    try
                    {
                        string UserURL = "";
                        string APIURL = ConfigurationManager.AppSettings["APIURL"];

                        UserURL = APIURL + "addMemberToGroup.action?" +
                               "groupId=GRP" + Request.QueryString["GrpId"] +
                               "&members=USR" + ViewState["AccRegId"];
                        
                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                        myRequest1.Method = "GET";
                        WebResponse myResponse1 = myRequest1.GetResponse();

                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = UserURL;
                        objAPILogDO.strAPIType = "Group Member";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strIPAddress = objGrpJoinDO.strIpAddress;
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);

                    }
                    catch { }
                }
                else
                {
                    return;
                }
            }

            EmailId = Convert.ToString(dtSearch.Rows[0]["vchrUserName"]);
            Name = Convert.ToString(dtSearch.Rows[0]["Name"]);
            GroupName = Convert.ToString(dtSearch.Rows[0]["strGroupName"]);
            ViewState["AccRegId"] = null;
            divPopupMember.Style.Add("display", "none");
            divSuccessAcceptMember.Style.Add("display", "block");
            lblSuccessacc.Text = "Group joining request accepted successfully and a mail is sent to the member.";
            SendGroupMail(EmailId, Name, GroupName);

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "12";
            BinduserSearch();


        }
        else if (ViewState["RejRegId"] != null)
        {
            objGrpJoinDO.intRegistrationId = Convert.ToInt32(ViewState["RejRegId"]);
            objGrpJoinDO.inGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
            objGrpJoinDO.PageSize = 100;
            objGrpJoinDO.Currentpage = 1;
            DataTable dtSearch = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.SingleRecord);

            if (dtSearch.Rows.Count > 0)
            {
                intRequestJoinId = Convert.ToInt32(dtSearch.Rows[0]["intRequestJoinId"]);

                if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
                {
                    objGrpJoinDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intRequestJoinId = intRequestJoinId;
                    objGrpJoinDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    objGrpJoinDO.isAccepted = 2;
                    // objGrpJoinDO.isAccepted = 0;
                    if (objGrpJoinDO.strIpAddress == null)
                        objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                    objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Update);
                    DeleteMemberRole();

                    try
                    {
                        string APIURL = ConfigurationManager.AppSettings["APIURL"];
                        string UserURL = "";

                        UserURL = APIURL + "removeMemberFromGroup.action?" +
                                 "groupId=GRP" + Request.QueryString["GrpId"] +
                                 "&members=USR" + ViewState["RejRegId"];

                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                        myRequest1.Method = "GET";
                        WebResponse myResponse1 = myRequest1.GetResponse();

                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = UserURL;
                        objAPILogDO.strAPIType = "Group Member";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strIPAddress = objGrpJoinDO.strIpAddress;
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                    catch { }
                }
                else
                {
                    return;
                }
            }

            // SendMail(lblEmailId.Text, lblName.Text, 2);
            divPopupMember.Style.Add("display", "none");
            divSuccessAcceptMember.Style.Add("display", "block");
            lblSuccessacc.Text = "Group joining request rejected successfully.";
            ViewState["RejRegId"] = null;
            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "12";
            BinduserSearch();

        }
        else if (ViewState["AccGrpOrgRegId"] != null)
        {
            objGrpJoinDO.intRegistrationId = Convert.ToInt32(ViewState["AccGrpOrgRegId"]);
            objGrpJoinDO.inGroupId = Convert.ToInt32(Request.QueryString["GrpId"]); //Convert.ToInt32(ViewState["intGroupId"]);
            objGrpJoinDO.intOrgnisationID = Convert.ToInt32(Request.QueryString["orgid"]);
            objGrpJoinDO.PageSize = 100;
            objGrpJoinDO.Currentpage = 1;
            DataTable dtSearch = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_OrgnisationGroupJoin.GetJoinStatus);

            if (dtSearch.Rows.Count > 0)
            {
                intRequestJoinId = Convert.ToInt32(dtSearch.Rows[0]["JoinId"]);

                if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
                {
                    objGrpJoinDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intRequestJoinId = intRequestJoinId;
                    objGrpJoinDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    objGrpJoinDO.isAccepted = 1;
                    if (objGrpJoinDO.strIpAddress == null)
                        objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                    objGrpJoinDA.AddEditDel_Scrl_OrgnisationGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_OrgnisationGroupJoin.Update);
                    //SaveMemberRole();
                }
                else
                {
                    return;
                }
            }

            divPopupMember.Style.Add("display", "none");
            divSuccessAcceptMember.Style.Add("display", "block");
            lblSuccessacc.Text = "Group joining request accepted successfully and a mail is sent to the member.";

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "12";
            BinduserSearch();

        }
        else if (ViewState["RejGrpOrgRegId"] != null)
        {

            objGrpJoinDO.intRegistrationId = Convert.ToInt32(ViewState["RejGrpOrgRegId"]);
            objGrpJoinDO.inGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
            objGrpJoinDO.intOrgnisationID = Convert.ToInt32(Request.QueryString["orgid"]);
            objGrpJoinDO.PageSize = 100;
            objGrpJoinDO.Currentpage = 1;
            DataTable dtSearch = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_OrgnisationGroupJoin.SingleRecord);

            if (dtSearch.Rows.Count > 0)
            {
                intRequestJoinId = Convert.ToInt32(dtSearch.Rows[0]["JoinId"]);

                if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
                {
                    objGrpJoinDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intRequestJoinId = intRequestJoinId;
                    objGrpJoinDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    objGrpJoinDO.isAccepted = 2;
                    if (objGrpJoinDO.strIpAddress == null)
                        objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                    objGrpJoinDA.AddEditDel_Scrl_OrgnisationGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_OrgnisationGroupJoin.Update);
                    DeleteMemberRole();
                }
                else
                {
                    return;
                }
            }
            divPopupMember.Style.Add("display", "none");
            ddlRoleDetails.Visible = false;
            tdddlRoles.Visible = false;
            divSuccessAcceptMember.Style.Add("display", "block");
            lblSuccessacc.Text = "Group joining request rejected successfully.";

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "12";
            BinduserSearch();

        }
        else if (ViewState["DisConnFrnd"] != null)
        {
            Response.Redirect("Home.aspx?RegId=" + Request.QueryString["DisConnFrnd"]);
            // Response.Redirect("MyProfile.aspx?ConnFrnd=" + Request.QueryString["ConnFrnd"]);
            divPopupMember.Style.Add("display", "none");

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "12";
            BinduserSearch();
        }
        else if (ViewState["ConnFrnd"] != null)
        {
            Response.Redirect("Home.aspx?RegId=" + Request.QueryString["ConnFrnd"]);
            divPopupMember.Style.Add("display", "none");

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "12";
            BinduserSearch();
        }
        else if (Request.QueryString["FollId"] != null)
        {
            Response.Redirect("Home.aspx?RegId=" + Request.QueryString["FollId"]);
            divPopupMember.Style.Add("display", "none");
        }
        else if (Request.QueryString["UnFollId"] != null)
        {
            Response.Redirect("Home.aspx?RegId=" + Request.QueryString["UnFollId"]);
            divPopupMember.Style.Add("display", "none");
        }
        else if (Request.QueryString["MessId"] != null)
        {
            objRecmndDO.intInvitedUserId = Convert.ToInt32(ViewState["RegId"]);
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                objRecmndDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }
            else
            {
                return;
            }

            objRecmndDO.StrRecommendation = txtBody.InnerText.Trim().Replace("'", "''").Replace("\n", "<br>");
            objRecmndDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objRecmndDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (objRecmndDO.strIpAddress == null)
                objRecmndDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.Add);
            txtBody.InnerText = "";
            Response.Redirect("Home.aspx?RegId=" + Request.QueryString["MessId"]);
            divPopupMember.Style.Add("display", "none");
        }
        else if (Request.QueryString["GrpFollId"] != null || Request.QueryString["UnGrpFollId"] != null)
        {
            Response.Redirect("SearchGroup.aspx");
            divPopupMember.Style.Add("display", "none");
        }
        else if (Request.QueryString["GrpReqJoinId"] != null || Request.QueryString["GrpAutoJoinId"] != null || Request.QueryString["GrpUnJoinId"] != null)
        {
            Response.Redirect("SearchGroup.aspx");
            divPopupMember.Style.Add("display", "none");
        }
        else if (Request.QueryString["OrgJoinId"] != null || Request.QueryString["OrgUnJoinId"] != null)
        {
            Response.Redirect("SearchOrganization.aspx");
            divPopupMember.Style.Add("display", "none");
        }
        else if (Request.QueryString["OrgFollId"] != null || Request.QueryString["UnOrgFollId"] != null)
        {
            Response.Redirect("SearchOrganization.aspx");
            divPopupMember.Style.Add("display", "none");
        }
        else if (Request.QueryString["OrgFollIdfirm"] != null || Request.QueryString["UnOrgFollIdfirm"] != null)
        {
            Response.Redirect("SearchOrganization.aspx");
            divPopupMember.Style.Add("display", "none");
        }
        else if (Request.QueryString["GrpFollowId"] != null)
        {
            int GrpFollowId = Convert.ToInt32(Request.QueryString["GrpFollowId"]);
            divPopupMember.Style.Add("display", "none");
            Response.Redirect("groups-members.aspx?GrpId=" + GrpFollowId);


        }
        else if (Request.QueryString["GrpUnFollowId"] != null)
        {
            int GrpUnFollowId = Convert.ToInt32(Request.QueryString["GrpUnFollowId"]);
            Response.Redirect("groups-members.aspx?GrpId=" + GrpUnFollowId);
            divPopupMember.Style.Add("display", "none");
        }
        else if (Request.QueryString["GrpAutoJoin"] != null)
        {
            int GrpAutoJoin = Convert.ToInt32(Request.QueryString["GrpAutoJoin"]);
            divPopupMember.Style.Add("display", "none");
            Response.Redirect("groups-members.aspx?GrpId=" + GrpAutoJoin);


        }
        else if (Request.QueryString["GrpUnJoin"] != null)
        {
            int GrpUnJoin = Convert.ToInt32(Request.QueryString["GrpUnJoin"]);
            divPopupMember.Style.Add("display", "none");
            Response.Redirect("groups-members.aspx?GrpId=" + GrpUnJoin);


        }
        else if (Request.QueryString["MessPepId"] != null)
        {

            objRecmndDO.intInvitedUserId = Convert.ToInt32(ViewState["RegId"]);
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                objRecmndDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }
            else
            {
                return;
            }

            objRecmndDO.StrRecommendation = txtBody.InnerText.Trim().Replace("'", "''").Replace("\n", "<br>");
            objRecmndDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objRecmndDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (objRecmndDO.strIpAddress == null)
                objRecmndDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.Add);
            txtBody.InnerText = "";
            Response.Redirect("SearchPeople.aspx?ViewAll=" + Request.QueryString["ViewAll"]);
            divPopupMember.Style.Add("display", "none");
        }

        //Main objMain = new Main();
        //objMain.GetNotifications();

    }

    protected void lnkcose_Click(object sender, EventArgs e)
    {
        divPopupMember.Style.Add("display", "none");
        divSuccessAcceptMember.Style.Add("display", "none");
    }

    protected void lnkSuccessFailure_Click(object sender, EventArgs e)
    {
        divSuccessAcceptMember.Style.Add("display", "none");
    }

    private void SendGroupMail(string mailid, string name, string groupname)
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


            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = true;
            if (MailSSL != "0")
                clientip.EnableSsl = true;


            try
            {
                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom);
                Rmm2.Body = Mailbody.ToString();
                //FOR AUTO-JOIN
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel group joining invitation</b>" + "<br><br>" + " " + name + " " + "You are successfuly join the" + " " + groupname + " " + "group" + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");

                //FOR REQUEST-To-JOIN
                //System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel group joining invitation</b>" + "<br><br>" + " " + name + " " + "You have a Skorkel group joining request posted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                Rmm2.To.Clear();
                Rmm2.To.Add(MailTo);
                Rmm2.Subject = "Skorkel group joining invitation";
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

    protected void DeleteMemberRole()
    {
        objDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        objDO.strIpAddress = ip;

        objDO.inGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);

        if (Request.QueryString["RejRegId"] != null)
        {
            objDO.inviteMembers =Convert.ToString(ViewState["RejRegId"]);
            objDA.AddEditDel_Scrl_UserGroupDetailTbl(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.DeleteGrpInvities);
        }
        else
        {
            objDO.inviteMembers = Convert.ToString(ViewState["RejGrpOrgRegId"]);
            objDA.AddEditDel_Scrl_OrgGroupDetailTbl(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_OrgGroupDetailTbl.DeleteGrpInvities);
        }
    }



}