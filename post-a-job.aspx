<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="post-a-job.aspx.cs"
    Inherits="post_a_job" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <link href="Styles/jquery.datepick.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.datepick.js" type="text/javascript"></script>
    <script type="text/javascript">
        function getMultipleValues(ctrlId) {

            $('#tdDepartment').find('label.error').remove();
            var control = document.getElementById(ctrlId);
            var strSelText = '';
            var cnt = 0;
            for (var i = 0; i < control.length; i++) {
                if (control.options[i].selected) {
                    if (cnt == 0) {
                        strSelText += control.options[i].value;
                    }
                    else {
                        strSelText += ',' + control.options[i].value;
                    }
                    cnt++;
                }
            }
            $('#hdnDepartmentIds').val(strSelText);

        }
    
    
    </script>
    <script type="text/javascript">

        function HideTextBox(lstCity) {

            var ControlName = document.getElementById(lstCity.id);
            document.getElementById('txtOther').style.display = 'none';
            if (ControlName.value != 57)  //it depends on which value Selection do u want to hide or show your textbox 
            {

                document.getElementById('txtOther').style.display = 'none';
            }
            else {
                document.getElementById('txtOther').style.display = '';


            }
        } 
    </script>
    <meta charset="utf-8">
    <link rel="stylesheet" href="docsupport/style.css">
    <link rel="stylesheet" href="docsupport/prism.css">
    <link href="docsupport/chosen.css" rel="stylesheet" type="text/css" />
    <%-- <link rel="stylesheet" href="chosen.css">--%>
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
        .SelectedTag
        {
            text-decoration: none;
            color: #00A4AD;
            font-weight: bold;
            font-size: 18px;
        }
        .crtFrumBtn
        {
            text-decoration: none;
            color: #00A4AD;
            font-weight: bold;
            margin-right: 10px;
            text-align: right;
        }
        .divUpimage
        {
            margin-top: 5px;
            margin-left: 30px;
            background-color: #00A4AD;
        }
        .divAllForum
        {
            margin-left: 20px;
            margin-top: -22px;
            font-size: large;
        }
        .createPollBtn
        {
            width: 130px;
            height: 23px;
            float: right;
            background: url(../images/create-forum.png) no-repeat;
            color: #fff;
            text-decoration: none;
            padding: 2px 0px;
            text-align: center;
        }
    </style>
    <script src="docsupport/chosen.jquery.js" type="text/javascript"></script>
    <script src="docsupport/prism.js" type="text/javascript"></script>
    <script type="text/javascript">

        function DateControl() {
            $(function () {

                $('#txtExpireDate').datepick({ showTrigger: '#imgInterview' });

            });
        }
        DateControl();
       
    </script>
    <script language="javascript" type="text/javascript">
        function rdoChanged(rdo) {
            if (rdo.id == '<%= rdblNever.ClientID %>') {
                document.getElementById('<%= txtExpireDate.ClientID %>').disabled = true;
            }

            else if (rdo.id == '<%= rdblDays.ClientID %>') {
                document.getElementById('<%= txtExpireDate.ClientID %>').disabled = false;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#Close').click(function () {
                $("#txtTitle").val('Title');
                $("#CKDescription").val('Description');
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container" style="padding-top: 65px">
        <div class="innerDocumentContainer">
            <div class="innerContainer">
                <!--groups top box starts-->
                <Group:GroupDetails ID="grpDetails" runat="server" />
                <!--groups top box ends-->
                <div class="clsFooter" style="border: 0">
                </div>
                <!--left box starts-->
                <div class="innerGroupBox" style="width: 960px">
                    <div class="innerContainerLeft" style="width: 900px">
                        <div class="tagContainer" style="width: 900px">
                            <div class="tagsTitle" style="width: 900px">
                                <div class="tagM">
                                    <asp:LinkButton ID="lnkProfile" runat="server" Text="Profile" ClientIDMode="Static"
                                        OnClick="lnkProfile_Click"></asp:LinkButton>
                                </div>
                                <div id="DivHome" style="margin-left: 5px; display: none" class="tagM" runat="server">
                                    <asp:LinkButton ID="lnkHome" runat="server" Text="Home" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                                </div>
                                <div class="tagM" id="DivForumTab" runat="server" clientidmode="Static" style="display: none">
                                    <asp:LinkButton ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static"
                                        OnClick="lnkForumTab_Click"></asp:LinkButton>
                                </div>
                                <div class="tagM" id="DivUploadTab" runat="server" clientidmode="Static" style="display: none">
                                    <asp:LinkButton ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static"
                                        OnClick="lnkUploadTab_Click"></asp:LinkButton>
                                </div>
                                <div class="tagM" id="DivPollTab" runat="server" clientidmode="Static" style="display: none">
                                    <asp:LinkButton ID="lnkPollTab" runat="server" Text="Poll" ClientIDMode="Static"
                                        OnClick="lnkPollTab_Click"></asp:LinkButton>
                                </div>
                                <div class="tagM" id="DivEventTab" runat="server" clientidmode="Static" style="display: none">
                                    <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                                        OnClick="lnkEventTab_Click"></asp:LinkButton>
                                </div>
                                <div id="DivJobTab" runat="server" clientidmode="Static" class="divOver" style="display: none;
                                    width: 65px; text-align: center;">
                                    <asp:LinkButton ID="tagSelected1" runat="server" Text="Jobs" ClientIDMode="Static"
                                        CssClass="SelectedTag" OnClick="lnkJobTab_Click"></asp:LinkButton>
                                    <div class="divUpimage">
                                        <asp:Image ID="img" runat="server" ImageUrl="~/images/up.jpg" BackColor="#00A4AD" /></div>
                                </div>
                                <div class="tagM" id="DivMemberTab" runat="server" clientidmode="Static" style="display: none">
                                    <asp:LinkButton ID="lnMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                        OnClick="lnkMemberTab_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div style="border: #c6c8ca solid 1px; height: 100%; margin-top: 35px; text-align: left;">
                            <div class="cls">
                                <br />
                            </div>
                            <%-- <a class="blueLink" href="forum-landing-page.html">&lt;&lt; All Forums</a>--%>
                            <div class="cls">
                            </div>
                            <%-- <div class="searchResult">
                            <asp:LinkButton CssClass="teal" ID="lnkAllForum" Font-Underline="false" Text=" << All Jobs"
                                OnClick="lnkAllJobs_Click" runat="server"></asp:LinkButton>
                            <div class="documentHeading">
                                Post a Job</div>
                        </div>--%>
                            <div class="searchResult" style="border-bottom: #c6c8ca solid 1px; width: 898px;margin-top: -10px;">
                                <img src="images/previous.jpg" id="img1" runat="server" />
                                <div class="divAllForum">
                                    <asp:LinkButton ID="lnkAllForum" runat="server" class="blueLink" OnClick="lnkAllJobs_Click"
                                        Text="Back"></asp:LinkButton></div>
                                <div style="margin-left: 45%; margin-top: -22px;">
                                    <p class="documentHeading">
                                        Post a Job</p>
                                </div>
                            </div>
                            <div align="left">
                                <b>
                                    <br />
                                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></b>
                            </div>
                            <div class="cls">
                            </div>
                            <div id="divJobSuccess" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                                float: left; width: 500px; padding-top: 0px; position: relative; margin: -200px 0 0 50px;
                                z-index: 100; display: none;" clientidmode="Static">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                    <tr>
                                        <td>
                                            <strong>&nbsp;&nbsp;
                                                <asp:Label ID="lblSuccess" runat="server" Text="Job posted successfully." ForeColor="Green"
                                                    Font-Size="Small"></asp:Label>
                                                <asp:Label ID="lblEditSuccess" runat="server" Text="Job updated successfully." ForeColor="Green"
                                                    Font-Size="Small"></asp:Label>
                                            </strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="popBgLineGray">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <table width="100" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <a href="#" clientidmode="Static" causesvalidation="false" sstyle="float: left; text-align: center;
                                                        text-decoration: none; width: 82px; padding-top: 5px; color: #000;" onclick="javascript:JobMesClose();">
                                                            Close </a>
                                                        <%-- <asp:LinkButton ID="lnkClose" runat="server" Style="float: left; text-align: center;
                                                        text-decoration: none; width: 82px; padding-top: 5px; color: #000;" ClientIDMode="Static"
                                                        Text="Close" CausesValidation="false" OnClick="lnkClose_Click"></asp:LinkButton>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="postAJob">
                                <!--field starts-->
                                <asp:TextBox ID="txtTitle" Style="font-size: small;" runat="server" class="jobInput"
                                    ClientIDMode="Static" MaxLength="100" value="Title" onblur="if(this.value=='') this.value='Title';"
                                    onfocus="if(this.value=='Title') this.value='';"></asp:TextBox>
                                <div style="padding-left: 25px;" class="fieldTxt">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="Title" runat="server"
                                        ControlToValidate="txtTitle" Display="Dynamic" ValidationGroup="t" ErrorMessage="Please select Title"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <!--field ends-->
                                <!--Content Editor starts-->
                                <div class="textEditorPostNew">
                                    <textarea rows="10" style="font-family: Calibri; font-size: 14px; margin-left: 0px;
                                        margin-top: 0; width: 412px; border: 2px solid #dfd5d0; -moz-border-radius: 3px;
                                        -webkit-border-radius: 7px; -khtml-border-radius: 3px; -ms-border-radius: 3px;
                                        border-radius: 7px; padding: 4px 5px; color: #666666; -moz-box-shadow: 2px 2px 5px #d4d3d3 inset;
                                        box-shadow: 2px 2px 5px #d4d3d3 inset;" id="CKDescription" runat="server" onfocus="if(this.value=='Description') this.value='';"
                                        onblur="if(this.value=='') this.value='Description';" value="Description" clientidmode="Static"></textarea>
                                </div>
                                <div class="cls">
                                </div>
                            </div>
                            <div style="padding-top: 5px; padding-bottom: 8px;">
                                <asp:TextBox ID="txtOrgName" value="Organization Name" onblur="if(this.value=='') this.value='Organization Name';"
                                    onfocus="if(this.value=='Organization Name') this.value='';" runat="server" Style="border: 1px solid #cccccc;
                                    -moz-border-radius: 3px; -webkit-border-radius: 3px; -khtml-border-radius: 3px;
                                    -ms-border-radius: 3px; border-radius: 3px; padding: 4px 2px; width: 264px; font-size: small"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="Organization Name"
                                    runat="server" ControlToValidate="txtOrgName" Display="Dynamic" ValidationGroup="t"
                                    ErrorMessage="Please enter organization name" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="postStat">
                                <span class="chooseALoc">Choose a Location</span><br />
                                <asp:ListBox ID="lstCity" Style="font-size: small; color: #333333;" ClientIDMode="Static"
                                    SelectionMode="Multiple" runat="server" onchange="HideTextBox(this);"></asp:ListBox>
                                <div class="fieldTxt" style="padding-top: 0px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="lstCity"
                                        InitialValue="" runat="server" ValidationGroup="t" ErrorMessage="Please select atleast one city"></asp:RequiredFieldValidator>
                                </div>
                                <asp:TextBox ID="txtOther" Style="font-size: small;" runat="server" class="chooseLoc"
                                    value="Other" onblur="if(this.value=='') this.value='Other';" onfocus="if(this.value=='Other') this.value='';"
                                    ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="postCheckBox">
                                <asp:UpdatePanel ID="UpdatePanel1" ClientIDMode="Static" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBoxList ID="chkJobType" runat="server" AutoPostBack="true">
                                            <asp:ListItem Value="PT">Part time</asp:ListItem>
                                            <asp:ListItem Value="FT">Full time</asp:ListItem>
                                            <asp:ListItem Value="FR">Freelance</asp:ListItem>
                                            <asp:ListItem Value="TI">Trainee/ Intern</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <p style="margin-left: 25px">
                                    Expires
                                </p>
                                <p style="margin-left: 30px;">
                                    <asp:RadioButton ID="rdblNever" Checked="true" class="checkBox" Text="Never" GroupName="Never"
                                        runat="server" onclick="rdoChanged(this);" />
                                </p>
                                <p style="margin-left: 30px;">
                                    <asp:RadioButton ID="rdblDays" GroupName="Never" runat="server" onclick="rdoChanged(this);" />
                                    <asp:TextBox ID="txtExpireDate" runat="server" ClientIDMode="Static" CssClass="txtFieldSmallNew"
                                        onkeypress="event.returnValue = false;" onkeydown="event.returnValue = false;"
                                        onfocus="if(this.value=='Select Date') this.value='';" onblur="if(this.value=='') this.value='Select Date';"
                                        value="Select Date" MaxLength="50"></asp:TextBox>
                                    <div style="display: none;">
                                        &nbsp;&nbsp;<img style="vertical-align: bottom" src="images/calender.png" id="imgInterview" />
                                    </div>
                                    <asp:Label ID="lblDateMess" ForeColor="Red" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                            <div class="cls">
                            </div>
                            <p class="line" style="max-width: 898px;">
                            </p>
                            <p style="padding-left: 42%;">
                                <asp:LinkButton ID="btnSave" CssClass="connect" runat="server" Text="Post" Style="background: url(images/connect-btn1.png) no repeat scroll 0 0 transparent;
                                    color: #FFFFFF; text-align: center; text-decoration: none; width: 82px; text-decoration: none;"
                                    ValidationGroup="t" OnClick="btnSave_Click"></asp:LinkButton>
                                <asp:LinkButton ID="btnUpdate" CssClass="connect" runat="server" Text="Update" Style="background: url(images/connect-btn1.png) no repeat scroll 0 0 transparent;
                                    color: #FFFFFF; text-align: center; text-decoration: none; width: 82px; text-decoration: none;"
                                    ValidationGroup="t" OnClick="btnUpdate_Click"></asp:LinkButton>
                                  <a id="Close" href="#" clientidmode="Static" causesvalidation="true" style="float: left; text-align: center; 
                                                            text-decoration: none; width: 82px; padding-top: 20px; color: #000;">
                                                            Cancel </a>
                               <%-- <asp:LinkButton ID="Close" CssClass="connect" runat="server" Text="Cancel" Style="background: url(images/connect-btn1.png) no repeat scroll 0 0 transparent;
                                    color: #FFFFFF; text-align: center; text-decoration: none; width: 82px; text-decoration: none;"
                                    ValidationGroup="t"></asp:LinkButton>--%>
                            </p>
                            <br />
                            <br />
                        </div>
                    </div>
                    <!--left box ends-->
                </div>
                <!--left verticle search list ends-->
            </div>
        </div>
        <asp:HiddenField ID="hdnJobId" ClientIDMode="Static" runat="server" />
        <div style="display: none">
            <asp:Button ID="btnStatus" runat="server" ClientIDMode="Static" OnClick="btnStatus_Click" />
        </div>
    </div>
    <script type="text/javascript">
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "95%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }

        function AlertUpdate() {
            //  alert('Job updated successfully.');
            $("#btnStatus").click();
        }
    </script>
    <script type="text/javascript">
        function JobMesClose() {
            document.getElementById("divJobSuccess").style.display = 'none';
        }
    </script>
</asp:Content>
