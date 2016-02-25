using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Polls_Details : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    DO_Scrl_UserPollTbl objDOPoll = new DO_Scrl_UserPollTbl();
    DA_Scrl_UserPollTbl objDAPoll = new DA_Scrl_UserPollTbl();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divDeletesucess.Style.Add("display", "none");
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
            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "6";
            BindAllPolls();
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

    protected void BindAllPolls()
    {
        objDOPoll.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objDOPoll.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objDOPoll.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        if (txtsearch.Text != "Search title")
            objDOPoll.strSearch = txtsearch.Text.Trim().Replace("'", "''");

        dt = objDAPoll.GetDataTable(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.GetPoll);

        if (dt.Rows.Count > 0)
        {
            divheight.Style.Add("height", "auto");
            ViewState["MaxCount"] = dt.Rows[0]["Maxcount"];
            hdnMaxcount.Value = dt.Rows[0]["Maxcount"].ToString();
            if (Convert.ToInt32(ViewState["MaxCount"]) <= Convert.ToInt32(hdnTotalItem.Value))
            {
                if (Convert.ToInt32(hdnTotalItem.Value) <= 10)
                {
                    pLoadMore.Style.Add("display", "none");
                    lblNoMoreRslt.Visible = false;
                }
                else
                {
                    pLoadMore.Visible = false;
                    lblNoMoreRslt.Visible = true;
                }
            }
            else
            {
                pLoadMore.Visible = true;
                lblNoMoreRslt.Visible = false;

            }

            dvPage.Visible = true;
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));           
            lstPollsDetails.DataSource = dt;
            lstPollsDetails.DataBind();

        }
        else
        {
            lstPollsDetails.DataSource = null;
            lstPollsDetails.DataBind();
            dvPage.Visible = false;
            pLoadMore.Style.Add("display", "none");
            lblNoMoreRslt.Visible = false;
            pLoadMore.Visible = false;
            divheight.Style.Add("height","400px");
        }
    }

    #region AssignRole

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
                    case "Members": DivMemberTab.Style.Add("display", "block");
                        break;
                }
            }

        }

    }

    #endregion

    protected void lstPollsDetails_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lblVoters = (Label)e.Item.FindControl("lblVoters");
        Label lblExpiredt = (Label)e.Item.FindControl("lblExpiredt");
        HiddenField hdnstrVoteType = (HiddenField)e.Item.FindControl("hdnstrVoteType");
        HtmlControl Pollrow = (HtmlControl)e.Item.FindControl("Pollrow");
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        LinkButton lnkDeleteEvent = (LinkButton)e.Item.FindControl("lnkDelete");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
        HtmlControl trDescription = (HtmlControl)e.Item.FindControl("trDescription");
        Label lblDescription = (Label)e.Item.FindControl("lblDescription");
        HtmlControl pdiv = (HtmlControl)e.Item.FindControl("pdiv");

        if (lblDescription.Text == "")
        {
            trDescription.Visible = false;
            pdiv.Style.Add("margin-top", "10px");
        }

        if (hdnstrVoteType.Value == "Group Member")
        {
            objDOPoll.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objDOPoll.intRegistrationId =Convert.ToInt32(hdnRegistrationId.Value);
            dt = objDAPoll.GetDataTable(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.GetUserMember);
            if (dt.Rows[0][0].ToString() == "0")
            {
                Pollrow.Visible = false;
            }
        }

        // lnkEdit lnkDelete
        string ff = hdnRegistrationId.Value;
        string UserID = Convert.ToString(ViewState["UserID"]);
        if (hdnRegistrationId.Value == UserID)
        {
            lnkDeleteEvent.Visible = true;
            lnkEdit.Visible = true;
        }
        else
        {
            lnkDeleteEvent.Visible = false;
            lnkEdit.Visible = false;
        }

        if(string.IsNullOrEmpty(lblExpiredt.Text))
            lblExpiredt.Text="Never";

        if (String.IsNullOrEmpty(lblVoters.Text))
            lblVoters.Text = "0";
    }

    protected void lstPollsDetails_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        LinkButton lnkPollName = (LinkButton)e.Item.FindControl("lnkPollName");
        HiddenField hdnPollId = (HiddenField)e.Item.FindControl("hdnPollId");
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        ViewState["hdnPollId"] = hdnPollId.Value;
        ViewState["lnkPollName"] = lnkPollName.Text;

        if (e.CommandName == "Get Poll")
        {
            Response.Redirect("Polls-VoteComment.aspx?GrpId=" + ViewState["intGroupId"] + "&PollId=" + hdnPollId.Value);
        }

        if (e.CommandName == "Details")
        {
            Response.Redirect("Home.aspx?RegId=" + hdnRegistrationId.Value);
        }

        else if (e.CommandName == "DeletePoll")
        {
            divDeletesucess.Style.Add("display","block");
        }
        else if (e.CommandName == "Edit Poll")
        {
            Response.Redirect("create-poll.aspx?GrpId=" + ViewState["intGroupId"] + "&PollId=" + hdnPollId.Value);
        }

    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {

        objDOPoll.intPollId = Convert.ToInt32(ViewState["hdnPollId"]);
        objDAPoll.AddEditDel_Scrl_UserPollTbl(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.Delete);

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(ViewState["hdnPollId"]);
        objLog.strAction = "Group Poll";
        objLog.strActionName =Convert.ToString(ViewState["lnkPollName"]);
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 7;   // Group Poll
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
        divCancelPoll.Style.Add("display", "none");
        divDeletesucess.Style.Add("display", "none");
        BindAllPolls();

    }

    protected void lnkConnDisconn_Click(object sender, EventArgs e)
    {
        objDOPoll.intPollId = Convert.ToInt32(ViewState["hdnPollId"]);
        objDAPoll.AddEditDel_Scrl_UserPollTbl(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.Delete);

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(ViewState["hdnPollId"]);
        objLog.strAction = "Group Poll";
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 7;   // Group Poll
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);

        divCancelPoll.Style.Add("display", "none");
        BindAllPolls();
        divSuccessPolls.Style.Add("display", "block");
    }

    protected void chkRecent_CheckedChanged(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        divCancelPoll.Style.Add("display", "none");
        divSuccessPolls.Style.Add("display", "none");
        LinkButton1.Style.Add("color", "#00B7BD !important");
        LinkButton2.Style.Add("color", "#9c9c9c !important");
        BindRecentPolls();
    }

    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        divCancelPoll.Style.Add("display", "none");
        divSuccessPolls.Style.Add("display", "none");
        LinkButton2.Style.Add("color", "#00B7BD !important");
        LinkButton1.Style.Add("color", "#9c9c9c !important");
       
        BindActivePolls();
    }

    protected void lnkDeleteCancel_Click(object sender, EventArgs e)
    {
        ViewState["hdnPollId"] = null;
        ViewState["lnkPollName"] = null;
        divDeletesucess.Style.Add("display", "none");
    }

    protected void BindRecentPolls()
    {
        objDOPoll.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objDOPoll.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objDOPoll.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        if (txtsearch.Text != "Search title")
            objDOPoll.strSearch = txtsearch.Text.Trim().Replace("'", "''");

        dt.Clear();
        dt = objDAPoll.GetDataTable(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.GetRecentPolls);

        if (dt.Rows.Count > 0)
        {
            ViewState["MaxCount"] = dt.Rows[0]["Maxcount"];
            if (Convert.ToInt32(ViewState["MaxCount"]) <= Convert.ToInt32(hdnTotalItem.Value))
            {
                pLoadMore.Visible = false;
                lblNoMoreRslt.Visible = true;
            }
            else
            {
                pLoadMore.Visible = true;
                lblNoMoreRslt.Visible = false;
            }
            dvPage.Visible = true;
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));
            lstPollsDetails.DataSource = dt;
            lstPollsDetails.DataBind();
        }
        else
        {
            lstPollsDetails.DataSource = null;
            lstPollsDetails.DataBind();
            dvPage.Visible = false;
            pLoadMore.Visible = false;
            lblNoMoreRslt.Visible = true;
        }
    }

    protected void BindActivePolls()
    {
       
        objDOPoll.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objDOPoll.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objDOPoll.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        if (txtsearch.Text != "Search title")
            objDOPoll.strSearch = txtsearch.Text.Trim().Replace("'", "''");

        dt.Clear();
        dt = objDAPoll.GetDataTable(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.GetActivePolls);

        if (dt.Rows.Count > 0)
        {
            ViewState["MaxCount"] = dt.Rows[0]["Maxcount"];
            if (Convert.ToInt32(ViewState["MaxCount"]) <= Convert.ToInt32(hdnTotalItem.Value))
            {
                pLoadMore.Visible = false;
                lblNoMoreRslt.Visible = true;
            }
            else
            {
                pLoadMore.Visible = true;
                lblNoMoreRslt.Visible = false;
            }
            dvPage.Visible = true;
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));
            lstPollsDetails.DataSource = dt;
            lstPollsDetails.DataBind();
        }
        else
        {
            lstPollsDetails.DataSource = null;
            lstPollsDetails.DataBind();
            dvPage.Visible = false;
            pLoadMore.Visible = false;
            lblNoMoreRslt.Visible = true;
        }
    }

    protected void lnkcreatePoll_Click(object sender, EventArgs e)
    {
        Response.Redirect("create-poll.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        divCancelPoll.Style.Add("display", "none");
        divSuccessPolls.Style.Add("display", "none");
        BindAllPolls();
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

    protected void lnkPollTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("Polls-Details.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    #endregion

    #region Paging For All

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
        divDeletesucess.Style.Add("display", "none");
        hdnCurrentPage.Value = hdnNextPage.Value;
        BindAllPolls();
    }

    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        hdnCurrentPage.Value = "1";
        BindAllPolls();
    }

    protected void lnkLast_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = hdnLastPage.Value;
        BindAllPolls();
    }

    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = hdnPreviousPage.Value;
        BindAllPolls();
    }

    protected void rptDvPage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        if (e.CommandName == "PageLink")
        {
            LinkButton lnkPageLink = (LinkButton)e.Item.FindControl("lnkPageLink");
            if (lnkPageLink != null)
            {
                hdnCurrentPage.Value = lnkPageLink.Text;
                lnkPageLink.Enabled = false;
                BindAllPolls();
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
                    lnkPageLink.Enabled = false;
                }

                else
                {
                    lnkPageLink.Enabled = true;
                }
            }
        }
    }

    #endregion

    protected void imgLoadMore_OnClick(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");

        int NextPage = 2, count = 0;

        for (int i = 0; i <= Convert.ToInt32(hdnTotalItem.Value); i++)
        {
            NextPage = NextPage + 1;
        }
        if (NextPage <= Convert.ToInt32(ViewState["MaxCount"]) || (Convert.ToInt32(ViewState["nextValue"]) < Convert.ToInt32(ViewState["MaxCount"])))
        {

            hdnTotalItem.Value = Convert.ToString(NextPage);
            ViewState["nextValue"] = hdnTotalItem.Value;
            BindAllPolls();
            count = NextPage - 3;
            String ID = "ctl00_ContentPlaceHolder1_lstPollsDetails_ctrl" + count + "_lblPostlink";
            hdnLoader.Value = ID;

            if ((Convert.ToInt32(ViewState["nextValue"]) > Convert.ToInt32(ViewState["MaxCount"])))
            {
                lblNoMoreRslt.Visible = true;
                pLoadMore.Visible = false;
            }
        }
        else
        {
            count = NextPage - 4;
            String ID = "ctl00_ContentPlaceHolder1_lstPollsDetails_ctrl" + count + "_lblPostlink";
            hdnLoader.Value = ID;
            pLoadMore.Visible = false;
            lblNoMoreRslt.Visible = true;

        }
    }


}