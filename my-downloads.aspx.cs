using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class my_downloads : System.Web.UI.Page
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
            BindTop4FreeDownloadDetails();
        }
    }

    protected void BindTop4FreeDownloadDetails()
    {
        DataTable dt = new DataTable();
        objdocase.intAddedBy = Convert.ToInt32(ViewState["UserID"]);

        dt = objdacase.GetMicroTagDataTable(objdocase, DA_CaseList.MicroTagLikeShare.GetMyFreeTop4Downloads);

        if (dt.Rows.Count > 0)
        {
            lstFreeDownload.DataSource = dt;
            lstFreeDownload.DataBind();
            lblFreeDownload.Text = "Free Download" + " " + "(" + Convert.ToString(dt.Rows.Count) + ")";
        }
        else
        {
           // divdownload.Style.Add("height", "600px");
            lstFreeDownload.DataSource = "";
            lstFreeDownload.DataBind();
            lblFreeDownload.Text = "Free Download" + " " + "(" + Convert.ToString(dt.Rows.Count) + ")";
        }
        
    }

    protected void BindFreeDownloadDetails()
    {
        DataTable dt = new DataTable();
        objdocase.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (lnkFreeViewAll.Text == "View all")
            dt = objdacase.GetMicroTagDataTable(objdocase, DA_CaseList.MicroTagLikeShare.GetMyFreeTop4Downloads);
        else
            dt = objdacase.GetMicroTagDataTable(objdocase, DA_CaseList.MicroTagLikeShare.GetMyFreeDownloads);

        if (dt.Rows.Count > 0)
        {
            lstFreeDownload.DataSource = dt;
            lstFreeDownload.DataBind();
            lblFreeDownload.Text = "Free Download" + " " + "(" + Convert.ToString(dt.Rows.Count) + ")";
        }
        else
        {
            divdownload.Style.Add("height", "600px");
            lstFreeDownload.DataSource = "";
            lstFreeDownload.DataBind();
            lblFreeDownload.Text = "Free Download" + " " + "(" + Convert.ToString(dt.Rows.Count) + ")";
        }
    }

    protected void lnkFreeViewAll_OnClick(object sender, EventArgs e)
    {
        if (lnkFreeViewAll.Text == "View all")
            lnkFreeViewAll.Text = "Top 4";
        else
            lnkFreeViewAll.Text = "View all";
        BindFreeDownloadDetails();
    }

    protected void lstFreeDownload_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnCaseID = (HiddenField)e.Item.FindControl("hdnCaseID");
        LinkButton lnkTitle = (LinkButton)e.Item.FindControl("lnkTitle");
        Label lblDescription = (Label)e.Item.FindControl("lblDescription");
        Label lblAuthor = (Label)e.Item.FindControl("lblAuthor");
        Label lblPublisher = (Label)e.Item.FindControl("lblPublisher");
        Label lbldate = (Label)e.Item.FindControl("lbldate");

        ViewState["hdnCaseID"] = Convert.ToString(hdnCaseID.Value);
        ViewState["lnkTitle"] = lnkTitle.Text.Trim();
        ViewState["lblDescription"] = lblDescription.Text.Trim();

        if (e.CommandName == "Title")
            Response.Redirect("~/doc-view-links.aspx?CTid=" + hdnContentTypeID.Value.ToString() + "&cId=" + hdnCaseID.Value.ToString());

        else if (e.CommandName == "Delete Download")
        {
            divCancelPopup.Style.Add("display", "block");
            divSuccess.Style.Add("display", "none");
            lblConnDisconn.Text = "Do you want to remove download document?";
        }
    }

    protected void lnkConnDisconn_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        objdocase.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objdocase.intDocId = Convert.ToInt32(ViewState["hdnCaseID"]);
        objdocase.ContentTitle = Convert.ToString(ViewState["lnkTitle"]);
        objdocase.strDescription = Convert.ToString(ViewState["lblDescription"]);
        objdocase.strAuthor = Convert.ToString(ViewState["lblAuthor"]);
        objdocase.strPublisher = Convert.ToString(ViewState["lblPublisher"]);
        objdocase.dtdate = Convert.ToString(ViewState["lbldate"]);
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        objdocase.strIpAddress = ip;
        objdacase.AddEditDel_MicroTagLikeShare(objdocase, DA_CaseList.MicroTagLikeShare.DeleteDownloadedDoc);

        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(ViewState["hdnCaseID"]);
        objLog.strAction = "MyDownload";
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 12;   // MyDownload 
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);

        BindFreeDownloadDetails();
        divCancelPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "block");
    }
}