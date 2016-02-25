using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

public partial class forum_detail : System.Web.UI.Page
{
    string CommaSepratedIDs = "", PostForumHashSeprated = string.Empty;
    DataTable dt = new DataTable();

    DO_Scrl_UserForumPosting objDOBForumPosting = new DO_Scrl_UserForumPosting();
    DA_Scrl_UserForumPosting objDAForumPosting = new DA_Scrl_UserForumPosting();

    DO_Registrationdetails objdoreg = new DO_Registrationdetails();
    DA_Registrationdetails objdareg = new DA_Registrationdetails();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];

     protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divDeletesucess.Style.Add("display", "none");
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

            GetForumDetails();
            BindlstReplyForum();
        }
    }

    protected void lnkPostForum_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        if (txtPostForum.Text != "")
        {
            lblmsg.Visible = false;
            if (!string.IsNullOrEmpty(txtPostForum.Text))
            {
                if (Application["ConnectedUserListOnComment"] != null)
                {
                    DataTable dtContent = (DataTable)Application["ConnectedUserListOnComment"];
                    if (dtContent.Rows.Count > 0)
                    {
                        if (txtPostForum.Text.Contains("'"))
                            txtPostForum.Text = txtPostForum.Text.Replace("'", "''");
                        for (int i = 0; i < dtContent.Rows.Count; i++)
                        {
                            string AutoSugestName = Convert.ToString(dtContent.Rows[i]["Name"]);
                            bool contains = txtPostForum.Text.IndexOf(AutoSugestName, StringComparison.OrdinalIgnoreCase) >= 0;
                            if (contains == true)
                            {
                                int CommentUserId = Convert.ToInt32(dtContent.Rows[i]["intRegistrationId"].ToString());
                                if (String.IsNullOrEmpty(CommaSepratedIDs))
                                {
                                    PostForumHashSeprated = Regex.Replace(txtPostForum.Text, AutoSugestName, "#" + Convert.ToString(CommentUserId), RegexOptions.IgnoreCase);
                                    CommaSepratedIDs += CommentUserId;
                                }
                                else
                                {
                                    PostForumHashSeprated = Regex.Replace(PostForumHashSeprated, AutoSugestName, "#" + Convert.ToString(CommentUserId), RegexOptions.IgnoreCase);
                                    CommaSepratedIDs += "," + CommentUserId;
                                }
                            }
                        }
                    }
                }
                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    ip = Request.ServerVariables["REMOTE_ADDR"];
                objDOBForumPosting.strIpAddress = ip;

                if (ViewState["intForumReplyLikeShareId"] == null)
                {
                    objDOBForumPosting.strCommentUserId = CommaSepratedIDs;
                    objDOBForumPosting.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
                    objDOBForumPosting.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);

                    objDOBForumPosting.strRepLiShStatus = "RE";
                    if (!string.IsNullOrEmpty(PostForumHashSeprated))
                        objDOBForumPosting.strComment = PostForumHashSeprated;
                    else if (!string.IsNullOrEmpty(txtPostForum.Text))
                        objDOBForumPosting.strComment = txtPostForum.Text.Trim().Replace("''", "'");

                    objDOBForumPosting.intForumPostingId = Convert.ToInt32(Request.QueryString["ForumId"]);
                    objDOBForumPosting.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
                    objDAForumPosting.AddEditDel_Scrl_UserForumPostingTbl(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.InsertPostReply);
                    ViewState["ForumReplyLikeShareId"] = objDOBForumPosting.intForumReplyLikeShareId;

                    if (ISAPIURLACCESSED != "0")
                    {
                        try
                        {
                            String url = APIURL + "addCommentOnForum.action?" +
                                "commentId=" + objDOBForumPosting.intForumReplyLikeShareId +
                                        "&forumUid=" + objDOBForumPosting.intForumPostingId +
                                        "&groupid=" + objDOBForumPosting.intAddedBy +
                                        "&commentByUid=" + null +
                                        "&insertDt=" + DateTime.Now +
                                        "&content=" + objDOBForumPosting.strComment;

                            HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                            myRequest1.Method = "GET";
                            if (ISAPIResponse == "1")
                            {
                                WebResponse myResponse1 = myRequest1.GetResponse();
                                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                                String result = sr.ReadToEnd();
                                objAPILogDO.strURL = url;
                                objAPILogDO.strAPIType = "Forum Comment";
                                objAPILogDO.strResponse = result;
                                objAPILogDO.strIPAddress = ip;
                                objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                            }
                        }
                        catch { }
                    }

                    txtPostForum.Text = "";
                    GetForumDetails();
                    string ID = txtPostForum.ClientID;
                    hdnForumrRefresh.Value = ID;
                }
                else
                {
                    objDOBForumPosting.ForumReplyLikeShareId = Convert.ToInt32(ViewState["intForumReplyLikeShareId"]);
                    objDOBForumPosting.strCommentUserId = CommaSepratedIDs;
                    objDOBForumPosting.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
                    objDOBForumPosting.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);

                    objDOBForumPosting.strRepLiShStatus = "RE";
                    if (!string.IsNullOrEmpty(PostForumHashSeprated))
                        objDOBForumPosting.strComment = PostForumHashSeprated;
                    else if (!string.IsNullOrEmpty(txtPostForum.Text))
                        objDOBForumPosting.strComment = txtPostForum.Text.Trim().Replace("''", "'");

                    objDOBForumPosting.intForumPostingId = Convert.ToInt32(Request.QueryString["ForumId"]);
                    objDOBForumPosting.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
                    objDAForumPosting.AddEditDel_Scrl_UserForumPostingTbl(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.UpdatePostReply);
                    ViewState["ForumReplyLikeShareId"] = objDOBForumPosting.intForumReplyLikeShareId;

                    if (ISAPIURLACCESSED != "0")
                    {
                        try
                        {
                            String url = APIURL + "addCommentOnForum.action?" +
                                "commentId=" + objDOBForumPosting.intForumReplyLikeShareId +
                                        "&forumUid=" + objDOBForumPosting.intForumPostingId +
                                        "&groupid=" + objDOBForumPosting.intAddedBy +
                                        "&commentByUid=" + null +
                                        "&insertDt=" + DateTime.Now +
                                        "&content=" + objDOBForumPosting.strComment;

                            HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                            myRequest1.Method = "GET";
                            if (ISAPIResponse == "1")
                            {
                                WebResponse myResponse1 = myRequest1.GetResponse();
                                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                                String result = sr.ReadToEnd();
                                objAPILogDO.strURL = url;
                                objAPILogDO.strAPIType = "Forum Comment";
                                objAPILogDO.strResponse = result;
                                objAPILogDO.strIPAddress = ip;
                                objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                            }
                        }
                        catch { }
                    }

                    txtPostForum.Text = "";
                    BindlstReplyForum();
                    GetForumDetails();
                    string ID = txtPostForum.ClientID;
                    hdnForumrRefresh.Value = ID;
                    ViewState["intForumReplyLikeShareId"] = null;
                }

            }
            else
            {
                lblmsg.Visible = true;
                return;
            }
           
        }
        else
        {
            lblmsg.Visible = true;
            return;
        }

        BindlstReplyForum();
    }

    protected void GetForumDetails()
    {
        objDOBForumPosting.intForumPostingId = Convert.ToInt32(Request.QueryString["ForumId"]);
        dt = objDAForumPosting.GetDataTable(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.GetForumDetailsById);
        if (dt.Rows.Count > 0)
        {
            lstForumDetails.DataSource = dt;
            lstForumDetails.DataBind();
        }

    }

    #region AssignRole

    protected void GetAccessModuleDetails()
    {
        objDO.intUserId = Convert.ToInt32(ViewState["UserID"]);
        objDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        dt = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpModuleDetailsAcces);
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

        Label lbShareTitle = (Label)e.Item.FindControl("lbShareTitle");
        Label lbShareDesc = (Label)e.Item.FindControl("lbShareDesc");
        Label lblShare = (Label)e.Item.FindControl("lblShare");
        HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
        HiddenField hdnRegid = (HiddenField)e.Item.FindControl("hdnRegid");
        LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete1");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit1");

        string UserID = ViewState["UserID"].ToString();
        if (hdnRegid.Value == UserID)
        {
            lnkEdit.Visible = true;
            lnkDelete.Visible = true;
        }
        else
        {
            lnkEdit.Visible = false;
            lnkDelete.Visible = false;
        }


        if (String.IsNullOrEmpty(lnkTitle.Text))
        {
            lnkTitle.Text = lbShareTitle.Text;
        }

        Label lblTotallike = (Label)e.Item.FindControl("lblTotallike");
        Label lblreply = (Label)e.Item.FindControl("lblreply");
        LinkButton btnLike = (LinkButton)e.Item.FindControl("btnLike");
        objDOBForumPosting.intForumPostingId = Convert.ToInt32(Request.QueryString["ForumId"]);
        objDOBForumPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAForumPosting.GetDataTable(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.GetTotalLikeByById);
        if (dt.Rows.Count > 0)
        {
            int TotalLike = Convert.ToInt32(dt.Rows[0]["TotalLike"]);
            int TotalReply = Convert.ToInt32(dt.Rows[0]["TotalReply"]);
            int TotalShare = Convert.ToInt32(dt.Rows[0]["TotalShare"]);
            lblTotallike.Text = Convert.ToString(TotalLike);
            lblreply.Text = Convert.ToString(TotalReply);
            lblShare.Text = Convert.ToString(TotalShare);
            if (dt.Rows[0]["LikeUserId"].ToString() == ViewState["UserID"].ToString())
            {
                btnLike.Text = "Unlike";
            }
        }
        else
        {
            lblTotallike.Text = "";
            lblreply.Text = "";
            lblShare.Text = "";
        }
    }

    protected void lstForumDetails_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnForumPostId = (HiddenField)e.Item.FindControl("hdnForumPostId");
        HiddenField hdnRegid = (HiddenField)e.Item.FindControl("hdnRegid");
        LinkButton btnLike = (LinkButton)e.Item.FindControl("btnLike");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit1");
        LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete1");
        LinkButton lnkTitle = (LinkButton)e.Item.FindControl("lnkTitle");

        ViewState["ForumPostId"] = hdnForumPostId.Value;

        if (e.CommandName == "Edit forum")
        {
            Response.Redirect("create-forum.aspx?GrpId=" + ViewState["intGroupId"] + "&ForumId=" + hdnForumPostId.Value);

        }

        if (e.CommandName == "Delete forum")
        {
            ViewState["hdnForumPostId"] = Convert.ToInt32(hdnForumPostId.Value);
            ViewState["lnkTitle"] = lnkTitle.Text;
            divDeletesucess.Style.Add("display", "block");
        }


        if (e.CommandName == "Forum")
        {
            Response.Redirect("forum-detail.aspx?GrpId=" + ViewState["intGroupId"] + "&ForumId=" + hdnForumPostId.Value);
        }

        if (e.CommandName == "ReplyPost")
        {
            pnlReply.Visible = true;
            txtPostForum.Focus();
        }

        if (e.CommandName == "LikeForum")
        {

            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objDOBForumPosting.strIpAddress = ip;
            objDOBForumPosting.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
            objDOBForumPosting.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objDOBForumPosting.strRepLiShStatus = "LI";
            objDOBForumPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objDOBForumPosting.strComment = txtPostForum.Text.Trim().Replace("'", "''");
            objDOBForumPosting.intForumPostingId = Convert.ToInt32(Request.QueryString["ForumId"]);
            objDAForumPosting.AddEditDel_Scrl_UserForumPostingTbl(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.InsertLike);
            ViewState["ForumReplyLikeShareId"] = objDOBForumPosting.intForumReplyLikeShareId;

            string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
            string APIURL = ConfigurationManager.AppSettings["APIURL"];

            try
            {
                string UserURL = "";

                String url =APIURL+ "likeForum.action?" +
                   "userId=" + ViewState["UserID"] +
                   "&forumId=FUM" + ViewState["ForumReplyLikeShareId"];

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                myRequest1.Method = "GET";
                if (ISAPIResponse == "1")
                {
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();
                    objAPILogDO.strURL = UserURL;
                    objAPILogDO.strAPIType = "Poll";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDO.strIPAddress = ip;
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }

            }
            catch
            {

            }
            GetForumDetails();
            string ID = "ctl00_ContentPlaceHolder1_lstForumDetails_ctrl0_btnShare";
            hdnForumrRefresh.Value = ID;
        }

        if (e.CommandName == "ShareForum")
        {
            ViewState["intSharedPostingId"] = Convert.ToInt32(hdnForumPostId.Value);
            string ID = "ctl00_ContentPlaceHolder1_lstForumDetails_ctrl0_btnShare";
            hdnForumrRefresh.Value = ID;
            Response.Redirect("forum-detail.aspx?GrpId=" + ViewState["intGroupId"] + "&ShareId=" + ViewState["intSharedPostingId"] + "&SharedPostingId=" + ViewState["intSharedPostingId"] + "&ForumPostingId=" + ViewState["ForumPostId"] + "&ForumId=" + ViewState["intSharedPostingId"]);
        }

        if (e.CommandName == "Details")
        {
            Response.Redirect("Home.aspx?segId=" + hdnRegid.Value);
        }
    }

    protected void lstReplyForum_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnForumPostId = (HiddenField)e.Item.FindControl("hdnForumPostId");
        HiddenField hdnRegid = (HiddenField)e.Item.FindControl("hdnRegid");
        LinkButton btnLike = (LinkButton)e.Item.FindControl("btnLike");
        ViewState["forumreplyId"] = hdnForumPostId.Value;
        if (e.CommandName == "Edits")
        {
            ViewState["intForumReplyLikeShareId"] = hdnForumPostId.Value;
            objDOBForumPosting.ForumReplyLikeShareId = Convert.ToInt32(hdnForumPostId.Value); ;
           DataTable dtfrm = objDAForumPosting.GetDataTable(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.GetReplyForumComments);
           if (dtfrm.Rows.Count > 0)
           {
               txtPostForum.Text = Convert.ToString(dtfrm.Rows[0]["strComment"]);
               txtPostForum.Focus();
           }

        }
        else
        if (e.CommandName == "Deletes")
        {
            divDeletesucess.Style.Add("display", "block");
        }
        else
        if (e.CommandName == "LikeForum")
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objDOBForumPosting.strIpAddress = ip;
            objDOBForumPosting.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
            objDOBForumPosting.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objDOBForumPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objDOBForumPosting.strRepLiShStatus = "LI";
            objDOBForumPosting.ForumReplyLikeShareId = Convert.ToInt32(hdnForumPostId.Value);
            objDOBForumPosting.strComment = txtPostForum.Text.Trim().Replace("'", "''");
            objDOBForumPosting.intForumPostingId = Convert.ToInt32(Request.QueryString["ForumId"]);
            objDAForumPosting.AddEditDel_Scrl_UserForumPostingTbl(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.InsertChildLike);
            ViewState["ForumReplyLikeShareId"] = objDOBForumPosting.intForumReplyLikeShareId;

            try
            {
                string UserURL = "";
                String url =APIURL+ "likeForum.action?" +
                    "userId=" + ViewState["UserID"] +
                    "&forumId=FUM" + ViewState["ForumReplyLikeShareId"];

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                myRequest1.Method = "GET";
                if (ISAPIResponse == "1")
                {
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();
                    objAPILogDO.strURL = UserURL;
                    objAPILogDO.strAPIType = "Poll";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDO.strIPAddress = ip;
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }

            }
            catch
            {

            }
            BindlstReplyForum();
        }
        else
        if (e.CommandName == "Details")
        {
            Response.Redirect("Home.aspx?RegId=" + hdnRegid.Value);
        }
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        if (ViewState["hdnForumPostId"] != null)
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

            Response.Redirect("forum-landing-page.aspx?GrpId=" + ViewState["intGroupId"]);
        }
        else
        {
            objDOBForumPosting.ForumReplyLikeShareId = Convert.ToInt32(ViewState["forumreplyId"]); ;
            objDAForumPosting.AddEditDel_Scrl_UserForumPostingTbl(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.DeletePostReply);
            BindlstReplyForum();
            divDeletesucess.Style.Add("display", "none");
        }


    }

    protected void BindlstReplyForum()
    {
        objDOBForumPosting.intForumPostingId = Convert.ToInt32(Request.QueryString["ForumId"]); ;
        dt = objDAForumPosting.GetDataTable(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.GetReplyForumComment);

        if (dt.Rows.Count > 0)
        {
            lstReplyForum.Visible = true;
            lstReplyForum.DataSource = dt;
            lstReplyForum.DataBind();
        }
        else
        {
            lstReplyForum.Visible = false;
            lstReplyForum.DataSource = null;
            lstReplyForum.DataBind();
        }
    }

    protected void lstReplyForum_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        Label lblTotallike = (Label)e.Item.FindControl("lblTotallike");
        Label lblReplyComment = (Label)e.Item.FindControl("lblReplyComment");
        HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
        HiddenField hdnimgprofile = e.Item.FindControl("hdnimgprofile") as HiddenField;
        HiddenField hdnRegid = (HiddenField)e.Item.FindControl("hdnRegid");
        LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");

        string UserID = ViewState["UserID"].ToString();
        if (hdnRegid.Value == UserID)
        {
            lnkEdit.Visible = true;
            lnkDelete.Visible = true;
        }
        else
        {
            lnkEdit.Visible = false;
            lnkDelete.Visible = false;
        }

        if (imgprofile.Src == "" || imgprofile.Src == null || imgprofile.Src == "CroppedPhoto/")
        {
            imgprofile.Src = "images/photo2.jpg";
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

        string filtertext = "";

        string ReplyComment = lblReplyComment.Text;
        string filterComment = lblReplyComment.Text.Trim().Replace("#", "");
        string[] SubCatID = ReplyComment.Split('#');

        String[] val = ReplyComment.Split(new Char[] { ' ' });
        String userid1;
        for (int i = 0; i < val.Length; i++)
        {
            if (val[i].StartsWith("#", StringComparison.CurrentCultureIgnoreCase))
            {
                userid1 = val[i].Split(new char[] { '#' })[1];

                if (!string.IsNullOrEmpty(userid1))
                {
                    objdoreg.AddedBy = Convert.ToInt32(userid1);
                    DataTable dtReq = new DataTable();
                    dtReq = objdareg.GetDataTable(objdoreg, DA_Registrationdetails.RegistrationDetails.GetApprovedStudentByInstitute);
                    if (dtReq.Rows.Count > 0)
                    {
                        string url = "Home.aspx?RegId=" + userid1;

                        if (String.IsNullOrEmpty(filtertext))
                        {
                            filtertext = Regex.Replace(filterComment, userid1, "<a href='" + url + "'>" + dtReq.Rows[0]["FullName"] + "</a>");
                        }
                        else
                        {
                            filtertext = Regex.Replace(filtertext, userid1, "<a href='" + url + "'>" + dtReq.Rows[0]["FullName"] + "</a>");
                        }
                        lblReplyComment.Text = filtertext;
                    }

                }

            }
        }

        HiddenField hdnForumPostId = (HiddenField)e.Item.FindControl("hdnForumPostId");
        LinkButton btnLike = (LinkButton)e.Item.FindControl("btnLike");

        objDOBForumPosting.intForumPostingId = Convert.ToInt32(Request.QueryString["ForumId"]); ;
        objDOBForumPosting.ForumReplyLikeShareId = Convert.ToInt32(hdnForumPostId.Value);
        objDOBForumPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAForumPosting.GetDataTable(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.GetChildLikeCount);
        if (dt.Rows.Count > 0)
        {
            int TotalLike = Convert.ToInt32(dt.Rows[0]["TotalLike"]);
            lblTotallike.Text = Convert.ToString(TotalLike);
            if (dt.Rows[0]["LIKEUSERID"].ToString() == ViewState["UserID"].ToString())
            {
                btnLike.Text = "Unlike";
            }
        }
        else
        {
            lblTotallike.Text = "";
        }
    }

    protected void lnkAllForum_Click(object sender, EventArgs e)
    {
        Response.Redirect("forum-landing-page.aspx?GrpId=" + ViewState["intGroupId"]);
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

    protected void lnkDeleteCancel_Click(object sender, EventArgs e)
    {
        ViewState["hdnForumPostId"] = null;
        divDeletesucess.Style.Add("display", "none");
    }
}