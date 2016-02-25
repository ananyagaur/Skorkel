<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="Research_SearchResult_S.aspx.cs" Inherits="Research_SearchResult_S" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
    <script type="text/javascript" src="<%=ResolveUrl("js/ddsmoothmenu.js")%>"></script>
    <style type="text/css">
        input::-webkit-input-placeholder, textarea::-webkit-input-placeholder
        {
            color: #9c9c9c !important;
        }
        input:-moz-placeholder, textarea:-moz-placeholder
        {
            color: #9c9c9c !important;
        }
        .TooltipDetailsPop{	-webkit-box-shadow:0 0 5px #999;
	-moz-box-shadow:0 0 5px #999;
	box-shadow:0 0 5px #999;  background: none repeat scroll 0 0 #fff;
    float: left;
    left: 10px;
    position: absolute;
    top: 800px;
    width: auto; padding:0px;
    z-index:9999;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="cls">
    </div>
    <div class="container-popupreserch" id="divPopp" style="display: none;">
        <div class="popupreserch" style="width: 200px; font-weight: bold;">
            Please Wait..
        </div>
    </div>
     <div class="container-popuprewait" id="divWait" style="display: none;">
        <div class="popuprewait" style="width: 200px; font-weight: bold;">
           <img src="images/waitImg.gif" />
        </div>
    </div>
    <div class="innerContainer" id="documentContainer" style="margin-top: -40px;">
        <div class="headingNew">
            Research</div>
    </div>
    <div class="cls">
    </div>
    <div class="innerDocumentContainerSpc">
        <div class="innerContainer">
            <div class="innerGroupBox">
                <div id="divsearchHeight" runat="server" style="height: 550px;">
                    <div id="divSavesearch" clientidmode="Static" runat="server" class="divsaveSearch">
                        <div style="margin: 21px 0px 0px 43px;">
                            <asp:TextBox ID="txtsaveTitle" runat="server" value="Title" onblur="if(this.value=='') this.value='Title';"
                                ValidationGroup="titles" onfocus="if(this.value=='Title') this.value='';" class="forumTitlenew"
                                Style="width: 400px; margin-left: 0px; z-index: 1000;"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtsaveTitle"
                                InitialValue="Title" Display="Dynamic" ClientIDMode="Static" ValidationGroup="titles"
                                ErrorMessage="Please Enter Title." ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <div style="margin: 15px 0px 0px 135px;">
                            <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Save" OnClientClick="CallSearchSave();"
                                ValidationGroup="titles" CausesValidation="true" Style="margin-left: 0%;" CssClass="vote"
                                OnClick="lnkPopupOK_Click"></asp:LinkButton>
                        </div>
                        <br />
                        <div style="margin: -2px 0px 0px 0px;">
                            <a href="#" clientidmode="Static" style="text-align: center;
                                text-decoration: none; width: 82px; color: #000; margin-left: 30px;" onclick="MessClose();">
                                Cancel </a>
                        </div>
                    </div>
                    <!--search box starts-->
                    <div class="innerContainerLeftMenu">
                        <div>
                            <asp:DropDownList ID="ddlSelect" runat="server" ClientIDMode="Static" CssClass="researchListBox" >
                                <asp:ListItem Text="Free Text" Value="F"></asp:ListItem>
                                <asp:ListItem Text="Citation" Value="C"></asp:ListItem>
                                <asp:ListItem Text="Skorkel" Value="S"></asp:ListItem>
                                <asp:ListItem Text="Target" Value="T"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <!--search box ends-->
                    <!--left box starts-->
                    <div class="innerContainerLeft noBorder">
                        <div class="researchSearchTxt">
                            <div class="researchSearchBox" id="divMainSearch" runat="server" clientidmode="Static">
                                <div id="NormalSearch">
                                    <input type="text" id="txtResearch" runat="server" clientidmode="Static" class="researchSearchTxtInput"
                                        style="width: 649px;" placeholder="Enter Keywords" /><asp:ImageButton runat="server"
                                            ID="btnSearch" src="images/research-search.png" ClientIDMode="Static" Style="padding: 3px 16px;
                                            margin: 0 9px 0 -58px;" class="reserachSearchImg" OnClick="btnSearch_Click" />
                                    <p id="pAdvSearch" style="margin: 5px 70px 10px 0px;">
                                        <a href="#" runat="server" clientidmode="Static" id="anchorAdvnceSearch" class="hideResearch" style="font-size: 18px">Advanced Search</a>
                                    </p>
                                </div>
                            </div>
                            <div class="researchSearchBox" id="divCitationSearch" runat="server" clientidmode="Static" style="margin: 32px 10px 10px 0px;display:none;">
                                <div class="researchBorder" style="  width: 660px;">
                                    <asp:DropDownList ID="ddlYear" runat="server" ClientIDMode="Static" CssClass="yearList">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlCourt" runat="server" ClientIDMode="Static" CssClass="courtListBoxN" style="width: 165px;">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlReporterName" runat="server" ClientIDMode="Static" CssClass="reporterNameList" style="width: 166px;" >
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlVolumns" runat="server" ClientIDMode="Static" CssClass="volumeList">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlPageNo" runat="server" ClientIDMode="Static" CssClass="pagenoList">
                                    </asp:DropDownList>
                                </div>
                                <asp:ImageButton runat="server" ID="ImageButton1" src="images/research-search.png" OnClientClick="javascript:citationCall();return true;"
                                    ClientIDMode="Static" Style="padding: 3px 16px; margin: 0 9px 0 -60px;" class="reserachSearchImg"
                                    OnClick="btnSearch_Click" />
                                <div class="cls">
                                </div>
                            </div>
                            <div id="divFreeSkorlTargetSearch" runat="server" clientidmode="Static" >
                                <div class="cls hgt">
                                </div>
                                <div style="margin-bottom:5px;" >
                                <span style="color: Black;  margin-left: 7px;">
                                    <label id="lbljudgename" runat="server" clientidmode="Static">
                                        Name of the Judges
                                    </label>
                                </span>
                                </div>
                                <select data-placeholder="Enter the names of the judges" class="chosen-select researchSearchTxtInput"
                                    style="color: #9c9c9c; width: 660px;" id="txtJudgesMember" onchange="getAllMemberValue(this.id)"
                                    runat="server" clientidmode="Static" multiple tabindex="4">
                                </select>
                                <asp:ImageButton runat="server" ID="imgBtntarget" ImageUrl="images/research-search.png" OnClientClick="javascript:targetCall();return true;"
                                    ClientIDMode="Static" Style="padding: 3px 16px;margin: 0 10px 0 -60px; display: none;" class="reserachSearchImg"
                                    OnClick="btnSearch_Click" />
                                <asp:RequiredFieldValidator InitialValue="Enter the names of the Judges" ValidationGroup="InvMem"
                                    ClientIDMode="Static" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtJudgesMember"
                                    ErrorMessage="Enter members name" ForeColor="Red" Style="position: absolute;
                                    padding-left: 0px; padding-top: 0px; font-size: small"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hdnMembId" ClientIDMode="Static" runat="server" />
                                 <asp:HiddenField ID="hdnMembIdText" ClientIDMode="Static" runat="server" />
                                <div class="cls hgt">
                                </div> <div class="cls hgt">
                                </div>
                                <div class="year">
                                <div style="margin-bottom:-13px;" >
                                    <span style="color: Black;  margin-left: 7px;">Year</span>
                                    </div>
                                    <br />
                                    <asp:DropDownList ID="ddlYearFT" runat="server" ClientIDMode="Static" CssClass="yearList"
                                        Style="width: auto;">
                                    </asp:DropDownList>
                                </div>
                                <div class="courtList">
                                <div style="margin-bottom:-13px;" >
                                    <span style="color: Black;  margin-left: 7px;">Court</span>
                                    </div>
                                    <br />
                                    <asp:DropDownList ID="ddlCourtFT" runat="server" ClientIDMode="Static" CssClass="yearList"
                                        Style="width: 530px;">
                                    </asp:DropDownList>
                                </div>
                                <div class="cls hgt">
                                </div>
                                <div class="provisionList" style="margin-bottom:2px;">
                                    <span style="color: Black;  margin-left: 7px;">Context</span><br />
                                </div>
                                <div id="MicroTag" class="subjectMacroTags" style="border: none;">
                                    <div class="researchTagAreaa" style="display: none;">
                                        <ul>
                                            <asp:UpdatePanel ID="uplstSubject" runat="server">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="lstSubjCategory" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:ListView ID="lstSubjCategory" runat="server" OnItemDataBound="LstSubjCategory_ItemDataBound"
                                                        OnItemCommand="LstSubjCategory_ItemCommand">
                                                        <ItemTemplate>
                                                            <div class="subjectTags">
                                                                <div class="researchTag">
                                                                    <li id="SubLi" runat="server">
                                                                        <asp:HiddenField ID="hdnSubCatId" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                                        <asp:LinkButton ID="lnkCatName" CssClass="copr" Style="text-decoration: none !important;
                                                                            color: #fff;" Font-Underline="false" runat="server" Text='<%#Eval("strCategoryName")%>'
                                                                            CommandName="Subject Category"></asp:LinkButton>
                                                                        <div class="corpClose" style="-moz-margin-top: 0px;">
                                                                            <img src="images/research-tag-close.png" alt="no image."></div>
                                                                    </li>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ul>
                                    </div>
                                    <br />
                                    <div style="margin-top: -15px;">
                                        <select data-placeholder="Select multiple subjects to add in search" class="chosen-select multipleSubjects"
                                            style="width: 660px;" id="txtSubjectList" onchange="getAllSubjectValue(this.id)"
                                            runat="server" clientidmode="Static" multiple tabindex="4">
                                        </select>
                                        <asp:HiddenField ID="hdnsubject" ClientIDMode="Static" runat="server" />
                                         <asp:HiddenField ID="hdnsubjectText" ClientIDMode="Static" runat="server" />
                                    </div>
                                    <div class="cls">
                                    </div>
                                    <div class="cls hgt">
                                    </div>
                                </div>
                                <div class="cls hgt">
                                </div>
                                <input id="btnSaveSearch" visible="false" type="submit" runat="server" clientidmode="Static"
                                    value="Save this Search" class="saveSearchBtn" onclick="return btnSaveSearch_onclick()" />
                                <div class="cls hgt">
                                </div>
                           </div>
                       </div>
                    </div>
                    <!--left box ends-->
                    <div id="divResearchResult">
                    <asp:UpdatePanel runat="server" ID="Up1"> 
                    <ContentTemplate>
                        <!---search result starts-->
                        <div class="researchSearchResultHeading">
                            <div class="enterKeyword">
                            <asp:Label id="lblEnterKeyword" runat="server" ClientIDMode="Static" Text="Entered Keyword or Search Attributes"  ></asp:Label>
                                <%--(Entered Keyword or Search Attributes)--%></div>
                                <asp:HiddenField ID="hdnEnterKeyword" runat="server" ClientIDMode="Static" Value="" />
                            <div class="enterKeywordResults">
                                Results:
                                <asp:Label ID="lblResultCount" runat="server" Text="0"></asp:Label>
                            </div>
                            <div class="enterKeywordMostRecent" style="margin-left: 52px">
                                <div id="smoothmenu1" class="ddsmoothmenu">
                                    <ul class="menus" style="margin-top: 0px;">
                                        <li id="idzIndex" runat="server">
                                            <img src="images/filter.jpg" style="z-index: 0;" alt="" /><a href="#" id="active" onclick="return false;" >Most Recent</a>
                                            <ul class="subMenus" style="margin-top: -5px;">
                                             <li><asp:LinkButton ID="lnkMR" runat="server" OnClick="lnkMR_click" >Most Recent</asp:LinkButton></li>
                                                <li><asp:LinkButton ID="lnkMS" runat="server" OnClick="lnkMS_click" > Most Shared</asp:LinkButton></li>
                                                <li><asp:LinkButton ID="lnkMD" runat="server" OnClick="lnkMD_click" >Most Downloaded</asp:LinkButton></li>
                                                <li><asp:LinkButton ID="lnkComment" runat="server" OnClick="lnkComment_click" >Most Commented</asp:LinkButton></li>
                                              </ul>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div class="enterKeywordSave">
                                <asp:LinkButton ID="lnkSAvesearch" runat="server" ClientIDMode="Static" Text="Save this Search"
                                    Enabled="true" OnClick="btnSaveSearch_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <!---search result ends-->
                        <div class="cls">
                        </div>
                        <div class="tabsBox">
                            <!--tabs starts-->
                            <div class="tabs">
                                <ul>
                                    <li class="activeTab"><a href="#">Cases</a></li>
                                </ul>
                            </div>
                            <div class="bottomBorder">
                            </div>
                            <!--tabs ends-->
                            <div>
                                <asp:ListView ID="lstSearchResult" runat="server" >
                                    <ItemTemplate>
                                        <!--tab text starts-->
                                        <div class="tabTxtBox">
                                         <asp:HiddenField ID="judgeNames" Value='<%# Eval("judgeNames") %>' runat="server"  ClientIDMode="Static" />
                                            <div class="tabTxt">
                                                <div class="partyNameLabel" style="width: 160px;" >
                                                    <asp:Label ID="lblPartyName" runat="server" Text='<%#Eval("appellant")%>' ClientIDMode="Static"></asp:Label></div>
                                                <div class="partyNameLabelTxt" style="width: 250px;margin-left:5px;">
                                                    <asp:Label ID="lblCitation" runat="server" Text='<%#Eval("citation")%>' ClientIDMode="Static"></asp:Label>
                                                </div>
                                                <div class="partyNameLabel"  style="width: 250px;margin-left:5px;">
                                                    <asp:Label ID="lblCourt" runat="server" Text='<%#Eval("court")%>' ClientIDMode="Static"></asp:Label>
                                                </div>
                                                <div class="partyNameLabelTxt"  style="width: 150px;margin-left:5px;">
                                                    <asp:Label ID="lblYear" runat="server" Text='<%#Eval("year")%>' ClientIDMode="Static"></asp:Label>
                                                </div>
                                                <div class="cls">
                                                </div>
                                                <p class="tabHeadingNew">
                                                   <asp:HiddenField ID="hdnDocId" Value='<%# Eval("docUid") %>' runat="server" ClientIDMode="Static" />
                                                    <asp:LinkButton ID="lbldisplayContent" runat="server" Text='<%#Eval("title")%>'
                                                        CommandName="NavigateToDoc" Style="text-decoration: none !important; font-size: 21px;
                                                        color: #141414;"></asp:LinkButton>
                                                </p>
                                                <div class="citedBy">
                                                    Cited By:
                                                    <asp:Label ID="lblCitedBy" runat="server" Text='<%#Eval("citedBy")%>'></asp:Label>
                                                    Cases
                                                </div>
                                                <div class="citedBy">
                                                    Doc Type:
                                                    <asp:Label ID="lbldocType" runat="server" Text='<%#Eval("docType")%>'></asp:Label>
                                                </div>
                                                <div class="cls">
                                                </div>
                                                <div class="commentsTag">
                                                    Comments:
                                                    <asp:Label ID="lblcommentCount" runat="server" Text='<%#Eval("commentCount")%>'></asp:Label>
                                                </div>
                                                <div class="commentsTag">
                                                    Share:
                                                    <asp:Label ID="lblshareCount" runat="server" Text='<%#Eval("shareCount")%>'></asp:Label>
                                                </div>
                                                <div class="commentsTag">
                                                    Download:
                                                    <asp:Label ID="lbldownloadCount" runat="server" Text='<%#Eval("downloadCount")%>'></asp:Label>
                                                </div>
                                                <div class="commentsTag">
                                                    Total Tags:
                                                    <asp:Label ID="lbltagCnt" runat="server" Text='<%#Eval("tagCnt")%>'></asp:Label>
                                                </div>
                                            </div>
                                            <div class="tabArrow">
                                             <asp:HiddenField ID="hdnDocIdArrow" Value='<%# Eval("docUid") %>' runat="server" ClientIDMode="Static" />
                                                <asp:ImageButton ID="imgnavigate" runat="server" CommandName="NavigateToDoc" ImageUrl="~/images/mybookmarkarrow.jpg" />
                                            </div>
                                        </div>
                                        <!--tab text ends-->
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                            <p id="pLoadMore" runat="server" align="center">
                                <asp:ImageButton ID="imgLoadMore" runat="server" ClientIDMode="Static" ImageUrl="~/images/loadingIcon.gif"
                                    CssClass="imageLoadmore" OnClick="imgLoadMore_OnClick" Height="100px" Width="100px" style="display:none;" />
                                <asp:Button ID="imgLoadMore1" runat="server" ClientIDMode="Static" Style="display: none;"
                                    CssClass="imageLoadmore" OnClick="imgLoadMore_OnClick" Height="100px" Width="100px" />
                            </p>
                            <p align="center">
                                <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server"
                                    ClientIDMode="Static" ForeColor="Red" Visible="false"></asp:Label>
                            </p>
                            <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
                        </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="cls hgt">
                    </div>
                    <br />
                </div>
                 <div class="TooltipDetailsPop" id="eventPop" clientidmode="Static">
                        </div>
            </div>
            <asp:HiddenField ID="CiYear" runat="server" ClientIDMode="Static" Value="Year" />
            <asp:HiddenField ID="CiCourt" runat="server" ClientIDMode="Static" Value="Court" />
            <asp:HiddenField ID="CiReport" runat="server" ClientIDMode="Static" Value="Reporter Name" />
            <asp:HiddenField ID="CiVolume" runat="server" ClientIDMode="Static" Value="Volume" />
            <asp:HiddenField ID="CiPage" runat="server" ClientIDMode="Static" Value="Page No." />
            <!--left verticle search list ends-->
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('#ddlSelect').val() == 'Select Search Type') {
                $('#txtResearch').val('');
                $('#txtResearch').attr("placeholder", "Please select 'Search Type' to start searching");
                $('#divCitationSearch').css('display', 'none');
                $('#divFreeSkorlTargetSearch').css('display', 'none');
                $('#pAdvSearch').css('display', 'none');
            }
            if ($('#ddlSelect').val() == 'C') {
                $('#divMainSearch').css('display', 'none');
                $('#divFreeSkorlTargetSearch').css('display', 'none');
                $('#divCitationSearch').css('display', 'block');
            }
            if ($('#ddlSelect').val() == 'T') {
                $('#divMainSearch').css('display', 'none');
                $('#divCitationSearch').css('display', 'none');
                $('#lbljudgename').css('display', 'none');
                $('#divFreeSkorlTargetSearch').css('margin-top', '18px');
                $('#imgBtntarget').css('display', 'block');
            }
            if ($('#ddlSelect').val() == 'S') {
                $('#divCitationSearch').css('display', 'none');
            }
            $("#ddlSelect").change(function () {
                if ($('#ddlSelect').val() == 'Select Search Type') {
                    $('#txtResearch').val('');
                    $('#txtResearch').attr("placeholder", "Please select 'Search Type' to start searching");
                    $('#divCitationSearch').css('display', 'none');
                    $('#divFreeSkorlTargetSearch').css('display', 'none');
                    $('#pAdvSearch').css('display', 'none');
                } else if ($('#ddlSelect').val() == 'C') {
                    $('#hdnEnterKeyword').val('');
                    $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
                    $('#imgBtntarget').css('display', 'none');
                    $('#txtResearch').val('');
                    $('#txtResearch').attr("placeholder", "Enter Keywords");
                    $('#divCitationSearch').css('display', 'block');
                    $('#divFreeSkorlTargetSearch').css('display', 'none');
                    $('#pAdvSearch').css('display', 'none');
                    $('#divMainSearch').css('display', 'none');
                    $('#txtJudgesMember').val('').trigger('chosen:updated');
                    $('#hdnMembId').val('');
                    $('#hdnMembIdText').val('');
                    $('#hdnsubject').val('');
                    $('#hdnsubjectText').val('');
                    $('#txtSubjectList').val('').trigger('chosen:updated');
                    $("#ddlYearFT option:first").attr("selected", true);
                    $("#ddlCourtFT option:first").attr("selected", true);
                    $("#ddlYear option:first").attr("selected", true);
                    $("#ddlCourt option:first").attr("selected", true);
                    $("#ddlReporterName option:first").attr("selected", true);
                    $("#ddlVolumns option:first").attr("selected", true);
                    $("#ddlPageNo option:first").attr("selected", true);
                } else if ($('#ddlSelect').val() == 'F') {
                    $('#hdnEnterKeyword').val('');
                    $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
                    $('#imgBtntarget').css('display', 'none');
                    $('#divFreeSkorlTargetSearch').css('display', 'none');
                    $('#txtResearch').css('display', 'block');
                    $('#btnSearch').css('display', 'block');
                    $('#btnSearch').css('margin-top', '-35px');
                    $('#txtResearch').val('');
                    $('#txtResearch').attr("placeholder", "Enter Keywords");
                    $('#divCitationSearch').css('display', 'none');
                    $('#pAdvSearch').css('display', 'block');
                    $('#divMainSearch').css('display', 'block');
                    $('#txtJudgesMember').val('').trigger('chosen:updated');
                    $('#hdnMembId').val('');
                    $('#hdnMembIdText').val('');
                    $('#hdnsubject').val('');
                    $('#hdnsubjectText').val('');
                    $('#txtSubjectList').val('').trigger('chosen:updated');
                    $("#ddlYearFT option:first").attr("selected", true);
                    $("#ddlCourtFT option:first").attr("selected", true);
                    $('#lbljudgename').css('display', 'block');
                    $('#divFreeSkorlTargetSearch').css('margin-top', '0px');
                    $("#ddlYear option:first").attr("selected", true);
                    $("#ddlCourt option:first").attr("selected", true);
                    $("#ddlReporterName option:first").attr("selected", true);
                    $("#ddlVolumns option:first").attr("selected", true);
                    $("#ddlPageNo option:first").attr("selected", true);
                    $('#anchorAdvnceSearch').text('Advanced Search');
                } else if ($('#ddlSelect').val() == 'S') {
                    $('#hdnEnterKeyword').val('');
                    $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
                    $('#imgBtntarget').css('display', 'none');
                    $('#divFreeSkorlTargetSearch').css('display', 'none');
                    $('#txtResearch').val('');
                    $('#txtResearch').attr("placeholder", "Enter Keywords");
                    $('#divCitationSearch').css('display', 'none');
                    $('#pAdvSearch').css('display', 'block');
                    $('#divMainSearch').css('display', 'block');
                    $('#txtJudgesMember').val('').trigger('chosen:updated');
                    $('#hdnMembId').val('');
                    $('#hdnMembIdText').val('');
                    $('#hdnsubject').val('');
                    $('#hdnsubjectText').val('');
                    $('#txtSubjectList').val('').trigger('chosen:updated');
                    $("#ddlYearFT option:first").attr("selected", true);
                    $("#ddlCourtFT option:first").attr("selected", true);
                    $('#lbljudgename').css('display', 'block');
                    $('#lbljudgename').css('margin-left', '7px');
                    $('#divFreeSkorlTargetSearch').css('margin-top', '0px');
                    $("#ddlYear option:first").attr("selected", true);
                    $("#ddlCourt option:first").attr("selected", true);
                    $("#ddlReporterName option:first").attr("selected", true);
                    $("#ddlVolumns option:first").attr("selected", true);
                    $("#ddlPageNo option:first").attr("selected", true);
                    $('#anchorAdvnceSearch').text('Advanced Search');
                } else if ($('#ddlSelect').val() == 'T') {
                    $('#hdnEnterKeyword').val('');
                    $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
                    $('#imgBtntarget').css('display', 'block');
                    $('#divFreeSkorlTargetSearch').css('display', 'block');
                    $('#txtResearch').val('');
                    $('#txtResearch').attr("placeholder", "Enter Keywords");
                    $('#divCitationSearch').css('display', 'none');
                    $('#pAdvSearch').css('display', 'block');
                    $('#divMainSearch').css('display', 'none');
                    $('#hdnsubject').val('');
                    $('#hdnsubjectText').val('');
                    $('#txtSubjectList').val('').trigger('chosen:updated');
                    $('#txtJudgesMember').val('').trigger('chosen:updated');
                    $('#hdnMembId').val('');
                    $('#hdnMembIdText').val('');
                    $("#ddlYearFT option:first").attr("selected", true);
                    $("#ddlCourtFT option:first").attr("selected", true);
                    $('#lbljudgename').css('display', 'none');
                    $('#divFreeSkorlTargetSearch').css('margin-top', '18px');
                    $("#ddlYear option:first").attr("selected", true);
                    $("#ddlCourt option:first").attr("selected", true);
                    $("#ddlReporterName option:first").attr("selected", true);
                    $("#ddlVolumns option:first").attr("selected", true);
                    $("#ddlPageNo option:first").attr("selected", true);
                }
            });
            $('#anchorAdvnceSearch').click(function () {
                if ($('#anchorAdvnceSearch').text() == 'Advanced Search') {
                    $('#anchorAdvnceSearch').text('Hide');
                } else {
                    $('#anchorAdvnceSearch').text('Advanced Search');
                }
                $('#divFreeSkorlTargetSearch').slideToggle('slow');
            });
            $("#ddlYear").change(function () {
                //alert($("#ddlYear").val());
                $("#ddlCourt").prop("disabled", false);
                $("#ddlReporterName").prop("disabled", false)
                $("#ddlVolumns").prop("disabled", false)
                $("#ddlPageNo").prop("disabled", false)
                $("#CiYear").val($("#ddlYear option:selected").text());
                var listCourt = "";
                var listReport = "";
                var YearIDs = $("#ddlYear").val();
                if ($("#ddlYear").val() == 'None')
                    YearIDs = 0;

                $.ajax({
                    type: "POST",
                    url: "Research_SearchResult_S.aspx/GetCourts",
                    data: "{'yearId':" + (YearIDs) + "}",
                    contentType: "application/json; charset=utf-8",
                    global: false,
                    async: false,
                    dataType: "json",
                    success: function (response) {
                        var data = eval('(' + response.d + ')');
                        listCourt = "<option value='0'>Court</option>";
                        listReport = "<option value='0'>Reporter Name</option>";
                        if(YearIDs!=0)
                        for (var i = 0; i < data.length; i++) {
                            listCourt += "<option value='" + data[i].intEqCourtId + "'>" + data[i].strEqCourt + "</option>";
                        }
                        $("#ddlCourt").html(listCourt);
                        $("#ddlReporterName").html(listReport);
                        $(".container-popuprewait").hide();

                        $.ajax({
                            type: "POST",
                            url: "Research_SearchResult_S.aspx/GetRCourts",
                            data: "{'yearId':" + (YearIDs) + "}",
                            contentType: "application/json; charset=utf-8",
                            global: false,
                            async: false,
                            dataType: "json",
                            success: function (response) {
                                var data = eval('(' + response.d + ')');
                                listReport = "<option value='0'>Reporter Name</option>";
                                for (var i = 0; i < data.length; i++) {
                                    listReport += "<option value='" + data[i].intReportId + "'>" + data[i].strReportName + "</option>";
                                }
                                $("#ddlReporterName").html(listReport);
                                $(".container-popuprewait").hide();
                            }
                        });
                    }
                });
            });
            $("#ddlCourt").change(function () {
                var listCourt = "";
                var listReport = "";
                $("#CiCourt").val($("#ddlCourt option:selected").text());
                var YearIDs = $("#ddlYear").val();
                if ($("#ddlYear").val() == 'None')
                    YearIDs = 0;
                $.ajax({
                    type: "POST",
                    url: "Research_SearchResult_S.aspx/GetReports",
                    data: "{'courtId':" + ($("#ddlCourt").val()) + ",'yearId':" + (YearIDs) + "}",
                    contentType: "application/json; charset=utf-8",
                    global: false,
                    async: false,
                    dataType: "json",
                    success: function (response) {
                        var data = eval('(' + response.d + ')');
                        listReport = "<option value='0'>Reporter Name</option>";
                        for (var i = 0; i < data.length; i++) {
                            listReport += "<option value='" + data[i].intReportId + "'>" + data[i].strReportName + "</option>";
                        }
                        $("#ddlReporterName").html(listReport);
                    }
                });
            });
            $("#ddlReporterName").change(function () {
                var listvolume = "";
                var listpage = "";
                var YearIDs = $("#ddlYear").val();
                if ($("#ddlYear").val() == 'None')
                    YearIDs = 0;
                $("#CiReport").val($("#ddlReporterName option:selected").text());
                $.ajax({
                    type: "POST",
                    url: "Research_SearchResult_S.aspx/GetVolumns",
                    data: "{'reportId':" + ($("#ddlReporterName").val()) + ",'yearId':" + (YearIDs) + "}",
                    contentType: "application/json; charset=utf-8",
                    global: false,
                    async: false,
                    dataType: "json",
                    success: function (response) {
                        var data = eval('(' + response.d + ')');
                        //alert(data);
                        listvolume = "<option value='0'>Volume</option>";
                        listpage = "<option value='0'>Page No.</option>";
                        for (var i = 0; i < data.length; i++) {
                            listvolume += "<option value='" + data[i].intVolumeId + "'>" + data[i].strEqVolume + "</option>";
                        }
                        $("#ddlVolumns").html(listvolume);
                        $("#ddlPageNo").html(listpage);
                    }
                });
                $.ajax({
                    type: "POST",
                    url: "Research_SearchResult_S.aspx/GetPVolumns",
                    data: "{'reportId':" + ($("#ddlReporterName").val()) + ",'yearId':" + (YearIDs) + "}",
                    contentType: "application/json; charset=utf-8",
                    global: false,
                    async: false,
                    dataType: "json",
                    success: function (response) {
                        var data = eval('(' + response.d + ')');
                        listpage = "<option value='0'>Page No.</option>";
                        for (var i = 0; i < data.length; i++) {
                            listpage += "<option value='" + data[i].intPageNo + "'>" + data[i].intPageNumber + "</option>";
                        }
                        $("#ddlPageNo").html(listpage);
                    }
                });
            });
            $("#ddlVolumns").change(function () {
                var listvolume = "";
                var listpage = "";
                var YearIDs = $("#ddlYear").val();
                if ($("#ddlYear").val() == 'None')
                    YearIDs = 0;
                $("#CiVolume").val($("#ddlVolumns option:selected").text());
                $.ajax({
                    type: "POST",
                    url: "Research_SearchResult_S.aspx/GetYears",
                    data: "{'volumeId':" + ($("#ddlVolumns").val()) + ",'yearId':" + (YearIDs) + "}",
                    contentType: "application/json; charset=utf-8",
                    global: false,
                    async: false,
                    dataType: "json",
                    success: function (response) {
                        var data = eval('(' + response.d + ')');
                        listpage = "<option value='0'>Page No.</option>";
                        for (var i = 0; i < data.length; i++) {
                            listpage += "<option value='" + data[i].intPageNo + "'>" + data[i].intPageNumber + "</option>";
                        }
                        $("#ddlPageNo").html(listpage);
                    }
                });
            });
            $("#ddlPageNo").change(function () {
                $("#CiPage").val($("#ddlPageNo option:selected").text());

            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                if ($('#ddlSelect').val() == 'Select Search Type') {
                    $('#txtResearch').val('');
                    $('#txtResearch').attr("placeholder", "Please select 'Search Type' to start searching");
                    $('#divCitationSearch').css('display', 'none');
                    $('#divFreeSkorlTargetSearch').css('display', 'none');
                    $('#pAdvSearch').css('display', 'none');
                }
                if ($('#ddlSelect').val() == 'C') {
                    $('#divMainSearch').css('display', 'none');
                    $('#divFreeSkorlTargetSearch').css('display', 'none');
                    $('#divCitationSearch').css('display', 'block');
                }
                if ($('#ddlSelect').val() == 'T') {
                    $('#divMainSearch').css('display', 'none');
                    $('#divCitationSearch').css('display', 'none');
                    $('#lbljudgename').css('display', 'none');
                    $('#divFreeSkorlTargetSearch').css('margin-top', '18px');
                    $('#imgBtntarget').css('display', 'block');
                }
                if ($('#ddlSelect').val() == 'S') {
                    $('#divCitationSearch').css('display', 'none');
                }
                $("#ddlSelect").change(function () {
                    if ($('#ddlSelect').val() == 'Select Search Type') {
                        $('#txtResearch').val('');
                        $('#txtResearch').attr("placeholder", "Please select 'Search Type' to start searching");
                        $('#divCitationSearch').css('display', 'none');
                        $('#divFreeSkorlTargetSearch').css('display', 'none');
                        $('#pAdvSearch').css('display', 'none');
                    } else if ($('#ddlSelect').val() == 'C') {
                        $('#hdnEnterKeyword').val('');
                        $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
                        $('#imgBtntarget').css('display', 'none');
                        $('#txtResearch').val('');
                        $('#txtResearch').attr("placeholder", "Enter Keywords");
                        $('#divCitationSearch').css('display', 'block');
                        $('#divFreeSkorlTargetSearch').css('display', 'none');
                        $('#pAdvSearch').css('display', 'none');
                        $('#divMainSearch').css('display', 'none');
                        $('#txtJudgesMember').val('').trigger('chosen:updated');
                        $('#hdnMembId').val('');
                        $('#hdnMembIdText').val('');
                        $('#hdnsubject').val('');
                        $('#hdnsubjectText').val('');
                        $('#txtSubjectList').val('').trigger('chosen:updated');
                        $("#ddlYearFT option:first").attr("selected", true);
                        $("#ddlCourtFT option:first").attr("selected", true);
                        $("#ddlYear option:first").attr("selected", true);
                        $("#ddlCourt option:first").attr("selected", true);
                        $("#ddlReporterName option:first").attr("selected", true);
                        $("#ddlVolumns option:first").attr("selected", true);
                        $("#ddlPageNo option:first").attr("selected", true);
                    } else if ($('#ddlSelect').val() == 'F') {
                        $('#hdnEnterKeyword').val('');
                        $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
                        $("#ddlCourt").prop("disabled", true)
                        $("#ddlReporterName").prop("disabled", true)
                        $("#ddlVolumns").prop("disabled", true)
                        $("#ddlPageNo").prop("disabled", true)
                        $('#imgBtntarget').css('display', 'none');
                        $('#divFreeSkorlTargetSearch').css('display', 'none');
                        $('#txtResearch').css('display', 'block');
                        $('#btnSearch').css('display', 'block');
                        $('#btnSearch').css('margin-top', '-35px');
                        $('#txtResearch').val('');
                        $('#txtResearch').attr("placeholder", "Enter Keywords");
                        $('#divCitationSearch').css('display', 'none');
                        $('#pAdvSearch').css('display', 'block');
                        $('#divMainSearch').css('display', 'block');
                        $('#txtJudgesMember').val('').trigger('chosen:updated');
                        $('#hdnMembId').val('');
                        $('#hdnMembIdText').val('');
                        $('#hdnsubject').val('');
                        $('#hdnsubjectText').val('');
                        $('#txtSubjectList').val('').trigger('chosen:updated');
                        $("#ddlYearFT option:first").attr("selected", true);
                        $("#ddlCourtFT option:first").attr("selected", true);
                        $('#lbljudgename').css('display', 'block');
                        $('#divFreeSkorlTargetSearch').css('margin-top', '0px');
                        $("#ddlYear option:first").attr("selected", true);
                        $("#ddlCourt option:first").attr("selected", true);
                        $("#ddlReporterName option:first").attr("selected", true);
                        $("#ddlVolumns option:first").attr("selected", true);
                        $("#ddlPageNo option:first").attr("selected", true);
                        $('#anchorAdvnceSearch').text('Advanced Search');
                    } else if ($('#ddlSelect').val() == 'S') {
                        $('#hdnEnterKeyword').val('');
                        $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
                        $("#ddlCourt").prop("disabled", true)
                        $("#ddlReporterName").prop("disabled", true)
                        $("#ddlVolumns").prop("disabled", true)
                        $("#ddlPageNo").prop("disabled", true)
                        $('#imgBtntarget').css('display', 'none');
                        $('#divFreeSkorlTargetSearch').css('display', 'none');
                        $('#txtResearch').val('');
                        $('#txtResearch').attr("placeholder", "Enter Keywords");
                        $('#divCitationSearch').css('display', 'none');
                        $('#pAdvSearch').css('display', 'block');
                        $('#divMainSearch').css('display', 'block');
                        $('#txtJudgesMember').val('').trigger('chosen:updated');
                        $('#hdnMembId').val('');
                        $('#hdnMembIdText').val('');
                        $('#hdnsubject').val('');
                        $('#hdnsubjectText').val('');
                        $('#txtSubjectList').val('').trigger('chosen:updated');
                        $("#ddlYearFT option:first").attr("selected", true);
                        $("#ddlCourtFT option:first").attr("selected", true);
                        $('#lbljudgename').css('display', 'block');
                        $('#lbljudgename').css('margin-left', '7px');
                        $('#divFreeSkorlTargetSearch').css('margin-top', '0px');
                        $("#ddlYear option:first").attr("selected", true);
                        $("#ddlCourt option:first").attr("selected", true);
                        $("#ddlReporterName option:first").attr("selected", true);
                        $("#ddlVolumns option:first").attr("selected", true);
                        $("#ddlPageNo option:first").attr("selected", true);
                        $('#anchorAdvnceSearch').text('Advanced Search');
                    } else if ($('#ddlSelect').val() == 'T') {
                        $('#hdnEnterKeyword').val('');
                        $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
                        $("#ddlCourt").prop("disabled", true)
                        $("#ddlReporterName").prop("disabled", true)
                        $("#ddlVolumns").prop("disabled", true)
                        $("#ddlPageNo").prop("disabled", true)
                        $('#imgBtntarget').css('display', 'block');
                        $('#divFreeSkorlTargetSearch').css('display', 'block');
                        $('#txtResearch').val('');
                        $('#txtResearch').attr("placeholder", "Enter Keywords");
                        $('#divCitationSearch').css('display', 'none');
                        $('#pAdvSearch').css('display', 'block');
                        $('#divMainSearch').css('display', 'none');
                        $('#hdnsubject').val('');
                        $('#hdnsubjectText').val('');
                        $('#txtSubjectList').val('').trigger('chosen:updated');
                        $('#txtJudgesMember').val('').trigger('chosen:updated');
                        $('#hdnMembId').val('');
                        $('#hdnMembIdText').val('');
                        $("#ddlYearFT option:first").attr("selected", true);
                        $("#ddlCourtFT option:first").attr("selected", true);
                        $('#lbljudgename').css('display', 'none');
                        $('#divFreeSkorlTargetSearch').css('margin-top', '18px');
                        $("#ddlYear option:first").attr("selected", true);
                        $("#ddlCourt option:first").attr("selected", true);
                        $("#ddlReporterName option:first").attr("selected", true);
                        $("#ddlVolumns option:first").attr("selected", true);
                        $("#ddlPageNo option:first").attr("selected", true);
                    }
                });

            });
        });
    </script>
    <script type="text/javascript" >
        function DefaultCall() {
            $('#divCitationSearch').css('display', 'none');
            $('#divFreeSkorlTargetSearch').css('display', 'none');
        }
        function citationCall() {
            $('#imgBtntarget').css('display', 'none');
            $('#txtResearch').val('');
            $('#txtResearch').attr("placeholder", "Enter Keywords");
            $('#divCitationSearch').css('display', 'block');
            $('#divFreeSkorlTargetSearch').css('display', 'none');
            $('#pAdvSearch').css('display', 'none');
            $('#divMainSearch').css('display', 'none');
            $('#txtJudgesMember').val('').trigger('chosen:updated');
            $('#hdnMembId').val('');
            $('#hdnMembIdText').val('');
            $('#hdnsubject').val('');
            $('#hdnsubjectText').val('');
            $('#txtSubjectList').val('').trigger('chosen:updated');
            $("#ddlYearFT option:first").attr("selected", true);
            $("#ddlCourtFT option:first").attr("selected", true);
        }
        function freeTextCall() {
            $('#imgBtntarget').css('display', 'none');
            $('#divFreeSkorlTargetSearch').css('display', 'none');
            $('#txtResearch').val('');
            $('#txtResearch').attr("placeholder", "Enter Keywords");
            $('#divCitationSearch').css('display', 'none');
            $('#pAdvSearch').css('display', 'block');
            $('#divMainSearch').css('display', 'block');
            $('#txtJudgesMember').val('').trigger('chosen:updated');
            $('#hdnMembId').val('');
            $('#hdnMembIdText').val('');
            $('#hdnsubject').val('');
            $('#hdnsubjectText').val('');
            $('#txtSubjectList').val('').trigger('chosen:updated');
            $("#ddlYearFT option:first").attr("selected", true);
            $("#ddlCourtFT option:first").attr("selected", true);
            $('#lbljudgename').css('display', 'block');
            $('#divFreeSkorlTargetSearch').css('margin-top', '0px');
            $("#ddlYear option:first").attr("selected", true);
            $("#ddlCourt option:first").attr("selected", true);
            $("#ddlReporterName option:first").attr("selected", true);
            $("#ddlVolumns option:first").attr("selected", true);
            $("#ddlPageNo option:first").attr("selected", true);
            $('#anchorAdvnceSearch').text('Advanced Search');
        }

        function SkorkelCall() {
            $('#imgBtntarget').css('display', 'none');
            $('#divFreeSkorlTargetSearch').css('display', 'none');
            $('#txtResearch').val('');
            $('#txtResearch').attr("placeholder", "Enter Keywords");
            $('#divCitationSearch').css('display', 'none');
            $('#pAdvSearch').css('display', 'block');
            $('#divMainSearch').css('display', 'block');
            $('#txtJudgesMember').val('').trigger('chosen:updated');
            $('#hdnMembId').val('');
            $('#hdnMembIdText').val('');
            $('#hdnsubject').val('');
            $('#hdnsubjectText').val('');
            $('#txtSubjectList').val('').trigger('chosen:updated');
            $("#ddlYearFT option:first").attr("selected", true);
            $("#ddlCourtFT option:first").attr("selected", true);
            $('#lbljudgename').css('display', 'block');
            $('#divFreeSkorlTargetSearch').css('margin-top', '0px');
            $("#ddlYear option:first").attr("selected", true);
            $("#ddlCourt option:first").attr("selected", true);
            $("#ddlReporterName option:first").attr("selected", true);
            $("#ddlVolumns option:first").attr("selected", true);
            $("#ddlPageNo option:first").attr("selected", true);
            $('#anchorAdvnceSearch').text('Advanced Search');
        }
        function targetCall() {
            $('#imgBtntarget').css('display', 'block');
            $('#divFreeSkorlTargetSearch').css('display', 'block');
            $('#txtResearch').val('');
            $('#txtResearch').attr("placeholder", "Enter Keywords");
            $('#divCitationSearch').css('display', 'none');
            $('#pAdvSearch').css('display', 'block');
            $('#divMainSearch').css('display', 'none');
            $('#lbljudgename').css('display', 'none');
            $("#ddlYear option:first").attr("selected", true);
            $("#ddlCourt option:first").attr("selected", true);
            $("#ddlReporterName option:first").attr("selected", true);
            $("#ddlVolumns option:first").attr("selected", true);
            $("#ddlPageNo option:first").attr("selected", true);
        }
    </script>
    <script type="text/javascript">
        ddsmoothmenu.init({
            mainmenuid: "smoothmenu1", //menu DIV id
            orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
            classname: 'ddsmoothmenu', //class added to menu's outer DIV
            //customtheme: ["#1c5a80", "#18374a"],
            contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
        })          
    </script>
    <script type="text/javascript">
        function getAllMemberValue(ctrlId) {
            $('#divFreeSkorlTargetSearch').find('label.error').remove();
            var control = document.getElementById(ctrlId);
            var strSelTexts = '';
            var strTexts = '';
            var cnt = 0;
            for (var i = 0; i < control.length; i++) {
                if (control.options[i].selected) {
                    if (cnt == 0) {
                        strSelTexts += control.options[i].value;
                        strTexts += control.options[i].text;
                    }
                    else {
                        strSelTexts += ',' + control.options[i].value;
                        strTexts += ',' + control.options[i].text;
                    }
                    cnt++;
                }
            }
            $('#hdnMembId').val(strSelTexts);
            $('#hdnMembIdText').val(strTexts);
        }
        function getAllSubjectValue(ctrlId) {
            $('#MicroTag').find('label.error').remove();
            var control = document.getElementById(ctrlId);
            var strSelTexts = '';
            var strTexts = '';
            var cnt = 0;
            for (var i = 0; i < control.length; i++) {
                if (control.options[i].selected) {
                    if (cnt == 0) {
                        strSelTexts += control.options[i].value;
                        strTexts += control.options[i].text;
                    }
                    else {
                        strSelTexts += ',' + control.options[i].value;
                        strTexts += ',' + control.options[i].text;
                    }
                    cnt++;
                }
            }
            $('#hdnsubject').val(strSelTexts);
            $('#hdnsubjectText').val(strTexts);
        }
        function ClearAllFields() {
            $('#imgBtntarget').css('display', 'none');
            $('#divFreeSkorlTargetSearch').css('display', 'none');
            $('#txtResearch').val('');
            $('#txtResearch').attr("placeholder", "Enter Keywords");
            $('#divCitationSearch').css('display', 'none');
            $('#pAdvSearch').css('display', 'block');
            $('#divMainSearch').css('display', 'block');
            $('#txtJudgesMember').val('').trigger('chosen:updated');
            $('#hdnMembId').val('');
            $('#hdnMembIdText').val('');
            $('#hdnsubject').val('');
            $('#hdnsubjectText').val('');
            $('#txtSubjectList').val('').trigger('chosen:updated');
            $("#ddlYearFT option:first").attr("selected", true);
            $("#ddlCourtFT option:first").attr("selected", true);
            $('#lbljudgename').css('display', 'none');
            $('#divFreeSkorlTargetSearch').css('margin-top', '0px');
            $("#ddlYear option:first").attr("selected", true);
            $("#ddlCourt option:first").attr("selected", true);
            $("#ddlReporterName option:first").attr("selected", true);
            $("#ddlVolumns option:first").attr("selected", true);
            $("#ddlPageNo option:first").attr("selected", true);
            $('#anchorAdvnceSearch').text('Advanced Search');
        }
    </script>
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
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {
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
         });
    </script>
    <script type="text/javascript">
            function MessClose() {
                document.getElementById("divSavesearch").style.display = 'none';
            }
            function callDisable() {
                $("*").attr("disabled", "disabled");
            }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(window).scroll(function () {
                if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                    document.getElementById('imgLoadMore').style.display = 'block';
                    var v = $("#lblNoMoreRslt").text();
                    var maxCount = $("#hdnMaxcount").val();
                    if (maxCount <= 10) {
                        $("#lblNoMoreRslt").css("display", "none");
                    } else {
                        if (v != "No more results available...") {
                            var elm = '#imgLoadMore1';
                            $(elm).click();
                        } else {
                            document.getElementById('imgLoadMore').style.display = 'none';
                        }
                    }
                }
                if (maxCount == '') {
                    document.getElementById('imgLoadMore').style.display = 'none';
                }
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(window).scroll(function () {
                    if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                        document.getElementById('imgLoadMore').style.display = 'block';
                        var v = $("#lblNoMoreRslt").text();
                        var maxCount = $("#hdnMaxcount").val();
                        if (maxCount <= 10) {
                            $("#lblNoMoreRslt").css("display", "none");
                        } else {
                            if (v != "No more results available...") {
                                var elm = '#imgLoadMore1';
                                $(elm).click();
                            } else {
                                document.getElementById('imgLoadMore').style.display = 'none';
                            }
                        }
                    }
                    if (maxCount == '') {
                        document.getElementById('imgLoadMore').style.display = 'none';
                    }
                });
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                ddsmoothmenu.init({
                    mainmenuid: "smoothmenu1", //menu DIV id
                    orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
                    classname: 'ddsmoothmenu', //class added to menu's outer DIV
                    contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
                })
                var ir = 0;
                $(".endronsecssimg").mouseover(function () {
                    $(this).parent().children(".endronsecss").css('display', 'block');
                });

                $(".endronsecss").mouseout(function () {
                    $(this).parent().children(".endronsecss").css('display', 'none');
                });
                $(".scroll").click(function (event) {
                    $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 500);
                });

                $(".showMouseOver").mouseover(function () {
                    $(this).parent().children(".imageRolloverBg").css('display', 'block');
                });
                $(".showMouseOver").mouseout(function () {
                    $(this).parent().children(".imageRolloverBg").css('display', 'none');
                });

                $(".photoIcon").mouseover(function () {
                    $("#imgCamera").css('display', 'block');
                });
                $(".photoIcon").mouseout(function () {
                    $("#imgCamera").css('display', 'none');
                });
                $("#FileUplogo").change(function (event) {
                    var tmppath = URL.createObjectURL(event.target.files[0]);
                    var ext = $('#FileUplogo').val().split('.').pop().toLowerCase();
                    if (ext != '') {
                        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'bmp']) == -1) {
                            if (ext == 'pdf' || ext == 'xlxs' || ext == 'txt' || ext == 'doc' || ext == 'docx' || ext == 'xlx' || ext == 'odt') {
                                alert('Please select image or video.');
                            } else {
                                $("#Mediavideo").fadeIn("fast").attr('src', URL.createObjectURL(event.target.files[0]));
                                $("#imgselect").css("display", "none");
                                $("#imgBtnDelete").css("display", "block");
                            }
                        } else {
                            $("#imgselect").fadeIn("fast").attr('src', URL.createObjectURL(event.target.files[0]));
                            $("#Mediavideo").css("display", "none");
                            $("#imgBtnDelete").css("display", "block");
                        }
                    } else {
                        $("#imgselect").css("display", "none");
                        $("#Mediavideo").css("display", "none");
                        $("#imgBtnDelete").css("display", "none");
                    }
                });
                $("#uploadDoc").change(function (event) {
                    var tmppath = URL.createObjectURL(event.target.files[0]);
                    $("#lblfilenamee").text($("#uploadDoc").val().split('\\').pop());
                });
                $("#imgGroup").click(function () {
                    $("#divfrdgrp").removeClass("fGroupBox frd").addClass("fGroupBox grp");
                    $("#divgroupSection").show();
                    $("#divFriendSection").hide();
                    return false;
                });
                $("#imgFriend").click(function () {
                    $("#divfrdgrp").removeClass("fGroupBox grp").addClass("fGroupBox frd");
                    $("#divFriendSection").show();
                    $("#divgroupSection").hide();
                    return false;
                });
            });
        });
    </script> 
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSearch').click(function () {
                $("#divPopp").show();
            });
            $('#ImageButton1').click(function () {
                $("#divPopp").show();
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#btnSearch').click(function () {
                    $("#divPopp").show();
                });
                $('#ImageButton1').click(function () {
                    $("#divPopp").show();
                });
            });
        });
        function functionCall() {
            $(".divPopp").show();
        }
        function functionHideCall() {
            $("#divPopp").hide();
        }
    </script>
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
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
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
        });
    </script>
    <script type="text/javascript">
        function getAllMemberValue(ctrlId) {
            $('#divFreeSkorlTargetSearch').find('label.error').remove();
            var control = document.getElementById(ctrlId);
            var strSelTexts = '';
            var strTexts = '';
            var cnt = 0;
            for (var i = 0; i < control.length; i++) {
                if (control.options[i].selected) {
                    if (cnt == 0) {
                        strSelTexts += control.options[i].value;
                        strTexts += control.options[i].text;
                    }
                    else {
                        strSelTexts += ',' + control.options[i].value;
                        strTexts += ',' + control.options[i].text;
                    }
                    cnt++;
                }
            }
            $('#hdnMembId').val(strSelTexts);
            $('#hdnMembIdText').val(strTexts);
        }
        function getAllSubjectValue(ctrlId) {
            $('#MicroTag').find('label.error').remove();
            var control = document.getElementById(ctrlId);
            var strSelTexts = '';
            var strTexts = '';
            var cnt = 0;
            for (var i = 0; i < control.length; i++) {
                if (control.options[i].selected) {
                    if (cnt == 0) {
                        strSelTexts += control.options[i].value;
                        strTexts += control.options[i].text;
                    }
                    else {
                        strSelTexts += ',' + control.options[i].value;
                        strTexts += ',' + control.options[i].text;
                    }
                    cnt++;
                }
            }
            $('#hdnsubject').val(strSelTexts);
            $('#hdnsubjectText').val(strTexts);
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var ID = "#" + $("#hdnTabId").val();
            $(ID).focus();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                var ID = "#" + $("#hdnTabId").val();
                $(ID).focus();
            });
        });
        function ShowLoading(id) {
            location.href = '#' + id;
        }
        function btnSaveSearch_onclick() {
            document.getElementById("divSavesearch").style.display = 'block';
            return false;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtResearch").keydown(function (e) {
                if (e.keyCode == 13) {
                    $('#btnSearch').click();
                    e.preventDefault();
                }
            });
        });
        function HidedivFT() {
            $('#anchorAdvnceSearch').text('Advanced Search');
            $('#divFreeSkorlTargetSearch').css('display', 'none');
        }
        function CallSearchSave() {
            $('#lnkPopupOK').css("box-shadow", "0px 0px 5px #00B7E5");
            setTimeout(
                function () {
                    $('#lnkPopupOK').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
        }
    </script>
     <script type="text/javascript">
         $(function () {
             $("#lblEnterKeyword").mouseover(function () {
                 var table;
                 if ($('#ddlSelect').val() == 'F') {
                     table = '<div> <label id="lblTitle">'+$('#hdnEnterKeyword').val() +' </label></div>';
                 } else if ($('#ddlSelect').val() == 'C') {
                     table = '<div> <label id="lblTitle">' + $('#hdnEnterKeyword').val() + ' </label></div>';
                 } else if ($('#ddlSelect').val() == 'S') {
                     table = '<div> <label id="lblTitle">' + $('#hdnEnterKeyword').val() + ' </label></div>';
                 } else if ($('#ddlSelect').val() == 'T') {
                     table = '<div> <label id="lblTitle">' + $('#hdnEnterKeyword').val() + ' </label></div>';
                 }
                 $('#eventPop').html(table);
                 document.getElementById('eventPop').style.display = "block";
             });
             $('#lblEnterKeyword').mouseout(function () {
                 document.getElementById('eventPop').style.display = "none";
             });
         });
         window.onmousemove = function (e) {
             $("#eventPop").offset({ right: e.pageX, top: e.pageY + 15 });
         }
      </script>
      <script type="text/javascript">
          $(document).ready(function () {
              $('.tabHeadingNew').click(function () {
                  window.location = "Research-Case%20Details.aspx?CTid=1&cId="+$(this).children('#hdnDocId').val();
              });
              $('.tabArrow').click(function () {
                  window.location = "Research-Case%20Details.aspx?CTid=1&cId=" + $(this).children('#hdnDocIdArrow').val();
              });

              var prm = Sys.WebForms.PageRequestManager.getInstance();
              prm.add_endRequest(function () {
                  $('.tabHeadingNew').click(function () {
                      window.location = "Research-Case%20Details.aspx?CTid=1&cId=" + $(this).children('#hdnDocId').val();
                  });
                  $('.tabArrow').click(function () {
                      window.location = "Research-Case%20Details.aspx?CTid=1&cId=" + $(this).children('#hdnDocIdArrow').val();
                  });
              });

          });

        </script>

</asp:Content>
