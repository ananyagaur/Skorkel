using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Web.UI;

public partial class Main : System.Web.UI.MasterPage
{
    DataTable dt = new DataTable();

    DO_Registrationdetails objRegistration = new DO_Registrationdetails();
    DA_Registrationdetails objRegistrationDB = new DA_Registrationdetails();

    DO_Scrl_UserSendMessage objMessageDO = new DO_Scrl_UserSendMessage();
    DA_Scrl_UserSendMessage objMessageDA = new DA_Scrl_UserSendMessage();

    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();

    DO_Scrl_UserRecommendation objRecmndDO = new DO_Scrl_UserRecommendation();
    DA_Scrl_UserRecommendation objRecmndDA = new DA_Scrl_UserRecommendation();

    DO_Scrl_UserGroupJoin objGrpJoinDO = new DO_Scrl_UserGroupJoin();
    DA_Scrl_UserGroupJoin objGrpJoinDA = new DA_Scrl_UserGroupJoin();

    DA_Login objLoginDB = new DA_Login();
    DO_Login objLogin = new DO_Login();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            form1.Action = QueryStringModule.Encrypt(Request.Url.Query.ToString().Replace("?", ""));
            ViewState["orgid"] = Convert.ToInt32(Request.QueryString["orgid"]);
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }
            else
            {
                Response.Redirect("Landing.aspx", true);
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());
            lblUserName.InnerText = Convert.ToString(Session["LoginName"]);
            GetLoginUserDetails();
            BindMessages();
            BindNotificationRequest();
            GetMessageNotification();
        }
        GetNotifications();

    }

    #region Notification

    protected void GetMessageNotification()
    {
        objRecmndDO.striInvitedUserId = Convert.ToString(ViewState["UserID"]);
        dt = objRecmndDA.GetDataTable(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.GetMessNotification);
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["IsRead"]) != "0")
            {
                lblInboxCount.Text = Convert.ToString(dt.Rows[0]["IsRead"]);
            }
            else
            {
                lblInboxCount.Text = "0";
            }
        }
    }

    public void GetNotifications()
    {
        BindTopNotifcations();
    }

    protected void ImgNotification_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (lblNotifyCount.Text != "0")
        {
            dvNotification.Style.Add("display", "block");
            BindTopNotifcations();
        }
    }

    protected void BindNotificationRequest()
    {
        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "500";

        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            DataTable dtSearch = objdanetwork.GetUserSearch(Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(Session["ExternalUserId"]), DA_Networks.NetworkDetails.SingleRecord);
            dt.Clear();
            objRegistration.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
            dt = objRegistrationDB.GetDataTable(objRegistration, DA_Registrationdetails.RegistrationDetails.SingleRecord);
            if (dtSearch.Rows.Count > 0)
            {
                hdnNotificationcount.Value = Convert.ToString(dtSearch.Rows[0]["Maxcount"]);

                if (dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["intNotificationCount"])))
                    {
                        int Ncount = Convert.ToInt32(Convert.ToString(dt.Rows[0]["intNotificationCount"]));
                        int MaxNCount = Convert.ToInt32(hdnNotificationcount.Value);

                        if (MaxNCount > Ncount)
                        {
                            lblNotifyCount.Text = Convert.ToString(dtSearch.Rows[0]["Maxcount"]);
                            lblNotifyCount.Text = Convert.ToString(MaxNCount - Ncount);
                        }
                        else
                        {
                            lblNotifyCount.Text = "0";
                        }
                    }
                    else
                    {
                        lblNotifyCount.Text = "0";
                    }
                }
                else
                {
                    lblNotifyCount.Text = Convert.ToString(dtSearch.Rows[0]["Maxcount"]);
                }
            }
            else
            {
                lblNotifyCount.Text = "0";
                hdnNotificationcount.Value = "0";
            }
        }
    }

    protected void BindTopNotifcations()
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "500";

            DataTable dtSearch = objdanetwork.GetUserSearch(Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(Session["ExternalUserId"]), DA_Networks.NetworkDetails.GetTopNotifications);
            if (dtSearch.Rows.Count > 0)
            {
                dvAllNotifi.Style.Add("display", "block");
                lstNotification.DataSource = dtSearch;
                lstNotification.DataBind();
            }
            else
            {
                lstNotification.DataSource = null;
                lstNotification.DataBind();
            }
            if (dtSearch.Rows.Count >= 5)
            {
                dvAllNotifi.Style.Add("display", "block");
            }
        }
        else
        {
            lstNotification.DataSource = null;
            lstNotification.DataBind();

        }
    }

    protected void BindAllRequest()
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "500";

            DataTable dtSearch = objdanetwork.GetUserSearch(Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(Session["ExternalUserId"]), DA_Networks.NetworkDetails.SingleRecord);

            if (dtSearch.Rows.Count > 0)
            {
                lstNotification.DataSource = dtSearch;
                lstNotification.DataBind();
            }
            else
            {
                lstNotification.DataSource = null;
                lstNotification.DataBind();

            }
        }
        else
        {
            lstNotification.DataSource = null;
            lstNotification.DataBind();

        }
    }

    protected void lstNotification_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnReqID = (HiddenField)e.Item.FindControl("hdnReqID");
        HiddenField hdnPkId = (HiddenField)e.Item.FindControl("hdnPkId");
        HiddenField hdnTableName = (HiddenField)e.Item.FindControl("hdnTableName");
        Label lblEmailId = (Label)e.Item.FindControl("lblEmailId");
        //Label lblDescription = (Label)e.Item.FindControl("lblDescription");
        Label lblUserType = (Label)e.Item.FindControl("lblUserType");
        Label lblGroupName = (Label)e.Item.FindControl("lblGroupName");
        HiddenField hdnrequserid = (HiddenField)e.Item.FindControl("hdnrequserid");
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegID");
        HiddenField hdnintUserTypeId = (HiddenField)e.Item.FindControl("hdnintUserTypeId");
        LinkButton lnkConfirm = (LinkButton)e.Item.FindControl("lnkConfirm");
        LinkButton lnkCancel = (LinkButton)e.Item.FindControl("lnkCancel");
        HiddenField hdnIsAccept = (HiddenField)e.Item.FindControl("hdnIsAccept");
        HiddenField hdnStrRecommendation = (HiddenField)e.Item.FindControl("hdnStrRecommendation");
        HiddenField hdnShareInvitee = (HiddenField)e.Item.FindControl("hdnShareInvitee");

        hdnEmailId.Value = lblEmailId.Text;
        ViewState["lblGroupName"] = lblGroupName.Text;

        if (e.CommandName == "Details")
        {
            if (hdnRegistrationId.Value != Convert.ToString(ViewState["UserID"]))
            {
                Response.Redirect("Notifications_Details_2.aspx");
            }
        }

        if (e.CommandName == "ShareDetails")
        {
            if (hdnTableName.Value == "Scrl_GrpShareUserStatusTbl")
            {
                Response.Redirect(hdnStrRecommendation.Value);
                return;
            }

            if (hdnRegistrationId.Value != Convert.ToString(ViewState["UserID"]))
            {
                Response.Redirect("Group-Profile.aspx?GrpId=" + hdnrequserid.Value);
            }
        }

        if (e.CommandName == "QAShareDetails")
        {
            if (hdnRegistrationId.Value != Convert.ToString(ViewState["UserID"]))
            {
                Response.Redirect(hdnStrRecommendation.Value);
            }
        }
        if (e.CommandName == "BlogShareDetails")
        {
            if (hdnRegistrationId.Value != Convert.ToString(ViewState["UserID"]))
            {
                Response.Redirect(hdnStrRecommendation.Value);
            }
        }

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallUserMethod();", true);
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
                if (lblmaster.InnerText == "Home")
                {
                    Response.Redirect("Home.aspx");
                }
            }
            else if (hdnTableName.Value == "Scrl_OrgEndorsement")
            {
                objRecmndDO.intOrgEndorseId = Convert.ToInt32(hdnPkId.Value);
                objRecmndDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objRecmndDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objRecmndDO.strIpAddress == null)
                    objRecmndDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objRecmndDA.Scrl_AddEditDelOrganization(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_OrgMessage.UpdateEnodorsement);
            }
            else if (hdnTableName.Value == "Scrl_OrgnisationGroupJoiningTbl")
            {
                if (objGrpJoinDO.strIpAddress == null)
                    objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objGrpJoinDO.intRequestJoinId = Convert.ToInt32(hdnPkId.Value);
                objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDO.isAccepted = 1;
                objGrpJoinDA.AddEditDel_Scrl_OrgnisationGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_OrgnisationGroupJoin.Update);

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
                objGrpJoinDO.intInvitedUserId = Convert.ToInt32(hdnRegistrationId.Value);
                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    ip = Request.ServerVariables["REMOTE_ADDR"];
                objGrpJoinDO.strIpAddress = ip;

                objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
                DataTable dtt = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.GetDataFrom);
                if (dtt.Rows.Count == 0)
                {
                    objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Insert);
                    objGrpJoinDO.intInvitedUserId = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intRegistrationId = Convert.ToInt32(hdnRegistrationId.Value);
                }
                else
                {
                    objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Insert);
                }
                objGrpJoinDO.intRequestJoinId = Convert.ToInt32(hdnPkId.Value);
                objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.UpdateGroupMember);

                SendMail(e.CommandName, hdnTableName.Value);
            }
            BindAllRequest();
            BindNotificationRequest();
            BindTopNotifcations();

            if (lblmaster.InnerText == "All Notifications")
            {
                Response.Redirect("Notifications_Details_2.aspx");
            }
        }

        if (e.CommandName == "Delete")
        {
            if (hdnTableName.Value == "Scrl_UserRequestInvitationTbl")
            {
                objRegistration.intRequestInvitaionId = Convert.ToInt32(hdnPkId.Value);
                objRegistration.AddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objRegistration.IpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objRegistration.IpAddress == null)
                    objRegistration.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objRegistrationDB.AddEditDel_Request(objRegistration, DA_Registrationdetails.RegistrationDetails.Delete);
                SendMail(e.CommandName, hdnTableName.Value);
            }
            else if (hdnTableName.Value == "Scrl_UserGroupJoiningTbl")
            {
                objGrpJoinDO.intRequestJoinId = Convert.ToInt32(hdnPkId.Value);
                objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDO.isAccepted = 2;
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
                objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.Delete);
                SendMail(e.CommandName, hdnTableName.Value);
            }
            else if (hdnTableName.Value == "Scrl_OrgEndorsement")
            {

                objRecmndDO.intRecommendationId = Convert.ToInt32(hdnPkId.Value);
                objRecmndDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objRecmndDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objRecmndDO.strIpAddress == null)
                    objRecmndDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.Delete);
            }
            else if (hdnTableName.Value == "Scrl_RequestGroupJoin")
            {
                objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objGrpJoinDO.strIpAddress == null)
                    objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objGrpJoinDO.intRequestJoinId = Convert.ToInt32(hdnPkId.Value);
                objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.DeleteGroupMember);
                SendMail(e.CommandName, hdnTableName.Value);
            }

            BindNotificationRequest();
            BindTopNotifcations();

            if (lblmaster.InnerText == "All Notifications")
            {
                Response.Redirect("Notifications_Details_2.aspx");
            }
        }

    }

    protected void lstNotification_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegID");
        HiddenField hdnShareInvitee = (HiddenField)e.Item.FindControl("hdnShareInvitee");
        HiddenField hdnTableName = (HiddenField)e.Item.FindControl("hdnTableName");
        HiddenField hdnStrRecommendation = (HiddenField)e.Item.FindControl("hdnStrRecommendation");
        HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
        Label lblnotificationname = (Label)e.Item.FindControl("lblnotificationname");
        Label lblGroupName = (Label)e.Item.FindControl("lblGroupName");
        LinkButton lnkConfirm = (LinkButton)e.Item.FindControl("lnkConfirm");
        LinkButton lnkCancel = (LinkButton)e.Item.FindControl("lnkCancel");
        LinkButton lnkShareDetail = (LinkButton)e.Item.FindControl("lnkShareDetail");
        Label lnkName = (Label)e.Item.FindControl("lnkName");
        Label lblComment = (Label)e.Item.FindControl("lblComment");
        HiddenField hdnIsAccept = (HiddenField)e.Item.FindControl("hdnIsAccept");
        LinkButton lnkQAshare = (LinkButton)e.Item.FindControl("lnkQAshare");
        LinkButton lnkBlogshare = (LinkButton)e.Item.FindControl("lnkBlogshare");
        HiddenField hdnintIsAccept = (HiddenField)e.Item.FindControl("hdnintIsAccept");
        LinkButton lnkConnected = (LinkButton)e.Item.FindControl("lnkConnected");
        HtmlGenericControl SearchRept = (HtmlGenericControl)e.Item.FindControl("SearchRept");
        LinkButton hyp = (LinkButton)e.Item.FindControl("hyp");

        if (hdnTableName.Value == "Scrl_UserGroupJoiningTbl")
        {
            ViewState["lblGroupNames"] = lblGroupName.Text;
            ViewState["hyp"] = hyp.Text;
            lblnotificationname.Text = "Request to join" + " " + lblGroupName.Text + " " + "group";
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
                if (ViewState["lblGroupNames"].ToString() == lblGroupName.Text && ViewState["hyp"].ToString() == hyp.Text)
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

            lblnotificationname.Text = "Request to join" + " " + lblGroupName.Text + " " + "group";
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
            lblnotificationname.Text = "Recommendation";
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

        else if (hdnTableName.Value == "Scrl_UserProfileWallTbl")
        {
            lblnotificationname.Text = "Wall Post";
        }

        else if (hdnTableName.Value == "Scrl_GroupShareTbl")
        {
            DataTable dtShare = new DataTable();
            lnkConfirm.Style.Add("display", "none");
            lnkCancel.Style.Add("display", "none");

            lblnotificationname.Text = "Share" + " " + "group";
            lnkShareDetail.Text = lblGroupName.Text;
        }
        else if (hdnTableName.Value == "Scrl_UserPostQAReplyTbl")
        {
            DataTable dtShare = new DataTable();
            lnkConfirm.Style.Add("display", "none");
            lnkCancel.Style.Add("display", "none");

            lblnotificationname.Text = "Share" + " " + "QA";
            lnkQAshare.Text = lblGroupName.Text;

        }
        else if (hdnTableName.Value == "Scrl_BlogHeadingLikeShareTbl")
        {
            DataTable dtShare = new DataTable();
            lnkConfirm.Style.Add("display", "none");
            lnkCancel.Style.Add("display", "none");

            lblnotificationname.Text = "Share" + " " + "Blog";
            lnkBlogshare.Text = lblGroupName.Text;

        }
        else if (hdnTableName.Value == "Scrl_MicrolTagLikeShareTbl")
        {
            DataTable dtShare = new DataTable();
            lnkConfirm.Style.Add("display", "none");
            lnkCancel.Style.Add("display", "none");

            lblnotificationname.Text = "Share" + " " + "Document";
            lnkBlogshare.Text = lblGroupName.Text;

        }

        else if (hdnTableName.Value == "Scrl_OrgnisationGroupJoiningTbl")
        {
            lblnotificationname.Text = "Request to join" + " " + lblGroupName.Text + " " + "Orgnisation group";
        }
        else if (hdnTableName.Value == "Scrl_GrpShareUserStatusTbl")
        {
            DataTable dtShare = new DataTable();
            lnkConfirm.Style.Add("display", "none");
            lnkCancel.Style.Add("display", "none");

            lblnotificationname.Text = "Share" + " " + "group status link";
            lnkShareDetail.Text = lblGroupName.Text;
        }
        if (imgprofile.Src == "" || imgprofile.Src == null || imgprofile.Src == "CroppedPhoto/")
        {
            imgprofile.Src = "images/profile-photo.png";
        }
        else
        {
            string imgPathPhysical = Server.MapPath("~/" + imgprofile.Src);
            if (File.Exists(imgPathPhysical))
            {

            }
            else
            {
                imgprofile.Src = "images/profile-photo.png";
            }

        }
        if (hdnRegistrationId.Value == ViewState["UserID"].ToString())
        {
            SearchRept.Style.Add("display", "none");
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

            NetworkCredential cre = new NetworkCredential(username, Password);

            string MailTo = "";
            if (TableName == "Ask For Recommendation" && status == "Confirm")
                MailTo = Convert.ToString(ViewState["AskForRecommMailId"]);
            else
                MailTo = hdnEmailId.Value;

            string Mailbody = "";


            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            if (MailSSL != "0")
                clientip.EnableSsl = true;

            clientip.Credentials = cre;
            clientip.UseDefaultCredentials = true;
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
                    Rmm2.Subject = "Skorkel Group Invitation Status.";
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

    protected void lstNotification_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {

    }

    #endregion

    protected void lnkSignOut_Click(object sender, EventArgs e)
    {
        objLogin.intRegistartionID = Convert.ToInt32(Session["ExternalUserId"]);
        if (objLogin.intRegistartionID != 0)
            objLoginDB.AddAndGetLoginDetails(objLogin, DA_SKORKEL.DA_Login.Login_1.Logout);

        Session.Abandon();
        Response.Redirect("Landing.aspx");
    }

    protected void GetLoginUserDetails()
    {
        DataTable dtDetails = new DataTable();
        objRegistration.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        dtDetails = objRegistrationDB.GetDataTable(objRegistration, DA_Registrationdetails.RegistrationDetails.GetApprovedStudentByInstitute);
        if (dtDetails.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(dtDetails.Rows[0]["vchrPhotoPath"])))
            {
                string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + dtDetails.Rows[0]["vchrPhotoPath"].ToString());
                if (File.Exists(imgPathPhysical))
                {
                    imgProfilePic.Src = "CroppedPhoto/" + dtDetails.Rows[0]["vchrPhotoPath"].ToString();
                }
                else
                {
                    imgProfilePic.Src = "images/profile-photo.png";
                }
            }
            else
            {
                imgProfilePic.Src = "images/comment-profile.jpg";
            }

        }
    }

    protected void BindMessages()
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            lblInboxCount.Text = "";
            objMessageDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
            DataTable dtmessage = objMessageDA.GetDataTable(objMessageDO, DA_Scrl_UserSendMessage.Scrl_UserSendMessage.AllRecords);
            if (dtmessage.Rows.Count > 0)
            {
                ViewState["MaxCount"] = dtmessage.Rows[0]["Maxcount"].ToString();
                lblInboxCount.Text = dtmessage.Rows[0]["Maxcount"].ToString();
            }
            else
            {
                lblInboxCount.Text = "0";

            }
        }
        else
        {
            lblInboxCount.Text = "0";
        }
    }

    protected void lstOrgName_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnOrgId = (HiddenField)e.Item.FindControl("hdnOrgId");
        LinkButton lnkOrgName = (LinkButton)e.Item.FindControl("lnkOrgName");
        string OrgName = lnkOrgName.Text;
    }

    protected void lstOrgName_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnOrgId = (HiddenField)e.Item.FindControl("hdnOrgId");
        LinkButton lnkOrgName = (LinkButton)e.Item.FindControl("lnkOrgName");
        if (e.CommandName == "OrgName")
        {
            objRegistration.RegistrationId = Convert.ToInt32(hdnOrgId.Value);
            Response.Redirect("MyOrganization.aspx?orgid=" + hdnOrgId.Value);
        }
    }
}
