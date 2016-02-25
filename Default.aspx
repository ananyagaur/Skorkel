<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
            <div class="topContainer">
                <div class="logo">
                    <img src="images/logo-inner.png" alt="Skorkel" /></div>
                <div class="navigationInner">
                    <ul>
                        <li><a href="index.html">Home</a></li>
                        <li><a href="my-skorkel.html">My Skorkel</a></li>
                        <li><a href="research.html">Research</a></li>
                        <li><a href="interact.html">Interact</a></li>
                        <li><a href="organization.html">Organization</a></li>
                    </ul>
                </div>
                <div class="searchInner">
                    <div class="searchBtn">
                        <a href="#">
                            <img src="images/search-icon.png" alt="search" /></a></div>
                    <div class="search" id="searchTxt">
                        <input type="text" name="txtUsername" value="People, organizations, groups" onblur="if(this.value=='') this.value='People, organizations, groups';"
                            onfocus="if(this.value=='People, organizations, groups') this.value='';" />
                    </div>
                </div>
                <!--header ends-->
            </div>
            <!--heading ends-->
            <div class="cls">
            </div>
            <!--inner container starts-->
            <div class="innerContainer" id="documentContainer">
                <div class="headingNew">
                    Home</div>
            </div>
            <div class="cls">
            </div>
            <!--inner container ends-->
            <div class="innerDocumentContainer">
                <div class="innerContainer">
                    <div class="leftDocumentPanelNew">
                        <!--left verticle icons starts-->
                        <div class="leftLeftPanelNew">
                            <ul>
                                <li><a href="deepak-sharma.html">
                                    <img src="images/home-icon.png" align="" /></a></li>
                                <li><a href="skillset.html">
                                    <img src="images/briefcase-icon.png" /></a></li>
                                <li><a href="education.html">
                                    <img src="images/education-icon.png" /></a></li>
                                <li id="active"><a href="documents.html">
                                    <img src="images/dictionary-icon.png" /></a></li>
                                <li><a href="achievements.html">
                                    <img src="images/tag-icon.png" /></a></li>
                            </ul>
                        </div>
                        <!--left verticle icons ends-->
                        <div class="leftRightPanelNew">
                            <div class="top">
                            </div>
                            <div class="center">
                                <div class="documentHeading">
                                    Documents</div>
                                <div class="cls">
                                </div>
                                <div class="cls" style="height: 10px">
                                </div>
                                <div id="sliderContent" class="ui-corner-all">
                                    <div class="viewer ui-corner-all" style="height: 208px">
                                        <div class="content-conveyor ui-helper-clearfix">
                                            <!--item starts-->
                                            <div class="itemDocument">
                                                <div class="rightTag">
                                                    <p class="achievementTitle">
                                                        <a href="#">Document Name.pdf</a></p>
                                                    <p>
                                                        Lorem Ipsum has been the industry's standard dummy
                                                    </p>
                                                </div>
                                                <div class="cls">
                                                </div>
                                            </div>
                                            <!--item ends-->
                                            <!--item starts-->
                                            <div class="itemDocument">
                                                <div class="rightTag">
                                                    <p class="achievementTitle">
                                                        <a href="#">Document Name.pdf</a></p>
                                                    <p>
                                                        Lorem Ipsum has been the industry's standard dummy
                                                    </p>
                                                </div>
                                                <div class="cls">
                                                </div>
                                                <div class="sale">
                                                    <img src="images/sale.png" /></div>
                                            </div>
                                            <!--item ends-->
                                            <!--item starts-->
                                            <div class="itemDocument">
                                                <div class="rightTag redTag">
                                                    <p class="achievementTitle">
                                                        <a href="#">Document Name.pdf</a></p>
                                                    <p>
                                                        Lorem Ipsum has been the industry's standard dummy
                                                    </p>
                                                </div>
                                                <div class="cls">
                                                </div>
                                            </div>
                                            <!--item ends-->
                                            <!--item starts-->
                                            <div class="itemDocument">
                                                <div class="rightTag">
                                                    <p class="achievementTitle">
                                                        <a href="#">Document Name.pdf</a></p>
                                                    <p>
                                                        Lorem Ipsum has been the industry's standard dummy
                                                    </p>
                                                </div>
                                                <div class="cls">
                                                </div>
                                            </div>
                                            <!--item ends-->
                                            <!--item starts-->
                                            <div class="itemDocument">
                                                <div class="rightTag">
                                                    <p class="achievementTitle">
                                                        <a href="#">Document Name.pdf</a></p>
                                                    <p>
                                                        Lorem Ipsum has been the industry's standard dummy
                                                    </p>
                                                </div>
                                                <div class="cls">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                </div>
                                <div id="slider">
                                </div>
                                <div class="cls" style="height: 10px">
                                </div>
                            </div>
                            <div class="bottom">
                            </div>
                        </div>
                    </div>
                    <div class="clsFooter">
                    </div>
                </div>
                <!--left verticle search list ends-->
            </div>
        </div>
    <script type="text/javascript">
        $(function () {

            //vars
            var conveyor = $(".content-conveyor", $("#sliderContent")),
		item = $(".itemDocument", $("#sliderContent"));

            //set length of conveyor
            conveyor.css("width", (item.length * parseInt(item.css("width"))));

            //config
            var sliderOpts = {
                max: (item.length * parseInt(item.css("width"))) - parseInt($(".viewer", $("#sliderContent")).css("width")),
                slide: function (e, ui) {
                    conveyor.css("left", "-" + (ui.value) + "px");
                }
            };

            //create slider
            $("#slider").slider(sliderOpts);
        });
    </script>
</asp:Content>
