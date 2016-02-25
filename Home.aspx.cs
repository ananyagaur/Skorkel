using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DA_SKORKEL;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

public partial class Home : System.Web.UI.Page
{
    #region Class Member
    string documentPath1 = "";
    int FileStatus = 0;

    string documentName1 = "";

    DA_Profile ObjDAprofile = new DA_Profile();
    DO_Profile objDoProfile = new DO_Profile();

    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();

    DA_MyPoints objDAPoint = new DA_MyPoints();
    DO_MyPoints objDOPoint = new DO_MyPoints();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_UserStatusUpdateTbl objstatusDO = new DO_Scrl_UserStatusUpdateTbl();
    DA_Scrl_UserStatusUpdateTbl objstatusDA = new DA_Scrl_UserStatusUpdateTbl();

    DO_Registrationdetails objdoreg = new DO_Registrationdetails();
    DA_Registrationdetails objdareg = new DA_Registrationdetails();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    DataTable dt = new DataTable();
    DataTable dtUpdate = new DataTable();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_ProfileDocuments DocDO = new DO_ProfileDocuments();
    DA_ProfileDocuments DocDA = new DA_ProfileDocuments();

    DA_ProfileDocuments objDAProDocs = new DA_ProfileDocuments();
    DO_ProfileDocuments objDoProDocs = new DO_ProfileDocuments();

    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DO_Scrl_UserExperienceTbl objUserExpDO = new DO_Scrl_UserExperienceTbl();
    DA_Scrl_UserExperienceTbl objUserExpDA = new DA_Scrl_UserExperienceTbl();

    DO_Scrl_UserEducationTbl objDOEdu = new DO_Scrl_UserEducationTbl();
    DA_Scrl_UserEducationTbl objDAEdu = new DA_Scrl_UserEducationTbl();

    DO_Scrl_UserRecommendation objRecmndDO = new DO_Scrl_UserRecommendation();
    DA_Scrl_UserRecommendation objRecmndDA = new DA_Scrl_UserRecommendation();

    static string APIURL = ConfigurationManager.AppSettings["APIURL"];
    static string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    static string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        ViewState["BrowserName"] = browser.Browser;
        if (!IsPostBack)
        {
            lnkHome.Style.Add("border-bottom", "2px solid #008f9b");
            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Home";
            PnlHome.Visible = true;
            Session["SubmitTime"] = DateTime.Now.ToString();
            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "10";
            int LoginId = Convert.ToInt32(Session["ExternalUserId"]);
            int FrndId = 0;
            ViewState["UserID"] = LoginId;
            ViewState["RegId"] = FrndId;
            hdnUserID.Value = Convert.ToString(LoginId);
            divEditUserProfile.Style.Add("display", "block");
            lnkChangeImage.Visible = true;
            if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
            {
                FrndId = Convert.ToInt32(Request.QueryString["RegId"]);

                if (LoginId == FrndId)
                {
                    GetProfileDetails(Convert.ToInt32(ViewState["UserID"]));
                    FrndId = 0;
                }
                else
                {
                    divEditUserProfile.Style.Add("display", "none");
                    lnkChangeImage.Visible = false;
                    ViewState["UserID"] = LoginId;
                    ViewState["RegId"] = FrndId;
                    GetProfileDetails(Convert.ToInt32(Request.QueryString["RegId"]));
                    ConnectedUserAdd();
                    BindConnectedUser(FrndId);
                    BindGroup(FrndId);
                }
            }
            else
            {
                GetProfileDetails(Convert.ToInt32(ViewState["UserID"]));
            }

            if (FrndId == 0)
            {
                BindPostUpdate(LoginId, 0, 1); //self wall
                BindConnectedUser(LoginId);
                hdnloginIds.Value = Convert.ToString(LoginId);
                BindGroup(LoginId);
                LoadEndrosAndMsg();
                TotalscoreCount();
            }
            else
            {
                BindPostUpdate(LoginId, FrndId, 2);
                BindConnectedUser(FrndId);
                hdnloginIds.Value = Convert.ToString(FrndId);
                BindGroup(FrndId);
                LoadEndrosAndMsg();
                TotalscoreCount();
            }

            if (Request.QueryString["Endrosment"] != "" && Request.QueryString["Endrosment"] != null)
            {
                lnkWorkex.Style.Add("border-bottom", "2px solid #008f9b");
                lnkHome.Style.Add("border-bottom", "none");
                lnkDocuments.Style.Add("border-bottom", "none");
                lnkEducation.Style.Add("border-bottom", "none");
                lnkAchievements.Style.Add("border-bottom", "none");
                PnlHome.Visible = false;
                PnlDocuments.Visible = false;
                PnlWorkex.Visible = true;
                PnlEduction.Visible = false;
                PnlAchivement.Visible = false;
                LoadUserExp();
                LoadUserAreaExp();
                LoadUserAreaSkill();
                LoadUserexpYear();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "groupConnChange();", true);
                this.ClientScript.RegisterStartupScript(this.GetType(), "navigate", "window.onload = function() {window.location.hash='#SkillsetSection';}", true);
            }

