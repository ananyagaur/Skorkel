using System;
using System.Data;
using DA_SKORKEL;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.UI;
using System.Threading;

public partial class post_new_question : System.Web.UI.Page
{
    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DA_Scrl_UserQAPosting objDAQAPosting = new DA_Scrl_UserQAPosting();
    DO_Scrl_UserQAPosting objDOQAPosting = new DO_Scrl_UserQAPosting();

    DataTable dt = new DataTable();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["SubmitTime"] = Session["SubmitTime"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Session["SubmitTime"] = DateTime.Now.ToString();
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Q&A";

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());
            SubjectTempDataTable();
            BindSubjectList();
            GetRelatedQuestion();
           // HideShowSubject();
            if (!string.IsNullOrEmpty(Request.QueryString["PostQAId"]))
            {
                ViewState["PostQAId"] = Request.QueryString["PostQAId"];
                LoadEditQA();
                BindEditSubjectList();
                BindSubjectLists();
            }

        }
    }

    protected void LoadEditQA()
    {
        DataTable dtSub = new DataTable();
        objDOQAPosting.intPostQuestionId = Convert.ToInt32(ViewState["PostQAId"]);
        dtSub = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetQA);
        if (dtSub.Rows.Count > 0)
        {
            CKDescription.InnerText = Convert.ToString(dtSub.Rows[0]["strQuestionDescription"]);
        }
    }

    protected void BindEditSubjectList()
    {
        DataTable dtSub = new DataTable();
        DataSet ds = new DataSet();
        objDOQAPosting.intPostQuestionId = Convert.ToInt32(ViewState["PostQAId"]);
        dtSub = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetSubjectListQA);
        ViewState["SaveSubCatname"] = dtSub;
    }

    protected void BindSubjectLists()
    {
        string[] body;
        DataTable dtSub = new DataTable();
        objDOQAPosting.intPostQuestionId = Convert.ToInt32(ViewState["PostQAId"]);
        dtSub = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetAllSubjectListQA);
        if (dtSub.Rows.Count > 0)
        {
            if (dtSub.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSub.Rows)
                {
                    hdnsubject.Value = hdnsubject.Value + dr["intCategoryId"].ToString() + ",";
                }
                body = hdnsubject.Value.Split(',');
                for (int i = 0; i < body.Length; i++)
                {
                    string selectedvalue = body[i];
                    if (selectedvalue != "")
                    {
                        foreach (ListItem item in txtSubjectList.Items)
                        {
                            if (item.Value == selectedvalue)
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }

            }
           
        }
    }

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

    private void BindSubjectList()
    {
        DataTable dtSub = new DataTable();

        dtSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.AllRecord);
        if (dtSub.Rows.Count > 0)
        {
            txtSubjectList.DataSource = dtSub;
            txtSubjectList.DataValueField = "intCategoryId";
            txtSubjectList.DataTextField = "strCategoryName";
            txtSubjectList.DataBind();
        }
    }

    protected void LstSubjCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnSubCatId = (HiddenField)e.Item.FindControl("hdnSubCatId");
        LinkButton lnkCatName = (LinkButton)e.Item.FindControl("lnkCatName");
        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
        DataTable dtsub = new DataTable();
        HiddenField hdnCountSub = (HiddenField)e.Item.FindControl("hdnCountSub");
        if (hdnCountSub.Value == "1")
        {
            SubLi.Style.Add("background", "none repeat scroll 0 0 #00B7BD !important");
            SubLi.Style.Add("color", "#FFFFFF !important");
            SubLi.Style.Add("text-decoration", "none !important");
            lnkCatName.ForeColor = System.Drawing.Color.White;
            lnkCatName.Style.Add("color", "#FFFFFF !important");
            //SubLi.Attributes.Add("class", "categoryBox");
        }
        else
        {
            SubLi.Style.Add("background", "#ebeaea");
            SubLi.Style.Add("border", "1px solid #c8c8c8");
            SubLi.Style.Add("color", "#646161");
            //SubLi.Attributes.Add("class", "gray");
        }

        dtsub = (DataTable)ViewState["SaveSubCatname"];
        ViewState["SubjectCategory"] = dtsub;

    }

    protected void LstSubjCategory_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "Subject Category")
        {
            HiddenField hdnSubCatId = (HiddenField)e.Item.FindControl("hdnSubCatId");
            LinkButton lnkCatName = (LinkButton)e.Item.FindControl("lnkCatName");
            HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");

            string Subjectid = "0", SubjectCat = string.Empty;
            DataTable dtSubjCat = new DataTable();
            DataTable dtsub = new DataTable();

            dtSubjCat = (DataTable)ViewState["SubjectCategory"];
            DataRow rwSubj = dtSubjCat.NewRow();

            if (ViewState["SubjectCategory"] != null)
            {
                DataTable dtContent = (DataTable)ViewState["SubjectCategory"];
                if (dtContent.Rows.Count > 0)
                {
                    if (lnkCatName.Text.Contains("'"))
                        lnkCatName.Text = lnkCatName.Text.Replace("'", "''");
                    for (int i = 0; i < dtContent.Rows.Count; i++)
                    {
                        string AutoSugestName = Convert.ToString(dtContent.Rows[i]["strCategoryName"]);
                        bool contains = lnkCatName.Text.IndexOf(AutoSugestName, StringComparison.OrdinalIgnoreCase) >= 0;
                        if (contains == true)
                        {
                            dtSubjCat = (DataTable)ViewState["SubjectCategory"];
                            //to check whethere the column is newly added 
                            if (Convert.ToInt32(ViewState["SearchMId"]) > 0)
                                dtSubjCat.Rows.Remove(dtContent.Rows[i]);
                            ViewState["SubjectCategory"] = dtSubjCat;
                            // SubLi.Attributes.Add("class", "gray");
                            SubLi.Style.Add("background", "#ebeaea");
                            SubLi.Style.Add("border", "1px solid #c8c8c8");
                            SubLi.Style.Add("color", "#646161");
                            lnkCatName.Style.Add("color", "#646161");
                            return;
                        }
                    }
                    if (dtSubjCat.Rows.Count <= 0)
                    {
                        ViewState["SearchMId"] = hdnSubCatId.Value;
                        rwSubj["intCategoryId"] = hdnSubCatId.Value;
                        rwSubj["strCategoryName"] = lnkCatName.Text.Trim();
                        dtSubjCat.Rows.Add(rwSubj);
                        ViewState["SubjectCategory"] = dtSubjCat;
                    }
                    else
                    {
                        Subjectid = hdnSubCatId.Value.ToString();
                        ViewState["SearchMId"] = Subjectid;
                        rwSubj["intCategoryId"] = Subjectid;
                        rwSubj["strCategoryName"] = lnkCatName.Text.Trim();

                        for (int i = 0; i < dtSubjCat.Rows.Count; i++)
                        {
                            if (SubjectCat == dtSubjCat.Rows[i]["strCategoryName"].ToString())
                                return;
                        }
                        dtSubjCat.Rows.Add(rwSubj);
                        dtSubjCat = (DataTable)ViewState["SubjectCategory"];
                        ViewState["SubjectCategory"] = dtSubjCat;
                    }
                }
                else
                {
                    if (dtSubjCat.Rows.Count <= 0)
                    {
                        ViewState["SearchMId"] = hdnSubCatId.Value;
                        rwSubj["intCategoryId"] = hdnSubCatId.Value;
                        rwSubj["strCategoryName"] = lnkCatName.Text.Trim();
                        dtSubjCat.Rows.Add(rwSubj);
                        ViewState["SubjectCategory"] = dtSubjCat;
                    }
                    else
                    {
                        Subjectid = hdnSubCatId.Value.ToString();
                        ViewState["SearchMId"] = Subjectid;
                        rwSubj["intCategoryId"] = Subjectid;
                        rwSubj["strCategoryName"] = lnkCatName.Text.Trim();

                        for (int i = 0; i < dtSubjCat.Rows.Count; i++)
                        {
                            if (SubjectCat == dtSubjCat.Rows[i]["strCategoryName"].ToString())
                                return;
                        }
                        dtSubjCat.Rows.Add(rwSubj);
                        dtSubjCat = (DataTable)ViewState["SubjectCategory"];
                        ViewState["SubjectCategory"] = dtSubjCat;
                    }
                }
            }

            dtsub = (DataTable)ViewState["SubjectCategory"];
            if (dtsub.Rows.Count > 0)
            {
                SubLi.Style.Add("background", "none repeat scroll 0 0 #00B7BD !important");
                SubLi.Style.Add("color", "#FFFFFF !important");
                SubLi.Style.Add("text-decoration", "none !important");
                lnkCatName.ForeColor = System.Drawing.Color.White;
                lnkCatName.Style.Add("color", "#FFFFFF !important");
            }
            else
            {
                SubLi.Style.Add("background", "#ebeaea");
                SubLi.Style.Add("border", "1px solid #c8c8c8");
                SubLi.Style.Add("color", "#646161");
                lnkCatName.Style.Add("color", "#646161");

            }
        }
    }
  
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Thread.Sleep(40000);
        //return;
        string docPath = "";
        if (Session["SubmitTime"].ToString() == ViewState["SubmitTime"].ToString())
        {
            if (CKDescription.InnerText != "Write your question here...")
            {
                if (CKDescription.InnerText.Trim() != "")
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
                            objDOQAPosting.strFilePath = docPath;
                            objDOQAPosting.strFileName = uploadDoc.FileName;
                        }
                        else
                        {
                            lblMessage.Text = "File size should be less than or equal to 3MB";
                            lblMessage.CssClass = "RedErrormsg";
                            return;
                        }
                    }
                    string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ip == null)
                        ip = Request.ServerVariables["REMOTE_ADDR"];
                    objDOQAPosting.strIPAddress = ip;

                    if (ViewState["PostQAId"] == null)
                    {
                        string editor = CKDescription.InnerText.Trim().Replace(",", "''");
                        string noHTML = Regex.Replace(editor, @"<[^>]+>|&nbsp;", "").Trim();
                        objDOQAPosting.strQuestionDescription = Regex.Replace(noHTML, @"\s{2,}", " ");
                        objDOQAPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.Add);
                        ViewState["QueAnsId"] = objDOQAPosting.intPostQuestionId;
                        string totalMembers = hdnsubject.Value;
                        string[] members = totalMembers.Split(',');
                        for (int i = 0; i < members.Length; i++)
                        {
                            if (Convert.ToString(members.GetValue(i)) != "")
                            {
                                int ID = Convert.ToInt32(members.GetValue(i));
                                objDOQAPosting.intPostQuestionId = Convert.ToInt32(ViewState["QueAnsId"]);
                                objDOQAPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                                objDOQAPosting.intSubjectCategoryId = ID;
                                objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.ADDSelectedSubCatId);
                            }
                        }

                        if (ISAPIURLACCESSED != "0")
                        {
                            try
                            {
                                String url = APIURL + "addQuestion.action?" +
                                            "questionId=" + objDOQAPosting.intPostQuestionId +
                                            "&userId=" + objDOQAPosting.intAddedBy +
                                            "&userName=" + null +
                                            "&insertDt=" + DateTime.Now +
                                            "&content=" + objDOQAPosting.strQuestionDescription +
                                            "&title=" + objDOQAPosting.strQuestionDescription;

                                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                                myRequest1.Method = "GET";
                                WebResponse myResponse1 = myRequest1.GetResponse();
                                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                                String result = sr.ReadToEnd();
                                objAPILogDO.strURL = url;
                                objAPILogDO.strAPIType = "Post Question";
                                objAPILogDO.strResponse = result;
                                objAPILogDO.strIPAddress = ip;
                                objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                            }
                            catch { }
                        }
                        hdnsubject.Value = "";
                        CKDescription.InnerText = "";
                        lblSuMess.Text = "";
                        GetRelatedQuestion();
                        Session["SubmitTime"] = DateTime.Now.ToString();
                        lblSuccess.Text = "Question Created successfully.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallPostimg1", "CallPostimgs();", true);
                        divSuccess.Style.Add("display", "block");
                    }
                    else
                    {
                        string editor = CKDescription.InnerText.Trim().Replace(",", "''");
                        string noHTML = Regex.Replace(editor, @"<[^>]+>|&nbsp;", "").Trim();
                        objDOQAPosting.strQuestionDescription = Regex.Replace(noHTML, @"\s{2,}", " ");
                        objDOQAPosting.intPostQuestionId = Convert.ToInt32(ViewState["PostQAId"]);
                        objDOQAPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.Update);

                        if (!string.IsNullOrEmpty(Convert.ToString(ViewState["SubjectCategory"])))
                        {
                            objDOQAPosting.intPostQuestionId = Convert.ToInt32(ViewState["PostQAId"]);
                            objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.DeleteSelectedSubCatId);
                            string totalMembers = hdnsubject.Value;
                            string[] members = totalMembers.Split(',');
                            for (int i = 0; i < members.Length; i++)
                            {
                                if (Convert.ToString(members.GetValue(i)) != "")
                                {
                                    int ID = Convert.ToInt32(members.GetValue(i));
                                    objDOQAPosting.intPostQuestionId = Convert.ToInt32(ViewState["PostQAId"]);
                                    objDOQAPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                                    objDOQAPosting.intSubjectCategoryId = ID;
                                    objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.ADDSelectedSubCatId);
                                }
                            }
                        }

                        if (ISAPIURLACCESSED != "0")
                        {
                            try
                            {
                                String url = APIURL + "addQuestion.action?" +
                                            "questionId=" + objDOQAPosting.intPostQuestionId +
                                            "&userId=" + objDOQAPosting.intAddedBy +
                                            "&userName=" + null +
                                            "&insertDt=" + DateTime.Now +
                                            "&content=" + objDOQAPosting.strQuestionDescription +
                                            "&title=" + objDOQAPosting.strQuestionDescription;

                                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                                myRequest1.Method = "GET";
                                WebResponse myResponse1 = myRequest1.GetResponse();
                                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                                String result = sr.ReadToEnd();
                                objAPILogDO.strURL = url;
                                objAPILogDO.strAPIType = "Post Question";
                                objAPILogDO.strResponse = result;
                                objAPILogDO.strIPAddress = ip;
                                objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                            }
                            catch { }
                        }

                        hdnsubject.Value = "";
                        CKDescription.InnerText = "";
                        lblSuMess.Text = "";
                        hdnsubject.Value = "";
                        GetRelatedQuestion();
                        SubjectTempDataTable();
                        Session["SubmitTime"] = DateTime.Now.ToString();
                        lblSuccess.Text = "Question Updated Successfully.";
                        divSuccess.Style.Add("display", "block");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallPostimg2", "CallPostimgs();", true);
                    }
                    ViewState["PostQAId"] = null;
                }
                else
                {
                    lblSuMess.Text = "Please enter question";
                    lblSuMess.ForeColor = System.Drawing.Color.Red;
                    divSuccess.Style.Add("display", "none");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CallPostimg3", "CallPostimgs();", true);
                }
            }
            else
            {
               
                lblSuMess.Text = "Please enter question";
                lblSuMess.ForeColor = System.Drawing.Color.Red;
                divSuccess.Style.Add("display", "none");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallPostimg4", "CallPostimgs();", true);
            }
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

    protected void lstRelQuestions_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPostQuestionId = (HiddenField)e.Item.FindControl("hdnPostQuestionId");
        if (e.CommandName == "OpenQ")
        {
            Response.Redirect("Question-Details-SendContact.aspx?PostQAId=" + hdnPostQuestionId.Value);
        }

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        divSuccess.Style.Add("display", "none");
        hdnsubject.Value = "";
        CKDescription.InnerText = "";
        lblSuMess.Text = "";
        hdnsubject.Value = "";
        txtSubjectList.Controls.Clear();
    }

}