using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class forum_landing_page : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DO_Scrl_UserForumPosting objDOBForumPosting = new DO_Scrl_UserForumPosting();
    DA_Scrl_UserForumPosting objDAForumPosting = new DA_Scrl_UserForumPosting();

    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();
    String frndIds = "";

    DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "10";

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
            AccessModulePermisssion();
            GetForumDetails();
        }
    }

    protected void AccessModulePermisssion()
    {

        objDO.intUserId = Convert.ToInt32(ViewState["UserID"]);
        objDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objgrp.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        DataSet dschk = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.ViewGrpAssignUser);
        DataTable dtRoleAP = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpRoleRequestPermission);
        if (dschk.Tables[3].Rows.Count > 0)
        {
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

    protected void GetForumDetails()
    {
        objdonetwork.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
        dt = objdanetwork.GetUserConnections(objdonetwork, DA_Networks.NetworkDetails.ConnectedUsers);
        if (dt.Rows.Count > 0)
        {
            ViewState["FriendList"] = dt;
        }

        DataTable dtfriendlst = new DataTable();
        if (ViewState["FriendList"] != null)
        {
            dtfriendlst = (DataTable)ViewState["FriendList"];

            if (dtfriendlst.Rows.Count > 0)
            {
                for (int i = 0; i < dtfriendlst.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(frndIds))
                        frndIds = Convert.ToString(dtfriendlst.Rows[i]["intInvitedUserId"]);
                    else
                        frndIds += "," + Convert.ToString(dtfriendlst.Rows[i]["intInvitedUserId"]);
                }
            }
        }

        objDOBForumPosting.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        objDOBForumPosting.strFriendList = frndIds + "," + objdonetwork.RegistrationId;
        objDOBForumPosting.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        objDOBForumPosting.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objDOBForumPosting.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objDOBForumPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAForumPosting.GetDataTable(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.GetForumDetails);
        if (dt.Rows.Count > 0)
        {
            divheight.Style.Add("height", "auto");
            ViewState["MaxCount"] = dt.Rows[0]["Maxcount"];
            hdnMaxcount.Value = dt.Rows[0]["Maxcount"].ToString();
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
            lstForumDetails.DataSource = dt;
            lstForumDetails.DataBind();
        }
        else
        {
            lstForumDetails.DataSource = null;
            lstForumDetails.DataBind();
            pLoadMore.Style.Add("display", "none");
            lblNoMoreRslt.Visible = false;
            divheight.Style.Add("height", "400px");
        }

    }

    #region AssignRole

    protected void GetAccessModuleDetails()
    {
        objDO.intUserId = Convert.ToInt32(ViewState["UserID"]);
        objDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        dt = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpModuleDetailsAcces);
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

    protected void lstForumDetails_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        LinkButton lnkTitle = (LinkButton)e.Item.FindControl("lnkTitle");
        Label lblDescrption = (Label)e.Item.FindControl("lblDescrption");
        HiddenField hdnForumPostId = (HiddenField)e.Item.FindControl("hdnForumPostId");
        Label lblreply = (Label)e.Item.FindControl("lblreply");
        HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
        LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete");

        // lnkEdit lnkDelete
        string UserID = ViewState["UserID"].ToString();
        if (hdnRegistrationId.Value == UserID)
        {
            lnkEdit.Visible = true;
            lnkDelete.Visible = true;
        }
        else
        {
            lnkEdit.Visible = false;
            lnkDelete.Visible = false;
        }

        objDOBForumPosting.intForumPostingId = Convert.ToInt32(hdnForumPostId.Value);
        dt = objDAForumPosting.GetDataTable(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.GetTotalLikeByById);
        if (dt.Rows.Count > 0)
        {
            int TotalReply = Convert.ToInt32(dt.Rows[0]["TotalReply"]);
            lblreply.Text = Convert.ToString(TotalReply);
        }
        else
        {
            lblreply.Text = "";
        }
    }

    protected void lnkcreateForum_Click(object sender, EventArgs e)
    {
        Response.Redirect("create-forum.aspx?GrpId=" + ViewState["intGroupId"]);
    }

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

    protected void lnkForum_Click(object sender, EventArgs e)
    {
        Response.Redirect("forum-detail.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lstForumDetails_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnForumPostId = (HiddenField)e.Item.FindControl("hdnForumPostId");
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        HiddenField hdnSharePostuserId = (HiddenField)e.Item.FindControl("hdnSharePostuserId");
        LinkButton lnkTitle = (LinkButton)e.Item.FindControl("lnkTitle");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
        LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete");
        if (e.CommandName == "Forum")
        {
            Response.Redirect("forum-detail.aspx?GrpId=" + ViewState["intGroupId"] + "&ForumId=" + hdnForumPostId.Value);
        }
        else
        if (e.CommandName == "Details")
        {
            Response.Redirect("MyprofileWall.aspx?RegId=" + hdnRegistrationId.Value);

        }
        else
        if (e.CommandName == "SharedDetails")
        {
            Response.Redirect("MyprofileWall.aspx?RegId=" + hdnSharePostuserId.Value);
        }
        else
        if (e.CommandName == "EditForum")
        {
            //lnkEdit.Style.Add("box-shadow","0px 0px 5px #00B7E5");
            Response.Redirect("create-forum.aspx?GrpId=" + ViewState["intGroupId"] + "&ForumId=" + hdnForumPostId.Value);

        }
        else
        if (e.CommandName == "DeleteForum")
        {
            //lnkDelete.Style.Add("box-shadow", "0px 0px 5px #00B7E5");
            ViewState["hdnForumPostId"] = Convert.ToInt32(hdnForumPostId.Value);
            ViewState["lnkTitle"] = lnkTitle.Text;
            divDeletesucess.Style.Add("display", "block");
        }
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {

        objDOBForumPosting.intForumPostingId = Convert.ToInt32(ViewState["hdnForumPostId"]);
        objDAForumPosting.AddEditDel_Scrl_UserForumPostingTbl(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.DeleteForum);

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(ViewState["hdnForumPostId"]);
        objLog.strAction = "Group Forum";
        objLog.strActionName = ViewState["lnkTitle"].ToString();
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 4;   // Group Forum
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
        GetForumDetails();
        divDeletesucess.Style.Add("display", "none");
    }

    protected void lnkDeleteCancel_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
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
            GetForumDetails();
            count = NextPage - 10;

            String ID = "ctl00_ContentPlaceHolder1_lstForumDetails_ctrl" + count + "_lnkShareTitle";
            hdnLoader.Value = ID;

            String Id = "ctl00_ContentPlaceHolder1_lstForumDetails_ctrl" + count + "_lnkTitle";
            hdnLoadernew.Value = Id;

            if ((Convert.ToInt32(ViewState["nextValue"]) > Convert.ToInt32(ViewState["MaxCount"])))
            {
                pLoadMore.Style.Add("display", "none");
                lblNoMoreRslt.Visible = true;
            }
        }
        else
        {
            count = NextPage - 11;
            String ID = "ctl00_ContentPlaceHolder1_lstForumDetails_ctrl" + count + "_lnkShareTitle";
            hdnLoader.Value = ID;
            String Id = "ctl00_ContentPlaceHolder1_lstForumDetails_ctrl" + count + "_lnkTitle";
            hdnLoadernew.Value = Id;
            //imgLoadMore.Enabled = false; 
            pLoadMore.Style.Add("display", "none");
            lblNoMoreRslt.Visible = true;

        }
    }

    protected void lstForumDetails_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {

    }

    protected void lnkConnDisconn_Click(object sender, EventArgs e)
    {
        objDOBForumPosting.intForumPostingId = Convert.ToInt32(ViewState["hdnForumPostId"]);
        objDAForumPosting.AddEditDel_Scrl_UserForumPostingTbl(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.DeleteForum);
        GetForumDetails();

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(ViewState["hdnForumPostId"]);
        objLog.strAction = "Group Forum";
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 4;   // Group Forum
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);

        divCancelPoll.Style.Add("display", "none");
    }
}