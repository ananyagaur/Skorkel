using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Web;

public partial class Research_SearchResult_S : System.Web.UI.Page
{
    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DA_ResearchResult DAobjResearch = new DA_ResearchResult();
    DO_ResearchResult DOobjResearch = new DO_ResearchResult();

    DO_SaveMySearch objSaveMySearchDO = new DO_SaveMySearch();
    DA_SaveMySearch objSaveMySearchDA = new DA_SaveMySearch();

    DataTable dtResearch = new DataTable();
    DataRow row;

    static string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];

    protected void Page_Load(object sender, EventArgs e)
    {
        divCitationSearch.Style.Add("display", "none");
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            BindSubjectList();
            SubjectTempDataTable();
            BindYear();
            BindCourt();
            getJudgesName();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "DefaultCall();", true);

            if (Session["strSearchQuery"] != null)
            {
                GetMySavedSearchResult(Session["strSearchQuery"].ToString());
                lnkSAvesearch.Enabled = true;
                ViewState["Searchresult"] = "Searchresult";
                Session.Remove("strSearchQuery");
            }
        }
    }

    protected void BindYear()
    {

        ddlReporterName.Items.Insert(0, "Reporter Name");
        ddlVolumns.Items.Insert(0, "Volume");
        ddlPageNo.Items.Insert(0, "Page No.");
        DataTable dtSub = new DataTable();
        dtSub = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetYear);
        if (dtSub.Rows.Count > 0)
        {
            ddlYearFT.DataSource = dtSub;
            ddlYearFT.DataTextField = "intYear";
            ddlYearFT.DataValueField = "intYear";
            ddlYearFT.DataBind();
            ddlYearFT.Items.Insert(0, "Select year");
        }
        else
        {
            ddlYearFT.DataSource = null;
        }

        DataTable dtCit = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetYearCitation);
        if (dtCit.Rows.Count > 0)
        {
            ddlYear.DataSource = dtCit;
            ddlYear.DataTextField = "intYear";
            ddlYear.DataValueField = "intYearId";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, "Year");
            ddlYear.Items.Insert(dtCit.Rows.Count+1, "None");
        }
        else
        {
            ddlYear.DataSource = null;
        }
    }

    protected void BindCourt()
    {
        DataTable dtSub = new DataTable();
        dtSub = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetCourt);
        ddlCourtFT.DataSource = dtSub;
        ddlCourtFT.DataTextField = "CourtName";
        ddlCourtFT.DataValueField = "CourtName";
        ddlCourtFT.DataBind();
        ddlCourtFT.Items.Insert(0, "Select type of court");
        ddlCourt.Items.Insert(0, "Court");

    }

    protected void getJudgesName()
    {
        DataTable dtSub = new DataTable();
        dtSub = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetJudes);
        if (dtSub.Rows.Count > 0)
        {
            txtJudgesMember.DataSource = dtSub;
            txtJudgesMember.DataValueField = "intRegistrationId";
            txtJudgesMember.DataTextField = "JudgeName";
            txtJudgesMember.DataBind();
        }
    }

    #region SubjectCatgory

    /// <summary>
    /// Bind viewstate "ViewState["SubjectCategory"]" with DataTable
    /// For Subject Category
    /// </summary>
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

    protected void BindSubjectList()
    {

        DataTable dtSub = new DataTable();
        dtSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.AllRecord);
        if (dtSub.Rows.Count > 0)
        {
            lstSubjCategory.DataSource = dtSub;
            lstSubjCategory.DataBind();

            txtSubjectList.DataSource = dtSub;
            txtSubjectList.DataValueField = "intCategoryId";
            txtSubjectList.DataTextField = "strCategoryName";
            txtSubjectList.DataBind();
        }
        else
        {
            lstSubjCategory.DataSource = null;
            lstSubjCategory.DataBind();
        }

    }

    /// <summary>
    /// DataBound Event For SubjectCategory
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LstSubjCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnSubCatId = (HiddenField)e.Item.FindControl("hdnSubCatId");
        LinkButton lnkCatName = (LinkButton)e.Item.FindControl("lnkCatName");
        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
        DataTable dtsub = new DataTable();
        if (ViewState["SubjectCategory"] != null)
        {
            dtsub = (DataTable)ViewState["SubjectCategory"];
            if (dtsub.Rows.Count > 0)
            {
                for (int i = 0; i < dtsub.Rows.Count; i++)
                {
                    if (hdnSubCatId.Value == dtsub.Rows[i]["intCategoryId"].ToString())
                    {
                        SubLi.Style.Add("background", "none repeat scroll 0 0 #00B7BD !important");
                        SubLi.Style.Add("color", "#FFFFFF !important");
                        SubLi.Style.Add("text-decoration", "none !important");
                        lnkCatName.ForeColor = System.Drawing.Color.White;
                        lnkCatName.Style.Add("color", "#FFFFFF !important");
                    }
                }

            }
            else
            {
                SubLi.Style.Add("background", "#ebeaea");
                SubLi.Style.Add("border", "1px solid #c8c8c8");
                SubLi.Style.Add("color", "#646161");
            }

        }
    }

    /// <summary>
    /// Subject Category Item Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                            if (Convert.ToInt32(ViewState["MId"]) > 0)
                                dtSubjCat.Rows.Remove(dtContent.Rows[i]);
                            ViewState["SubjectCategory"] = dtSubjCat;
                            //SubLi.Attributes.Add("class", "gray");
                            //SubLi.Style.Add("background", "#ebeaea");
                            //SubLi.Style.Add("border", "1px solid #c8c8c8");
                            //SubLi.Style.Add("color", "#646161");
                            lnkCatName.Style.Add("color", "#fff");
                            return;
                        }
                    }
                    if (dtSubjCat.Rows.Count <= 0)
                    {
                        ViewState["MId"] = hdnSubCatId.Value;
                        rwSubj["intCategoryId"] = hdnSubCatId.Value;
                        rwSubj["strCategoryName"] = lnkCatName.Text.Trim();
                        dtSubjCat.Rows.Add(rwSubj);
                        ViewState["SubjectCategory"] = dtSubjCat;
                    }
                    else
                    {
                        Subjectid = hdnSubCatId.Value.ToString();
                        ViewState["MId"] = Subjectid;
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
                        ViewState["MId"] = hdnSubCatId.Value;
                        rwSubj["intCategoryId"] = hdnSubCatId.Value;
                        rwSubj["strCategoryName"] = lnkCatName.Text.Trim();
                        dtSubjCat.Rows.Add(rwSubj);
                        ViewState["SubjectCategory"] = dtSubjCat;
                    }
                    else
                    {
                        Subjectid = hdnSubCatId.Value.ToString();
                        ViewState["MId"] = Subjectid;
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
                // SubLi.Style.Add("background", "none repeat scroll 0 0 #00B7BD !important");
                // SubLi.Style.Add("color", "#FFFFFF !important");
                // SubLi.Style.Add("text-decoration", "none !important");
                lnkCatName.ForeColor = System.Drawing.Color.White;
                lnkCatName.Style.Add("color", "#329196 !important");
            }
            else
            {
                //SubLi.Style.Add("background", "#ebeaea");
                //SubLi.Style.Add("border", "1px solid #c8c8c8");
                //SubLi.Style.Add("color", "#646161");
                lnkCatName.Style.Add("color", "#329196");
            }
        }


    }

    #endregion

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        hdnEnterKeyword.Value = "";
        if (hdnMembId.Value == "" && hdnsubject.Value == "" && ddlYearFT.Text == "Select year" && ddlCourtFT.Text == "Select type of court")
        {
            if (ddlSelect.SelectedItem.Text != "Target")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts0", "HidedivFT();", true);
            }
        }

        string year = "", court = "", pageNo = "", volume = "", report = "";
        string EqCitatios = "";
        divSavesearch.Style.Add("display", "none");
        ddlYear.SelectedIndex = 0;
        if (ddlCourtFT.Text != "Select type of court")
        {
            ViewState["ddlCourtFT"] = ddlCourtFT.Text;
        }
        else { ViewState["ddlCourtFT"] = ""; }

        if (ddlYearFT.Text != "Select year")
        {
            ViewState["ddlYearFT"] = ddlYearFT.Text;
        }
        else { ViewState["ddlYearFT"] = ""; }
        ViewState["sortCriteriya"] = "MR";

        if (Convert.ToString(CiCourt.Value) != "Court" && Convert.ToString(CiVolume.Value) != "Volume")
        {
            EqCitatios = Convert.ToString(CiYear.Value) + " " + Convert.ToString(CiCourt.Value) + " " + Convert.ToString(CiReport.Value) + " " + Convert.ToString(CiVolume.Value) + " " + Convert.ToString(CiPage.Value);
        }
        else if (Convert.ToString(CiCourt.Value) != "Court" && Convert.ToString(CiVolume.Value) == "Volume")
        {
            EqCitatios = Convert.ToString(CiYear.Value) + " " + Convert.ToString(CiCourt.Value) + " " + Convert.ToString(CiReport.Value) + " " + Convert.ToString(CiPage.Value);
        }
        else if (Convert.ToString(CiCourt.Value) == "Court" && Convert.ToString(CiVolume.Value) != "Volume")
        {
            EqCitatios = Convert.ToString(CiYear.Value) + " " + Convert.ToString(CiReport.Value) + " " + Convert.ToString(CiVolume.Value) + " " + Convert.ToString(CiPage.Value);
        }
        else if (Convert.ToString(CiCourt.Value) == "Court" && Convert.ToString(CiVolume.Value) == "Volume")
        {
            if (Convert.ToString(CiYear.Value) != "Year")
            {
                EqCitatios = Convert.ToString(CiYear.Value) + " " + Convert.ToString(CiReport.Value) + " " + Convert.ToString(CiPage.Value);
            }
        }

        if (Convert.ToString(ViewState["ddlYearFT"]) == "")
        {
            if (Convert.ToString(CiYear.Value) != "Year")
            {
                year = Convert.ToString(CiYear.Value);
            }
        }
        else
        {
            year = Convert.ToString(ViewState["ddlYearFT"]);
        }

        if (Convert.ToString(ViewState["ddlCourtFT"]) == "")
        {
            if (Convert.ToString(CiCourt.Value) != "Court")
            {
                court = Convert.ToString(CiCourt.Value);
            }
        }
        else
        {
            court = Convert.ToString(ViewState["ddlCourtFT"]);
        }
        if (Convert.ToString(CiPage.Value) == "Page No.")
        {
            pageNo = "";
        }
        else
        {
            pageNo = Convert.ToString(CiPage.Value);
        }

        if (CiVolume.Value != "Volume")
        {
            volume = CiVolume.Value;
        }
        if (CiReport.Value != "Reporter Name")
        {
            report = CiReport.Value;
        }

        String url = APIURL + "skorkelAdvanceSearch.action?"
                    + "suid=" + Convert.ToString(ViewState["UserID"])
                    + "&lookInto=" + ""
                    + "&title=" + Convert.ToString(txtResearch.Value)
                    + "&citation=" + Convert.ToString(EqCitatios.Trim())
                    + "&context=" + Convert.ToString(hdnsubjectText.Value)
                    + "&dateFrom=" + null
                    + "&dateTo=" + null
                    + "&judgeName=" + Convert.ToString(hdnMembIdText.Value)
                    + "&sortCriteriya=" + "MR"
                    + "&maxResultPerPage=" + 50
                    + "&pageIdx=" + null
                    + "&year=" + Convert.ToString(year)
                    + "&court=" + Convert.ToString(court)
                    + "&reporter=" + Convert.ToString(report)
                    + "&volume=" + Convert.ToString(volume)
                    + "&cursor=" + null
                    + "&searchtype=" + ddlSelect.SelectedItem.Text
                    + "&pageNo=" + Convert.ToString(pageNo);


        if (ddlSelect.SelectedItem.Text == "Free Text")
        {
            string s = txtResearch.Value.Trim() + "," + hdnMembIdText.Value + "," + year + "," + court;
            if (txtResearch.Value.Trim() != "")
            {
                string Fstring = "";
                if (hdnMembIdText.Value.Trim() != "")
                {
                    if (year.Trim() != "")
                    {
                        if (court.Trim() != "")
                        {
                            s = txtResearch.Value.Trim() + "," + hdnMembIdText.Value + "," + year + "," + court;
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }
                        else
                        {
                            s = txtResearch.Value.Trim() + "," + hdnMembIdText.Value + "," + year;
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }

                    }
                    else
                    {
                        if (court.Trim() != "")
                        {
                            s = txtResearch.Value.Trim() + "," + hdnMembIdText.Value + "," + court;
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }
                        else
                        {
                            s = txtResearch.Value.Trim() + "," + hdnMembIdText.Value;
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }
                    }
                }
                else
                {
                    if (year.Trim() != "")
                    {
                        if (court.Trim() != "")
                        {
                            s = txtResearch.Value.Trim() + "," + year + "," + court;
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }
                        else
                        {
                            s = txtResearch.Value.Trim() + "," + year;
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }

                    }
                    else
                    {
                        if (court.Trim() != "")
                        {
                            s = txtResearch.Value.Trim() + "," + court;
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }
                        else
                        {
                            s = txtResearch.Value.Trim();
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }
                    }
                }

                lblEnterKeyword.Text = Fstring;
                hdnEnterKeyword.Value = s;
            }
        }
        else if (ddlSelect.SelectedItem.Text == "Citation")
        {
            lblEnterKeyword.Text = EqCitatios.Trim();
            hdnEnterKeyword.Value = EqCitatios.Trim();
        }
        else if (ddlSelect.SelectedItem.Text == "Skorkel")
        {
            string s = txtResearch.Value.Trim() + "," + hdnMembIdText.Value + "," + year + "," + court;
            if (txtResearch.Value.Trim() != "")
            {
                string Fstring = "";
                if (hdnMembIdText.Value.Trim() != "")
                {
                    if (year.Trim() != "")
                    {
                        if (court.Trim() != "")
                        {
                            s = txtResearch.Value.Trim() + "," + hdnMembIdText.Value + "," + year + "," + court;
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }
                        else
                        {
                            s = txtResearch.Value.Trim() + "," + hdnMembIdText.Value + "," + year;
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }

                    }
                    else
                    {
                        if (court.Trim() != "")
                        {
                            s = txtResearch.Value.Trim() + "," + hdnMembIdText.Value + "," + court;
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }
                        else
                        {
                            s = txtResearch.Value.Trim() + "," + hdnMembIdText.Value;
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }
                    }
                }
                else
                {
                    if (year.Trim() != "")
                    {
                        if (court.Trim() != "")
                        {
                            s = txtResearch.Value.Trim() + "," + year + "," + court;
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }
                        else
                        {
                            s = txtResearch.Value.Trim() + "," + year;
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }

                    }
                    else
                    {
                        if (court.Trim() != "")
                        {
                            s = txtResearch.Value.Trim() + "," + court;
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }
                        else
                        {
                            s = txtResearch.Value.Trim();
                            Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
                        }
                    }
                }

                lblEnterKeyword.Text = Fstring;
                hdnEnterKeyword.Value = s;
            }
        }
        else if (ddlSelect.SelectedItem.Text == "Target")
        {
            string s = hdnMembIdText.Value + ", " + year + ", " + court;
            if (hdnMembIdText.Value != "")
            {
                s = hdnMembIdText.Value;
            }
            if (year != "")
            {
                if (hdnMembIdText.Value != "")
                {
                    s = hdnMembIdText.Value + ", " + year;
                }
                else
                {
                    s = year;
                }
            }
            if (court != "")
            {
                if (year != "")
                {
                    if (hdnMembIdText.Value != "")
                    {
                        s = hdnMembIdText.Value + ", " + year + ", " + court;
                    }
                    else
                    {
                        s = year + ", " + court;
                    }
                }
                else
                {
                    s = hdnMembIdText.Value + ", " + court;
                }
            }

            string Fstring = s.Length <= 30 ? s : new string(s.Take(30).ToArray()) + "...";
            lblEnterKeyword.Text = Fstring;
            hdnEnterKeyword.Value = s;
        }

        ViewState["SortUrl"] = url;
        GetMySavedSearchResult(url);
        lnkSAvesearch.Enabled = true;
        ViewState["Searchresult"] = "Searchresult";
        CiYear.Value = "Year";
        CiCourt.Value = "Court";
        CiReport.Value = "Reporter Name";
        CiVolume.Value = "Volume";
        CiPage.Value = "Page No.";
        ViewState["ddlCourtFT"] = null;
        ViewState["ddlYearFT"] = null;


    }

    protected void GetMySavedSearchResult(string Research)
    {
        try
        {
            HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(Convert.ToString(Research));
            myRequest1.Method = "GET";
            WebResponse myResponse1 = myRequest1.GetResponse();

            StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
            String result = sr.ReadToEnd();
            GetJsonDocument(result);
            ClearSearchData();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            pLoadMore.Style.Add("display", "none");
            lblNoMoreRslt.Visible = true;
            lblEnterKeyword.Text = "Entered Keyword or Search Attributes";
            hdnEnterKeyword.Value = "";
        }
    }

    protected void GetJsonDocument(string rslt)
    {
        string data = Convert.ToString(ViewState["url"]);

        _responseJson1 my = JsonConvert.DeserializeObject<_responseJson1>(rslt);
        StringBuilder sb = new StringBuilder();
        ResearchDataTable();
        if (my.responseJson != null)
        {
            try
            {
                lblResultCount.Text = my.responseJson.totalResultCount.ToString();
                ViewState["searchCount"] = my.responseJson.totalResultCount.ToString();
                ViewState["CursorID"] = my.responseJson.cursorId;
                var NameList = (from r in my.responseJson.searchResultDocumentSets
                                select new
                                {
                                    appellant = r.appellant,
                                    court = r.court,
                                    bookmark = r.bookmark,
                                    commentCount = r.commentCount,
                                    displayContent = r.displayContent,
                                    docType = r.docType,
                                    downloadCount = r.downloadCount,
                                    judgeTagCount = r.judgeTagCount,
                                    likes = r.likes,
                                    profTagCount = r.profTagCount,
                                    rating = r.rating,
                                    score = r.score,
                                    shareCount = r.shareCount,
                                    studentTagCount = r.studentTagCount,
                                    title = r.title,
                                    uploadBy = r.uploadBy,
                                    uploadByName = r.uploadByName,
                                    uploadDt = r.uploadDt,
                                    weightage = r.weightage,
                                    tagCnt = r.tagCnt,
                                    citation = r.citation,
                                    r.year,
                                    r.docUid,
                                    r.citedBy,
                                    r.subject,
                                    r.judgeNames
                                }).Distinct().ToList();
                foreach (var v in NameList)
                {
                    row = dtResearch.NewRow();
                    row["appellant"] = v.appellant;
                    row["bookmark"] = v.bookmark;
                    row["commentCount"] = v.commentCount;
                    row["court"] = v.court;
                    row["displayContent"] = v.displayContent;
                    row["docType"] = v.docType;
                    row["downloadCount"] = v.downloadCount;
                    row["judgeTagCount"] = v.judgeTagCount;
                    row["likes"] = v.likes;
                    row["profTagCount"] = v.profTagCount;
                    row["rating"] = v.rating;
                    row["score"] = v.score;
                    row["shareCount"] = v.shareCount;
                    row["studentTagCount"] = v.studentTagCount;
                    row["tagCnt"] = v.tagCnt;
                    row["title"] = v.title;
                    row["uploadBy"] = v.uploadBy;
                    row["uploadByName"] = v.uploadByName;
                    row["uploadDt"] = v.uploadDt;
                    row["weightage"] = v.weightage;
                    row["citation"] = v.citation;
                    row["year"] = v.year;
                    row["docUid"] = v.docUid;
                    row["citedBy"] = v.citedBy;
                    row["subject"] = v.subject;
                    row["judgeNames"] = v.judgeNames;
                    dtResearch.Rows.Add(row);
                }

                if (dtResearch.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "functionHideCall();", true);
                    divsearchHeight.Style.Add("height", "auto");

                    ViewState["SearchResults"] = dtResearch;
                    hdnMaxcount.Value = my.responseJson.totalResultCount.ToString();
                    int pageNum = 1;
                    int pageSize = 10;
                    DataTable dtPage = dtResearch.Rows.Cast<System.Data.DataRow>().Skip((pageNum - 1) * pageSize).Take(pageSize).CopyToDataTable();
                    lstSearchResult.DataSource = dtPage;
                    lstSearchResult.DataBind();
                    if (dtResearch.Rows.Count > pageSize)
                    {
                        pLoadMore.Style.Add("display", "block");
                        lblNoMoreRslt.Visible = false;
                    }
                    else
                    {
                        pLoadMore.Style.Add("display", "none");
                        lblNoMoreRslt.Visible = true;
                        //lblEnterKeyword.Text = "Entered Keyword or Search Attributes";
                    }
                    dtResearch = new DataTable();
                    ViewState["NewPageSize"] = null;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "functionHideCall();", true);
                    lstSearchResult.DataSource = null;
                    lstSearchResult.DataBind();
                    divsearchHeight.Style.Add("height", "550px");
                    dtResearch = new DataTable();
                    pLoadMore.Style.Add("display", "none");
                    lblNoMoreRslt.Visible = true;
                    //lblEnterKeyword.Text = "Entered Keyword or Search Attributes";
                }

            }
            catch
            {
            }
        }

        else if (my.responseJson == null)
        {
            divsearchHeight.Style.Add("height", "550px");
            lblResultCount.Text = "0";
            lstSearchResult.DataSource = null;
            lstSearchResult.DataBind();
            pLoadMore.Style.Add("display", "none");
            lblNoMoreRslt.Visible = true;
            lblEnterKeyword.Text = "Entered Keyword or Search Attributes";
        }
    }

    protected void imgLoadMore_OnClick(object sender, EventArgs e)
    {
        int pageSize = 10;
        DataTable dt = (DataTable)ViewState["SearchResults"];
        hdnMaxcount.Value = ViewState["searchCount"].ToString();
        int pageNum = 1;
        if (ViewState["NewPageSize"] == null)
        {
            pageSize = pageSize + 10;
        }
        else
        {
            pageSize = Convert.ToInt32(ViewState["NewPageSize"]);
        }

        if (pageSize < dt.Rows.Count)
        {
            DataTable dtPage = dt.Rows.Cast<System.Data.DataRow>().Skip((pageNum - 1) * pageSize).Take(pageSize).CopyToDataTable();
            ViewState["NewPageSize"] = pageSize + 10;
            lstSearchResult.DataSource = dtPage;
            lstSearchResult.DataBind();

        }
        else if (pageSize > dt.Rows.Count)
        {
            DataTable dtPage = dt.Rows.Cast<System.Data.DataRow>().Skip((pageNum - 1) * pageSize).Take(pageSize).CopyToDataTable();
            ViewState["NewPageSize"] = pageSize + 10;
            lstSearchResult.DataSource = dtPage;
            lstSearchResult.DataBind();
            pLoadMore.Style.Add("display", "none");
            lblNoMoreRslt.Visible = true;
        }
        else
        {
            DataTable dtPage = dt.Rows.Cast<System.Data.DataRow>().Skip((pageNum - 1) * pageSize).Take(pageSize).CopyToDataTable();
            ViewState["NewPageSize"] = pageSize + 10;
            lstSearchResult.DataSource = dtPage;
            lstSearchResult.DataBind();
            FetchNextRows();
        }
    }

    protected void FetchNextRows()
    {
        String result = string.Empty;
        string CursorId = ViewState["CursorID"].ToString();
        String url = APIURL + "skorkelSearchHits.action?"
                    + "cursorId=" + CursorId;

        try
        {
            HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(Convert.ToString(url));
            myRequest1.Method = "GET";
            WebResponse myResponse1 = myRequest1.GetResponse();

            StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
            result = sr.ReadToEnd();
            //GetJsonDocument(result);
            ClearSearchData();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            SubjectTempDataTable();
        }

        string data = Convert.ToString(ViewState["url"]);


        _responseJson1 my = JsonConvert.DeserializeObject<_responseJson1>(result);
        StringBuilder sb = new StringBuilder();
        ResearchDataTable();
        if (my.responseJson != null)
        {
            try
            {
                lblResultCount.Text = my.responseJson.totalResultCount.ToString();
                ViewState["searchCount"] = my.responseJson.totalResultCount.ToString();
                ViewState["CursorID"] = my.responseJson.cursorId;
                var NameList = (from r in my.responseJson.searchResultDocumentSets
                                select new
                                {
                                    appellant = r.appellant,
                                    court = r.court,
                                    bookmark = r.bookmark,
                                    commentCount = r.commentCount,
                                    displayContent = r.displayContent,
                                    docType = r.docType,
                                    downloadCount = r.downloadCount,
                                    judgeTagCount = r.judgeTagCount,
                                    likes = r.likes,
                                    profTagCount = r.profTagCount,
                                    rating = r.rating,
                                    score = r.score,
                                    shareCount = r.shareCount,
                                    studentTagCount = r.studentTagCount,
                                    title = r.title,
                                    uploadBy = r.uploadBy,
                                    uploadByName = r.uploadByName,
                                    uploadDt = r.uploadDt,
                                    weightage = r.weightage,
                                    tagCnt = r.tagCnt,
                                    citation = r.citation,
                                    r.year,
                                    r.docUid,
                                    r.citedBy,
                                    r.subject
                                }).Distinct().ToList();
                foreach (var v in NameList)
                {
                    row = dtResearch.NewRow();
                    //row["RowNumber"] = i;
                    row["appellant"] = v.appellant;
                    row["bookmark"] = v.bookmark;
                    row["commentCount"] = v.commentCount;
                    row["court"] = v.court;
                    row["displayContent"] = v.displayContent;
                    row["docType"] = v.docType;
                    row["downloadCount"] = v.downloadCount;
                    row["judgeTagCount"] = v.judgeTagCount;
                    row["likes"] = v.likes;
                    row["profTagCount"] = v.profTagCount;
                    row["rating"] = v.rating;
                    row["score"] = v.score;
                    row["shareCount"] = v.shareCount;
                    row["studentTagCount"] = v.studentTagCount;
                    row["tagCnt"] = v.tagCnt;
                    row["title"] = v.title;
                    row["uploadBy"] = v.uploadBy;
                    row["uploadByName"] = v.uploadByName;
                    row["uploadDt"] = v.uploadDt;
                    row["weightage"] = v.weightage;
                    row["citation"] = v.citation;
                    row["year"] = v.year;
                    row["docUid"] = v.docUid;
                    row["citedBy"] = v.citedBy;
                    row["subject"] = v.subject;
                    dtResearch.Rows.Add(row);
                }

                if (dtResearch.Rows.Count > 0)
                {
                    DataTable dt = (DataTable)ViewState["SearchResults"];
                    string s = Convert.ToString(ViewState["sortCriteriya"]);
                    foreach (DataRow row in dtResearch.Rows)
                        dt.ImportRow(row);


                    if (Convert.ToString(ViewState["sortCriteriya"]) == "MS")
                    {
                        dt.DefaultView.Sort = "shareCount DESC";
                        DataView dw = dt.DefaultView;
                        dt = dw.ToTable();
                    }
                    else if (Convert.ToString(ViewState["sortCriteriya"]) == "MD")
                    {
                        dt.DefaultView.Sort = "downloadCount DESC";
                        DataView dw = dt.DefaultView;
                        dt = dw.ToTable();
                    }
                    else if (Convert.ToString(ViewState["sortCriteriya"]) == "MR")
                    {
                        //dt.DefaultView.Sort = "shareCount DESC";
                        //DataView dw = dt.DefaultView;
                        //dt = dw.ToTable();
                    }
                    else if (Convert.ToString(ViewState["sortCriteriya"]) == "MC")
                    {
                        dt.DefaultView.Sort = "commentCount DESC";
                        DataView dw = dt.DefaultView;
                        dt = dw.ToTable();
                    }

                    ViewState["SearchResults"] = dt;

                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "functionHideCall();", true);
                    //lstSearchResult.DataSource = null;
                    //lstSearchResult.DataBind();
                    //divsearchHeight.Style.Add("height", "550px");
                    //dtResearch = new DataTable();
                    pLoadMore.Style.Add("display", "none");
                    lblNoMoreRslt.Visible = false;
                }

            }
            catch
            {
            }
        }

    }

    protected void ResearchDataTable()
    {

        DataColumn judgeNames = new DataColumn();
        judgeNames.DataType = System.Type.GetType("System.String");
        judgeNames.ColumnName = "judgeNames";
        dtResearch.Columns.Add(judgeNames);

        DataColumn bookmark = new DataColumn();
        bookmark.DataType = System.Type.GetType("System.String");
        bookmark.ColumnName = "bookmark";
        dtResearch.Columns.Add(bookmark);

        DataColumn UserTypeId = new DataColumn();
        UserTypeId.DataType = System.Type.GetType("System.String");
        UserTypeId.ColumnName = "UserTypeId";
        dtResearch.Columns.Add(UserTypeId);

        DataColumn commentCount = new DataColumn();
        commentCount.DataType = System.Type.GetType("System.Int32");
        commentCount.ColumnName = "commentCount";
        dtResearch.Columns.Add(commentCount);

        DataColumn displayContent = new DataColumn();
        displayContent.DataType = System.Type.GetType("System.String");
        displayContent.ColumnName = "displayContent";
        dtResearch.Columns.Add(displayContent);

        DataColumn docType = new DataColumn();
        docType.DataType = System.Type.GetType("System.String");
        docType.ColumnName = "docType";
        dtResearch.Columns.Add(docType);

        DataColumn downloadCount = new DataColumn();
        downloadCount.DataType = System.Type.GetType("System.Int32");
        downloadCount.ColumnName = "downloadCount";
        dtResearch.Columns.Add(downloadCount);

        DataColumn judgeTagCount = new DataColumn();
        judgeTagCount.DataType = System.Type.GetType("System.String");
        judgeTagCount.ColumnName = "judgeTagCount";
        dtResearch.Columns.Add(judgeTagCount);

        DataColumn rating = new DataColumn();
        rating.DataType = System.Type.GetType("System.String");
        rating.ColumnName = "rating";
        dtResearch.Columns.Add(rating);

        DataColumn likes = new DataColumn();
        likes.DataType = System.Type.GetType("System.String");
        likes.ColumnName = "likes";
        dtResearch.Columns.Add(likes);

        DataColumn profTagCount = new DataColumn();
        profTagCount.DataType = System.Type.GetType("System.String");
        profTagCount.ColumnName = "profTagCount";
        dtResearch.Columns.Add(profTagCount);

        DataColumn score = new DataColumn();
        score.DataType = System.Type.GetType("System.String");
        score.ColumnName = "score";
        dtResearch.Columns.Add(score);

        DataColumn shareCount = new DataColumn();
        shareCount.DataType = System.Type.GetType("System.Int32");
        shareCount.ColumnName = "shareCount";
        dtResearch.Columns.Add(shareCount);

        DataColumn studentTagCount = new DataColumn();
        studentTagCount.DataType = System.Type.GetType("System.String");
        studentTagCount.ColumnName = "studentTagCount";
        dtResearch.Columns.Add(studentTagCount);

        DataColumn title = new DataColumn();
        title.DataType = System.Type.GetType("System.String");
        title.ColumnName = "title";
        dtResearch.Columns.Add(title);

        DataColumn uploadBy = new DataColumn();
        uploadBy.DataType = System.Type.GetType("System.String");
        uploadBy.ColumnName = "uploadBy";
        dtResearch.Columns.Add(uploadBy);

        DataColumn uploadDt = new DataColumn();
        uploadDt.DataType = System.Type.GetType("System.String");
        uploadDt.ColumnName = "uploadDt";
        dtResearch.Columns.Add(uploadDt);

        DataColumn weightage = new DataColumn();
        weightage.DataType = System.Type.GetType("System.String");
        weightage.ColumnName = "weightage";
        dtResearch.Columns.Add(weightage);

        DataColumn uploadByName = new DataColumn();
        uploadByName.DataType = System.Type.GetType("System.String");
        uploadByName.ColumnName = "uploadByName";
        dtResearch.Columns.Add(uploadByName);

        DataColumn tagCnt = new DataColumn();
        tagCnt.DataType = System.Type.GetType("System.String");
        tagCnt.ColumnName = "tagCnt";
        dtResearch.Columns.Add(tagCnt);

        DataColumn appellant = new DataColumn();
        appellant.DataType = System.Type.GetType("System.String");
        appellant.ColumnName = "appellant";
        dtResearch.Columns.Add(appellant);

        DataColumn court = new DataColumn();
        court.DataType = System.Type.GetType("System.String");
        court.ColumnName = "court";
        dtResearch.Columns.Add(court);

        DataColumn citation = new DataColumn();
        citation.DataType = System.Type.GetType("System.String");
        citation.ColumnName = "citation";
        dtResearch.Columns.Add(citation);

        DataColumn year = new DataColumn();
        year.DataType = System.Type.GetType("System.String");
        year.ColumnName = "year";
        dtResearch.Columns.Add(year);

        DataColumn docUid = new DataColumn();
        docUid.DataType = System.Type.GetType("System.String");
        docUid.ColumnName = "docUid";
        dtResearch.Columns.Add(docUid);

        DataColumn citedBy = new DataColumn();
        citedBy.DataType = System.Type.GetType("System.String");
        citedBy.ColumnName = "citedBy";
        dtResearch.Columns.Add(citedBy);

        DataColumn subject = new DataColumn();
        subject.DataType = System.Type.GetType("System.String");
        subject.ColumnName = "subject";
        dtResearch.Columns.Add(subject);

    }

    protected void btnSaveSearch_Click(object sender, EventArgs e)
    {
        if (ViewState["Searchresult"] != null)
        {
            divSavesearch.Style.Add("display", "block");
        }
    }

    protected void SearchRsltUrl()
    {
        APIURL = APIURL + "skorkelAdvanceSearch.action";
        ViewState["SortUrl"] = APIURL;
    }

    protected void lstSearchResult_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnDocId = (HiddenField)e.Item.FindControl("hdnDocId");
        HiddenField judgeNames = (HiddenField)e.Item.FindControl("judgeNames");
        Label lblPartyName = (Label)e.Item.FindControl("lblPartyName");
        Label lblCitation = (Label)e.Item.FindControl("lblCitation");
        Label lblCourt = (Label)e.Item.FindControl("lblCourt");
        Label lblYear = (Label)e.Item.FindControl("lblYear");
        Label lblCitedBy = (Label)e.Item.FindControl("lblCitedBy");
        if (e.CommandName == "NavigateToDoc")
        {
           Response.Redirect("Research-Case%20Details.aspx?CTid=1&cId=" + hdnDocId.Value);
        }

    }

    protected void ClearSearchData()
    {
        if (ddlSelect.SelectedItem.Text == "Free Text")
        {
            divMainSearch.Style.Add("display", "block");
            txtResearch.Style.Add("display", "block");
            btnSearch.Style.Add("display", "block");
            imgBtntarget.Style.Add("display", "none");
            divCitationSearch.Style.Add("display", "none");
            btnSearch.Style.Add("margin-top", "-35px");
            anchorAdvnceSearch.InnerText = "Hide";
            lbljudgename.Style.Add("margin-left", "7px");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "freeTextCall();", true);
        }
        else if (ddlSelect.SelectedItem.Text == "Skorkel")
        {
            divMainSearch.Style.Add("display", "block");
            txtResearch.Style.Add("display", "block");
            btnSearch.Style.Add("display", "block");
            imgBtntarget.Style.Add("display", "none");
            divCitationSearch.Style.Add("display", "none");
            btnSearch.Style.Add("margin-top", "-35px");
            anchorAdvnceSearch.InnerText = "Hide";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SkorkelCall();", true);
        }

    }

    /// <summary>
    /// Saved Search Calls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkPopupOK_Click(object sender, EventArgs e)
    {
        if (ViewState["Searchresult"] != null)
        {
            //SearchRsltUrl();
            objSaveMySearchDO.strSavedMyTitle = txtsaveTitle.Text.Trim();
            objSaveMySearchDO.strSearchQuery = Convert.ToString(ViewState["SortUrl"]);
            objSaveMySearchDO.intDocId = Convert.ToInt32(0);
            objSaveMySearchDO.intSearchResultCount = 0;
            if (ViewState["searchCount"] != null)
            {
                objSaveMySearchDO.intSearchResultCount = Convert.ToInt32(ViewState["searchCount"]);
            }
            objSaveMySearchDO.strSearchFor = Convert.ToString(ddlSelect.SelectedItem.Text);

            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objSaveMySearchDO.strIpAddress = ip;
            objSaveMySearchDO.intAddedBy = Convert.ToInt32(Convert.ToString(ViewState["UserID"]));

            objSaveMySearchDA.AddEditDel_Searchs(objSaveMySearchDO, DA_SaveMySearch.MySearch.Add);
            divSavesearch.Style.Add("display", "none");
        }
        else
        {

        }
    }

    protected void lnkMS_click(object sender, EventArgs e)
    {
        try
        {
            ViewState["sortCriteriya"] = "MS";
            lnkMS.Style.Add("color", "#3dbac0");
            lnkMD.Style.Add("color", "#ADADAD");
            lnkComment.Style.Add("color", "#ADADAD");
            lnkMR.Style.Add("color", "#ADADAD");

            DataTable dtSMS = new DataTable();
            dtSMS = (DataTable)ViewState["SearchResults"];

            dtSMS.DefaultView.Sort = "shareCount DESC";
            DataView dw = dtSMS.DefaultView;
            DataTable dtShare = dw.ToTable();
            ViewState["SearchResults"] = dtShare;

            int pageNum = 1;
            int pageSize = 10;
            DataTable dtPage = dtShare.Rows.Cast<System.Data.DataRow>().Skip((pageNum - 1) * pageSize).Take(pageSize).CopyToDataTable();
            lstSearchResult.DataSource = dtPage;
            lstSearchResult.DataBind();
            if (dtSMS.Rows.Count > pageSize)
            {
                pLoadMore.Style.Add("display", "block");
                lblNoMoreRslt.Visible = false;
            }
            else
            {
                pLoadMore.Style.Add("display", "none");
                lblNoMoreRslt.Visible = true;
            }
            dtSMS = new DataTable();
            ViewState["NewPageSize"] = null;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }

    protected void lnkMD_click(object sender, EventArgs e)
    {
        try
        {
            ViewState["sortCriteriya"] = "MD";
            lnkMS.Style.Add("color", "#ADADAD");
            lnkMD.Style.Add("color", "#3dbac0");
            lnkComment.Style.Add("color", "#ADADAD");
            lnkMR.Style.Add("color", "#ADADAD");
            DataTable dtSMS = new DataTable();
            dtSMS = (DataTable)ViewState["SearchResults"];

            dtSMS.DefaultView.Sort = "downloadCount DESC";
            DataView dw = dtSMS.DefaultView;
            DataTable dtShare = dw.ToTable();
            ViewState["SearchResults"] = dtShare;

            int pageNum = 1;
            int pageSize = 10;
            DataTable dtPage = dtShare.Rows.Cast<System.Data.DataRow>().Skip((pageNum - 1) * pageSize).Take(pageSize).CopyToDataTable();
            lstSearchResult.DataSource = dtPage;
            lstSearchResult.DataBind();
            if (dtSMS.Rows.Count > pageSize)
            {
                pLoadMore.Style.Add("display", "block");
                lblNoMoreRslt.Visible = false;
            }
            else
            {
                pLoadMore.Style.Add("display", "none");
                lblNoMoreRslt.Visible = true;
            }
            dtSMS = new DataTable();
            ViewState["NewPageSize"] = null;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }

    protected void lnkComment_click(object sender, EventArgs e)
    {
        try
        {
            ViewState["sortCriteriya"] = "MC";
            lnkMS.Style.Add("color", "#ADADAD");
            lnkMD.Style.Add("color", "#ADADAD");
            lnkComment.Style.Add("color", "#3dbac0");
            lnkMR.Style.Add("color", "#ADADAD");

            DataTable dtSMS = new DataTable();
            dtSMS = (DataTable)ViewState["SearchResults"];

            dtSMS.DefaultView.Sort = "commentCount DESC";
            DataView dw = dtSMS.DefaultView;
            DataTable dtShare = dw.ToTable();
            ViewState["SearchResults"] = dtShare;

            int pageNum = 1;
            int pageSize = 10;
            DataTable dtPage = dtShare.Rows.Cast<System.Data.DataRow>().Skip((pageNum - 1) * pageSize).Take(pageSize).CopyToDataTable();
            lstSearchResult.DataSource = dtPage;
            lstSearchResult.DataBind();
            if (dtSMS.Rows.Count > pageSize)
            {
                pLoadMore.Style.Add("display", "block");
                lblNoMoreRslt.Visible = false;
            }
            else
            {
                pLoadMore.Style.Add("display", "none");
                lblNoMoreRslt.Visible = true;
            }
            dtSMS = new DataTable();
            ViewState["NewPageSize"] = null;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void lnkMR_click(object sender, EventArgs e)
    {
        ViewState["sortCriteriya"] = "MR";
        lnkMS.Style.Add("color", "#ADADAD");
        lnkMD.Style.Add("color", "#ADADAD");
        lnkComment.Style.Add("color", "#ADADAD");
        lnkMR.Style.Add("color", "#3dbac0");
        pLoadMore.Style.Add("display", "none");
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex != 0)
        {
            DOobjResearch.SerchKey = ddlYear.SelectedValue;
            DataTable dtCit = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetCourtCi);
            if (dtCit.Rows.Count > 0)
            {
                ddlCourt.DataSource = dtCit;
                ddlCourt.DataTextField = "strEqCourt";
                ddlCourt.DataValueField = "intEqCourtId";
                ddlCourt.DataBind();
                ddlCourt.Items.Insert(0, "Court");

                DataTable dtCit5 = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetReportyearCi);
                if (dtCit5.Rows.Count > 0)
                {
                    ddlReporterName.DataSource = dtCit5;
                    ddlReporterName.DataTextField = "strReportName";
                    ddlReporterName.DataValueField = "intReportId";
                    ddlReporterName.DataBind();
                    ddlReporterName.Items.Insert(0, "Reporter Name");
                }
                else
                {
                    ddlReporterName.DataSource = null;
                }
            }
            else
            {
                ddlCourt.DataSource = null;
                DataTable dtCit1 = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetReportCi);
                if (dtCit1.Rows.Count > 0)
                {
                    ddlReporterName.DataSource = dtCit1;
                    ddlReporterName.DataTextField = "strReportName";
                    ddlReporterName.DataValueField = "intReportId";
                    ddlReporterName.DataBind();
                    ddlReporterName.Items.Insert(0, "Reporter Name");
                }
                else
                {
                    ddlReporterName.DataSource = null;
                }
            }
        }
    }

    protected void ddlCourt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCourt.SelectedIndex != 0)
        {
            DOobjResearch.SerchKey = ddlCourt.SelectedValue;
            DataTable dtCit = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetReportCi);
            if (dtCit.Rows.Count > 0)
            {
                ddlReporterName.DataSource = dtCit;
                ddlReporterName.DataTextField = "strReportName";
                ddlReporterName.DataValueField = "intReportId";
                ddlReporterName.DataBind();
                ddlReporterName.Items.Insert(0, "Reporter Name");
            }
            else
            {
                ddlReporterName.DataSource = null;
            }
        }
        else
        {
            DOobjResearch.SerchKey = ddlYear.SelectedValue;
            DataTable dtCit = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetReportyearsCi);
            if (dtCit.Rows.Count > 0)
            {
                ddlReporterName.DataSource = dtCit;
                ddlReporterName.DataTextField = "strReportName";
                ddlReporterName.DataValueField = "intReportId";
                ddlReporterName.DataBind();
                ddlReporterName.Items.Insert(0, "Reporter Name");
            }
            else
            {
                ddlReporterName.DataSource = null;
            }
        }

    }

    protected void ddlReporterName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReporterName.SelectedIndex != 0)
        {
            DOobjResearch.SerchKey = ddlReporterName.SelectedValue;
            DataTable dtCit = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetVolumeCi);
            if (dtCit.Rows.Count > 0)
            {
                ddlVolumns.DataSource = dtCit;
                ddlVolumns.DataTextField = "strEqVolume";
                ddlVolumns.DataValueField = "intVolumeId";
                ddlVolumns.DataBind();
                ddlVolumns.Items.Insert(0, "Volume");
            }
            else
            {
                ddlVolumns.DataSource = null;
                DOobjResearch.SerchKey = ddlYear.SelectedValue;
                DataTable dtCit2 = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetPagenoyearCi);
                if (dtCit2.Rows.Count > 0)
                {
                    ddlPageNo.DataSource = dtCit2;
                    ddlPageNo.DataTextField = "intPageNumber";
                    ddlPageNo.DataValueField = "intPageNo";
                    ddlPageNo.DataBind();
                    ddlPageNo.Items.Insert(0, "Page No.");
                }
                else
                {
                    ddlPageNo.DataSource = null;
                }
            }
        }

    }

    protected void ddlVolumns_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVolumns.SelectedIndex != 0)
        {
            DOobjResearch.SerchKey = ddlVolumns.SelectedValue;
            DataTable dtCit = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetPagenoCi);
            if (dtCit.Rows.Count > 0)
            {
                ddlPageNo.DataSource = dtCit;
                ddlPageNo.DataTextField = "intPageNumber";
                ddlPageNo.DataValueField = "intPageNo";
                ddlPageNo.DataBind();
                ddlPageNo.Items.Insert(0, "Page No.");
            }
            else
            {
                DOobjResearch.SerchKey = ddlYear.SelectedValue;
                DataTable dtCit3 = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetPagenoyearCi);
                if (dtCit3.Rows.Count > 0)
                {
                    ddlPageNo.DataSource = dtCit3;
                    ddlPageNo.DataTextField = "intPageNumber";
                    ddlPageNo.DataValueField = "intPageNo";
                    ddlPageNo.DataBind();
                    ddlPageNo.Items.Insert(0, "Page No.");
                }

                ddlPageNo.Items.Insert(0, "Page No.");
            }
        }

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string GetCourts(string yearId)
    {
        string JSONresult = "";
        DA_ResearchResult DAobjResearch = new DA_ResearchResult();
        DO_ResearchResult DOobjResearch = new DO_ResearchResult();

        DOobjResearch.SerchKey = yearId;
        DataTable dtCit = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetCourtCi);
        if (dtCit.Rows.Count > 0)
        {
            JSONresult = JsonConvert.SerializeObject(dtCit);
            return JSONresult;
        }
        else
        {
            DataTable dtCit5 = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetReportyearCi);
            if (dtCit5.Rows.Count > 0)
            {
                JSONresult = JsonConvert.SerializeObject(dtCit5);
                return JSONresult;
            }
        }

        return "";
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string GetRCourts(string yearId)
    {
        string JSONresult = "";
        DA_ResearchResult DAobjResearch = new DA_ResearchResult();
        DO_ResearchResult DOobjResearch = new DO_ResearchResult();

        DOobjResearch.SerchKey = yearId;
        DataTable dtCit5 = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetReportyearCi);
        if (dtCit5.Rows.Count > 0)
        {
            JSONresult = JsonConvert.SerializeObject(dtCit5);
        }
        return JSONresult;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string GetReports(string courtId, string yearId)
    {
        string JSONresult = "";
        DA_ResearchResult DAobjResearch = new DA_ResearchResult();
        DO_ResearchResult DOobjResearch = new DO_ResearchResult();
        DOobjResearch.SerchKey = courtId;
        DataTable dtCit = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetReportCi);
        if (dtCit.Rows.Count > 0)
        {
            JSONresult = JsonConvert.SerializeObject(dtCit);
            return JSONresult;
        }
        else
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            //Dictionary<string, object> row;
            DOobjResearch.SerchKey = yearId;
            DataTable dtCit5 = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetReportyearCi);
            if (dtCit5.Rows.Count > 0)
            {
                JSONresult = JsonConvert.SerializeObject(dtCit);
                return JSONresult;
            }
        }
        return JSONresult;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string GetVolumns(string reportId, string yearId)
    {
        string JSONresult = "";
        DA_ResearchResult DAobjResearch = new DA_ResearchResult();
        DO_ResearchResult DOobjResearch = new DO_ResearchResult();
        DOobjResearch.SerchKey = reportId;
        DataTable dtCit = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetVolumeCi);
        if (dtCit.Rows.Count > 0)
        {
            JSONresult = JsonConvert.SerializeObject(dtCit);
            return JSONresult;
        }
        else
        {
            DOobjResearch.SerchKey = yearId;
            DataTable dtCit2 = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetPagenoyearCi);
            if (dtCit2.Rows.Count > 0)
            {
                JSONresult = JsonConvert.SerializeObject(dtCit);
                return JSONresult;
            }

        }
        return JSONresult;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string GetPVolumns(string reportId, string yearId)
    {
        string JSONresult = "";
        DA_ResearchResult DAobjResearch = new DA_ResearchResult();
        DO_ResearchResult DOobjResearch = new DO_ResearchResult();
        DOobjResearch.SerchKey = yearId;
        DataTable dtCit5 = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetPagenoyearCi);
        if (dtCit5.Rows.Count > 0)
        {
            JSONresult = JsonConvert.SerializeObject(dtCit5);
        }

        return JSONresult;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string GetYears(string volumeId, string yearId)
    {
        string JSONresult = "";
        DA_ResearchResult DAobjResearch = new DA_ResearchResult();
        DO_ResearchResult DOobjResearch = new DO_ResearchResult();

        DOobjResearch.SerchKey = volumeId;
        DataTable dtCit = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetPagenoCi);
        if (dtCit.Rows.Count > 0)
        {
            JSONresult = JsonConvert.SerializeObject(dtCit);
            return JSONresult;
        }
        else
        {
            DOobjResearch.SerchKey = yearId;
            DataTable dtCit5 = DAobjResearch.GetDataTable(DOobjResearch, DA_ResearchResult.Research.GetPagenoyearCi);
            if (dtCit5.Rows.Count > 0)
            {
                JSONresult = JsonConvert.SerializeObject(dtCit);
                return JSONresult;
            }
        }

        return "";
    }

    
}



