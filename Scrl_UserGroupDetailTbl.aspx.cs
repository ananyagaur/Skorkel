using System;
using System.Web.UI.WebControls;
using System.Data;
using DA_SKORKEL;
using System.Net;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Configuration;


public partial class Scrl_UserGroupDetailTbl : System.Web.UI.Page
{
    int UserID;
    DataTable dt = new DataTable();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["ExternalUserId"] != null)
            {
                UserID = Convert.ToInt32(Session["ExternalUserId"]);
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"]);
            }
            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Create Groups";
            BindGroupType();
            PanlHide();
            BindScrl_UserGroupDetailTblList(Convert.ToInt32(ViewState["UserID"]));
        }
    }

    private void PanlHide()
    {
        lblMessage.Text = "";
        lblId.Text = "";
        lnkAdd.Text = "+ Add New";
        PnlAdd.Visible = false;
        PnlView.Visible = true;
    }

    private void PanlShow()
    {
        lblMessage.Text = "";
        lnkAdd.Text = "- Add New";
        PnlAdd.Visible = true;
        PnlView.Visible = false;
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        ClearControl();
        if (lnkAdd.Text == "+ Add New")
        {
            PanlShow();
        }
        else if (lnkAdd.Text == "- Add New")
        {
            PanlHide();
        }
    }

    private void BindGroupType()
    {
        DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
        DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();

        drpGroupType.DataSource = objDA.GetDataTableGroupType(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetgroupType);
        drpGroupType.DataTextField = "strGroupType";
        drpGroupType.DataValueField = "intGroupTypeId";
        drpGroupType.DataBind();
        drpGroupType.Items.Insert(0, "Select Group");

    }

    private void BindScrl_UserGroupDetailTblList(int userid)
    {
        DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
        DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();
        objDO.intAddedBy = userid;
        dtlScrl_UserGroupDetailTbl.DataSource = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.AllRecords);
        dtlScrl_UserGroupDetailTbl.DataBind();
    }

    protected void dtlScrl_UserGroupDetailTbl_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        lblId.Text = "";
        DataTable dtScrl_UserGroupDetailTbl;
        if (e.CommandName == "Edit")
        {
            int index = e.Item.DataItemIndex;
            ListViewDataItem item = (ListViewDataItem)e.Item;
            HiddenField hdninGroupId = e.Item.FindControl("hdninGroupId") as HiddenField;
            int inGroupId = Convert.ToInt32(hdninGroupId.Value);
            PanlShow();
            DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
            DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();
            lblId.Text = Convert.ToString(inGroupId);
            objDO.inGroupId = inGroupId;
            dtScrl_UserGroupDetailTbl = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.SingleRecord);

            txtGroupName.Text = Convert.ToString(dtScrl_UserGroupDetailTbl.Rows[0]["strGroupName"]);
            txtSummary.Text = Convert.ToString(dtScrl_UserGroupDetailTbl.Rows[0]["strSummary"]);

            //   drpGroupType.Text=Convert.ToString(dtScrl_UserGroupDetailTbl.Rows[0]["strGroupType"]);
            drpGroupType.ClearSelection();
            BindGroupType();
            try
            {
                drpGroupType.Items.FindByValue(dtScrl_UserGroupDetailTbl.Rows[0]["strGroupType"].ToString()).Selected = true;
            }
            catch
            {
            }

            RdBList_Access.ClearSelection();
            RdBList_Access.Items.FindByValue(dtScrl_UserGroupDetailTbl.Rows[0]["strAccess"].ToString()).Selected = true;

            if (dtScrl_UserGroupDetailTbl.Rows[0]["bitPrivatePublic"].ToString() == "True")
            {
                RdblistPrivPub.ClearSelection();
                RdblistPrivPub.Items.FindByValue("1").Selected = true;
            }
            else if (dtScrl_UserGroupDetailTbl.Rows[0]["bitPrivatePublic"].ToString() == "False")
            {
                RdblistPrivPub.ClearSelection();
                RdblistPrivPub.Items.FindByValue("0").Selected = true;
            }
            else
            {
                RdblistPrivPub.ClearSelection();
            }

            txtDescription.Text = Convert.ToString(dtScrl_UserGroupDetailTbl.Rows[0]["strDescription"]);
            // txtAccess.Text=Convert.ToString(dtScrl_UserGroupDetailTbl.Rows[0]["strAccess"]);

            //  txtLogoPath.Text=Convert.ToString(dtScrl_UserGroupDetailTbl.Rows[0]["strLogoPath"]);

        }
        else if (e.CommandName == "Delete")
        {
            int index = e.Item.DataItemIndex;
            ListViewDataItem item = (ListViewDataItem)e.Item;
            HiddenField hdninGroupId = e.Item.FindControl("hdninGroupId") as HiddenField;
            int inGroupId = Convert.ToInt32(hdninGroupId.Value);
            DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
            DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();
            objDO.inGroupId = inGroupId;

            dtScrl_UserGroupDetailTbl = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.Delete);
            lblMessage.Text = "Record deleted successfully";
            lblMessage.CssClass = "GreenErrormsg";

            BindScrl_UserGroupDetailTblList(Convert.ToInt32(ViewState["UserID"]));
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
        DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();
        String GroupeAccess = String.Empty;

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objDO.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        objDO.strGroupName = txtGroupName.Text.Trim();
        objDO.strSummary = txtSummary.Text.Trim();
        objDO.strGroupType = Convert.ToInt32(drpGroupType.SelectedItem.Value);
        objDO.strDescription = txtDescription.Text;
        objDO.strAccess = Convert.ToString(RdBList_Access.SelectedItem.Value);
        //It should come from dropdown,time being it is kept as static.
        objDO.locationId = 4;

        if (Convert.ToInt32(RdblistPrivPub.SelectedItem.Value) == 1)
        {
            objDO.bitPrivatePublic = true;
            GroupeAccess = "PRIVATE";
        }
        else
        {
            objDO.bitPrivatePublic = false;
            GroupeAccess = "PUBLIC";
        }

        string documentPath = "";
        if (fileupload_LogoPath.HasFile)
        {
            int FileLength = fileupload_LogoPath.PostedFile.ContentLength;
            if (FileLength <= 3145728)
            {
                documentPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(fileupload_LogoPath.FileName).ToString();
                fileupload_LogoPath.SaveAs(Server.MapPath("~\\CroppedPhoto\\" + documentPath));
            }
            else
            {
                documentPath = ViewState["ImagePath"].ToString();
            }
            objDO.strLogoPath = documentPath;
        }

        objDO.strIpAddress = ip;
        if (lblId.Text != "")
        {
            objDO.inGroupId = Convert.ToInt32(lblId.Text);
            objDO.intModifiedBy = Convert.ToInt32(ViewState["UserID"]);
            objDA.AddEditDel_Scrl_UserGroupDetailTbl(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.Update);
            lblMessage.Text = "Record updated successfully";
            lblMessage.CssClass = "GreenErrormsg";
            PanlHide();
        }
        else
        {
            objDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objDA.AddEditDel_Scrl_UserGroupDetailTbl(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.Insert);
            lblMessage.Text = "Record Added successfully";
            lblMessage.CssClass = "GreenErrormsg";
            ViewState["GroupOutId"] = objDO.GroupOutId;

            string APIURL = ConfigurationManager.AppSettings["APIURL"];
            string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];

            if (!string.IsNullOrEmpty(Convert.ToString(ViewState["GroupOutId"])))
            {
                string UserURL = APIURL+"createGroup.action?groupId=" + objDO.GroupOutId + "&groupName=" + objDO.strGroupName + "&scope=PRIVATE&groupParent=Null&groupOwner=" + objDO.intRegistrationId + "";
                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();

                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();

                    objAPILogDO.strURL = UserURL;
                    objAPILogDO.strAPIType = "Group";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
                catch { }
            }
        }
        
        ClearControl();
        BindScrl_UserGroupDetailTblList(Convert.ToInt32(ViewState["UserID"]));


    }

    private void ClearControl()
    {
        txtGroupName.Text = "";
        txtSummary.Text = "";
        drpGroupType.ClearSelection();
        txtDescription.Text = "";
        //RdBList_Access.ClearSelection();
        RdBList_Access.SelectedValue = "A";
        RdblistPrivPub.SelectedValue = "0";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        PanlHide();
        ClearControl();
    }

    protected void dtlScrl_UserGroupDetailTbl_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {

    }

    protected void dtlScrl_UserGroupDetailTbl_ItemEditing(object sender, ListViewEditEventArgs e)
    {

    }

}
