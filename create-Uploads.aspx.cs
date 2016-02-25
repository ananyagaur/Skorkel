using System;
using System.Data;
using DA_SKORKEL;
using System.Net.Mail;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.Xml;
using System.Web.UI.HtmlControls;

public partial class create_Uploads : System.Web.UI.Page
{
    DA_ProfileDocuments objDAProDocs = new DA_ProfileDocuments();
    DO_ProfileDocuments objDoProDocs = new DO_ProfileDocuments();
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

            if (Request.QueryString["GrpId"] != "" && Request.QueryString["GrpId"] != null)
            {
                ViewState["intGroupId"] = Request.QueryString["GrpId"];
            }
            //rbDocSaleYes.Enabled = false;
            //rbDocSaleNo.Enabled = false;
            //txtPrice.Visible = false;
            //lblInr.Visible = false;



        }
    }

    protected void rbDocSaleYes_CheckedChanged(object sender, EventArgs e)
    {
        // pnlPrice.Visible = true;
        txtPrice.Visible = true;
        lblInr.Visible = true;
    }

    protected void rbDocSaleNo_CheckedChanged(object sender, EventArgs e)
    {
        // pnlPrice.Visible = false;
        txtPrice.Visible = false;
        lblInr.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string docPath = "";
        if (FileUpload1.HasFile)
        {
            int FileLength = FileUpload1.PostedFile.ContentLength;
            string ext = System.IO.Path.GetExtension(this.FileUpload1.PostedFile.FileName);

            if (FileLength <= 3145728)
            {
                if (Session["Path"] != null)
                {
                    docPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(FileUpload1.FileName).ToString();
                    FileUpload1.SaveAs(Session["Path"] + docPath);
                }
                else
                {
                    docPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(FileUpload1.FileName).ToString();
                    FileUpload1.SaveAs(Server.MapPath("~\\GroupDocuments\\" + docPath));
                }
            }
            else
            {
                lblMessage.Text = "File size should be less than or equal to 3MB";
                lblMessage.CssClass = "RedErrormsg";
                return;
            }
            objDoProDocs.FilePath = docPath;
            objDoProDocs.DocTitle = FileUpload1.FileName;
            if (rbDocSaleYes.Checked == true)
            {
                objDoProDocs.IsDocsSale = "Y";
                if (txtPrice.Text != "Price")
                    objDoProDocs.Price = Convert.ToDouble(txtPrice.Text);
            }
            else
            {
                objDoProDocs.IsDocsSale = "N";
            }
            if (rdDownloadYes.Checked == true)
            {
                objDoProDocs.IsDocsDownload = "Y";
            }
            else
            {
                objDoProDocs.IsDocsDownload = "N";
            }
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDoProDocs.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objDoProDocs.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
            objDoProDocs.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objDoProDocs.StrDocsDetails = txtDocTitle.Text.Trim().Replace("'", "''");
            if (Session["Path"] != null)
            {
                if (Request.QueryString["FolderID"] != null)
                    objDoProDocs.intFolderId = Convert.ToInt32(Request.QueryString["FolderID"]);
                objDoProDocs.IsFolder = "Y";
                objDAProDocs.AddEditDel_GroupDocument(objDoProDocs, DA_ProfileDocuments.GropDocument.InsertGroupUploadDocs);
                Session["Path"] = null;
            }
            else
            {
                //if (Request.QueryString["Id"] != null)
                //    objDoProDocs.UploadDocId = Convert.ToInt32(Request.QueryString["Id"]);
                if (Request.QueryString["FolderID"] != null && Request.QueryString["FolderID"] !="")
                {
                    objDoProDocs.intFolderId = Convert.ToInt32(Request.QueryString["FolderID"]);
                    objDoProDocs.intParentId = Convert.ToInt32(Request.QueryString["FolderID"]);
                    objDoProDocs.IsFolder = "Y";
                }
                else
                {
                    //objDoProDocs.intFolderId = Convert.ToInt32(Request.QueryString["FolderID"]);
                    //objDoProDocs.intParentId = Convert.ToInt32(Request.QueryString["FolderID"]);
                    objDoProDocs.IsFolder = "N";
                }
                objDAProDocs.AddEditDel_GroupDocument(objDoProDocs, DA_ProfileDocuments.GropDocument.InsertGroupUploadDocs);
            }
            lblMessage.Text = "Document uploaded successfully.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            clear();
        }
        else
        {
            lblMessage.Text = "Please Select Document.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }



    }


    protected void clear()
    {
        txtDocTitle.Text = "";
        rbDocSaleYes.Enabled = false;
        rbDocSaleNo.Enabled = false;
        txtPrice.Text = "";
    }

    #region Tabs
    protected void lnkForumTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("forum-landing-page.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkUploadTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("uploads-docs-details.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkPollTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("Polls-Details.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkJobTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("jobs.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkEventTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("group-event-main.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkEventMemberTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("groups-members.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    #endregion
}