#region Classes
public class _responseJson1
{
    public responseJson responseJson { get; set; }
}

public class responseJson
{
    public string cursorId { get; set; }
    public searchResultDocumentSets[] searchResultDocumentSets;//{ get; set; }
    public string sortBy { get; set; }
    public int totalResultCount { get; set; }
    public string esTurnAroundTime { get; set; }
}

//{"responseJson":{"cursorId":"258026761","searchResultDocumentSets":[{

public class searchResultDocumentSets
{
    public string docType { get; set; }
    public documentes[] documentes { get; set; }
    public string searchCount; // { get; set; }
    public string bookmark { get; set; }
    public Int32 commentCount { get; set; }
    public string displayContent { get; set; }
    public Int32 docUid { get; set; }
    public Int32 downloadCount { get; set; }
    public string judgeTagCount { get; set; }
    public string likes { get; set; }
    public string profTagCount { get; set; }
    public string rating { get; set; }
    public string score { get; set; }
    public Int32 shareCount { get; set; }
    public string studentTagCount { get; set; }
    public string title { get; set; }
    public string uploadBy { get; set; }
    //public string uploaderName { get; set; }
    public string uploadDt { get; set; }
    public string weightage { get; set; }
    public string uploadByName { get; set; }
    public string tagCnt { get; set; }
    public string appellant { get; set; }
    public string court { get; set; }
    public string citation { get; set; }
    public string citedBy { get; set; }
    public string year { get; set; }
    public string subject { get; set; }
    public string ownerName { get; set; }
    public string judgeNames { get; set; }
}

public class documentes
{
    public string bookmark { get; set; }
    public Int32 commentCount { get; set; }
    public string displayContent { get; set; }
    public string docType { get; set; }
    public Int32 docUid { get; set; }
    public Int32 downloadCount { get; set; }
    public string judgeTagCount { get; set; }
    public string likes { get; set; }
    public string profTagCount { get; set; }
    public string rating { get; set; }
    public string score { get; set; }
    public Int32 shareCount { get; set; }
    public string studentTagCount { get; set; }
    public string title { get; set; }
    public string uploadBy { get; set; }
    //public string uploaderName { get; set; }
    public string uploadDt { get; set; }
    public string weightage { get; set; }
    public string uploadByName { get; set; }
    public string tagCnt { get; set; }
    public string appellant { get; set; }
    public string court { get; set; }
    public string citation { get; set; }
    public string citedBy { get; set; }
    public string year { get; set; }
    public string subject { get; set; }
    public string ownerName { get; set; }
    //public Int32 docUid { get; set; }
}
#endregion