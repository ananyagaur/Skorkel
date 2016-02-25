using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public partial class my_points : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DA_MyPoints objDAPoint = new DA_MyPoints();
    DO_MyPoints objDOPoint = new DO_MyPoints();
    DataTable dtPoint = new DataTable();
    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "My Skorkel";
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            LoadPersonalPoint();
            LoadDeFactoPoint();
        }
    }

    protected void LoadPersonalPoint()
    {
        objDOPoint.UserID = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAPoint.GetDataTable(objDOPoint, DA_MyPoints.MyPoint.GetPersonalPoint);

        if (ISAPIURLACCESSED != "0")
        {
            String url = APIURL
                    + "getUserScore.action?uId=" + Convert.ToString(ViewState["UserID"]);
            GetMyPersonalScoreResult(url);
        }

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows.Count > 5)
            {
                divpoint.Style.Add("height", "100%");
            }
            else
            {
                divpoint.Style.Add("height", "510px");
            }
        }
        else
        {
            divpoint.Style.Add("height", "510px");
        }

        int Totalexp = 0;
        int TotalexpScore = 0;
        int FinalTotalexpScore = 0;
        objDOPoint.UserID = Convert.ToInt32(ViewState["UserID"]);

        string totalPersonalPt = (Convert.ToInt32(lblIssueScore.Text) + Convert.ToInt32(lblFactScore.Text) + Convert.ToInt32(lblImportScore.Text) + Convert.ToInt32(lblOrbiterScore.Text) + Convert.ToInt32(lblPrecedentScore.Text) + Convert.ToInt32(lblRatioScore.Text) + Convert.ToInt32(lblSummaryScore.Text)).ToString();

        if (totalPersonalPt == "")
        {
            totalPersonalPt = "0";
        }

        int TotalDefacto = 0;
        objDOPoint.UserID = Convert.ToInt32(ViewState["UserID"]);
        DataTable dtDefactoTotalScore = objDAPoint.GetDataTable(objDOPoint, DA_MyPoints.MyPoint.GetDefactoTotalScore);
        if ((dtDefactoTotalScore.Rows[0]["TotalDefacto"].ToString()) != "")
        {
            TotalDefacto = Convert.ToInt32(dtDefactoTotalScore.Rows[0]["TotalDefacto"]);
        }
        DataTable dtDefacto = objDAPoint.GetDataTable(objDOPoint, DA_MyPoints.MyPoint.GetDefactoScore);
        if (dtDefacto.Rows.Count > 0)
        {
            int count = dtDefacto.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                if (dtDefacto.Rows[i]["Education"].ToString() == "Professional Experience")
                {
                    Totalexp = Convert.ToInt32(dtDefacto.Rows[i]["Points"]);
                    FinalTotalexpScore = (Convert.ToInt32(TotalDefacto) - Totalexp);
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
                    if (Convert.ToString((dtDefactoTotalScore.Rows[0]["TotalDefacto"])) != "")
                    {
                        FinalTotalexpScore = 50 + (Convert.ToInt32(dtDefactoTotalScore.Rows[0]["TotalDefacto"]));
                    }
                    else
                    { FinalTotalexpScore = 0; }
                }
                else
                {
                    if (Convert.ToString((dtDefactoTotalScore.Rows[0]["TotalDefacto"])) != "")
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
        lbltotalScore.Text = Convert.ToString(AllScore);
        ViewState["Totalexp"] = null;
    }

    protected void LoadDeFactoPoint()
    {
        dt = new DataTable();
        DataRow row;
        int pe = 0; int sk = 0; 
        objDOPoint.UserID = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAPoint.GetDataTable(objDOPoint, DA_MyPoints.MyPoint.GetDefactoScore);

        if (dt.Rows.Count < 4)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToString(dt.Rows[i]["Education"]) == "Professional Experience")
                {
                    pe = 1;
                }
                else if (Convert.ToString(dt.Rows[i]["Education"]) == "Skillsets")
                {
                    sk = 1;
                }
            }

            if (pe == 0)
            {
                row = dt.NewRow();
                row["Education"] = "Professional Experience";
                row["Points"] = DBNull.Value;
                row["SrNo"] = "3";
                dt.Rows.InsertAt(row, 2);
            }
            if (sk == 0)
            {
                row = dt.NewRow();
                row["Education"] = "Skillsets";
                row["Points"] = DBNull.Value;
                row["SrNo"] = "4";
                dt.Rows.InsertAt(row, 3);
            }

        }
        else if (dt.Rows.Count < 5)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToString(dt.Rows[i]["Education"]) == "Professional Experience")
                {
                    pe = 1;
                }
                else if (Convert.ToString(dt.Rows[i]["Education"]) == "Skillsets")
                {
                    sk = 1;
                }
            }

            if (pe == 0)
            {
                row = dt.NewRow();
                row["Education"] = "Professional Experience";
                row["Points"] = DBNull.Value;
                row["SrNo"] = "3";
                dt.Rows.InsertAt(row, 2);
            }
            if (sk == 0)
            {
                row = dt.NewRow();
                row["Education"] = "Skillsets";
                row["Points"] = DBNull.Value;
                row["SrNo"] = "4";
                dt.Rows.InsertAt(row, 0);
            }
        }

        lstDeFacto.DataSource = dt;
        lstDeFacto.DataBind();
    }

    protected void lstDeFacto_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lblEducation = (Label)e.Item.FindControl("lblEducation");
        Label lblintScore = (Label)e.Item.FindControl("lblintScore");
        if (lblEducation.Text == "Professional Experience")
        {
            if (lblintScore.Text != "")
            {
                int ExpYear = Convert.ToInt32(lblintScore.Text);
                if (ExpYear < 3)
                {
                    lblintScore.Text = "50";
                }
                else if (ExpYear >= 3 && ExpYear < 6)
                {
                    lblintScore.Text = "100";
                }
                else if (ExpYear >= 6 && ExpYear < 10)
                {
                    lblintScore.Text = "150";
                }
                else if (ExpYear >= 10)
                {
                    lblintScore.Text = "200";
                }
                else
                {
                    lblintScore.Text = "0";
                }
            }

        }
        if (lblintScore.Text == "")
        {
            lblintScore.Text = "0";
        }
    }

    protected void GetMyPersonalScoreResult(string Research)
    {
        try
        {
            HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(Convert.ToString(Research));
            myRequest1.Method = "GET";
            WebResponse myResponse1 = myRequest1.GetResponse();
            StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
            String result = sr.ReadToEnd();
            GetJsonDocument(result);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void GetJsonDocument(string rslt)
    {
        PointDataTable();
        string data = Convert.ToString(ViewState["url"]);
        _responseJsons my = JsonConvert.DeserializeObject<_responseJsons>(rslt);
        StringBuilder sb = new StringBuilder();
        if (my.responseJson != null)
        {
            try
            {
                if (my.responseJson.issueCount == null)
                    lblIssueCount.Text = "0";
                else
                    lblIssueCount.Text = my.responseJson.issueCount;

                if (my.responseJson.issueScore == null)
                    lblIssueScore.Text = "0";
                else
                    lblIssueScore.Text = my.responseJson.issueScore;

                if (my.responseJson.factCount == null)
                    lblFactCount.Text = "0";
                else
                    lblFactCount.Text = my.responseJson.factCount;

                if (my.responseJson.factScore == null)
                    lblFactScore.Text = "0";
                else
                    lblFactScore.Text = my.responseJson.factScore;

                if (my.responseJson.importantParagraphCount == null)
                    lblImportCount.Text = "0";
                else
                    lblImportCount.Text = my.responseJson.importantParagraphCount;

                if (my.responseJson.importantParagraphScore == null)
                    lblImportScore.Text = "0";
                else
                    lblImportScore.Text = my.responseJson.importantParagraphScore;

                if (my.responseJson.orbiterDictumCount == null)
                    lblOrbiterCount.Text = "0";
                else
                    lblOrbiterCount.Text = my.responseJson.orbiterDictumCount;

                if (my.responseJson.orbiterDictumScore == null)
                    lblOrbiterScore.Text = "0";
                else
                    lblOrbiterScore.Text = my.responseJson.orbiterDictumScore;

                if (my.responseJson.precedentCount == null)
                    lblPrecedentCount.Text = "0";
                else
                    lblPrecedentCount.Text = my.responseJson.precedentCount;

                if (my.responseJson.precedentScore == null)
                    lblPrecedentScore.Text = "0";
                else
                    lblPrecedentScore.Text = my.responseJson.precedentScore;

                if (my.responseJson.ratioDecidenciCount == null)
                    lblRatioCount.Text = "0";
                else
                    lblRatioCount.Text = my.responseJson.ratioDecidenciCount;

                if (my.responseJson.ratioDecidenciScore == null)
                    lblRatioScore.Text = "0";
                else
                    lblRatioScore.Text = my.responseJson.ratioDecidenciScore;

                if (my.responseJson.summaryCount == null)
                    lblSummaryCount.Text = "0";
                else
                    lblSummaryCount.Text = my.responseJson.summaryCount;

                if (my.responseJson.summaryScore == null)
                    lblSummaryScore.Text = "0";
                else
                    lblSummaryScore.Text = my.responseJson.summaryScore;

            }
            catch
            {
            }
        }

    }

    protected void PointDataTable()
    {
        DataColumn intCount = new DataColumn();
        intCount.DataType = System.Type.GetType("System.String");
        intCount.ColumnName = "intCount";
        dtPoint.Columns.Add(intCount);

        DataColumn strTitle = new DataColumn();
        strTitle.DataType = System.Type.GetType("System.String");
        strTitle.ColumnName = "strTitle";
        dtPoint.Columns.Add(strTitle);

        DataColumn intScore = new DataColumn();
        intScore.DataType = System.Type.GetType("System.String");
        intScore.ColumnName = "intScore";
        dtPoint.Columns.Add(intScore);
    }
}

#region Classes
public class _responseJsons
{
    public responseJsons responseJson { get; set; }
}

public class responseJsons
{
    public string responseId { get; set; }
    public searchResultDocumentSetes[] searchResultDocumentSetes;  //{ get; set; }
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

public class searchResultDocumentSetes
{
    public string docType { get; set; }
    public documentese[] documentes { get; set; }
    public string searchCount { get; set; }

}

public class documentese
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