using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Net;
using System.IO;

public partial class Inbox : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DO_Scrl_UserRecommendation objRecmndDO = new DO_Scrl_UserRecommendation();
    DA_Scrl_UserRecommendation objRecmndDA = new DA_Scrl_UserRecommendation();

    DO_WallMessage WallMessageDO = new DO_WallMessage();
    DA_WallMessage WallMessageDA = new DA_WallMessage();
    DataSet ds = new DataSet();
    string maxcount = "";

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];

    protected void Page_Load(object sender, EventArgs e)
    {
        dvPopup.Style.Add("display", "none");
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "All Messages";

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "50";
            GetMessageNotification();
            GetTotalInbox();
            lnkOutBox.Style.Add("font-weight", "normal");
            lnkInbox.Style.Add("font-weight", "bold");
        }
    }

    protected void GetMessageNotification()
    {
        objRecmndDO.striInvitedUserId = Convert.ToString(ViewState["UserID"]);
        dt = objRecmndDA.GetDataTable(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.GetMessNotification);
        if (dt.Rows.Count > 0)
        {
            ViewState["InBoxCount"] = Convert.ToString(dt.Rows[0]["IsRead"]);
            lnkInbox.Text = "Inbox(" + ViewState["InBoxCount"] + ")";
        }

        //objRecmndDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        //DataTable Senddt = objRecmndDA.GetDataTable(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.GetOutBoxCount);
        //if (Senddt.Rows.Count > 0)
        //{
        //    ViewState["InSendCount"] = Convert.ToString(Senddt.Rows[0]["Maxcount"]);
        //    lnkOutBox.Text = "Sent(" + ViewState["InSendCount"] + ")";
        //}
        //else
        //{
        //    lnkOutBox.Text = "Sent("+0 +")";
        //    ViewState["InSendCount"] = 0;
        //}


    }

    protected void GetTotalInbox()
    {
        lnkOutBox.Style.Add("font-weight", "normal");
        lnkInbox.Style.Add("font-weight", "bold");
        objRecmndDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objRecmndDO.striInvitedUserId = Convert.ToString(ViewState["UserID"]);
        objRecmndDO.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objRecmndDO.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        dt = objRecmndDA.GetDataTable(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.GetInboxDetails);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows.Count < 20)
            {
                //dvInBox.Style.Add("height", "550px");
            }
            else
            {
                dvInBox.Style.Add("height", "auto");
            }

            lblTotalInbox.Text = Convert.ToString(ViewState["InBoxCount"]);//Convert.ToString(dt.Rows[0]["Maxcount"]);
            lstInbox.DataSource = dt;
            lstInbox.DataBind();
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));
        }
        else
        {
            lstInbox.DataSource = null;
            lstInbox.DataBind();
            ViewState["InBoxCount"] = "0";
            dvInBox.Style.Add("height", "550px");
            lblTotalInbox.Text = "0";
            dvPage.Visible = false;
        }
    }

    protected void lnkInbox_Click(object sender, EventArgs e)
    {
        divSuccessMess.Visible = false;
        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "50";
        aReplyMess.Visible = true;
        ViewState["Sent"] = null;
        ViewState["IsReadSent"] = null;
        GetTotalInbox();
    }

    protected void lnkOutBox_Click(object sender, EventArgs e)
    {
        divSuccessMess.Visible = false;
        //dvPage.Visible = false;
        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "50";
        aReplyMess.Visible = false;
        ViewState["Sent"] = "SentClick";
        ViewState["IsReadSent"] = "Sent";
        GetTotalOutBox(); 

    }

    protected void GetTotalOutBox()
    {
        objRecmndDO.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objRecmndDO.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        objRecmndDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);//Convert.ToInt32(hdnAddedBy.Value);
        dt = objRecmndDA.GetDataTable(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.GetOutBoxDetails);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows.Count < 20)
            {
                //dvInBox.Style.Add("height", "550px");
            }
            else
            {
                dvInBox.Style.Add("height", "auto");
            }
            lnkOutBox.Style.Add("font-weight", "bold");
            lnkInbox.Style.Add("font-weight", "normal");
            lstInbox.DataSource = dt;
            lstInbox.DataBind();
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));
        }
        else
        {
            //lblTotalSent.Text = "0";
            lstInbox.DataSource = null;
            lstInbox.DataBind();
           // lblSendBox.Text = "Sent(0)";
            dvPage.Visible = false;
        }
    }
   
    protected void lstInbox_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

        LinkButton lnkinbox = (LinkButton)e.Item.FindControl("lnkinbox");
        HiddenField hdnMaxCount = (HiddenField)e.Item.FindControl("hdnMaxCount");
        LinkButton lnkOutbox = (LinkButton)e.Item.FindControl("lnkOutbox");
        HiddenField hdnAddedBy = (HiddenField)e.Item.FindControl("hdnAddedBy");
        LinkButton lnksubject = (LinkButton)e.Item.FindControl("lnksubject");
        HiddenField hdnIsRead = (HiddenField)e.Item.FindControl("hdnIsRead");
        LinkButton lblSenderName = (LinkButton)e.Item.FindControl("lblSenderName");
        LinkButton lblDate = (LinkButton)e.Item.FindControl("lblDate");
       
        HtmlTableRow lblinboxFrom = (HtmlTableRow)e.Item.FindControl("lblinboxFrom");
        HtmlTableRow lblinboxTo = (HtmlTableRow)e.Item.FindControl("lblinboxTo");
        HtmlHead lblinboxFrom1 = (HtmlHead)e.Item.FindControl("lblinboxFrom");
        HtmlTableCell lblinboxFrom2 = (HtmlTableCell)e.Item.FindControl("lblinboxFrom");
        Label lblinboxFrom3 = (Label)e.Item.FindControl("lblinboxFrom");
       HtmlTableCell thFrom = (HtmlTableCell)lstInbox.FindControl("thFrom");
        HtmlTableCell thTo = (HtmlTableCell)lstInbox.FindControl("thTo");
        HiddenField hdnIsReadSent = (HiddenField)e.Item.FindControl("hdnIsReadSent");
        HiddenField hdnSubjectmsg = (HiddenField)e.Item.FindControl("hdnSubjectmsg");
        HiddenField hdnstrTotalGrpMemberID = (HiddenField)e.Item.FindControl("hdnstrTotalGrpMemberID");

        int IsRead = 0;
        if (hdnIsRead.Value != "")
        {
            IsRead = Convert.ToInt32(hdnIsRead.Value);
        }
        if (hdnIsRead.Value.ToString().Trim() != "" && hdnIsRead.Value.ToString().Trim() != null && hdnIsRead.Value.ToString().Trim() != "0")
        {

            lnksubject.ForeColor = System.Drawing.Color.Gray;
            lblSenderName.ForeColor = System.Drawing.Color.Gray;
            lblDate.ForeColor = System.Drawing.Color.Gray;
        }
        else
        {
            lnksubject.ForeColor = System.Drawing.Color.Black;
            lblSenderName.ForeColor = System.Drawing.Color.Black;
            lblDate.ForeColor = System.Drawing.Color.Black;
        }

        if (ViewState["IsReadSent"] != null)
        {
            if (hdnIsReadSent.Value.ToString().Trim() != "" && hdnIsReadSent.Value.ToString().Trim() != null && hdnIsReadSent.Value.ToString().Trim() != "0")
            {

                lnksubject.ForeColor = System.Drawing.Color.Gray;
                lblSenderName.ForeColor = System.Drawing.Color.Gray;
                lblDate.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                lnksubject.ForeColor = System.Drawing.Color.Black;
                lblSenderName.ForeColor = System.Drawing.Color.Black;
                lblDate.ForeColor = System.Drawing.Color.Black;
            }
        }

        if (string.IsNullOrEmpty(maxcount))
        {
            maxcount = "1";
        }
        if (hdnstrTotalGrpMemberID.Value != "")
        {
            lnksubject.Text = hdnstrTotalGrpMemberID.Value + " : " + hdnSubjectmsg.Value;
        }
        
        if (Convert.ToString(ViewState["Sent"]) == "SentClick")
        {
            thTo.Style.Add("display", "block");
            thFrom.Visible = false;
        }
        else
        {
            thTo.Style.Add("display", "none");
            thFrom.Visible = true;
            lnksubject.Enabled = true;
            lblSenderName.Enabled = true;
            lblDate.Enabled = true;

        }

    }

    protected void lstInbox_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnMessId = (HiddenField)e.Item.FindControl("hdnMessId");
        HiddenField hdnGrpId = (HiddenField)e.Item.FindControl("hdnGrpId");
        HtmlTableCell thFrom = (HtmlTableCell)lstInbox.FindControl("thFrom");
        HtmlTableCell thTo = (HtmlTableCell)lstInbox.FindControl("thTo");
        if (e.CommandName == "GetMessageSetails")
        {
            if (ViewState["IsReadSent"] == null)
            {
                divSuccessMess.Style.Add("display", "none");
                dvPopup.Style.Add("display", "block");
                objRecmndDO.intMessageId = Convert.ToInt32(hdnMessId.Value);//Convert.ToInt32(hdnAddedBy.Value);
                ViewState["MessageId"] = objRecmndDO.intMessageId;
                objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.UpdateIsRead);
                dt = objRecmndDA.GetDataTable(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.GetMessageDetaisByMessId);
                if (dt.Rows.Count > 0)
                {
                    ViewState["GrpId"] = hdnGrpId.Value;
                    lblTo.Text = Convert.ToString(dt.Rows[0]["Name"]) + "  [" + Convert.ToString(dt.Rows[0]["vchrUserName"]) + "]";
                    lblFrom.Text = Convert.ToString(dt.Rows[0]["Name"]) + "  [" + Convert.ToString(dt.Rows[0]["vchrUserName"]) + "]";
                    lblMessesage.Text = Convert.ToString(dt.Rows[0]["StrRecommendation"]);
                    GetTotalInbox();
                    GetMessageNotification();
                   
                }
                MessTo.Style.Add("display", "none");
                MessFrom.Style.Add("display", "block");
            }
            else
            {
                divSuccessMess.Style.Add("display", "none");
                dvPopup.Style.Add("display", "block");
                objRecmndDO.intMessageId = Convert.ToInt32(hdnMessId.Value);//Convert.ToInt32(hdnAddedBy.Value);
                ViewState["MessageId"] = objRecmndDO.intMessageId;
                objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.UpdateIsReadSent);
                dt = objRecmndDA.GetDataTable(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.GetSentMessageDetaisByMessId);
                if (dt.Rows.Count > 0)
                {
                    ViewState["GrpId"] = hdnGrpId.Value;
                    lblTo.Text = Convert.ToString(dt.Rows[0]["Name"]) + "  [" + Convert.ToString(dt.Rows[0]["vchrUserName"]) + "]";
                    lblFrom.Text = Convert.ToString(dt.Rows[0]["Name"]) + "  [" + Convert.ToString(dt.Rows[0]["vchrUserName"]) + "]";
                    lblMessesage.Text = Convert.ToString(dt.Rows[0]["StrRecommendation"]);
                    GetTotalOutBox();
                    GetMessageNotification();
                    thTo.Style.Add("display", "block");
                    thFrom.Visible = false;
                    MessTo.Style.Add("display", "block");
                    MessFrom.Style.Add("display", "none");
                    
                }
            }

        }
    }

    protected void lnkPopupOK_Click(object sender, EventArgs e)
    {
        objRecmndDO.intMessageId = Convert.ToInt32(ViewState["MessageId"]);
        dt = objRecmndDA.GetDataTable(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.GetMessageDetaisByMessId);
        WallMessageDO.striInvitedUserId = Convert.ToString(dt.Rows[0]["intAddedBy"]);
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            WallMessageDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
        }
        else
        {
            return;
        }

        if (Convert.ToString(dt.Rows[0]["intGroupId"]) != "")
        {
            WallMessageDO.intGroupId = Convert.ToInt32(dt.Rows[0]["intGroupId"]);
        }
       
        WallMessageDO.StrRecommendation = txtMessage.InnerText.Replace("'", "''");
         WallMessageDO.strSubject = txtSubject.Text.Trim().Replace("'", "''");
        WallMessageDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
        WallMessageDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (WallMessageDO.strIpAddress == null)
            WallMessageDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
        WallMessageDA.Scrl_AddEditDelWallMessage(WallMessageDO, DA_WallMessage.WallMessage.Add);

        try
        {
            string UserURL = "";
            if (ISAPIURLACCESSED == "1")
            {
                UserURL = APIURL + "massageToUser.action?" +
                           "messageByUserId=USR" + WallMessageDO.intRegistrationId +
                           "&messageToUserId=USR" + WallMessageDO.striInvitedUserId +
                           "&message=" + WallMessageDO.StrRecommendation;

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                myRequest1.Method = "GET";
                if (ISAPIResponse != "0")
                {
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();
                    objAPILogDO.strURL = UserURL;
                    objAPILogDO.strAPIType = "Reply User Message";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDO.strIPAddress = WallMessageDO.strIpAddress;
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
            }
        }
        catch { }

        txtMessage.InnerText = "";
        divSuccessMess.Style.Add("display", "block");
        lblSuccess.Text = "Message sent successfully.";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //dvPopup.Style.Add("display", "none");
    }

    #region Paging For All

    protected void BindRptPager(Int64 PageSize, Int64 CurrentPage, Int64 MaxCount)
    {


        if (MaxCount > 0 && CurrentPage > 0 && PageSize > 0)
        {
            Int64 DisplayPage = 10;

            Int64 totalPage = (MaxCount / PageSize) + ((MaxCount % PageSize) == 0 ? 0 : 1);

            Int64 StartPage = (((CurrentPage / DisplayPage) - ((CurrentPage % DisplayPage) == 0 ? 1 : 0)) * DisplayPage) + 1;    // ((40 /10) - 1) * 10

            Int64 EndPage = ((CurrentPage / DisplayPage) + ((CurrentPage % DisplayPage) == 0 ? 0 : 1)) * DisplayPage;

            hdnNextPage.Value = (CurrentPage + 1).ToString();
            hdnPreviousPage.Value = (CurrentPage - 1).ToString();
            hdnEndPage.Value = EndPage.ToString();

            if (totalPage < EndPage)
            {
                EndPage = totalPage;
                hdnEndPage.Value = EndPage.ToString();
            }

            if (totalPage == 1)
            {
                lnkPrevious.Visible = false;
                lnkNext.Visible = false;

                img2.Visible = false;
                lnkNextshow.Visible = false;

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
                    hdnLastPage.Value = i.ToString();
                }

                rptDvPage.DataSource = dtPage;
                rptDvPage.DataBind();


                if (CurrentPage > DisplayPage)
                {
                    lnkPrevious.Visible = true;
                    hdnPreviousPage.Value = (StartPage - 1).ToString();
                }
                else
                {
                    lnkPrevious.Visible = true;
                }
                if (totalPage > EndPage)
                {
                    lnkNext.Visible = true;
                    hdnNextPage.Value = (EndPage + 1).ToString();
                    hdnLastPage.Value = totalPage.ToString();
                }
                else
                {
                    lnkNext.Visible = true;

                }
            }
        }
    }

    protected void lnkNext_Click(object sender, EventArgs e)
    {
        imgPaging.Style.Add("opacity", "1.2");
        hdnCurrentPage.Value = hdnNextPage.Value;
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            if (ViewState["Sent"] == null)
            {
                GetTotalInbox();
            }
            else
            {
                GetTotalOutBox();
            }
        }
        else
        {
            if (ViewState["Sent"] == null)
            {
                GetTotalInbox();
            }
            else
            {
                GetTotalOutBox();
            }
        }
    }

    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = "1";
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            if (ViewState["Sent"] == null)
            {
                GetTotalInbox();
            }
            else
            {
                GetTotalOutBox();
            }
        }
        else
        {
            if (ViewState["Sent"] == null)
            {
                GetTotalInbox();
            }
            else
            {
                GetTotalOutBox();
            }
        }
    }

    protected void lnkLast_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = hdnLastPage.Value;
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            GetTotalInbox();
        }
        else
        {
            GetTotalInbox();
        }
    }

    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        if (hdnPreviousPage.Value != "0")
        {
            hdnCurrentPage.Value = hdnPreviousPage.Value;
            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                if (ViewState["Sent"] == null)
                {
                    GetTotalInbox();
                }
                else
                {
                    GetTotalOutBox();
                }
            }
            else
            {
                if (ViewState["Sent"] == null)
                {
                    GetTotalInbox();
                }
                else
                {
                    GetTotalOutBox();
                }
            }
        }
    }

    protected void rptDvPage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "PageLink")
        {
            LinkButton lnkPageLink = (LinkButton)e.Item.FindControl("lnkPageLink");
            if (lnkPageLink != null)
            {
                hdnCurrentPage.Value = lnkPageLink.Text;
                lnkPageLink.Style.Add("color", "#141414 !important");
                lnkPageLink.Style.Add("text-decoration", "none !important");

                if (lnkPageLink.Text == "")
                {
                    hdnCurrentPage.Value = "1";
                }
                if (lnkPageLink.Text != "1")
                {
                    imgPaging.Style.Add("opacity", "1.2");
                }
                else
                {
                    imgPaging.Style.Add("opacity", "0.2");
                }

                if (Convert.ToString(ViewState["ViewAll"]) == "1")
                {
                    if (ViewState["Sent"] == null)
                    {
                        GetTotalInbox();
                    }
                    else
                    {
                        GetTotalOutBox();
                    }
                }
                else
                {
                    if (ViewState["Sent"] == null)
                    {
                        GetTotalInbox();
                    }
                    else
                    {
                        GetTotalOutBox();
                    }
                }
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
                    if (ViewState["lnkPageLink"] != null)
                    {
                        if (lnkPageLink.Text == "1")
                        {
                            ViewState["lnkPageLink"] = null;
                        }
                    }
                }
                else
                {
                    lnkPageLink.Enabled = true;
                }
                if (hdnCurrentPage.Value == "1")
                {
                    ViewState["lnkPageLink"] = "PageCount";
                }
            }
        }
    }

    #endregion

}