using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DA_SKORKEL;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;

public partial class Create_Group : System.Web.UI.Page
{
    DO_Scrl_UserGroupDetailTbl objDOgroup = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDAgroup = new DA_Scrl_UserGroupDetailTbl();

    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_Scrl_UserGroupJoin objGrpJoinDO = new DO_Scrl_UserGroupJoin();
    DA_Scrl_UserGroupJoin objGrpJoinDA = new DA_Scrl_UserGroupJoin();

    DA_Profile ObjDAprofile = new DA_Profile();
    DO_Profile objDoProfile = new DO_Profile();

    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();

    DO_Registrationdetails objdoreg = new DO_Registrationdetails();
    DA_Registrationdetails objdareg = new DA_Registrationdetails();

    DA_MyPoints objDAPoint = new DA_MyPoints();
    DO_MyPoints objDOPoint = new DO_MyPoints();

    DO_WallMessage WallMessageDO = new DO_WallMessage();
    DA_WallMessage WallMessageDA = new DA_WallMessage();


    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Session.Remove("ImagePath");
            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Add Group";
            string ukey = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + ViewState["UserID"];
            ViewState["ukey"] = ukey;
            int LoginId = Convert.ToInt32(Session["ExternalUserId"]);
            int FrndId = 0;
            ViewState["UserID"] = LoginId;
            ViewState["RegId"] = FrndId;
            LoadEndrosAndMsg();
            TotalscoreCount();
            ConnectedUserAdd();
            GetProfileDetails(Convert.ToInt32(ViewState["UserID"]));
            ViewState["join"] = "A";
            getInviteeName();
            SubjectTempDataTableGroup();
            BindGroupSubjectList();
            divEditUserProfile.Style.Add("display", "block");
            lnkChangeImage.Visible = true;
            imgGroupUser.Src = "~/images/groupPhoto.jpg";
        }
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
                    }
                    else
                    {
                        imgUser.Src = "images/profile-photo.png";
                        ViewState["imgComment"] = "images/profile-photo.png";
                    }

                }
                catch
                {
                    imgUser.Src = "images/profile-photo.png";
                    ViewState["imgComment"] = "images/profile-photo.png";
                }
            }
            else
            {
                imgUser.Src = "images/profile-photo.png";
                ViewState["imgComment"] = "images/profile-photo.png";

            }

            lblUserProfName.Text = Convert.ToString(dt.Rows[0]["NAME"]);
            ViewState["LoginName"] = lblUserProfName.Text;
        }
    }

    protected void lnkChangeImage_Click(object sender, EventArgs e)
    {
        divEditProfile.Style.Add("display", "none");
        PopUpCropImage.Style.Add("display", "block");
    }

    protected void lnkCancelProfilediv_click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void lnkeditProfile_click(object sender, EventArgs e)
    {
        PopUpCropImage.Style.Add("display", "none");
        EditUserProfile(Convert.ToInt32(ViewState["UserID"]));
        divEditProfile.Style.Add("display", "block");
    }

    protected void lnkCancelProfile_click(object sender, EventArgs e)
    {
        //GetProfileDetails(Convert.ToInt32(Session["ExternalUserId"]));
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

    protected void LoadEndrosAndMsg()
    {
        objDoProfile.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
        objDoProfile.ConnectRegistrationId = Convert.ToInt32(ViewState["RegId"]);
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
        objDoProfile.striInvitedUserId = Convert.ToString(ViewState["UserID"]);
    }

    protected void ConnectedUserAdd()
    {
        // lnkAddFriend
        objdoreg.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
        objdoreg.InvitedUserId = Convert.ToInt32(ViewState["RegId"]);
        DataTable dtReq = new DataTable();
        dtReq = objdareg.GetExistsRequest(objdoreg, DA_Registrationdetails.RegistrationDetails.SingleRecord);
        if (dtReq.Rows.Count > 0)
        {
            int IsAccepted = Convert.ToInt32(dtReq.Rows[0]["IsAccepted"]);
            if (IsAccepted == 0)
            {
                hdnConnCommandName.Value = "C";
            }
            else if (IsAccepted == 2)
            {
                hdnConnCommandName.Value = "C";

            }
            else
            {
                hdnConnCommandName.Value = "DC";
            }
        }
        else
        {
            hdnConnCommandName.Value = "C";
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
        string totalPersonalPt = objDAPoint.GetTotalscore(objDOPoint, DA_MyPoints.MyPoint.GetTotalScore);
        if (totalPersonalPt == "")
        {
            totalPersonalPt = "0";
        }

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

        FinalTotalexpScore = FinalTotalexpScore + TotalexpScore;
        int AllScore = Convert.ToInt32(totalPersonalPt) + FinalTotalexpScore;
        lblMessCount.Text = Convert.ToString(AllScore);
        ViewState["Totalexp"] = null;
    }

    protected void lblEndorseCount_click(object sender, EventArgs e)
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            Response.Redirect("Home.aspx?RegId=" + Request.QueryString["RegId"] + "&Endrosment=E");
            //Home.aspx?RegId=532
        }
        else
        {
            Response.Redirect("Home.aspx?Endrosment=E");
        }

    }

    protected void lnkHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx?ActiveStatus=H");
    }

    protected void lnkDocuments_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx?ActiveStatus=D");
    }

    protected void lnkWorkex_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx?ActiveStatus=W");
    }

    protected void lnkEducation_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx?ActiveStatus=E");
    }

    protected void lnkAchievements_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx?ActiveStatus=Ac");
    }

    private void BindGroupSubjectList()
    {
        DataTable dtSub = new DataTable();
        dtSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.AllRecord);
        if (dtSub.Rows.Count > 0)
        {
            lstCreateGroup.DataSource = dtSub;
            lstCreateGroup.DataBind();
        }
        else
        {
            lstCreateGroup.DataSource = null;
            lstCreateGroup.DataBind();
        }
    }

    protected void SubjectTempDataTableGroup()
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
        ViewState["SubjectCategorygr"] = dtSubjCat;
    }

    protected void lstCreateGroup_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
        SubLi.Attributes.Add("class", "graycreateGroup");
    }

    protected void imgAutobtn_Click(object sender, EventArgs e)
    {
        ViewState["join"] = "A";
        if (imgAutojoin.ImageUrl != "images/checked2.png")
        {
            imgAutojoin.ImageUrl = "images/checked2.png";
            imgReqjoin.ImageUrl = "images/unchecked2.png";
        }
    }

    protected void imgReqjoin_Click(object sender, EventArgs e)
    {
        ViewState["join"] = "R";
        if (imgReqjoin.ImageUrl == "images/unchecked2.png")
        {
            imgAutojoin.ImageUrl = "images/unchecked2.png";
            imgReqjoin.ImageUrl = "images/checked2.png";
        }
    }

    protected void btnSaveGroup_Click(object sender, EventArgs e)
    {
        if (hdnAutoreqJoin.Value == "1")
        {
            ViewState["join"] = "A";
        }
        else
        {
            ViewState["join"] = "R";
        }

       // return;
        String GroupeAccess = String.Empty;

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        string PhotoPath = "";

        if (FileUpload1.HasFile)
        {
            string name = FileUpload1.FileName;
            string ext = System.IO.Path.GetExtension(name);

            if (ext.Trim() != ".jpg" && ext.Trim() != ".jpeg" && ext.Trim() != ".png" && ext.Trim() != ".gif" && ext.Trim() != ".org")
            {
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
                lblSuccMess.Text = "";
            }
            else
                PhotoPath = ViewState["ImagePath"].ToString();
        }

        if (ViewState["ImagePath"] != null)
        {
            objDOgroup.strLogoPath = PhotoPath.ToString();
        }

        objDOgroup.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        objDOgroup.strGroupName = txtCreateGr.Text.Trim();
        ViewState["strGroupName"] = txtCreateGr.Text.Trim(); 
        if (txtPurpose.InnerText.Trim() != "Description")
        {
            objDOgroup.strSummary = txtPurpose.InnerText.Trim();
        }
        else
        {
            objDOgroup.strSummary = "";
        }

        objDOgroup.strUniqueKey = ViewState["ukey"].ToString();
        if (ViewState["join"].ToString() == "A")
        {
            objDOgroup.strAccess = Convert.ToString(ViewState["join"]);
        }
        else
        {
            objDOgroup.strAccess = Convert.ToString(ViewState["join"]);
        }
        objDOgroup.strInvitationMessage = "";


        objDOgroup.strIpAddress = ip;

        objDOgroup.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.Insert);
        ViewState["GroupOutId"] = objDOgroup.GroupOutId;

        objDOgroup.strIpAddress = ip;
        string totalMembers = hdnMembId.Value;
        objDOgroup.strUniqueKey = ViewState["ukey"].ToString();

        if (hdnSubject.Value != "")
        {
            string[] totalSubjects = hdnSubject.Value.Split(',');
            var dictionary = new Dictionary<int, int>();

            Dictionary<string, int> counts = totalSubjects.GroupBy(x => x)
                                          .ToDictionary(g => g.Key,
                                                        g => g.Count());

            foreach (var v in counts)
            {
                if (v.Value == 1)
                {
                    objDOgroup.inGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                    objDOgroup.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDOgroup.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.InserCotextOrgGroupDocument);
                }
                else if (v.Value == 3)
                {
                    objDOgroup.inGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                    objDOgroup.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDOgroup.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.InserCotextOrgGroupDocument);

                }
                else if (v.Value == 5)
                {
                    objDOgroup.inGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                    objDOgroup.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDOgroup.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.InserCotextOrgGroupDocument);

                }
                else if (v.Value == 7)
                {
                    objDOgroup.inGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                    objDOgroup.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDOgroup.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.InserCotextOrgGroupDocument);

                }
                else if (v.Value == 9)
                {
                    objDOgroup.inGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                    objDOgroup.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDOgroup.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.InserCotextOrgGroupDocument);

                }

            }
        }

        string[] members = totalMembers.Split(',');
        for (int i = 0; i < members.Length; i++)
        {
            if ( Convert.ToString(members.GetValue(i)) != "")
            {
                int IDs = Convert.ToInt32(members.GetValue(i));
                objDOgroup.inGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                objDOgroup.inviteMembers = Convert.ToString(IDs);
                objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.InsertGroupInvitation);

                if (ViewState["join"].ToString() == "A")
                {
                    objGrpJoinDO.inGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                    objGrpJoinDO.intInvitedUserId = Convert.ToInt32(ViewState["UserID"]);

                    ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ip == null)
                        ip = Request.ServerVariables["REMOTE_ADDR"];
                    objGrpJoinDO.strIpAddress = ip;

                    objGrpJoinDO.intAddedBy = Convert.ToInt32(IDs);
                    objGrpJoinDO.intRegistrationId = Convert.ToInt32(IDs);
                    objGrpJoinDO.isAccepted = 1;
                    objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Insert);
                }

            }
        }

        for (int i = 0; i < members.Length; i++)
        {
            if (Convert.ToString(members.GetValue(i)) != "")
            {
                int IDs = Convert.ToInt32(members.GetValue(i));
                WallMessageDO.intAddedBy = Convert.ToInt32(Convert.ToString(Session["ExternalUserId"]));
                WallMessageDO.intGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                if (ViewState["join"].ToString() == "A")
                {
                    WallMessageDO.StrRecommendation = "Group Owner added You to Group.";
                }
                else
                {
                    WallMessageDO.StrRecommendation = "Group Owner Invite You to Join Group.";
                }
                WallMessageDO.strIpAddress = ip;
                WallMessageDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                WallMessageDO.intInvitedUserId = Convert.ToInt32(IDs);
                WallMessageDO.striInvitedUserId = Convert.ToString(IDs);
                WallMessageDO.strSubject = "Group Invitation";
                WallMessageDO.strTotalGrpMemberID = txtCreateGr.Text.Trim();
                WallMessageDA.Scrl_AddEditDelWallMessage(WallMessageDO, DA_WallMessage.WallMessage.Add);


            }
        }
        
        getInviteeName();
        string ID = "ctl00$ContentPlaceHolder1$lnkCreateGroup";
        hdnTabId.Value = ID;

        if (ISAPIURLACCESSED != "0")
        {
            if (!string.IsNullOrEmpty(Convert.ToString(ViewState["GroupOutId"])))
            {
                string UserURL = APIURL + "createGroup.action?groupId=" + objDOgroup.inGroupId +
                    "&groupName=" + objDOgroup.strGroupName +
                    "&scope=PRIVATE&groupParent=Null&groupOwner=" + objDOgroup.intRegistrationId +
                    "&groupCreationDate=" + DateTime.Now.ToString();
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
                        objAPILogDO.strAPIType = "Group";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }


                    UserURL = APIURL + "addMemberToGroup.action?" +
                     "groupId=GRP" + Request.QueryString["GrpId"] +
                     "&members=" + totalMembers;

                    HttpWebRequest myRequest11 = (HttpWebRequest)WebRequest.Create(UserURL);
                    myRequest11.Method = "GET";
                    if (ISAPIResponse != "0")
                    {
                        WebResponse myResponse11 = myRequest1.GetResponse();

                        StreamReader sr1 = new StreamReader(myResponse11.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result1 = sr1.ReadToEnd();

                        objAPILogDO.strURL = UserURL;
                        objAPILogDO.strAPIType = "Group Member";
                        objAPILogDO.strResponse = result1;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strIPAddress = objGrpJoinDO.strIpAddress;
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }

                }
                catch { }
            }
        }
        clearGroup();
        divDeletesucess.Style.Add("display", "block");
    }

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


    protected void fileuplad_onload(object sender, EventArgs e)
    {

        string PhotoPath = "";
        if (FileUpload1.HasFile)
        {
            string name = FileUpload1.FileName;
            string ext = System.IO.Path.GetExtension(name);

            if (ext.Trim() != ".jpg" && ext.Trim() != ".jpeg" && ext.Trim() != ".png" && ext.Trim() != ".gif" && ext.Trim() != ".org")
            {
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
                imgGroupUser.Src = "CroppedPhoto/" + Session["ImagePath"].ToString();
                ImgRemovePic.Visible = true;
                lblSuccMess.Text = "";
            }
            else
                PhotoPath = ViewState["ImagePath"].ToString();
        }
        else
        {
            lblSuccMess.Text = "Please select image to upload.";
            lblSuccMess.CssClass = "RedErrormsg";
            return;
        }
    }

    protected void ImgRemovePic_OnClick(object sender, EventArgs e)
    {
        string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + Session["ImagePath"].ToString());
        if (File.Exists(imgPathPhysical))
        {
            File.Delete(imgPathPhysical);
            imgGroupUser.Src = "~/images/groupPhoto.jpg";
            ImgRemovePic.Visible = false;
            ViewState["ImagePath"] = null;
            Session["ImagePath"] = null;
        }
    }

    private void SendMailGroup()
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
            string MailSSL = ConfigurationManager.AppSettings["MailSSL"];
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];
            string MailTo = Convert.ToString(ViewState["TotalMailId"]);
            string Mailbody = "";

            NetworkCredential cre = new NetworkCredential(username, Password);
            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = true;
            if (MailSSL != "0")
                clientip.EnableSsl = true;

            clientip.Credentials = cre;

            try
            {

                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom);
                Rmm2.Body = Mailbody.ToString();
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("");

                DataTable dtMailId = new DataTable();
                objDOgroup.strUniqueKey = ViewState["ukey"].ToString();
                dtMailId = objDAgroup.GetDataTable(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetMailId);
                if (dtMailId.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMailId.Rows.Count; i++)
                    {
                        string name = Convert.ToString(dtMailId.Rows[i]["Name"]);
                        string MailId = Convert.ToString(dtMailId.Rows[i]["vchrUserName"]);
                        if (MailId != null)
                        {
                            //MailTo += "," + MailId;
                            MailTo = MailId;
                            htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel group joining invitation</b>" 
                                + "<br><br>" + "Dear " + name + " "
                                + "<br><br>" + ViewState["LoginName"]
                                + " request you to allow joining " + ViewState["strGroupName"] + " group. <br><br>" 
                                + "Thanks," + "<br>" 
                                + "The Skorkel Team<br><br>"
                                + "****This is a system generated Email. Kindly do not reply****", null, "text/html");
                            Rmm2.To.Clear();
                            Rmm2.To.Add(MailTo);
                            Rmm2.Subject = "Skorkel Group Joining Request.";
                            Rmm2.AlternateViews.Add(htmlView);
                            Rmm2.IsBodyHtml = true;
                            clientip.Send(Rmm2);
                            Rmm2.To.Clear();
                            // ViewState["TotalMailId"] = TotalMailId;
                        }
                    }
                }

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

    public void clearGroup()
    {
        ViewState["join"] = "A";
        hdnSubject.Value = "";
        lblSuccMess.Text = "";
        Session.Remove("ImagePath");
        ViewState["SubjectCategorygr"] = null;
        BindGroupSubjectList();
        SubjectTempDataTableGroup();
        txtCreateGr.Text = "";
        txtPurpose.InnerText = "";
        string ukey1 = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + ViewState["UserID"];
        ViewState["ukey"] = ukey1;
        imgGroupUser.Src = "~/images/groupPhoto.jpg";
        ImgRemovePic.Visible = false;
        ViewState["SubjectCategorygr"] = null;
        SubjectTempDataTableGroup();
        hdnMembId.Value = "";
    }

}