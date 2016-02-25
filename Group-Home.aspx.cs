using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.IO;

public partial class Group_Home : System.Web.UI.Page
{
    string documentPath1 = "";
    int FileStatus = 0;
    string documentName1 = "";

    DataTable dt = new DataTable();
    DataTable dtUpdate = new DataTable();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_UserStatusUpdateTbl objstatusDO = new DO_Scrl_UserStatusUpdateTbl();
    DA_Scrl_UserStatusUpdateTbl objstatusDA = new DA_Scrl_UserStatusUpdateTbl();

    DA_GroupUserStatus objGrpstatusDA = new DA_GroupUserStatus();
    DO_GroupUserStatus objGrpstatusDO = new DO_GroupUserStatus();

    DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        //to get the browser name from server side.
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        ViewState["BrowserName"] = browser.Browser;

        if (!IsPostBack)
        {
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            Session["SubmitTime"] = DateTime.Now.ToString();

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "10";

            int LoginId = Convert.ToInt32(Session["ExternalUserId"]);
            int FrndId = 0;

            ViewState["UserID"] = LoginId;
            ViewState["RegId"] = FrndId;

            if (Request.QueryString["GrpId"] != "" && Request.QueryString["GrpId"] != null)
            {
                ViewState["intGroupId"] = Request.QueryString["GrpId"];
            }
            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Groups";

            if (FrndId == 0)
            {
                BindPostUpdate(LoginId, 0, 1); //self wall
            }
            else
            {
                BindPostUpdate(LoginId, FrndId, 2);
            }

            if ((Convert.ToString(Request.QueryString["RegId"]) != Convert.ToString(Session["ExternalUserId"])))
            {
                if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
                {
                    ViewState["RegId"] = Request.QueryString["RegId"];
                    ViewState["UserID"] = Request.QueryString["RegId"];
                }
            }
            AccessModulePermisssion();
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
                divSecondWall.Style.Add("display", "block");
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
                divSecondWall.Style.Add("display", "block");
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

    protected void GetAccessModuleDetails()
    {
        objgrp.intUserId = Convert.ToInt32(ViewState["UserID"]);
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        dt = objgrpDB.GetDataTable(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpModuleDetailsAcces);

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

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["SubmitTime"] = Session["SubmitTime"];
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

    #region Wall

    #region Search

    private void BindPostUpdate(int LoginId, int FriendId, int flag) // FLag 1 self wall , 2  Friends wall & Not a friends wall
    {
        objGrpstatusDO.intAddedBy = LoginId;
        objGrpstatusDO.intRegistrationId = FriendId;
        objGrpstatusDO.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objGrpstatusDO.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        objGrpstatusDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);

        if (flag == 1)
            dtUpdate = objGrpstatusDA.GetDataTable(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.GetSelfWall);
        else if (flag == 2)
            dtUpdate = objGrpstatusDA.GetDataTable(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.GetFriendAndNotfrndWall);

        if (dtUpdate.Rows.Count > 0)
        {
            ViewState["MaxCount"] = dtUpdate.Rows[0]["Maxcount"];
            hdnMaxcount.Value = dtUpdate.Rows[0]["Maxcount"].ToString();
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

            lstPostUpdates.DataSource = dtUpdate;
            lstPostUpdates.DataBind();
            dvPage.Visible = true;
        }
        else
        {
            lstPostUpdates.DataSource = null;
            lstPostUpdates.DataBind();
            dvPage.Visible = false;
            pLoadMore.Style.Add("display", "none");
            lblNoMoreRslt.Visible = false;
        }
    }

    protected void btnPostUpdate_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        SavePost();
    }

    protected void txtPostUpdate_TextChanged(object sender, EventArgs e)
    {
        SavePost();
    }

