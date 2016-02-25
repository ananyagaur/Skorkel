using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class LinkedInAccountDetails : System.Web.UI.Page
{
    private oAuthLinkedIn _oauth = new oAuthLinkedIn();
    string accessToken = "";
    string accessTokenSecret = "";
    string gettoken = null;
    string getotokensecret = null;
    string getverifier = null;
    protected void Page_Load(object sender, EventArgs e)
    {


        gettoken = Session["Session_OToken"].ToString();
        getotokensecret = Session["Session_OTokenSecret"].ToString();
        getverifier = Request.QueryString["oauth_verifier"];

        //Get Access Token
        _oauth.Token = gettoken;
        _oauth.TokenSecret = getotokensecret;
        _oauth.Verifier = getverifier;

        _oauth.AccessTokenGet(gettoken);
        accessToken = _oauth.Token;
        accessTokenSecret = _oauth.TokenSecret;

        _oauth.Token = accessToken;
        _oauth.TokenSecret = accessTokenSecret;
        _oauth.Verifier = getverifier;


        //Display profile
        string response = _oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/people/~/", null);

        //txtApiResponse.Text = response;

        XmlDocument xml = new XmlDocument();
        xml.LoadXml(response); // suppose that myXmlString contains "<Names>...</Names>"

        XmlNodeList xnList = xml.SelectNodes("/person");

        foreach (XmlNode xn in xnList)
        {
            string firstName = xn["first-name"].InnerText;
            string lastName = xn["last-name"].InnerText;
            string CompanyName = xn["headline"].InnerText;

            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
            //lblCompanyName.Text = CompanyName;

        }
        XmlNodeList xnList1 = xml.SelectNodes("/");
        string response2 = _oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/people/~/email-address", null);
        xml.LoadXml(response2);
        foreach (XmlNode Xnn in xnList1)
        {
            string emailAddress = Xnn["email-address"].InnerText;
            txtEmailID.Text = emailAddress;
        }
    }
}