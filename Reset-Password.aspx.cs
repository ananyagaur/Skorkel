using System;
using DA_SKORKEL;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Reset_Password : System.Web.UI.Page
{
    DA_Login objLoginDB = new DA_Login();
    DO_Login objLogin = new DO_Login();
    DO_Registrationdetails objregistration = new DO_Registrationdetails();

    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtOldPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfPassword.Text = "";
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }
            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Reset Password";
        }
    }

    protected void lnkResetPassword_Click(object sender, EventArgs e)
    {
        CryptoGraphy objEncrypt = new CryptoGraphy();
        lnkResetPassword.Enabled = false;
        objLogin.intRegistartionID = Convert.ToInt32(ViewState["UserID"]);
        dt = objLoginDB.GetDataTable(objLogin, DA_Login.Login_1.UserDetails);
        if (dt.Rows.Count > 0)
        {
            string oldPass = objEncrypt.Decrypt(Convert.ToString(dt.Rows[0]["vchrPassword"]));
            if (txtOldPassword.Text.Trim() == Convert.ToString(objEncrypt.Decrypt(Convert.ToString(dt.Rows[0]["vchrPassword"]))))
            {
                objregistration.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
                objregistration.Password = objEncrypt.Encrypt(txtNewPassword.Text.Trim());
                objLoginDB.AddEditDel_RegistrationDetails(objregistration, DA_Login.Login_1.SelectForUser);

                objLoginDB.AddAndGetLoginDetails(objLogin, DA_SKORKEL.DA_Login.Login_1.ChangePassword);

                lblMessage.Text = "Password updated successfully.";
                lblMessage.CssClass = "GreenErrormsg";
                lnkResetPassword.Enabled = true;
            }
            else
            {
                lblMessage.Text = "Old password is incorrect.";
                lblMessage.CssClass = "RedErrormsg";
                lnkResetPassword.Enabled = true;
                return;
            }
        }
        else
        {
            lblMessage.Text = "Old password is incorrect.";
            lblMessage.CssClass = "RedErrormsg";
            lnkResetPassword.Enabled = true;
            return;
        }
    }
}