    protected void SavePost()
    {
        try
        {
            if (Session["SubmitTime"].ToString() == ViewState["SubmitTime"].ToString())
            {
                if (string.IsNullOrEmpty(txtPostUpdate.InnerHtml) || txtPostUpdate.InnerHtml == "What's on  your mind ?")
                {
                    return;
                }

                lblMessage.Text = "";
                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    ip = Request.ServerVariables["REMOTE_ADDR"];

                objGrpstatusDO.strPostType = ddlPostType.SelectedValue;

                objGrpstatusDO.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
                objGrpstatusDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
                objGrpstatusDO.strPostDescription = txtPostUpdate.InnerHtml.Trim().Replace("'", "''");
                string filePath = hdnPhoto.Value;
                string fileName = hdnDocName.Value;
                string error = hdnErrorMsg.Value;
                if (error != "")
                {
                    
                    if (hdnImage.Value != "")
                    {
                        lblErrorMess.Visible = true;
                        lblErrorMess.Text = error;
                        div2.Style.Add("display", "block");
                        div3.Style.Add("display", "none");
                    }
                    if (hdnVideo.Value != "")
                    {
                        lblErrorMess.Visible = true;
                        lblErrorMess.Text = error;
                        div3.Style.Add("display", "block");
                        div2.Style.Add("display", "none");
                    }
                    return;
                }
                if (fileName != "")
                {
                    string[] arr = fileName.Split('.');
                    string ext = arr[1];
                    if (ext == "jpg" || ext == "jpeg" || ext == "png" || ext == "gif" || ext == "org")
                    {
                        UploadPhoto();
                        if (!String.IsNullOrEmpty(ViewState["PhotoUpload"].ToString()))
                        {
                            objGrpstatusDO.strPhotoPath = Convert.ToString(ViewState["PhotoUpload"]);
                            objGrpstatusDO.strVideoPath = "";
                        }
                        else
                            return;
                    }
                    else if (ext == "mp4" || ext == "wmv" || ext == "flv" || ext == "mpg" || ext == "MP4" || ext == "WMV" || ext == "FLV" || ext == "MPG" || ext == "mp3" || ext == "MP3")
                    {
                        UploadVideoFiles();
                        if (FileStatus != 1)
                        {
                            if (!String.IsNullOrEmpty(ViewState["VideoUpload"].ToString()))
                            {
                                objGrpstatusDO.strVideoPath = Convert.ToString(ViewState["VideoUpload"]);
                                objGrpstatusDO.strPhotoPath = "";
                            }
                        }
                        else
                            return;
                    }

                }

                if (hdnvdeonme.Value != "")
                {
                    objGrpstatusDO.strVideoPath = hdnvdeonme.Value;
                }

                if (hdnPhotoName.Value != "")
                {
                    objGrpstatusDO.strPhotoPath = hdnPhotoName.Value;
                }
                if (txtPostLink.Text != "Link")
                {
                    objGrpstatusDO.strPostLink = txtPostLink.Text.Trim();
                }

                objGrpstatusDO.strIpAddress = ip;
                objGrpstatusDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);

                if (string.IsNullOrEmpty(hdnintStatusUpdateId.Value))
                    objGrpstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.Insert);
                else
                {
                    objGrpstatusDO.intStatusUpdateId = Convert.ToInt32(hdnintStatusUpdateId.Value);
                    objGrpstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.Update);
                }


                BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall

