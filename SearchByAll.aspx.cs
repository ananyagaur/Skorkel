using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Web.UI;


public partial class SearchByAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "My Skorkel";
        }
    }
}