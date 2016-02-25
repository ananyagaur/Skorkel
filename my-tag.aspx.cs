using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class my_tag : System.Web.UI.Page
{
    DO_CaseList objDoCaseList = new DO_CaseList();
    DA_CaseList objCaseListDb = new DA_CaseList();

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
            ViewState["TagTypeId"] = 1;
            BindDocumentsList();
        }
    }

    protected void BindDocumentsList()
    {
        objDoCaseList.Currentpage = Convert.ToInt32(hdnCurrentPage.Value);
        objDoCaseList.PageSize = Convert.ToInt32(hdnTotalItem.Value);
        objDoCaseList.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        objDoCaseList.intTagType = Convert.ToString(ViewState["TagTypeId"]);
        dt = objCaseListDb.GetDataTable(objDoCaseList, DA_CaseList.CaseList.GetMySkorkelNewTag);

        if (dt.Rows.Count > 0)
        {
            ViewState["MaxCount"] = dt.Rows[0]["MaxCount"];
            hdnMaxcount.Value = dt.Rows[0]["Maxcount"].ToString();
            divtag.Style.Add("height", "");
            lstMainMyTag.DataSource = dt;
            lstMainMyTag.DataBind();
        }
        else
        {
            divtag.Style.Add("height", "500px");
            lstMainMyTag.DataSource = "";
            lstMainMyTag.DataBind();
        }
    }

    protected void lstMainMyTag_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataTable dtMain = new DataTable();
        DataTable dtchild = new DataTable();
        HiddenField hdnId = (HiddenField)e.Item.FindControl("hdnId");
        HiddenField hdnCaseId = e.Item.FindControl("hdnCaseId") as HiddenField;
        Label lblTitle = (Label)e.Item.FindControl("lblTitle");
        Label lblDescription = (Label)e.Item.FindControl("lblDescription");
        Label lblTagName = (Label)e.Item.FindControl("lblTagName");

        if (lblTagName.Text == "Imp Paragraph")
        {
            lblTagName.Text = "Important Paragraph";
        }

        ListView lstChildMyTag = (ListView)e.Item.FindControl("lstChildMyTag");
        objDoCaseList.Caseid = Convert.ToInt32(hdnCaseId.Value);
        objDoCaseList.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        objDoCaseList.intTagType = Convert.ToString(ViewState["TagTypeId"]);
    }

    protected void lstMainMyTag_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnCaseId = (HiddenField)e.Item.FindControl("hdnCaseId");
        LinkButton lnkTitle = (LinkButton)e.Item.FindControl("lnkTitle");

        if (e.CommandName == "Title")
            Response.Redirect("~/Research-Case Details.aspx?CTid=" + hdnContentTypeID.Value.ToString() + "&cId=" + hdnCaseId.Value.ToString());
    }

    protected void lnkLink_OnClick(object sender, EventArgs e)
    {
        ViewState["TagTypeId"] = 1;
        BindDocumentsList();
    }

    protected void lnkDefination_OnClick(object sender, EventArgs e)
    {
        ViewState["TagTypeId"] = 2;
        BindDocumentsList();
    }

    protected void lnkImpParagraph_OnClick(object sender, EventArgs e)
    {
        ViewState["TagTypeId"] = 3;
        BindDocumentsList();
    }

    protected void lnkIssue_OnClick(object sender, EventArgs e)
    {
        ViewState["TagTypeId"] = 4;
        BindDocumentsList();
    }

    protected void lnkFact_OnClick(object sender, EventArgs e)
    {
        ViewState["TagTypeId"] = 5;
        BindDocumentsList();
    }

    protected void lnkJudgement_OnClick(object sender, EventArgs e)
    {
        ViewState["TagTypeId"] = 6;
        BindDocumentsList();
    }

    protected void lnkPrinciple_OnClick(object sender, EventArgs e)
    {
        ViewState["TagTypeId"] = 7;
        BindDocumentsList();
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
            BindDocumentsList();
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