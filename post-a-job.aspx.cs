using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Web.UI;
using System.Net;
using System.IO;

public partial class post_a_job : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DO_Scrl_UserJobPostingTbl objDOBJobPosting = new DO_Scrl_UserJobPostingTbl();
    DA_Scrl_UserJobPostingTbl objDAJobPosting = new DA_Scrl_UserJobPostingTbl();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string APIURL = ConfigurationManager.AppSettings["APIURL"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            btnSave.Visible = true;
            btnUpdate.Visible = false;
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
            txtOther.Style.Add("display", "none");

            //  txtOther.Visible = false;
            BindCity();

            if (Request.QueryString["GrpId"] != "" && Request.QueryString["JobId"] != null)
            {
                btnSave.Visible = false;
                btnUpdate.Visible = true;
                int JobID = Convert.ToInt32(Request.QueryString["JobId"]);
                EditJob(JobID);
                hdnJobId.Value = JobID.ToString();
            }

            GetAssignRole();
        }
    }

    protected void BindCity()
    {
        dt = objDAJobPosting.GetDataTable(objDOBJobPosting, DA_Scrl_UserJobPostingTbl.Scrl_UserJobPostingTbl.GetAllCity);
        if (dt.Rows.Count > 0)
        {
            lstCity.DataSource = dt;
            lstCity.DataTextField = "vchrCityName";
            lstCity.DataValueField = "intCityId";
            lstCity.DataBind();
        }
    }

    public void EditJob(int JobId)
    {
        objDOBJobPosting.intJobPostingId = JobId;
        DataTable dtedit = objDAJobPosting.GetDataTable(objDOBJobPosting, DA_Scrl_UserJobPostingTbl.Scrl_UserJobPostingTbl.EditJobPost);
        if (dtedit.Rows.Count > 0)
        {
            txtTitle.Text = dtedit.Rows[0]["strTitle"].ToString();
            txtOrgName.Text = dtedit.Rows[0]["strOrganization"].ToString();
            if (dtedit.Rows[0]["expiryDate"].ToString() != "")
            {
                txtExpireDate.Text = Convert.ToDateTime(dtedit.Rows[0]["expiryDate"]).ToString("dd-MMM-yyyy");
                rdblDays.Checked = true;
            }

            CKDescription.InnerText = dtedit.Rows[0]["strDescription"].ToString().Trim().Replace("<br>", "\n");
            string JobType = dtedit.Rows[0]["strJobType"].ToString();
            string OtheCity = dtedit.Rows[0]["strOtherCity"].ToString();
            txtOther.Text = OtheCity;
            if (txtOther.Text == "Other" || txtOther.Text == "")
            {
                txtOther.Style.Add("display", "none");
            }
            else { txtOther.Style.Add("display", "block"); }
            //For Multple Job Type selection
            string[] JobTypeVal = JobType.Split(',');
            for (int i = 0; i < JobTypeVal.Length; i++)
            {
                string selectedvalue = JobTypeVal[i];
                if (selectedvalue != "")
                {
                    foreach (ListItem item in chkJobType.Items)
                    {
                        if (item.Value == selectedvalue)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }

            //For Multiple City selection
            lstCity.SelectionMode = ListSelectionMode.Multiple;
            string strcityId = dtedit.Rows[0]["StrCityId"].ToString();
            string[] CityVal = strcityId.Split(',');
            for (int i = 0; i < CityVal.Length; i++)
            {
                string selectedCity = CityVal[i];
                if (selectedCity != "")
                {
                    foreach (ListItem item in lstCity.Items)
                    {
                        string sss = item.Value;
                        if (item.Value == selectedCity)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }

            //rdblNever.Checked=
            string Expiry = dtedit.Rows[0]["strExpiry"].ToString();
            if (Expiry == "Never")
            {
                rdblNever.Checked = true;
            }

        }
    }

    #region AssignRole
    public void GetAssignRole()
    {
        if (ViewState["intGroupId"] != null)
        {
            objgrp.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
            objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            DataSet dschk = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.ViewGrpAssignUser);
            string Status = string.Empty;

            DataTable dtGrpOpt = objgrpDB.GetDataTable(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.ChkGroupOption);

            if (dtGrpOpt.Rows[0]["strAccess"].ToString() == "A")
            {
                DivHome.Style.Add("display", "block");
                DivForumTab.Style.Add("display", "block");
                DivUploadTab.Style.Add("display", "block");
                DivPollTab.Style.Add("display", "block");
                DivEventTab.Style.Add("display", "block");
                DivJobTab.Style.Add("display", "block");
                DivMemberTab.Style.Add("display", "block");
            }
            else
            {
                if (dschk.Tables[0].Rows[0][0].ToString() != "0")
                {
                    DivHome.Style.Add("display", "block");
                    DivForumTab.Style.Add("display", "block");
                    DivUploadTab.Style.Add("display", "block");
                    DivPollTab.Style.Add("display", "block");
                    DivEventTab.Style.Add("display", "block");
                    DivJobTab.Style.Add("display", "block");
                    DivMemberTab.Style.Add("display", "block");
                }
                else
                {
                    GetAccessModuleDetails();
                }
            }
        }
    }

    protected void GetAccessModuleDetails()
    {
        objgrp.intUserId = Convert.ToInt32(ViewState["UserID"]);
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        dt = objgrpDB.GetDataTable(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpModuleDetailsAcces);
        //Profile,Home=Wall,Uploads=Uploads,Events=Events,Discussion=Forum,Polls=Polls,Jobs=Jobs,Members=Members
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
                    case "Jobs": DivJobTab.Style.Add("display", "block");
                        break;
                    case "Members": DivMemberTab.Style.Add("display", "block");
                        break;

                }
            }

        }

    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ListItem[] lstitem = new ListItem[lstCity.Items.Count];
        string CityId = "";
        string TotalCity = "";
        string ChkSelected = "";
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        objDOBJobPosting.strIpAddress = ip;
        objDOBJobPosting.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
        objDOBJobPosting.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objDOBJobPosting.strTitle = txtTitle.Text.Trim().Replace("'", "''");
        objDOBJobPosting.strDescription = CKDescription.InnerText.Trim().Replace("'", "''").Replace("\n", "<br>");
        for (int i = 0; i < lstCity.Items.Count; i++)
        {

            if (lstCity.Items[i].Selected)
            {
                CityId = lstCity.Items[i].Value;
                TotalCity += "," + CityId;
            }

        }
        objDOBJobPosting.StrCityId = TotalCity;
        objDOBJobPosting.strOtherCity = txtOther.Text.Trim().Replace("'", "''");
        if (chkJobType.SelectedItem != null)
        {
            foreach (ListItem item in chkJobType.Items)
            {
                if (item.Selected)
                {
                    ChkSelected += "," + item.Value;
                }

            }
        }
        else
        {
            lblDateMess.Text = "Select at least one job type.";
            return;
        }
        objDOBJobPosting.strJobType = ChkSelected;
        if (rdblNever.Checked == true)
        {
            objDOBJobPosting.strExpiry = "Never";
        }
        else
        {
            if (txtExpireDate.Text != "Select Date")
            {
                objDOBJobPosting.expiryDate = Convert.ToDateTime(txtExpireDate.Text);
                DateTime todaydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                if (objDOBJobPosting.expiryDate < todaydate)
                {
                    lblDateMess.Text = "Date not less than today's date.";
                    return;
                }
                else
                {
                    objDOBJobPosting.expiryDate = Convert.ToDateTime(txtExpireDate.Text);
                }
            }
        }
        objDOBJobPosting.strOrganization = txtOrgName.Text.Trim().Replace("'", "''");
        objDOBJobPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objDAJobPosting.AddEditDel_Scrl_UserJobPostingTbl(objDOBJobPosting, DA_Scrl_UserJobPostingTbl.Scrl_UserJobPostingTbl.Insert);
        ViewState["JobPostingId"] = objDOBJobPosting.intJobPostingId;
        if (ISAPIURLACCESSED == "1")
        {
            try
            {
                 // UserURL = APIURL + "createPoll.action?" +

                String url = APIURL + "createJobPosting.action?" +
                       "groupId=GRP" + ViewState["intGroupId"] +
                       "&jobId=JOB" + ViewState["JobPostingId"] +
                       "&groupOwnerId=USR" + Session["GroupOwnerId"] +
                       "&insertDt=" + DateTime.Now +
                       "&content=" + objDOBJobPosting.strDescription +
                       "&scope=" + null +
                       "&title=" + objDOBJobPosting.strTitle;


                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                myRequest1.Method = "GET";
                WebResponse myResponse1 = myRequest1.GetResponse();

                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                String result = sr.ReadToEnd();

                objAPILogDO.strURL = url;
                objAPILogDO.strAPIType = "Group Job";
                objAPILogDO.strResponse = result;
                objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objAPILogDO.strIPAddress = ip;
                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);

            }
            catch
            {

            }
        }
        clear();
        lblEditSuccess.Visible = false;
        lblSuccess.Visible = true;
        divJobSuccess.Style.Add("display", "block");
        Response.Redirect("jobs.aspx?GrpId=" + ViewState["intGroupId"]);
        //lblMessage.Text = "Job posted successfully.";
        //lblMessage.ForeColor = System.Drawing.Color.Green;

    }

    protected void lstCity_SelectedIndexChanged(object sender, EventArgs e)
    {

        //if (lstCity.SelectedItem.Value == "57")
        //{
        //    txtOther.Visible = true;
        //}
        //else
        //{
        //    txtOther.Visible = false;
        //}
    }

    public void clear()
    {
        txtTitle.Text = "";
        CKDescription.InnerText = "";
        lstCity.ClearSelection();
        txtOther.Text = "";
        chkJobType.ClearSelection();
        rdblNever.Checked = false;
        txtExpireDate.Text = "";
        txtOrgName.Text = "";

    }

    protected void rdblNever_CheckedChanged(object sender, EventArgs e)
    {
        //if (rdblNever.Checked == true)
        //{
        txtExpireDate.Enabled = false;
        //}

    }

    protected void rdblDays_CheckedChanged1(object sender, EventArgs e)
    {
        //if (rdblDays.Checked == true)
        //{
        txtExpireDate.Enabled = true;
        // pnlExpiry.Enabled = true;
        //}
    }

    protected void lnkAllJobs_Click(object sender, EventArgs e)
    {
        Response.Redirect("jobs.aspx?GrpId=" + ViewState["intGroupId"]);
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

    protected void lnkJobTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("jobs.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkPollTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("Polls-Details.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkEventTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("group-event-main.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkMemberTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("groups-members.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    #endregion

    protected void lnkClose_Click(object sender, EventArgs e)
    {
        divJobSuccess.Style.Add("display", "none");

        Response.Redirect("jobs.aspx?GrpId=" + ViewState["intGroupId"] + "", true);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        lblEditSuccess.Visible = true;
        lblSuccess.Visible = false;
        ListItem[] lstitem = new ListItem[lstCity.Items.Count];
        string CityId = "";
        string TotalCity = "";
        string ChkSelected = "";
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        objDOBJobPosting.strIpAddress = ip;
        objDOBJobPosting.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
        objDOBJobPosting.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objDOBJobPosting.strTitle = txtTitle.Text.Trim().Replace("'", "''");
        objDOBJobPosting.strDescription = CKDescription.InnerText.Trim().Replace("'", "''").Replace("\n", "<br>");
        objDOBJobPosting.intModifiedBy = Convert.ToInt32(ViewState["UserID"]);

        for (int i = 0; i < lstCity.Items.Count; i++)
        {

            if (lstCity.Items[i].Selected)
            {
                CityId = lstCity.Items[i].Value;
                TotalCity += "," + CityId;
            }

        }
        objDOBJobPosting.StrCityId = TotalCity;
        objDOBJobPosting.strOtherCity = txtOther.Text.Trim().Replace("'", "''");
        if (chkJobType.SelectedItem != null)
        {
            foreach (ListItem item in chkJobType.Items)
            {
                if (item.Selected)
                {
                    ChkSelected += "," + item.Value;
                }
            }
        }
        objDOBJobPosting.strJobType = ChkSelected;
        if (rdblNever.Checked == true)
        {
            objDOBJobPosting.strExpiry = "Never";
            // objDOBJobPosting.expiryDate = DateTime.Now;
        }
        else
        {
            objDOBJobPosting.expiryDate = Convert.ToDateTime(txtExpireDate.Text);
        }
        objDOBJobPosting.strOrganization = txtOrgName.Text.Trim().Replace("'", "''");
        objDOBJobPosting.intJobPostingId = Convert.ToInt32(hdnJobId.Value);
        objDOBJobPosting.strOtherCity = txtOther.Text;
        objDAJobPosting.AddEditDel_Scrl_UserJobPostingTbl(objDOBJobPosting, DA_Scrl_UserJobPostingTbl.Scrl_UserJobPostingTbl.Update);
        ViewState["JobPostingId"] = objDOBJobPosting.intJobPostingId;
        //try
        //{
        //    String url = "http://192.168.168.13:8082/SkorkelWeb/createJobPosting.action?" +
        //           "groupId=GRP" + ViewState["intGroupId"] +
        //           "&jobId=JOB" + ViewState["JobPostingId"] +
        //           "&groupOwnerId=USR" + Session["GroupOwnerId"] +
        //           "&insertDt=" + DateTime.Now +
        //           "&content=" + objDOBJobPosting.strDescription +
        //           "&scope=" + null +
        //           "&title=" + objDOBJobPosting.strTitle;


        //    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
        //    myRequest1.Method = "GET";
        //    WebResponse myResponse1 = myRequest1.GetResponse();

        //    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
        //    String result = sr.ReadToEnd();

        //    objAPILogDO.strURL = url;
        //    objAPILogDO.strAPIType = "Group Job";
        //    objAPILogDO.strResponse = result;
        //    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        //    objAPILogDO.strIPAddress = ip;
        //    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);

        //}
        //catch
        //{

        //}
        clear();
        divJobSuccess.Style.Add("display", "block");
        //lblMessage.Text = "Job posted successfully.";
        //lblMessage.ForeColor = System.Drawing.Color.Green;

        // ScriptManager.RegisterStartupScript(this, this.GetType(), "statrus", "AlertUpdate()", true);

    }

    protected void btnStatus_Click(object sender, EventArgs e)
    {
        Response.Redirect("jobs.aspx?GrpId=" + ViewState["intGroupId"] + "", true);
    }
}