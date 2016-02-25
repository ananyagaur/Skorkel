<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Create_Group.aspx.cs" Inherits="Create_Group" %>

<%@ Register Src="~/UserControl/CropImg.ascx" TagName="CropImage" TagPrefix="crp1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.min.js")%>" type="text/javascript"></script>
    <script type="text/javascript" src="<%=ResolveUrl("js/ddsmoothmenu.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="innerDocumentContainerSpc">
        <div id="divDeletesucess" clientidmode="Static" runat="server" class="EditProfilepopupHome"
            style="display: none;">
            <div id="divDeleteConfirm" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                float: left; width: 500px; padding-top: 0px; position: fixed; margin: 20% 0 0 30%;
                z-index: 100;" clientidmode="Static">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                    <tr>
                        <td>
                            <b>
                                <asp:Label ID="lbl" runat="server"></asp:Label>
                            </b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>&nbsp;&nbsp;
                                <asp:Label ID="lblConnDisconn" runat="server" Text="Group created sucessfully." Font-Size="Small"
                                    ForeColor="Green"></asp:Label>
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
                                    </td>
                                    <td style="padding-right: 20px;">
                                        <asp:LinkButton ID="lnkDeleteCancel" runat="server" ClientIDMode="Static" Text="Ok"
                                            CssClass="joinBtn" OnClientClick="javascript:divCancels();return false;"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="innerContainer">
            <div class="innerGroupBox">
                <div class="innerSignUpTabs">
                    <!--top profile starts-->
                    <asp:HiddenField ID="hdnConnCommandName" runat="server" ClientIDMode="Static" Value="" />
                     <asp:UpdatePanel ID="upProfileMain" runat="server">
                        <ContentTemplate>
                    <div class="tabProfileBox">
                        <div class="photoIcon">
                            <img id="imgUser" runat="server" height="160" width="160" />
                            <asp:LinkButton ID="lnkChangeImage" CssClass="camera" runat="server" ToolTip="Change Profile Image"
                                ClientIDMode="Static" OnClick="lnkChangeImage_Click">
                               <img id="imgCamera" src="images/camera-icon.png" style="display:none;" />
                            </asp:LinkButton>
                        </div>
                        <div class="CropImagepopup" id="PopUpCropImage" clientidmode="Static" runat="server"
                            style="display: none;">
                            <div class="popUp1" id="PopUpCropImage1" clientidmode="Static" runat="server" style="z-index: 1000;">
                                <div class="popUp" id="popUp" style="height: 475px; width: 625px;">
                                    <div class="popUpBox" style="height: 480px; margin: 10% 0px 0px 12%; width: 745px;
                                        overflow: auto">
                                        <p class="shareContent" style="margin-top: 5px;">
                                            Change Image</p>
                                        <div style="">
                                            <crp1:CropImage ID="crp1" runat="server" />
                                            <br />
                                            <asp:LinkButton ID="lnkCancelProfilediv" runat="server" Text="Cancel" CssClass="cancelcropdivgrp"
                                                Style="float: left; margin-left: 3px; margin-top: -32px;" OnClick="lnkCancelProfilediv_click"> </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="upprofile" runat="server">
                            <ContentTemplate>
                                <div class="photoIconDtls">
                                    <p class="photoIconDtlsName">
                                        <asp:Label ID="lblUserProfName" runat="server" Text=""></asp:Label>
                                    </p>
                                    <br />
                                </div>
                                <div class="endMsg">
                                    <div class="endorsements">
                                        <p class="txtT">
                                            <asp:LinkButton ID="lblEndorseCount" runat="server" Text="" Style="text-decoration: none !important;
                                                color: #00b5bc;" OnClick="lblEndorseCount_click"></asp:LinkButton>
                                        </p>
                                        <p class="subTxtT">
                                            Endorsements</p>
                                    </div>
                                    <div class="messages">
                                        <p class="txtT">
                                            <asp:Label ID="lblMessCount" runat="server" Text=""></asp:Label></p>
                                        <p class="subTxtT">
                                            Score</p>
                                    </div>
                                </div>
                                <!--edit starts-->
                                <div class="EditProfilepopup" id="divEditProfile" runat="server" style="display: none;">
                                    <div class="editNamePop" id="divEditProfiles" style="margin: 11% 0px 0 51%;">
                                        <p>
                                            <asp:TextBox ID="txtName" runat="server" placeholder="Name"></asp:TextBox>
                                        </p>
                                        <p>
                                            <asp:TextBox ID="txtGender" runat="server" placeholder="Gender"></asp:TextBox>
                                        </p>
                                        <p>
                                            <asp:TextBox ID="txtLanguage" runat="server" placeholder="Languages Known"></asp:TextBox>
                                        </p>
                                        <asp:TextBox ID="txtEmailId" runat="server" placeholder="Email Id" Enabled="false"></asp:TextBox>
                                        <p>
                                            <asp:TextBox ID="txtPhone" runat="server" ClientIDMode="Static" MaxLength="11" placeholder="Phone Number"></asp:TextBox>
                                        </p>
                                        <p style="margin-left: 42px;">
                                            <asp:Label ID="lblProfilemsg" runat="server" ForeColor="Red"></asp:Label>
                                        </p>
                                        <p class="btnEdit">
                                            <asp:LinkButton ID="lnkCancelProfile" CssClass="editNameCancel" runat="server" Text="Cancel"
                                                OnClientClick="javascript:callBodyEnable();" OnClick="lnkCancelProfile_click"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkProfileSave" CssClass="vote" runat="server" Text="Save" OnClick="lnkProfileSave_click"
                                                OnClientClick="javascript:callBodyEnable();"></asp:LinkButton>
                                        </p>
                                    </div>
                                </div>
                                <!--edit starts-->
                                <!--edit starts-->
                                <div class="editIcon editName" id="divEditUserProfile" runat="server" style="margin: -85px 20px 0 180px;">
                                    <div id="smoothmenu1" class="ddsmoothmenu">
                                        <ul class="iconUl">
                                            <li><a href="#">
                                                <img src="images/edit.png" /></a>
                                                <ul>
                                                    <li style="border-bottom: 1px solid #cdced0;">
                                                        <asp:LinkButton ID="lnkeditProfile" runat="server" Text="Edit" OnClick="lnkeditProfile_click"
                                                            OnClientClick="javascript:callBodyDisable();"></asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </li>
                                        </ul>
                                        <br style="clear: left" />
                                    </div>
                                </div>
                                <!--edit starts-->
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                      </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnkChangeImage" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <!--top profile ends-->
                    <div class="cls">
                    </div>
                    <!--tabs starts-->
                    <div class="innerTabBox">
                        <ul>
                            <li>
                                <asp:LinkButton runat="server" ID="lnkHome" class="innerTabHome" Text="Home" OnClick="lnkHome_Click"></asp:LinkButton></li>
                            <li>
                                <asp:LinkButton runat="server" ID="lnkDocuments" class="innerTabDoc" Text="Documents"
                                    OnClick="lnkDocuments_Click"></asp:LinkButton></li>
                            <li style="width: 250px;">
                                <asp:LinkButton runat="server" ID="lnkWorkex" class="innerWrkex" Text="Work experience"
                                    Width="250px" OnClick="lnkWorkex_Click"></asp:LinkButton></li>
                            <li>
                                <asp:LinkButton runat="server" ID="lnkEducation" class="innerEdu" Text="Education"
                                    OnClick="lnkEducation_Click"></asp:LinkButton></li>
                            <li style="border: 0">
                                <asp:LinkButton runat="server" ID="lnkAchievements" class="innerAch" Text="Achievements"
                                    OnClick="lnkAchievements_Click"></asp:LinkButton></li>
                        </ul>
                    </div>
                    <!--tabs ends-->
                    <div class="cls">
                    </div>
                    <!--main section starts-->
                    <div class="mainSection">
                        <p class="newGroupHeading">
                            New Group</p>
                        <p>
                            <asp:TextBox ID="txtCreateGr" runat="server" ClientIDMode="Static" CssClass="newGroupInput"
                                placeholder="Group Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCreateGr"
                                Display="Dynamic" ValidationGroup="cgroup" ErrorMessage="Please enter Group Name"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </p>
                        <div>
                            <p class="newGroupHeading">
                                <span style="font-size: 18px; color: #666666;">Group Profile Image</span>
                            </p>
                            <div class="cls">
                            </div>
                            <div style="margin-left: 0px; margin-top: 12px;">
                                <div>
                                    <div style="margin: 0px 0px 0px 0px">
                                        <asp:FileUpload ID="FileUpload1" ClientIDMode="Static" runat="server" CompleteBackColor="#FFFFFF" />
                                    </div>
                                    <div>
                                        <asp:Label ID="lblSuccMess" runat="server"></asp:Label>
                                    </div>
                                    <div style="margin-right: 150px; width: 115px; margin-left: -4px; margin-top: 11px;
                                        background: url(images/profile-border.png) no-repeat scroll 0 0 transparent;
                                        float: left; padding: 9px 10px;">
                                        <img id="imgGroupUser" runat="server" clientidmode="Static" height="95" width="80"
                                            style="padding-bottom: 10px;" />
                                        <br />
                                        <asp:ImageButton ID="ImgRemovePic" ToolTip="Remove Profile Picture." runat="server"
                                            Style="display: none; margin-left: 30px;" ClientIDMode="Static" OnClientClick="removeimages();return false;"
                                            ImageUrl="images/Delete.gif" />
                                    </div>
                                    <asp:LinkButton ID="uploadimage" runat="server" ClientIDMode="Static" Text="Upload"
                                        OnClick="fileuplad_onload" CssClass="vote" Style="margin: 13px 0px 0px -20%;
                                        display: none;"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="cls">
                            <p>
                            </p>
                        </div>
                        <p class="newGroupContext">
                            <span style="font-size: 18px; color: #666666;">Context:</span>
                        </p>
                        <asp:HiddenField ID="hdnSubject" ClientIDMode="Static" runat="server" />
                        <asp:UpdatePanel ID="hhd" runat="server">
                            <ContentTemplate>
                                <ul class="context" id="ultags">
                                    <asp:ListView ID="lstCreateGroup" runat="server" OnItemDataBound="lstCreateGroup_ItemDataBound">
                                        <ItemTemplate>
                                            <li id="SubLi" runat="server" style="cursor: pointer;">
                                                <asp:HiddenField ID="hdnSubCatId" ClientIDMode="Static" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                <asp:Label ID="lnkCatName" ClientIDMode="Static" CssClass="unselectedtagnameGroup"
                                                    runat="server" Text='<%#Eval("strCategoryName")%>' CommandName="Subject Category"></asp:Label>
                                            </li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </ul>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lstCreateGroup" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <p>
                            <textarea class="newGroupDescp" id="txtPurpose" runat="server" placeholder="Description"></textarea>
                        </p>
                        <div>
                            <select data-placeholder="Enter members name here" class="chosen-select MySkorkeltxtFieldselect divnewGroupDescp"
                                id="txtInviteMember" onchange="getAllMemberValue(this.id)" runat="server" clientidmode="Static"
                                multiple style="width: 865px; margin-top: 2px; border: 1px solid #1797a2;" tabindex="4">
                            </select>
                            <asp:RequiredFieldValidator InitialValue="Enter members name here" ValidationGroup="cgroup"
                                ClientIDMode="Static" ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtInviteMember"
                                ErrorMessage="Enter members name" ForeColor="Red" Style="position: absolute;
                                padding-left: 0px; padding-top: 0px; font-size: small"></asp:RequiredFieldValidator>
                            <asp:HiddenField ID="hdnMembId" ClientIDMode="Static" runat="server" />
                        </div>
                        <asp:HiddenField ID="hdnAutoreqJoin" ClientIDMode="Static" runat="server" />
                        <asp:UpdatePanel ID="idd" runat="server">
                            <ContentTemplate>
                                <div class="access">
                                    Access:
                                    <asp:ImageButton ID="imgAutojoin" runat="server" ClientIDMode="Static" OnClientClick="checkimages();return false;" />
                                    Auto Join
                                    <asp:ImageButton ID="imgReqjoin" runat="server" ClientIDMode="Static" OnClientClick="checkremoveimages();return false;" />
                                    Request to Join
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgAutojoin" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="imgReqjoin" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:LinkButton ID="lnkCreateGroup" Text="Create Group" runat="server" ClientIDMode="Static"
                            OnClientClick="javascript:callSavegrp();" ValidationGroup="cgroup" OnClick="btnSaveGroup_Click"
                            class="createGroup"></asp:LinkButton>
                        <a href="Home.aspx" id="aCancel" onclick="callCancelgrp()" class="cancelGroup">Cancel</a>
                    </div>
                    <asp:HiddenField ID="hdnTabId" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdnPostId" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdnLoader" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdncommentfocus" runat="server" ClientIDMode="Static" />
                    <!--main section ends-->
                </div>
                <div class="cls">
                </div>
                <!--left verticle search list ends-->
            </div>
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
        function callBodyDisable() {
            $("body").css("position", "fixed");
            $("body").css("overflow-y", "scroll");
        }
        function callBodyEnable() {
            $("body").css("position", "static");
            $("body").css("overflow-y", "auto");
        }
    </script>
    <script type="text/javascript">
        var strSelTexts = '';
        $(document).ready(function () {
            $('ul.context li').click(function () {
                $(this).toggleClass('selectedcreateGroup graycreateGroup');
                if ($(this).hasClass("selectedcreateGroup")) {
                    $(this).children(".unselectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
                } else {
                    $(this).children(".selectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
                }
                AddSubjectCall($(this).children("#hdnSubCatId").val());
            });

        });

        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('ul.context li').click(function () {
                    $(this).toggleClass('selectedcreateGroup graycreateGroup');
                    if ($(this).hasClass("selectedcreateGroup")) {
                        $(this).children(".unselectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
                    } else {
                        $(this).children(".selectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
                    }
                    AddSubjectCall($(this).children("#hdnSubCatId").val());
                });

            });
        });
        function AddSubjectCall(ids) {
            var subVal = $("#hdnSubject").val();
            if (subVal == '') {
                $("#hdnSubject").val(ids);
            } else {
                strSelTexts = $("#hdnSubject").val();
                strSelTexts += ',' + ids;
                $("#hdnSubject").val(strSelTexts);
                strSelTexts = '';
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#imgAutojoin").attr('src', 'images/checked2.png');
            $("#imgReqjoin").attr('src', 'images/unchecked2.png');
            $("#hdnAutoreqJoin").val('1');
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("#imgAutojoin").attr('src', 'images/checked2.png');
                $("#imgReqjoin").attr('src', 'images/unchecked2.png');
            });
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
                        $("#imgGroupUser").attr('src', URL.createObjectURL(event.target.files[0]));
                        $("#ImgRemovePic").css("display", "block");
                    }
                } else {
                    alert('Please select image.');
                }
            });

        });
        function removeimages() {
            $("#imgGroupUser").fadeIn("fast").attr('src', 'images/groupPhoto.jpg');
            $('#FileUpload1').val('');
            $("#ImgRemovePic").css("display", "none");
            return false;
        }
        function checkimages() {
            if ($("#imgAutojoin").attr('src') == "images/checked2.png") {
                $("#hdnAutoreqJoin").val('1');
                $("#imgReqjoin").attr('src', 'images/unchecked2.png');
            } else {
                $("#hdnAutoreqJoin").val('1');
                $("#imgAutojoin").attr('src', 'images/checked2.png');
                $("#imgReqjoin").attr('src', 'images/unchecked2.png');
            }
        }
        function checkremoveimages() {
            if ($("#imgReqjoin").attr('src') == 'images/checked2.png') {
                $("#hdnAutoreqJoin").val('2');
                $("#imgAutojoin").attr('src', 'images/unchecked2.png');
            } else {
                $("#hdnAutoreqJoin").val('2');
                $("#imgReqjoin").attr('src', 'images/checked2.png');
                $("#imgAutojoin").attr('src', 'images/unchecked2.png');
            }
        }
    </script>
    <script type="text/javascript">
        function docdelete() {
            $('#divDeletesucess').css("display", "block");
        }
        function divCancels() {
            $('#divDeletesucess').css("display", "none");
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#lblNotifyCount").text() == '0') {
                document.getElementById("divNotification1").style.display = "none";
            }
            if ($("#lblInboxCount").text() == '0') {
                document.getElementById("divInbox").style.display = "none";
            }
        });

        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                if ($("#lblNotifyCount").text() == '0') {
                    document.getElementById("divNotification1").style.display = "none";
                }
                if ($("#lblInboxCount").text() == '0') {
                    document.getElementById("divInbox").style.display = "none";
                }
            });
        });
    </script>
    <script type="text/javascript">
        ddsmoothmenu.init({
            mainmenuid: "smoothmenu1", //menu DIV id
            orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
            classname: 'ddsmoothmenu', //class added to menu's outer DIV
            contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
        })
        $(document).ready(function () {
            $(".photoIcon").mouseover(function () {
                $("#imgCamera").css('display', 'block');
            });
            $(".photoIcon").mouseout(function () {
                $("#imgCamera").css('display', 'none');
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById('imgLoadMore').style.display = 'none';
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                ddsmoothmenu.init({
                    mainmenuid: "smoothmenu1", //menu DIV id
                    orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
                    classname: 'ddsmoothmenu', //class added to menu's outer DIV
                    contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
                })

                $(".scroll").click(function (event) {
                    $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 500);
                });

                $(".photoIcon").mouseover(function () {
                    $("#imgCamera").css('display', 'block');
                });
                $(".photoIcon").mouseout(function () {
                    $("#imgCamera").css('display', 'none');
                });
            });
        });
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
        function callSavegrp() {
            $('#lnkCreateGroup').css("background", "#00A5AA");
            $('#lnkCreateGroup').css("box-shadow", "0px 0px 5px #00B7E5");
            setTimeout(
            function () {
                if ($('#txtCreateGr').text() == "") {
                    $('#lnkCreateGroup').css("background", "#0096a1");
                    $('#lnkCreateGroup').css("box-shadow", "0px 0px 0px #00B7E5");
                }
            }, 1000);
        }
        function callCancelgrp() {
            $('#aCancel').css("background", "#D0D0D0");
            $('#aCancel').css("border", "2px solid #BCBDCE");
        }
    </script>
</asp:Content>
