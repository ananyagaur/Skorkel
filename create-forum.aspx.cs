using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Web.UI;
using System.Net;
using System.IO;
public partial class create_forum : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();

    DO_Scrl_UserForumPosting objDOBForumPosting = new DO_Scrl_UserForumPosting();
    DA_Scrl_UserForumPosting objDAForumPosting = new DA_Scrl_UserForumPosting();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();

    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];

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
            masterlbl.InnerText = "Groups";

            if (Request.QueryString["GrpId"] != "" && Request.QueryString["GrpId"] != null)
            {
                ViewState["intGroupId"] = Request.QueryString["GrpId"];
            }

            AccessModulePermisssion();

            lnkUpdate.Visible = false;
         
            if (Convert.ToString(Request.QueryString["ForumId"]) != "" && Request.QueryString["ForumId"] != null)
            {
                int ForumeID =Convert.ToInt32( Request.QueryString["ForumId"]);
                EditForum(ForumeID);
                hdnForumID.Value = ForumeID.ToString();
                btnSave.Visible = false;
                lnkUpdate.Visible = true;
                lblcreatefrm.Text = "Edit Forum";
            }

            GetAssignRole();
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
                // divSecondWall.Style.Add("display", "block");
                DivHome.Style.Add("display", "block");
                DivForumTab.Style.Add("display", "block");
                DivUploadTab.Style.Add("display", "block");
                DivPollTab.Style.Add("display", "block");
                DivEventTab.Style.Add("display", "block");
                //DivJobTab.Style.Add("display", "block");
                DivMemberTab.Style.Add("display", "block");
                return;
            }
        }

        if (dtRoleAP.Rows.Count > 0)
        {
            if (dtRoleAP.Rows[0]["IsAccepted"].ToString() != "0" && dtRoleAP.Rows[0]["IsAccepted"].ToString() != "2")
            {
                //divSecondWall.Style.Add("display", "block");
                DivHome.Style.Add("display", "block");
                DivForumTab.Style.Add("display", "block");
                DivUploadTab.Style.Add("display", "block");
                DivPollTab.Style.Add("display", "block");
                DivEventTab.Style.Add("display", "block");
                //DivJobTab.Style.Add("display", "block");
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

    #region AssignRole

    public void GetAssignRole()
    {
        if (ViewState["intGroupId"] != null)
        {
            objgrp.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
            objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            DataSet dschk = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.ViewGrpAssignUser);
            string Status = string.Empty;

            DataTable dtGrpOpt = objgrpDB.GetDataTable(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.ChkGroupOption);

            if (dtGrpOpt.Rows[0]["strAccess"].ToString() == "A")
            {
                DivHome.Style.Add("display", "block");
                DivForumTab.Style.Add("display", "block");
                DivUploadTab.Style.Add("display", "block");
                DivPollTab.Style.Add("display", "block");
                DivEventTab.Style.Add("display", "block");
                //DivJobTab.Style.Add("display", "block");
                DivMemberTab.Style.Add("display", "block");
            }
            else
            {
                if (dschk.Tables[0].Rows[0][0].ToString() != "0")
                {
                    DivHome.Style.Add("display", "block");
                    DivForumTab.Style.Add("display", "block");
                    DivUploadTab.Style.Add("display", "block");
                    DivPollTab.Style.Add("display", "block");
                    DivEventTab.Style.Add("display", "block");
                    //DivJobTab.Style.Add("display", "block");
                    DivMemberTab.Style.Add("display", "block");
                }
                else
                {
                    GetAccessModuleDetails();
                }
            }
        }
    }
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
                    //case "Jobs": DivJobTab.Style.Add("display", "block");
                    //    break;
                }
            }

        }

    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
        ip = Request.ServerVariables["REMOTE_ADDR"];
        objDOBForumPosting.strIpAddress = ip;
        objDOBForumPosting.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
        objDOBForumPosting.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objDOBForumPosting.strTitle = txtTitle.Text.Trim().Replace("'", "''"); ;
        if (CKDescription.InnerText != "Description")
        {
            objDOBForumPosting.strDescription = CKDescription.InnerText.Trim().Replace("'", "''").Replace("\n", "<br>");
        }
        else
        {
            objDOBForumPosting.strDescription = "";
        }
        if (chkPrivateForm.Checked == true)
        {
            objDOBForumPosting.chkPrivateForum = "Y";
        }
        else
        {
            objDOBForumPosting.chkPrivateForum = "N";
        }
       
            objDOBForumPosting.chkNotify = "Y";
       
        objDAForumPosting.AddEditDel_Scrl_UserForumPostingTbl(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.Insert);

        if (ISAPIURLACCESSED == "1")
        {
            try
            {
                string UserURL = "";

                if (chkPrivateForm.Checked == true)
                {
                    UserURL = APIURL + "addPrivateMemberToForum.action?" +
                        "forumId=FUM" + objDOBForumPosting.intForumReplyLikeShareId +
                        "&members=USR" + null +
                        "&groupId=GRP" + ViewState["intGroupId"];
                }
                else
                {
                    UserURL = APIURL + "createForum.action?" +
                        "forumId=FUM" + objDOBForumPosting.intForumReplyLikeShareId +
                        "&members=USR" + null +
                        "&groupId=GRP" + ViewState["intGroupId"];
                }

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                myRequest1.Method = "GET";
                WebResponse myResponse1 = myRequest1.GetResponse();
                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                if (ISAPIResponse == "1")
                {
                    String result = sr.ReadToEnd();
                    objAPILogDO.strURL = UserURL;
                    objAPILogDO.strAPIType = "Create Group Forum";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDO.strIPAddress = ip;
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
            }
            catch { }
        }
        clear();
        lblSuccess.Visible = true;
        lblEditSuccess.Visible = false;
        divForumSuccess.Style.Add("display", "block");

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("forum-landing-page.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void clear()
    {
        txtTitle.Text = "";
        CKDescription.InnerText = "";
        chkPrivateForm.Checked = false;
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("forum-landing-page.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    #region Tabs

    protected void lnkProfile_Click(object sender, EventArgs e)
    {
       // Session["grProfile"] = "Profile";
        Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Group-Home.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkForumTab_Click(object sender, EventArgs e)
    {
        //Session["grForum"] = "Forum";
        Response.Redirect("create-forum.aspx?GrpId=" + ViewState["intGroupId"]);
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

    public void EditForum(int ForumId)
    {
        try
        {
            objDOBForumPosting.intForumPostingId = ForumId;
            DataTable dtforum = objDAForumPosting.GetDataTable(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.EditForumDetails);
            if (dtforum.Rows.Count > 0)
            {
                txtTitle.Text = dtforum.Rows[0]["strTitle"].ToString();
                CKDescription.InnerText = dtforum.Rows[0]["strDescription"].ToString().Trim().Replace("<br>", "\n");
                string Invities= dtforum.Rows[0]["inviteMembers"].ToString();
                string[] body = Invities.Split(',');
                
                string ChkPrivatyFrm = dtforum.Rows[0]["chkPrivateForum"].ToString();
                string chkNotifyData = dtforum.Rows[0]["chkNotify"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ShowpnlNotify()", true);

            }
        }
        catch { }
    }

    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        objDOBForumPosting.strIpAddress = ip;
        objDOBForumPosting.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
        objDOBForumPosting.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objDOBForumPosting.strTitle = txtTitle.Text.Trim().Replace("'", "''"); ;
        objDOBForumPosting.strDescription = CKDescription.InnerText.Trim().Replace("'", "''").Replace("\n", "<br>"); 
        if (chkPrivateForm.Checked == true)
        {
            objDOBForumPosting.chkPrivateForum = "Y";
        }
        else
        {
            objDOBForumPosting.chkPrivateForum = "N";
        }
        
            objDOBForumPosting.chkNotify = "Y";
       
        objDOBForumPosting.intForumPostingId = Convert.ToInt32(hdnForumID.Value);
        
        objDAForumPosting.AddEditDel_Scrl_UserForumPostingTbl(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.UpdateForum);
        lblSuccess.Visible = false;
        lblEditSuccess.Visible = true;
        divForumSuccess.Style.Add("display", "block");
       
        
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        Response.Redirect("forum-landing-page.aspx?GrpId="+ViewState["intGroupId"]+" ",true);
    }

    protected void lnkClose_Click(object sender, EventArgs e)
    {
        divForumSuccess.Style.Add("display", "none");
        Response.Redirect("forum-landing-page.aspx?GrpId=" + ViewState["intGroupId"] + " ", true);
    }

    protected void lnkBacks_Click(object sender, EventArgs e)
    {
        Response.Redirect("forum-landing-page.aspx?GrpId=" + ViewState["intGroupId"].ToString());
    }
}