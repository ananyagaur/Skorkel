using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class my_documents : System.Web.UI.Page
{
    DA_ProfileDocuments objDAProDocs = new DA_ProfileDocuments();
    DO_ProfileDocuments objDoProDocs = new DO_ProfileDocuments();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    DataTable dt = new DataTable();
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["SubmitTime"] = Session["SubmitTime"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divDeletesucess.Style.Add("display", "none");
            Session["SubmitTime"] = DateTime.Now.ToString();
            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "My Skorkel";
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            BindDocsDetails();
            BindDocsSale();
            TotalDocSale();
            BindPublisDocs();
            TotalPublishDocs();
            TotalDocs();
        }

    }

    protected void BindDocsDetails()
    {
        objDoProDocs.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAProDocs.GetDataTable(objDoProDocs, DA_ProfileDocuments.DocumenTemp.GetMyDocDetailByAddedBy);
        if (dt.Rows.Count > 0)
        {
            LstDocument.DataSource = dt;
            LstDocument.DataBind();
        }
    }

    protected void LstDocument_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lblDateAddedOn = (Label)e.Item.FindControl("lblDateAddedOn");
        Label lblDateModOn = (Label)e.Item.FindControl("lblDateModOn");
        if (lblDateModOn.Text != null && lblDateModOn.Text != "")
        {
            lblDateModOn.Visible = true;
        }
        else
        {
            lblDateAddedOn.Visible = true;
        }
    }

    protected void BindDocsSale()
    {
        objDoProDocs.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAProDocs.GetDataTable(objDoProDocs, DA_ProfileDocuments.DocumenTemp.GetDocsSaleByAddedBy);
        if (dt.Rows.Count > 0)
        {
            lstDocSale.DataSource = dt;
            lstDocSale.DataBind();
        }
    }

    protected void TotalDocSale()
    {
        objDoProDocs.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAProDocs.GetDataTable(objDoProDocs, DA_ProfileDocuments.DocumenTemp.GetTotalDocSale);
        if (dt.Rows.Count > 0)
        {
            lblSale.Text = Convert.ToString(dt.Rows[0]["TotalDocSale"]);
        }
    }

    protected void BindPublisDocs()
    {
        objDoProDocs.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAProDocs.GetDataTable(objDoProDocs, DA_ProfileDocuments.DocumenTemp.GetPublishDocsByAddedBy);
        if (dt.Rows.Count > 0)
        {
            lstPublishDocs.DataSource = dt;
            lstPublishDocs.DataBind();
        }
    }

    protected void TotalPublishDocs()
    {
        objDoProDocs.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAProDocs.GetDataTable(objDoProDocs, DA_ProfileDocuments.DocumenTemp.GetTotalPublishDocs);
        if (dt.Rows.Count > 0)
        {
            lblPublishDocs.Text = Convert.ToString(dt.Rows[0]["TotalPublDocs"]);
        }
    }

    protected void TotalDocs()
    {
        objDoProDocs.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAProDocs.GetDataTable(objDoProDocs, DA_ProfileDocuments.DocumenTemp.GetTotalDocs);
        if (dt.Rows.Count > 0)
        {
            lblTotalDocs.Text = Convert.ToString(dt.Rows[0]["TotalDocsCount"]);
        }
    }

    protected void lnkFreeViewAll_OnClick(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        Response.Redirect("CreateProfile-Documents.aspx?updateid=1");
    }

    protected void lnkViewAll_OnClick(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        Response.Redirect("documents.aspx");
    }

    protected void LstDocument_ItemCommand(object source, ListViewCommandEventArgs e)
    {
        HiddenField hdnDocsId = (HiddenField)e.Item.FindControl("hdnDocsId");
        HiddenField hdnstrDocTitle = (HiddenField)e.Item.FindControl("hdnstrDocTitle");
        HiddenField hdnstrFilePath = (HiddenField)e.Item.FindControl("hdnstrFilePath");
        ViewState["hdnDocsId"] = hdnDocsId.Value;
        ViewState["hdnstrDocTitle"] = hdnstrDocTitle.Value;
        ViewState["hdnstrFilePath"] = hdnstrFilePath.Value;
        if (e.CommandName == "Deletedoc")
        {
            if (Session["SubmitTime"].ToString() == ViewState["SubmitTime"].ToString())
            {
                divDeletesucess.Style.Add("display", "block");
            }
        }
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        objDoProDocs.DocId = Convert.ToInt32(ViewState["hdnDocsId"]);
        objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.DeleteDocument);
        string PathPhysical = Server.MapPath("~/UploadDocument/" + Convert.ToString(ViewState["hdnstrFilePath"]));
        if (File.Exists(PathPhysical))
        {
            File.Delete(PathPhysical);
        }
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(ViewState["hdnDocsId"]);
        objLog.strAction = "Document";
        objLog.strActionName = Convert.ToString(ViewState["hdnstrDocTitle"]);
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 10;   // Document Job
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
        BindDocsDetails();
        TotalDocs();
        TotalDocSale();
        BindPublisDocs();
        Session["SubmitTime"] = DateTime.Now.ToString();
        divDeletesucess.Style.Add("display", "none");
    }


}