using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Net;
using System.IO;

public partial class create_poll : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    DO_Scrl_UserPollTbl objDOPoll = new DO_Scrl_UserPollTbl();
    DA_Scrl_UserPollTbl objDAPoll = new DA_Scrl_UserPollTbl();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divSuccessPolls.Style.Add("display","none");
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

            AccessModulePermisssion();

            if (Request.QueryString["PollId"] != "" && Request.QueryString["PollId"] != null)
            {
                ViewState["PollId"] = Request.QueryString["PollId"];
                EditPoll();
                lnkSavePoll.Text = "Update Poll";
            }
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

    protected void EditPoll()
    {
        objDOPoll.intPollId = Convert.ToInt32(ViewState["PollId"]);
        DataTable dtPoll = objDAPoll.GetDataTable(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.SingleRecord);
        if (dtPoll.Rows.Count > 0)
        {
            txtQuestion.Text = dtPoll.Rows[0]["strPollName"].ToString();
            txtDescription.InnerHtml = dtPoll.Rows[0]["strDescription"].ToString();

            TextBox1.Text = dtPoll.Rows[0]["stroption1"].ToString();

            if (!string.IsNullOrEmpty(dtPoll.Rows[0]["stroption2"].ToString()))
            {
                TextBox2.Text = dtPoll.Rows[0]["stroption2"].ToString();
            }
            if (!string.IsNullOrEmpty(dtPoll.Rows[0]["stroption3"].ToString()))
            {
                TextBox3.Text = dtPoll.Rows[0]["stroption3"].ToString();
                div3.Style.Add("display","block");
            }
            if (!string.IsNullOrEmpty(dtPoll.Rows[0]["stroption4"].ToString()))
            {
                TextBox4.Text = dtPoll.Rows[0]["stroption4"].ToString();
                div4.Style.Add("display", "block");
            }
            if (!string.IsNullOrEmpty(dtPoll.Rows[0]["stroption5"].ToString()))
            {
                TextBox5.Text = dtPoll.Rows[0]["stroption5"].ToString();
                div5.Style.Add("display", "block");
            }
            if (!string.IsNullOrEmpty(dtPoll.Rows[0]["stroption6"].ToString()))
            {
                TextBox6.Text = dtPoll.Rows[0]["stroption6"].ToString();
                div6.Style.Add("display", "block");
            }
            if (!string.IsNullOrEmpty(dtPoll.Rows[0]["stroption7"].ToString()))
            {
                TextBox7.Text = dtPoll.Rows[0]["stroption7"].ToString();
                div7.Style.Add("display", "block");
            }
            if (!string.IsNullOrEmpty(dtPoll.Rows[0]["stroption8"].ToString()))
            {
                TextBox8.Text = dtPoll.Rows[0]["stroption8"].ToString();
                div8.Style.Add("display", "block");
            }
            if (!string.IsNullOrEmpty(dtPoll.Rows[0]["stroption9"].ToString()))
            {
                TextBox9.Text = dtPoll.Rows[0]["stroption9"].ToString();
                div9.Style.Add("display", "block");
            }
            if (!string.IsNullOrEmpty(dtPoll.Rows[0]["stroption10"].ToString()))
            {
                TextBox10.Text = dtPoll.Rows[0]["stroption10"].ToString();
                div10.Style.Add("display", "block");
            }
            if ((dtPoll.Rows[0]["strVotingPattern"].ToString() == "Single"))
            {
                rdbSinglePattern.Checked = true;
                rdbMultiplePattern.Checked = false;
            }
            else
            {
                rdbMultiplePattern.Checked = true;
                rdbSinglePattern.Checked = false;
            }
            if (!string.IsNullOrEmpty(dtPoll.Rows[0]["strVotingEnds"].ToString()))
            {
                rdVotingNeverEnds.Checked = true;
                rdVotingEnds.Checked = false;
            }
            else
            {
                rdVotingNeverEnds.Checked = false;
                rdVotingEnds.Checked = true;
                txtExpireDate.Text= dtPoll.Rows[0]["dtPollExpireDate"].ToString();
                ddlTime.SelectedValue = dtPoll.Rows[0]["strPollExpireTime"].ToString();

            }

        }
    }

    protected void lnkSavePoll_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if (rdVotingNeverEnds.Checked != true)
        {
            if (txtExpireDate.Text == "")
            {
                lblDateMessage.Style.Add("display", "block");
                lblDateMessage.Text = "Please select poll expire date.";
                return;
            }
            else
            {
                string ExpTime = ddlTime.SelectedValue;
                DateTime curTime = DateTime.Now;
                DateTime ETime = DateTime.Parse(ExpTime);

                DateTime Currdt = DateTime.Now;
                DateTime Selecdt = Convert.ToDateTime(txtExpireDate.Text);
                if (Currdt.Date < Selecdt.Date)
                {

                }
                else if (Currdt.Date == Selecdt.Date)
                {
                        if (ETime <= curTime)
                        {
                            lblDateMessage.Style.Add("display", "block");
                            lblDateMessage.Text = "Expire time should be equal to or greater than today's time";
                            return;
                        }
                }
            }
        }
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (ViewState["PollId"] == null)
        {
            objDOPoll.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
            objDOPoll.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objDOPoll.strPollName = txtQuestion.Text.Replace("'", "''");
            objDOPoll.strDescription = txtDescription.InnerHtml;
            if (txtDescription.InnerHtml == "Description")
            {
                objDOPoll.strDescription = "";
            }

            if (rdbSinglePattern.Checked == true)
                objDOPoll.strVotingPattern = "Single";
            else
                objDOPoll.strVotingPattern = "Multiple";

            if (rdVotingNeverEnds.Checked == true)
            {
                objDOPoll.strVotingEnds = "Never";
            }
            else if (rdVotingEnds.Checked == true)
            {
                if (Convert.ToDateTime(txtExpireDate.Text.Trim().Replace("'", "''")) < DateTime.Today)
                {
                    lblDateMessage.Style.Add("display", "block");
                    lblDateMessage.Text = "Expire date should be equal to or greater than today's date";
                    return;
                }

                objDOPoll.strVotingEnds = "";
                objDOPoll.dtPollExpireDate = Convert.ToDateTime(txtExpireDate.Text.Trim().Replace("'", "''"));
                objDOPoll.strPollExpireTime = ddlTime.SelectedValue;
            }

            if (rdVoteTypePublic.Checked == true)
                objDOPoll.strVoteType = "Public";
            else
                objDOPoll.strVoteType = "Group Member";

            if (TextBox1.Text == "Option 1" && TextBox2.Text == "Option 2" && TextBox3.Text == "Option 3" && TextBox4.Text == "Option 4" && TextBox5.Text == "Option 5" && TextBox6.Text == "Option 6" && TextBox7.Text == "Option 7" && TextBox8.Text == "Option 8" && TextBox9.Text == "Option 9" && TextBox10.Text == "Option 10")
            {
                lblMessage.Text = "Please enter at least two option.";
                lblMessage.CssClass = "RedErrormsg";
                return;
            }
          
            if (TextBox1.Text != "Option 1")
            {
                if (TextBox1.Text.Trim() == "")
                {
                    lblMessage.Text = "Please enter option 1.";
                    lblMessage.CssClass = "RedErrormsg";
                    return;
                }
                objDOPoll.Option1 = TextBox1.Text.Trim().Replace("'", "''");
            }
            else
            {
                lblMessage.Text = "Please enter option 1.";
                lblMessage.CssClass = "RedErrormsg";
                return;
            }
            if (TextBox2.Text != "Option 2")
            {
                if (TextBox2.Text.Trim() == "")
                {
                    lblMessage.Text = "Please enter option 2.";
                    lblMessage.CssClass = "RedErrormsg";
                    return;
                }
                objDOPoll.Option2 = TextBox2.Text.Trim().Replace("'", "''");
            }
            else
            {
                lblMessage.Text = "Please enter option 2.";
                lblMessage.CssClass = "RedErrormsg";
                return;
            }
            if (TextBox3.Text != "Option 3")
            {
                objDOPoll.Option3 = TextBox3.Text.Trim().Replace("'", "''");
            }

            if (TextBox4.Text != "Option 4")
            {
                objDOPoll.Option4 = TextBox4.Text.Trim().Replace("'", "''");
            }

            if (TextBox5.Text != "Option 5")
            {
                objDOPoll.Option5 = TextBox5.Text.Trim().Replace("'", "''");
            }
            if (TextBox6.Text != "Option 6")
            {
                objDOPoll.Option6 = TextBox6.Text.Trim().Replace("'", "''");
            }

            if (TextBox7.Text != "Option 7")
            {
                objDOPoll.Option7 = TextBox7.Text.Trim().Replace("'", "''");
            }

            if (TextBox8.Text != "Option 8")
            {
                objDOPoll.Option8 = TextBox8.Text.Trim().Replace("'", "''");
            }

            if (TextBox9.Text != "Option 9")
            {
                objDOPoll.Option9 = TextBox9.Text.Trim().Replace("'", "''");
            }

            if (TextBox10.Text != "Option 10")
            {
                objDOPoll.Option10 = TextBox10.Text.Trim().Replace("'", "''");
            }

            objDOPoll.strIpAddress = ip;

            objDOPoll.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
            objDAPoll.AddEditDel_Scrl_UserPollTbl(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.Insert);

            if (ISAPIURLACCESSED == "1")
            {
                try
                {
                    string UserURL = "";

                    UserURL = APIURL + "createPoll.action?" +
                        "groupId=GRP" + ViewState["intGroupId"] +
                        "&pollId=POL" + objDOPoll.intPollOutId +
                        "&groupOwnerId=USR" + Session["GroupOwnerId"] +
                        "&insertDt=" + DateTime.Now +
                        "&content=" + objDOPoll.strDescription +
                        "&scope=" + objDOPoll.strVoteType +
                        "&title=" + objDOPoll.strPollName;


                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();

                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();

                    objAPILogDO.strURL = UserURL;
                    objAPILogDO.strAPIType = "Group Poll";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDO.strIPAddress = ip;
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
                catch
                {

                }
            }

            lblSuccess.Text = "Poll created successfully.";
            divSuccessPolls.Style.Add("display", "block");
            txtQuestion.Text = "";
            txtDescription.InnerText = "";
            txtExpireDate.Text = "";
            ddlTime.ClearSelection();
        }
        else
        {
            objDOPoll.intPollId = Convert.ToInt32(ViewState["PollId"]);
            objDOPoll.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
            objDOPoll.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objDOPoll.strPollName = txtQuestion.Text.Replace("'", "''");
            objDOPoll.strDescription = txtDescription.InnerHtml;
            if (txtDescription.InnerHtml == "Description")
            {
                objDOPoll.strDescription = "";
            }

            if (rdbSinglePattern.Checked == true)
                objDOPoll.strVotingPattern = "Single";
            else
                objDOPoll.strVotingPattern = "Multiple";

            if (rdVotingNeverEnds.Checked == true)
            {
                objDOPoll.strVotingEnds = "Never";
            }
            else if (rdVotingEnds.Checked == true)
            {
                if (Convert.ToDateTime(txtExpireDate.Text.Trim().Replace("'", "''")) < DateTime.Today)
                {
                    lblDateMessage.Style.Add("display", "block");
                    lblDateMessage.Text = "Expire date should be equal to or greater than today's date";
                    return;
                }

                objDOPoll.strVotingEnds = "";
                objDOPoll.dtPollExpireDate = Convert.ToDateTime(txtExpireDate.Text.Trim().Replace("'", "''"));
                objDOPoll.strPollExpireTime = ddlTime.SelectedValue;
            }

            if (rdVoteTypePublic.Checked == true)
                objDOPoll.strVoteType = "Public";
            else
                objDOPoll.strVoteType = "Group Member";

            if (TextBox1.Text == "Option 1" && TextBox2.Text == "Option 2" && TextBox3.Text == "Option 3" && TextBox4.Text == "Option 4" && TextBox5.Text == "Option 5" && TextBox6.Text == "Option 6" && TextBox7.Text == "Option 7" && TextBox8.Text == "Option 8" && TextBox9.Text == "Option 9" && TextBox10.Text == "Option 10")
            {
                lblMessage.Text = "Please enter atleast one option";
                lblMessage.CssClass = "RedErrormsg";
                return;
            }

            if (TextBox1.Text != "Option 1")
            {
                objDOPoll.Option1 = TextBox1.Text.Trim().Replace("'", "''");
            }
            if (TextBox2.Text != "Option 2")
            {
                objDOPoll.Option2 = TextBox2.Text.Trim().Replace("'", "''");
            }
            if (TextBox3.Text != "Option 3")
            {
                objDOPoll.Option3 = TextBox3.Text.Trim().Replace("'", "''");
            }

            if (TextBox4.Text != "Option 4")
            {
                objDOPoll.Option4 = TextBox4.Text.Trim().Replace("'", "''");
            }

            if (TextBox5.Text != "Option 5")
            {
                objDOPoll.Option5 = TextBox5.Text.Trim().Replace("'", "''");
            }
            if (TextBox6.Text != "Option 6")
            {
                objDOPoll.Option6 = TextBox6.Text.Trim().Replace("'", "''");
            }

            if (TextBox7.Text != "Option 7")
            {
                objDOPoll.Option7 = TextBox7.Text.Trim().Replace("'", "''");
            }

            if (TextBox8.Text != "Option 8")
            {
                objDOPoll.Option8 = TextBox8.Text.Trim().Replace("'", "''");
            }

            if (TextBox9.Text != "Option 9")
            {
                objDOPoll.Option9 = TextBox9.Text.Trim().Replace("'", "''");
            }

            if (TextBox10.Text != "Option 10")
            {
                objDOPoll.Option10 = TextBox10.Text.Trim().Replace("'", "''");
            }

            objDOPoll.strIpAddress = ip;

            objDOPoll.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
            objDAPoll.AddEditDel_Scrl_UserPollTbl(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.Update);

            if (ISAPIURLACCESSED == "1")
            {
                try
                {
                    string UserURL = "";

                    UserURL = APIURL + "createPoll.action?" +
                        "groupId=GRP" + ViewState["intGroupId"] +
                        "&pollId=POL" + objDOPoll.intPollOutId +
                        "&groupOwnerId=USR" + Session["GroupOwnerId"] +
                        "&insertDt=" + DateTime.Now +
                        "&content=" + objDOPoll.strDescription +
                        "&scope=" + objDOPoll.strVoteType +
                        "&title=" + objDOPoll.strPollName;


                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();

                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();

                    objAPILogDO.strURL = UserURL;
                    objAPILogDO.strAPIType = "Group Poll";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDO.strIPAddress = ip;
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);

                }
                catch
                {

                }
            }

            lblSuccess.Text = "Poll Updated successfully.";
            divSuccessPolls.Style.Add("display", "block");
            txtQuestion.Text = "";
            txtDescription.InnerText = "";
            txtExpireDate.Text = "";
            ddlTime.ClearSelection();
        }

        lnkSavePoll.Text = "Create Poll";
        ViewState["PollId"] = null;
        div1.Style.Add("display", "block");
        TextBox1.Text = "";
        div2.Style.Add("display", "block");
        TextBox2.Text = "";
        div3.Style.Add("display", "none");
        TextBox3.Text = "";
        div4.Style.Add("display", "none");
        TextBox4.Text = "";
        div5.Style.Add("display", "none");
        TextBox5.Text = "";
        div6.Style.Add("display", "none");
        TextBox6.Text = "";
        div7.Style.Add("display", "none");
        TextBox7.Text = "";
        div8.Style.Add("display", "none");
        TextBox8.Text = "";
        div9.Style.Add("display", "none");
        TextBox9.Text = "";
        div10.Style.Add("display", "none");
        TextBox10.Text = "";
        lblMessage.Text = "";
    }

    protected void lnkSuccessPoll_Click(object sender, EventArgs e)
    {
        Response.Redirect("Polls-Details.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkCancelPoll_Click(object sender, EventArgs e)
    {
        Response.Redirect("Polls-Details.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Polls-Details.aspx?GrpId=" + ViewState["intGroupId"]);
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

    #endregion

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

}