            if (Request.QueryString["ActiveStatus"] == "H")
            {
                HomeLoad();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallMessageNotification();", true);
            }
            else
                if (Request.QueryString["ActiveStatus"] == "D")
                {
                    Document();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallMessageNotification();", true);
                }
                else
                    if (Request.QueryString["ActiveStatus"] == "W")
                    {
                        WorkExp();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallMessageNotification();", true);
                    }
                    else
                        if (Request.QueryString["ActiveStatus"] == "E")
                        {
                            Education();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallMessageNotification();", true);
                        }
                        else
                            if (Request.QueryString["ActiveStatus"] == "Ac")
                            {
                                Achivement();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallMessageNotification();", true);
                            }
        }

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SubmitTime"] = Session["SubmitTime"];
        }
    }

    protected void lnkHome_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        HomeLoad();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallHome", "CallHomeLI();", true);
    }

    protected void HomeLoad()
    {
        lnkHome.Style.Add("border-bottom", "2px solid #008f9b");
        lnkDocuments.Style.Add("border-bottom", "none");
        lnkWorkex.Style.Add("border-bottom", "none");
        lnkEducation.Style.Add("border-bottom", "none");
        lnkAchievements.Style.Add("border-bottom", "none");
        PnlHome.Visible = true;
        PnlDocuments.Visible = false;
        PnlEduction.Visible = false;
        PnlWorkex.Visible = false;
        PnlAchivement.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "groupConnChange();", true);
    }

    protected void lnkDocuments_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            Response.Redirect("Home.aspx?RegId=" + Request.QueryString["RegId"] + "&ActiveStatus=D");
        }
        Document();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DocLI", "CallDocLI();", true);
    }

    protected void Document()
    {
        lnkDocuments.Style.Add("border-bottom", "2px solid #008f9b");
        lnkHome.Style.Add("border-bottom", "none");
        lnkWorkex.Style.Add("border-bottom", "none");
        lnkEducation.Style.Add("border-bottom", "none");
        lnkAchievements.Style.Add("border-bottom", "none");
        PnlDocuments.Visible = true;
        PnlHome.Visible = false;
        PnlEduction.Visible = false;
        PnlWorkex.Visible = false;
        PnlAchivement.Visible = false;
        BindDocuments();
        BindSubjectList();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "groupConnChange();", true);
    }

    protected void lnkWorkex_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            Response.Redirect("Home.aspx?RegId=" + Request.QueryString["RegId"] + "&ActiveStatus=W");
        }
        AddWorkExp.Style.Add("display", "none");
        divAddskill.Style.Add("display", "none");
        WorkExp();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallWork", "CallWorkLI();", true);
    }

    protected void WorkExp()
    {
        lnkWorkex.Style.Add("border-bottom", "2px solid #008f9b");
        lnkHome.Style.Add("border-bottom", "none");
        lnkDocuments.Style.Add("border-bottom", "none");
        lnkEducation.Style.Add("border-bottom", "none");
        lnkAchievements.Style.Add("border-bottom", "none");
        PnlHome.Visible = false;
        PnlDocuments.Visible = false;
        PnlWorkex.Visible = true;
        PnlEduction.Visible = false;
        PnlAchivement.Visible = false;
        LoadUserExp();
        LoadUserAreaExp();
        LoadUserAreaSkill();
        LoadUserexpYear();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "groupConnChange();", true);

    }

    protected void lnkEducation_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            Response.Redirect("Home.aspx?RegId=" + Request.QueryString["RegId"] + "&ActiveStatus=E");
        }
        divEducation.Style.Add("display", "none");
        Education();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallEducation", "CallEduLI();", true);
    }

    protected void Education()
    {
        lnkEducation.Style.Add("border-bottom", "2px solid #008f9b");
        lnkHome.Style.Add("border-bottom", "none");
        lnkDocuments.Style.Add("border-bottom", "none");
        lnkWorkex.Style.Add("border-bottom", "none");
        lnkAchievements.Style.Add("border-bottom", "none");
        PnlHome.Visible = false;
        PnlDocuments.Visible = false;
        PnlWorkex.Visible = false;
        PnlEduction.Visible = true;
        PnlAchivement.Visible = false;
        LoadEducation();
        LoadYear();

    }

    protected void lnkAchievements_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            Response.Redirect("Home.aspx?RegId=" + Request.QueryString["RegId"] + "&ActiveStatus=Ac");
        }

        Achivement();
        divAchivement.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAchi", "CallAchLI();", true);
    }

    protected void Achivement()
    {
        lnkAchievements.Style.Add("border-bottom", "2px solid #008f9b");
        lnkHome.Style.Add("border-bottom", "none");
        lnkDocuments.Style.Add("border-bottom", "none");
        lnkWorkex.Style.Add("border-bottom", "none");
        lnkEducation.Style.Add("border-bottom", "none");
        PnlHome.Visible = false;
        PnlDocuments.Visible = false;
        PnlWorkex.Visible = false;
        PnlEduction.Visible = false;
        PnlAchivement.Visible = true;
        LoadAchivment();
        LoadMilestones();
    }

    protected void GetProfileDetails(int UserId)
    {
        objDoProfile.RegistrationId = UserId;
        DataTable dt = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetAllProfileUSerDetails);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["vchrPhotoPath"].ToString() != "" && dt.Rows[0]["vchrPhotoPath"].ToString() != string.Empty)
            {
                try
                {

                    string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + dt.Rows[0]["vchrPhotoPath"].ToString());
                    if (File.Exists(imgPathPhysical))
                    {
                        imgUser.Src = "~\\CroppedPhoto\\" + dt.Rows[0]["vchrPhotoPath"].ToString();
                        ViewState["imgComment"] = "~\\CroppedPhoto\\" + dt.Rows[0]["vchrPhotoPath"].ToString();
                        hdnImageProfile.Value = "CroppedPhoto/" + dt.Rows[0]["vchrPhotoPath"].ToString();
                    }
                    else
                    {
                        imgUser.Src = "images/profile-photo.png";
                        ViewState["imgComment"] = "images/profile-photo.png";
                        hdnImageProfile.Value = "images/profile-photo.png";
                    }

                }
                catch
                {
                    imgUser.Src = "images/profile-photo.png";
                    ViewState["imgComment"] = "images/profile-photo.png";
                    hdnImageProfile.Value = "images/profile-photo.png";
                }
            }
            else
            {
                imgUser.Src = "images/profile-photo.png";
                ViewState["imgComment"] = "images/profile-photo.png";
                hdnImageProfile.Value = "images/profile-photo.png";
            }
            lblUserProfName.Text = Convert.ToString(dt.Rows[0]["NAME"]);
            ViewState["LoginName"] = lblUserProfName.Text;
            if (Request.QueryString["RegId"] == null)
            {
                Session["LoginName"] = lblUserProfName.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallProfilename", "CallProfilename();", true);
            }
        }
    }

    protected void LoadEndrosAndMsg()
    {
        objDoProfile.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
        objDoProfile.ConnectRegistrationId = Convert.ToInt32(ViewState["RegId"]);
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            objDoProfile.RegistrationId = Convert.ToInt32(Request.QueryString["RegId"]);
            DataTable dtEndorse = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetEndorseCount);
            if (dtEndorse.Rows.Count > 0)
            {
                int Endorse = Convert.ToInt32(dtEndorse.Rows[0]["Endorse"]);
                lblEndorseCount.Text = Convert.ToString(Endorse);
            }
            else
            {
                lblEndorseCount.Text = "0";
            }
        }
        else
        {
            DataTable dtEndorse = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetEndorseCount);
            if (dtEndorse.Rows.Count > 0)
            {
                int Endorse = Convert.ToInt32(dtEndorse.Rows[0]["Endorse"]);
                lblEndorseCount.Text = Convert.ToString(Endorse);
            }
            else
            {
                lblEndorseCount.Text = "0";
            }
        }
        objDoProfile.striInvitedUserId = Convert.ToString(ViewState["UserID"]);
    }

    protected void ConnectedUserAdd()
    {
        objdoreg.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
        objdoreg.InvitedUserId = Convert.ToInt32(ViewState["RegId"]);
        DataTable dtReq = new DataTable();
        dtReq = objdareg.GetExistsRequest(objdoreg, DA_Registrationdetails.RegistrationDetails.SingleRecord);
        if (dtReq.Rows.Count > 0)
        {
            int IsAccepted = Convert.ToInt32(dtReq.Rows[0]["IsAccepted"]);
            if (IsAccepted == 0)
            {
                lnkAddFriend.Style.Add("display", "block");
                if (dtReq.Rows[0]["Accepts"].ToString() == "2")
                    lnkAddFriend.Text = "Accept Invitation";

                hdnConnCommandName.Value = "C";
                divdisplayWall.Style.Add("display", "none");
                divDisp.Style.Add("display", "block");
                lblEndorseCount.OnClientClick = "return false;";
            }
            else if (IsAccepted == 2)
            {
                lnkAddFriend.Style.Add("display", "block");
                hdnConnCommandName.Value = "C";
                divdisplayWall.Style.Add("display", "none");
                divDisp.Style.Add("display", "block");
                lblEndorseCount.OnClientClick = "return false;";

            }
            else
            {
                lnkConnectedUser.Style.Add("display", "block");
                lnkAddFriend.Style.Add("display", "none");
                hdnConnCommandName.Value = "DC";
                divdisplayWall.Style.Add("display", "block");
                divDisp.Style.Add("display", "none");
            }
        }
        else
        {
            if (objdoreg.RegistrationId == objdoreg.InvitedUserId)
            {
                lnkAddFriend.Style.Add("display", "none");
                lnkConnectedUser.Style.Add("display", "none");
            }
            else
            {
                lnkAddFriend.Style.Add("display", "block");
                hdnConnCommandName.Value = "C";
                divdisplayWall.Style.Add("display", "none");
                divDisp.Style.Add("display", "block");
                lblEndorseCount.OnClientClick = "return false;";
            }

        }
    }

    protected void TotalscoreCount()
    {
        int Totalexp = 0;
        int TotalexpScore = 0;
        int FinalTotalexpScore = 0;
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            objDOPoint.UserID = Convert.ToInt32(ViewState["RegId"]);
        }
        else
        {
            objDOPoint.UserID = Convert.ToInt32(ViewState["UserID"]);
        }
        string totalPersonalPt = CalculatePersonalPoint(objDOPoint.UserID);
        DataTable dtDefactoTotalScore = objDAPoint.GetDataTable(objDOPoint, DA_MyPoints.MyPoint.GetDefactoTotalScore);
        DataTable dtDefacto = objDAPoint.GetDataTable(objDOPoint, DA_MyPoints.MyPoint.GetDefactoScore);
        if (dtDefacto.Rows.Count > 0)
        {
            int count = dtDefacto.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                if (dtDefacto.Rows[i]["Education"].ToString() == "Professional Experience")
                {
                    Totalexp = Convert.ToInt32(dtDefacto.Rows[i]["Points"]);
                    FinalTotalexpScore = (Convert.ToInt32(dtDefactoTotalScore.Rows[0]["TotalDefacto"]) - Totalexp);
                    ViewState["Totalexp"] = Totalexp;
                }
            }
        }

        int ExpYear = Convert.ToInt32(Totalexp);
        if (ExpYear == 0)
        {
            if (dtDefactoTotalScore.Rows.Count > 0)
            {
                if (ViewState["Totalexp"] != null)
                {
                    if ((dtDefactoTotalScore.Rows[0]["TotalDefacto"]).ToString() != "")
                    {
                        FinalTotalexpScore = 50 + (Convert.ToInt32(dtDefactoTotalScore.Rows[0]["TotalDefacto"]));
                    }
                    else
                    { FinalTotalexpScore = 0; }
                }
                else
                {
                    if ((dtDefactoTotalScore.Rows[0]["TotalDefacto"]).ToString() != "")
                    {
                        FinalTotalexpScore = (Convert.ToInt32(dtDefactoTotalScore.Rows[0]["TotalDefacto"]));
                    }
                    else
                    { FinalTotalexpScore = 0; }
                }
            }
            else
            {
                FinalTotalexpScore = 0;
            }
        }
        else
            if (ExpYear < 3)
            {
                TotalexpScore = 50;
            }
            else if (ExpYear >= 3 && ExpYear < 6)
            {
                TotalexpScore = 100;
            }
            else if (ExpYear >= 6 && ExpYear < 10)
            {
                TotalexpScore = 150;
            }
            else if (ExpYear >= 10)
            {
                TotalexpScore = 200;
            }
            else
            {
                TotalexpScore = 0;
            }
        int AllScore = 0;
        FinalTotalexpScore = FinalTotalexpScore + TotalexpScore;
        if (totalPersonalPt != "")
        {
            AllScore = Convert.ToInt32(totalPersonalPt) + FinalTotalexpScore;
        }
        else
        {
            AllScore = FinalTotalexpScore;
        }
        lblMessCount.Text = Convert.ToString(AllScore);
        ViewState["Totalexp"] = null;
    }

    protected void lnkeditProfile_click(object sender, EventArgs e)
    {
        lblProfilemsg.Text = "";
        divEditProfile.Style.Add("display", "block");
    }

    protected void lnkCancelProfile_click(object sender, EventArgs e)
    {
        lblProfilemsg.Text = "";
        GetProfileDetails(Convert.ToInt32(Session["ExternalUserId"]));
        divEditProfile.Style.Add("display", "none");
    }

    protected void EditUserProfile(int UserId)
    {
        objDoProfile.RegistrationId = UserId;
        DataTable dt = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetAllProfileUSerDetails);
        if (dt.Rows.Count > 0)
        {
            lblUserProfName.Text = Convert.ToString(dt.Rows[0]["NAME"]);
            ViewState["LoginName"] = lblUserProfName.Text;
            txtName.Text = Convert.ToString(dt.Rows[0]["FullNAME"]);
            txtGender.Text = Convert.ToString(dt.Rows[0]["chrSex"]);
            txtLanguage.Text = Convert.ToString(dt.Rows[0]["vchrLanguages"]);
            txtEmailId.Text = Convert.ToString(dt.Rows[0]["vchrUserName"]);
            if (Convert.ToString(dt.Rows[0]["intMobile"]) != "0")
            {
                txtPhone.Text = Convert.ToString(dt.Rows[0]["intMobile"]);
            }
            else
            {
                txtPhone.Text = "";
            }

        }
    }

    protected void lnkProfileSave_click(object sender, EventArgs e)
    {
        PopUpCropImage.Style.Add("display", "none");
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        //Update user details.
        objdoreg.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
        objdoreg.ModifiedBy = Convert.ToInt32(ViewState["UserID"]);
        if (txtName.Text.Trim() == "")
        {
            lblProfilemsg.Text = "Please enter name.";
            return;
        }
        if (txtName.Text.Trim() == "")
        {
            lblProfilemsg.Text = "Please enter name.";
            return;
        }
        string name = txtName.Text.Trim();
        string[] ssize = name.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        if (ssize.Count() > 2)
        {
            if (ssize.Count() > 3)
            {
                objdoreg.FirstName = ssize[0] + " " + ssize[1] + " " + ssize[2];
                objdoreg.LastName = ssize[3];
            }
            else
            {
                objdoreg.FirstName = ssize[0] + " " + ssize[1];
                objdoreg.LastName = ssize[2];
            }
        }
        else
        {
            objdoreg.FirstName = ssize[0];
            objdoreg.LastName = ssize[1];
        }

        if (txtGender.Text == "Male" || txtGender.Text == "male")
        {
            objdoreg.Sex = "M";
        }
        else if (txtGender.Text == "Female" || txtGender.Text == "female")
        {
            objdoreg.Sex = "F";
        }

        if (txtPhone.Text.Trim() != "")
        {
            objdoreg.Mobile = Convert.ToInt64(txtPhone.Text.Trim());
        }

        objdoreg.Languages = txtLanguage.Text.Trim().Replace("'", "''");
        objdoreg.IpAddress = ip;
        objdareg.AddEditDel_Profile(objdoreg, DA_Registrationdetails.RegistrationDetails.Update);
        divEditProfile.Style.Add("display", "none");
        GetProfileDetails(Convert.ToInt32(ViewState["UserID"]));
        lblProfilemsg.Text = "";
    }

    protected void lblEndorseCount_click(object sender, EventArgs e)
    {
        PopUpCropImage.Style.Add("display", "none");
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            Response.Redirect("Home.aspx?RegId=" + Request.QueryString["RegId"] + "&Endrosment=E");
        }
        else
        {
            // Response.Redirect("Home.aspx?Endrosment=E");
            lnkWorkex.Style.Add("border-bottom", "2px solid #008f9b");
            lnkHome.Style.Add("border-bottom", "none");
            lnkDocuments.Style.Add("border-bottom", "none");
            lnkEducation.Style.Add("border-bottom", "none");
            lnkAchievements.Style.Add("border-bottom", "none");
            PnlHome.Visible = false;
            PnlDocuments.Visible = false;
            PnlWorkex.Visible = true;
            PnlEduction.Visible = false;
            PnlAchivement.Visible = false;
            LoadUserExp();
            LoadUserAreaExp();
            LoadUserAreaSkill();
            LoadUserexpYear();
            divAddskill.Style.Add("display", "block");
            txtAreaExpert.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "groupConnChange();", true);
            //this.ClientScript.RegisterStartupScript(this.GetType(), "navigates", "window.onload = function() {window.location.hash='#SkillsetSection';}", true);
        }

    }

    protected void lnkAddNewGrp_Click(object sender, EventArgs e)
    {
        Response.Redirect("Create_Group.aspx?ActiveStatus=P");
    }

    protected void lnkAddFriend_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        string name = "", emailId = "";
        int accept = 0;
        int InvitedUserId = Convert.ToInt32(ViewState["RegId"]);
        objdoreg.InvitedUserId = Convert.ToInt32(ViewState["RegId"]);
        objdoreg.RegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
        objdoreg.AddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            DataTable dtReq = new DataTable();
            dtReq = objdareg.GetExistsRequest(objdoreg, DA_Registrationdetails.RegistrationDetails.SingleRecord);
            if (dtReq.Rows.Count > 0)
            {
                name = Convert.ToString(dtReq.Rows[0]["Name"]);
                emailId = Convert.ToString(dtReq.Rows[0]["vchrUserName"]);
                accept = Convert.ToInt32(dtReq.Rows[0]["IsAccepted"]);
                ViewState["accept"] = accept;
                if (Convert.ToInt32(dtReq.Rows[0]["IsAccepted"]) == 0)
                {
                    if (Convert.ToInt32(dtReq.Rows[0]["Accepts"]) != 2)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideAddUserProcess0", "HideAddUserProcess();", true);
                        string strpopup = "alert(\"You have already send the request. \");";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "script", strpopup, true);
                        return;
                    }
                    else
                    {
                        objdoreg.intRequestInvitaionId = Convert.ToInt32(dtReq.Rows[0]["intRequestInvitaionId"]);
                        objdareg.AddEditDel_Request(objdoreg, DA_Registrationdetails.RegistrationDetails.Update);
                        ConnectedUserAdd();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideAddUserProcess2", "HideAddUserProcess();", true);
                        return;
                    }
                }
                else if (Convert.ToInt32(dtReq.Rows[0]["IsAccepted"]) == 2)
                {
                    objdareg.AddEditDel_Request(objdoreg, DA_Registrationdetails.RegistrationDetails.Add);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HideAddUserProcess1", "HideAddUserProcess();", true);
                    string strpopup = "alert(\"Request Invitation and a mail has been send to the person. \");";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "script", strpopup, true);
                    SendMailConnections(emailId, name, accept);
                    return;
                }
            }
        }
        else
        {
            return;
        }
        objdoreg.ConnectRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
        objdoreg.IpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (objdoreg.IpAddress == null)
            objdoreg.IpAddress = Request.ServerVariables["REMOTE_ADDR"];

        if (Convert.ToString(hdnConnCommandName.Value).Trim() == "DC")
        {
            objdareg.AddEditDel_Request(objdoreg, DA_Registrationdetails.RegistrationDetails.UpdateConnection);
            string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
            if (ISAPIURLACCESSED != "0")
            {
                try
                {
                    String url = APIURL + "disconnectUser.action?" +
                                 "uidFirstUser=" + objdoreg.RegistrationId +
                                 "&uidSecondUser=" + objdoreg.InvitedUserId;

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                    myRequest1.Method = "GET";
                    if (ISAPIResponse != "0")
                    {
                        WebResponse myResponse1 = myRequest1.GetResponse();
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.strURL = url;
                        objAPILogDO.strAPIType = "Profile Disconnect User";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = objdoreg.IpAddress;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }

                }
                catch { }
            }

        }
        else
        {
            objdareg.AddEditDel_Request(objdoreg, DA_Registrationdetails.RegistrationDetails.Add);
            objdoreg.RegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objdoreg.InvitedUserId = Convert.ToInt32(ViewState["RegId"]);
            DataTable dtReqs = new DataTable();
            dtReqs = objdareg.GetExistsRequest(objdoreg, DA_Registrationdetails.RegistrationDetails.SingleRecord);
            if (dtReqs.Rows.Count > 0)
            {
                name = Convert.ToString(dtReqs.Rows[0]["Name"]);
                emailId = Convert.ToString(dtReqs.Rows[0]["vchrUserName"]);
                accept = Convert.ToInt32(dtReqs.Rows[0]["IsAccepted"]);

            }

            string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
            if (ISAPIURLACCESSED != "0")
            {
                try
                {
                    String url = APIURL + "connectUser.action?" +
                                 "uidFirstUser=" + objdoreg.RegistrationId +
                                 "&uidSecondUser=" + objdoreg.InvitedUserId;

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                    myRequest1.Method = "GET";
                    if (ISAPIResponse != "0")
                    {
                        WebResponse myResponse1 = myRequest1.GetResponse();
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = url;
                        objAPILogDO.strAPIType = "Profile Connect User";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = objdoreg.IpAddress;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideAddUserProcess3", "HideAddUserProcess();", true);
            string strpopup = "alert(\"Request Invitation and a mail has been send to the person. \");";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "script", strpopup, true);
            SendMailConnections(emailId, name, accept);
            return;

        }

    }

    protected void lnkChangeImage_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        divEditProfile.Style.Add("display", "none");
        PopUpCropImage.Style.Add("display", "block");
    }

    protected void lnkCancelProfilediv_click(object sender, EventArgs e)
    {
        lblProfilemsg.Text = "";
        PopUpCropImage.Style.Add("display", "none");
        GetProfileDetails(Convert.ToInt32(Session["ExternalUserId"]));
        HtmlImage masterlbl = (HtmlImage)Master.FindControl("imgProfilePic");
        masterlbl.Src = imgUser.Src;

    }

    private void SendMailConnections(string mailid, string name, int accept)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

            string mailfrom = ConfigurationManager.AppSettings["mailfrom"];
            string mailServer = ConfigurationManager.AppSettings["mailServer"];

            string username = ConfigurationManager.AppSettings["UserName"];
            string Password = ConfigurationManager.AppSettings["Password"];
            string Port = ConfigurationManager.AppSettings["Port"];
            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];
            string MailSSL = ConfigurationManager.AppSettings["MailSSL"];
            string MailTo = mailid;
            string Mailbody = "";

            NetworkCredential cre = new NetworkCredential(username, Password);
            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = true;
            clientip.Credentials = cre;
            if (MailSSL != "0")
                clientip.EnableSsl = true;
            try
            {

                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom);
                Rmm2.Body = Mailbody.ToString();
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("");

                htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Request Invitation</b>"
                    + "<br><br>" + "Dear " + name + " "
                    + "<br><br> You have a freind request sent by " + Session["LoginName"] + "<br><br>"
                    + "<br>Regards," + "<br>" + "Skorkel Team<br><br>"
                    + "****This is a system generated Email. Kindly do not reply****", null, "text/html");

                Rmm2.To.Clear();
                Rmm2.To.Add(MailTo);
                Rmm2.Subject = "Skorkel Request Invitation.";
                Rmm2.AlternateViews.Add(htmlView);
                Rmm2.IsBodyHtml = true;
                clientip.Send(Rmm2);
                Rmm2.To.Clear();

            }
            catch (FormatException ex)
            {
                ex.Message.ToString();
                return;
            }
            catch (SmtpException ex)
            {
                ex.Message.ToString();
                return;
            }
            finally
            {
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    #region Connection User And Groups

    protected void ClickAccept(object sender, EventArgs e)
    {
        try
        {
            BindConnectedUser(Convert.ToInt32(hdnloginIds.Value));
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void BindConnectedUser(int loginId)
    {
        if (Convert.ToInt32(ViewState["UserID"]) != loginId)
        {
            aAddConnection.Visible = false;
        }

        objdonetwork.RegistrationId = loginId;
        dt = objdanetwork.GetUserConnections(objdonetwork, DA_Networks.NetworkDetails.GetTopFriendList);
        if (dt.Rows.Count > 0)
        {
            ViewState["FriendList"] = dt;
            if (dt.Rows.Count > 0)
            {
                lstFrnds.DataSource = dt;
                lstFrnds.DataBind();
            }

        }

    }

    protected void lstFrnds_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Image imgfrnd = (Image)e.Item.FindControl("imgfrnd");
        HiddenField hdnimgprofile = (HiddenField)e.Item.FindControl("hdnimgprofile");

        if (imgfrnd.ImageUrl == "CroppedPhoto/" || imgfrnd.ImageUrl == "")
        {
            imgfrnd.ImageUrl = "images/comment-profile.jpg";
        }
        else
        {
            string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + hdnimgprofile.Value);
            if (!File.Exists(imgPathPhysical))
            {
                imgfrnd.ImageUrl = "images/comment-profile.jpg";
            }
        }

    }

    private void BindGroup(int LoginId)
    {
        if (Convert.ToInt32(ViewState["UserID"]) != LoginId)
        {
            lnkNewGroups.Visible = false;
        }

        DataTable dtGroup = new DataTable();
        objgrp.intRegistrationId = LoginId;
        dtGroup = objgrpDB.GetDataTable(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpupDetails);

        if (dtGroup.Rows.Count > 0)
        {
            rptGroup.DataSource = dtGroup;
            rptGroup.DataBind();
        }

    }

    protected void rptGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Image img_myGrp = (Image)e.Item.FindControl("img_myGrp");
        Label lblMembers = (Label)e.Item.FindControl("lblMembers");
        HiddenField hdnGroupId = (HiddenField)e.Item.FindControl("hdnGroupId");
        HiddenField hdnimgprofile = (HiddenField)e.Item.FindControl("hdnimgprofile");

        if (img_myGrp.ImageUrl == "CroppedPhoto/")
        {
            img_myGrp.ImageUrl = "images/groupPhoto.jpg";
        }
        else
        {
            string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + hdnimgprofile.Value);
            if (!File.Exists(imgPathPhysical))
            {
                img_myGrp.ImageUrl = "images/groupPhoto.jpg";
            }
        }

    }

    #endregion

    #region Home Section
    protected void lnkPostUpdate_Click(object sender, EventArgs e)
    {
        if (ViewState["UserID"] != null)
        {
            divDeletesucess.Style.Add("display", "none");
            SavePost();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallHome0", "CallHomeLI();", true);
        }
    }

    private void BindPostUpdate(int LoginId, int FriendId, int flag) // FLag 1 self wall , 2  Friends wall & Not a friends wall
    {
        objstatusDO.intAddedBy = LoginId;
        objstatusDO.intRegistrationId = FriendId;
        objstatusDO.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objstatusDO.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);

        if (flag == 1)
            dtUpdate = objstatusDA.GetDataTable(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.GetSelfWall);
        else if (flag == 2)
            dtUpdate = objstatusDA.GetDataTable(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.GetFriendAndNotfrndWall);

        if (dtUpdate.Rows.Count > 0)
        {
            ViewState["MaxCount"] = dtUpdate.Rows[0]["MaxCount"];
            hdnMaxcount.Value = dtUpdate.Rows[0]["Maxcount"].ToString();
            if (Convert.ToInt32(ViewState["MaxCount"]) <= Convert.ToInt32(hdnTotalItem.Value))
            {
                pLoadMore.Style.Add("display", "none");
                //lblNoMoreRslt.Visible = true;
            }

            lstPostUpdates.DataSource = dtUpdate;
            lstPostUpdates.DataBind();
            dvPage.Visible = true;
        }
        else
        {
            lstPostUpdates.DataSource = null;
            lstPostUpdates.DataBind();
            dvPage.Visible = false;
            pLoadMore.Style.Add("display", "none");
            lblNoMoreRslt.Visible = false;
        }
    }

    protected void SavePost()
    {
        try
        {
            lblPostMsg.Visible = false;
            lblfilename.Text = ".";
            lblfilename.ForeColor = System.Drawing.Color.White;
            if (string.IsNullOrEmpty(txtPostUpdate.InnerText) || txtPostUpdate.InnerText == "Share an update...")
            {
                lblPostMsg.Visible = true;
                return;
            }

            string filepath = hdnPhoto.Value;
            string filename = hdnDocName.Value;
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];

            objstatusDO.strPostType = "public";
            objstatusDO.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);

            if (ViewState["RegId"].ToString() != "0")
                objstatusDO.intRegistrationId = Convert.ToInt32(ViewState["RegId"]);

            objstatusDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
            objstatusDO.strPostDescription = txtPostUpdate.InnerHtml; //InnerText.Trim().Replace("'", "''");
            if (filename != "")
            {
                UploadPhoto();
                if (ViewState["PhotoUpload"] != null)
                {
                    ViewState["editVideo"] = null;
                    ViewState["editImage"] = null;
                    objstatusDO.strPhotoPath = Convert.ToString(ViewState["PhotoUpload"]);
                    objstatusDO.strVideoPath = "";
                }

            }
            if (ViewState["PhotoUpload"] == null)
            {
                if (filename != "")
                {
                    UploadVideoFiles();
                    if (FileStatus != 1)
                    {
                        if (ViewState["VideoUpload"] != null)
                        {
                            ViewState["editVideo"] = null;
                            ViewState["editImage"] = null;
                            objstatusDO.strVideoPath = Convert.ToString(ViewState["VideoUpload"]);
                            objstatusDO.strVideoPath = documentPath1;
                            objstatusDO.strPhotoPath = "";
                        }
                    }
                    else
                        return;
                }
            }

            if (ViewState["editImage"] != null)
            {
                objstatusDO.strPhotoPath = Convert.ToString(ViewState["editImage"]);
                objstatusDO.strVideoPath = "";
            }

            if (ViewState["editVideo"] != null)
            {
                if (ViewState["editVideo"].ToString() != "")
                {
                    objstatusDO.strVideoPath = Convert.ToString(ViewState["editVideo"]);
                    objstatusDO.strPhotoPath = "";
                }
            }
            objstatusDO.strIpAddress = ip;

            if (string.IsNullOrEmpty(hdnintStatusUpdateId.Value))
            {
                objstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.Insert);
            }
            else
            {
                objstatusDO.intStatusUpdateId = Convert.ToInt32(hdnintStatusUpdateId.Value);
                objstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.Update);
            }

            if (ViewState["RegId"] == null || Convert.ToInt32(ViewState["RegId"]) == 0)
            {
                BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
            }
            else
            {
                BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), Convert.ToInt32(ViewState["RegId"]), 2);
            }

            ViewState["editVideo"] = null;
            ViewState["editImage"] = null;
            ViewState["PhotoUpload"] = null;
            ViewState["VideoUpload"] = null;
            txtPostUpdate.InnerText = "";
            lblPostMsg.Visible = false;
            hdnintStatusUpdateId.Value = "";
            Session["SubmitTime"] = DateTime.Now.ToString();
            hdnPhoto.Value = "";
            hdnDocName.Value = "";
            hdnErrorMsg.Value = "";
        }
        catch
        {

        }
    }

    protected void UploadPhoto()
    {
        try
        {
            string filepath = hdnPhoto.Value;
            string filename = hdnDocName.Value;
            string error = hdnErrorMsg.Value;
            string[] arr = filename.Split('.');
            ViewState["PhotoUpload"] = filepath;
            string ext = "." + arr[1].ToString();

            if (filename != "")
            {
                if (error == "")
                {
                    if (ext.Trim() != ".jpg" && ext.Trim() != ".jpeg" && ext.Trim() != ".png" && ext.Trim() != ".gif" && ext.Trim() != ".org" && ext.Trim() != ".bmp")
                    {
                        ViewState["PhotoUpload"] = null;
                    }
                    else
                    {
                        ViewState["PhotoUpload"] = filepath;
                    }
                }
                else
                {
                    lblfilename.Text = error;
                    lblfilename.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void UploadVideoFiles()
    {
        try
        {
            string filepath = hdnPhoto.Value;
            string filename = hdnDocName.Value;
            string error = hdnErrorMsg.Value;
            string[] arr = filename.Split('.');
            ViewState["VideoUpload"] = filepath;
            string ext = "." + arr[1].ToString();
            if (filename != "")
            {
                if (error == "")
                {
                    if (ext == ".mp4" || ext == ".wmv" || ext == ".flv" || ext == ".mpg" || ext == ".MP4" || ext == ".WMV" || ext == ".FLV" || ext == ".MPG" || ext == ".avi" || ext == ".AVI")
                    {
                        documentPath1 = filepath;
                        documentName1 = filename;
                        ViewState["VideoUpload"] = filepath;
                    }
                    else
                    {
                        ViewState["VideoUpload"] = null;
                        FileStatus = 1;
                        lblfilename.Text = "Please Choose valid image or video file.";
                        lblfilename.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
                else
                {
                    lblfilename.Text = error;
                    lblfilename.ForeColor = System.Drawing.Color.Red;
                    return;
                }

            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }

    protected void lstPostUpdates_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallEducation", "CallEduLI();", true);
        HiddenField hdnPostUpdateId = e.Item.FindControl("hdnPostUpdateId") as HiddenField;
        Label lnkLikePost = e.Item.FindControl("lnkLikePost") as Label;
        HiddenField hdnIframe = e.Item.FindControl("hdnIframe") as HiddenField;
        HtmlGenericControl frm1 = (HtmlGenericControl)e.Item.FindControl("frm1");
        HiddenField hdnRegistrationId = e.Item.FindControl("hdnRegistrationId") as HiddenField;
        HiddenField hdnSharedId = (HiddenField)e.Item.FindControl("hdnSharedId");
        LinkButton lnkLike = (LinkButton)e.Item.FindControl("lnkLike");
        ListView lstChild = e.Item.FindControl("lstChild") as ListView;
        LinkButton lnkDeleteComment = (LinkButton)e.Item.FindControl("lnkDeletePost");
        Label lblPostDescription = e.Item.FindControl("lblPostDescription") as Label;

        ViewState["hdnPostUpdateId"] = Convert.ToString(hdnPostUpdateId.Value);
        if (e.CommandName == "Like Post")
        {
            objstatusDO.intStatusUpdateId = Convert.ToInt32(hdnPostUpdateId.Value);
            objstatusDO.intLikeDisLike = 1;//For Like
            objstatusDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];

            objstatusDO.strIpAddress = ip;
            if (lnkLike.Text == "Unlike")
            {
                objstatusDO.intLikeDisLike = 0;//For UnLike
                DataTable dttAction = new DataTable();
                dttAction = objstatusDA.GetLikeDataTable(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.UnLikeStatus);
                if (dttAction.Rows[0]["Action"].ToString() == "0")
                {
                    lnkLikePost.Text = Convert.ToString(Convert.ToInt32(lnkLikePost.Text) + 1);
                }
            }
            else
            {
                DataTable dtAction = new DataTable();
                dtAction = objstatusDA.GetLikeDataTable(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.LikeStatus);
                if (dtAction.Rows[0]["Action"].ToString() == "1")
                {
                    lnkLikePost.Text = Convert.ToString(Convert.ToInt32(lnkLikePost.Text) + 1);
                }
            }
        }
        else
            if (e.CommandName == "EnterComment")
            {
                string str = Convert.ToString(e.CommandArgument);
                try
                {
                    TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");
                    if (txtComment.Text != "" && txtComment.Text != "Write a comment" && txtComment.Text != null)
                    {
                        objstatusDO.intStatusUpdateId = Convert.ToInt32(hdnPostUpdateId.Value);
                        objstatusDO.strComment = txtComment.Text;
                        objstatusDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            ip = Request.ServerVariables["REMOTE_ADDR"];
                        objstatusDO.strIpAddress = ip;
                        if (string.IsNullOrEmpty(lblintCommentId.Text.Trim()))
                        {
                            objstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.AddComment);

                            if (ISAPIURLACCESSED != "0")
                            {

                                string UserURL = APIURL + "addComment.action?commentId=" + objstatusDO.intCommentId +
                                    "&docMacroTagUid=" + null +
                                    "&commentByUid=" + objstatusDO.intAddedBy +
                                    "&insertDt=" + DateTime.Now.ToString() +
                                    "&content=" + objstatusDO.strComment;
                                try
                                {
                                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                                    myRequest1.Method = "GET";
                                    if (ISAPIResponse != "0")
                                    {
                                        WebResponse myResponse1 = myRequest1.GetResponse();
                                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                                        String result = sr.ReadToEnd();

                                        objAPILogDO.strURL = UserURL;
                                        objAPILogDO.strAPIType = "Wall Comment";
                                        objAPILogDO.strResponse = result;
                                        objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                                    }
                                }
                                catch { }
                            }
                        }
                        else
                        {
                            objstatusDO.intCommentId = Convert.ToInt32(lblintCommentId.Text.Trim());
                            objstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.UpdateComment);
                            lblintCommentId.Text = "";
                        }
                        txtComment.Text = "";
                        if (ViewState["RegId"] == null || ViewState["RegId"].ToString() == "0")
                        {
                            BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
                        }
                        else
                        {
                            BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), Convert.ToInt32(ViewState["RegId"]), 2); //frnd wall
                        }
                    }
                }
                catch
                {

                }
            }else
        if (e.CommandName == "video")
        {
            frm1.Attributes.Add("src", hdnIframe.Value);
        }else
        if (e.CommandName == "Comment")
        {
            TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");
            string ID = txtComment.ClientID;
        }
        else
        if (e.CommandName == "Edit Post")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", " CallHomeMiddle();", true);
            dt.Clear();
            objstatusDO.intStatusUpdateId = Convert.ToInt32(hdnPostUpdateId.Value);
            hdnintStatusUpdateId.Value = Convert.ToString(hdnPostUpdateId.Value);
            dt = objstatusDA.GetDataTable(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.SingleRecord);
            if (dt.Rows.Count > 0)
            {
                txtPostUpdate.InnerText = Convert.ToString(dt.Rows[0]["strPostDescription"]);
                ViewState["editImage"] = Convert.ToString(dt.Rows[0]["strPhotoPath"]);
                ViewState["editVideo"] = Convert.ToString(dt.Rows[0]["strVideoPath"]);
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["strPostLink"])))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "123", "showImage('4');", true);
                }
            }
        }
        else if (e.CommandName == "Details")
        {
            Response.Redirect("Home.aspx?RegId=" + hdnRegistrationId.Value);
        }
    }

    protected void lstPostUpdates_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataTable dtchild = new DataTable();
        DataTable dtLike = new DataTable();
        HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
        HtmlImage imgPhoto = (HtmlImage)e.Item.FindControl("imgPhoto");
        HiddenField hdnVideoName = (HiddenField)e.Item.FindControl("hdnVideoName");
        HiddenField hdnRegistrationId = e.Item.FindControl("hdnRegistrationId") as HiddenField;
        HiddenField hdnPostUpdateId = e.Item.FindControl("hdnPostUpdateId") as HiddenField;
        LinkButton lnkDeleteComment = (LinkButton)e.Item.FindControl("lnkDeletePost");
        Label lnkLikePost = e.Item.FindControl("lnkLikePost") as Label;
        HtmlGenericControl dvVideo = (HtmlGenericControl)e.Item.FindControl("dvVideo");
        HtmlGenericControl divChrome = (HtmlGenericControl)e.Item.FindControl("divChrome");
        HyperLink hplLinkUrl = (HyperLink)e.Item.FindControl("hplLinkUrl");
        LinkButton lnkEditPost = (LinkButton)e.Item.FindControl("lnkEditPost");
        HiddenField hdnPhotoPath = e.Item.FindControl("hdnPhotoPath") as HiddenField;
        HiddenField hdnPostLink = (HiddenField)e.Item.FindControl("hdnPostLink");
        LinkButton lnkLike = (LinkButton)e.Item.FindControl("lnkLike");
        HiddenField hdnPostLikeUserId = (HiddenField)e.Item.FindControl("hdnPostLikeUserId");
        HiddenField hdnimgprofile = (HiddenField)e.Item.FindControl("hdnimgprofile");
        HtmlGenericControl editUser = (HtmlGenericControl)e.Item.FindControl("editUser");
        HtmlImage imgComment = (HtmlImage)e.Item.FindControl("imgComment");
        HiddenField hdnstrPostDescription = (HiddenField)e.Item.FindControl("hdnstrPostDescription");
        Label lblPostDescription = (Label)e.Item.FindControl("lblPostDescription");

        string stt = hdnstrPostDescription.Value.Replace("\n", "<br>");
        lblPostDescription.Text = stt;

        if (ViewState["imgComment"] != null)
            imgComment.Src = ViewState["imgComment"].ToString();

        if (String.IsNullOrEmpty(hplLinkUrl.Text) || (hplLinkUrl.Text == "http://"))
            hplLinkUrl.Visible = false;


        if (hdnVideoName.Value == "" || hdnVideoName.Value == null)
        {
            dvVideo.Style.Add("display", "none");
            divChrome.Style.Add("display", "none");
        }
        else
        {
            if (Convert.ToString(ViewState["BrowserName"]) == "Chrome" || Convert.ToString(ViewState["BrowserName"]) == "Firefox")
                dvVideo.Style.Add("display", "none");
            else
                divChrome.Style.Add("display", "none");
            //Firefox
        }

        int index = e.Item.DataItemIndex;
        ListViewDataItem item = (ListViewDataItem)e.Item;
        int PostId = Convert.ToInt32(hdnPostUpdateId.Value);

        if (imgprofile.Src == "CroppedPhoto/")
        {
            imgprofile.Src = "images/comment-profile.jpg";
        }
        else
        {

            string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + hdnimgprofile.Value);
            if (File.Exists(imgPathPhysical))
            {
            }
            else
            {
                imgprofile.Src = "images/comment-profile.jpg";
            }

        }

        if (imgPhoto.Src == "UploadedPhoto/")
        {
            imgPhoto.Visible = false;
        }
        else
        {
            string imgPathPhysical = Server.MapPath("~/" + imgPhoto.Src);
            if (!File.Exists(imgPathPhysical))
            {
                imgPhoto.Src = "images/no_image.jpg";
            }

        }

        if (hdnRegistrationId.Value == Convert.ToString(Session["ExternalUserId"]))
        {
            editUser.Style.Add("display", "block");
            lnkDeleteComment.Visible = true;
            if (imgPhoto.Src == "UploadedPhoto/" && string.IsNullOrEmpty(hdnVideoName.Value))
            {
                lnkEditPost.Visible = true;
            }
        }

        ListView lstChild = (ListView)e.Item.FindControl("lstChild");
        objstatusDO.intStatusUpdateId = PostId;
        dtchild = objstatusDA.GetDataTable(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.BindChildList);

        if (dtchild.Rows.Count > 0)
        {
            lstChild.DataSource = dtchild;
            lstChild.DataBind();
        }

        lnkLikePost.ToolTip = "View Likes";
        objstatusDO.intStatusUpdateId = Convert.ToInt32(hdnPostUpdateId.Value);
        dtLike = objstatusDA.GetDataTable(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.GetPostLikeUserLists);
        if (dtLike.Rows.Count > 0)
        {
            for (int i = 0; i < dtLike.Rows.Count; i++)
            {
                if (lnkLikePost.ToolTip != "View Likes")
                    lnkLikePost.ToolTip += Convert.ToString(dtLike.Rows[i]["UserName"]) + Environment.NewLine;
                else
                    lnkLikePost.ToolTip = Convert.ToString(dtLike.Rows[i]["UserName"]) + Environment.NewLine;
            }
        }
    }

    protected void lstPostUpdates_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        ListView lstChild = e.Item.FindControl("lstChild") as ListView;
        lstChild.ItemCommand += new EventHandler<ListViewCommandEventArgs>(lstChild_ItemCommand);
        lstChild.ItemDataBound += new EventHandler<ListViewItemEventArgs>(lstChild_ItemDataBound);
    }

    protected void lstChild_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataTable dtChildLike = new DataTable();
        HiddenField hdnCommentId = e.Item.FindControl("hdnCommentId") as HiddenField;
        HiddenField hdnRegistrationId = e.Item.FindControl("hdnRegistrationId") as HiddenField;
        Label lnkLikeComment = (Label)e.Item.FindControl("lnkLikeComment");
        HtmlImage imgCommentpic = (HtmlImage)e.Item.FindControl("imgCommentpic");
        HiddenField hdnCommentLikeUserId = e.Item.FindControl("hdnCommentLikeUserId") as HiddenField;
        LinkButton lnkLikes = e.Item.FindControl("lnkLikes") as LinkButton;
        HiddenField hdnimgprofile = e.Item.FindControl("hdnimgprofile") as HiddenField;
        HtmlGenericControl editUserComment = e.Item.FindControl("editUserComment") as HtmlGenericControl;

        if (imgCommentpic.Src == "CroppedPhoto/")
        {
            imgCommentpic.Src = "images/comment-by.jpg";
        }
        else
        {
            string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + hdnimgprofile.Value);
            if (File.Exists(imgPathPhysical))
            {
            }
            else
            {
                imgCommentpic.Src = "images/comment-profile.jpg";
            }
        }

        if (hdnRegistrationId.Value == Convert.ToString(Session["ExternalUserId"]))
        {
            editUserComment.Style.Add("display", "block");
        }

        lnkLikeComment.ToolTip = "View Likes";
        objstatusDO.intCommentId = Convert.ToInt32(hdnCommentId.Value);
        dtChildLike = objstatusDA.GetLikeDataTable(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.GetCommentLikeUserLists);
        if (dtChildLike.Rows.Count > 0)
        {
            for (int i = 0; i < dtChildLike.Rows.Count; i++)
            {
                if (lnkLikeComment.ToolTip != "View Likes")
                    lnkLikeComment.ToolTip += Convert.ToString(dtChildLike.Rows[i]["UserName"]) + Environment.NewLine;
                else
                    lnkLikeComment.ToolTip = Convert.ToString(dtChildLike.Rows[i]["UserName"]) + Environment.NewLine;
            }
        }
    }

    protected void lstChild_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnCommentId = e.Item.FindControl("hdnCommentId") as HiddenField;
        HiddenField hdnintUserTypeId = e.Item.FindControl("hdnintUserTypeId") as HiddenField;
        HiddenField hdnRegistrationId = e.Item.FindControl("hdnRegistrationId") as HiddenField;
        Label lnkLikeComment = e.Item.FindControl("lnkLikeComment") as Label;
        LinkButton lnkLike = e.Item.FindControl("lnkLike") as LinkButton;
        LinkButton lnkEditComment = e.Item.FindControl("lnkEditComment") as LinkButton;
        LinkButton lnkDeleteComment = e.Item.FindControl("lnkDeleteComment") as LinkButton;
        HiddenField intUserId = e.Item.FindControl("intUserId") as HiddenField;
        HiddenField hdnCommentLikeUserId = e.Item.FindControl("hdnCommentLikeUserId") as HiddenField;
        LinkButton lnkLikes = e.Item.FindControl("lnkLikes") as LinkButton;
        Label lblstr = e.Item.FindControl("lblstr") as Label;

        lblintCommentId.Text = "";

        if (e.CommandName == "Like Comment")
        {
            objstatusDO.intCommentId = Convert.ToInt32(hdnCommentId.Value);
            objstatusDO.intLikeDisLike = 1;//For Like
            objstatusDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objstatusDO.strIpAddress = ip;


            if (lnkLikes.Text == "Unlike")
            {
                DataTable dtAction = new DataTable();
                dtAction = objstatusDA.GetLikeDataTable(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.UnAddLike);
                if (Convert.ToString(dtAction.Rows[0]["Action"]) == "0")
                {
                    lnkLikeComment.Text = Convert.ToString(Convert.ToInt32(lnkLikeComment.Text) + 1);
                }

                if (ISAPIURLACCESSED != "0")
                {

                    string UserURL = APIURL + "dislikeUserComment.action?userId =" + hdnRegistrationId.Value +
                        "&commentId=" + objstatusDO.intCommentId;
                    try
                    {
                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                        myRequest1.Method = "GET";
                        if (ISAPIResponse != "0")
                        {
                            WebResponse myResponse1 = myRequest1.GetResponse();
                            StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                            String result = sr.ReadToEnd();

                            objAPILogDO.strURL = UserURL;
                            objAPILogDO.strAPIType = "Wall User Comment UnLike";
                            objAPILogDO.strResponse = result;
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                            objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                        }
                    }
                    catch { }

                }

            }
            else
            {
                DataTable dtAction = new DataTable();
                dtAction = objstatusDA.GetLikeDataTable(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.AddLike);
                if (Convert.ToString(dtAction.Rows[0]["Action"]) == "1")
                {
                    lnkLikeComment.Text = Convert.ToString(Convert.ToInt32(lnkLikeComment.Text) + 1);
                }


                if (ISAPIURLACCESSED != "0")
                {

                    string UserURL = APIURL + "likeUserComment.action?userId =" + hdnRegistrationId.Value +
                        "&commentId=" + objstatusDO.intCommentId;
                    try
                    {
                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                        myRequest1.Method = "GET";
                        if (ISAPIResponse != "0")
                        {
                            WebResponse myResponse1 = myRequest1.GetResponse();
                            StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                            String result = sr.ReadToEnd();

                            objAPILogDO.strURL = UserURL;
                            objAPILogDO.strAPIType = "Wall User Comment Like";
                            objAPILogDO.strResponse = result;
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                            objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                        }
                    }
                    catch { }

                }

            }

        }

        if (e.CommandName == "Edit Comment")
        {
            objstatusDO.intCommentId = Convert.ToInt32(hdnCommentId.Value);
            lblintCommentId.Text = Convert.ToString(hdnCommentId.Value);
            DataTable dtEdit = new DataTable();
            dtEdit = objstatusDA.GetLikeDataTable(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.GetComment);
            if (dtEdit.Rows.Count > 0)
            {
                var item = ((Control)sender).NamingContainer as ListViewItem;
                if (item != null)
                {
                    TextBox txtComment = ((TextBox)item.FindControl("txtComment"));
                    txtComment.Text = Convert.ToString(dtEdit.Rows[0]["strComment"]);
                }
            }
        }
        if (e.CommandName == "Post Comment Details")
        {
            Response.Redirect("Home.aspx?RegId=" + intUserId.Value);
        }
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (hdnintPostId.Value != "")
        {
            objstatusDO.intStatusUpdateId = Convert.ToInt32(hdnintPostId.Value);
            objstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.Delete);
            objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.intActionId = Convert.ToInt32(hdnintPostId.Value);
            objLog.strAction = "Wall Post";
            objLog.strActionName = hdnstrPostDescriptiondele.Value;
            objLog.strIPAddress = ip;
            objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.SectionId = 1;   // User Wall Post
            objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
            BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
            Session.Remove("PostDelete");
            Session.Remove("hdnPostUpdateId");
            Session.Remove("lblPostDescription");
            hdnintPostId.Value = "";
            hdnstrPostDescriptiondele.Value = "";
            divDeletesucess.Style.Add("display", "none");
        }
        else if (hdnintPostceIdelet.Value != "")
        {
            objstatusDO.intCommentId = Convert.ToInt32(hdnintPostceIdelet.Value);
            DataTable dtDelete = new DataTable();
            dtDelete = objstatusDA.GetLikeDataTable(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.DeleteComment);
            if (ISAPIURLACCESSED != "0")
            {
                string UserURL = APIURL + "removeComment.action?userId =" + ViewState["UserID"] +
                    "&commentId=" + objstatusDO.intCommentId;
                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = UserURL;
                        objAPILogDO.strAPIType = "Wall User remove Comment";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }

            }
            divDeletesucess.Style.Add("display", "none");
            if (ViewState["RegId"] == null || ViewState["RegId"].ToString() == "0")
            {
                BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
            }
            else
            {
                BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), Convert.ToInt32(ViewState["RegId"]), 2); //frnd wall
            }

            objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.intActionId = Convert.ToInt32(hdnintPostceIdelet.Value);
            objLog.strAction = "Wall Post Comment";
            objLog.strActionName = hdnstrPostDescriptioncedel.Value;
            objLog.strIPAddress = ip;
            objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.SectionId = 1;   // User Wall Post 
            objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
            hdnintPostceIdelet.Value = "";
            hdnstrPostDescriptioncedel.Value = "";
        }
        else if (hdnintdocIdelete.Value != "")
        {
            objDoProDocs.DocId = Convert.ToInt32(hdnintdocIdelete.Value);
            objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.DeleteDocument);
            string PathPhysical = Server.MapPath("~/UploadDocument/" + Convert.ToString(hdnfilestrFilePathe.Value));
            if (File.Exists(PathPhysical))
            {
                File.Delete(PathPhysical);
            }

            objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.intActionId = Convert.ToInt32(hdnintdocIdelete.Value);
            objLog.strAction = "Document";
            objLog.strActionName = hdnstrdocDescriptiondele.Value;
            objLog.strIPAddress = ip;
            objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.SectionId = 10;   // Document
            objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
            hdnintdocIdelete.Value = "";
            hdnstrdocDescriptiondele.Value = "";
            hdnfilestrFilePathe.Value = "";
            BindDocuments();
            divDeletesucess.Style.Add("display", "none");
        }
        else if (hdnintworkeIdelet.Value != "")
        {
            AddWorkExp.Style.Add("display", "none");
            objUserExpDO.intExperienceId = Convert.ToInt32(hdnintworkeIdelet.Value);
            objUserExpDO.dtFromDate = DateTime.Now;
            objUserExpDO.dtToDate = DateTime.Now;
            objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.Delete);
            LoadUserExp();
            TotalscoreCount();
            hdnintworkeIdelet.Value = "";
            divDeletesucess.Style.Add("display", "none");
        }
        else if (hdnintedueIdelet.Value != "")
        {
            objDOEdu.intEducationId = Convert.ToInt32(hdnintedueIdelet.Value);
            objDOEdu.dtDate = DateTime.Now;
            objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.Delete);
            LoadEducation();
            hdnintedueIdelet.Value = "";
        }
        else if (hdnintacheIdelet.Value != "")
        {
            objDOEdu.intAchivmentId = Convert.ToInt32(hdnintacheIdelet.Value);
            objDOEdu.dtDate = DateTime.Now;
            objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.DeleteAchivement);
            LoadAchivment();
            hdnintacheIdelet.Value = "";
            divDeletesucess.Style.Add("display", "none");
        }

        hdnintPostId.Value = "";
        hdnintPostceIdelet.Value = "";
        hdnintacheIdelet.Value = "";
        hdnintedueIdelet.Value = "";
        hdnintdocIdelete.Value = "";
        divDeletesucess.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallEducation", "CallEduLI();", true);

    }

    protected void lnkDeleteCancel_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
    }

    protected void lnkConnDisconn_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objstatusDO.intStatusUpdateId = Convert.ToInt32(ViewState["hdnPostUpdateId"]);
        objstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.Delete);
        BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall

        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(ViewState["hdnPostUpdateId"]);
        objLog.strAction = "Wall Post";
        objLog.strActionName = "";
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 1;   // User Wall Post
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
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
            if (ViewState["RegId"] == null || ViewState["RegId"].ToString() == "0")
            {
                BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
            }
            else
            {
                BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), Convert.ToInt32(ViewState["RegId"]), 2); //frnd wall
            }
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
        imgLoadMore.Style.Add("display", "none");
        //pLoadMore.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallHome1", "CallHomeLI();", true);

    }

    #region Paging For Home

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
        BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
    }

    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = "1";
        BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
    }

    protected void lnkLast_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = hdnLastPage.Value;
        BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
    }

    protected void lnkPrevious_Click(object sender, EventArgs e)
    {

        hdnCurrentPage.Value = hdnPreviousPage.Value;
        BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
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
                BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall

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

    #endregion

    #region Document Section
    private void BindDocuments()
    {

        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            lnkuploadDoc.Visible = false;
            DocDO.AddedBy = Convert.ToInt32(ViewState["RegId"]);
            DocDO.FriendId = Convert.ToInt32(Session["ExternalUserId"]);
            dt = DocDA.GetDataTable(DocDO, DA_ProfileDocuments.DocumenTemp.GetPublicDocsForFriend);
        }
        else
        {
            DocDO.AddedBy = Convert.ToInt32(ViewState["UserID"]);
            dt = DocDA.GetDataTable(DocDO, DA_ProfileDocuments.DocumenTemp.Getdocuments);
        }

        if (dt.Rows.Count > 0)
        {
            LstDocument.DataSource = dt;
            LstDocument.DataBind();
        }
        else
        {
            LstDocument.DataSource = null;
            LstDocument.DataBind();
        }
    }

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

    protected void LstDocument_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataTable dtIsDownload = new DataTable();
        HiddenField hdnDocId = (HiddenField)e.Item.FindControl("hdnDocId");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HtmlControl editUserComment = (HtmlControl)e.Item.FindControl("editUserComment");
        HiddenField hdnstrFilePath = (HiddenField)e.Item.FindControl("hdnstrFilePath");
        HiddenField hdnIsDocsDownload = (HiddenField)e.Item.FindControl("hdnIsDocsDownload");
        HyperLink lblDocument1 = (HyperLink)e.Item.FindControl("lblDocument1");
        Label lblDocument = (Label)e.Item.FindControl("lblDocument");

        if (hdnintAddedBy.Value != ViewState["UserID"].ToString())
        {
            editUserComment.Style.Add("display", "none");
        }

        if (hdnIsDocsDownload.Value != "Y")
        {
            if (Convert.ToString(hdnintAddedBy.Value) != Convert.ToString(ViewState["UserID"]))
            {
                lblDocument.Style.Add("display", "block");
                lblDocument1.Style.Add("display", "none");
            }
        }
    }

    protected void LstDocument_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnDocId = (HiddenField)e.Item.FindControl("hdnDocId");
        HiddenField hdnstrFilePath = (HiddenField)e.Item.FindControl("hdnstrFilePath");
        HiddenField hdnIsDocsDownload = (HiddenField)e.Item.FindControl("hdnIsDocsDownload");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HiddenField hdnstrDocTitle = (HiddenField)e.Item.FindControl("hdnstrDocTitle");
        if (e.CommandName == "EditDocs")
        {
            PopUpCropImage.Style.Add("display", "none");
            BindEditSubjectList(hdnDocId.Value);
            BindEditTopSubjectList(hdnDocId.Value);
            divDocumentUplaod.Style.Add("display", "block");
            ViewState["DocIdEdit"] = hdnDocId.Value;
            objDoProDocs.DocId = Convert.ToInt32(hdnDocId.Value);
            DataTable dtDoc = objDAProDocs.GetDataTable(objDoProDocs, DA_ProfileDocuments.Document.SelectDoc);
            if (dtDoc.Rows.Count > 0)
            {
                txtDocTitle.Text = dtDoc.Rows[0]["strDocTitle"].ToString();
                txtDescrition.InnerText = dtDoc.Rows[0]["strDescrition"].ToString();
                if (dtDoc.Rows[0]["IsDocsDownload"].ToString() == "Y")
                {
                    imgDownload.Src = "images/chk1.png";
                    hdnimgDownload.Value = "1";
                }
                else
                {
                    imgDownload.Src = "images/unchk1.png";
                    hdnimgDownload.Value = "0";
                }
                if (dtDoc.Rows[0]["intDocsSee"].ToString() == "Public")
                {
                    imgPrivate.Src = "images/unchk1.png";
                    hdnimgPrivate.Value = "0";
                }
                else
                {
                    imgPrivate.Src = "images/chk1.png";
                    hdnimgPrivate.Value = "1";
                }

                if (dtDoc.Rows[0]["strFilePath"].ToString() != "")
                {
                    lblfilenamee.Text = dtDoc.Rows[0]["strDocName"].ToString();
                    lblfilenamee.Style.Add("color", "black");
                    hdnUploadFile.Value = dtDoc.Rows[0]["strFilePath"].ToString();
                    hdnUploadFile1.Value = dtDoc.Rows[0]["strDocName"].ToString();
                    lnkDeleteDoc123.Style.Add("display", "block");
                }
                else
                {
                    lblfilenamee.Text = "Uploaded file not found.";
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallDocMiddle();", true);
        }
        if (e.CommandName == "DownloadDoc")
        {
            if (hdnIsDocsDownload.Value == "Y")
            {

                //try
                //{
                //    //HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                //    //HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                //    //HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                //    // Clear the content of the response
                //    HttpContext.Current.Response.ClearContent();
                //    HttpContext.Current.Response.ClearHeaders();
                //    // Buffer response so that page is sent
                //    // after processing is complete.
                //    HttpContext.Current.Response.BufferOutput = true;
                //    // Add the file name and attachment,
                //    // which will force the open/cance/save dialog to show, to the header
                //    string fileName = "~/UploadDocument/" + hdnstrFilePath.Value;
                //    if (File.Exists(Request.PhysicalApplicationPath + fileName) || File.Exists(Server.MapPath(fileName)))
                //    {
                //        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + hdnstrFilePath.Value);
                //        HttpContext.Current.Response.ContentType = "application/octet-stream";
                //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //        HttpContext.Current.Response.Write(fileName);
                //        ////HttpContext.Current.Response.End();
                //        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                //        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                //        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                //        HttpContext.Current.Response.End();
                //    }
                //    else
                //    {
                //        if (Request.UrlReferrer != null)
                //        {
                //            Type csType = GetType();
                //            string jsScript = "alert('File Not Found');";
                //            ScriptManager.RegisterClientScriptBlock(Page, csType, "popup", jsScript, true);
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    string errorMsg = ex.Message;
                //}

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DocLI", "CallDocLI();", true);
        }

    }

    protected void LstSubjCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lnkCatName = (Label)e.Item.FindControl("lnkCatName");
        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
        HiddenField hdnCountSub = (HiddenField)e.Item.FindControl("hdnCountSub");

        SubLi.Attributes.Add("class", "graycreateGroup");
        if (hdnCountSub.Value == "1")
        {
            SubLi.Attributes.Add("class", "selectedcreateGroup");
            lnkCatName.Style.Add("color", "#FFFFFF !important");
        }
        else
        {
            SubLi.Attributes.Add("class", "graycreateGroup");
        }
    }

    private void BindEditTopSubjectList(string Id)
    {
        DataTable dtTopSub = new DataTable();
        objCategory.intPostQuestionId = Convert.ToInt32(Id);
        dtTopSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.GetEditDocSubjectList);
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

    protected void BindEditSubjectList(string DocId)
    {
        DocDO.AddedBy = Convert.ToInt32(ViewState["RegId"]);
        DocDO.DocId = Convert.ToInt32(DocId);
        DataTable dtSub = DocDA.GetDataTable(DocDO, DA_ProfileDocuments.DocumenTemp.GetDocSubcategory);
        string subval = "";
        if (dtSub.Rows.Count > 0)
        {
            for (int i = 0; i < dtSub.Rows.Count; i++)
            {
                if (subval == "")
                {
                    subval = dtSub.Rows[i]["intCategoryId"].ToString();
                }
                else
                {
                    subval += "," + dtSub.Rows[i]["intCategoryId"].ToString();
                }
            }
            hdnSubject.Value = subval;

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string FilePath = hdnFilePath.Value;
        string DocName = hdnDocName.Value;
        string Error = hdnErrorMsg.Value;

        string docPath = "";
        string filename = hdnUploadFile.Value;

        #region File Upload Comment
        lblfilenamee.Text = "";
        if (DocName != "")
        {
            objDoProDocs.strDocName = DocName;
            if (ViewState["strDocName"] == null)
            {

                ViewState["docPath"] = FilePath;

            }
        }
        else if (hdnUploadFile.Value != "")
        {
            objDoProDocs.strDocName = hdnUploadFile1.Value;
            ViewState["docPath"] = hdnUploadFile.Value;
        }
        else
        {
            if (Error != "")
            {
                lblfilenamee.Text = Error;
                lblfilenamee.CssClass = "RedErrormsg";
                hdnErrorMsg.Value = "";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "callDocsave", "callDocSave();", true);
                lblfilenamee.Text = "Please select file to upload.";
                lblfilenamee.CssClass = "RedErrormsg";
            }

            return;
        }
        #endregion

        if (txtDocTitle.Text != "Title")
        {
            objDoProDocs.DocTitle = txtDocTitle.Text.Trim().Replace("'", "''");
        }
        if (txtDocTitle.Text != "Description")
        {
            objDoProDocs.strDescrition = txtDescrition.InnerText.Trim().Replace("'", "''");
        }
        if (Session["docPathNew"] != null)
        {
            objDoProDocs.FilePath = docPath;

        }
        else
        {
            objDoProDocs.FilePath = Convert.ToString((ViewState["docPath"]));
        }

        if (hdnimgPrivate.Value == "1")
        {
            objDoProDocs.intDocsSee = "Private";
        }
        else
        {
            objDoProDocs.intDocsSee = "Public";
        }


        if (hdnimgDownload.Value == "1")
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

        objDoProDocs.intDocumentTypeID = 1;
        objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (ViewState["DocIdEdit"] != null)
        {
            objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
            objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.Update);
        }
        else
        {
            objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.Add);
        }

        ViewState["DocId"] = objDoProDocs.DocId;

        if (hdnSubject.Value != "")
        {
            if (ViewState["DocIdEdit"] != null)
            {
                objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.DeleteTempTable);
            }

            string[] totalSubjects = hdnSubject.Value.Split(',');
            var dictionary = new Dictionary<int, int>();

            Dictionary<string, int> counts = totalSubjects.GroupBy(x => x)
                                          .ToDictionary(g => g.Key,
                                                        g => g.Count());

            foreach (var v in counts)
            {
                if (v.Value == 1)
                {
                    if (ViewState["DocIdEdit"] != null)
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    }
                    else
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
                    }
                    objDoProDocs.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
                }
                else if (v.Value == 3)
                {
                    if (ViewState["DocIdEdit"] != null)
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    }
                    else
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
                    }
                    objDoProDocs.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
                }
                else if (v.Value == 5)
                {

                    if (ViewState["DocIdEdit"] != null)
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    }
                    else
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
                    }
                    objDoProDocs.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
                }
                else if (v.Value == 7)
                {

                    if (ViewState["DocIdEdit"] != null)
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    }
                    else
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
                    }
                    objDoProDocs.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
                }
                else if (v.Value == 9)
                {
                    if (ViewState["DocIdEdit"] != null)
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    }
                    else
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
                    }
                    objDoProDocs.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
                }

            }
        }

        clearDoc();
        BindDocuments();
        Session["SubmitTime"] = DateTime.Now.ToString();
        ViewState["docPathNew"] = null;
        divDocumentUplaod.Style.Add("display", "block");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DocLI12", "CallDocLI();", true);
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
        objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.UpdateDocumentByDocId);
        uploadDoc.Visible = true;
        try
        {
            string imgPathPhysical = Server.MapPath("~/UploadDocument/" + hdnUploadFile.Value.ToString());
            if (File.Exists(imgPathPhysical))
            {
                File.Delete(imgPathPhysical);
                lnkDeleteDoc123.Style.Add("display", "none");
                lblfilenamee.Text = "";
                hdnUploadFile.Value = "";
                hdnUploadFile1.Value = "";
            }

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }

    protected void lnkuploadDoc_click(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        divDocumentUplaod.Style.Add("display", "block");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DocLI", "CallDocLI();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallDocMiddle();", true);
    }

    protected void imgPrivate_Click(object sender, EventArgs e)
    {
        hdnSubject.Value = "";
        string imgName = imgPrivate.Src;
        if (imgPrivate.Src == "images/unchk1.png")
        {
            imgPrivate.Src = "images/chk1.png";
        }
        else
        {
            imgPrivate.Src = "images/unchk1.png";
        }
    }

    protected void imgDownload_Click(object sender, EventArgs e)
    {
        hdnSubject.Value = "";
        string imgName = imgPrivate.Src;
        if (imgDownload.Src == "images/chk1.png")
        {
            imgDownload.Src = "images/unchk1.png";
        }
        else
        {
            imgDownload.Src = "images/chk1.png";
        }

    }

    protected void uploadDoc_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        string docPath = "";
        lblfilenamee.Text = "";
        string path = e.filename;
        Session["docPathNew"] = e.filename;
        int FileLength = Convert.ToInt32(e.filesize);
        string ext = System.IO.Path.GetExtension(e.filename);
        if (ext.Trim() == ".jpg" || ext.Trim() == ".jpeg" || ext.Trim() == ".png" || ext.Trim() == ".gif")
        {
            lblfilenamee.Text = "File format not supported.";
            lblfilenamee.CssClass = "RedErrormsg";
            return;
        }
        if (FileLength <= 3145728)
        {
            docPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(uploadDoc.FileName).ToString();
            uploadDoc.SaveAs(Server.MapPath("~\\UploadDocument\\" + docPath));
            Session["docPath"] = docPath;
        }
        else
        {
            lblfilenamee.Text = "File size should be less than or equal to 3MB";
            lblfilenamee.CssClass = "RedErrormsg";
            return;
        }
    }

    protected void clearDoc()
    {
        hdnDocName.Value = "";
        hdnFilePath.Value = "";
        hdnErrorMsg.Value = "";
        hdnSubject.Value = "";
        hdnimgDownload.Value = "1";
        hdnimgPrivate.Value = "0";
        // ViewState["SubjectCategory"] = null;
        txtDocTitle.Text = "";
        BindSubjectList();
        txtDescrition.InnerText = "";
        lblfilenamee.Text = "";
        lnkDeleteDoc123.Style.Add("display", "none");
        ViewState["Callsmoothmenu"] = "smooth";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "groupConnChange();", true);
    }

    protected void lnkCancelDoc_Click(object sender, EventArgs e)
    {
        clearDoc();
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        divDocumentUplaod.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DocLI", "CallDocLI();", true);
    }

    #endregion

    #region UserExp And Skill
    protected void LoadUserExp()
    {
        objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            lnkuploadDoc.Visible = false;
            aAddworkExp.Visible = false;
            objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["RegId"]);
            DataTable dtExp = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.AllRecords);
            if (dtExp.Rows.Count > 0)
            {
                lstWorkExperience.DataSource = dtExp;
                lstWorkExperience.DataBind();
            }
            else
            {
                lstWorkExperience.DataSource = null;
                lstWorkExperience.DataBind();
            }
        }
        else
        {
            DataTable dtExp = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.AllRecords);
            if (dtExp.Rows.Count > 0)
            {
                lstWorkExperience.DataSource = dtExp;
                lstWorkExperience.DataBind();
            }
            else
            {
                lstWorkExperience.DataSource = null;
                lstWorkExperience.DataBind();
            }
        }
    }

    protected void LoadUserexpYear()
    {
        DataTable dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.LoadYear);
        if (dt.Rows.Count > 0)
        {
            txtFromYear.DataSource = dt;
            txtFromYear.DataTextField = "intYear";
            txtFromYear.DataValueField = "intYear";
            txtFromYear.DataBind();
            txtFromYear.Items.Insert(0, new ListItem("Year"));

            txtToYear.DataSource = dt;
            txtToYear.DataTextField = "intYear";
            txtToYear.DataValueField = "intYear";
            txtToYear.DataBind();
            txtToYear.Items.Insert(0, new ListItem("Year"));

        }

    }

    protected void lstWorkExperience_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HtmlControl divUserexperenceED = (HtmlControl)e.Item.FindControl("divUserexperenceED");
        if (hdnintAddedBy.Value != ViewState["UserID"].ToString())
        {
            divUserexperenceED.Style.Add("display", "none");
        }
    }

    protected void lstWorkExperience_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnintExperienceId = (HiddenField)e.Item.FindControl("hdnintExperienceId");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        objUserExpDO.intExperienceId = Convert.ToInt32(hdnintExperienceId.Value);
        if (e.CommandName == "EditExp")
        {
            PopUpCropImage.Style.Add("display", "none");
            AddWorkExp.Style.Add("display", "block");
            DataTable dtuser = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.SingleRecord);

            try
            {
                if (dtuser.Rows.Count > 0)
                {
                    ViewState["UnqKey"] = Convert.ToString(dtuser.Rows[0]["strUnqId"]);
                    ViewState["hdnintExperienceId"] = hdnintExperienceId.Value;
                    txtInstituteName.Text = Convert.ToString(dtuser.Rows[0]["strCompanyName"]);
                    txtDegree.Text = Convert.ToString(dtuser.Rows[0]["strDesignation"]);
                    txtDescription.InnerText = Convert.ToString(dtuser.Rows[0]["strDescription"]);

                    if (string.IsNullOrEmpty(Convert.ToString(dtuser.Rows[0]["bitAtPresent"])))
                        chkAtPresent.Checked = false;
                    else
                        if (Convert.ToBoolean(dtuser.Rows[0]["bitAtPresent"]) == true)
                            chkAtPresent.Checked = true;
                        else
                            chkAtPresent.Checked = false;

                    SelectMonth(dtuser);
                    txtFromYear.Text = Convert.ToString(dtuser.Rows[0]["intFromYear"]);
                    txtToYear.Text = Convert.ToString(dtuser.Rows[0]["intToYear"]);

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallWorkMiddle();", true);
            LoadUserAreaSkill();
        }
    }

    public void SelectMonth(DataTable dtt)
    {
        if (dtt.Rows[0]["inFromtMonth"].ToString() == "1")
        {
            fromMonth.Value = "Jan";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "2")
        {
            fromMonth.Value = "Feb";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "3")
        {
            fromMonth.Value = "Mar";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "4")
        {
            fromMonth.Value = "Apr";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "5")
        {
            fromMonth.Value = "May";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "6")
        {
            fromMonth.Value = "Jun";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "7")
        {
            fromMonth.Value = "Jul";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "8")
        {
            fromMonth.Value = "Aug";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "9")
        {
            fromMonth.Value = "Sep";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "10")
        {
            fromMonth.Value = "Oct";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "11")
        {
            fromMonth.Value = "Nov";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "12")
        {
            fromMonth.Value = "Dec";
        }
        else
        {
            fromMonth.Value = "Jan";
        }

        if (dtt.Rows[0]["intToMonth"].ToString() == "1")
        {
            toMonth.SelectedValue = "Jan";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "2")
        {
            toMonth.SelectedValue = "Feb";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "3")
        {
            toMonth.SelectedValue = "Mar";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "4")
        {
            toMonth.SelectedValue = "Apr";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "5")
        {
            toMonth.SelectedValue = "May";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "6")
        {
            toMonth.SelectedValue = "Jun";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "7")
        {
            toMonth.SelectedValue = "Jul";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "8")
        {
            toMonth.SelectedValue = "Aug";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "9")
        {
            toMonth.SelectedValue = "Sep";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "10")
        {
            toMonth.SelectedValue = "Oct";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "11")
        {
            toMonth.SelectedValue = "Nov";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "12")
        {
            toMonth.SelectedValue = "Dec";
        }
        else
        {
            toMonth.SelectedValue = "Jan";
        }

    }

    protected void chkAtPresent_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAtPresent.Checked == true)
        {
            txtToYear.Text = DateTime.Now.Year.ToString();
            txtToYear.Enabled = false;
            int i = DateTime.Now.Month;
            toMonth.SelectedIndex = i;
            toMonth.Enabled = false;
        }
        else
        {
            txtToYear.SelectedIndex = 0;
            txtToYear.Enabled = true;
            toMonth.Enabled = true;
            toMonth.SelectedIndex = 0;
        }
    }

    string UserTypeId = string.Empty, CurrentlyWork = string.Empty;
    protected void lnlSaveExp_Click(object sender, EventArgs e)
    {
        if (ViewState["hdnintExperienceId"] == null)
        {
            if (txtInstituteName.Text != "")
            {
                string ukey1 = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "");
                ViewState["UnqKey"] = ukey1;
                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    ip = Request.ServerVariables["REMOTE_ADDR"];
                objUserExpDO.strCompanyName = txtInstituteName.Text.Trim();
                objUserExpDO.strDesignation = txtDegree.Text.Trim();
                objUserExpDO.intToYear = Convert.ToInt32(txtToYear.Text.Trim());
                objUserExpDO.intFromYear = Convert.ToInt32(txtFromYear.Text.Trim());

                if (fromMonth.Value == "Jan")
                {
                    objUserExpDO.inFromtMonth = 1;
                }
                else if (fromMonth.Value == "Feb")
                {
                    objUserExpDO.inFromtMonth = 2;
                }
                else if (fromMonth.Value == "Mar")
                {
                    objUserExpDO.inFromtMonth = 3;
                }
                else if (fromMonth.Value == "Apr")
                {
                    objUserExpDO.inFromtMonth = 4;
                }
                else if (fromMonth.Value == "May")
                {
                    objUserExpDO.inFromtMonth = 5;
                }
                else if (fromMonth.Value == "Jun")
                {
                    objUserExpDO.inFromtMonth = 6;
                }
                else if (fromMonth.Value == "Jul")
                {
                    objUserExpDO.inFromtMonth = 7;
                }
                else if (fromMonth.Value == "Aug")
                {
                    objUserExpDO.inFromtMonth = 8;
                }
                else if (fromMonth.Value == "Sep")
                {
                    objUserExpDO.inFromtMonth = 9;
                }
                else if (fromMonth.Value == "Oct")
                {
                    objUserExpDO.inFromtMonth = 10;
                }
                else if (fromMonth.Value == "Nov")
                {
                    objUserExpDO.inFromtMonth = 11;
                }
                else if (fromMonth.Value == "Dec")
                {
                    objUserExpDO.inFromtMonth = 12;
                }

                if (toMonth.SelectedValue == "Jan")
                {
                    objUserExpDO.intToMonth = 1;
                }
                else if (toMonth.SelectedValue == "Feb")
                {
                    objUserExpDO.intToMonth = 2;
                }
                else if (toMonth.SelectedValue == "Mar")
                {
                    objUserExpDO.intToMonth = 3;
                }
                else if (toMonth.SelectedValue == "Apr")
                {
                    objUserExpDO.intToMonth = 4;
                }
                else if (toMonth.SelectedValue == "May")
                {
                    objUserExpDO.intToMonth = 5;
                }
                else if (toMonth.SelectedValue == "Jun")
                {
                    objUserExpDO.intToMonth = 6;
                }
                else if (toMonth.SelectedValue == "Jul")
                {
                    objUserExpDO.intToMonth = 7;
                }
                else if (toMonth.SelectedValue == "Aug")
                {
                    objUserExpDO.intToMonth = 8;
                }
                else if (toMonth.SelectedValue == "Sep")
                {
                    objUserExpDO.intToMonth = 9;
                }
                else if (toMonth.SelectedValue == "Oct")
                {
                    objUserExpDO.intToMonth = 10;
                }
                else if (toMonth.SelectedValue == "Nov")
                {
                    objUserExpDO.intToMonth = 11;
                }
                else if (toMonth.SelectedValue == "Dec")
                {
                    objUserExpDO.intToMonth = 12;
                }

                if (chkAtPresent.Checked == true)
                {
                    objUserExpDO.intToMonth = DateTime.Now.Month;
                }

                if (Convert.ToInt32(txtToYear.Text.Trim()) < Convert.ToInt32(txtFromYear.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check from year To to year.');</script>", false);
                    return;
                }
                else if (Convert.ToInt32(txtToYear.Text.Trim()) == Convert.ToInt32(txtFromYear.Text.Trim()))
                {
                    if (Convert.ToInt32(objUserExpDO.intToMonth) < Convert.ToInt32(objUserExpDO.inFromtMonth))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check from month To to month.');</script>", false);
                        return;
                    }
                }


                objUserExpDO.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
                objUserExpDO.strDescription = txtDescription.InnerHtml;
                objUserExpDO.strIpAddress = ip;
                objUserExpDO.strUnqId = ViewState["UnqKey"].ToString();

                if (chkAtPresent.Checked == true)
                {
                    objUserExpDO.bitAtPresent = true;
                    CurrentlyWork = null;
                }
                else
                {
                    objUserExpDO.bitAtPresent = false;
                    CurrentlyWork = Convert.ToString(objUserExpDO.dtToDate);
                }

                objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objUserExpDO.dtFromDate = DateTime.Now;
                objUserExpDO.dtToDate = DateTime.Now;
                objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.Insert);
                objUserExpDO.intAddedBy = objUserExpDO.intOutId;

                if (ISAPIURLACCESSED == "1")
                {
                    try
                    {
                        String url = APIURL + "updateWorkExpDetails.action?" +
                                    "uid=" + Convert.ToInt32(ViewState["UserID"]) +
                                    "&expId=" + objUserExpDO.intOutId +
                                    "&designation=" + objUserExpDO.strDesignation +
                                    "&company=" + objUserExpDO.strCompanyName +
                                    "&workFrom=" + objUserExpDO.dtFromDate +
                                    "&workTo=" + CurrentlyWork +
                                    "&details=" + objUserExpDO.strDescription;

                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                        myRequest1.Method = "GET";
                        if (ISAPIResponse != "0")
                        {
                            WebResponse myResponse1 = myRequest1.GetResponse();
                            StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                            String result = sr.ReadToEnd();

                            objAPILogDO.strURL = url;
                            objAPILogDO.strAPIType = "Profile Work Experience";
                            objAPILogDO.strResponse = result;
                            objAPILogDO.strIPAddress = ip;
                            objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                            objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                        }
                    }

                    catch { }
                }
            }

        }
        else
        {
            if (txtInstituteName.Text != "")
            {
                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    ip = Request.ServerVariables["REMOTE_ADDR"];
                objUserExpDO.intExperienceId = Convert.ToInt32(ViewState["hdnintExperienceId"]);
                objUserExpDO.strCompanyName = txtInstituteName.Text.Trim();
                objUserExpDO.strDesignation = txtDegree.Text.Trim();
                objUserExpDO.intToYear = Convert.ToInt32(txtToYear.Text.Trim());
                objUserExpDO.intFromYear = Convert.ToInt32(txtFromYear.Text.Trim());

                if (fromMonth.Value == "Jan")
                {
                    objUserExpDO.inFromtMonth = 1;
                }
                else if (fromMonth.Value == "Feb")
                {
                    objUserExpDO.inFromtMonth = 2;
                }
                else if (fromMonth.Value == "Mar")
                {
                    objUserExpDO.inFromtMonth = 3;
                }
                else if (fromMonth.Value == "Apr")
                {
                    objUserExpDO.inFromtMonth = 4;
                }
                else if (fromMonth.Value == "May")
                {
                    objUserExpDO.inFromtMonth = 5;
                }
                else if (fromMonth.Value == "Jun")
                {
                    objUserExpDO.inFromtMonth = 6;
                }
                else if (fromMonth.Value == "Jul")
                {
                    objUserExpDO.inFromtMonth = 7;
                }
                else if (fromMonth.Value == "Aug")
                {
                    objUserExpDO.inFromtMonth = 8;
                }
                else if (fromMonth.Value == "Sep")
                {
                    objUserExpDO.inFromtMonth = 9;
                }
                else if (fromMonth.Value == "Oct")
                {
                    objUserExpDO.inFromtMonth = 10;
                }
                else if (fromMonth.Value == "Nov")
                {
                    objUserExpDO.inFromtMonth = 11;
                }
                else if (fromMonth.Value == "Dec")
                {
                    objUserExpDO.inFromtMonth = 12;
                }

                if (toMonth.SelectedValue == "Jan")
                {
                    objUserExpDO.intToMonth = 1;
                }
                else if (toMonth.SelectedValue == "Feb")
                {
                    objUserExpDO.intToMonth = 2;
                }
                else if (toMonth.SelectedValue == "Mar")
                {
                    objUserExpDO.intToMonth = 3;
                }
                else if (toMonth.SelectedValue == "Apr")
                {
                    objUserExpDO.intToMonth = 4;
                }
                else if (toMonth.SelectedValue == "May")
                {
                    objUserExpDO.intToMonth = 5;
                }
                else if (toMonth.SelectedValue == "Jun")
                {
                    objUserExpDO.intToMonth = 6;
                }
                else if (toMonth.SelectedValue == "Jul")
                {
                    objUserExpDO.intToMonth = 7;
                }
                else if (toMonth.SelectedValue == "Aug")
                {
                    objUserExpDO.intToMonth = 8;
                }
                else if (toMonth.SelectedValue == "Sep")
                {
                    objUserExpDO.intToMonth = 9;
                }
                else if (toMonth.SelectedValue == "Oct")
                {
                    objUserExpDO.intToMonth = 10;
                }
                else if (toMonth.SelectedValue == "Nov")
                {
                    objUserExpDO.intToMonth = 11;
                }
                else if (toMonth.SelectedValue == "Dec")
                {
                    objUserExpDO.intToMonth = 12;
                }

                if (chkAtPresent.Checked == true)
                {
                    objUserExpDO.intToMonth = DateTime.Now.Month;
                }

                objUserExpDO.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
                objUserExpDO.strDescription = txtDescription.InnerHtml;
                objUserExpDO.strIpAddress = ip;
                objUserExpDO.strUnqId = ViewState["UnqKey"].ToString();

                if (chkAtPresent.Checked == true)
                {
                    objUserExpDO.bitAtPresent = true;
                    CurrentlyWork = null;
                }
                else
                {
                    objUserExpDO.bitAtPresent = false;
                    CurrentlyWork = Convert.ToString(objUserExpDO.dtToDate);
                }

                if (Convert.ToInt32(txtToYear.Text.Trim()) < Convert.ToInt32(txtFromYear.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check from year To to year.');</script>", false);
                    return;
                }
                else if (Convert.ToInt32(txtToYear.Text.Trim()) == Convert.ToInt32(txtFromYear.Text.Trim()))
                {
                    if (Convert.ToInt32(objUserExpDO.intToMonth) <= Convert.ToInt32(objUserExpDO.inFromtMonth))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check from month To to month.');</script>", false);
                        return;
                    }
                }

                objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objUserExpDO.dtFromDate = DateTime.Now;
                objUserExpDO.dtToDate = DateTime.Now;
                objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.Update);
                objUserExpDO.intAddedBy = objUserExpDO.intOutId;

                if (ISAPIURLACCESSED == "1")
                {
                    try
                    {
                        String url = APIURL + "updateWorkExpDetails.action?" +
                                    "uid=" + Convert.ToInt32(ViewState["UserID"]) +
                                    "&expId=" + objUserExpDO.intOutId +
                                    "&designation=" + objUserExpDO.strDesignation +
                                    "&company=" + objUserExpDO.strCompanyName +
                                    "&workFrom=" + objUserExpDO.dtFromDate +
                                    "&workTo=" + CurrentlyWork +
                                    "&details=" + objUserExpDO.strDescription;

                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                        myRequest1.Method = "GET";
                        if (ISAPIResponse != "0")
                        {
                            WebResponse myResponse1 = myRequest1.GetResponse();
                            StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                            String result = sr.ReadToEnd();

                            objAPILogDO.strURL = url;
                            objAPILogDO.strAPIType = "Profile Work Experience";
                            objAPILogDO.strResponse = result;
                            objAPILogDO.strIPAddress = ip;
                            objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                            objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                        }
                    }

                    catch { }
                }

            }
        }
        LoadUserExp();
        TotalscoreCount();
        ClearExperience();
        divAddskill.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallWork8", "CallWorkLI();", true);
    }

    protected void ClearExperience()
    {
        txtToYear.SelectedIndex = 0;
        txtInstituteName.Text = "";
        txtFromYear.SelectedIndex = 0;
        txtDescription.InnerText = "";
        txtDegree.Text = "";
        fromMonth.Value = "Month";
        toMonth.SelectedValue = "Month";
        chkAtPresent.Checked = false;
        lblareaskill.Text = "";
        toMonth.Enabled = true;
        txtToYear.Enabled = true;
        ViewState["Callsmoothmenu"] = "smooth";
    }

    protected void aAddworkExp_click(object sender, EventArgs e)
    {
        AddWorkExp.Style.Add("display", "block");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripte", "CallExpNumaric();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript2", "CallWorkMiddle();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallWork7", "CallWorkLI();", true);
    }

    protected void lstAreaExpert_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HtmlControl divUserexperenceED = (HtmlControl)e.Item.FindControl("divUserexperenceED");
        HtmlImage imgPlus = (HtmlImage)e.Item.FindControl("imgPlus");
        LinkButton lnkEndrose = (LinkButton)e.Item.FindControl("lnkEndrose");
        Label lblEndronseCount = (Label)e.Item.FindControl("lblEndronseCount");
        HiddenField hdnintUserSkillId = (HiddenField)e.Item.FindControl("hdnintUserSkillId");

        if (ViewState["RegId"].ToString() != "0")
        {
            imgPlus.Visible = true;
        }

        if (ViewState["RegId"].ToString() == "0")
        {
            objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        }
        else
        {
            objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["RegId"]);
        }
        objUserExpDO.intUserSkillId = Convert.ToInt32(hdnintUserSkillId.Value);
        DataTable dt = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.GetEndronseCount);
        if (dt.Rows.Count > 0)
        {
            lblEndronseCount.Text = dt.Rows[0]["Endorse"].ToString();
        }

    }

    protected void lstAreaExpert_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnintUserSkillId = (HiddenField)e.Item.FindControl("hdnintUserSkillId");
        LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkdDelete");
        Label lblstrSkillName = (Label)e.Item.FindControl("lblstrSkillName");
        PopUpCropImage.Style.Add("display", "none");
        if (e.CommandName == "EndronseSkill")
        {
            objRecmndDO.intInvitedUserId = Convert.ToInt32(ViewState["RegId"]);
            objRecmndDO.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
            objRecmndDO.StrRecommendation = lblstrSkillName.Text.Replace("'", "''").Replace("\n", "<br>");
            objRecmndDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objRecmndDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (objRecmndDO.strIpAddress == null)
                objRecmndDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objRecmndDO.intSkillId = Convert.ToInt32(hdnintUserSkillId.Value);
            objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.Add);

            try
            {
                if (objRecmndDO.intOutRecommendationId != 0)
                {
                    String url = APIURL + "endorseUserSkill.action?" +
                                    "endorseByUserId =" + UserTypeId + objRecmndDO.intAddedBy +
                                    "&endorseToUserId =" + UserTypeId + objRecmndDO.intInvitedUserId +
                                    "&skillId=" + objRecmndDO.intOutRecommendationId;

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();

                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();

                    objAPILogDO.strURL = url;
                    objAPILogDO.strAPIType = "User Endorse Skill";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.strIPAddress = objdoreg.IpAddress;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }

            }
            catch { }
            LoadUserAreaExp();
            LoadEndrosAndMsg();
        }

    }

    protected void btnAreaExpSave_Click(object sender, EventArgs e)
    {
        lblareaskill.Text = "";
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        if (txtAreaExpert.Text.Trim() != "")
        {
            objUserExpDO.dtFromDate = DateTime.Now;
            objUserExpDO.dtToDate = DateTime.Now;
            objUserExpDO.strSkillName = txtAreaExpert.Text.Trim();
            objUserExpDO.strIpAddress = ip;
            objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objUserExpDO.strUnqId = "";
            objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.InsertUserExpSkill);
            txtAreaExpert.Text = "";
            LoadUserAreaSkill();
        }
        else
        {
            LoadUserAreaSkill();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAddSkill", "CallAddSkill();", true);
            lblareaskill.Text = "Please enter area of expertise.";
            return;
        }
        LoadUserAreaExp();
    }

    protected void LoadUserAreaExp()
    {
        objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            divSkillEditdelete.Visible = false;
            lnkAddskill.Visible = false;
            objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["RegId"]);
            DataTable dt = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.GetSkillByUserId);
            if (dt.Rows.Count > 0)
            {
                lstAreaExpert.Visible = true;
                lstAreaExpert.DataSource = dt;
                lstAreaExpert.DataBind();
                divSkillEditdelete.Visible = false;
            }
            else
            {
                lstAreaExpert.Visible = false;
                lstAreaExpert.DataSource = null;
                lstAreaExpert.DataBind();
                divSkillEditdelete.Visible = false;
            }
        }
        else
        {
            DataTable dt = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.GetSkillByUserId);
            if (dt.Rows.Count > 0)
            {
                lstAreaExpert.Visible = true;
                lstAreaExpert.DataSource = dt;
                lstAreaExpert.DataBind();
                divSkillEditdelete.Visible = true;
            }
            else
            {
                lstAreaExpert.Visible = false;
                lstAreaExpert.DataSource = null;
                lstAreaExpert.DataBind();
                divSkillEditdelete.Visible = false;
            }
        }

    }

    protected void LoadUserAreaSkill()
    {
        objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        DataTable dt = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.GetSkillWithoudUkey);
        if (dt.Rows.Count > 0)
        {
            ViewState["lstAreaSkill"] = dt;
            lstAreaSkill.Visible = true;
            lstAreaSkill.DataSource = dt;
            lstAreaSkill.DataBind();
        }
        else
        {
            lstAreaSkill.Visible = false;
            lstAreaSkill.DataSource = null;
            lstAreaSkill.DataBind();
            return;
        }

    }

    protected void lstAreaSkill_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        lblareaskill.Text = "";
        HiddenField hdnintUserSkillId = (HiddenField)e.Item.FindControl("hdnintUserSkillId");
        LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkdDelete");
        if (e.CommandName == "DeleteExp")
        {
            objUserExpDO.dtFromDate = DateTime.Now;
            objUserExpDO.dtToDate = DateTime.Now;
            objUserExpDO.intUserSkillId = Convert.ToInt32(hdnintUserSkillId.Value);
            objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.DeleteUserSkill);
            if (ViewState["Editskillarea"] == null)
            {
                LoadUserAreaSkill();
            }
            else
            {
                LoadUserAreaExp();
                LoadlstArea();
            }
            TotalscoreCount();
        }

    }

    protected void lnkSaveSkill_Click(object sender, EventArgs e)
    {
        objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (ViewState["lstAreaSkill"] != null)
        {
            lblareaskill.Text = "";
            DataTable dt = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.GetSkillUnqId);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["strUnqId"].ToString() != "")
                {
                    objUserExpDO.dtFromDate = DateTime.Now;
                    objUserExpDO.dtToDate = DateTime.Now;
                    objUserExpDO.strUnqId = dt.Rows[0]["strUnqId"].ToString();
                    objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.UpdateSkill);
                    LoadUserAreaSkill();
                    LoadUserAreaExp();
                }
            }
            else
            {
                objUserExpDO.dtFromDate = DateTime.Now;
                objUserExpDO.dtToDate = DateTime.Now;
                objUserExpDO.strUnqId = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "");
                objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.UpdateSkill);
                LoadUserAreaSkill();
                LoadUserAreaExp();
            }
            ViewState["lstAreaSkill"] = null;
            TotalscoreCount();
            divAddskill.Style.Add("display", "block");
            lstAreaExpert.Visible = true;
            lstAreaSkill.Visible = true;
        }
        else
        {
            LoadUserAreaSkill();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "callSaveSkill", "callSaveSkill();", true);
            lblareaskill.Text = "Please add skill.";
            return;
        }
    }

    protected void lnkAddSkill_Click(object sender, EventArgs e)
    {
        divAddskill.Style.Add("display", "block");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallWork5", "CallWorkLI();", true);
    }

    protected void lnlCancel_Click(object sender, EventArgs e)
    {
        AddWorkExp.Style.Add("display", "none");
        ClearExperience();
        LoadUserAreaSkill();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallWork1", "CallWorkLI();", true);
    }

    protected void lnkCancelSkill_Click(object sender, EventArgs e)
    {

        objUserExpDO.dtFromDate = DateTime.Now;
        objUserExpDO.dtToDate = DateTime.Now;
        objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.DeleteUserSkills);
        LoadUserAreaSkill();
        ClearExperience();
        divAddskill.Style.Add("display", "none");
        ViewState["Callsmoothmenu"] = "smooth";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "groupConnChange();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallWork2", "CallWorkLI();", true);
    }

    protected void lnkEditSkill_Click(object sender, EventArgs e)
    {
        lblareaskill.Text = "";
        LoadlstArea();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "CallSkillMiddle();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallWork6", "CallWorkLI();", true);
    }

    public void LoadlstArea()
    {
        divAddskill.Style.Add("display", "block");
        objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        DataTable dt = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.GetSkillByUserId);
        if (dt.Rows.Count > 0)
        {
            ViewState["Editskillarea"] = "1";
            ViewState["lstAreaSkill"] = "1";
            lstAreaSkill.Visible = true;
            lstAreaSkill.DataSource = dt;
            lstAreaSkill.DataBind();
        }
        else
        {
            ViewState["Editskillarea"] = null;
            ViewState["lstAreaSkill"] = null;
            lstAreaSkill.Visible = false;
            lstAreaSkill.DataSource = null;
            lstAreaSkill.DataBind();
        }
    }

    protected void lnkDeleteSkill_Click(object sender, EventArgs e)
    {
        objUserExpDO.dtFromDate = DateTime.Now;
        objUserExpDO.dtToDate = DateTime.Now;
        objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.DeleteSkills);
        LoadUserAreaExp();
        LoadUserExp();
        TotalscoreCount();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallWork3", "CallWorkLI();", true);
    }

    #endregion

    #region Education Section
    protected void LoadEducation()
    {
        objDOEdu.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            lnlAddEducation.Visible = false;
            objDOEdu.intAddedBy = Convert.ToInt32(ViewState["RegId"]);
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetEducationOnly);
            if (dt.Rows.Count > 0)
            {
                lstEducation.DataSource = dt;
                lstEducation.DataBind();
            }
            else
            {
                lstEducation.DataSource = null;
                lstEducation.DataBind();
            }

        }
        else
        {
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetEducationOnly);
            if (dt.Rows.Count > 0)
            {
                lstEducation.DataSource = dt;
                lstEducation.DataBind();
            }
            else
            {
                lstEducation.DataSource = null;
                lstEducation.DataBind();
            }
        }
    }

    protected void LoadYear()
    {
        DataTable dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.LoadYear);
        if (dt.Rows.Count > 0)
        {
            txtEducationFromdt.DataSource = dt;
            txtEducationFromdt.DataTextField = "intYear";
            txtEducationFromdt.DataValueField = "intYear";
            txtEducationFromdt.DataBind();
            txtEducationFromdt.Items.Insert(0, new ListItem("Year"));

            txtEducationTodt.DataSource = dt;
            txtEducationTodt.DataTextField = "intYear";
            txtEducationTodt.DataValueField = "intYear";
            txtEducationTodt.DataBind();
            txtEducationTodt.Items.Insert(0, new ListItem("Year"));
        }

    }

    protected void lstEducation_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HtmlControl divEducationED = (HtmlControl)e.Item.FindControl("divEducationED");
        Label lblintToMonth = (Label)e.Item.FindControl("lblintToMonth");
        Label lblintToYear = (Label)e.Item.FindControl("lblintToYear");
        Label lblPresentDay = (Label)e.Item.FindControl("lblPresentDay");
        HiddenField hdnToMonth = (HiddenField)e.Item.FindControl("hdnToMonth");

        if (hdnintAddedBy.Value != ViewState["UserID"].ToString())
        {
            divEducationED.Style.Add("display", "none");
        }
        if (hdnToMonth.Value != "")
        {
            if (Convert.ToInt32(hdnToMonth.Value) == DateTime.Now.Month)
            {
                lblintToMonth.Visible = false;
                lblintToYear.Visible = false;
                lblPresentDay.Text = "Present Day";
            }
        }
        else
        {
            lblintToMonth.Visible = false;
            lblintToYear.Visible = false;
            lblPresentDay.Text = "Present Day";
        }

    }

    protected void lstEducation_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnintEducationId = (HiddenField)e.Item.FindControl("hdnintEducationId");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        objDOEdu.intEducationId = Convert.ToInt32(hdnintEducationId.Value);
        if (e.CommandName == "EditEdu")
        {
            PopUpCropImage.Style.Add("display", "none");
            divEducation.Style.Add("display", "block");
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.SingleRecord);
            if (dt.Rows.Count > 0)
            {
                ViewState["intEducationId"] = dt.Rows[0]["intEducationId"].ToString();

                txtInstitute.Text = Convert.ToString(dt.Rows[0]["strInstituteName"]);
                txtTitle.Text = Convert.ToString(dt.Rows[0]["strDegree"]);
                txtEducationFromdt.Text = Convert.ToString(dt.Rows[0]["intYear"]);
                txtEducationTodt.SelectedValue = Convert.ToString(dt.Rows[0]["intToYear"]);
                txtEduDescription.InnerText = Convert.ToString(dt.Rows[0]["strSpclLibrary"]);
                SelectEducationMonth(dt);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallEducationMiddle();", true);
        }
    }

    protected void lnkSaveEducation_Click(object sender, EventArgs e)
    {

        objDOEdu.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (txtTitle.Text != "")
        {
            objDOEdu.strDegree = txtTitle.Text.Trim().Replace("'", "''");
            objDOEdu.intDegreeId = objDAEdu.GetintDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetDeegreeid);
        }
        else
            return;

        if (txtInstitute.Text != "")
        {
            objDOEdu.strInstituteName = txtInstitute.Text.Trim().Replace("'", "''");
            objDOEdu.intInstituteId = objDAEdu.GetintDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetInstituteid);
        }
        else
            return;

        if (txtDescription.InnerText != "Description")
            objDOEdu.strSpclLibrary = txtEduDescription.InnerHtml.Trim();
        if (txtEducationFromdt.Text.Trim() != "Year")
            objDOEdu.intYear = Convert.ToInt32(txtEducationFromdt.Text.Trim());
        else
            return;
        if (txtEducationTodt.Text.Trim() != "Year")
            objDOEdu.intToYear = Convert.ToInt32(txtEducationTodt.Text.Trim());
        else
            return;

        if (ddlFromMonth.Value == "Jan")
        {
            objDOEdu.intMonth = 1;
        }
        else if (ddlFromMonth.Value == "Feb")
        {
            objDOEdu.intMonth = 2;
        }
        else if (ddlFromMonth.Value == "Mar")
        {
            objDOEdu.intMonth = 3;
        }
        else if (ddlFromMonth.Value == "Apr")
        {
            objDOEdu.intMonth = 4;
        }
        else if (ddlFromMonth.Value == "May")
        {
            objDOEdu.intMonth = 5;
        }
        else if (ddlFromMonth.Value == "Jun")
        {
            objDOEdu.intMonth = 6;
        }
        else if (ddlFromMonth.Value == "Jul")
        {
            objDOEdu.intMonth = 7;
        }
        else if (ddlFromMonth.Value == "Aug")
        {
            objDOEdu.intMonth = 8;
        }
        else if (ddlFromMonth.Value == "Sep")
        {
            objDOEdu.intMonth = 9;
        }
        else if (ddlFromMonth.Value == "Oct")
        {
            objDOEdu.intMonth = 10;
        }
        else if (ddlFromMonth.Value == "Nov")
        {
            objDOEdu.intMonth = 11;
        }
        else if (ddlFromMonth.Value == "Dec")
        {
            objDOEdu.intMonth = 12;
        }
        else
        {
            objDOEdu.intMonth = 1;
        }

        if (ddlToMonth.SelectedValue == "Jan")
        {
            objDOEdu.intToMonth = 1;
        }
        else if (ddlToMonth.SelectedValue == "Feb")
        {
            objDOEdu.intToMonth = 2;
        }
        else if (ddlToMonth.SelectedValue == "Mar")
        {
            objDOEdu.intToMonth = 3;
        }
        else if (ddlToMonth.SelectedValue == "Apr")
        {
            objDOEdu.intToMonth = 4;
        }
        else if (ddlToMonth.SelectedValue == "May")
        {
            objDOEdu.intToMonth = 5;
        }
        else if (ddlToMonth.SelectedValue == "Jun")
        {
            objDOEdu.intToMonth = 6;
        }
        else if (ddlToMonth.SelectedValue == "Jul")
        {
            objDOEdu.intToMonth = 7;
        }
        else if (ddlToMonth.SelectedValue == "Aug")
        {
            objDOEdu.intToMonth = 8;
        }
        else if (ddlToMonth.SelectedValue == "Sep")
        {
            objDOEdu.intToMonth = 9;
        }
        else if (ddlToMonth.SelectedValue == "Oct")
        {
            objDOEdu.intToMonth = 10;
        }
        else if (ddlToMonth.SelectedValue == "Nov")
        {
            objDOEdu.intToMonth = 11;
        }
        else if (ddlToMonth.SelectedValue == "Dec")
        {
            objDOEdu.intToMonth = 12;
        }
        else
        {
            objDOEdu.intToMonth = 1;
        }

        //objDOEdu.intToMonth = Convert.ToInt32(txtEducationTodt.Text);
        if (chkAtPresent.Checked == true)
        {
            objDOEdu.intToMonth = DateTime.Now.Month;
        }

        if (Convert.ToInt32(objDOEdu.intToYear) < Convert.ToInt32(objDOEdu.intYear))
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check from year To to year.');</script>", false);
            return;
        }
        else if (Convert.ToInt32(objDOEdu.intToYear) == Convert.ToInt32(objDOEdu.intYear))
        {
            if (Convert.ToInt32(objDOEdu.intToMonth) <= Convert.ToInt32(objDOEdu.intMonth))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check from month To to month.');</script>", false);
                return;
            }
        }

        objDOEdu.strEducationAchievement = "Education";
        objDOEdu.strIpAddress = ip;
        objDOEdu.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objDOEdu.dtDate = DateTime.Now;

        if (ViewState["intEducationId"] == null)
        {
            objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.Insert);
        }
        else
        {
            objDOEdu.intEducationId = Convert.ToInt32(ViewState["intEducationId"]);
            objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.Update);
            objDOEdu.intOutId = 1;
        }
        if (objDOEdu.intOutId == 0)
        {
            lblMessage.Text = "Record Already Inserted..!";
            return;
        }

        string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
        if (ISAPIURLACCESSED != "0")
        {
            try
            {
                String url = APIURL + "updateUserEducationDetails.action?" +
                            "uid=" + UserTypeId + Convert.ToInt32(ViewState["UserID"]) +
                            "&educationId=" + objDOEdu.intOutId +
                            "&degree=" + objDOEdu.strDegree +
                            "&institution=" + objDOEdu.strInstituteName +
                            "&year=" + objDOEdu.intYear +
                            "&educationType=" + objDOEdu.strEducationAchievement +
                            "&achievementMarked=" + 1 +
                            "&description=" + objDOEdu.strSpclLibrary;

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                myRequest1.Method = "GET";
                if (ISAPIResponse != "0")
                {
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();
                    objAPILogDO.strAPIType = "Profile Education";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.strIPAddress = ip;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
            }
            catch { }
        }

        clearEducation();
        LoadEducation();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallEducation5", "CallEduLI();", true);
    }

    protected void lnkCancelEducation_Click(object sender, EventArgs e)
    {
        clearEducation();
        divEducation.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallEducation1", "CallEduLI();", true);
    }

    public void SelectEducationMonth(DataTable dtt)
    {
        if (dtt.Rows[0]["intMonth"].ToString() == "1")
        {
            ddlFromMonth.Value = "Jan";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "2")
        {
            ddlFromMonth.Value = "Feb";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "3")
        {
            ddlFromMonth.Value = "Mar";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "4")
        {
            ddlFromMonth.Value = "Apr";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "5")
        {
            ddlFromMonth.Value = "May";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "6")
        {
            ddlFromMonth.Value = "Jun";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "7")
        {
            ddlFromMonth.Value = "Jul";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "8")
        {
            ddlFromMonth.Value = "Aug";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "9")
        {
            ddlFromMonth.Value = "Sep";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "10")
        {
            ddlFromMonth.Value = "Oct";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "11")
        {
            ddlFromMonth.Value = "Nov";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "12")
        {
            ddlFromMonth.Value = "Dec";
        }
        else
        {
            ddlFromMonth.Value = "Jan";
        }

        if (dtt.Rows[0]["intToMonth"].ToString() == "1")
        {
            ddlToMonth.SelectedValue = "Jan";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "2")
        {
            ddlToMonth.SelectedValue = "Feb";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "3")
        {
            ddlToMonth.SelectedValue = "Mar";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "4")
        {
            ddlToMonth.SelectedValue = "Apr";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "5")
        {
            ddlToMonth.SelectedValue = "May";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "6")
        {
            ddlToMonth.SelectedValue = "Jun";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "7")
        {
            ddlToMonth.SelectedValue = "Jul";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "8")
        {
            ddlToMonth.SelectedValue = "Aug";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "9")
        {
            ddlToMonth.SelectedValue = "Sep";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "10")
        {
            ddlToMonth.SelectedValue = "Oct";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "11")
        {
            ddlToMonth.SelectedValue = "Nov";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "12")
        {
            ddlToMonth.SelectedValue = "Dec";
        }
        else
        {
            ddlToMonth.SelectedValue = "Jan";
        }

    }

    protected void chkAtPresent_CheckedChanged1(object sender, EventArgs e)
    {
        if (chkEducation.Checked == true)
        {
            txtEducationTodt.SelectedValue = DateTime.Now.Year.ToString();
            txtEducationTodt.Enabled = false;
            int i = DateTime.Now.Month;
            ddlToMonth.SelectedIndex = i;
            ddlToMonth.Enabled = false;
        }
        else
        {
            txtEducationTodt.SelectedIndex = 0;
            txtEducationTodt.Enabled = true;
            ddlToMonth.Enabled = true;
            ddlToMonth.SelectedIndex = 0;
        }
    }

    protected void clearEducation()
    {
        lblMessage.Text = "";
        LoadYear();
        ViewState["intEducationId"] = null;
        txtEducationFromdt.SelectedValue = "Year";
        txtEducationTodt.SelectedValue = "Year";
        txtEduDescription.InnerText = "";
        ddlToMonth.SelectedValue = "Month";
        ddlFromMonth.Value = "Month";
        txtTitle.Text = "";
        txtInstitute.Text = "";
        chkEducation.Checked = false;
        txtEducationTodt.SelectedIndex = 0;
        txtEducationTodt.Enabled = true;
        ddlToMonth.Enabled = true;
        ddlToMonth.SelectedIndex = 0;
        ViewState["Callsmoothmenu"] = "smooth";
    }

    protected void lnlAddEducation_Click(object sender, EventArgs e)
    {
        divEducation.Style.Add("display", "block");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", " CallEducationMiddle();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallEducation5", "CallEduLI();", true);
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select strInstituteName from Scrl_InstituteNameTbl where " + "strInstituteName like @Search + '%'";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> InstituteName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        InstituteName.Add(sdr["strInstituteName"].ToString());
                    }
                }
                con.Close();
                return InstituteName;

            }

        }

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetDegreeList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select strDegreeName from Scrl_InstituteDegreeTbl where " + "strDegreeName like @Search + '%'";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> InstituteName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        InstituteName.Add(sdr["strDegreeName"].ToString());
                    }
                }
                con.Close();
                return InstituteName;

            }

        }

    }

    #endregion

    #region Achivement Section

    protected void LoadMilestones()
    {
        objDOEdu.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        DataTable dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetMilestones);

        if (dt.Rows.Count > 0)
        {
            ddlMilestone.DataSource = dt;
            ddlMilestone.DataTextField = "strMilestoneName";
            ddlMilestone.DataValueField = "intMilestoneId";
            ddlMilestone.DataBind();
            ddlMilestone.Items.Insert(0, new ListItem("Type of Milestone", "0"));
        }
        else
        {
            ddlMilestone.DataSource = null;
            ddlMilestone.DataBind();
        }

        DataTable dtt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetPosition);

        if (dtt.Rows.Count > 0)
        {
            ddlPosition.DataSource = dtt;
            ddlPosition.DataTextField = "strPositionName";
            ddlPosition.DataValueField = "intPostionId";
            ddlPosition.DataBind();
            ddlPosition.Items.Insert(0, new ListItem("Position", "0"));
        }
        else
        {
            ddlPosition.DataSource = null;
            ddlPosition.DataBind();
        }

    }

    protected void LoadAchivment()
    {
        objDOEdu.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            lnkAddachive.Visible = false;
            objDOEdu.intAddedBy = Convert.ToInt32(ViewState["RegId"]);
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetAchivementOnly);
            if (dt.Rows.Count > 0)
            {
                lstAchivement.DataSource = dt;
                lstAchivement.DataBind();
            }
            else
            {
                lstAchivement.DataSource = null;
                lstAchivement.DataBind();
            }
        }
        else
        {
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetAchivementOnly);
            if (dt.Rows.Count > 0)
            {
                lstAchivement.DataSource = dt;
                lstAchivement.DataBind();
            }
            else
            {
                lstAchivement.DataSource = null;
                lstAchivement.DataBind();
            }
        }
        divDeletesucess.Style.Add("display", "none");
    }

    protected void lstAchivement_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HtmlControl divAchivementED = (HtmlControl)e.Item.FindControl("divAchivementED");
        if (hdnintAddedBy.Value != ViewState["UserID"].ToString())
        {
            divAchivementED.Style.Add("display", "none");
        }
    }

    protected void lstAchivement_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnintAchivmentId = (HiddenField)e.Item.FindControl("hdnintAchivmentId");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        objDOEdu.intAchivmentId = Convert.ToInt32(hdnintAchivmentId.Value);
        if (e.CommandName == "EditAch")
        {
            PopUpCropImage.Style.Add("display", "none");
            LoadMilestones();
            divAchivement.Style.Add("display", "block");
            ViewState["hdnintAchivmentId"] = hdnintAchivmentId.Value;
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.SingleRecordAchivemrnt);

            if (dt.Rows.Count > 0)
            {
                txtCompitition.Text = Convert.ToString(dt.Rows[0]["strCompititionName"]);
                ddlPosition.SelectedValue = Convert.ToString(dt.Rows[0]["intPostionId"]);
                ddlMilestone.SelectedValue = Convert.ToString(dt.Rows[0]["intMilestoneId"]);
                txtAdditionalAwrd.Text = Convert.ToString(dt.Rows[0]["strAdditionalAward"]);
                txtAchivDescription.InnerText = Convert.ToString(dt.Rows[0]["strDescription"]);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallAchiveMiddle();", true);
        }
    }

    protected void lnkSaveAchivemnt_Click(object sender, EventArgs e)
    {
        objDOEdu.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (txtCompitition.Text != "")
        {

            if (ddlMilestone.SelectedItem.Text != "Type of Milestone")
            {
                objDOEdu.intMilestoneId = Convert.ToInt32(ddlMilestone.SelectedValue);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "callSaveAch", "callSaveAch();", true);
                lblAchivmentmsg.Text = "Please select milestone.";
                return;
            }
            if (ddlPosition.SelectedItem.Text != "Position")
            {
                objDOEdu.intPostionId = Convert.ToInt32(ddlPosition.SelectedValue);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "callSaveAchs", "callSaveAch();", true);
                lblAchivmentmsg.Text = "Please select position.";
                return;
            }

            objDOEdu.dtDate = DateTime.Now;
            objDOEdu.strEducationAchievement = "Achievement";
            objDOEdu.strIpAddress = ip;
            //if (string.IsNullOrEmpty(lblId.Text))
            //{

            objDOEdu.strCompititionName = txtCompitition.Text.Trim();
            objDOEdu.CompLavel = objDAEdu.GetintDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetCompLavel);
            objDOEdu.strPosition = ddlPosition.SelectedItem.Text;
            objDOEdu.strMilestone = ddlMilestone.SelectedItem.Text;
            objDOEdu.strAdditionalAward = txtAdditionalAwrd.Text.Trim();
            objDOEdu.strAchDescription = txtAchivDescription.InnerText.Trim();
            objDOEdu.intAddedBy = Convert.ToInt32(ViewState["UserID"]);


            if (ViewState["hdnintAchivmentId"] == null)
            {
                objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.InsertAchivement);
            }
            else
            {
                objDOEdu.intAchivmentId = Convert.ToInt32(ViewState["hdnintAchivmentId"]);
                objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.UpdateAchivement);
            }
            string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
            if (ISAPIURLACCESSED != "0")
            {
                try
                {
                    String url = APIURL + "updateUserEducationDetails.action?" +
                                "uid=" + UserTypeId + Convert.ToInt32(ViewState["UserID"]) +
                                "&educationId=" + objDOEdu.intOutId +
                                "&degree=" + objDOEdu.strDegree +
                                "&institution=" + objDOEdu.strInstituteName +
                                "&year=" + objDOEdu.intYear +
                                "&educationType=" + objDOEdu.strEducationAchievement +
                                "&achievementMarked=" + 1 +
                                "&description=" + objDOEdu.strSpclLibrary;

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                    myRequest1.Method = "GET";
                    if (ISAPIResponse != "0")
                    {
                        WebResponse myResponse1 = myRequest1.GetResponse();
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.strAPIType = "Profile Achivement";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = ip;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }

            LoadAchivment();
            ClearAchivement();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAchi4", "CallAchLI();", true);
        }
        else
        {
            return;
        }
    }

    [System.Web.Services.WebMethod]
    public static List<string> GetCompititionList(string compName, string Milevalue)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select CompName,intMilestoneId from Scrl_CompetitionMasterTbl where " + "CompName like @Search + '%' and intMilestoneId=@MilestoneId";
                com.Parameters.AddWithValue("@Search", compName);
                com.Parameters.AddWithValue("@MilestoneId", Milevalue);
                com.Connection = con;
                con.Open();
                List<string> InstituteName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        InstituteName.Add(sdr["CompName"].ToString());
                    }
                }
                con.Close();
                return InstituteName;

            }

        }

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompititionLists(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select CompName,intMilestoneId from Scrl_CompetitionMasterTbl where " + "CompName like @Search + '%'";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> InstituteName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        InstituteName.Add(sdr["CompName"].ToString());
                    }
                }
                con.Close();
                return InstituteName;

            }

        }

    }

    protected void ClearAchivement()
    {
        ViewState["hdnintAchivmentId"] = null;
        txtCompitition.Text = "";
        ddlPosition.SelectedIndex = 0;
        ddlMilestone.SelectedIndex = 0;
        txtAdditionalAwrd.Text = "";
        txtAchivDescription.InnerText = "";
        lblAchivmentmsg.Text = "";
        ViewState["Callsmoothmenu"] = "smooth";
    }

    protected void lnkAddachive_Click(object sender, EventArgs e)
    {
        divAchivement.Style.Add("display", "block");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallAchiveMiddle();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAchi2", "CallAchLI();", true);
    }

    protected void lnkCancelAchivemnt_Click(object sender, EventArgs e)
    {
        ClearAchivement();
        divAchivement.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAchi1", "CallAchLI();", true);
    }

    #endregion

    #region Personal Point
    protected string CalculatePersonalPoint(int UserId)
    {
        string Total = string.Empty;
        try
        {
            String url = APIURL + "getUserScore.action?"
                               + "uId=" + Convert.ToString(UserId);
            if (ISAPIURLACCESSED == "1")
            {
                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(Convert.ToString(url));
                myRequest1.Method = "GET";
                WebResponse myResponse1 = myRequest1.GetResponse();

                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                String result = sr.ReadToEnd();
                string data = Convert.ToString(ViewState["url"]);
                _responseJson1 my = JsonConvert.DeserializeObject<_responseJson1>(result);
                StringBuilder sb = new StringBuilder();

                //ResearchDataTable();
                if (my.responseJson != null)
                {
                    try
                    {
                        int issueScore = 0, factScore = 0, importantParagraphScore = 0, orbiterDictumScore = 0, precedentScore = 0, ratioDecidenciScore = 0, summaryScore = 0;
                        if (my.responseJson.issueScore != null)
                            issueScore = Convert.ToInt32(my.responseJson.issueScore);
                        if (my.responseJson.factScore != null)
                            factScore = Convert.ToInt32(my.responseJson.factScore);
                        if (my.responseJson.importantParagraphScore != null)
                            importantParagraphScore = Convert.ToInt32(my.responseJson.importantParagraphScore);
                        if (my.responseJson.orbiterDictumScore != null)
                            orbiterDictumScore = Convert.ToInt32(my.responseJson.orbiterDictumScore);
                        if (my.responseJson.precedentScore != null)
                            precedentScore = Convert.ToInt32(my.responseJson.precedentScore);
                        if (my.responseJson.ratioDecidenciScore != null)
                            ratioDecidenciScore = Convert.ToInt32(my.responseJson.ratioDecidenciScore);
                        if (my.responseJson.summaryScore != null)
                            summaryScore = Convert.ToInt32(my.responseJson.summaryScore);

                        Total = (issueScore + factScore + importantParagraphScore + orbiterDictumScore + precedentScore + ratioDecidenciScore + summaryScore).ToString();

                    }
                    catch
                    {
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return Total;
    }
    #endregion

    [WebMethod]
    public static string GetUserJSONdata()
    {
        string st;
        DA_Profile ObjDAprofile = new DA_Profile();
        DO_Profile objDoProfile = new DO_Profile();

        objDoProfile.RegistrationId = Convert.ToInt32(HttpContext.Current.Session["ExternalUserId"]);
        DataTable dt = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetAllProfileUSerDetails);


        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        st = serializer.Serialize(rows);
        return st;
    }

    [WebMethod]
    public static string UpdateUserStatusLike(string UserId, string hdnPostUpdateId)
    {
        DO_Scrl_UserStatusUpdateTbl objstatusDO = new DO_Scrl_UserStatusUpdateTbl();
        DA_Scrl_UserStatusUpdateTbl objstatusDA = new DA_Scrl_UserStatusUpdateTbl();
        objstatusDO.intStatusUpdateId = Convert.ToInt32(hdnPostUpdateId);
        objstatusDO.intLikeDisLike = 1;//For Like
        objstatusDO.intAddedBy = Convert.ToInt32(UserId);
        string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

        objstatusDO.strIpAddress = ip;
        DataTable dtAction = new DataTable();
        dtAction = objstatusDA.GetLikeDataTable(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.LikeStatus);
        return "";
    }

    [WebMethod]
    public static string UpdateUserStatusCommLike(string UserId, string hdnCommentId)
    {
        DO_Scrl_UserStatusUpdateTbl objstatusDO = new DO_Scrl_UserStatusUpdateTbl();
        DA_Scrl_UserStatusUpdateTbl objstatusDA = new DA_Scrl_UserStatusUpdateTbl();
        DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
        DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

        objstatusDO.intCommentId = Convert.ToInt32(hdnCommentId);
        objstatusDO.intLikeDisLike = 1;//For Like
        objstatusDO.intAddedBy = Convert.ToInt32(UserId);
        string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        objstatusDO.strIpAddress = ip;
        DataTable dtAction = new DataTable();
        dtAction = objstatusDA.GetLikeDataTable(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.AddLike);
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dtAction.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dtAction.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }

        if (ISAPIURLACCESSED != "0")
        {
            string UserURL = APIURL + "dislikeUserComment.action?userId =" + UserId +
                "&commentId=" + hdnCommentId;
            try
            {
                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                myRequest1.Method = "GET";
                if (ISAPIResponse != "0")
                {
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();
                    objAPILogDO.strURL = UserURL;
                    objAPILogDO.strAPIType = "Wall User Comment UnLike";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.strIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
            }
            catch { }

        }

        return serializer.Serialize(rows);
    }

    [WebMethod]
    public static string LoadProfileImage(string UserId)
    {
        Page page = (Page)HttpContext.Current.Handler;
        string PathImg;
        DA_Profile ObjDAprofile = new DA_Profile();
        DO_Profile objDoProfile = new DO_Profile();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        row = new Dictionary<string, object>();
        objDoProfile.RegistrationId = Convert.ToInt32(UserId);
        DataTable dt = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetAllProfileUSerDetails);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["vchrPhotoPath"].ToString() != "" && dt.Rows[0]["vchrPhotoPath"].ToString() != string.Empty)
            {
                try
                {

                    string imgPathPhysical = HttpContext.Current.Server.MapPath("~/CroppedPhoto/" + dt.Rows[0]["vchrPhotoPath"].ToString());
                    if (File.Exists(imgPathPhysical))
                    {
                        PathImg = "CroppedPhoto/" + dt.Rows[0]["vchrPhotoPath"].ToString();
                        //Image1.ImageUrl = "~\\CroppedPhoto\\" + dt.Rows[0]["vchrPhotoPath"].ToString();
                    }
                    else
                    {
                        PathImg = "images/profile-photo.png";
                    }
                }
                catch
                {
                    PathImg = "images/profile-photo.png";
                }
            }
            else
            {
                PathImg = "images/profile-photo.png";
            }
            row.Add("path", PathImg);
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    [WebMethod]
    public static string InsertPostCommentSafari(string PostComment, string PostIDS, string intCommentId)
    {
        DO_Scrl_UserStatusUpdateTbl objstatusDO = new DO_Scrl_UserStatusUpdateTbl();
        DA_Scrl_UserStatusUpdateTbl objstatusDA = new DA_Scrl_UserStatusUpdateTbl();
        if (PostComment.Trim() != "")
        {
            objstatusDO.intStatusUpdateId = Convert.ToInt32(PostIDS);
            objstatusDO.strComment = PostComment;
            objstatusDO.intAddedBy = Convert.ToInt32(HttpContext.Current.Session["ExternalUserId"]);
            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            objstatusDO.strIpAddress = ip;
            if (intCommentId == "")
            {
                objstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.AddComment);
            }
            else
            {
                objstatusDO.intCommentId = Convert.ToInt32(intCommentId);
                objstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.UpdateComment);
            }

        }

        return "";
    }

    public void btnPostCommentSave_Click(object sender, EventArgs e)
    {
        if (ViewState["RegId"] == null || ViewState["RegId"].ToString() == "0")
        {
            BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
        }
        else
        {
            BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), Convert.ToInt32(ViewState["RegId"]), 2); //frnd wall
        }
    }

    #region Classes
    public class _responseJson1
    {
        public responseJson responseJson { get; set; }
    }

    public class responseJson
    {
        public string responseId { get; set; }
        public searchResultDocumentSets[] searchResultDocumentSets;  //{ get; set; }
        public string dateOfCalculation { get; set; }
        public string factCount { get; set; }
        public string factScore { get; set; }
        public string importantParagraphScore { get; set; }
        public string importantParagraphCount { get; set; }
        public string issueCount { get; set; }
        public string issueScore { get; set; }
        public string orbiterDictumCount { get; set; }
        public string orbiterDictumScore { get; set; }
        public string precedentCount { get; set; }
        public string precedentScore { get; set; }
        public string ratioDecidenciCount { get; set; }
        public string ratioDecidenciScore { get; set; }
        public string summaryCount { get; set; }
        public string summaryScore { get; set; }

    }

    public class searchResultDocumentSets
    {
        public string docType { get; set; }
        public documents[] documents { get; set; }
        public string searchCount { get; set; }

    }

    public class documents
    {
        public string dateOfCalculation { get; set; }
        public string factCount { get; set; }
        public string factScore { get; set; }
        public string importantParagraphScore { get; set; }
        public string issueCount { get; set; }
        public string issueScore { get; set; }
        public string orbiterDictumCount { get; set; }
        public string orbiterDictumScore { get; set; }
        public string precedentCount { get; set; }
        public string precedentScore { get; set; }
        public string ratioDecidenciCount { get; set; }
        public string ratioDecidenciScore { get; set; }
        public string summaryCount { get; set; }
        public string summaryScore { get; set; }

    }
    #endregion
}