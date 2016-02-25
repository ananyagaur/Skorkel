using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DA_SKORKEL;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using iTextSharp.tool.xml;


public partial class Research_Case_Details : System.Web.UI.Page
{
    DO_Role ObjRole = new DO_Role();
    DA_Role ObjRoleDB = new DA_Role();
    DA_Case objdacaseDB = new DA_Case();

    DA_CaseComment objdacase = new DA_CaseComment();
    DO_Case objdocase = new DO_Case();

    DO_Comment objcomment = new DO_Comment();
    DA_Comment dbcomment = new DA_Comment();

    DO_CaseList objDoCaseList = new DO_CaseList();
    DA_CaseList objCaseListDb = new DA_CaseList();

    DA_ContentTagDef objTagDefDB = new DA_ContentTagDef();
    DO_ContentTagDef objTagDef = new DO_ContentTagDef();

    DO_ContentLink objLink = new DO_ContentLink();
    DA_ContentLink objLinkDb = new DA_ContentLink();

    DO_ContentRating objRating = new DO_ContentRating();
    DA_ContentRating objRatingDb = new DA_ContentRating();

    DA_ContentFact objFactDB = new DA_ContentFact();
    DO_ContentFact objFact = new DO_ContentFact();

    DO_ContentRatio objRatio = new DO_ContentRatio();
    DA_ContentRatio objRatioDB = new DA_ContentRatio();

    DA_ContentSummary objSummaryDb = new DA_ContentSummary();
    DO_ContentSummary objSummary = new DO_ContentSummary();

    DO_Mark objmark = new DO_Mark();
    DA_Mark dbmark = new DA_Mark();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DA_Ratio objDARatio = new DA_Ratio();
    DO_Ratio objDORatio = new DO_Ratio();

