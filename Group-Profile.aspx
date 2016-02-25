<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Group-Profile.aspx.cs" MasterPageFile="~/Main.master"
    Inherits="Group_Profile" %>

<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("js/jquery.jcarousel.min.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("js/jquery.custom-scrollbar.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
    <style type="text/css">
        .categoryBox ul li
        {
            min-width: 30px;
            text-align: center;
            padding: 0 8px;
            margin-right: 10px;
        }
        .categoryBox ul li a, .categoryBox ul li a:hover
        {
            text-decoration: none !important;
        }
        .categoryBox ul li:hover
        {
            background: none repeat scroll 0 0 #00B7BD !important;
            color: #FFFFFF;
            text-decoration: none !important;
        }
        .categoryBox ul li:hover a, .categoryBox ul li a:hover, .categoryBox ul li.categoryBox a
        {
            color: #FFFFFF !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container" style="padding-top: 40px">
        <div class="cls">
        </div>
        <div class="innerDocumentContainerGroup">
            <div id="divGroupSucces" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                float: left; width: 500px; padding-top: 100px; position: relative; margin: -200px 0 0 50px;
                z-index: 100; display: none;" clientidmode="Static">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                    <tr>
                        <td>
                            <strong>&nbsp;&nbsp;
                                <asp:Label ID="lblSuccMess" runat="server" Text="Updated Succesfully." Font-Size="Small"></asp:Label>
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
                                        <a clientidmode="Static" style="float: left; text-align: center; text-decoration: none;
                                            width: 82px; padding-top: 5px; color: #000; cursor: pointer;" onclick="javascript:MesProfileClose();">
                                            Close </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <!--groups top box starts-->
            <Group:GroupDetails ID="grpDetails" runat="server" />
            <!--groups top box ends-->
            <!--left box starts-->
            <div id="divheight" runat="server">
                <div id="divSecondWall" runat="server" clientidmode="Static" style="width: 100%">
                    <div class="innerGroupBoxnew" style="margin-top: 5px;">
                        <div class="innerContainerLeft" style="width: 900px">
                            <div class="tagContainer" style="width: 900px">
                                <div class="forumsTabs" style="margin: -21px 0 0 0; border: none;">
                                    <ul style="margin-left: 20px; padding: 0px;">
                                        <li style="margin-right: 54px;">
                                            <asp:LinkButton ID="lnkProfile" runat="server" Text="Profile" ClientIDMode="Static"
                                                class="forumstabAcitve" OnClick="lnkProfile_Click"></asp:LinkButton></li>
                                        <li id="DivHome" runat="server" style="display: none; margin-right: 54px;">
                                            <div>
                                                <asp:LinkButton ID="lnkHome" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                                            </div>
                                        </li>
                                        <li id="DivForumTab" runat="server" clientidmode="Static" style="display: none; margin-right: 54px;">
                                            <div>
                                                <asp:LinkButton ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static"
                                                    OnClick="lnkForumTab_Click"></asp:LinkButton></div>
                                        </li>
                                        <li id="DivUploadTab" runat="server" clientidmode="Static" style="display: none;
                                            margin-right: 54px;">
                                            <div>
                                                <asp:LinkButton ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static"
                                                    OnClick="lnkUploadTab_Click"></asp:LinkButton>
                                            </div>
                                        </li>
                                        <li id="DivPollTab" runat="server" clientidmode="Static" style="display: none; margin-right: 54px;">
                                            <div>
                                                <asp:LinkButton ID="lnkPollTab" runat="server" Text="Polls" ClientIDMode="Static"
                                                    OnClick="lnkPollTab_Click"></asp:LinkButton>
                                            </div>
                                        </li>
                                        <li id="DivEventTab" runat="server" clientidmode="Static" style="display: none; margin-right: 54px;">
                                            <div>
                                                <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                                                    OnClick="lnkEventTab_Click"></asp:LinkButton></div>
                                        </li>
                                        <li id="DivMemberTab" runat="server" clientidmode="Static" style="display: none;
                                            margin-right: 54px;">
                                            <div>
                                                <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                                    OnClick="lnkEventMemberTab_Click"></asp:LinkButton></div>
                                        </li>
                                    </ul>
                                    <div class="cls">
                                    </div>
                                </div>
                            </div>
                            <div style="border: #e7e7e7 solid 1px; height: 99%; margin-top: 33px; width: 938px;">
                                <div class="cls">
                                    <asp:Label ID="lblGroupCategory" Style="text-align: left;" runat="server" Text=""></asp:Label>
                                    <asp:LinkButton ID="lnkEdit" CssClass="educationYear" ClientIDMode="Static" Style="text-decoration: none;
                                        margin-left: 94%; margin-right: 0px; padding-right: 12px; margin-top: 0px; display: none;"
                                        Text="Edit" OnClick="lnkEdit_Click" runat="server">
                                    </asp:LinkButton>
                                </div>
                                <div class="cls">
                                </div>
                                <div id="divMainProfile" runat="server" style="margin: 10px 0px 0px 50px;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 10%;">
                                                <span style="color: Black; font-size: 16px;">Title</span>
                                            </td>
                                            <td style="width: 2%;">
                                                :
                                            </td>
                                            <td>
                                                <p class="wordbreakspr">
                                                    <asp:Label ID="lblTitle" runat="server" ClientIDMode="Static" Text="" Style="font-size: 16px;"></asp:Label></p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span style="color: Black; font-size: 16px;">Context</span>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <div class="categoryBox" style="width: 650px;">
                                                    <div class="categoryTxt" style="margin: 0% 0% 0% 0%; width: 460px;">
                                                        <ul>
                                                            <asp:ListView ID="LstProfSubjCategory" runat="server" OnItemDataBound="LstSubjCategory_ItemDataBound"
                                                                GroupItemCount="3" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">
                                                                <LayoutTemplate>
                                                                    <table width="60%">
                                                                        <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <GroupTemplate>
                                                                    <tr>
                                                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                                    </tr>
                                                                </GroupTemplate>
                                                                <ItemTemplate>
                                                                    <li id="SubLi" runat="server">
                                                                        <asp:HiddenField ID="hdnCountSub" runat="server" Value='<%#Eval("CountSub")%>' />
                                                                        <asp:HiddenField ID="hdnSubCatId" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                                        <asp:LinkButton ID="lnkCatName" ForeColor="#646161" Font-Underline="false" runat="server"
                                                                            Text='<%#Eval("strCategoryName")%>' CommandName="Subject Category"></asp:LinkButton>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span style="color: Black; font-size: 16px;">Description </span>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <p class="breakallwords" style="">
                                                    <asp:Label ID="lblDescription" runat="server" ClientIDMode="Static" Style="font-size: 16px;"
                                                        Text="" ForeColor="#ADADAD"></asp:Label>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span style="color: Black; font-size: 16px;">Access</span>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:Label ID="rbProfileJoin" runat="server" Style="font-size: 16px;" ForeColor="#ADADAD"
                                                    ClientIDMode="Static"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <asp:Panel ID="pnlGeneral" runat="server" Visible="true" Style="margin-left: 20px;">
                                    <div id="divProfileTitle" runat="server">
                                        <div class="createForumBox members Mem profileSection">
                                            <asp:TextBox ID="txtTitle" Enabled="False" runat="server" ClientIDMode="Static" Style="font-family: Calibri;"
                                                CssClass="uploadTxtField profile" placeholder="Title/Name"></asp:TextBox>
                                            <asp:HiddenField ID="hdnTitle" runat="server" />
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="font-size: 14px;
                                                margin-left: 21px;" runat="server" ControlToValidate="txtTitle" Display="Dynamic"
                                                ValidationGroup="cg" ErrorMessage="Please enter group name" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <!--search result starts-->
                                            <p class="classifyQ">
                                                Classify Your Profile</p>
                                            <div class="categoryBox" style="width: 650px;">
                                                <asp:UpdatePanel ID="UpdateSub" runat="server">
                                                    <ContentTemplate>
                                                        <div class="categoryTxt" style="padding-left: 20px; width: 460px;">
                                                            <ul>
                                                                <asp:ListView ID="LstSubjCategory" runat="server" OnItemCommand="LstSubjCategory_ItemCommand"
                                                                    OnItemDataBound="LstSubjCategory_ItemDataBound" GroupItemCount="3" GroupPlaceholderID="groupPlaceHolder1"
                                                                    ItemPlaceholderID="itemPlaceHolder1">
                                                                    <LayoutTemplate>
                                                                        <table width="60%">
                                                                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <GroupTemplate>
                                                                        <tr>
                                                                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                                        </tr>
                                                                    </GroupTemplate>
                                                                    <ItemTemplate>
                                                                        <li id="SubLi" runat="server">
                                                                            <asp:HiddenField ID="hdnCountSub" runat="server" Value='<%#Eval("CountSub")%>' />
                                                                            <asp:HiddenField ID="hdnSubCatId" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                                            <asp:LinkButton ID="lnkCatName" ForeColor="#646161" Font-Underline="false" runat="server"
                                                                                Text='<%#Eval("strCategoryName")%>' CommandName="Subject Category"></asp:LinkButton>
                                                                        </li>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </ul>
                                                        </div>
                                                        <div class="cls">
                                                        </div>
                                                        <div class="viewAll">
                                                            <asp:LinkButton ID="lnkViewSubj" Text="Close" Font-Underline="false" runat="server"
                                                                OnClick="lnkViewSubj_Click" Style="padding-right: 215px;"></asp:LinkButton></div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="cls">
                                                <br />
                                            </div>
                                            <textarea rows="10" disabled="disabled" class="uploadDescriptionTxt profile" id="txtPurpose"
                                                cols="0" runat="server" onfocus="if(this.value=='Description') this.value='';"
                                                onblur="if(this.value=='') this.value='Description';" value="Description"></textarea>
                                            <div class="cls">
                                            </div>
                                            <p class="classifyQ">
                                                Access</p>
                                            <div class="chkBox">
                                                <asp:UpdatePanel ID="groupjoin" runat="server">
                                                    <ContentTemplate>
                                                        <span>
                                                            <asp:ImageButton ID="imgAutojoin" runat="server" class="chkEnabled" ImageUrl="~/images/spacer.gif"
                                                                OnClick="imgAutobtn_Click" />
                                                            Auto Join </span><span>
                                                                <asp:ImageButton ID="imgReqjoin" runat="server" class="chkDisabled" ImageUrl="~/images/spacer.gif"
                                                                    OnClick="imgReqjoin_Click" />Request to Join </span>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="cls">
                                            </div>
                                            <div>
                                                <p class="classifyQ">
                                                    Group Profile Image
                                                </p>
                                                <div class="cls">
                                                    <br />
                                                </div>
                                                <div style="margin-left: 25px;">
                                                    <div>
                                                        <div style="margin: 0px 0px 0px 0px">
                                                            <asp:FileUpload ID="FileUpload1" ClientIDMode="Static" runat="server" CompleteBackColor="#FFFFFF" />
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="lblSuccMessage" runat="server"></asp:Label>
                                                        </div>
                                                        <div style="margin-right: 150px; width: 115px; margin-left: -4px; margin-top: 11px;
                                                            background: url(images/profile-border.png) no-repeat scroll 0 0 transparent;
                                                            float: left; padding: 9px 10px;">
                                                            <img id="imgUser" runat="server" clientidmode="Static" height="95" width="80" style="padding-bottom: 10px;"
                                                                alt="" />
                                                            <br />
                                                            <asp:ImageButton ID="ImgRemovePic" ToolTip="Remove Profile Picture." runat="server"
                                                                Visible="true" Style="margin-left: 30px; display: block;" CausesValidation="false"
                                                                ClientIDMode="Static" OnClientClick="removeimages();return false;" ImageUrl="images/Delete.gif" />
                                                        </div>
                                                        <asp:LinkButton ID="uploadimage" runat="server" ClientIDMode="Static" Text="Upload"
                                                            OnClick="fileuplad_onload" CssClass="connect" Style="background: url(images/create-forum.png) no repeat scroll 0 0 transparent;
                                                            color: #FFFFFF; font-family: Calibri; font-size: 18px; margin-left: -178px; display: none;"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="cls">
                                                <br />
                                            </div>
                                            <a name="Membersection"></a>
                                            <div style="margin: 0px 0px 20px 25px;">
                                                <select data-placeholder="Enter members name here" class="chosen-select txtFieldNew"
                                                    id="txtInviteMember" onchange="getAllMemberValue(this.id)" runat="server" clientidmode="Static"
                                                    multiple style="width: 600px; font-family: Calibri; margin-top: -2px;" tabindex="4">
                                                </select>
                                                <asp:RequiredFieldValidator InitialValue="Enter members name here" ValidationGroup="InvMem"
                                                    ClientIDMode="Static" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtInviteMember"
                                                    ErrorMessage="Enter members name" ForeColor="Red" Style="position: absolute;
                                                    padding-left: 0px; padding-top: 0px; font-size: small"></asp:RequiredFieldValidator>
                                                <asp:HiddenField ID="hdnMembId" ClientIDMode="Static" runat="server" />
                                            </div>
                                            <!--search result ends-->
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div id="divSaveCancel" runat="server" style="margin-left: 34%; height: 29px; border: 0px solid #e3e2e2;
                                    clear: both; background-color: White;">
                                    <div style="width: 53%; float: left; margin-top: -20px; background-color: White">
                                        <asp:LinkButton ID="lnkCreateGroup" Text="Update" runat="server" ClientIDMode="Static"
                                            OnClientClick="javascript:CallEditSavegr();" Style="" ValidationGroup="cg" OnClick="btnSaveGroup_Click"
                                            class="vote"></asp:LinkButton>
                                        <asp:LinkButton ID="btnCancelExperience" CommandName="Join" CausesValidation="false"
                                            Style="float: left; text-align: center; text-decoration: none; width: 137px;
                                            padding-top: 15px; margin-left: -8%; color: Black;" runat="server" Text="Cancel"
                                            OnClick="btnCancel_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="cls">
                                <p>
                                    &nbsp;</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnMemberName" runat="server" ClientIDMode="Static" Value="" />
            <asp:HiddenField ID="hdnMemberId" runat="server" ClientIDMode="Static" Value="" />
            <!--box starts-->
        </div>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#FileUpload1").change(function (event) {
                    var tmppath = URL.createObjectURL(event.target.files[0]);
                    var ext = $('#FileUpload1').val().split('.').pop().toLowerCase();
                    if (ext != '') {
                        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'bmp']) == -1) {
                            if (ext == 'pdf' || ext == 'xlxs' || ext == 'txt' || ext == 'doc' || ext == 'docx' || ext == 'xlx' || ext == 'odt') {
                                alert('Please select image.');
                            } else {
                                alert('Please select image.');
                            }
                        } else {
                            $("#imgUser").attr('src', URL.createObjectURL(event.target.files[0]));
                            $("#ImgRemovePic").css("display", "block");
                        }
                    } else {
                        alert('Please select image.');
                    }
                });

            });
            function removeimages() {
                $("#imgUser").attr('src', 'images/groupPhoto.jpg');
                $('#FileUpload1').val('');
                $("#ImgRemovePic").css("display", "none");
                return false;
            }
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                $(".categoryTxt ul li").click(function () {
                    $(this).toggleClass("gray");
                });
                $(".viewAll a").click(function () {
                    $(this).parent().parent().toggleClass("categoryBoxToggle");
                    var abc = this.text;
                    if (abc == "Close") {
                        $(this).text("View all");
                    }
                    if (abc == "View all") {
                        $(this).text("Close");
                    }
                });
            });
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                $(":input[data-watermark]").each(function () {
                    $(this).val($(this).attr("data-watermark"));
                    $(this).bind('focus', function () {
                        if ($(this).val() == $(this).attr("data-watermark")) $(this).val('');
                    });
                    $(this).bind('blur', function () {
                        if ($(this).val() == '') $(this).val($(this).attr("data-watermark"));
                        $(this).css('color', '#a8a8a8');
                    });
                });
            });
        </script>
        <script type="text/javascript">
            function ShowLoading(id) {
                location.href = '#' + id;
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
        </script>
        <script type="text/javascript">
            function MesProfileClose() {
                document.getElementById("divGroupSucces").style.display = 'none';
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
            $(document).ready(function () {
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
            });
        </script>
        <script type="text/javascript">
            function getAllMemberValue(ctrlId) {
                $('#tdDepartments').find('label.error').remove();
                var control = document.getElementById(ctrlId);
                var strSelTexts = '';
                var cnt = 0;
                for (var i = 0; i < control.length; i++) {
                    if (control.options[i].selected) {
                        if (cnt == 0) {
                            strSelTexts += control.options[i].value;
                        }
                        else {
                            strSelTexts += ',' + control.options[i].value;
                        }
                        cnt++;
                    }
                }
                $('#hdnMembId').val(strSelTexts);
            }
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
               var ID = "#" + $("#hdnTabId").val();
                $(ID).focus();
            });
            function ShowLoading(id) {
                location.href = '#' + id;
            }
            function CallEditSavegr() {
                $('#lnkCreateGroup').css("box-shadow", "0px 0px 5px #00B7E5");
                if ($('#txtTitle').text() == '') {
                    setTimeout(
                    function () {
                        $('#lnkCreateGroup').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
                }
            }
        </script>
    </div>
</asp:Content>
