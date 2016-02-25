using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DA_SKORKEL;
using System.Data;
using System.Web.UI.HtmlControls;


public partial class AllQuestionDetails : System.Web.UI.Page
{
    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DA_Scrl_UserQAPosting objDAQAPosting = new DA_Scrl_UserQAPosting();
    DO_Scrl_UserQAPosting objDOQAPosting = new DO_Scrl_UserQAPosting();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    DataTable dt = new DataTable();
    DataTable dtt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["SubmitTime"] = DateTime.Now.ToString();
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"]);
            }
            else
            {
                Response.Redirect("~/Landing.aspx", true);
            }

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Q&A";

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "10";

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"]);
            SubjectSearchTempDataTable();
            if (!string.IsNullOrEmpty(Request.QueryString["MP"]))
            {
                lnkRecentBlogs.Attributes.Add("class", "activeq");
                BindMostPopular();
            }
            else
            {
                lnkAllBlog.Attributes.Add("class", "activeq");
                BindQADetails();
            }
            BindSearchSubjectList();
            HideShowSubject();
        }

    }

    protected void BindQADetails()
    {
        String ID = string.Empty;
        objDOQAPosting.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objDOQAPosting.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        if (txtSearchQuestion.Text != "")
        {
            objDOQAPosting.strSearch = txtSearchQuestion.Text.Trim().Replace("'", "''");
            if (hdnSubject.Value != "")
            {
                string[] totalSubjects = hdnSubject.Value.Split(',');
                var dictionary = new Dictionary<int, int>();

                if (totalSubjects.Count() > 0)
                {
                    Dictionary<string, int> counts = totalSubjects.GroupBy(x => x)
                                                  .ToDictionary(g => g.Key,
                                                                g => g.Count());

                    foreach (var v in counts)
                    {
                        if (v.Value == 1)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 3)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 5)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 7)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 9)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }

                    }
                }
            }

            ViewState["SearchQA"] = txtSearchQuestion.Text.Trim().Replace("'", "''");
            ViewState["SearchQAID"] = ID;

            objDOQAPosting.ID = ID;
            dtt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetSearchResults);
        }
        else
        {
            if (ViewState["SearchQA"] != null)
            {
                objDOQAPosting.strSearch = Convert.ToString(ViewState["SearchQA"]);
                objDOQAPosting.ID = Convert.ToString(ViewState["SearchQAID"]);
                dtt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetSearchResults);
            }
            else
            {
                dtt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetQueAnsDetailss);
            }

        }

        if (dtt.Rows.Count > 0)
        {
            lstParentQADetails.Visible = true;
            lblmsg.Visible = false;
            lstParentQADetails.DataSource = dtt;
            lstParentQADetails.DataBind();
            dvPage.Visible = true;
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dtt.Rows[0]["Maxcount"]));

        }
        else
        {
            lblmsg.Visible = true;
            lstParentQADetails.DataSource = null;
            lstParentQADetails.DataBind();
            lstParentQADetails.Visible = false;
            dvPage.Visible = false;
        }
        txtSearchQuestion.Text = "";
        hdnSubject.Value = "";
    }

    protected void lstParentQADetails_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnPostQuestionID = (HiddenField)e.Item.FindControl("hdnPostQuestionID");
        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
        Panel pnlAttachFile = (Panel)e.Item.FindControl("pnlAttachFile");
        Label lblAttachDocs = (Label)e.Item.FindControl("lblAttachDocs");
        ListView lstSubjCategory = (ListView)e.Item.FindControl("lstSubjCategory");
        HiddenField intAddedBy = (HiddenField)e.Item.FindControl("hdnAddedBy");
        LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
        DataTable dtChildContext = new DataTable();

        if (Convert.ToInt32(ViewState["UserID"]) == Convert.ToInt32(intAddedBy.Value))
        {
            lnkDelete.Visible = true;
            lnkEdit.Visible = true;
        }

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
        Label btnLike = (Label)e.Item.FindControl("btnLike");
        Label lblTotallike = (Label)e.Item.FindControl("lblTotallike");
        Label lblreply = (Label)e.Item.FindControl("lblreply");
        Label lblShare = (Label)e.Item.FindControl("lblShare");
        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
        objDOQAPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetTotalLikeByById);
        if (dt.Rows.Count > 0)
        {
            int TotalLike = Convert.ToInt32(dt.Rows[0]["TotalLike"]);
            int TotalReply = Convert.ToInt32(dt.Rows[0]["TotalReply"]);
            int TotalShare = Convert.ToInt32(dt.Rows[0]["TotalShare"]);
            lblTotallike.Text = Convert.ToString(TotalLike);
            lblreply.Text = Convert.ToString(TotalReply);
            lblShare.Text = Convert.ToString(TotalShare);

        }
        else
        {
            lblTotallike.Text = "0";
            lblreply.Text = "0";
            lblShare.Text = "0";
        }
        lblTotallike.ToolTip = "View Likes";
        DataTable dtLike = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetLikeUser);
        if (dtLike.Rows.Count > 0)
        {
            for (int i = 0; i < dtLike.Rows.Count; i++)
            {
                if (lblTotallike.ToolTip != "View Likes")
                    lblTotallike.ToolTip += Convert.ToString(dtLike.Rows[i]["UserName"]) + Environment.NewLine;
                else
                    lblTotallike.ToolTip = Convert.ToString(dtLike.Rows[i]["UserName"]) + Environment.NewLine;
            }
        }
    }

    protected void lstParentQADetails_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPostQuestionID = (HiddenField)e.Item.FindControl("hdnPostQuestionID");
        LinkButton Label1 = (LinkButton)e.Item.FindControl("Label1");
        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
        if (e.CommandName == "QADetails")
        {
            if (ViewState["MP"] == null)
            {
                Response.Redirect("Question-Details-SendContact.aspx?PostQAId=" + hdnPostQuestionID.Value);
            }
            else
            {
                Response.Redirect("Question-Details-SendContact.aspx?PostQAId=" + hdnPostQuestionID.Value + "&MP=" + Convert.ToString(ViewState["MP"]));
            }
        }
        else if (e.CommandName == "Delete QA")
        {
            ViewState["hdnPostQuestionID"] = hdnPostQuestionID.Value;
            ViewState["Questiona"] = Label1.Text;
        }
        else if (e.CommandName == "Edit QA")
        {
            Response.Redirect("post-new-question.aspx?PostQAId=" + hdnPostQuestionID.Value);
        }
        if (ViewState["MP"] != null)
        {
            BindMostPopular();
        }
        else
        {
            BindQADetails();
        }
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnDeletePostQuestionID.Value);
        objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.DeleteQA);

        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(ViewState["hdnPostQuestionID"]);
        objLog.strAction = "Q&A";
        objLog.strActionName = hdnstrQuestionDescription.Value;
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 25;   // Q&A Delete
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
        if (ViewState["MP"] != null)
        {
            BindMostPopular();
        }
        else
        {
            BindQADetails();
        }
        divDeletesucess.Style.Add("display", "none");
        hdnDeletePostQuestionID.Value = "";
    }

    protected void lstSubjCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnPostQuestionID = (HiddenField)e.Item.FindControl("hdnPostQuestionID");
        HiddenField hdnPostQueAnsnID = (HiddenField)e.Item.FindControl("hdnPostQueAnsnID");

        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");

        SubLi.Attributes.Add("class", "#A1A1A1");
    }

    protected void chkRecent_CheckedChanged(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "10";
        ViewState["SearchQA"] = null;
        ViewState["MP"] = null;
        lnkAllBlog.Attributes.Add("class", "activeq");
        lnkRecentBlogs.Attributes.Add("class", "activeqs");
        BindQADetails();
    }

    protected void chkMostPopular_CheckedChanged(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        ViewState["SearchQA"] = null;
        lnkAllBlog.Attributes.Add("class", "activeqs");
        lnkRecentBlogs.Attributes.Add("class", "activeq");
        BindMostPopular();
        ViewState["MP"] = "MP";
    }

    protected void BindMostPopular()
    {
        objDOQAPosting.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objDOQAPosting.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        dtt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetMostLike);
        if (dtt.Rows.Count > 0)
        {
            lstParentQADetails.Visible = true;
            lstParentQADetails.DataSource = dtt;
            lstParentQADetails.DataBind();
            dvPage.Visible = false;
            lblmsg.Visible = false;
        }
        else
        {
            dvPage.Visible = false;
            lstParentQADetails.Visible = false;
            lblmsg.Visible = true;
        }
    }

    protected void SubjectSearchTempDataTable()
    {
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
        ViewState["SubjectSearchCategory"] = dtSubjCat;
    }

    private void BindSearchSubjectList()
    {
        DataTable dtSub = new DataTable();

        dtSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.AllRecord);
        if (dtSub.Rows.Count > 0)
        {
            lstSerchSubjCategory.DataSource = dtSub;
            lstSerchSubjCategory.DataBind();
        }
        else
        {
            lstSerchSubjCategory.DataSource = null;
            lstSerchSubjCategory.DataBind();
        }
    }

    protected void lstSerchSubjCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnSubCatId = (HiddenField)e.Item.FindControl("hdnSubCatId");
        LinkButton lnkCatName = (LinkButton)e.Item.FindControl("lnkCatName");
        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
        DataTable dtsub = new DataTable();
        string ID = "";
        SubLi.Attributes.Add("class", "unselectBlogLi");
        if (ViewState["SubjectSearchCategory"] != null)
        {
            if (Convert.ToString(ViewState["SubjectSearchCategory"]) != "")
            {
                string[] totalSubjects = Convert.ToString(ViewState["SubjectSearchCategory"]).Split(',');
                var dictionary = new Dictionary<int, int>();

                if (totalSubjects.Count() > 0)
                {
                    Dictionary<string, int> counts = totalSubjects.GroupBy(x => x)
                                                  .ToDictionary(g => g.Key,
                                                                g => g.Count());

                    foreach (var v in counts)
                    {
                        if (v.Value == 1)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 3)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 5)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 7)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 9)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }

                        if (hdnSubCatId.Value == Convert.ToString(ID))
                        {
                            SubLi.Attributes.Add("class", "selectBlogLi");
                            lnkCatName.Attributes.Add("class", "selectBlogcat");
                        }
                        ID = "";
                    }
                }
            }

        }
    }

    protected void HideShowSubject()
    {
        if (lnkViewSubj.Text == "View all")
        {
            BindTopSubjectList();
        }
        else
        {
            BindSearchSubjectList();
        }
    }

    private void BindTopSubjectList()
    {
        DataTable dtTopSub = new DataTable();
        dtTopSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.GetTopRecords);
        if (dtTopSub.Rows.Count > 0)
        {
            lstSerchSubjCategory.DataSource = dtTopSub;
            lstSerchSubjCategory.DataBind();
        }
        else
        {
            lstSerchSubjCategory.DataSource = null;
            lstSerchSubjCategory.DataBind();
        }
        ViewState["DocId"] = null;
    }

    protected void lnkViewSubj_Click(object sender, EventArgs e)
    {
        ViewState["SubjectSearchCategory"] = hdnSubject.Value;
        viewAllsub();
    }

    protected void viewAllsub()
    {
        divDeletesucess.Style.Add("display", "none");
        ViewState["Update"] = "Update";
        if (lnkViewSubj.Text == "View all")
        {
            lnkViewSubj.Text = "Close";
        }
        else
        {
            lnkViewSubj.Text = "View all";
        }
        HideShowSubject();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", " CallTop();", true);
        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "10";
        BindQADetails();
        txtSearchQuestion.Text = "";
        divDeletesucess.Style.Add("display", "none");
    }

    protected void btnTag_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", " CallTop();", true);
        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "10";
        BindTags();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallTagSelections", " CallTagSelections();", true);
        divDeletesucess.Style.Add("display", "none");
    }

    public void BindTags()
    {
        String ID = string.Empty;
        objDOQAPosting.strSearch = txtSearchQuestion.Text.Trim().Replace("'", "''");
        if (hdnSubject.Value != "")
        {
            string[] totalSubjects = hdnSubject.Value.Split(',');
            var dictionary = new Dictionary<int, int>();

            if (totalSubjects.Count() > 0)
            {
                Dictionary<string, int> counts = totalSubjects.GroupBy(x => x)
                                              .ToDictionary(g => g.Key,
                                                            g => g.Count());

                foreach (var v in counts)
                {
                    if (v.Value == 1)
                    {
                        if (string.IsNullOrEmpty(ID))
                            ID = Convert.ToString(v.Key);
                        else
                            ID += "," + Convert.ToString(v.Key);
                    }
                    else if (v.Value == 3)
                    {
                        if (string.IsNullOrEmpty(ID))
                            ID = Convert.ToString(v.Key);
                        else
                            ID += "," + Convert.ToString(v.Key);
                    }
                    else if (v.Value == 5)
                    {
                        if (string.IsNullOrEmpty(ID))
                            ID = Convert.ToString(v.Key);
                        else
                            ID += "," + Convert.ToString(v.Key);
                    }
                    else if (v.Value == 7)
                    {
                        if (string.IsNullOrEmpty(ID))
                            ID = Convert.ToString(v.Key);
                        else
                            ID += "," + Convert.ToString(v.Key);
                    }
                    else if (v.Value == 9)
                    {
                        if (string.IsNullOrEmpty(ID))
                            ID = Convert.ToString(v.Key);
                        else
                            ID += "," + Convert.ToString(v.Key);
                    }

                }
            }
        }

        ViewState["SearchQA"] = txtSearchQuestion.Text.Trim().Replace("'", "''");
        ViewState["SearchQAID"] = ID;

        objDOQAPosting.ID = ID;
       DataTable datat = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetSearchResults);
       if (datat.Rows.Count > 0)
        {
            lstParentQADetails.Visible = true;
            lblmsg.Visible = false;
            lstParentQADetails.DataSource = datat;
            lstParentQADetails.DataBind();
            dvPage.Visible = true;
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(datat.Rows[0]["Maxcount"]));

        }
        else
        {
            lblmsg.Visible = true;
            lstParentQADetails.DataSource = null;
            lstParentQADetails.DataBind();
            lstParentQADetails.Visible = false;
            dvPage.Visible = false;
        }

    }

    protected void btnAllQuestion_Click(object sender, EventArgs e)
    {
        BindQADetails();
    }

    #region Paging For All

    protected void BindRptPager(Int64 PageSize, Int64 CurrentPage, Int64 MaxCount)
    {
        if (MaxCount > 0 && CurrentPage > 0 && PageSize > 0)
        {
            Int64 DisplayPage = 10;
            Int64 totalPage = (MaxCount / PageSize) + ((MaxCount % PageSize) == 0 ? 0 : 1);
            Int64 StartPage = (((CurrentPage / DisplayPage) - ((CurrentPage % DisplayPage) == 0 ? 1 : 0)) * DisplayPage) + 1;    
            Int64 EndPage = ((CurrentPage / DisplayPage) + ((CurrentPage % DisplayPage) == 0 ? 0 : 1)) * DisplayPage;

            hdnNextPage.Value = (CurrentPage + 1).ToString();
            hdnPreviousPage.Value = (CurrentPage - 1).ToString();
            hdnEndPage.Value = totalPage.ToString();

            if (totalPage < EndPage)
            {
                if (totalPage != StartPage)
                {
                    EndPage = totalPage;
                    hdnEndPage.Value = EndPage.ToString();
                }
                else
                {
                    StartPage = StartPage - DisplayPage;
                    StartPage++;
                    EndPage = totalPage;
                    hdnEndPage.Value = EndPage.ToString();
                }
            }
            else
            {
                if (Convert.ToInt32(hdnNextPage.Value) == totalPage)
                {
                  StartPage ++;
                  EndPage = totalPage;
                  hdnEndPage.Value = EndPage.ToString();
                }
            }

            if (totalPage == 1)
            {
                dvPage.Visible = false;
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
                    hdnLastPage.Value = i.ToString();
                }

                rptDvPage.DataSource = dtPage;
                rptDvPage.DataBind();


                if (CurrentPage > DisplayPage)
                {
                    lnkPrevious.Visible = true;
                    //hdnPreviousPage.Value = (StartPage - 1).ToString();
                    hdnPreviousPage.Value = (CurrentPage - 1).ToString();
                }
                else
                {
                    lnkPrevious.Visible = true;
                }
                if (totalPage >= EndPage)
                {
                    lnkNext.Visible = true;
                    //hdnNextPage.Value = (EndPage + 1).ToString();
                    hdnLastPage.Value = totalPage.ToString();
                }
                else
                {
                    lnkNext.Visible = true;
                }
            }
        }

    }

    protected void lnkNext_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        if (Convert.ToInt32(hdnEndPage.Value) >= Convert.ToInt32(hdnNextPage.Value))
        {
            imgPaging.Style.Add("opacity", "1.2");
            hdnCurrentPage.Value = hdnNextPage.Value;
            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindQADetails();
            }
            else
            {
                BindQADetails();
            }
        }
    }

    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        hdnCurrentPage.Value = "1";
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindQADetails();
        }
        else
        {
            BindQADetails();
        }
    }

    protected void lnkLast_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        hdnCurrentPage.Value = hdnLastPage.Value;
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindQADetails();
        }
        else
        {
            BindQADetails();
        }
    }

    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        if (hdnPreviousPage.Value != "0")
        {
            hdnCurrentPage.Value = hdnPreviousPage.Value;
            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindQADetails();
            }
            else
            {
                BindQADetails();
            }
        }
    }

    protected void rptDvPage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        if (e.CommandName == "PageLink")
        {
            LinkButton lnkPageLink = (LinkButton)e.Item.FindControl("lnkPageLink");
            if (lnkPageLink != null)
            {
                hdnCurrentPage.Value = lnkPageLink.Text;
                lnkPageLink.Style.Add("color", "#141414 !important");
                lnkPageLink.Style.Add("text-decoration", "none !important");

                if (lnkPageLink.Text == "")
                {
                    hdnCurrentPage.Value = "1";
                }
                if (lnkPageLink.Text != "1")
                {
                    imgPaging.Style.Add("opacity", "1.2");
                }
                else
                {
                    imgPaging.Style.Add("opacity", "0.2");
                }
                if (Convert.ToString(ViewState["ViewAll"]) == "1")
                {
                    BindQADetails();
                }
                else
                {
                    BindQADetails();
                }
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
                    lnkPageLink.Enabled = false;
                    if (ViewState["lnkPageLink"] != null)
                    {
                        if (lnkPageLink.Text == "1")
                        {
                            ViewState["lnkPageLink"] = null;
                        }
                    }
                }
                else
                {
                    lnkPageLink.Enabled = true;
                }

                if (hdnCurrentPage.Value == "1")
                {
                    ViewState["lnkPageLink"] = "PageCount";
                }

            }
        }

    }

    #endregion


}