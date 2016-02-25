using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Net;
using System.IO;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;

public partial class group_event_main : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    DO_Scrl_GroupEventsTbl objDO = new DO_Scrl_GroupEventsTbl();
    DA_Scrl_GroupEventsTbl objDA = new DA_Scrl_GroupEventsTbl();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    DO_Scrl_UserGroupDetailTbl objDOG = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDAG = new DA_Scrl_UserGroupDetailTbl();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];

    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarEvent.SelectedDates.Clear();
        CalendarEvent.EnableViewState = false;
        if (!IsPostBack)
        {
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
            BindTitle();

        }
        if (!IsPostBack)
        {
            BindTitle();
            divCancelEvent.Style.Add("display", "none");
            divSuccessEvents.Style.Add("display", "none");
            dvPopup.Style.Add("display", "none");
            hdnEventPopupRefresh.Value = "";
        }
        BindTitle();
    }

    protected void AccessModulePermisssion()
    {
        objDOG.intUserId = Convert.ToInt32(ViewState["UserID"]);
        objDOG.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objgrp.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        DataSet dschk = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.ViewGrpAssignUser);
        DataTable dtRoleAP = objDAG.GetDataTable(objDOG, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpRoleRequestPermission);
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

    private void BindTitle()
    {
        dt.Clear();
        objDO.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        dt = objDA.GetDataTable(objDO, DA_Scrl_GroupEventsTbl.Scrl_GroupEventsTbl.AllRecords);
        if (dt.Rows.Count > 0)
        {
            RptCreatedEvent.DataSource = dt;
            RptCreatedEvent.DataBind();
            divmyevents.Style.Add("display", "block");
        }
        else
        {
            RptCreatedEvent.DataSource = "";
            RptCreatedEvent.DataBind();
            divmyevents.Style.Add("display", "none");
        }
    }

    protected void lnkSave_Click(object sender, EventArgs e)
    {
        divSuccessEvent.Style.Add("display", "none");
        lblDateMessage1.Text = "";
        lbleventColor.Text = "";
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        if (string.IsNullOrEmpty(Convert.ToString(hdnUpdateEventId.Value)))
        {
            if (Convert.ToDateTime(txtFromDate.Text) < DateTime.Today)
            {
                lblDateMessage1.Style.Add("display", "block");
                lblDateMessage1.Text = "From date should be equal to or greater than today's date";
                return;
            }
            else if (Convert.ToDateTime(txtToDate.Text) < Convert.ToDateTime(txtFromDate.Text))
            {
                lblDateMessage1.Style.Add("display", "block");
                lblDateMessage1.Text = "To date should be equal to or greater than From date";
                return;
            }
        }
        else
        {
            if (Convert.ToDateTime(txtToDate.Text) < DateTime.Today)
            {
                lblDateMessage1.Style.Add("display", "block");
                lblDateMessage1.Text = "To date should be equal to or greater than today's date";
                return;
            }
        }

        if (ddlPriorityType.SelectedItem.Value == "S")
        {
            lblDateMessage.Style.Add("display", "block");
            lblDateMessage.Text = "Select Event Priority..!";
            return;
        }

        if (txtColorCode.Text == "")
        {
            lbleventColor.Style.Add("display", "block");
            lbleventColor.Text = "Select Event Color..!";
            return;
        }

        objDO.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objDO.strTitle = txtTitle.Text.Trim().Replace("'", "''");
        if (txtDescription.InnerText.Trim() == "Description")
            objDO.strDescription = "";
        else
            objDO.strDescription = txtDescription.InnerText.Trim().Replace("'", "''");
        objDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objDO.strLocation = txtVenue.Text.Trim().Replace("'", "''"); ;
        objDO.strColor = txtColorCode.Text.Trim().Replace("'", "''");

        if (txtFromDate.Text.Trim() != "" && txtFromDate.Text.Trim() != null)
            objDO.dtFromDate = Convert.ToDateTime(txtFromDate.Text.Trim().Replace("'", "''"));
        else
            return;
        objDO.dtTodate = Convert.ToDateTime(txtToDate.Text.Trim().Replace("'", "''"));

        objDO.strPriority = ddlPriorityType.SelectedItem.Value;
        objDO.strIpAddress = ip;
        if (!string.IsNullOrEmpty(txtContactPerson.Text.Trim()) && txtContactPerson.Text.Trim() != "Contact person")
            objDO.strContactPerson = txtContactPerson.Text.Trim().Replace("'", "''");
        if (!string.IsNullOrEmpty(txtContactPerNumber.Text.Trim()) && txtContactPerNumber.Text.Trim() != "Contact person number")
            objDO.strContactNumber = txtContactPerNumber.Text.Trim().Replace("'", "''");

        if (!string.IsNullOrEmpty(Convert.ToString(hdnUpdateEventId.Value)))
        {
            objDO.intGrpEventId = Convert.ToInt32(hdnUpdateEventId.Value);
            objDO.intModifiedBy = Convert.ToInt32(ViewState["UserID"]);
            objDA.AddEditDel_Scrl_GroupEventsTbl(objDO, DA_Scrl_GroupEventsTbl.Scrl_GroupEventsTbl.Update);
        }
        else
        {
            objDA.AddEditDel_Scrl_GroupEventsTbl(objDO, DA_Scrl_GroupEventsTbl.Scrl_GroupEventsTbl.Insert);
        }
        lblDateMessage.Text = "";
        lbleventColor.Text = "";
        if (ISAPIURLACCESSED == "1")
        {
            try
            {
                string UserURL = "";

                UserURL = APIURL + "createEvent.action?" +
                    "groupId=GRP" + ViewState["intGroupId"] +
                    "&eventId=USR" + objDO.intNewEventID +
                    "&groupOwnerId=USR" + Session["GroupOwnerId"] +
                    "&insertDt=" + DateTime.Now +
                    "&content=" + objDO.strDescription +
                    "&scope=" + ddlPriorityType.SelectedItem.Text +
                    "&title=" + objDO.strTitle;

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                myRequest1.Method = "GET";
                WebResponse myResponse1 = myRequest1.GetResponse();

                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                String result = sr.ReadToEnd();

                objAPILogDO.strURL = UserURL;
                objAPILogDO.strAPIType = "Group Event";
                objAPILogDO.strResponse = result;
                objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objAPILogDO.strIPAddress = ip;
                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
            }
            catch
            {

            }
        }

        if (!string.IsNullOrEmpty(Convert.ToString(hdnUpdateEventId.Value)))
        {
            lblSuccess.Text = "Event updated successfully.";
        }

        BindTitle();
        divSuccessEvent.Style.Add("display", "block");
        divSuccessEvents.Style.Add("display", "none");
        ClearControl();
    }

    protected void ClearControl()
    {
        txtFromDate.Enabled = true;
        lnkSave.Text = "Create Event";
        txtTitle.Text = "";
        txtDescription.InnerText = "";
        txtVenue.Text = "";
        txtContactPerson.Text = "";
        txtContactPerNumber.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtColorCode.Text = "";
        TextBox1.Style.Add("background-color", "");
        ddlPriorityType.ClearSelection();
        hdnUpdateEventId.Value = "";
    }

    protected void RptCreatedEvent_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HiddenField hdnColor = (HiddenField)e.Item.FindControl("hdnColor");
        HtmlGenericControl dvcolor = (HtmlGenericControl)e.Item.FindControl("dvcolor");

        if (hdnColor.Value != "")
        {
            dvcolor.Style.Add("background", hdnColor.Value);
        }
    }

    protected void RptCreatedEvent_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        HiddenField hdnEventId = (HiddenField)e.Item.FindControl("hdnEventId");
        Label lblTitle = (Label)e.Item.FindControl("lblTitles");

        ViewState["hdnEventId"] = hdnEventId.Value;
        ViewState["lblTitle"] = lblTitle.Text;

        divSuccessEvent.Style.Add("display", "none");
        divSuccessEvents.Style.Add("display", "none");
        divCancelEvent.Style.Add("display", "none");
        objDO.intGrpEventId = Convert.ToInt32(hdnEventId.Value);
        hdnUpdateEventId.Value = Convert.ToString(hdnEventId.Value);
        if (e.CommandName == "Edit")
        {
            dt = objDA.GetDataTable(objDO, DA_Scrl_GroupEventsTbl.Scrl_GroupEventsTbl.SingleRecord);
            if (dt.Rows.Count > 0)
            {
                txtFromDate.Enabled = false;
                lnkSave.Text = "Update Event";
                txtTitle.Text = Convert.ToString(dt.Rows[0]["strTitle"]);
                txtDescription.InnerText = Convert.ToString(dt.Rows[0]["strDescription"]);
                txtVenue.Text = Convert.ToString(dt.Rows[0]["strLocation"]);
                txtContactPerson.Text = Convert.ToString(dt.Rows[0]["strContactPerson"]);
                txtContactPerNumber.Text = Convert.ToString(dt.Rows[0]["strContactNumber"]);
                txtFromDate.Text = Convert.ToString(dt.Rows[0]["dtFromDate"]);
                txtToDate.Text = Convert.ToString(dt.Rows[0]["dtTodate"]);
                ddlPriorityType.ClearSelection();
                ddlPriorityType.Items.FindByValue(dt.Rows[0]["strPriority"].ToString()).Selected = true;
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["strColor"])))
                {
                    TextBox1.Style.Add("background-color", Convert.ToString(dt.Rows[0]["strColor"]));
                    txtColorCode.Text = Convert.ToString(dt.Rows[0]["strColor"]);
                }
                else
                {
                    TextBox1.Style.Add("background-color", "");
                    txtColorCode.Text = "";
                }
                txtTitle.Focus();
                BindTitle();
            }
        }
        else
        if (e.CommandName == "Delete")
        {
            divDeletesucess.Style.Add("display", "block");
        }
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {

        objDO.intGrpEventId = Convert.ToInt32(hdnDeletePostQuestionID.Value);
        objDA.AddEditDel_Scrl_GroupEventsTbl(objDO, DA_Scrl_GroupEventsTbl.Scrl_GroupEventsTbl.Delete);

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(hdnDeletePostQuestionID.Value);
        objLog.strAction = "Group Event";
        objLog.strActionName = hdnstrQuestionDescription.Value;
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 8;   // Group Event
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
        ViewState["hdnEventId"] = null;
        ViewState["lblTitle"] = null;
        hdnUpdateEventId.Value = "";
        hdnDeletePostQuestionID.Value = "";
        hdnstrQuestionDescription.Value = "";
        BindTitle();
        divDeletesucess.Style.Add("display", "none");
    }

    protected void lnkDeleteCancel_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        divSuccessEvent.Style.Add("display", "none");
    }

    protected void lnksucessClose_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        divSuccessEvent.Style.Add("display", "none");
    }

    protected void lnkConnDisconn_Click(object sender, EventArgs e)
    {
        objDO.intGrpEventId = Convert.ToInt32(hdnUpdateEventId.Value);
        objDA.AddEditDel_Scrl_GroupEventsTbl(objDO, DA_Scrl_GroupEventsTbl.Scrl_GroupEventsTbl.Delete);

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(hdnUpdateEventId.Value);
        objLog.strAction = "Group Event";
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 8;   // Group Event
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);

        divCancelEvent.Style.Add("display", "none");
        BindTitle();
        divSuccessEvents.Style.Add("display", "block");
    }

    protected void CalendarEvent_DayRender(object sender, DayRenderEventArgs e)
    {
        try
        {
            if (!e.Day.IsOtherMonth)
            {
                e.Cell.Attributes.Add("class", "overout");
                foreach (DataRow dr in dt.Rows)
                {

                    DateTime dts = (DateTime)dr.Field<DateTime?>("dtFromDate");
                    DateTime todts = (DateTime)dr.Field<DateTime?>("dtTodate");

                    if (e.Day.Date == dts.Date)
                    {
                        LinkButton lb = new LinkButton();
                        LinkButton lbcolor = new LinkButton();

                        DataTable dttooltip = new DataTable();
                        objDO.dtFromDate = dts;
                        if (Convert.ToString(ViewState["CompareDate"]) != Convert.ToString(objDO.dtFromDate))
                        {
                            dttooltip = objDA.GetDataTable(objDO, DA_Scrl_GroupEventsTbl.Scrl_GroupEventsTbl.GetEventDetailsByDate);
                        }
                        if (dttooltip.Rows.Count > 0)
                        {
                            ViewState["CompareDate"] = objDO.dtFromDate;
                            lb.Text = Convert.ToString(dttooltip.Rows[0]["Maxcount"]);
                            lb.CssClass = "calendarCount";
                            e.Cell.Controls.Add(lb);
                            lbcolor.Style.Add("vertical-align", "text-bottom");
                            lbcolor.Text = "Events";
                            lbcolor.CssClass = "event1";
                            e.Cell.Controls.Add(lbcolor);
                            e.Cell.ToolTip = "Calendar";
                            ViewState["appEvent"] = objDO.dtFromDate;
                            e.Cell.Attributes.Add("onmouseover", "showpopevnt('" + dts.ToString() + "','" + dttooltip.Rows[0]["intGrpEventtId"] + "','" + ViewState["intGroupId"].ToString() + "')");
                        }

                    }
                }
            }
            else
            {
                e.Cell.Text = "";
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void LstEventPopup_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnstrColor = (HiddenField)e.Item.FindControl("hdnstrColor");
        HtmlControl dvcolor = (HtmlControl)e.Item.FindControl("dvcolor");
        dvcolor.Style.Add("background-color", hdnstrColor.Value);
    }

    [WebMethod]
    public static string getData(string frmDate, string EventId, string intGroupId)
    {
        string Data = string.Empty;
        group_event_main obj = new group_event_main();
        try
        {
            DO_Scrl_GroupEventsTbl objDOa = new DO_Scrl_GroupEventsTbl();
            DA_Scrl_GroupEventsTbl objDAa = new DA_Scrl_GroupEventsTbl();
            objDOa.intGroupId = Convert.ToInt32(intGroupId);
            objDOa.dtFromDate = Convert.ToDateTime(frmDate);
            DataTable dttooltip1 = objDAa.GetDataTable(objDOa, DA_Scrl_GroupEventsTbl.Scrl_GroupEventsTbl.GetEventDetailsByDates);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dttooltip1.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dttooltip1.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            return Data;
        }
    }

    private void LosdBusinessListview(DataTable LocalityName)
    {
    }

    protected void CalendarEvent_SelectionChanged(object sender, EventArgs e)
    {
        objDO.dtFromDate = CalendarEvent.SelectedDate;
        objDO.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        dt = objDA.GetDataTable(objDO, DA_Scrl_GroupEventsTbl.Scrl_GroupEventsTbl.GetEventDetailsByDate);
        if (dt.Rows.Count > 0)
        {
            CalendarEvent.SelectedDates.Clear();
            if (dt.Rows.Count > 1)
            {
                dvPopup.Style.Add("display", "none");
                ShowTitle.Visible = true;
                ShowDetails.Visible = false;
                lstViewEvents.DataSource = dt;
                lstViewEvents.DataBind();
            }
            else
            {
                lstViewEvents.DataSource = "";
                lstViewEvents.DataBind();
                dvPopup.Style.Add("display", "none");
                ShowTitle.Visible = false;
                ShowDetails.Visible = true;
                tdPopupColor.Style.Add("background", Convert.ToString(dt.Rows[0]["strColor"]));
                lblTitle.Text = Convert.ToString(dt.Rows[0]["strTitle"]);
                lblDescription.Text = Convert.ToString(dt.Rows[0]["strDescription"]);
                lblDate.Text = Convert.ToString(dt.Rows[0]["dtFromDate"]);
                lblVenue.Text = Convert.ToString(dt.Rows[0]["strLocation"]);
                lblContactPerson.Text = Convert.ToString(dt.Rows[0]["strContactPerson"]);
                lblContactNumber.Text = Convert.ToString(dt.Rows[0]["strContactNumber"]);
            }

        }
        else
            dvPopup.Style.Add("display", "none");
        BindTitle();
        divSuccessEvent.Style.Add("display", "none");
        string ID = TextBox1.ClientID;
        hdnEventPopupRefresh.Value = ID;
    }

    protected void lnkPopupOK_Click(object sender, EventArgs e)
    {
        dvPopup.Style.Add("display", "none");
        Response.Redirect("group-event-main.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lstViewEvents_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnEventId = (HiddenField)e.Item.FindControl("hdnEventId");
        if (e.CommandName == "EventDetails")
        {
            objDO.intGrpEventId = Convert.ToInt32(hdnEventId.Value);
            dt.Clear();
            dt = objDA.GetDataTable(objDO, DA_Scrl_GroupEventsTbl.Scrl_GroupEventsTbl.SingleRecord);
            if (dt.Rows.Count > 0)
            {
                dvPopup.Style.Add("display", "none");
                ShowDetails.Visible = true;
                tdPopupColor.Style.Add("background", Convert.ToString(dt.Rows[0]["strColor"]));
                lblTitle.Text = Convert.ToString(dt.Rows[0]["strTitle"]);
                lblDescription.Text = Convert.ToString(dt.Rows[0]["strDescription"]);
                lblDate.Text = Convert.ToString(dt.Rows[0]["dtFromDate"]) + " <strong>To</strong> " + Convert.ToString(dt.Rows[0]["dtTodate"]);
                lblVenue.Text = Convert.ToString(dt.Rows[0]["strLocation"]);
                lblContactPerson.Text = Convert.ToString(dt.Rows[0]["strContactPerson"]);
                lblContactNumber.Text = Convert.ToString(dt.Rows[0]["strContactNumber"]);
            }
        }
        BindTitle();
    }

    protected void lstViewEvents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdntxtColor = (HiddenField)e.Item.FindControl("hdntxtColor");
        TextBox txtColor = (TextBox)e.Item.FindControl("txttdPopupColor");
        txtColor.Style.Add("background", Convert.ToString(hdntxtColor.Value));
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        dvPopup.Style.Add("display", "none");
        Response.Redirect("group-event-main.aspx?GrpId=" + ViewState["intGroupId"]);
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

    protected void lnkJobTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("jobs.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkEventMemberTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("groups-members.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkEventTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("group-event-main.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    #endregion

    protected void DatesValidator_Validate(object source, ServerValidateEventArgs args)
    {
        DateTime startDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime endDate = Convert.ToDateTime(txtToDate.Text);

        if (endDate < startDate)
        {
            args.IsValid = false;
        }
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
    }

}