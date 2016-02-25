<%@ Page Language="C#" AutoEventWireup="true" CodeFile="create-Uploads.aspx.cs" MasterPageFile="~/Main.master"
    Inherits="create_Uploads" %>

<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script type="text/javascript">
        function ShowHide(val) {
            var lblInr = document.getElementById('<% = lblInr.ClientID %>');
            var txtPrice = document.getElementById('<% = txtPrice.ClientID %>');
            if (val == 1) {
                lblInr.style.visibility = 'visible';
                txtPrice.style.visibility = 'visible';
            }
            else {
                lblInr.style.visibility = 'hidden';
                txtPrice.style.visibility = 'hidden';
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container" style="padding-top: 65px">
                <div class="cls">
                </div>
                <!--inner container ends-->
                <div class="innerDocumentContainer">
                    <div class="innerContainer">
                        <!--groups top box starts-->
                        <Group:GroupDetails ID="grpDetails" runat="server" />
                        <!--groups top box ends-->
                        <div class="clsFooter" style="border: 0">
                        </div>
                        <!--left box starts-->
                        <div class="innerGroupBox">
                            <div class="innerContainerLeft">
                                <div class="tagContainer">
                                    <div class="tagsTitle">
                                        <div class="tag">
                                            <asp:LinkButton ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static"
                                                OnClick="lnkForumTab_Click"></asp:LinkButton>
                                        </div>
                                        <div class="tag">
                                            <%--<a href="#" id="tagSelected">Uploads</a>--%>
                                            <asp:LinkButton ID="tagSelected" runat="server" Text="Uploads" ClientIDMode="Static"
                                                OnClick="lnkUploadTab_Click"></asp:LinkButton>
                                        </div>
                                        <div class="tag">
                                            <asp:LinkButton ID="lnkPollTab" runat="server" Text="Poll" ClientIDMode="Static"
                                                OnClick="lnkPollTab_Click"></asp:LinkButton>
                                        </div>
                                        <div class="tag">
                                            <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                                                OnClick="lnkEventTab_Click"></asp:LinkButton>
                                        </div>
                                        <div class="tag">
                                            <asp:LinkButton ID="lnkJobTab" runat="server" Text="Jobs" ClientIDMode="Static" OnClick="lnkJobTab_Click"></asp:LinkButton>
                                        </div>
                                        <div class="tag">
                                            <asp:LinkButton ID="lnMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                                OnClick="lnkEventMemberTab_Click"></asp:LinkButton></div>
                                    </div>                                   
                                </div>
                                <%--<div class="tagContainer">
                                    <div class="tagsTitle">
                                        <div class="tag">
                                            <a href="#">Forums</a></div>
                                        <div class="tag">
                                            <a href="#" id="tagSelected">Uploads</a></div>
                                        <div class="tag">
                                            <a href="#">Poll</a></div>
                                        <div class="tag">
                                            <a href="#">Events</a></div>
                                        <div class="tag">
                                            <a href="#">Jobs</a></div>
                                        <div class="tag">
                                            <a href="#">Members</a></div>
                                    </div>
                                </div>--%>
                                <div class="cls">
                                    <br />
                                </div>
                                <p class="documentHeading">
                                    Upload New File</p>
                                <div align="left">
                                    <b>
                                        <br />
                                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></b>
                                </div>
                                <div class="fieldTxt">
                                    <strong>Upload New File</strong>
                                </div>
                                <div class="fieldTxt">
                                    <br />
                                    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>--%>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                    <%-- </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                                </div>
                                <div class="cls">
                                </div>
                                <div class="fieldTxt">
                                    <br />
                                    <strong>More about document</strong>
                                </div>
                                <div class="fieldTxt">
                                    <asp:TextBox ID="txtDocTitle" runat="server" ClientIDMode="Static" CssClass="MySkorkeltxtField"
                                        onfocus="if(this.value=='Add keywords seperated by commas') this.value='';" onblur="if(this.value=='') this.value='Add keywords seperated by commas';"
                                        value="Add keywords seperated by commas" MaxLength="50"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="Add keywords seperated by commas"
                                    runat="server" ControlToValidate="txtDocTitle" Display="Dynamic" ValidationGroup="t"
                                    ErrorMessage="Please enter about document" ForeColor="Red"></asp:RequiredFieldValidator>
                                <div class="cls">
                                </div>
                                <div class="fieldTxt">
                                    Put this document for sale?
                                    <asp:RadioButton runat="server" ID="rbDocSaleYes" Checked="true" Text="Yes" GroupName="RdbTest"
                                        onclick="ShowHide('1');" />
                                    <asp:RadioButton runat="server" ID="rbDocSaleNo" Text="No" GroupName="RdbTest" onclick="ShowHide('2');" />
                                    <asp:TextBox ID="txtPrice" runat="server" ClientIDMode="Static" CssClass="CalenderTxtfield"
                                        onfocus="if(this.value=='Price') this.value='';" onblur="if(this.value=='') this.value='Price';"
                                        value="Price" MaxLength="50"></asp:TextBox>&nbsp;
                                    <asp:Label ID="lblInr" runat="server" Text="INR"></asp:Label>
                                    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:RadioButton ID="rbDocSaleYes" Font-Bold="true" Text="&nbsp;&nbsp;Yes" runat="server"
                                                GroupName="Sale" OnCheckedChanged="rbDocSaleYes_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbDocSaleNo" Font-Bold="true" Text="&nbsp;&nbsp;No" runat="server"
                                                GroupName="Sale" OnCheckedChanged="rbDocSaleNo_CheckedChanged" AutoPostBack="true" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <div class="cls">
                                            </div>
                                            <asp:TextBox ID="txtPrice" runat="server" ClientIDMode="Static" CssClass="CalenderTxtfield"
                                                onfocus="if(this.value=='Price') this.value='';" onblur="if(this.value=='') this.value='Price';"
                                                value="Price" MaxLength="50"></asp:TextBox>&nbsp;
                                            <asp:Label ID="lblInr" runat="server" Text="INR"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%--<asp:CheckBox ID="CheckBox1" runat="server" />Preview--%>
                                </div>
                                <div class="fieldTxt">
                                    Downloadable?
                                    <%--   <asp:Panel ID="Panel1" runat="server">--%>
                                    <asp:RadioButton ID="rdDownloadYes" Font-Bold="true" Text="&nbsp;&nbsp;Yes" runat="server"
                                        GroupName="Download" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rdDownloadNo" Font-Bold="true" Text="&nbsp;&nbsp;No" runat="server"
                                        GroupName="Download" />
                                    <%-- </asp:Panel>--%>
                                </div>
                                <div class="cls">
                                </div>
                                <div style="padding-left: 80px; text-align: center; padding-bottom: 10px; margin-top: 15px;"
                                    class="savebutton">
                                    <asp:LinkButton ID="btnSave" CssClass="connect" runat="server" Text="Upload" Style="background: url(images/connect-btn1.png) no repeat scroll 0 0 transparent;
                                        color: #FFFFFF; float: left; text-align: center; text-decoration: none; width: 82px;
                                        text-decoration: none;" OnClick="btnSave_Click" ValidationGroup="t"></asp:LinkButton>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
