using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class my_bookmarks : System.Web.UI.Page
{
    DA_CaseList objdacase = new DA_CaseList();
    DO_CaseList objdocase = new DO_CaseList();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    DataTable dt = new DataTable();

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
            masterlbl.InnerText = "My Skorkel";
            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "10";
            BindMyBookMarks();
        }
    }

    protected void BindMyBookMarks()
    {
        DataTable dt = new DataTable();
        objdocase.Currentpage = Convert.ToInt32(hdnCurrentPage.Value);
        objdocase.PageSize = Convert.ToInt32(hdnTotalItem.Value);
        objdocase.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objdacase.GetMicroTagDataTable(objdocase, DA_CaseList.MicroTagLikeShare.GetMyBookMarks);

        if (dt.Rows.Count > 0)
        {
            ViewState["MaxCount"] = dt.Rows[0]["MaxCount"];
            hdnMaxcount.Value = dt.Rows[0]["Maxcount"].ToString();
            if (dt.Rows.Count > 4)
            {
                divbookmark.Style.Add("height", "");
            }
            else
            {
                divbookmark.Style.Add("height", "500px");
            }
            lstBookMark.DataSource = dt;
            lstBookMark.DataBind();            
        }
        else
        {
            divbookmark.Style.Add("height", "500px");
            lstBookMark.DataSource = "";
            lstBookMark.DataBind();            
        }
    }

    protected void lstBookMark_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnCaseID = (HiddenField)e.Item.FindControl("hdnCaseID");
        HiddenField hdnstrJudgeNames = (HiddenField)e.Item.FindControl("hdnstrJudgeNames");
        LinkButton lnkTitle=(LinkButton)e.Item.FindControl("lnkTitle");
        Label lblDescription = (Label)e.Item.FindControl("lblDescription");
        Label lbldate = (Label)e.Item.FindControl("lbldate");
        Label lblstrPartyNames = (Label)e.Item.FindControl("lblstrPartyNames");
        Label lblEq_Citations = (Label)e.Item.FindControl("lblEq_Citations");
        Label lblstrJurisdiction = (Label)e.Item.FindControl("lblstrJurisdiction");
        Label lblintYear = (Label)e.Item.FindControl("lblintYear");
        ViewState["hdnCaseID"] = Convert.ToString(hdnCaseID.Value);
        ViewState["lnkTitle"] = lnkTitle.Text.Trim();
        ViewState["lbldate"] = lbldate.Text.Trim();

        if (e.CommandName == "Title")
        {
            Session["hdnjudgeName"] = Convert.ToString(hdnstrJudgeNames.Value);
            Session["lblPartyName"] = Convert.ToString(lblstrPartyNames.Text);
            Session["lblCitation"] = Convert.ToString(lblEq_Citations.Text);
            Session["lblCourt"] = Convert.ToString(lblstrJurisdiction.Text);
            Session["lblYear"] = Convert.ToString(lblintYear.Text);

            Response.Redirect("~/Research-Case Details.aspx?CTid=" + hdnContentTypeID.Value.ToString() + "&cId=" + hdnCaseID.Value.ToString());
        }

        else if (e.CommandName == "Delete Bookmark")
        {
            divCancelPopup.Style.Add("display", "block");
            divSuccess.Style.Add("display", "none");
            lblConnDisconn.Text = "Do you want to remove book mark?";
        }
    }

    protected void lnkConnDisconn_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        objdocase.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objdocase.intDocId = Convert.ToInt32(ViewState["hdnCaseID"]);
        objdocase.ContentTitle = Convert.ToString(ViewState["lnkTitle"]);
        objdocase.strDescription = Convert.ToString(ViewState["lblDescription"]);
        objdocase.dtdate = Convert.ToString(ViewState["lbldate"]);
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        objdocase.strIpAddress = ip;
        objdacase.AddEditDel_MicroTagLikeShare(objdocase, DA_CaseList.MicroTagLikeShare.DeleteBookmark);
        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(ViewState["hdnCaseID"]);
        objLog.strAction = "Bookmark";
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 11;   // Bookmark Job
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
        BindMyBookMarks();
        divCancelPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "block");        
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
            BindMyBookMarks();
            count = NextPage - 10;
            if ((Convert.ToInt32(ViewState["nextValue"]) > Convert.ToInt32(ViewState["MaxCount"])))
            {
                pLoadMore.Style.Add("display", "none");
                lblNoMoreRslt.Visible = true;
            }
        }
        else
        {
            count = NextPage - 11;
            pLoadMore.Style.Add("display", "none");
            lblNoMoreRslt.Visible = true;

        }
    }


}