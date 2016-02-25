<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="my-points.aspx.cs"
    Inherits="my_points" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="innerDocumentContainerSpc">
            <div class="innerContainer">
                <div class="innerGroupBox">
                    <div class="skorelScoreMenu">
                        <ul>
                            <li title="My Score"><a href="my-points.aspx" class="myscore myscoreactive"></a>
                            </li>
                            <li title="My Tags"><a href="my-tag.aspx" class="mytag"></a></li>
                            <li title="My Documents"><a href="my-documents.aspx" class="mybookmark"></a></li>
                            <li title="My Bookmarks "><a href="my-bookmarks.aspx" class="mysearches"></a></li>
                            <li title="My Search"><a href=" my-saved-searches.aspx" class="mydocs"></a></li>
                        </ul>
                    </div>
                    <div class="skorkelBox">
                        <p class="headingSk">
                            My Score</p>
                        <p class="headingSkCount">
                            <asp:Label ID="lbltotalScore" runat="server"></asp:Label></p>
                    </div>
                    <div id="divpoint" runat="server">
                        <!--de facto starts-->
                        <div class="tabBoxSk" id="facto">
                            <div class="tab1Sk activeSk">
                                De Facto</div>
                            <div class="tab1Sk" onclick="showpersonal()">
                                Personal</div>
                            <div class="defactoBox">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="categoryTxt" style="width: 100%; border-bottom: none;">
                                            <div class="listHeading" style="width: 100%">
                                                <asp:ListView ID="lstDeFacto" runat="server" OnItemDataBound="lstDeFacto_ItemDataBound">
                                                    <ItemTemplate>
                                                        <p class="skHeading">
                                                            <asp:Label ID="lblEducation" runat="server" Text='<%#Eval("Education")%>'></asp:Label></p>
                                                        <p class="skCount">
                                                            <asp:Label ID="lblintScore" runat="server" Text='<%#Eval("Points")%>'></asp:Label></p>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                        <div class="cls">
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <!--de facto ends-->
                        <!--de personal starts-->
                        <div class="tabBoxSk" id="personal">
                            <div class="tab1Sk" onclick="showfacto()">
                                De Facto</div>
                            <div class="tab1Sk activeSk">
                                Personal</div>
                            <div class="defactoBox">
                                <p class="skHeading">
                                    Facts</p>
                                <p class="skCount">
                                  <asp:Label ID="lblFactScore" runat="server" Text="0" > </asp:Label> </p>
                                <p class="subSKcount">
                                    <asp:Label ID="lblFactCount" runat="server" Text="0" > </asp:Label> facts</p>
                                <p class="skHeading">
                                    Issue</p>
                                <p class="skCount">
                                   <asp:Label ID="lblIssueScore" runat="server" Text="0" > </asp:Label></p>
                                <p class="subSKcount">
                                     <asp:Label ID="lblIssueCount" runat="server" Text="0" > </asp:Label> issues</p>
                                <p class="skHeading">
                                    Orbiter Dictum</p>
                                <p class="skCount">
                                    <asp:Label ID="lblOrbiterScore" runat="server" Text="0" > </asp:Label></p>
                                <p class="subSKcount">
                                     <asp:Label ID="lblOrbiterCount" runat="server" Text="0" > </asp:Label> Orbiter Dictum</p>
                                      <p class="skHeading">
                                    Precedent</p>
                                <p class="skCount">
                                    <asp:Label ID="lblPrecedentScore" runat="server" Text="0" > </asp:Label></p>
                                <p class="subSKcount">
                                     <asp:Label ID="lblPrecedentCount" runat="server" Text="0" > </asp:Label> Precedent</p>
                                      <p class="skHeading">
                                    Ratio Decidendi</p>
                                <p class="skCount">
                                    <asp:Label ID="lblRatioScore" runat="server" Text="0" > </asp:Label></p>
                                <p class="subSKcount">
                                     <asp:Label ID="lblRatioCount" runat="server" Text="0" > </asp:Label> Ratio Decidendi</p>
                                <p class="skHeading">
                                    Important Paragraph</p>
                                <p class="skCount">
                                   <asp:Label ID="lblImportScore" runat="server" Text="0" > </asp:Label></p>
                                <p class="subSKcount">
                                     <asp:Label ID="lblImportCount" runat="server" Text="0" > </asp:Label> Important Paragraph</p>
                                <p class="skHeading">
                                    Summary</p>
                                <p class="skCount">
                                    <asp:Label ID="lblSummaryScore" runat="server" Text="0" > </asp:Label></p>
                                <p class="subSKcount">
                                     <asp:Label ID="lblSummaryCount" runat="server" Text="0" > </asp:Label> Summary</p>
                            </div>
                        </div>
                        <!--de personal ends-->
                    </div>
                    <div class="cls">
                    </div>
                </div>
                <!--left box ends-->
            </div>
            <!--left verticle search list ends-->
        </div>
        <!--left verticle search list ends-->
    </div>
    <script language="javascript">
        function showfacto() {
            document.getElementById('facto').style.display = "block";
            document.getElementById('personal').style.display = "none";
        }
        function showpersonal() {
            document.getElementById('personal').style.display = "block";
            document.getElementById('facto').style.display = "none";
        }
    </script>
</asp:Content>
