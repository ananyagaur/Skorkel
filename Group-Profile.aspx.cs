using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI;

public partial class Group_Profile : System.Web.UI.Page
{

    #region #Memebers

    DataTable dt = new DataTable();

    DO_WallMessage WallMessageDO = new DO_WallMessage();
    DA_WallMessage WallMessageDA = new DA_WallMessage();

    DA_Profile ObjDAprofile = new DA_Profile();
    DO_Profile objDoProfile = new DO_Profile();

    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_UserGroupJoin objGrpJoinDO = new DO_Scrl_UserGroupJoin();
    DA_Scrl_UserGroupJoin objGrpJoinDA = new DA_Scrl_UserGroupJoin();

    DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_UserGroupDetailTbl objDOgroup = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDAgroup = new DA_Scrl_UserGroupDetailTbl();

    DataTable dtUpdate = new DataTable();
    int Action = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //to get the browser name from server side.
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        ViewState["BrowserName"] = browser.Browser;

        if (!IsPostBack)
        {

            Session["SubmitTime"] = DateTime.Now.ToString();
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Groups";

            if (Request.QueryString["GrpId"] != "" && Request.QueryString["GrpId"] != null)
            {
                ViewState["intGroupId"] = Request.QueryString["GrpId"];
            }
            if ((Convert.ToString(Request.QueryString["RegId"]) != Convert.ToString(Session["ExternalUserId"])))
            {
                if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
                {
                    ViewState["RegId"] = Request.QueryString["RegId"];
                    ViewState["UserID"] = Request.QueryString["RegId"];
                }
            }

            divMainProfile.Visible = true;
            divProfileTitle.Visible = false;
            BindSubjectList();
            GenaralGroupDetails();
            lnkCreateGroup.Visible = false;
            btnCancelExperience.Visible = false;
            AccessModulePermisssion();
            if (Request.QueryString["EditId"] != "" && Request.QueryString["EditId"] != null)
            {
                divMainProfile.Visible = false;
                divProfileTitle.Visible = true;
                lnkEdit.Visible = false;
                getInviteeName();
                getInviteNameEdit();
                divGroupSucces.Style.Add("display", "none");
                lnkViewSubj.Visible = true;
                divSaveCancel.Visible = true;
                txtTitle.Enabled = true;
                txtPurpose.Visible = true;
                txtPurpose.Disabled = false;
                lnkCreateGroup.Visible = true;
                btnCancelExperience.Visible = true;
                BindEditSubjectList();
                BindTopSubjectList();
                if (Request.QueryString["NavigationId"] == "A")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#Membersection';", true);
                }
            }
        }

    }

    #region AssignRole

    protected void AccessModulePermisssion()
    {

        objDO.intUserId = Convert.ToInt32(ViewState["UserID"]);
        objDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objgrp.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        DataSet dschk = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.ViewGrpAssignUser);
        DataTable dtRoleAP = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpRoleRequestPermission);

        if (dschk.Tables[3].Rows.Count > 0)
        {
            if (dschk.Tables[3].Rows[0][0].ToString() == Convert.ToString(ViewState["UserID"]))
            {
                divSecondWall.Style.Add("display", "block");
                DivHome.Style.Add("display", "block");
                DivForumTab.Style.Add("display", "block");
                DivUploadTab.Style.Add("display", "block");
                DivPollTab.Style.Add("display", "block");
                DivEventTab.Style.Add("display", "block");
                DivMemberTab.Style.Add("display", "block");
            }
        }

        if (dtRoleAP.Rows.Count > 0)
        {
            if (dtRoleAP.Rows[0]["IsAccepted"].ToString() != "0" && dtRoleAP.Rows[0]["IsAccepted"].ToString() != "2")
            {
                divSecondWall.Style.Add("display", "block");
                DivHome.Style.Add("display", "block");
                DivForumTab.Style.Add("display", "block");
                DivUploadTab.Style.Add("display", "block");
                DivPollTab.Style.Add("display", "block");
                DivEventTab.Style.Add("display", "block");
                DivMemberTab.Style.Add("display", "block");
            }
        }


    }

    public void GetAssignRole()
    {
        if (ViewState["intGroupId"] != null)
        {
            objgrp.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
            objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            DataSet dschk = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.ViewGrpAssignUser);
            string Status = string.Empty;

            DataTable dtGrpOpt = objgrpDB.GetDataTable(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.ChkGroupOption);

            if (Convert.ToString(dtGrpOpt.Rows[0]["strAccess"]) == "A")
            {
                GetAccessModuleDetails();

                if (Convert.ToString(dschk.Tables[0].Rows[0][0]) == "0")
                {
                    lnkEdit.Visible = false;
                }
                else { lnkEdit.Visible = true; }

                if (dschk.Tables[3].Rows.Count > 0)
                {
                    if (Convert.ToString(dschk.Tables[3].Rows[0][0]) == Convert.ToString(ViewState["UserID"]))
                    {
                        divSecondWall.Style.Add("display", "block");
                        DivHome.Style.Add("display", "block");
                        DivForumTab.Style.Add("display", "block");
                        DivUploadTab.Style.Add("display", "block");
                        DivPollTab.Style.Add("display", "block");
                        DivEventTab.Style.Add("display", "block");
                        DivMemberTab.Style.Add("display", "block");
                    }
                }
            }
            else
            {
                if (Convert.ToString(dschk.Tables[0].Rows[0][0]) != "0")
                {
                    divSecondWall.Style.Add("display", "block");
                    DivHome.Style.Add("display", "block");
                    DivForumTab.Style.Add("display", "block");
                    DivUploadTab.Style.Add("display", "block");
                    DivPollTab.Style.Add("display", "block");
                    DivEventTab.Style.Add("display", "block");
                    DivMemberTab.Style.Add("display", "block");

                }
                else
                {
                    lnkEdit.Visible = false;
                    divSecondWall.Style.Add("display", "block");
                    GetAccessModuleDetails();
                }
            }
        }
    }

    protected void GetAccessModuleDetails()
    {
        objDO.intUserId = Convert.ToInt32(ViewState["UserID"]);
        objDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        DataTable dtRoleAP = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpRoleRequestPermission);
        dt = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpModuleDetailsAcces);
        //Profile,Home=Wall,Uploads=Uploads,Events=Events,Discussion=Forum,Polls=Polls,Jobs=Jobs,Members=Members
        if (dtRoleAP.Rows.Count > 0)
        {
            if (dtRoleAP.Rows[0]["IsAccepted"].ToString() != "0" && dtRoleAP.Rows[0]["IsAccepted"].ToString() != "2")
            {
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
                    divheight.Style.Add("height", "Auto");
                }
            }
            else
            {
                divSecondWall.Style.Add("display", "none");
                DivHome.Style.Add("display", "none");
                DivForumTab.Style.Add("display", "none");
                DivUploadTab.Style.Add("display", "none");
                DivPollTab.Style.Add("display", "none");
                DivEventTab.Style.Add("display", "none");
                DivMemberTab.Style.Add("display", "none");
                divheight.Style.Add("height", "500px");
            }
        }
        else
        {
            divSecondWall.Style.Add("display", "none");
            DivHome.Style.Add("display", "none");
            DivForumTab.Style.Add("display", "none");
            DivUploadTab.Style.Add("display", "none");
            DivPollTab.Style.Add("display", "none");
            DivEventTab.Style.Add("display", "none");
            DivMemberTab.Style.Add("display", "none");
            divheight.Style.Add("height", "500px");
        }


    }

    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["SubmitTime"] = Session["SubmitTime"];
    }

    #region #Function

    /// <summary>
    /// Bind Subject Category From the table Scrl_Category_SubjectTbl
    /// </summary>
    private void BindSubjectList()
    {

        DataTable dtSub = new DataTable();
        DataSet ds = new DataSet();
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);

        dtSub = objgrpDB.GetDataTable(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetContextByGroupID);
        if (dtSub.Rows.Count > 0)
        {
            LstSubjCategory.DataSource = dtSub;
            LstSubjCategory.DataBind();
            LstProfSubjCategory.DataSource = dtSub;
            LstProfSubjCategory.DataBind();
        }
        else
        {
            LstSubjCategory.DataSource = null;
            LstSubjCategory.DataBind();
            LstProfSubjCategory.DataSource = null;
            LstProfSubjCategory.DataBind();
        }
    }

    private void BindEditSubjectList()
    {
        DataTable dtSub = new DataTable();
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        dtSub = objgrpDB.GetDataTable(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetContextByGroupID);
        ViewState["SaveSubCatname"] = dtSub;
    }

    private void BindTopSubjectList()
    {
        DataTable dtTopSub = new DataTable();
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        dtTopSub = objgrpDB.GetDataTable(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetAllSelectedContextID);
        if (dtTopSub.Rows.Count > 0)
        {
            LstSubjCategory.DataSource = dtTopSub;
            LstSubjCategory.DataBind();
        }
        else
        {
            LstSubjCategory.DataSource = null;
            LstSubjCategory.DataBind();
        }
        ViewState["DocId"] = null;
        ViewState["SubjectCategoryId"] = null;
    }

    protected void GenaralGroupDetails()
    {
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);

        dt = objgrpDB.GetDataTable(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGeneralGroupDetaiils);
        if (dt.Rows.Count > 0)
        {
            ViewState["strUniqueKey"] = Convert.ToString(dt.Rows[0]["strUniqueKey"]);
            txtTitle.Text = Convert.ToString(dt.Rows[0]["strGroupName"]);
            hdnTitle.Value = Convert.ToString(dt.Rows[0]["strGroupName"]);
            txtPurpose.InnerText = Convert.ToString(dt.Rows[0]["strSummary"]);
            if (dt.Rows[0]["strAccess"].ToString() == "A")
            {
                ViewState["join"] = "A";
                imgAutojoin.Attributes.Add("class", "chkEnabled");
                imgReqjoin.Attributes.Add("class", "chkDisabled");
            }
            else
            {
                ViewState["join"] = "R";
                imgAutojoin.Attributes.Add("class", "chkDisabled");
                imgReqjoin.Attributes.Add("class", "chkEnabled");
            }

            lnkViewSubj.Visible = false;

            lblTitle.Text = Convert.ToString(dt.Rows[0]["strGroupName"]);
            if (Convert.ToString(dt.Rows[0]["strSummary"]) != "")
            {
                lblDescription.Text = Convert.ToString(dt.Rows[0]["strSummary"]);

            }
            else
            {
                lblDescription.Text = " &nbsp;";
            }

            if (Convert.ToString((dt.Rows[0]["strAccess"])) == "R")
            {
                rbProfileJoin.Text = "Request to Join";
            }
            else
            {
                rbProfileJoin.Text = "Auto Join";
            }

            if ((dt.Rows[0]["strLogoPath"]).ToString() != "")
            {
                imgUser.Src = "~/CroppedPhoto/" + Convert.ToString((dt.Rows[0]["strLogoPath"]));
                Session["ImagePath"] = Convert.ToString((dt.Rows[0]["strLogoPath"]));
            }
            else
            {
                imgUser.Src = "~/images/groupPhoto.jpg";
                ImgRemovePic.Visible = false;
            }

        }
    }

    protected void HideShowSubject()
    {
        if (lnkViewSubj.Text == "View all")
        {
            if (Action == 1)
            {
                BindSubjectList();
            }
            else
            {
                BindTopSubjectList();
            }

        }
        else
        {
            BindTopSubjectList();

        }
    }
    #endregion

    #region Tabs

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

    protected void lnkViewSubj_Click(object sender, EventArgs e)
    {

        if (lnkViewSubj.Text == "Close")
        {
            lnkViewSubj.Text = "View all";
            Action = 1;

        }
        else
        {
            lnkViewSubj.Text = "Close";
        }
        HideShowSubject();
    }

    /// <summary>
    /// Command fire for Subject Selection
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
            if (ViewState["SubjectCategory"] != null)
            {
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
                                //if (Convert.ToInt32(ViewState["MId"]) > 0)
                                dtSubjCat.Rows.Remove(dtContent.Rows[i]);
                                ViewState["SubjectCategory"] = dtSubjCat;
                                SubLi.Style.Add("background", "#ebeaea");
                                SubLi.Style.Add("border", "1px solid #c8c8c8");
                                SubLi.Style.Add("color", "#646161");
                                lnkCatName.Style.Add("color", "#646161");
                                //SubLi.Attributes.Add("class", "gray");
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
        }


    }

    /// <summary>
    /// Subject Category Bind from table Scrl_Category_SubjectTbl
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LstSubjCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnSubCatId = (HiddenField)e.Item.FindControl("hdnSubCatId");
        LinkButton lnkCatName = (LinkButton)e.Item.FindControl("lnkCatName");
        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
        DataTable dtSub = new DataTable();
        DataTable dtlast = new DataTable();
        HiddenField hdnCountSub = (HiddenField)e.Item.FindControl("hdnCountSub");

        if (hdnCountSub.Value == "1")
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
            //SubLi.Attributes.Add("class", "gray");
        }

        dtSub = (DataTable)ViewState["SaveSubCatname"];
        ViewState["SubjectCategory"] = dtSub;


    }

    protected void lnkProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        divMainProfile.Visible = false;
        divProfileTitle.Visible = true;
        lnkEdit.Visible = false;
        getInviteeName();
        getInviteNameEdit();

        divGroupSucces.Style.Add("display", "none");
        lnkViewSubj.Visible = true;
        divSaveCancel.Visible = true;
        txtTitle.Enabled = true;
        txtPurpose.Visible = true;
        txtPurpose.Disabled = false;
        lnkCreateGroup.Visible = true;
        btnCancelExperience.Visible = true;
        BindEditSubjectList();
        BindTopSubjectList();
    }

    protected void btnSaveGroup_Click(object sender, EventArgs e)
    {
        try
        {
            lblSuccMessage.Text = "";
            String GroupeAccess = String.Empty;
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];

            objgrp.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
            objgrp.strGroupName = txtTitle.Text.Trim();
            if (txtPurpose.InnerText != "Description")
            {
                objgrp.strSummary = txtPurpose.InnerText.Trim();
            }
            else
            {
                objgrp.strSummary = "";
            }
            objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            string PhotoPath = "";
            if (FileUpload1.HasFile)
            {
                string name = FileUpload1.FileName;
                string ext = System.IO.Path.GetExtension(name);

                if (ext.Trim() != ".jpg" && ext.Trim() != ".jpeg" && ext.Trim() != ".png" && ext.Trim() != ".gif" && ext.Trim() != ".org")
                {
                    lblSuccMessage.Text = "File format not supported.Image should be '.jpg or .jpeg or .png or .gif'";
                    lblSuccMessage.CssClass = "RedErrormsg";
                    return;
                }
                int FileLength = FileUpload1.PostedFile.ContentLength;
                if (FileLength <= 3145728)
                {
                    PhotoPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(FileUpload1.FileName).ToString();
                    FileUpload1.SaveAs(Server.MapPath("~\\CroppedPhoto\\" + PhotoPath));
                    ViewState["ImagePath"] = PhotoPath;
                    Session["ImagePath"] = PhotoPath;
                    imgUser.Src = "CroppedPhoto/" + Convert.ToString(Session["ImagePath"]);
                    ImgRemovePic.Visible = true;
                    lblSuccMessage.Text = "";
                }
                else
                    PhotoPath = Convert.ToString(ViewState["ImagePath"]);
            }

            if (Session["ImagePath"] != null)
            {
                objgrp.strLogoPath = Session["ImagePath"].ToString();
            }
            else
            {
                objgrp.strLogoPath = "";
            }

            if (Convert.ToString(ViewState["join"]) == "A")
            {
                objgrp.strAccess = Convert.ToString(ViewState["join"]);
            }
            else
            {
                objgrp.strAccess = Convert.ToString(ViewState["join"]);
            }
            objgrp.strIpAddress = ip;
            objgrp.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objgrpDB.AddEditDel_Scrl_UserGroupDetailTbl(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.Update);
            //Added By Dilip 

            WallMessageDO.intAddedBy = Convert.ToInt32(Convert.ToString(Session["ExternalUserId"]));
            WallMessageDO.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);

            string totalMembers = hdnMembId.Value;
            string[] members = totalMembers.Split(',');
            for (int i = 0; i < members.Length; i++)
            {
                if (Convert.ToString(members.GetValue(i)) != "")
                {

                    int IDs = Convert.ToInt32(members.GetValue(i));
                    objDOgroup.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDOgroup.strUniqueKey = Convert.ToString(ViewState["strUniqueKey"]);
                    objDOgroup.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
                    objDOgroup.inviteMembers = Convert.ToString(IDs);

                    objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.UpGrpRoleRequestPermission);

                    objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.InsertGroupInvitationEdit);


                    int ID = Convert.ToInt32(members.GetValue(i));
                    WallMessageDO.intInvitedUserId = Convert.ToInt32(ID);
                    WallMessageDO.striInvitedUserId = Convert.ToString(ID);
                    WallMessageDO.intAddedBy = Convert.ToInt32(Convert.ToString(Session["ExternalUserId"]));
                    WallMessageDO.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
                    WallMessageDA.Scrl_AddEditDelWallMessage(WallMessageDO, DA_WallMessage.WallMessage.SelectMessageDetails);

                    if (hdnTitle.Value != objgrp.strGroupName)
                    {
                        WallMessageDO.intAddedBy = Convert.ToInt32(Convert.ToString(Session["ExternalUserId"]));
                        WallMessageDO.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
                        WallMessageDO.StrRecommendation = "Group owner updated the group name as \"" + objgrp.strGroupName + "\"";
                        WallMessageDO.strSubject = "Group Name Changed";
                        WallMessageDO.strIpAddress = ip;
                        WallMessageDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        WallMessageDO.intInvitedUserId = Convert.ToInt32(ID);
                        WallMessageDO.striInvitedUserId = Convert.ToString(ID);
                        WallMessageDO.strTotalGrpMemberID = hdnTitle.Value;
                        WallMessageDA.Scrl_AddEditDelWallMessage(WallMessageDO, DA_WallMessage.WallMessage.Add);
                    }

                    dt = WallMessageDA.GetDataTable(WallMessageDO, DA_WallMessage.WallMessage.CheckMessagesend);
                    if (dt.Rows.Count == 0)
                    {
                        WallMessageDO.intAddedBy = Convert.ToInt32(Convert.ToString(Session["ExternalUserId"]));
                        WallMessageDO.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
                        if (objgrp.strAccess == "A")
                        {
                            WallMessageDO.StrRecommendation = "Group Owner added You to Group.";
                            WallMessageDO.strSubject = "Group Invitation";
                        }
                        else
                        {
                            WallMessageDO.StrRecommendation = "Group Owner Invite You to Join Group.";
                            WallMessageDO.strSubject = "Group Invitation";
                        }
                        WallMessageDO.strIpAddress = ip;
                        WallMessageDO.intAddedBy = Convert.ToInt32(Convert.ToString(Session["ExternalUserId"]));
                        WallMessageDO.intInvitedUserId = Convert.ToInt32(ID);
                        WallMessageDO.striInvitedUserId = Convert.ToString(ID);

                        WallMessageDA.Scrl_AddEditDelWallMessage(WallMessageDO, DA_WallMessage.WallMessage.Add);
                    }
                    if (objgrp.strAccess == "A")
                    {
                        objGrpJoinDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
                        objGrpJoinDO.intInvitedUserId = Convert.ToInt32(ViewState["UserID"]);

                        ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            ip = Request.ServerVariables["REMOTE_ADDR"];
                        objGrpJoinDO.strIpAddress = ip;

                        objGrpJoinDO.intAddedBy = Convert.ToInt32(ID);
                        objGrpJoinDO.intRegistrationId = Convert.ToInt32(ID);
                        objGrpJoinDO.isAccepted = 1;
                        objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Insert);
                    }
                }
            }

            objDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objDO.strIpAddress = ip;
            objDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objDO.strUniqueKey = objDA.GetUniqueKeyPermissionDataTable(objDO, Convert.ToInt32(ViewState["intGroupId"]));
            objDO.grpInvMemberId = Convert.ToInt32(ViewState["GroupMemberId"]);
            ////commented by akash on 29/04/2015
            //DataTable dttt = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.deleteMemeberRole);
            //for (int i = 0; i < members.Length; i++)
            //{
            //    if (members.GetValue(i) != "")
            //    {

            //        lblinviteMemeber.Visible = false;
            //        int ID = Convert.ToInt32(members.GetValue(i));
            //        objDO.strMemberName = ID.ToString();

            //        objDO.inviteMembers = Convert.ToString(ID);
            //        DataTable dt = objDA.GetRolePermissionDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_GroupModulePermissionTbl.GetinviteMember);
            //        if (dt.Rows.Count > 0)
            //        {
            //            objDO.intModifiedBy = Convert.ToInt32(ViewState["UserID"]);
            //            objDO.intGrpInvitationMemberId = Convert.ToInt32(dt.Rows[0]["intGrpInvitationMemberId"].ToString());
            //            objDA.AddEditDel_Scrl_UserGroupDetailTbl(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.UpdateGroupInvitation);
            //        }
            //        else
            //        {
            //            objDA.AddEditDel_Scrl_UserGroupDetailTbl(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.InsertGroupInvitation);
            //        }
            //    }
            //    else
            //    {
            //        lblinviteMemeber.Visible = true;
            //    }
            //}
            ////-----------------------------------
            ViewState["GroupOutId"] = Convert.ToInt32(ViewState["intGroupId"]);
            DataTable dtsub = new DataTable();
            dtsub = (DataTable)ViewState["SubjectCategory"];
            if (dtsub.Rows.Count > 0)
            {
                objgrpDB.AddEditDel_Scrl_UserGroupDetailTbl(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.DeleteContextGroupDocument);
            }
            else
            {
                objgrpDB.AddEditDel_Scrl_UserGroupDetailTbl(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.DeleteContextGroupDocument);
            }

            foreach (DataRow dr in dtsub.Rows)
            {
                objgrp.inGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                objgrp.intSubjectCategoryId = Convert.ToInt32(dr["intCategoryId"]);
                objgrp.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objgrpDB.AddEditDel_Scrl_UserGroupDetailTbl(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.UpdateCotextGroupDocument);
            }
            divGroupSucces.Style.Add("display", "block");
            Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);

        }
        catch { }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    #region #Invitation Memeber

    protected void getInviteeName()
    {
        DataTable dtDoc = new DataTable();
        objdonetwork.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
        dtDoc = objdanetwork.GetUserConnections(objdonetwork, DA_Networks.NetworkDetails.ConnectedUsers);
        if (dtDoc.Rows.Count > 0)
        {
            txtInviteMember.DataSource = dtDoc;
            txtInviteMember.DataValueField = "intInvitedUserId";
            txtInviteMember.DataTextField = "Name";
            txtInviteMember.DataBind();
        }
    }

    protected void getInviteNameEdit()
    {
        string hdnJoin = "";
        string[] body;
        string[] Joins;
        objDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        DataTable dtinvt = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGroupMemeberRole);

        DataTable dtJoin = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGroupJoinUser);
        if (dtJoin.Rows.Count > 0)
        {
            foreach (DataRow dr in dtJoin.Rows)
            {
                hdnJoin = hdnJoin + dr["intRegistrationId"].ToString() + ",";
            }

            Joins = hdnJoin.Split(',');
            for (int i = 0; i < Joins.Length; i++)
            {
                string selectedvalue = Joins[i];
                if (selectedvalue != "")
                {
                    foreach (ListItem item in txtInviteMember.Items)
                    {
                        if (item.Value == selectedvalue)
                        {
                            item.Attributes.Add("class", "result-selected");
                        }
                    }
                }
            }
        }
        if (dtinvt.Rows.Count > 0)
        {
            foreach (DataRow dr in dtinvt.Rows)
            {
                hdnMembId.Value = hdnMembId.Value + dr["strMemberName"].ToString() + ",";
            }
            body = hdnMembId.Value.Split(',');
            for (int i = 0; i < body.Length; i++)
            {
                string selectedvalue = body[i];
                if (selectedvalue != "")
                {
                    foreach (ListItem item in txtInviteMember.Items)
                    {
                        if (item.Value == selectedvalue)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }

        }
    }
    #endregion

    protected void ImgRemovePic_OnClick(object sender, EventArgs e)
    {
        try
        {

            string imgPathPhysical = "";
            if (Session["ImagePath"] == null)
            {
                imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + Convert.ToString(ViewState["ImagePath"]));
            }
            else
            {
                imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + Convert.ToString(Session["ImagePath"]));
            }

            if (File.Exists(imgPathPhysical))
            {
                File.Delete(imgPathPhysical);
                imgUser.Src = "~/images/groupPhoto.jpg";
                ImgRemovePic.Visible = false;
                Session["ImagePath"] = null;
                ViewState["ImagePath"] = null;
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void fileuplad_onload(object sender, EventArgs e)
    {
        string PhotoPath = "";
        if (FileUpload1.HasFile)
        {
            string name = FileUpload1.FileName;
            string ext = System.IO.Path.GetExtension(name);

            if (ext.Trim() != ".jpg" && ext.Trim() != ".jpeg" && ext.Trim() != ".png" && ext.Trim() != ".gif" && ext.Trim() != ".org")
            {
                lblSuccMessage.Text = "File format not supported.Image should be '.jpg or .jpeg or .png or .gif'";
                lblSuccMessage.CssClass = "RedErrormsg";
                return;
            }

            int FileLength = FileUpload1.PostedFile.ContentLength;
            if (FileLength <= 3145728)
            {
                PhotoPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(FileUpload1.FileName).ToString();
                FileUpload1.SaveAs(Server.MapPath("~\\CroppedPhoto\\" + PhotoPath));
                ViewState["ImagePath"] = PhotoPath;
                Session["ImagePath"] = PhotoPath;
                imgUser.Src = "CroppedPhoto/" + Convert.ToString(Session["ImagePath"]);
                ImgRemovePic.Visible = true;
                lblSuccMessage.Text = "";
            }
            else
                PhotoPath = Convert.ToString(ViewState["ImagePath"]);
        }
        else
        {
            lblSuccMessage.Text = "Please select image to upload.";
            lblSuccMessage.CssClass = "RedErrormsg";
            return;
        }

    }

    protected void AsyncFileUpload1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        string PhotoPath = "";
        if (FileUpload1.HasFile)
        {
            string name = FileUpload1.FileName;
            string ext = System.IO.Path.GetExtension(name);

            if (ext.Trim() != ".jpg" && ext.Trim() != ".jpeg" && ext.Trim() != ".png" && ext.Trim() != ".gif" && ext.Trim() != ".org")
            {
                divGroupSucces.Style.Add("display", "block");
                lblSuccMess.Text = "File format not supported.Image should be '.jpg or .jpeg or .png or .gif'";
                lblSuccMess.CssClass = "RedErrormsg";
                return;
            }

            int FileLength = FileUpload1.PostedFile.ContentLength;
            if (FileLength <= 3145728)
            {
                PhotoPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(FileUpload1.FileName).ToString();
                FileUpload1.SaveAs(Server.MapPath("~\\CroppedPhoto\\" + PhotoPath));
                ViewState["ImagePath"] = PhotoPath;
                Session["ImagePath"] = PhotoPath;
                imgUser.Src = "~/CroppedPhoto/" + PhotoPath;
                ImgRemovePic.Visible = true;
            }
            else
                PhotoPath = Convert.ToString(ViewState["ImagePath"]);
        }
        else
        {
            divGroupSucces.Style.Add("display", "block");
            lblSuccMess.Text = "Please select image to upload.";
            lblSuccMess.CssClass = "RedErrormsg";
            return;
        }

    }

    protected void imgAutobtn_Click(object sender, EventArgs e)
    {
        ViewState["join"] = "A";
        imgAutojoin.Attributes.Add("class", "chkEnabled");
        imgReqjoin.Attributes.Add("class", "chkDisabled");
    }

    protected void imgReqjoin_Click(object sender, EventArgs e)
    {
        ViewState["join"] = "R";
        imgAutojoin.Attributes.Add("class", "chkDisabled");
        imgReqjoin.Attributes.Add("class", "chkEnabled");
    }

}