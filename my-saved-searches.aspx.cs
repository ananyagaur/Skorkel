using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class my_saved_searches : System.Web.UI.Page
{
    DA_CaseList objdacase = new DA_CaseList();
    DO_CaseList objdocase = new DO_CaseList();

    DO_SaveMySearch objSaveMySearchDO = new DO_SaveMySearch();
    DA_SaveMySearch objSaveMySearchDA = new DA_SaveMySearch();

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
            BindMySavedSearches();
        }
    }

    protected void BindMySavedSearches()
    {
        objSaveMySearchDO.Currentpage = Convert.ToInt32(hdnCurrentPage.Value);
        objSaveMySearchDO.PageSize = Convert.ToInt32(hdnTotalItem.Value);
        objSaveMySearchDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objSaveMySearchDA.GetDataTable(objSaveMySearchDO, DA_SaveMySearch.MySearch.GetMySavedSearches);
        if (dt.Rows.Count > 0)
        {
            ViewState["MaxCount"] = dt.Rows[0]["MaxCount"];
            hdnMaxcount.Value = dt.Rows[0]["Maxcount"].ToString();
            if (dt.Rows.Count <= 3)
            {
                divSavedSearch.Style.Add("height", "500px");
            }
            else
            {
                divSavedSearch.Style.Add("height", "100%");
            }
            lstMySavedSearches.DataSource = dt;
            lstMySavedSearches.DataBind();
        }
        else
        {
            divSavedSearch.Style.Add("height", "500px");
            lstMySavedSearches.DataSource = null;
            lstMySavedSearches.DataBind();
        }
    }

    protected void lstMySavedSearches_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnContextId = (HiddenField)e.Item.FindControl("hdnContextId");
        HiddenField hdnDocumenttype = (HiddenField)e.Item.FindControl("hdnDocumenttype");
        HiddenField hdnstrSearchQuery = (HiddenField)e.Item.FindControl("hdnstrSearchQuery");
        if (e.CommandName == "SaveSearch")
        {
            Session["strSearchQuery"] = hdnstrSearchQuery.Value;
            Response.Redirect("Research_SearchResult_S.aspx");
        }
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
            BindMySavedSearches();
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