                if (PhotoUpload.HasFile)
                {
                    lblMessage.Text = "Photo updated successfully";
                }
                if (VideoUpload.HasFile)
                {
                    lblMessage.Text = "Video updated successfully";
                }
                if (txtPostLink.Text != "")
                {
                    lblMessage.Text = "Link updated successfully";
                }
                if (txtPostUpdate.InnerHtml != "" && PhotoUpload.HasFile == false && VideoUpload.HasFile == false && txtPostLink.Text == "")
                {
                    lblMessage.Text = "Status updated successfully";
                }
                lblMessage.ForeColor = System.Drawing.Color.Green;
                txtPostUpdate.InnerHtml = "";
                txtPostLink.Text = "";
                hdnvdeonme.Value = "";
                hdnPhotoName.Value = "";
                hdnintStatusUpdateId.Value = "";
                Session["SubmitTime"] = DateTime.Now.ToString();
                ddlPostType.ClearSelection();
                hdnPhoto.Value = "";
                hdnDocName.Value = "";
                hdnErrorMsg.Value = "";

            }
        }
        catch
        {

        }
    }

    protected void UploadPhoto()
    {
        string filePath = hdnPhoto.Value;
        string fileName = hdnDocName.Value;
        string error = hdnErrorMsg.Value;
        if (fileName != "")
        {
            if (error == "")
            {
                string name = fileName;
                ViewState["PhotoUpload"] = filePath;
                div2.Style.Add("display", "none");
                lblErrorMess.Text = "";
                lblMessage.Text = "Photo updated successfully";
            }
            else
            {
                lblErrorMess.Visible = true;
                lblErrorMess.Text = error;
                div2.Style.Add("display", "block");
                return;
            }
        }
    }

    private void UploadVideoFiles()
    {
        string filePath = hdnPhoto.Value;
        string fileName = hdnDocName.Value;
        string error = hdnErrorMsg.Value;
        if (error == "")
        {
            documentPath1 = hdnPhoto.Value;
            ViewState["VideoUpload"] = documentPath1;
            documentName1 = hdnPhotoName.Value;
            div3.Style.Add("display", "none");
        }
        else
        {
            FileStatus = 1;
            lblErrorMess.Visible = true;
            lblErrorMess.Text = error;
            div2.Style.Add("display", "block");
            return;
        }
    }

    protected void lstPostUpdates_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        HiddenField hdnPostUpdateId = e.Item.FindControl("hdnPostUpdateId") as HiddenField;
        Label lnkLikePost = e.Item.FindControl("lnkLikePost") as Label;
        Label lblShare = e.Item.FindControl("lblShare") as Label;
        Label lblPostDescription = e.Item.FindControl("lblPostDescription") as Label;
        HiddenField videoname = e.Item.FindControl("hdnVideoName") as HiddenField;
        HiddenField hdnIframe = e.Item.FindControl("hdnIframe") as HiddenField;
        HtmlGenericControl frm1 = (HtmlGenericControl)e.Item.FindControl("frm1");
        HiddenField hdnRegistrationId = e.Item.FindControl("hdnRegistrationId") as HiddenField;
        HiddenField hdnintUserTypeId = e.Item.FindControl("hdnintUserTypeId") as HiddenField;
        HiddenField hdnSharedId = (HiddenField)e.Item.FindControl("hdnSharedId");
        HiddenField hdnSharedUserTypeId = (HiddenField)e.Item.FindControl("hdnSharedUserTypeId");
        LinkButton lnkLike = (LinkButton)e.Item.FindControl("lnkLike");
        LinkButton lnkShare = (LinkButton)e.Item.FindControl("lnkShare");
        ListView lstChild = e.Item.FindControl("lstChild") as ListView;
        ImageButton imgUpDown = (ImageButton)e.Item.FindControl("imgUpDown");
        LinkButton lnkDeleteComment = (LinkButton)e.Item.FindControl("lnkDeleteComment");
        HiddenField hdnLikeUserPost = e.Item.FindControl("hdnLikeUserPost") as HiddenField;

        objGrpstatusDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
        ViewState["intGroupId"] = objGrpstatusDO.intGroupId;
        ViewState["DeletePosts"] = "Delete Post";
        ViewState["hdnPostUpdateId"] = Convert.ToString(hdnPostUpdateId.Value); ;
        ViewState["lblPostDescription"] = lblPostDescription.Text;
        if (e.CommandName == "EnterComment")
        {
            string str = Convert.ToString(e.CommandArgument);
            try
            {
                if (Session["SubmitTime"].ToString() == ViewState["SubmitTime"].ToString())
                {
                    TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");
                    ListViewItem Item = (ListViewItem)txtComment.Parent.Parent.Parent.Parent.Parent.Parent;
                    if (txtComment.Text != "" && txtComment.Text != "Write a comment" && txtComment.Text != null)
                    {
                        objGrpstatusDO.intStatusUpdateId = Convert.ToInt32(hdnPostUpdateId.Value);
                        objGrpstatusDO.strComment = txtComment.Text;
                        objGrpstatusDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
                        objGrpstatusDO.strIpAddress = ip;
                        objGrpstatusDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);

                        if (string.IsNullOrEmpty(lblintCommentId.Text.Trim()))
                        {
                            objGrpstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.AddComment);
                            lblMessage.Text = "Comment saved successfully.";
                            lblMessage.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            objGrpstatusDO.intCommentId = Convert.ToInt32(lblintCommentId.Text.Trim());
                            objGrpstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.UpdateComment);
                            lblMessage.Text = "Comment updated successfully.";
                            lblMessage.ForeColor = System.Drawing.Color.Green;
                            lblintCommentId.Text = "";
                        }
                        txtComment.Text = "";
                        BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall                   
                    }
                    else
                    {

                    }
                    Session["SubmitTime"] = DateTime.Now.ToString();
                }
            }
            catch
            {

            }
        }

        if (e.CommandName == "Hide/ShowComments")
        {
            if (lstChild.Visible == true)
            {
                lstChild.Visible = false;
                imgUpDown.ImageUrl = "images/down.jpg";
            }
            else
            {
                lstChild.Visible = true;
                imgUpDown.ImageUrl = "images/up.jpg";
            }
        }


        if (e.CommandName == "Share")
        {
            objGrpstatusDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
            objGrpstatusDO.intStatusUpdateId = Convert.ToInt32(hdnPostUpdateId.Value);
            ViewState["intStatusUpdateId"] = Convert.ToInt32(hdnPostUpdateId.Value);
            dt.Clear();
            dt = objGrpstatusDA.GetDataTable(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.SingleRecord);

            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["strPhotoPath"])))
                {
                    Response.Redirect("Group-Home.aspx?GrpId=" + ViewState["intGroupId"] + "&StatusId=" + ViewState["intStatusUpdateId"]);

                }
                else if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["strVideoPath"])))
                {
                    Response.Redirect("Group-Home.aspx?GrpId=" + ViewState["intGroupId"] + "&StatusId=" + ViewState["intStatusUpdateId"]);
                }

                else if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["strPostLink"])))
                {
                    Response.Redirect("Group-Home.aspx?GrpId=" + ViewState["intGroupId"] + "&StatusId=" + ViewState["intStatusUpdateId"]);
                }
            }

            hdnTabId.Value = "";
            hdnPostId.Value = "";
            hdnLoader.Value = "";
            hdncommentfocus.Value = "";
        }

        if (e.CommandName == "video")
        {
            frm1.Attributes.Add("src", hdnIframe.Value);
        }

        if (e.CommandName == "Like Post")
        {
            objGrpstatusDO.intStatusUpdateId = Convert.ToInt32(hdnPostUpdateId.Value);

            objGrpstatusDO.intLikeDisLike = 1;//For Like

            objGrpstatusDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);

            objGrpstatusDO.strIpAddress = ip;

            if (lnkLike.Text == "Unlike")
            {
                DataTable dtAction = new DataTable();

                dtAction = objGrpstatusDA.GetLikeDataTable(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.UnLikeStatus);
                if (dtAction.Rows[0]["Action"].ToString() == "0")
                {
                    lnkLikePost.Text = Convert.ToString(Convert.ToInt32(lnkLikePost.Text) + 1);
                }
            }
            else
            {
                DataTable dtAction = new DataTable();
                dtAction = objGrpstatusDA.GetLikeDataTable(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.LikeStatus);
                if (dtAction.Rows[0]["Action"].ToString() == "1")
                {
                    lnkLikePost.Text = Convert.ToString(Convert.ToInt32(lnkLikePost.Text) + 1);
                }
            }

            BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
        }

        if (e.CommandName == "ShowOptions")
        {
            Label lblsave = (Label)e.Item.FindControl("lblsave");
            lblsave.Text = "";
            Panel pnlviewComment = (Panel)e.Item.FindControl("pnlviewComment");

            if (pnlviewComment.Visible == false)
            {
                pnlviewComment.Visible = true;
                LinkButton btnView = (LinkButton)e.Item.FindControl("btnView");
                btnView.ToolTip = "Close";

                TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");
                txtComment.Text = "";
            }
            else
            {
                pnlviewComment.Visible = false;
            }
        }

        if (e.CommandName == "Comment")
        {
            hdncommentfocus.Value = "";
            TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");
            string ID = txtComment.ClientID;
            hdncommentfocus.Value = ID;

        }
        else if (e.CommandName == "SavePost")
        {
            int index = e.Item.DataItemIndex;
            ListViewDataItem item = (ListViewDataItem)e.Item;
            Label lblsave = (Label)e.Item.FindControl("lblsave");

            TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");
            if (txtComment.Text == "")
            {
                lblsave.Text = "Please enter Comment";
                lblsave.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int PostId = Convert.ToInt32(hdnPostUpdateId.Value);
            objstatusDO.intStatusUpdateId = PostId;
            objstatusDO.strComment = txtComment.Text;
            objstatusDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objstatusDO.strIpAddress = ip;
            objstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.AddComment);
            txtComment.Text = "";
            lblsave.Text = "Comment Saved Successfully";
            lblsave.ForeColor = System.Drawing.Color.Green;
            BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
        }

        else if (e.CommandName == "CancelComment")
        {
            Label lblsave = (Label)e.Item.FindControl("lblsave");
            lblsave.Text = "";
            Panel pnlviewComment = (Panel)e.Item.FindControl("pnlviewComment");

            if (pnlviewComment.Visible == false)
            {
                pnlviewComment.Visible = true;
                LinkButton btnView = (LinkButton)e.Item.FindControl("btnView");
                btnView.ToolTip = "Close";

                TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");
                txtComment.Text = "";
            }
            else
            {
                pnlviewComment.Visible = false;
            }
        }

        if (e.CommandName == "Details")
        {
            Response.Redirect("Home.aspx?RegId=" + hdnRegistrationId.Value);

        }

        if (e.CommandName == "SharedDetails")
        {
            Response.Redirect("Home.aspx?RegId=" + hdnSharedId.Value);
        }

        if (e.CommandName == "Delete Post")
        {
            divDeletesucess.Style.Add("display", "block");
        }

        if (e.CommandName == "Edit Post")
        {
            ViewState["hdnPostUpdateId"] = "";
            dt.Clear();
            objGrpstatusDO.intStatusUpdateId = Convert.ToInt32(hdnPostUpdateId.Value);
            hdnintStatusUpdateId.Value = Convert.ToString(hdnPostUpdateId.Value);

            dt = objGrpstatusDA.GetDataTable(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.SingleRecord);
            if (dt.Rows.Count > 0)
            {
                ddlPostType.ClearSelection();
                ddlPostType.Items.FindByValue(dt.Rows[0]["strPostType"].ToString()).Selected = true;
                txtPostUpdate.InnerHtml = Convert.ToString(dt.Rows[0]["strPostDescription"]);
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["strPostLink"])))
                {
                    txtPostLink.Text = Convert.ToString(dt.Rows[0]["strPostLink"]);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "123", "showImage('4');", true);
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["strVideoPath"])))
                {
                    hdnvdeonme.Value = Convert.ToString(dt.Rows[0]["strVideoPath"]);
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["strPhotoPath"])))
                {
                    hdnPhotoName.Value = Convert.ToString(dt.Rows[0]["strPhotoPath"]);
                }
            }
        }
    }

    protected void lstPostUpdates_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataTable dtchild = new DataTable();
        DataTable dtLike = new DataTable();
        HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
        HtmlImage imgPhoto = (HtmlImage)e.Item.FindControl("imgPhoto");
        HiddenField hdnVideoName = (HiddenField)e.Item.FindControl("hdnVideoName");
        HiddenField hdnRegistrationId = e.Item.FindControl("hdnRegistrationId") as HiddenField;
        HiddenField hdnPostUpdateId = e.Item.FindControl("hdnPostUpdateId") as HiddenField;
        HiddenField hdnShared = (HiddenField)e.Item.FindControl("hdnShared");
        HtmlGenericControl divshare = (HtmlGenericControl)e.Item.FindControl("divshare");
        Label lblSahreType = (Label)e.Item.FindControl("lblSahreType");
        Label lblPostDescription = (Label)e.Item.FindControl("lblPostDescription");
        LinkButton lnkDeleteComment = (LinkButton)e.Item.FindControl("lnkDeleteComment");
        Label lnkLikePost = e.Item.FindControl("lnkLikePost") as Label;
        HtmlGenericControl dvVideo = (HtmlGenericControl)e.Item.FindControl("dvVideo");
        HtmlGenericControl divChrome = (HtmlGenericControl)e.Item.FindControl("divChrome");
        HyperLink hplLinkUrl = (HyperLink)e.Item.FindControl("hplLinkUrl");
        LinkButton lnkEditPost = (LinkButton)e.Item.FindControl("lnkEditPost");
        Label lblPipe = (Label)e.Item.FindControl("lblPipe");
        HtmlGenericControl dvSharelink = (HtmlGenericControl)e.Item.FindControl("dvSharelink");
        HiddenField hdnPhotoPath = e.Item.FindControl("hdnPhotoPath") as HiddenField;
        HiddenField hdnPostLink = (HiddenField)e.Item.FindControl("hdnPostLink");
        HiddenField hdnLikeUserPost = e.Item.FindControl("hdnLikeUserPost") as HiddenField;
        LinkButton lnkLike = (LinkButton)e.Item.FindControl("lnkLike");
        HiddenField hdnimgprofile = e.Item.FindControl("hdnimgprofile") as HiddenField;
        if (hdnLikeUserPost.Value == ViewState["UserID"].ToString())
        {
            lnkLike.Text = "Unlike";
        }
        objGrpstatusDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
        if (!string.IsNullOrEmpty(hdnPhotoPath.Value))
        {
            dvSharelink.Style.Add("display", "block");
        }
        if (!string.IsNullOrEmpty(hdnPostLink.Value))
        {
            dvSharelink.Style.Add("display", "block");
        }
        if (String.IsNullOrEmpty(hplLinkUrl.Text) || (hplLinkUrl.Text == "http://"))
            hplLinkUrl.Visible = false;

        if (hdnShared.Value == "1")
        {
            divshare.Style.Add("display", "block");
            if (!string.IsNullOrEmpty(lblPostDescription.Text) && imgPhoto.Src == "UploadedPhoto/" && string.IsNullOrEmpty(hdnVideoName.Value))
            {
                lblSahreType.Text = "Link";
            }
            else if (imgPhoto.Src != "UploadedPhoto/")
                lblSahreType.Text = "Photo";
            else if (!string.IsNullOrEmpty(hdnVideoName.Value))
                lblSahreType.Text = "Video";
        }
        else
        {
            divshare.Style.Add("display", "none");
        }

        if (hdnVideoName.Value == "" || hdnVideoName.Value == null)
        {
            dvVideo.Style.Add("display", "none");
            divChrome.Style.Add("display", "none");
        }
        else
        {
            dvSharelink.Style.Add("display", "block");
            if (Convert.ToString(ViewState["BrowserName"]) == "Chrome")
                dvVideo.Style.Add("display", "none");
            else
                divChrome.Style.Add("display", "none");
        }

        int index = e.Item.DataItemIndex;
        ListViewDataItem item = (ListViewDataItem)e.Item;
        int PostId = Convert.ToInt32(hdnPostUpdateId.Value);

        if (imgprofile.Src == "CroppedPhoto/")
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

        if (imgPhoto.Src == "UploadedPhoto/")
        {
            imgPhoto.Visible = false;
        }

        if (hdnRegistrationId.Value == Convert.ToString(ViewState["UserID"]))
        {
            lnkDeleteComment.Visible = true;
        }

        if (hdnRegistrationId.Value == Convert.ToString(Session["ExternalUserId"]))
        {
            lnkDeleteComment.Visible = true;
            lnkEditPost.Visible = true;
            lblPipe.Visible = true;
        }

        ListView lstChild = (ListView)e.Item.FindControl("lstChild");
        objGrpstatusDO.intStatusUpdateId = PostId;
        dtchild = objGrpstatusDA.GetDataTable(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.BindChildList);
        if (dtchild.Rows.Count > 0)
        {
            lstChild.DataSource = dtchild;
            lstChild.DataBind();
        }

        lnkLikePost.ToolTip = "View Likes";
        objGrpstatusDO.intStatusUpdateId = Convert.ToInt32(hdnPostUpdateId.Value);
        dtLike = objGrpstatusDA.GetDataTable(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.GetPostLikeUserLists);
        if (dtLike.Rows.Count > 0)
        {
            for (int i = 0; i < dtLike.Rows.Count; i++)
            {
                if (lnkLikePost.ToolTip != "View Likes")
                    lnkLikePost.ToolTip += Convert.ToString(dtLike.Rows[i]["UserName"]) + Environment.NewLine;
                else
                    lnkLikePost.ToolTip = Convert.ToString(dtLike.Rows[i]["UserName"]) + Environment.NewLine;
            }
        }
    }

    protected void lstPostUpdates_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        ListView lstChild = e.Item.FindControl("lstChild") as ListView;
        lstChild.ItemCommand += new EventHandler<ListViewCommandEventArgs>(lstChild_ItemCommand);
        lstChild.ItemDataBound += new EventHandler<ListViewItemEventArgs>(lstChild_ItemDataBound);
    }

    protected void lstPostUpdates_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {

    }

    protected void lstChild_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataTable dtChildLike = new DataTable();
        HiddenField hdnCommentId = e.Item.FindControl("hdnCommentId") as HiddenField;
        HiddenField hdnRegistrationId = e.Item.FindControl("hdnRegistrationId") as HiddenField;
        Label lnkLikeComment = (Label)e.Item.FindControl("lnkLikeComment");
        HtmlImage imgCommentpic = (HtmlImage)e.Item.FindControl("imgCommentpic");
        LinkButton lnkEditComment = e.Item.FindControl("lnkEditComment") as LinkButton;
        LinkButton lnkDeleteComment = e.Item.FindControl("lnkDeleteComment") as LinkButton;
        HiddenField hdnCommentLikeId = e.Item.FindControl("hdnCommentLikeId") as HiddenField;
        LinkButton lnkLikes = e.Item.FindControl("lnkLikes") as LinkButton;
        HiddenField hdnimgprofile = e.Item.FindControl("hdnimgprofile") as HiddenField;

        if (hdnCommentLikeId.Value == ViewState["UserID"].ToString())
        {
            lnkLikes.Text = "Unlike";
        }

        if (imgCommentpic.Src == "CroppedPhoto/")
        {
            imgCommentpic.Src = "images/comment-profile.jpg";
        }
        else
        {
            string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + hdnimgprofile.Value);
            if (File.Exists(imgPathPhysical))
            {
            }
            else
            {
                imgCommentpic.Src = "../images/comment-profile.jpg";
            }
        }

        if (hdnRegistrationId.Value == Convert.ToString(Session["ExternalUserId"]))
        {
            lnkDeleteComment.Visible = true;
            lnkEditComment.Visible = true;
            lblPipe.Visible = true;
        }

        lnkLikeComment.ToolTip = "View Likes";
        objstatusDO.intCommentId = Convert.ToInt32(hdnCommentId.Value);
        objstatusDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
        dtChildLike = objGrpstatusDA.GetLikeDataTable(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.GetCommentLikeUserLists);

        if (dtChildLike.Rows.Count > 0)
        {
            for (int i = 0; i < dtChildLike.Rows.Count; i++)
            {
                if (lnkLikeComment.ToolTip != "View Likes")
                    lnkLikeComment.ToolTip += Convert.ToString(dtChildLike.Rows[i]["UserName"]) + Environment.NewLine;
                else
                    lnkLikeComment.ToolTip = Convert.ToString(dtChildLike.Rows[i]["UserName"]) + Environment.NewLine;
            }
        }
    }

    protected void lstChild_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnCommentId = e.Item.FindControl("hdnCommentId") as HiddenField;
        HiddenField hdnintUserTypeId = e.Item.FindControl("hdnintUserTypeId") as HiddenField;
        HiddenField hdnRegistrationId = e.Item.FindControl("hdnRegistrationId") as HiddenField;
        Label lnkLikeComment = e.Item.FindControl("lnkLikeComment") as Label;
        LinkButton lnkLike = e.Item.FindControl("lnkLike") as LinkButton;
        LinkButton lnkEditComment = e.Item.FindControl("lnkEditComment") as LinkButton;
        LinkButton lnkDeleteComment = e.Item.FindControl("lnkDeleteComment") as LinkButton;
        HiddenField hdnCommentLikeId = e.Item.FindControl("hdnCommentLikeId") as HiddenField;
        LinkButton lnkLikes = e.Item.FindControl("lnkLikes") as LinkButton;
        Label lblstr = e.Item.FindControl("lblstr") as Label;

        ViewState["hdnCommentId"] = hdnCommentId.Value;
        ViewState["lblstr"] = lblstr.Text;
        ViewState["DeletePosts"] = "Delete Comments";
        lblintCommentId.Text = "";
        if (e.CommandName == "Edit Comment")
        {
            objGrpstatusDO.intCommentId = Convert.ToInt32(hdnCommentId.Value);
            lblintCommentId.Text = Convert.ToString(hdnCommentId.Value);
            DataTable dtEdit = new DataTable();
            dtEdit = objGrpstatusDA.GetLikeDataTable(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.GetComment);
            if (dtEdit.Rows.Count > 0)
            {
                var item = ((Control)sender).NamingContainer as ListViewItem;
                if (item != null)
                {
                    TextBox txtComment = ((TextBox)item.FindControl("txtComment"));
                    txtComment.Text = Convert.ToString(dtEdit.Rows[0]["strComment"]);
                }
            }
        }

        if (e.CommandName == "Delete Comment")
        {
            ViewState["DeletePosts"] = "Delete Comments";
        }

        if (e.CommandName == "Like Comment")
        {
            objGrpstatusDO.intCommentId = Convert.ToInt32(hdnCommentId.Value);
            objGrpstatusDO.intLikeDisLike = 1;//For Like
            objGrpstatusDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objGrpstatusDO.strIpAddress = ip;
            objGrpstatusDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);

            if (lnkLikes.Text == "Unlike")
            {
                DataTable dtAction = new DataTable();
                dtAction = objGrpstatusDA.GetLikeDataTable(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.UnAddLike);
                if (Convert.ToString(dtAction.Rows[0]["Action"]) == "0")
                {
                    lnkLikeComment.Text = Convert.ToString(Convert.ToInt32(lnkLikeComment.Text) + 1);
                }
            }
            else
            {
                DataTable dtAction = new DataTable();
                dtAction = objGrpstatusDA.GetLikeDataTable(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.AddLike);
                if (Convert.ToString(dtAction.Rows[0]["Action"]) == "1")
                {
                    lnkLikeComment.Text = Convert.ToString(Convert.ToInt32(lnkLikeComment.Text) + 1);
                }
            }

            BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
        }

        if (e.CommandName == "Post Comment Details")
        {
            Response.Redirect("Home.aspx?RegId=" + ViewState["UserID"]);
        }
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        if (ViewState["hdnCommentId"] == null)
        {
            objGrpstatusDO.intStatusUpdateId = Convert.ToInt32(ViewState["hdnPostUpdateId"]);
            objGrpstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.Delete);
            lblMessage.Text = "Status deleted successfully.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.intActionId = Convert.ToInt32(ViewState["hdnPostUpdateId"]);
            objLog.strAction = "Group Wall Post";
            objLog.strActionName = ViewState["lblPostDescription"].ToString();
            objLog.strIPAddress = ip;
            objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.SectionId = 2;   // Group Wall Post 
            objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
            ViewState["DeletePosts"] = null;
            divDeletesucess.Style.Add("display", "none");
            BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
        }
        else
        {
            objGrpstatusDO.intCommentId = Convert.ToInt32(ViewState["hdnCommentId"]);
            DataTable dtDelete = new DataTable();
            dtDelete = objGrpstatusDA.GetLikeDataTable(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.DeleteComment);
            lblMessage.Text = "Comment deleted successfully.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
            objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.intActionId = Convert.ToInt32(ViewState["hdnCommentId"]);
            objLog.strAction = "Group Wall Post Comment";
            objLog.strActionName = ViewState["lblstr"].ToString();
            objLog.strIPAddress = ip;
            objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.SectionId = 1;   // User Wall Post 
            objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
            ViewState["DeletePosts"] = null;
            ViewState["hdnCommentId"] = null;
            divDeletesucess.Style.Add("display", "none");
        }
    }

    protected void txtComment_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            TextBox textComment = (TextBox)sender;
            ListViewItem Item = (ListViewItem)textComment.Parent.Parent.Parent.Parent.Parent;
            if (Session["SubmitTime"].ToString() == ViewState["SubmitTime"].ToString())
            {
                HiddenField hdnPostUpdateId = Item.FindControl("hdnPostUpdateId") as HiddenField;
                Label lblCommentError = Item.FindControl("lblCommentError") as Label;
                //UpdatePanel upcomment = (UpdatePanel)Item.FindControl("upcomment");

                if (textComment.Text != "" && textComment.Text != "Write a comment" && textComment.Text != null)
                {
                    objGrpstatusDO.intStatusUpdateId = Convert.ToInt32(hdnPostUpdateId.Value);
                    objGrpstatusDO.strComment = textComment.Text;
                    objGrpstatusDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
                    string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ip == null)
                        ip = Request.ServerVariables["REMOTE_ADDR"];
                    objGrpstatusDO.strIpAddress = ip;
                    objGrpstatusDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);

                    objGrpstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.AddComment);

                    Session["SubmitTime"] = DateTime.Now.ToString();

                }
                else
                {
                    lblCommentError.Text = "Please enter comment";
                    lblCommentError.ForeColor = System.Drawing.Color.Red;
                    count = 1;
                }

            }
            string ID = "ctl00_ContentPlaceHolder1_lstPostUpdates_ctrl" + Item.DataItemIndex + "_txtComment";
            hdnTabId.Value = ID;
            textComment.Text = "";
            BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
            if (count == 0)
            {
                lblMessage.Text = "Comment Saved Successfully";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
        }
        catch
        {

        }
    }

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
        BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
    }

    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = "1";
        BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
    }

    protected void lnkLast_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = hdnLastPage.Value;
        BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
    }

    protected void lnkPrevious_Click(object sender, EventArgs e)
    {

        hdnCurrentPage.Value = hdnPreviousPage.Value;
        BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
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
                BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall

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
                    lnkPageLink.CssClass = "buttonPaging";
                    lnkPageLink.Enabled = false;
                }

                else
                {
                    lnkPageLink.CssClass = "buttonActPaging";
                    lnkPageLink.Enabled = true;
                }
            }
        }
    }

    #endregion

    #endregion

    #region Connection, Groups, Discussions
    
    protected void lnkGroups_Click(object sender, EventArgs e)
    {
        Response.Redirect("SearchGroup.aspx?ViewAll=1");
    }

    #endregion

    #endregion

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
            BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
            count = NextPage - 10;
            String ID = "ctl00_ContentPlaceHolder1_lstPostUpdates_ctrl" + count + "_txtComment";
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
            String ID = "ctl00_ContentPlaceHolder1_lstPostUpdates_ctrl" + count + "_txtComment";
            hdnLoader.Value = ID;
            pLoadMore.Style.Add("display", "none");
            lblNoMoreRslt.Visible = true;
        }
    }


}