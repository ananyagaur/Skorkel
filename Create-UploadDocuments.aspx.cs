using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;


public partial class Create_UploadDocuments : System.Web.UI.Page
{
    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DA_Login objLoginDB = new DA_Login();
    DO_Login objLogin = new DO_Login();

    DA_ProfileDocuments objDAProDocs = new DA_ProfileDocuments();
    DO_ProfileDocuments objDoProDocs = new DO_ProfileDocuments();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();

    DataTable dt = new DataTable();
    int DocId;
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

            AccessModulePermisssion();

            if (Request.QueryString["first"] != "" && Request.QueryString["first"] != null)
            {
                lblFirst.Visible = true;
                lnkFirst.Visible = true;
                lnkFirst.Text = Convert.ToString(Request.QueryString["first"]);
            }

            if (Request.QueryString["second"] != "" && Request.QueryString["second"] != null)
            {
                lblSecond.Visible = true;
                lnkSecond.Visible = true;
                lnkSecond.Text = Convert.ToString(Request.QueryString["second"]);
            }

            if (Request.QueryString["third"] != "" && Request.QueryString["third"] != null)
            {
                lnkThird.Visible = true;
                lnkThird.Text = Convert.ToString(Request.QueryString["third"]);
            }

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Groups";
            BindTopSubjectList();
            GetDocumentsCategory();
            SubjectTempDataTable();
            DocId = Convert.ToInt32(Request.QueryString["DocId"]);
            GetDocsDetailsForEdit();
        }
    }

    protected void AccessModulePermisssion()
    {
        objDO.intUserId = Convert.ToInt32(ViewState["UserID"]);
        objDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objgrp.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        DataSet dschk = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.ViewGrpAssignUser);
        DataTable dtRoleAP = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpRoleRequestPermission);

        if (dschk.Tables[3].Rows.Count > 0)
        {
            if (dschk.Tables[3].Rows[0][0].ToString() == ViewState["UserID"].ToString())
            {
                DivHome.Style.Add("display", "block");
                DivForumTab.Style.Add("display", "block");
                DivUploadTab.Style.Add("display", "block");
                DivPollTab.Style.Add("display", "block");
                DivEventTab.Style.Add("display", "block");
                DivMemberTab.Style.Add("display", "block");
                return;
            }
        }

        if (dtRoleAP.Rows.Count > 0)
        {
            if (dtRoleAP.Rows[0]["IsAccepted"].ToString() != "0" && dtRoleAP.Rows[0]["IsAccepted"].ToString() != "2")
            {
                DivHome.Style.Add("display", "block");
                DivForumTab.Style.Add("display", "block");
                DivUploadTab.Style.Add("display", "block");
                DivPollTab.Style.Add("display", "block");
                DivEventTab.Style.Add("display", "block");
                DivMemberTab.Style.Add("display", "block");
            }
            else
            {
                Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);
            }
        }
        else
        {
            Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);
        }
    }

    protected void GetDocsDetailsForEdit()
    {
        objDoProDocs.UploadDocId = DocId;
        ViewState["DocIdEdit"] = objDoProDocs.UploadDocId;
        dt = objDAProDocs.GetGrouDocumetDataTable(objDoProDocs, DA_ProfileDocuments.GropDocument.EditDocsByDocId);
        if (dt.Rows.Count > 0)
        {
            lblfilenamee.Visible = true;
            lnkDelete.Style.Add("display", "block");
            upload.Visible = false;
            txtDocTitle.Text = Convert.ToString(dt.Rows[0]["strDocTitle"]);
            txtAuthors.InnerHtml = Convert.ToString(dt.Rows[0]["strAuthors"]);
            hdnUploadFile.Value = Convert.ToString(dt.Rows[0]["strFilePath"]);
            string imgPathPhysical = Server.MapPath("~/UploadDocument/" + hdnUploadFile.Value.ToString());
            if (File.Exists(imgPathPhysical))
            {
                lblfilenamee.Text = Convert.ToString(dt.Rows[0]["strDocName"]);
            }
            else
            {
                hdnUploadFile.Value = "";
                lblDocName.Text = "Uploaded file not Found.";
                lnkDelete.Style.Add("display", "none");
                upload.Visible = true;
            }

            if ((dt.Rows[0]["intDocsSee"]).ToString() != "")
            {
                if ((dt.Rows[0]["intDocsSee"]).ToString() != "Public")
                {                   
                    ddlintDocsSee.Items.FindByValue(dt.Rows[0]["intDocsSee"].ToString()).Selected = true;
                }
            }

            if ((dt.Rows[0]["intDocsSee"]).ToString() != "")
            {
                if ((dt.Rows[0]["intDocsSee"]).ToString() != "Public")
                {
                    ddlintDocsSee.Items.FindByValue(dt.Rows[0]["intDocsSee"].ToString()).Selected = true;
                }
            }

            DataTable dtDoc = new DataTable();
            objDoProDocs.UploadDocId = DocId;
            objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            if (dt.Rows[0]["intDocumentTypeID"].ToString() == "1")
            {
                ChkCases.Checked = true;
            }
            else if (dt.Rows[0]["intDocumentTypeID"].ToString() == "3")
            {
                ChkArticles.Checked = true;
            }
            else if (dt.Rows[0]["intDocumentTypeID"].ToString() == "4")
            {
                ChkNotes.Checked = true;
            } if (dt.Rows[0]["intDocumentTypeID"].ToString() == "5")
            {
                ChkOthers.Checked = true;
            }
            DataTable dtSub = new DataTable();
            objDoProDocs.UploadDocId = DocId;
            dtSub = objDAProDocs.GetGrouDocumetDataTable(objDoProDocs, DA_ProfileDocuments.GropDocument.EditSUbCatIdByDocsID);
            if (dtSub.Rows.Count > 0)
            {
                dt = objDAProDocs.GetGrouDocumetDataTable(objDoProDocs, DA_ProfileDocuments.GropDocument.GetSelectedValue);
                if (dt.Rows.Count > 0)
                {
                    ViewState["SaveSubCatname"] = dt;
                }
                lstSubjCategory.DataSource = dtSub;
                lstSubjCategory.DataBind();
            }
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
        objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.UpdateDocumentByDocId);
        lblDocName.Visible = false;
        lblfilenamee.Text = "";
        upload.Visible = true;
        try
        {
            string imgPathPhysical = Server.MapPath("~/UploadDocument/" + hdnUploadFile.Value.ToString());
            if (File.Exists(imgPathPhysical))
            {
                File.Delete(imgPathPhysical);
                lnkDelete.Style.Add("display", "none");
                hdnUploadFile.Value = "";
            }
           
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void BindTopSubjectList()
    {
        DataTable dtTopSub = new DataTable();
        dtTopSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.GetTopRecords);
        if (dtTopSub.Rows.Count > 0)
        {
            lstSubjCategory.DataSource = dtTopSub;
            lstSubjCategory.DataBind();
        }
        else
        {
            lstSubjCategory.DataSource = null;
            lstSubjCategory.DataBind();
        }
        ViewState["DocId"] = null;

    }

    protected void lnkViewSubj_Click(object sender, EventArgs e)
    {
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

    protected void HideShowSubject()
    {
        if (lnkViewSubj.Text == "View all")
        {
            BindTopSubjectList();
        }
        else
        {
            BindSubjectList();
        }
    }

    protected void SubjectTempDataTable()
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
        ViewState["SubjectCategory"] = dtSubjCat;
    }

    private void BindSubjectList()
    {
        DataTable dtSub = new DataTable();
        dtSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.AllRecord);
        if (dtSub.Rows.Count > 0)
        {
            lstSubjCategory.DataSource = dtSub;
            lstSubjCategory.DataBind();
        }
        else
        {
            lstSubjCategory.DataSource = null;
            lstSubjCategory.DataBind();
        }
    }

    #region AssignRole

    protected void GetAccessModuleDetails()
    {
        objgrp.intUserId = Convert.ToInt32(ViewState["UserID"]);
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        dt = objgrpDB.GetDataTable(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpModuleDetailsAcces);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string ModuleName = Convert.ToString(dt.Rows[i]["strModuleName"]);

                switch (ModuleName)
                {
                    case "Wall": DivHome.Style.Add("display", "block");
                        break;
                    case "Uploads": DivUploadTab.Style.Add("display", "block");
                        break;
                    case "Events": DivEventTab.Style.Add("display", "block");
                        break;
                    case "Discussion": DivForumTab.Style.Add("display", "block");
                        break;
                    case "Polls": DivPollTab.Style.Add("display", "block");
                        break;
                    case "Members": DivMemberTab.Style.Add("display", "block");
                        break;

                }
            }

        }

    }

    #endregion

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
                            SubLi.Style.Add("background", "#ebeaea");
                            SubLi.Style.Add("border", "1px solid #c8c8c8");
                            SubLi.Style.Add("color", "#646161");
                            lnkCatName.Style.Add("color", "#646161");
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
                        Subjectid = (Convert.ToInt32(dtSubjCat.Rows.Count) + hdnSubCatId.Value).ToString();
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
            }
        }

        objDAProDocs.AddEditDel_GroupUploadDocument(objDoProDocs, DA_ProfileDocuments.UploadDocument.AddTempSubCat);
    }

    protected void LstSubjCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnSubCatId = (HiddenField)e.Item.FindControl("hdnSubCatId");
        LinkButton lnkCatName = (LinkButton)e.Item.FindControl("lnkCatName");
        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
        DataTable dtsub = new DataTable();
        DataTable dtlast = new DataTable();
        HiddenField hdnCountSub = (HiddenField)e.Item.FindControl("hdnCountSub");

        SubLi.Style.Add("background", "#ebeaea");
        SubLi.Style.Add("border", "1px solid #c8c8c8");
        SubLi.Style.Add("color", "#646161");

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
        }


        if (ViewState["SaveSubCatname"] != null)
        {
            dtsub = (DataTable)ViewState["SaveSubCatname"];
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
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string docPath = "";
        string fname = hdnUploadFile.Value.ToString();
        if (fname != "")
        {
            ViewState["docPath"] = fname;
            docPath = fname;
            if (txtDocTitle.Text != "Title")
            {
                objDoProDocs.DocTitle = txtDocTitle.Text.Trim().Replace("'", "''");
            }
            objDoProDocs.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objDoProDocs.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
            objDoProDocs.FilePath = docPath;
            if (hdnOrgFileName.Value != "")
            {
                objDoProDocs.strDocName = hdnOrgFileName.Value;
            }
            else
            {
                objDoProDocs.strDocName = lblfilenamee.Text;
            }
            objDoProDocs.intDocsSee = ddlintDocsSee.SelectedValue;
            objDoProDocs.strAuthors = txtAuthors.InnerHtml.Trim().Replace("'", "''");
                objDoProDocs.IsDocsSale = "Y";
                objDoProDocs.IsDocsDownload = "Y";
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDoProDocs.IpAddress = Request.ServerVariables["REMOTE_ADDR"];

            if (ChkCases.Checked == true)
            {
                objDoProDocs.intDocumentTypeID = Convert.ToInt32(1);
            }
            else if (ChkArticles.Checked == true)
            {
                objDoProDocs.intDocumentTypeID = Convert.ToInt32(3);
            }
            else if (ChkNotes.Checked == true)
            {
                objDoProDocs.intDocumentTypeID = Convert.ToInt32(4);
            }
            else if (ChkOthers.Checked == true)
            {
                objDoProDocs.intDocumentTypeID = Convert.ToInt32(5);
            }

            objDoProDocs.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            if (ViewState["DocIdEdit"] != null && ViewState["DocIdEdit"].ToString().Trim() != "0")
            {
                objDoProDocs.UploadDocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                objDAProDocs.AddEditDel_GroupDocument(objDoProDocs, DA_ProfileDocuments.GropDocument.Update);
            }
            else
            {
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
                    if (Request.QueryString["FolderID"] != null && Request.QueryString["FolderID"] != "")
                    {
                        objDoProDocs.intFolderId = Convert.ToInt32(Request.QueryString["FolderID"]);
                        objDoProDocs.intParentId = Convert.ToInt32(Request.QueryString["FolderID"]);
                        objDoProDocs.IsFolder = "Y";
                    }
                    else
                    {
                        objDoProDocs.IsFolder = "N";
                    }
                    objDAProDocs.AddEditDel_GroupDocument(objDoProDocs, DA_ProfileDocuments.GropDocument.InsertGroupUploadDocs);
                    lblMessage.Text = "";
                }
            }

            ViewState["DocId"] = objDoProDocs.DocId;
            string totalSelectSubCat = Convert.ToString(ViewState["SubjectCategory"]);

            DataTable dtsub = new DataTable();
            dtsub = (DataTable)ViewState["SubjectCategory"];
            if (dtsub.Rows.Count > 0)
            {
                if (ViewState["DocIdEdit"] != null && ViewState["DocIdEdit"].ToString().Trim() != "0")
                {
                    objDoProDocs.UploadDocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    objDAProDocs.AddEditDel_GroupDocument(objDoProDocs, DA_ProfileDocuments.GropDocument.DeleteOldContext);
                }
            }
            foreach (DataRow dr in dtsub.Rows)
            {
                if (ViewState["DocIdEdit"] != null && ViewState["DocIdEdit"].ToString().Trim() != "0")
                {
                    objDoProDocs.intGroupDocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                }
                else
                {
                    objDoProDocs.intGroupDocId = Convert.ToInt32(ViewState["DocId"]);
                }
                objDoProDocs.intSubjectCategoryId = Convert.ToInt32(dr["intCategoryId"].ToString());
                objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objDAProDocs.AddEditDel_GroupUploadDocument(objDoProDocs, DA_ProfileDocuments.UploadDocument.ADDSelectedSubCatId);
            }

            BindTopSubjectList();
            clear();
            divDocsSuccess.Style.Add("display", "block");

        }
        else if (upload.HasFile)
        {
            int FileLength = upload.PostedFile.ContentLength;
            string ext = System.IO.Path.GetExtension(this.upload.PostedFile.FileName);

            if (ext.Trim() == ".jpg" || ext.Trim() == ".jpeg" || ext.Trim() == ".png" || ext.Trim() == ".gif")
            {
                lblMessage.Text = "File format not supported.";
                lblMessage.CssClass = "RedErrormsg";
                return;
            }
            if (FileLength <= 3145728)
            {
                docPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(upload.FileName).ToString();
                upload.SaveAs(Server.MapPath("~\\UploadDocument\\" + docPath));
                ViewState["docPath"] = docPath;
            }
            else
            {
                lblMessage.Text = "File size should be less than or equal to 3MB";
                lblMessage.CssClass = "RedErrormsg";
                return;
            }

            if (txtDocTitle.Text != "Title")
            {
                objDoProDocs.DocTitle = txtDocTitle.Text.Trim().Replace("'", "''");
            }
            objDoProDocs.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objDoProDocs.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
            objDoProDocs.FilePath = docPath;
            objDoProDocs.strDocName = upload.FileName;
            if (ddlintDocsSee.SelectedValue == "")
            {
                objDoProDocs.intDocsSee = "Public";
            }
            else
            {
                objDoProDocs.intDocsSee = ddlintDocsSee.SelectedValue;
            }
            objDoProDocs.strAuthors = txtAuthors.InnerHtml.Trim().Replace("'", "''");
                objDoProDocs.IsDocsSale = "Y";
                objDoProDocs.IsDocsDownload = "Y";
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDoProDocs.IpAddress = Request.ServerVariables["REMOTE_ADDR"];

            if (ChkCases.Checked == true)
            {
                objDoProDocs.intDocumentTypeID = Convert.ToInt32(1);
            }
            else if (ChkArticles.Checked == true)
            {
                objDoProDocs.intDocumentTypeID = Convert.ToInt32(3);
            }
            else if (ChkNotes.Checked == true)
            {
                objDoProDocs.intDocumentTypeID = Convert.ToInt32(4);
            }
            else if (ChkOthers.Checked == true)
            {
                objDoProDocs.intDocumentTypeID = Convert.ToInt32(5);
            }

            objDoProDocs.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            if (ViewState["DocIdEdit"] != null && ViewState["DocIdEdit"].ToString().Trim() != "0")
            {
                objDoProDocs.UploadDocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                objDAProDocs.AddEditDel_GroupDocument(objDoProDocs, DA_ProfileDocuments.GropDocument.Update);
            }
            else
            {
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
                    if (Request.QueryString["FolderID"] != null && Request.QueryString["FolderID"] != "")
                    {
                        objDoProDocs.intFolderId = Convert.ToInt32(Request.QueryString["FolderID"]);
                        objDoProDocs.intParentId = Convert.ToInt32(Request.QueryString["FolderID"]);
                        objDoProDocs.IsFolder = "Y";
                    }
                    else
                    {
                        objDoProDocs.IsFolder = "N";
                    }
                    objDAProDocs.AddEditDel_GroupDocument(objDoProDocs, DA_ProfileDocuments.GropDocument.InsertGroupUploadDocs);
                    lblMessage.Text = "";
                }
            }

            ViewState["DocId"] = objDoProDocs.DocId;
            string totalSelectSubCat = Convert.ToString(ViewState["SubjectCategory"]);

            DataTable dtsub = new DataTable();
            dtsub = (DataTable)ViewState["SubjectCategory"];
            if (dtsub.Rows.Count > 0)
            {
                if (ViewState["DocIdEdit"] != null && ViewState["DocIdEdit"].ToString().Trim() != "0")
                {
                    objDoProDocs.UploadDocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    objDAProDocs.AddEditDel_GroupDocument(objDoProDocs, DA_ProfileDocuments.GropDocument.DeleteOldContext);
                }
            }
            foreach (DataRow dr in dtsub.Rows)
            {
                if (ViewState["DocIdEdit"] != null && ViewState["DocIdEdit"].ToString().Trim() != "0")
                {
                    objDoProDocs.intGroupDocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                }
                else
                {
                    objDoProDocs.intGroupDocId = Convert.ToInt32(ViewState["DocId"]);
                }
                objDoProDocs.intSubjectCategoryId = Convert.ToInt32(dr["intCategoryId"].ToString());
                objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objDAProDocs.AddEditDel_GroupUploadDocument(objDoProDocs, DA_ProfileDocuments.UploadDocument.ADDSelectedSubCatId);
            }

            BindTopSubjectList();
            clear();
            divDocsSuccess.Style.Add("display", "block");
        }
        else
        {
            lblMessage.Text = "Please Select Document.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
        objDAProDocs.AddEditDel_GroupDocument(objDoProDocs, DA_ProfileDocuments.GropDocument.DeleteTempTable);
        Response.Redirect("uploads-docs-details.aspx?GrpId=" + ViewState["intGroupId"] + "");

    }

    protected void GetDocumentsCategory()
    {
        DataTable dtDoc = new DataTable();

        dtDoc = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.AllRecordDocument);
        if (dtDoc.Rows.Count > 0)
        {
            ddlDocumentType.DataSource = dtDoc;
            ddlDocumentType.DataValueField = "intCategoryId";
            ddlDocumentType.DataTextField = "strCategoryName";
            ddlDocumentType.DataBind();
        }
    }

    protected void clear()
    {
        ViewState["SubjectCategory"] = null;
        SubjectTempDataTable();
        BindTopSubjectList();
        txtDocTitle.Text = "";
        txtAuthors.InnerHtml = "";
        ddlintDocsSee.ClearSelection();
        ddlDocumentType.ClearSelection();
        hdnUploadFile.Value = "";
        ChkCases.Checked = false;
        ChkArticles.Checked = false;
        ChkNotes.Checked = false;
        ChkOthers.Checked = false;
        lblfilenamee.Text = "";
        lnkDelete.Visible = false;
        upload.Visible = true;
    }
    
    protected void lnkAllUpload_Click(object sender, EventArgs e)
    {
        Response.Redirect("uploads-docs-details.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    #region Tabs

    protected void lnkProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);
    }
    protected void lnkHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Group-Home.aspx?GrpId=" + ViewState["intGroupId"]);
    }
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
    protected void lnkEventMemberTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("groups-members.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkJobTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("jobs.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkEventTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("group-event-main.aspx?GrpId=" + ViewState["intGroupId"]);
    }
    protected void lnkMemberTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("Create-UploadDocuments.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    #endregion

    }