    DO_Scrl_UserForumPosting objDOBForumPosting = new DO_Scrl_UserForumPosting();
    DA_Scrl_UserForumPosting objDAForumPosting = new DA_Scrl_UserForumPosting();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_ContentRelevantUser objDORelevntU = new DO_ContentRelevantUser();
    DA_ContentRelevantUser objDARelevntU = new DA_ContentRelevantUser();

    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();
    Int32 CaseID = 0;
    Int64 CaseId = 0, ContentTypeID = 0;
    string BrowserUsing = "";
    string strTagTypeId = string.Empty, Doc = "";
    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];
    string UserTypeId = "S";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            hdnLoginId.Value = Convert.ToString(ViewState["UserID"]);
        }

        if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
            ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());


        int ii = Convert.ToInt32(hdnBack.Value);
        ii++;
        hdnBack.Value = ii.ToString();

        if (Request.QueryString["CTid"] != null && Request.QueryString["CTid"] != "")
        {
            ContentTypeID = Convert.ToInt32(Request.QueryString["CTid"].Trim());
            ViewState["CTypeID"] = Convert.ToInt32(Request.QueryString["CTid"].Trim());
        }

        if (Request.QueryString["cId"] != null && Request.QueryString["cId"] != "")
        {
            CaseID = Convert.ToInt32(Request.QueryString["cId"]);
            ViewState["ContentID"] = Convert.ToInt32(Request.QueryString["cId"].Trim());
        }

        if (Request.QueryString["intCommentAddedFor"] != null && Request.QueryString[""] != "intCommentAddedFor")
        {
            ViewState["intCommentAddedFor"] = Convert.ToInt32(Request.QueryString["intCommentAddedFor"].Trim());
        }

        PopUpShare.Style.Add("display", "none");
        if (!IsPostBack)
        {
            System.Web.HttpBrowserCapabilities browser = Request.Browser;
            BrowserUsing = browser.Browser;

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Research";

            intCommentAddedFors.Value = "";
            lblOriginalTxt.Text = "Original Text";
            ViewState["LoginName"] = Convert.ToString(Session["LoginName"]);
            objDoCaseList.intDocId = CaseID;
            objDoCaseList.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            DataTable dtt = objCaseListDb.GetMicroTagDataTable(objDoCaseList, DA_CaseList.MicroTagLikeShare.GetfavrtDoc);
            if (dtt.Rows.Count > 0)
            {
                imgFvrt.Src = "~/images/red-tag.png";
            }
            else
            {
                imgFvrt.Src = "~/images/gray-tag.png";
            }

            if (ViewState["intCommentAddedFor"] == null)
            {
                ReleventUser();
                GetDocDetails();
                ShowDiv(CaseId, ContentTypeID);
                getInviteeName();
            }
            else
            {
                int jj = Convert.ToInt32(Request.QueryString["hdnHistoryback"]);
                jj++;
                hdnBack.Value = jj.ToString();

                hdnComments.Value = "comment";
                hdnCommentAddedFor.Value = ViewState["intCommentAddedFor"].ToString();
                objDORelevntU.AddedBy = Convert.ToInt32(ViewState["intCommentAddedFor"].ToString());
                DataTable dt = new DataTable();
                dt = objDARelevntU.GetDataTable(objDORelevntU, DA_ContentRelevantUser.RelevantUser.GetUserName);
                if (dt.Rows.Count > 0)
                {
                    lblOriginalTxt.Text = dt.Rows[0]["NAME"].ToString();
                }

                lnkWriteButton.Style.Add("display", "block");
                ReleventUser();
                GetDocDetails();
                ShowDiv(CaseId, ContentTypeID);
            }
        }
        ReleventUser();
        getInviteeName();
    }

    #region Doc Relevent User

    protected void ReleventUser()
    {
        objDORelevntU.CaseId = CaseID;
        objDORelevntU.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
        objDORelevntU.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        DataTable dt = new DataTable();
        dt = objDARelevntU.GetDataTable(objDORelevntU, DA_ContentRelevantUser.RelevantUser.GetRelevantUserDetails);
        if (dt.Rows.Count > 0)
        {
            lstUserActivityCase.DataSource = dt;
            lstUserActivityCase.DataBind();
        }
    }

    protected void lstUserActivityCase_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnintCommentAddedFor = (HiddenField)e.Item.FindControl("hdnintCommentAddedFor");
        HiddenField hdnintContentId = (HiddenField)e.Item.FindControl("hdnintContentId");
        LinkButton lnkName = (LinkButton)e.Item.FindControl("lnkName");
        LinkButton lnkTitle = (LinkButton)e.Item.FindControl("lnkTitle");
        ViewState["intCommentAddedFor"] = hdnintCommentAddedFor.Value;
        intCommentAddedFors.Value = hdnintCommentAddedFor.Value;
        if (e.CommandName == "UserName")
        {
            if (hdnintCommentAddedFor.Value != ViewState["UserID"].ToString())
            {
                objDORelevntU.CaseId = Convert.ToInt32(hdnintContentId.Value);
                objDORelevntU.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
                objDORelevntU.AddedBy = Convert.ToInt32(ViewState["UserID"]);
                objDORelevntU.intViewId = Convert.ToInt32(ViewState["UserID"]);
                objDORelevntU.intDocAddedBy = Convert.ToInt32(hdnintCommentAddedFor.Value);

                DataTable dt = new DataTable();
                dt = objDARelevntU.GetDataTable(objDORelevntU, DA_ContentRelevantUser.RelevantUser.InsertDocViewDetails);
            }
            usercommentLoad(objDORelevntU.CaseId, objDORelevntU.ContentTypeID);
        }
        if (e.CommandName == "Title")
        {
            if (hdnintCommentAddedFor.Value != ViewState["UserID"].ToString())
            {
                objDORelevntU.CaseId = Convert.ToInt32(hdnintContentId.Value);
                objDORelevntU.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
                objDORelevntU.AddedBy = Convert.ToInt32(ViewState["UserID"]);
                objDORelevntU.intViewId = Convert.ToInt32(ViewState["UserID"]);
                objDORelevntU.intDocAddedBy = Convert.ToInt32(hdnintCommentAddedFor.Value);

                DataTable dt = new DataTable();
                dt = objDARelevntU.GetDataTable(objDORelevntU, DA_ContentRelevantUser.RelevantUser.InsertDocViewDetails);
            }
            usercommentLoad(objDORelevntU.CaseId, objDORelevntU.ContentTypeID);
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "myTags1", "onlinkclick();", true);
    }

    protected void usercommentLoad(long CaseId1, long ContentTypeID1)
    {
        hdnComments.Value = "comment";
        hdnCommentAddedFor.Value = ViewState["intCommentAddedFor"].ToString();
        objDORelevntU.AddedBy = Convert.ToInt32(ViewState["intCommentAddedFor"].ToString());
        DataTable dt = new DataTable();
        dt = objDARelevntU.GetDataTable(objDORelevntU, DA_ContentRelevantUser.RelevantUser.GetUserName);
        if (dt.Rows.Count > 0)
        {
            lblOriginalTxt.Text = dt.Rows[0]["NAME"].ToString();
        }

        lnkWriteButton.Style.Add("display", "block");
        ReleventUser();
        GetDocDetails();
        ShowDiv(CaseId1, ContentTypeID1);
        BindComments();
    }

    protected void lstUserActivityCase_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
            HiddenField hdnimgprofile = (HiddenField)e.Item.FindControl("hdnimgprofile");
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
        }
    }

    #endregion

    #region text to HTML

    private void GetDocDetails()
    {
        objdocase.CaseId = Convert.ToInt32(ViewState["ContentID"]);
        DataTable dt = new DataTable();
        objdocase.CaseId = CaseID;
        dt = objdacase.GetDataTable(objdocase, DA_CaseComment.CaseComment.GetDocsDetails);
        if (dt.Rows.Count > 0)
        {
            DataTable dtJudgeName = new DataTable();
            DataRow row;
            DataColumn JudgeName = new DataColumn();
            JudgeName.DataType = System.Type.GetType("System.String");
            JudgeName.ColumnName = "JudgeName";
            dtJudgeName.Columns.Add(JudgeName);
            lblpartyname.Text = Convert.ToString(dt.Rows[0]["strPartyNames"]);
            lblcitation.Text = Convert.ToString(dt.Rows[0]["strCitation"]);
            lblcourt.Text = Convert.ToString(dt.Rows[0]["strJurisdiction"]);
            lblyear.Text = Convert.ToString(dt.Rows[0]["intYear"]);
            lblCitedBy.Text = Convert.ToString(dt.Rows[0]["strCitedBy"]);
            string Jname = Convert.ToString(dt.Rows[0]["strJudgeNames"]);
            if (Jname != "")
            {
                string[] JugName = Jname.Split(';');
                for (int i = 0; i < JugName.Length; i++)
                {
                    if (Convert.ToString(JugName.GetValue(i)) != "")
                    {
                        row = dtJudgeName.NewRow();
                        row["JudgeName"] = Convert.ToString(JugName.GetValue(i));
                        dtJudgeName.Rows.Add(row);
                    }
                }

                lstJudgeName.DataSource = dtJudgeName;
                lstJudgeName.DataBind();
            }
            else
            {
                lstJudgeName.DataSource = null;
                lstJudgeName.DataBind();
            }

            if (dt.Rows[0]["strFilePath"].ToString() != "" && dt.Rows[0]["strFilePath"].ToString() != null)
            {
                lblCaseTitle.Text = dt.Rows[0]["strCaseTitle"].ToString();
                ViewState["hdnDocTitle"] = dt.Rows[0]["strCaseTitle"].ToString();
                try
                {
                    Document document = new Document();
                    //string path = Request.PhysicalApplicationPath + "\\uploaddocument\\" + dt.Rows[0]["strfilepath"].ToString();
                    string path = Request.PhysicalApplicationPath + "\\CaseDocument\\" + dt.Rows[0]["strfilepath"].ToString();
                    string content = File.ReadAllText(path);
                    ViewState["Doc"] = TextToHtml(content);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    return;
                }
            }
        }
    }

    public static string TextToHtml(string text)
    {
        text = HttpUtility.HtmlEncode(text);
        StringBuilder b = new StringBuilder(text);
        b = b.Replace("&quot; /&gt;<br>", "<br>");               // &lt;span class=&quot;LegSpan&quot; style=&quot;cursor:hand&quot; onclick=&quot;#&quot;&gt;
        b = b.Replace("&lt;span&gt;", "");
        b = b.Replace("&lt;span class=&quot;LegSpan&quot; style=&quot;cursor:hand&quot; onclick=&quot;#&quot;&gt;", "");
        b = b.Replace("&lt;span style=&quot;font-family: Verdana;&quot;&gt;", "");
        b = b.Replace("&lt;/span&gt;", "");
        b = b.Replace("&lt;", "<");
        b = b.Replace("&gt;", ">");
        b = b.Replace("&amp;nbsp;", "");
        b = b.Replace("&#39;", "'");
        b = b.Replace("&amp;", "&");
        b = b.Replace("&sbquo;", ",");
        b = b.Replace("&hellip;", "…");
        b = b.Replace("&permil;", "‰");
        b = b.Replace("&trade;", "™");
        b = b.Replace("&iexcl;", "¡");
        b = b.Replace("&brvbar;", "¦");
        b = b.Replace("&sup2;", "²");
        b = b.Replace("&sup3;", "³");
        b = b.Replace("&Auml;", "Ä");
        b = b.Replace("&tilde;", "˜");

        int count = CountStringOccurrences(b.ToString(), "</div>");
        b = b.Replace("</div>", "");
        b = b.Replace("</i>", "");
        b = b.Replace("<i>", "");
        string finaltext;
        text = b.ToString();
        if (count > 0)
        {
            int txtL = text.Length;
            for (int i = 0; i < count; i++)
            {
                finaltext = text.Insert(txtL, "</div>");
                text = finaltext;
            }
        }
        //text.IndexOf(" ", 0);
        //text.Replace(
        return text;
    }

    public static int CountStringOccurrences(string text, string pattern)
    {
        // Loop through all instances of the string 'text'.
        int count = 0;
        int i = 0;
        while ((i = text.IndexOf(pattern, i)) != -1)
        {
            i += pattern.Length;
            count++;
        }
        return count;
    }

    public void BindList(Int64 CaseId)
    {
        DataTable dt = new DataTable();
        objdocase.CaseId = CaseID;
        dt = objdacase.GetDataTable(objdocase, DA_CaseComment.CaseComment.GetRecord);
        Doc = Convert.ToString(ViewState["Doc"]);
        if (!string.IsNullOrEmpty(Doc))
        {
            objDoCaseList.Caseid = CaseID;
            objDoCaseList.ContentTypeId = 1;
            objDoCaseList.AddedBy = Convert.ToInt32(ViewState["UserID"]);
            divdisp.InnerHtml = Doc.ToString().Replace("&lt;br&gt;", "<br />");

            if (Session["ExternalUserId"] != null)
            {
                String DescByRoleGroup = Doc.ToString().Replace("&lt;br&gt;", "<br />");
                divdisp.Visible = true;
                divGuest.Visible = false;
                divdisp.InnerHtml = DescByRoleGroup;
            }
            else
            {
                Response.Redirect("Landing.aspx");
            }
        }
    }

    #endregion

    protected void SubjectTempDataTable()
    {
        //Subjects
        DataTable dtSubjCat = new DataTable();

        DataColumn SubjId = new DataColumn();
        SubjId.DataType = System.Type.GetType("System.String");
        SubjId.ColumnName = "intCategoryId";
        dtSubjCat.Columns.Add(SubjId);

        DataColumn SubjCat = new DataColumn();
        SubjCat.DataType = System.Type.GetType("System.String");
        SubjCat.ColumnName = "strCategoryName";
        dtSubjCat.Columns.Add(SubjCat);

        DataRow rwSubj = dtSubjCat.NewRow();
        ViewState["SubjectCategory"] = dtSubjCat;
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

    public void ShowDiv(Int64 ContentID, Int64 ContentTypeID)
    {
        if (ContentTypeID == 1)
        {
            BindList(ContentID);
        }
    }

    #region PostComment
    protected void BtnSaveComment_Click(object sender, EventArgs e)
    {
        objcomment.CaseId = CaseID;
        objcomment.CommentId = 0;
        string comment = txtComment.InnerText;
        objcomment.Comment = comment.Replace("&nbsp;", "").Replace("‘", "\'").Replace("’", "\'");
        ViewState["Description"] = objcomment.Comment;
        objcomment.addedby = Convert.ToInt32(ViewState["UserID"]);
        objcomment.CommentedText = "";
        objcomment.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);

        if (ViewState["intCommentAddedFor"] != null)
        {
            objcomment.intCommentAddedFor = Convert.ToInt32(ViewState["intCommentAddedFor"]);
        }
        else
        {
            objcomment.intCommentAddedFor = Convert.ToInt32(ViewState["UserID"]);
        }

        if (hdnComments.Value == "")
        {
            objcomment.strOrigenalText = "OrgText";
        }

        DataTable dt = new DataTable();
        if (comment.Replace("\n", "").Replace("\t", "").Replace("\r", "").Trim() != "")
        {
            dt = dbcomment.GetDataTable(objcomment, DA_Comment.Comment.Add);
            objSummary.PointId = 40;
            UpdateRecentActivities();
            BindComments();
            string UserURL = "";
            if (ISAPIURLACCESSED == "1")
            {
                if (ViewState["intCommentAddedFor"] != null)
                {
                    UserURL = APIURL + "createMicroTag.action?microTagId=MT" + dt.Rows[0]["intCid"].ToString() +
                    "&docUid=" + CaseID +
                    "&addByUid=" + ViewState["UserID"].ToString() +
                    "&insertDt=" + DateTime.Now +
                    "&startPos=" + null +
                    "&endPos=" + null +
                    "&content=" + objcomment.Comment +
                    "&microTagType=" + 9 +
                    "&scope=null&parentUid=null&copyUserId=" + ViewState["intCommentAddedFor"];

                }
                else
                {
                    UserURL = APIURL + "createMicroTag.action?microTagId=MT" + dt.Rows[0]["intCid"].ToString() +
                    "&docUid=" + CaseID +
                    "&addByUid=" + ViewState["UserID"].ToString() +
                    "&insertDt=" + DateTime.Now +
                    "&startPos=" + null +
                    "&endPos=" + null +
                    "&content=" + objcomment.Comment +
                    "&microTagType=" + 9 +
                    "&scope=null&parentUid=null&copyUserId=" + ViewState["UserID"];

                }

                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                    myRequest1.Method = "GET";
                    if (ISAPIResponse == "1")
                    {
                        WebResponse myResponse1 = myRequest1.GetResponse();
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.strURL = UserURL;
                        objAPILogDO.strAPIType = "Comment Content";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
            txtComment.InnerText = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallLoadImg", "CallLoadImg();", true);
        }

    }

    protected void BtnCancelComment_Click(object sender, EventArgs e)
    {
        hdnTabId.Value = "";
    }

    public void BindComments()
    {
        string s = lblOriginalTxt.Text;
        objdocase.CaseId = CaseID;
        objdocase.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
        objdocase.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (ViewState["intCommentAddedFor"] != null)
            objdocase.CommnetAddedFor = Convert.ToInt32(ViewState["intCommentAddedFor"]);
        if (objdocase.CommnetAddedFor == 0)
            objdocase.CommnetAddedFor = Convert.ToInt32(ViewState["UserID"]);

        DataTable dt = new DataTable();
        if (hdnComments.Value == "")
        {
            dt = objdacase.GetDataTable(objdocase, DA_CaseComment.CaseComment.GetCommentsOrg);
        }
        else
        {
            dt = objdacase.GetDataTable(objdocase, DA_CaseComment.CaseComment.GetComments);
        }
        if (dt.Rows.Count > 0)
        {
            ViewState["Comment"] = "1";
            lstComments.DataSource = dt;
            lstComments.DataBind();
            ViewState["CommentListSource"] = dt;
        }
        else
        {
            lstComments.DataSource = null;
            lstComments.DataBind();
        }
    }

    protected void lnkComments_Click(object sender, EventArgs e)
    {
        DivCommentContent.Style.Add("display", "block");
        txtComment.Focus();
        BindComments();
        if (hdnComments.Value != "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "myTags", "onlinkclick();", true);
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "callCommentDivs", "callCommentDiv();", true);
    }

    protected void lnkShowOrgtxt_Click(object sender, EventArgs e)
    {
        DivCommentContent.Style.Add("display", "none");
        intCommentAddedFors.Value = "";
        hdnCommentAddedFor.Value = "";
        lblOriginalTxt.Text = "Original Text";
        ViewState["LoginName"] = Convert.ToString(Session["LoginName"]);
        objDoCaseList.intDocId = CaseID;
        objDoCaseList.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        DataTable dtt = objCaseListDb.GetMicroTagDataTable(objDoCaseList, DA_CaseList.MicroTagLikeShare.GetfavrtDoc);
        if (dtt.Rows.Count > 0)
        {
            imgFvrt.Src = "~/images/red-tag.png";
        }
        else
        {
            imgFvrt.Src = "~/images/gray-tag.png";
        }

        ViewState["intCommentAddedFor"] = null;
        intCommentAddedFors.Value = "";
        BtnSaveSummary.Style.Add("display", "block");
        ReleventUser();
        GetDocDetails();
        ShowDiv(Convert.ToInt32(ViewState["ContentID"]), Convert.ToInt32(ViewState["CTypeID"]));
        hdnComments.Value = "";
        getInviteeName();
    }

    protected void lstComment_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
            HiddenField hdnTagId = (HiddenField)e.Item.FindControl("hdnTagId");
            ListView lstCommentChild = (ListView)e.Item.FindControl("lstcommentChild");
            HiddenField hdnLinkUserId = (HiddenField)e.Item.FindControl("hdnLinkUserId");
            LinkButton ImgBtnLike = (LinkButton)e.Item.FindControl("ImgBtnLike");
            HiddenField hdnimgprofile = (HiddenField)e.Item.FindControl("hdnimgprofile");
            objdocase.CaseId = CaseID;
            objdocase.CommnetId = Convert.ToInt32(hdnTagId.Value);
            objdocase.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
            DataTable dtchild = new DataTable();
            Label lblLikes = (Label)e.Item.FindControl("lblLikes");
            objdocase.CaseId = CaseID;
            objdocase.TagId = Convert.ToInt32(hdnTagId.Value);
            objdocase.ContentTypeID = Convert.ToInt64(ViewState["ContentID"]);
            objdocase.AddedBy = Convert.ToInt32(ViewState["UserID"]);
            if (imgprofile.Src == "CroppedPhoto/")
            {
                imgprofile.Src = "images/comment-profile.jpg";
            }
            else
            {
                string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + hdnimgprofile.Value);
                if (!File.Exists(imgPathPhysical))
                {
                    imgprofile.Src = "images/comment-profile.jpg";
                }
            }

            dtchild = objdacase.GetDataTable(objdocase, DA_CaseComment.CaseComment.GetChildComment);
            if (dtchild.Rows.Count > 0)
            {
                ViewState["Comment"] = "1";
                lstCommentChild.DataSource = dtchild;
                lstCommentChild.DataBind();
                ViewState["CommentListChild"] = dtchild;
            }
            else { }
            Label lblComment = (Label)e.Item.FindControl("lblComment");
            if (lblComment.Text != "")
            {
                lblComment.Text = "''" + lblComment.Text.Trim() + "''";
            }

            HiddenField hdnAddedby = (HiddenField)e.Item.FindControl("hdnAddedby");
            HtmlGenericControl SpanAddCommentBox = (HtmlGenericControl)e.Item.FindControl("AddCommentBox");
            int addedby = Convert.ToInt32(hdnAddedby.Value);
            ObjRole.AddedBy = addedby;
            DataTable dtroleUserType = new DataTable();
            dtroleUserType = ObjRoleDB.GetDataTableTypeRole(ObjRole, DA_Role.Role.GetUserType);
            int CommentUserTypeId = 0;
            try
            {
                CommentUserTypeId = Convert.ToInt32(dtroleUserType.Rows[0]["intUserTypeID"].ToString());
            }
            catch
            {
                CommentUserTypeId = 0;
            }
        }
    }

    protected void lstComment_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (Convert.ToString(ViewState["ChildCount"]) != "1" && Convert.ToString(ViewState["ChildCount"]) == "")
        {
            hdnPostBackCheck.Value = "0";
            Label lblLikes = (Label)e.Item.FindControl("lblLikes");
            Label lblDisLike = (Label)e.Item.FindControl("lblDisLike");
            HiddenField hdnTagId = (HiddenField)e.Item.FindControl("hdnTagId");
            TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");
            Label lblCommentEdit = (Label)e.Item.FindControl("lblCommentEdit");
            LinkButton ImgBtnLike = (LinkButton)e.Item.FindControl("ImgBtnLike");
            if (e.CommandName == "Like")
            {
                if (Convert.ToString(ViewState["RatingAddRole"]) != "0" && Convert.ToString(ViewState["Comment"]) == "1")
                {
                    objRating.TagId = Convert.ToInt32(hdnTagId.Value);
                    objRating.TagType = "C";
                    objRating.Rating = 1;//For Like
                    objRating.ContentId = CaseID;
                    objRating.addedby = Convert.ToInt32(ViewState["UserID"]);
                    objRating.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
                    DataTable dtAction = new DataTable();
                    if (ImgBtnLike.Text == "Unlike")
                    {
                        dtAction = objRatingDb.GetDataTable(objRating, DA_ContentRating.ContentRating.UnlikeAdd);
                    }
                    else
                    {
                        dtAction = objRatingDb.GetDataTable(objRating, DA_ContentRating.ContentRating.Add);
                    }
                    if (dtAction.Rows[0]["Action"].ToString() == "1")
                        lblLikes.Text = Convert.ToString(Convert.ToInt32(lblLikes.Text) + 1);
                }
            }

            BindComments();
        }
        ViewState["ChildCount"] = "";
    }

    protected void lstComment_OnItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        try
        {
            hdnPostBackCheck.Value = "0";
            int index = e.ItemIndex;
            HiddenField hdnCommentId = (HiddenField)lstComments.Items[index].FindControl("hdnCommentId");
            objcomment.CommentId = Convert.ToInt32(hdnCommentId.Value);
            objcomment.CaseId = CaseID;
            objcomment.addedby = Convert.ToInt32(ViewState["UserID"]);
            objcomment.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
            dbcomment.GetDataTable(objcomment, DA_Comment.Comment.DeleteComment);
            DataTable dtComment = (DataTable)ViewState["CommentListSource"];
            dtComment.Rows.RemoveAt(index);
            lstComments.DataSource = dtComment;
            lstComments.DataBind();
            ViewState["CommentListSource"] = dtComment;
        }
        catch { }
    }

    protected void GetTotalComment()
    {
        objdocase.CaseId = CaseID;
        objdocase.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
        DataTable dt = new DataTable();
        dt = objdacase.GetDataTable(objdocase, DA_CaseComment.CaseComment.GetTotalComment);
    }

    #endregion

    #region Summary

    protected void lnkWriteButton_Onclick(object sender, EventArgs e)
    {
        string s = lblOriginalTxt.Text;
        if (lblOriginalTxt.Text == "Original Text" || lblOriginalTxt.Text == "")
            lblOriginalTxt.Text = "Original Text";

        if (ViewState["intCommentAddedFor"] != null)
        {
            if (Convert.ToInt32(ViewState["UserID"]) == Convert.ToInt32(ViewState["intCommentAddedFor"]))
            {
                BtnSaveSummary.Style.Add("display", "block");
                txtSummary.Disabled = false;
            }
            else
            {
                BtnSaveSummary.Style.Add("display", "none");
                txtSummary.Disabled = true;
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ShowSummaryPost(1, 'kiran')", true);

    }

    protected void BtnSaveSummary_Click(object sender, EventArgs e)
    {
        try
        {
            objSummary.ContentId = CaseID;
            string Summary = txtSummary.InnerText.Trim();
            objSummary.SummaryText = Summary.Replace("&nbsp;", "").Replace("‘", "\'").Replace("’", "\'");
            ViewState["Description"] = objSummary.SummaryText;
            objSummary.addedby = Convert.ToInt32(ViewState["UserID"]);
            objSummary.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);

            if (Summary.Replace("\n", "").Replace("\t", "").Replace("\r", "").Trim() != "")
            {
                objSummaryDb.AddEditDel_Summary(objSummary, DA_ContentSummary.ContentSummary.AddUpdateSummary);
                objSummary.PointId = 39;
                UpdateRecentActivities();
                string UserURL = "";
                if (ISAPIURLACCESSED == "1")
                {
                    UserURL = APIURL + "createMicroTag.action?microTagId=MT" + objSummary.SummaryId +
                    "&docUid=" + CaseID +
                    "&addByUid=" + ViewState["UserID"].ToString() +
                    "&insertDt=" + DateTime.Now +
                    "&startPos=" + null +
                    "&endPos=" + null +
                    "&content=" + objSummary.SummaryText +
                    "&microTagType=" + 8 +
                    "&scope=null&parentUid=null&copyUserId=null";

                    try
                    {
                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                        myRequest1.Method = "GET";
                        WebResponse myResponse1 = myRequest1.GetResponse();

                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = UserURL;
                        objAPILogDO.strAPIType = "Summary Content";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                    catch { }
                }

                txtSummary.InnerText = "";

            }

        }
        catch { }
    }

    protected void BtnCancelSummary_Click(object sender, EventArgs e)
    {
        hdnTabId.Value = "";
    }

    #endregion

    #region RecentActivity

    protected void UpdateRecentActivities()
    {
        try
        {
            objSummary.ContentId = CaseID;
            objSummary.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
            objSummary.addedby = Convert.ToInt32(ViewState["UserID"]);
            objSummary.strDescrption = Convert.ToString(ViewState["Description"]);
            objSummary.intUserTypeId = Convert.ToInt32(ViewState["FlagUser"]);
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objSummary.strIPAddress = ip;
            objSummary.intTagType = Convert.ToInt32(hdnTagtypeId.Value);

            if (Convert.ToInt32(hdnTagtypeId.Value) == 4)
            {
                objSummary.PointId = 35;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 5)
            {
                objSummary.PointId = 36;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 6)
            {
                objSummary.PointId = 37;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 7)
            {
                objSummary.PointId = 38;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 1)
            {
                objSummary.PointId = 32;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 2)
            {
                objSummary.PointId = 33;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 3)
            {
                objSummary.PointId = 34;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 8)
            {
                objSummary.PointId = 39;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 9)
            {
                objSummary.PointId = 40;
            }
            objSummaryDb.AddEditDel_RecentActivities(objSummary, DA_ContentSummary.RecentActivities.AddRecentActivity);
            hdnTagtypeId.Value = "";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    #endregion

    #region For Mark As Tabs
    protected void lnkIssueTab_Click(object sender, EventArgs e)
    {
        hdnComments.Value = "comment";
        hdnTabId.Value = "";
        hdnTagtypeId.Value = "2";
        lblOriginalTxt.Text = Convert.ToString(ViewState["LoginName"]);
        String MainDesc = hdndivvalue.Value.ToString();
        objDORatio.ContentTypeID = Convert.ToInt64(ViewState["CTypeID"]);
        objDORatio.CaseId = CaseID;
        objDORatio.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (BrowserUsing == "IE")
        {
            String DivHtml = hdnMarkText.Value.Replace("\r\n", "<br>");
            String WidBr = DivHtml.Replace("<br>", "#&#&#").Replace("<em>", " #&em&# ").Replace("</em>", " #&&#em&# ").Replace("<strong>", " #&strong&# ").Replace("</strong>", " #&&#strong&# ");
            String result = Regex.Replace(WidBr, @"<[^>]*>", String.Empty);

            String FinalText = result.Replace("#&#&#", "<br />").Replace(" #&strong&# ", "<strong>").Replace(" #&&#strong&# ", "</strong>").Replace(" #&em&# ", "<em>").Replace(" #&&#em&# ", "</em>");
            objDORatio.strTagDescription = FinalText;
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        else
        {
            objDORatio.strTagDescription = hdnMarkText.Value.Replace("\r\n", "<br>");
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        DataTable dt = new DataTable();
        if (Convert.ToInt32(hdnStartIdx.Value) >= 0 && Convert.ToInt32(hdnEndIdx.Value) >= 0)
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDORatio.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objDORatio.intTagType = Convert.ToInt32(2);
            DataTable dtchktitle = new DataTable();
            dtchktitle = objDARatio.GetDataTable(objDORatio, DA_Ratio.Ratio.GetTagTypeId);
            objDORatio.StartIndex = Convert.ToInt32(hdnStartIdx.Value);
            objDORatio.EndIndex = Convert.ToInt32(hdnEndIdx.Value);
            dt = objDARatio.GetMarkDataTable(objDORatio, DA_Ratio.Ratio.Add);
            UpdateRecentActivities();
            ReleventUser();
        }

        if (dt.Rows.Count > 0)
        {
            //Add API Log           
            string microTagId = Convert.ToString(dt.Rows[0]["intMarkId"]);
            string docUid = Convert.ToString(objDORatio.CaseId);
            string addByUid = Convert.ToString(ViewState["UserID"]);
            string insertDt = DateTime.Now.ToString();
            string startPos = Convert.ToString(objDORatio.StartIndex);
            string endPos = Convert.ToString(objDORatio.EndIndex);
            string content = objDORatio.strTagDescription;
            string microTagType = "2";

            if (ISAPIURLACCESSED == "1")
            {
                string UserURLs = APIURL + "createMicroTag.action?microTagId=MT" + microTagId +
                    "&docUid=" + docUid +
                    "&addByUid=" + addByUid +
                    "&insertDt=" + insertDt +
                    "&startPos=" + startPos +
                    "&endPos=" + endPos +
                    "&content=" + content +
                    "&microTagType=" + microTagType +
                    "&scope=null&parentUid=null&copyUserId=null";

                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURLs);
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strURL = UserURLs;
                        objAPILogDO.strAPIType = "Issue Content";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }
        hdndivvalue.Value = MainDesc.Replace("</FONT>", "</span>").Replace("</font>", "</span>").Replace("<font ", "<span "); ;
        BindList(CaseID);
        hdnPasteCode.Value = "";
        ScriptManager.RegisterStartupScript(this, GetType(), "myTags", "onlinkclick();", true);
    }

    protected void lnkFactTab_Click(object sender, EventArgs e)
    {
        hdnComments.Value = "comment";
        hdnTabId.Value = "";
        hdnTagtypeId.Value = "1";
        lblOriginalTxt.Text = Convert.ToString(ViewState["LoginName"]);
        String MainDesc = hdndivvalue.Value.ToString();
        objDORatio.ContentTypeID = Convert.ToInt64(ViewState["CTypeID"]);
        objDORatio.CaseId = CaseID;
        objDORatio.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (BrowserUsing == "IE")
        {
            String DivHtml = hdnMarkText.Value.Replace("\r\n", "<br>");
            String WidBr = DivHtml.Replace("<br>", "#&#&#").Replace("<em>", " #&em&# ").Replace("</em>", " #&&#em&# ").Replace("<strong>", " #&strong&# ").Replace("</strong>", " #&&#strong&# ");
            String result = Regex.Replace(WidBr, @"<[^>]*>", String.Empty);
            String FinalText = result.Replace("#&#&#", "<br />").Replace(" #&strong&# ", "<strong>").Replace(" #&&#strong&# ", "</strong>").Replace(" #&em&# ", "<em>").Replace(" #&&#em&# ", "</em>");
            objDORatio.strTagDescription = FinalText;
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        else
        {
            objDORatio.strTagDescription = hdnMarkText.Value.Replace("\r\n", "<br>");
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        DataTable dt = new DataTable();
        if (Convert.ToInt32(hdnStartIdx.Value) >= 0 && Convert.ToInt32(hdnEndIdx.Value) >= 0)
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDORatio.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objDORatio.intTagType = Convert.ToInt32(1);
            objDORatio.StartIndex = Convert.ToInt32(hdnStartIdx.Value);
            objDORatio.EndIndex = Convert.ToInt32(hdnEndIdx.Value);
            dt = objDARatio.GetMarkDataTable(objDORatio, DA_Ratio.Ratio.Add);
            UpdateRecentActivities();
        }

        if (dt.Rows.Count > 0)
        {
            //Add API Log           
            string microTagId = Convert.ToString(dt.Rows[0]["intMarkId"]);
            string docUid = Convert.ToString(objDORatio.CaseId);
            string addByUid = Convert.ToString(ViewState["UserID"]);
            string insertDt = null;
            string startPos = Convert.ToString(objDORatio.StartIndex);
            string endPos = Convert.ToString(objDORatio.EndIndex);
            string microTagType = "1";

            if (ISAPIURLACCESSED == "1")
            {
                string UserURL = APIURL + "createMicroTag.action?microTagId=MT"
                    + microTagId
                    + "&docUid=" + docUid
                    + "&addByUid=" + addByUid
                    + "&insertDt=" + insertDt
                    + "&startPos=" + startPos
                    + "&endPos=" + endPos
                    + "&content=" + objDORatio.strTagDescription
                    + "&microTagType=" + microTagType
                    + "&scope=null&parentUid=null&copyUserId=null";
                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strURL = UserURL;
                        objAPILogDO.strAPIType = "Fact";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }

        hdndivvalue.Value = MainDesc.Replace("</FONT>", "</span>").Replace("</font>", "</span>").Replace("<font ", "<span "); ;
        BindList(CaseID);
        hdnPasteCode.Value = "";
        ReleventUser();
        DivCommentContent.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, GetType(), "myTags", "onlinkclick();", true);

    }

    protected void lnkImportantPara_Click(object sender, EventArgs e)
    {
        hdnComments.Value = "comment";
        DivCommentContent.Style.Add("display", "none");
        hdnTabId.Value = "";
        hdnTagtypeId.Value = "3";
        lblOriginalTxt.Text = Convert.ToString(ViewState["LoginName"]);
        String MainDesc = hdndivvalue.Value.ToString();
        objDORatio.ContentTypeID = Convert.ToInt64(ViewState["CTypeID"]);
        objDORatio.CaseId = CaseID;
        objDORatio.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (BrowserUsing == "IE")
        {
            String DivHtml = hdnMarkText.Value.Replace("\r\n", "<br>");
            String WidBr = DivHtml.Replace("<br>", "#&#&#").Replace("<em>", " #&em&# ").Replace("</em>", " #&&#em&# ").Replace("<strong>", " #&strong&# ").Replace("</strong>", " #&&#strong&# ");
            String result = Regex.Replace(WidBr, @"<[^>]*>", String.Empty);

            String FinalText = result.Replace("#&#&#", "<br />").Replace(" #&strong&# ", "<strong>").Replace(" #&&#strong&# ", "</strong>").Replace(" #&em&# ", "<em>").Replace(" #&&#em&# ", "</em>");
            objDORatio.strTagDescription = FinalText;
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        else
        {
            objDORatio.strTagDescription = hdnMarkText.Value.Replace("\r\n", "<br>");
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        DataTable dt = new DataTable();
        if (Convert.ToInt32(hdnStartIdx.Value) >= 0 && Convert.ToInt32(hdnEndIdx.Value) >= 0)
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDORatio.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objDORatio.intTagType = Convert.ToInt32(3);
            objDORatio.StartIndex = Convert.ToInt32(hdnStartIdx.Value);
            objDORatio.EndIndex = Convert.ToInt32(hdnEndIdx.Value);
            dt = objDARatio.GetMarkDataTable(objDORatio, DA_Ratio.Ratio.Add);
            UpdateRecentActivities();
        }

        if (dt.Rows.Count > 0)
        {
            //Add API Log           
            string microTagId = Convert.ToString(dt.Rows[0]["intMarkId"]);
            string docUid = Convert.ToString(objDORatio.CaseId);
            string addByUid = Convert.ToString(ViewState["UserID"]);
            string insertDt = null;
            string startPos = Convert.ToString(objDORatio.StartIndex);
            string endPos = Convert.ToString(objDORatio.EndIndex);
            string microTagType = "3";

            if (ISAPIURLACCESSED == "1")
            {
                string UserURL = APIURL + "createMicroTag.action?microTagId=MT" + microTagId
                    + "&docUid=" + docUid
                    + "&addByUid=" + addByUid
                    + "&insertDt=" + insertDt
                    + "&startPos=" + startPos
                    + "&endPos=" + endPos
                    + "&content=" + objDORatio.strTagDescription
                    + "&microTagType=" + microTagType
                    + "&scope=null&parentUid=null&copyUserId=null";
                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strURL = UserURL;
                        objAPILogDO.strAPIType = "Importan Paragraph";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }

        hdndivvalue.Value = MainDesc.Replace("</FONT>", "</span>").Replace("</font>", "</span>").Replace("<font ", "<span ");
        BindList(CaseID);
        hdnPasteCode.Value = "";
        ReleventUser();
        ScriptManager.RegisterStartupScript(this, GetType(), "myTags", "onlinkclick();", true);
    }

    protected void lnkPrecedent_Click(object sender, EventArgs e)
    {
        DivCommentContent.Style.Add("display", "none");
        hdnComments.Value = "comment";
        hdnTabId.Value = "";
        hdnTagtypeId.Value = "4";
        lblOriginalTxt.Text = Convert.ToString(ViewState["LoginName"]);
        String MainDesc = hdndivvalue.Value.ToString();
        objDORatio.ContentTypeID = Convert.ToInt64(ViewState["CTypeID"]);
        objDORatio.CaseId = CaseID;
        objDORatio.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (BrowserUsing == "IE")
        {
            String DivHtml = hdnMarkText.Value.Replace("\r\n", "<br>");
            String WidBr = DivHtml.Replace("<br>", "#&#&#").Replace("<em>", " #&em&# ").Replace("</em>", " #&&#em&# ").Replace("<strong>", " #&strong&# ").Replace("</strong>", " #&&#strong&# ");
            String result = Regex.Replace(WidBr, @"<[^>]*>", String.Empty);
            String FinalText = result.Replace("#&#&#", "<br />").Replace(" #&strong&# ", "<strong>").Replace(" #&&#strong&# ", "</strong>").Replace(" #&em&# ", "<em>").Replace(" #&&#em&# ", "</em>");
            objDORatio.strTagDescription = FinalText;
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        else
        {
            objDORatio.strTagDescription = hdnMarkText.Value.Replace("\r\n", "<br>");
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        DataTable dt = new DataTable();
        if (Convert.ToInt32(hdnStartIdx.Value) >= 0 && Convert.ToInt32(hdnEndIdx.Value) >= 0)
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDORatio.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objDORatio.intTagType = Convert.ToInt32(4);
            objDORatio.strNewRatioTitle = Convert.ToString(ViewState["strRatioTitle"]);
            objDORatio.StartIndex = Convert.ToInt32(hdnStartIdx.Value);
            objDORatio.EndIndex = Convert.ToInt32(hdnEndIdx.Value);
            dt = objDARatio.GetMarkDataTable(objDORatio, DA_Ratio.Ratio.Add);
            UpdateRecentActivities();

        }

        if (dt.Rows.Count > 0)
        {
            //Add API Log           
            string microTagId = Convert.ToString(dt.Rows[0]["intMarkId"]);
            string docUid = Convert.ToString(objDORatio.CaseId);
            string addByUid = Convert.ToString(ViewState["UserID"]);
            string insertDt = null;
            string startPos = Convert.ToString(objDORatio.StartIndex);
            string endPos = Convert.ToString(objDORatio.EndIndex);
            string microTagType = "4";

            if (ISAPIURLACCESSED == "1")
            {
                string UserURL = APIURL + "createMicroTag.action?microTagId=MT" + microTagId
                    + "&docUid=" + docUid
                    + "&addByUid=" + addByUid
                    + "&insertDt=" + insertDt
                    + "&startPos=" + startPos
                    + "&endPos=" + endPos
                    + "&content=" + objDORatio.strTagDescription
                    + "&microTagType=" + microTagType
                    + "&scope=null&parentUid=null&copyUserId=null";
                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strURL = UserURL;
                        objAPILogDO.strAPIType = "Precedent";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }

        hdndivvalue.Value = MainDesc.Replace("</FONT>", "</span>").Replace("</font>", "</span>").Replace("<font ", "<span "); ;
        BindList(CaseID);
        ReleventUser();
        hdnPasteCode.Value = "";
        ScriptManager.RegisterStartupScript(this, GetType(), "myTags", "onlinkclick();", true);
    }

    protected void lnkDecidendit_Click(object sender, EventArgs e)
    {
        DivCommentContent.Style.Add("display", "none");
        hdnComments.Value = "comment";
        hdnTabId.Value = "";
        hdnTagtypeId.Value = "5";
        lblOriginalTxt.Text = Convert.ToString(ViewState["LoginName"]);
        String MainDesc = hdndivvalue.Value.ToString();
        objDORatio.ContentTypeID = Convert.ToInt64(ViewState["CTypeID"]);
        objDORatio.CaseId = CaseID;
        objDORatio.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (BrowserUsing == "IE")
        {
            String DivHtml = hdnMarkText.Value.Replace("\r\n", "<br>");
            String WidBr = DivHtml.Replace("<br>", "#&#&#").Replace("<em>", " #&em&# ").Replace("</em>", " #&&#em&# ").Replace("<strong>", " #&strong&# ").Replace("</strong>", " #&&#strong&# ");
            String result = Regex.Replace(WidBr, @"<[^>]*>", String.Empty);

            String FinalText = result.Replace("#&#&#", "<br />").Replace(" #&strong&# ", "<strong>").Replace(" #&&#strong&# ", "</strong>").Replace(" #&em&# ", "<em>").Replace(" #&&#em&# ", "</em>");
            objDORatio.strTagDescription = FinalText;
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        else
        {
            objDORatio.strTagDescription = hdnMarkText.Value.Replace("\r\n", "<br>");
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        DataTable dt = new DataTable();
        if (Convert.ToInt32(hdnStartIdx.Value) >= 0 && Convert.ToInt32(hdnEndIdx.Value) >= 0)
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDORatio.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objDORatio.intTagType = Convert.ToInt32(5);
            objDORatio.StartIndex = Convert.ToInt32(hdnStartIdx.Value);
            objDORatio.EndIndex = Convert.ToInt32(hdnEndIdx.Value);
            dt = objDARatio.GetMarkDataTable(objDORatio, DA_Ratio.Ratio.Add);
            UpdateRecentActivities();

        }

        if (dt.Rows.Count > 0)
        {
            //Add API Log           
            string microTagId = Convert.ToString(dt.Rows[0]["intMarkId"]);
            string docUid = Convert.ToString(objDORatio.CaseId);
            string addByUid = Convert.ToString(ViewState["UserID"]);
            string insertDt = null;
            string startPos = Convert.ToString(objDORatio.StartIndex);
            string endPos = Convert.ToString(objDORatio.EndIndex);
            string microTagType = "5";
            if (ISAPIURLACCESSED == "1")
            {
                string UserURL = APIURL + "createMicroTag.action?microTagId=MT" + microTagId
                    + "&docUid=" + docUid
                    + "&addByUid=" + addByUid
                    + "&insertDt=" + insertDt
                    + "&startPos=" + startPos
                    + "&endPos=" + endPos
                    + "&content=" + objDORatio.strTagDescription
                    + "&microTagType=" + microTagType
                    + "&scope=null&parentUid=null&copyUserId=null";
                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strURL = UserURL;
                        objAPILogDO.strAPIType = "Radio Decidendi";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }

        hdndivvalue.Value = MainDesc.Replace("</FONT>", "</span>").Replace("</font>", "</span>").Replace("<font ", "<span ");
        BindList(CaseID);
        ReleventUser();
        hdnPasteCode.Value = "";
        ScriptManager.RegisterStartupScript(this, GetType(), "myTags", "onlinkclick();", true);
    }

    protected void lnkOrbite_Click(object sender, EventArgs e)
    {
        DivCommentContent.Style.Add("display", "none");
        hdnComments.Value = "comment";
        hdnTabId.Value = "";
        hdnTagtypeId.Value = "6";
        lblOriginalTxt.Text = Convert.ToString(ViewState["LoginName"]);
        String MainDesc = hdndivvalue.Value.ToString();
        objDORatio.ContentTypeID = Convert.ToInt64(ViewState["CTypeID"]);
        objDORatio.CaseId = CaseID;
        objDORatio.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (BrowserUsing == "IE")
        {
            String DivHtml = hdnMarkText.Value.Replace("\r\n", "<br>");
            String WidBr = DivHtml.Replace("<br>", "#&#&#").Replace("<em>", " #&em&# ").Replace("</em>", " #&&#em&# ").Replace("<strong>", " #&strong&# ").Replace("</strong>", " #&&#strong&# ");
            String result = Regex.Replace(WidBr, @"<[^>]*>", String.Empty);
            String FinalText = result.Replace("#&#&#", "<br />").Replace(" #&strong&# ", "<strong>").Replace(" #&&#strong&# ", "</strong>").Replace(" #&em&# ", "<em>").Replace(" #&&#em&# ", "</em>");
            objDORatio.strTagDescription = FinalText;
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        else
        {
            objDORatio.strTagDescription = hdnMarkText.Value.Replace("\r\n", "<br>");
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        DataTable dt = new DataTable();
        if (Convert.ToInt32(hdnStartIdx.Value) >= 0 && Convert.ToInt32(hdnEndIdx.Value) >= 0)
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDORatio.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objDORatio.intTagType = Convert.ToInt32(6);
            objDORatio.strNewRatioTitle = Convert.ToString(ViewState["strRatioTitle"]);
            objDORatio.StartIndex = Convert.ToInt32(hdnStartIdx.Value);
            objDORatio.EndIndex = Convert.ToInt32(hdnEndIdx.Value);
            dt = objDARatio.GetMarkDataTable(objDORatio, DA_Ratio.Ratio.Add);
            UpdateRecentActivities();
        }

        if (dt.Rows.Count > 0)
        {
            string microTagId = Convert.ToString(dt.Rows[0]["intMarkId"]);
            string docUid = Convert.ToString(objDORatio.CaseId);
            string addByUid = Convert.ToString(ViewState["UserID"]);
            string insertDt = null;
            string startPos = Convert.ToString(objDORatio.StartIndex);
            string endPos = Convert.ToString(objDORatio.EndIndex);
            string microTagType = "6";

            if (ISAPIURLACCESSED == "1")
            {
                string UserURL = APIURL + "createMicroTag.action?microTagId=MT" + microTagId
                    + "&docUid=" + docUid
                    + "&addByUid=" + addByUid
                    + "&insertDt=" + insertDt
                    + "&startPos=" + startPos
                    + "&endPos=" + endPos
                    + "&content=" + objDORatio.strTagDescription
                    + "&microTagType=" + microTagType
                    + "&scope=null&parentUid=null&copyUserId=null";
                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strURL = UserURL;
                        objAPILogDO.strAPIType = "Orbiter Dictum";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }
        hdndivvalue.Value = MainDesc.Replace("</FONT>", "</span>").Replace("</font>", "</span>").Replace("<font ", "<span ");
        BindList(CaseID);
        ReleventUser();
        hdnPasteCode.Value = "";
        ScriptManager.RegisterStartupScript(this, GetType(), "myTags", "onlinkclick();", true);
    }

    protected void lnkHighlight_Click(object sender, EventArgs e)
    {
        DivCommentContent.Style.Add("display", "none");
        hdnComments.Value = "comment";
        hdnTabId.Value = "";
        hdnTagtypeId.Value = "7";
        lblOriginalTxt.Text = Convert.ToString(ViewState["LoginName"]);
        String MainDesc = hdndivvalue.Value.ToString();
        objDORatio.ContentTypeID = Convert.ToInt64(ViewState["CTypeID"]);
        objDORatio.CaseId = CaseID;
        objDORatio.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (BrowserUsing == "IE")
        {
            String DivHtml = hdnMarkText.Value.Replace("\r\n", "<br>");
            String WidBr = DivHtml.Replace("<br>", "#&#&#").Replace("<em>", " #&em&# ").Replace("</em>", " #&&#em&# ").Replace("<strong>", " #&strong&# ").Replace("</strong>", " #&&#strong&# ");
            String result = Regex.Replace(WidBr, @"<[^>]*>", String.Empty);
            String FinalText = result.Replace("#&#&#", "<br />").Replace(" #&strong&# ", "<strong>").Replace(" #&&#strong&# ", "</strong>").Replace(" #&em&# ", "<em>").Replace(" #&&#em&# ", "</em>");
            objDORatio.strTagDescription = FinalText;
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        else
        {
            objDORatio.strTagDescription = hdnMarkText.Value.Replace("\r\n", "<br>");
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        DataTable dt = new DataTable();
        if (Convert.ToInt32(hdnStartIdx.Value) >= 0 && Convert.ToInt32(hdnEndIdx.Value) >= 0)
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDORatio.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objDORatio.intTagType = Convert.ToInt32(7);
            objDORatio.strNewRatioTitle = Convert.ToString(ViewState["strRatioTitle"]);
            objDORatio.StartIndex = Convert.ToInt32(hdnStartIdx.Value);
            objDORatio.EndIndex = Convert.ToInt32(hdnEndIdx.Value);
            dt = objDARatio.GetMarkDataTable(objDORatio, DA_Ratio.Ratio.Add);
            UpdateRecentActivities();

        }

        if (dt.Rows.Count > 0)
        {
            //Add API Log           
            string microTagId = Convert.ToString(dt.Rows[0]["intMarkId"]);
            string docUid = Convert.ToString(objDORatio.CaseId);
            string addByUid = Convert.ToString(ViewState["UserID"]);
            string insertDt = null;
            string startPos = Convert.ToString(objDORatio.StartIndex);
            string endPos = Convert.ToString(objDORatio.EndIndex);
            string microTagType = "7";

            if (ISAPIURLACCESSED == "1")
            {
                string UserURL = APIURL + "createMicroTag.action?microTagId=MT" + microTagId
                    + "&docUid=" + docUid
                    + "&addByUid=" + addByUid
                    + "&insertDt=" + insertDt
                    + "&startPos=" + startPos
                    + "&endPos=" + endPos
                    + "&content=" + objDORatio.strTagDescription
                    + "&microTagType=" + microTagType
                    + "&scope=null&parentUid=null&copyUserId=null";
                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strURL = UserURL;
                        objAPILogDO.strAPIType = "Highlight";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }

        hdndivvalue.Value = MainDesc.Replace("Apple-style-span", "h3").Replace("</FONT>", "</span>").Replace("</font>", "</span>").Replace("<font ", "<span ");
        BindList(CaseID);
        ReleventUser();
        ScriptManager.RegisterStartupScript(this, GetType(), "myTags", "onlinkclick();", true);
    }

    #endregion

    protected void lnkFvrtImage_Click(object sender, EventArgs e)
    {
        objDoCaseList.intDocId = Convert.ToInt32(ViewState["ContentID"]);
        objDoCaseList.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        objDoCaseList.strIpAddress = ip;
        objDoCaseList.intAddedBy = Convert.ToInt32(ViewState["UserID"]);

        objCaseListDb.AddEditDel_MicroTagLikeShare(objDoCaseList, DA_CaseList.MicroTagLikeShare.InsertDeleteFavrt);
        if (objDoCaseList.intMicroLikeShareId == 0)
        {
            imgFvrt.Src = "~/images/gray-tag.png";
        }
        else
        {
            imgFvrt.Src = "~/images/red-tag.png";
            if (ISAPIURLACCESSED == "1")
            {
                try
                {
                    String url = APIURL + "markBookMark.action?" +
                                    "userId=" + UserTypeId + Convert.ToInt32(ViewState["UserID"]) +
                                    "&bookmarkOnUid=" + objDoCaseList.intMicroLikeShareId;

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();
                    objAPILogDO.strURL = url;
                    objAPILogDO.strAPIType = "Book mark";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDO.strIPAddress = ip;
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
                catch
                {
                }
            }
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ShowSummaryPostLoad(1, 'kiran')", true);
    }

    #region document Title

    protected void lnkPopupShare_Click(object sender, EventArgs e)
    {
        if (hdnInvId.Value != null && hdnInvId.Value != "")
        {
            objDoCaseList.intDocId = CaseID;
            objDoCaseList.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objDoCaseList.strIpAddress = ip;
            objDoCaseList.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objDoCaseList.strRepLiShStatus = "SH";
            string id = hdnInvId.Value;
            objDoCaseList.strInviteeShare = hdnInvId.Value;
            if (txtBody.InnerText.Trim().Replace("'", "''") == "Message")
            {
                objDoCaseList.strMessage = "";
            }
            else
            {
                objDoCaseList.strMessage = txtBody.InnerText.Trim().Replace("'", "''");
            }
            objDoCaseList.strLink = txtLink.Text.Trim().Replace("'", "''");
            objCaseListDb.AddEditDel_MicroTagLikeShare(objDoCaseList, DA_CaseList.MicroTagLikeShare.MicrotagShare);
            PopUpShare.Style.Add("display", "none");
            try
            {
                if (ISAPIURLACCESSED == "1")
                {
                    String UserURL = APIURL + "shareDocument.action?" +
                 "shareByUid=" + ViewState["UserID"] +
                 "&shareToUid=" + hdnInvId.Value +
                 "&shareObjUid=" + ViewState["ContentID"];
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
                            objAPILogDO.strAPIType = "Share Document";
                            objAPILogDO.strResponse = result;
                            if (ip == null)
                                objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                            objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                        }
                    }
                    catch { }
                }
            }
            catch
            {
            }
            txtInviteMembers.Controls.Clear();
            getInviteeName();
            lblMess.Text = "";
            txtBody.InnerText = "";
            txtLink.Text = "";
        }
        else
        {
            lblMess.Text = "Please select member";
            PopUpShare.Style.Add("display", "block");
            return;
        }
    }

    protected void getInviteeName()
    {
        DataTable dtDoc = new DataTable();
        objdonetwork.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
        dtDoc = objdanetwork.GetUserConnections(objdonetwork, DA_Networks.NetworkDetails.ConnectedUsers);

        if (dtDoc.Rows.Count > 0)
        {
            txtInviteMembers.DataSource = dtDoc;
            txtInviteMembers.DataValueField = "intInvitedUserId";
            txtInviteMembers.DataTextField = "NAME";
            txtInviteMembers.DataBind();
        }

    }
    #endregion

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        String UserURL = APIURL + "downloadDocument.action?" +
                "downloadByUId=" + ViewState["UserID"].ToString() +
                "&downloadedParentId=" + ViewState["ContentID"] +
                "&downloadType=" + "free";

        Session["divDisps"] = hdnDivContent.Value;
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Myss", "callDivHandler();", true);
        try
        {
            if (ISAPIURLACCESSED == "1")
            {
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
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strAPIType = "Download Document";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }
        catch
        {
        }

        //string texta = hdnDivContent.Value;
        StringBuilder text = new StringBuilder(hdnDivContent.Value);
        text = text.Replace("&quot;", "").Replace("<br>", "<br />").Replace("<br >", "<br />");
        int countCdiv = CountStringOccurrences(text.ToString(), "</div>");
        int countdiv = CountStringOccurrences(text.ToString(), "<div");
        if (countCdiv == 0 && countdiv == 0)
        {
            int txtL = text.Length;
            text = text.Insert(txtL, "</div>");
            text = text.Insert(0, "<div>");
        }

        if (Convert.ToInt32(hdnMarkMaxCount.Value) > 0)
        {
            for (int i = 0; i <= Convert.ToInt32(hdnMarkMaxCount.Value); i++)
            {
                string spanOpen = "<span" + i;
                string replace = "<span";
                string pattern = spanOpen;
                string result = Regex.Replace(text.ToString(), pattern, replace, RegexOptions.IgnoreCase);
                string spanClose = "</span" + i;
                replace = "</span";
                pattern = spanClose;
                result = Regex.Replace(result.ToString(), pattern, replace, RegexOptions.IgnoreCase);
                text = new StringBuilder(result);

            }
            text = text.Replace("class=\"preced\"", "style=\"background-color: Yellow;\"").Replace("class=\"rediod\"", "style=\"background-color: #AECF00;\"").Replace("class=\"issuss\"", "style=\"background-color: Orange;\"").Replace("span0", "span").Replace("span1", "span").Replace("span2", "span").Replace("span3", "span").Replace("span4", "span").Replace("span5", "span");
            text = text.Replace("class=\"factss\"", "style=\"background-color: Aqua;\"").Replace("class=\"highls\"", "style=\"background-color: Lime;\"").Replace("class=\"orbits\"", "style=\"background-color: Green;\"").Replace("class=\"ImpPss\"", "style=\"background-color: Fuchsia;\"");
        }

        createPDF(text.ToString());

    }

    private byte[] createPDF(string html)
    {

        using (var msOutput = new MemoryStream())
        {
            using (var document = new Document(PageSize.A4, 30, 30, 30, 30))
            {
                try
                {
                    using (var writer = PdfWriter.GetInstance(document, msOutput))
                    {
                        document.Open();
                        var example_css = @".body{font-family: 'Times New Roman'}";
                        Font contentFont = FontFactory.GetFont("Times New Roman", 16.0f, iTextSharp.text.Font.BOLD);
                        Paragraph para = new Paragraph(lblCaseTitle.Text.Trim(), contentFont);
                        para.Alignment = Element.ALIGN_CENTER;
                        document.Add(para);
                        Paragraph para1 = new Paragraph(" ", contentFont);
                        document.Add(para1);
                        document.HtmlStyleClass = @".IContent{font-family: Times New Roman;text-align: justify;font-size: 22px;}";

                        using (TextReader reader = new StringReader(html))
                        {
                            //Parse the HTML and write it to the document
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, reader);

                            ////get the XMLWorkerHelper Instance
                            //XMLWorkerHelper worker = XMLWorkerHelper.GetInstance();
                            ////convert to PDF
                            //worker.ParseXHtml(writer, document, reader);
                            ////worker.GetDefaultCSS(

                        }
                        document.Close();
                    }
                }
                catch (Exception ex)
                { Response.Write(ex.Message); }
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment; filename=Casedocument_" + ViewState["ContentID"] + ".pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.OutputStream.Write(msOutput.GetBuffer(), 0, msOutput.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
                return msOutput.ToArray();
            }
        }
    }

    protected void lnkShare_Click(object sender, EventArgs e)
    {
        if (lblOriginalTxt.Text == "Original Text" || lblOriginalTxt.Text == "")
            lblOriginalTxt.Text = "Original Text";

        string path = Request.Url.AbsoluteUri;
        txtLink.Text = path;
        lblMess.Text = "";
        hdnTabId.Value = "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "callchosencss();", true);
        imgGrp1.Src = "~/images/photo1.png";
        string DocTitle = Convert.ToString(ViewState["hdnDocTitle"]);
        lblDocTitle.Text = Convert.ToString(ViewState["hdnDocTitle"]);
        PopUpShare.Style.Add("display", "block");

    }
}