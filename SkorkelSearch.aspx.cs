using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using DA_SKORKEL;

public partial class SkorkelSearch : System.Web.UI.Page
{
    private const int _repeaterTotalColumns = 3;
    DO_Role ObjRole = new DO_Role();
    DA_Role ObjRoleDB = new DA_Role();

    DA_Case objdacase = new DA_Case();
    DO_Case objdocase = new DO_Case();
    DO_CaseList objDoCaseList = new DO_CaseList();
    DA_CaseList objCaseListDb = new DA_CaseList();

    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "10";
           // lnkAllCourt.CssClass = "active";
           // lnkAllyear.CssClass = "active";

            Session["SessionSearchCTId"] = "";
            Session.Add("SessionSearchCTId", 0);
          //  Tabindex.Value = "0";
            //lblContentTypeName.Text = "All";
           // spnYearDisplay.InnerText = "All Year";
           // spnCourtDisplay.InnerText = "All Court";
            MainSearchBind();
        }
    }
    public void MainSearchBind()
    {

        int year = 0;
        string CourtName = "";

        if (Convert.ToString(hdnYearMain.Value) != "")
            year = Convert.ToInt32(hdnYearMain.Value);
        if (Convert.ToString(hdnCourtMain.Value) != "")
            CourtName = Convert.ToString(hdnCourtMain.Value).Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim();
        Session.Add("CourtName", CourtName);// Session["CourtName"]
        autoCompleteSkorkelSearch obj = new autoCompleteSkorkelSearch();
        DataTable dtSearch = new DataTable();
        dtSearch = obj.GetSkorkelSearchData(Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(hdnTotalItem.Value), "", year, autoCompleteSkorkelSearch.Search.All);



        if (dtSearch.Rows.Count > 0)
        {
            // spnSearchCount.InnerText = " " + Convert.ToString(dtSearch.Rows[0]["Maxcount"]) + " ";
            lstSearchResult.DataSource = dtSearch;
            lstSearchResult.DataBind();
            dvPage.Visible = true;
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dtSearch.Rows[0]["Maxcount"]));
        }
        else
        {
            // spnSearchCount.InnerText = " 0 ";
            lstSearchResult.DataSource = null;
            lstSearchResult.DataBind();
            dvPage.Visible = false;
        }

        // SetColour();
    }
    protected void lstSearchResult_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnContentID = (HiddenField)e.Item.FindControl("hdnContentID");
        HiddenField hdnContentTypeID = (HiddenField)e.Item.FindControl("hdnContentTypeID");
        HtmlAnchor ancRedirect = (HtmlAnchor)e.Item.FindControl("ancRedirect");
        Label lblyearallList = (Label)e.Item.FindControl("lblyearallList");

        if (lblyearallList.Text == "0")
            lblyearallList.Text = "";
        DataTable dtSearch = new DataTable();
        if (hdnContentID != null && hdnContentTypeID != null)
        {
            string URL = Page.ResolveClientUrl("~/doc-view-links.aspx?CTid=" + hdnContentTypeID.Value.ToString() + "&cId=" + hdnContentID.Value.ToString());
            //string URL = Page.ResolveClientUrl("~/ContentList/Scrl_DetailTestPage.aspx?CTid=" + hdnContentTypeID.Value.ToString() + "&cId=" + hdnContentID.Value.ToString());
            ancRedirect.Attributes.Add("onclick", "mainrowredirect('" + URL + "')");
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
        MainSearchBind();
    }

    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = "1";
        MainSearchBind();
    }

    protected void lnkLast_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = hdnLastPage.Value;
        MainSearchBind();
    }

    protected void lnkPrevious_Click(object sender, EventArgs e)
    {

        hdnCurrentPage.Value = hdnPreviousPage.Value;
        MainSearchBind();
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
                MainSearchBind();
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
}