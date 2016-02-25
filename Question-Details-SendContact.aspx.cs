using System;
using System.Data;
using DA_SKORKEL;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class Question_Details_SendContact : System.Web.UI.Page
{
    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DA_Scrl_UserQAPosting objDAQAPosting = new DA_Scrl_UserQAPosting();
    DO_Scrl_UserQAPosting objDOQAPosting = new DO_Scrl_UserQAPosting();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();
    DataTable dt = new DataTable();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divDeletesucess.Style.Add("display", "none");
            divSuccess.Style.Add("display", "none");
            Session["SubmitTime"] = DateTime.Now.ToString();
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"]);
            }

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Q&A";
            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"]);
            BindQADetails();
            getInviteeName();
            GetRelatedQuestion();
            GetAllReplies();
            if (!string.IsNullOrEmpty(Request.QueryString["MP"]))
            {
                ViewState["MP"] = "MP";
            }

        }
    }

    protected void BindQADetails()
    {
        int PostQuestionId = Convert.ToInt32(Request.QueryString["PostQAId"]);
        objDOQAPosting.intPostQuestionId = PostQuestionId;
        dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetSingleQueAnsDetails);
        if (dt.Rows.Count > 0)
        {
            lstParentQADetails.DataSource = dt;
            lstParentQADetails.DataBind();
        }
       
    }

    protected void lstParentQADetails_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnPostQuestionID = (HiddenField)e.Item.FindControl("hdnPostQuestionID");
        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
        ListView lstSubjCategory = (ListView)e.Item.FindControl("lstSubjCategory");
        Panel pnlAttachFile = (Panel)e.Item.FindControl("pnlAttachFile");
        Label lblAttachDocs = (Label)e.Item.FindControl("lblAttachDocs");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        Label Label1 = (Label)e.Item.FindControl("Label1");
        ViewState["Labelname"] = Label1.Text;
        ViewState["QAUserId"] = hdnintAddedBy.Value;

        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
        DataTable dtChildContext = new DataTable();
        DataTable dtt = new DataTable();

        if (hdnPostQuestionID.Value != "" && hdnPostQuestionID.Value != null)
        {
            DataTable dtsub = new DataTable();
            objCategory.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
            dtsub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.GetCatNameByPostQuestionId);
            if (dtsub.Rows.Count > 0)
            {
                lstSubjCategory.DataSource = dtsub;
                lstSubjCategory.DataBind();
            }
            else
            {
                lstSubjCategory.DataSource = null;
                lstSubjCategory.DataBind();
            }
        }
        Label lblTotallike = (Label)e.Item.FindControl("lblTotallike");
        Label lblreply = (Label)e.Item.FindControl("lblreply");
        Label lblShare = (Label)e.Item.FindControl("lblShare");
        LinkButton btnLike = (LinkButton)e.Item.FindControl("btnLike");
        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
        objDOQAPosting.intAddedBy =Convert.ToInt32(ViewState["UserID"]);
        dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetTotalLikeByById);
        if (dt.Rows.Count > 0)
        {
            int TotalLike = Convert.ToInt32(dt.Rows[0]["TotalLike"]);
            int TotalReply = Convert.ToInt32(dt.Rows[0]["TotalReply"]);
            int TotalShare = Convert.ToInt32(dt.Rows[0]["TotalShare"]);
            lblTotallike.Text = Convert.ToString(TotalLike);
            lblreply.Text = Convert.ToString(TotalReply);
            lblShare.Text = Convert.ToString(TotalShare);
            if(dt.Rows[0]["LikeUserId"].ToString()==Convert.ToString(ViewState["UserID"]))
            {
                btnLike.Text = "Unlike";
            }
        }
        else
        {
            lblTotallike.Text = "0";
            lblreply.Text = "0";
            lblShare.Text = "0";
        }
        lblTotallike.ToolTip = "View Likes";
        dtt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetLikeUser);
        if (dtt.Rows.Count > 0)
        {
            for (int i = 0; i < dtt.Rows.Count; i++)
            {
                if (lblTotallike.ToolTip != "View Likes")
                    lblTotallike.ToolTip += Convert.ToString(dtt.Rows[i]["UserName"]) + Environment.NewLine;
                else
                    lblTotallike.ToolTip = Convert.ToString(dtt.Rows[i]["UserName"]) + Environment.NewLine;
            }
        }

    }

    protected void lstParentQADetails_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPostQuestionID = (HiddenField)e.Item.FindControl("hdnPostQuestionID");
        LinkButton btnLike = (LinkButton)e.Item.FindControl("btnLike");
        divDeletesucess.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");
        PopUpShare.Style.Add("display", "none");

        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
        if (e.CommandName == "LikeForum")
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objDOQAPosting.strIPAddress = ip;
            objDOQAPosting.strRepLiShStatus = "LI";
            objDOQAPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
            objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.LikeQAInsert);
            PopUpShare.Style.Add("display", "none");

        }
        if (e.CommandName == "QADetails")
        {
            Response.Redirect("Question-Details-SendContact.aspx?PostQAId=" + hdnPostQuestionID.Value);
        }
        if (e.CommandName == "ShareForum")
        {
           divSuccess.Style.Add("display", "none");
            clearsharepopup();
            string path = Request.Url.AbsoluteUri;
            txtLink.Text = path;
            PopUpShare.Style.Add("display", "block");
            ViewState["PostQuestionID"] = Convert.ToInt32(hdnPostQuestionID.Value);

        }
        if (e.CommandName == "ReplyPost")
        {
            PopUpShare.Style.Add("display", "none");
            CKEditorControl.Focus();
            pnlComment.Style.Add("display", "block");
            divSuccess.Style.Add("display", "none");
         }
        BindQADetails();
    }

    protected void clearsharepopup()
    {
        txtInviteMembers.Controls.Clear();
        getInviteeName();
        txtBody.InnerText = "";
        txtLink.Text = "";
        lblMess.Text = "";
    }

   protected void lnkPopupOK_Click(object sender, EventArgs e)
    {
        if (hdnInvId.Value != "")
        {
            lblmsg.Text = "";
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objDOQAPosting.intPostQuestionId = Convert.ToInt32(Request.QueryString["PostQAId"]);
            objDOQAPosting.strIPAddress = ip;
            objDOQAPosting.strRepLiShStatus = "SH";
            objDOQAPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objDOQAPosting.strInvitee = hdnInvId.Value;

            if (txtBody.InnerText.Trim().Replace("'", "''") != "Message")
            {
                objDOQAPosting.strComment = txtBody.InnerText.Trim().Replace("'", "''");
            }
            else
            {
                objDOQAPosting.strComment = "";
            }

            objDOQAPosting.strSharelink = txtLink.Text;
            objDOQAPosting.strFileName =Convert.ToString(ViewState["Labelname"]);
            objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.InsertShare);
            BindQADetails();
            hdnInvId.Value = "";
            txtInviteMembers.Controls.Clear();
            txtBody.InnerText = "";
            getInviteeName();
            PopUpShare.Style.Add("display", "none");
        }
        else
        {
            lblmsg.Text = "Please select members.";
            return;
        }
    }

    protected void getInviteeName()
    {
        DataTable dtDoc = new DataTable();
        objdonetwork.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
        dtDoc = objdanetwork.GetUserConnections(objdonetwork, DA_Networks.NetworkDetails.ConnectedUsers);
        if (dtDoc.Rows.Count > 0)
        {
            txtInviteMembers.DataSource = dtDoc;
            txtInviteMembers.DataValueField = "intInvitedUserId";
            txtInviteMembers.DataTextField = "Name";
            txtInviteMembers.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        PopUpShare.Style.Add("display", "none");
        if (CKEditorControl.InnerText != "")
        {
            string docPath = "";
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objDOQAPosting.intPostQuestionId = Convert.ToInt32(Request.QueryString["PostQAId"]);
            objDOQAPosting.strIPAddress = ip;

            if (ViewState["Edit"] == null)
            {
                if (uploadDoc.HasFile)
                {

                    int FileLength = uploadDoc.PostedFile.ContentLength;
                    string ext = System.IO.Path.GetExtension(this.uploadDoc.PostedFile.FileName);

                    if (FileLength <= 3145728)
                    {
                        docPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(uploadDoc.FileName).ToString();
                        uploadDoc.SaveAs(Server.MapPath("~\\UploadQuetionAns\\" + docPath));
                        ViewState["docPath"] = docPath;
                    }
                    
                }
                objDOQAPosting.strFilePath = docPath;
                objDOQAPosting.strFileName = uploadDoc.FileName;
                objDOQAPosting.strRepLiShStatus = "CM";
                objDOQAPosting.strComment = CKEditorControl.InnerText.Trim().Replace(",", "''");
                if (chkPrMess.Checked == true)
                {
                    objDOQAPosting.PrvateMessage = 1;
                }
                else
                {
                    objDOQAPosting.PrvateMessage = 0;
                }
                objDOQAPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.InsertComment);

                if (ISAPIURLACCESSED != "0")
                {
                    try
                    {
                        String url = APIURL + "addAnswer.action?" +
                            "answerId=" + objDOQAPosting.intPostQuestionId +
                                    "&questionId=" + Convert.ToInt32(Request.QueryString["PostQAId"]) +
                                    "&userId=" + objDOQAPosting.intAddedBy +
                                    "&userName=" + null +
                                    "&insertDt=" + DateTime.Now +
                                    "&content=" + objDOQAPosting.strComment +
                                    "&title=" + objDOQAPosting.strComment;

                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                        myRequest1.Method = "GET";
                        WebResponse myResponse1 = myRequest1.GetResponse();

                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = url;
                        objAPILogDO.strAPIType = "Add Answer";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = ip;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                    catch { }
                }


                CKEditorControl.InnerText = "";
                chkPrMess.Checked = false;
                BindQADetails();
                GetAllReplies();
                lblSuccess.Text = "Answers Added Succesfully.";
                divSuccess.Style.Add("display", "block");
                lblSuMess.Text = "";
                lblMess.ForeColor = System.Drawing.Color.Green;
                ViewState["Edit"] = null;
            }
            else
            {

                if (uploadDoc.HasFile)
                {

                    int FileLength = uploadDoc.PostedFile.ContentLength;
                    string ext = System.IO.Path.GetExtension(this.uploadDoc.PostedFile.FileName);

                    if (FileLength <= 3145728)
                    {
                        docPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(uploadDoc.FileName).ToString();
                        uploadDoc.SaveAs(Server.MapPath("~\\UploadQuetionAns\\" + docPath));
                        ViewState["docPath"] = docPath;
                    }
                   
                }
                objDOQAPosting.strFilePath = docPath;
                objDOQAPosting.strFileName = uploadDoc.FileName;
                objDOQAPosting.strRepLiShStatus = "CM";
                objDOQAPosting.strComment = CKEditorControl.InnerText.Trim().Replace(",", "''");
                if (chkPrMess.Checked == true)
                {
                    objDOQAPosting.PrvateMessage = 1;
                }
                else
                {
                    objDOQAPosting.PrvateMessage = 0;
                }
                objDOQAPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objDOQAPosting.intQAReplyLikeShareId = Convert.ToInt32(ViewState["Edit"]);
                objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.UpdateComment);

                if (ISAPIURLACCESSED != "0")
                {
                    try
                    {
                        String url = APIURL + "addAnswer.action?" +
                            "answerId=" + objDOQAPosting.intPostQuestionId +
                                    "&questionId=" + Convert.ToInt32(Request.QueryString["PostQAId"]) +
                                    "&userId=" + objDOQAPosting.intAddedBy +
                                    "&userName=" + null +
                                    "&insertDt=" + DateTime.Now +
                                    "&content=" + objDOQAPosting.strComment +
                                    "&title=" + objDOQAPosting.strComment;

                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                        myRequest1.Method = "GET";
                        WebResponse myResponse1 = myRequest1.GetResponse();

                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = url;
                        objAPILogDO.strAPIType = "Update Answer";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = ip;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                    catch { }
                }


                CKEditorControl.InnerText = "";
                chkPrMess.Checked = false;
                BindQADetails();
                GetAllReplies();
                lblSuccess.Text = "Answers Update Succesfully.";
                divSuccess.Style.Add("display", "block");
                lblSuMess.Text = "";
                lblMess.ForeColor = System.Drawing.Color.Green;
                ViewState["Edit"] = null;
            }


        }
        else
        {
            divSuccess.Style.Add("display", "none");
            lblSuMess.Text = "Please enter your answer.";
            lblSuMess.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void GetRelatedQuestion()
    {
        dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetRelatedAllQuestion);
        if (dt.Rows.Count > 0)
        {
            lstRelQuestions.DataSource = dt;
            lstRelQuestions.DataBind();
        }
    }

    protected void GetAllReplies()
    {
        objDOQAPosting.intPostQuestionId =Convert.ToInt32(Request.QueryString["PostQAId"]);
        if (Convert.ToString(ViewState["ViewAll"]) == "ViewAll")
        {
            dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetAllDetailsOfReplies);
            ViewState["ViewAll"] = "Close";
        }
        else
        {
            dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetAllReplies);
            ViewState["ViewAll"] = "View more";
        }

        if (dt.Rows.Count > 0)
        {
            lstAllReplies.Visible = true;
            lstAllReplies.DataSource = dt;
            lstAllReplies.DataBind();
        }
        else
        {
            lstAllReplies.Visible = false;
            lstAllReplies.DataSource = null;
            lstAllReplies.DataBind();
        }
    }

    protected void GetViewMoreDetails()
    {
        objDOQAPosting.intPostQuestionId = Convert.ToInt32(Request.QueryString["PostQAId"]);
        objDOQAPosting.intQAReplyLikeShareId =Convert.ToInt32(ViewState["QAReplyLikeShareId "]);
        if (Convert.ToString(ViewState["ViewAll"]) == "ViewAll")
        {
            dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetViewMoreDetails);
            ViewState["ViewAll"] = "Close";
        }
        else
        {
            dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetAllReplies);
            ViewState["ViewAll"] = "View more";
        }

        if (dt.Rows.Count > 0)
        {
            lstAllReplies.DataSource = dt;
            lstAllReplies.DataBind();
        }
    }

    protected void lstAllReplies_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Panel pnlAttachFile = (Panel)e.Item.FindControl("pnlAttachFile");
        Label lblAttachDocs=(Label)e.Item.FindControl("lblAttachDocs");
        LinkButton lnkClose = (LinkButton)e.Item.FindControl("lnkClose");
        Label lblReplyComment = (Label)e.Item.FindControl("lblReplyComment");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HiddenField hdnintPrivateMessage = (HiddenField)e.Item.FindControl("hdnintPrivateMessage");
        HtmlControl QArep = (HtmlControl)e.Item.FindControl("QArep");
        HiddenField hdnimgprofile = (HiddenField)e.Item.FindControl("hdnimgprofile");
        HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
        Label lblDate = (Label)e.Item.FindControl("lblDate");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
        LinkButton lnkdelete = (LinkButton)e.Item.FindControl("lnkdelete");

        if (Convert.ToInt32(ViewState["UserID"]) == Convert.ToInt32(hdnintAddedBy.Value))
        {
            lnkdelete.Visible = true;
            lnkEdit.Visible = true;
        }

        if (lblDate.Text == DateTime.Today.ToString("dd MMM yyyy"))
        {
            lblDate.Text = "Today";
        }
        else if (lblDate.Text == DateTime.Today.AddDays(-1).ToString("dd MMM yyyy"))
        {
            lblDate.Text = "Yesterday";
        }

        if (imgprofile.Src == "/CroppedPhoto/")
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

        if (hdnintPrivateMessage.Value == "1")
        {           
            if (Convert.ToString(ViewState["QAUserId"]) !=Convert.ToString(ViewState["UserID"]))
            {
                QArep.Visible = false;
            }

            if (hdnintAddedBy.Value == ViewState["UserID"].ToString())
            {
                QArep.Visible = true;
            }
        }

        if (Convert.ToString(ViewState["ViewAll"]) == "Close")
        {
            lnkClose.Style.Add("display", "block");
            lnkClose.Text = "Close";
            if (lblAttachDocs.Text != "" && lblAttachDocs.Text != null)
            {
                pnlAttachFile.Style.Add("display", "block");
            }
            else
            {
                pnlAttachFile.Style.Add("display", "none");
            }
        }        
    }

    protected void lstAllReplies_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        divSuccess.Style.Add("display", "none");
        PopUpShare.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");
        DataTable dtAns = new DataTable();
        LinkButton lnkClose = (LinkButton)e.Item.FindControl("lnkClose");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HiddenField hdnQAReplyLikeShareId = (HiddenField)e.Item.FindControl("hdnQAReplyLikeShareId");
        Label lblReplyComment = e.Item.FindControl("lblReplyComment") as Label;
        ViewState["QAReplyLikeShareId "] = hdnQAReplyLikeShareId.Value;
        if (e.CommandName == "View More")
        {
            GetAllReplies();
            divSuccess.Style.Add("display", "none");
        }else
        if (e.CommandName == "View Close")
        {
            GetAllReplies();
            divSuccess.Style.Add("display", "none");
        }
        else
        if (e.CommandName == "Edit Ans")
        {
            ViewState["Edit"] = hdnQAReplyLikeShareId.Value;
            objDOQAPosting.intQAReplyLikeShareId = Convert.ToInt32(hdnQAReplyLikeShareId.Value);
            dtAns = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetCommentByID);
            if (dtAns.Rows.Count > 0)
            {
                CKEditorControl.InnerText = Convert.ToString(dtAns.Rows[0]["strComment"]);
                CKEditorControl.Focus();
                ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#editqa';", true);
            }

        }
        else
        if (e.CommandName == "Delete Ans")
        {
            ViewState["lblReplyComment"] = lblReplyComment.Text;
            divDeletesucess.Style.Add("display", "block");
        }
        else
            if (e.CommandName == "Details")
            {
                Response.Redirect("Home.aspx?RegId=" + hdnintAddedBy.Value);
            }
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        objDOQAPosting.intQAReplyLikeShareId = Convert.ToInt32(ViewState["QAReplyLikeShareId "]);
        objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.DeleteComment);
        GetAllReplies();
        divDeletesucess.Style.Add("display", "none");
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(ViewState["QAReplyLikeShareId"]);
        objLog.strAction = "Q&A Ans";
        objLog.strActionName = Convert.ToString(ViewState["lblReplyComment"]); ;
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 25;   // Q&A Delete
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
    }

    protected void btnAllQuestion_Click(object sender, EventArgs e)
    {
        Response.Redirect("AllQuestionDetails.aspx");
    }

    protected void lstRelQuestions_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPostQuestionId = (HiddenField)e.Item.FindControl("hdnPostQuestionId");
        if (e.CommandName == "OpenQ")
        {
            Response.Redirect("Question-Details-SendContact.aspx?PostQAId=" + hdnPostQuestionId.Value);
        }
    }

    protected void lnkBack_click(object sender, EventArgs e)
    {
        if (ViewState["MP"] != null)
        {
            Response.Redirect("AllQuestionDetails.aspx?MP=" +Convert.ToString(ViewState["MP"]));
        }
        else
        {
            Response.Redirect("AllQuestionDetails.aspx");
        }
    }

}