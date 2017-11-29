<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="top_menu.ascx.cs" Inherits="Cb.Web.Controls.top_menu" %>

<!--block_topmenu-->
<%@ Register TagPrefix="dgc" TagName="block_like" Src="~/Controls/block_like.ascx" %>

<div class="header-area-wrapper">

    <div class="top-navigation-wrapper boxed-style">
        <div class="top-navigation-container container">
            <div class="col-xs-10">
                <div class="top-social-wrapper">
                    <div class="top-navigation-right-text">
                        <div style="float: left;">
                            <img src="/Images/icon-mail.png" style="width: 14px; float: left; margin-top: 3px;" alt="">
                            <span style="margin-right: 15px; margin-left: 9px; font-size: 12px; line-height: 14px; color: #fff;">
                                <a runat="server" id="hypEmail"  target="_blank">
                                    <asp:Literal runat="server" ID="ltrEmail"></asp:Literal></a>

                            </span>
                        </div>
                        <div style="float: left; color: #fff;">
                            <span>
                                <img src="/Images/icon-phone.png" style="width: 14px; float: left; margin-right: 5px;">
                                Viber / Whatsapp
                            </span>
                            
                            <span style="margin-left: 9px; font-size: 12px; line-height: 14px;">
                                <a runat="server" id="hypPhone" target="_blank">
                                    <asp:Literal runat="server" ID="ltrPhoneValue"></asp:Literal></a></span>
                        </div>
                    </div>
                </div>
                <div class="top-search-wrapper hidden">
                    <div class="gdl-search-form">
                        <%--form method="get" id="searchform" action="http://www.VietnamTravelGroup.vn/">
                            <input type="submit" id="searchsubmit" value="" />
                            <div class="search-text" id="search-text">
                                <input type="text" value="" name="s" id="s" autocomplete="off" data-default="" />
                            </div>
                            <div class="clear"></div>
                        </form>--%>
                    </div>
                </div>

                <dgc:block_like ID="block_like" runat="server" />
            </div>
            <div class="col-xs-2 hidden-lg hidden-md">
                <!--mobile menu-->
                <ul id="header-mobile-menu" class="hidden">
                    <%-- <asp:Repeater runat="server" ID="rptResultMobile" OnItemDataBound="rptResult_ItemDataBound">
                        <ItemTemplate>
                            <li><a class="scroll" runat="server" id="hypName">
                                <asp:Literal runat="server" ID="ltrName"></asp:Literal></a>
                                <ul id="ulSub" visible="false" runat="server">
                                    <asp:Repeater runat="server" ID="rptResultSub2" OnItemDataBound="rptSub2_ItemDataBound">
                                        <ItemTemplate>
                                            <li>
                                                <a runat="server" id="hypNameSub2">
                                                    <asp:Literal runat="server" ID="ltrNameSub2"></asp:Literal></a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>--%>
                </ul>
                <!--/mobile menu-->
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <!-- top navigation wrapper -->

    <div id="main-header" class="">
        <div class="main-header">
            <div class="container">
                <div class="wsmenucontainer" id="header">
                    <div class="wsmenuexpandermain slideRight">
                        <a id="navToggle" class="animated-arrow slideLeft menuclose" href="#" rel="nofollow"><span></span></a>
                    </div>
                    <div class="wsmenucontent overlapblackbg menuclose"></div>
                    <div class="logo navbar-brand ">
                        <a runat="server" id="hypImgHomePage">
                            <img runat="server" id="imgLogo" /></a>
                    </div>
                    <nav class="wsmenu slideLeft clearfix menuclose wsmenuMobile">
                        <ul class="mobile-sub wsmenu-list row">
                            <li><a runat="server" id="hypIcon" class="active"><i class="fa fa-home"></i><span class="hometext">Home</span></a></li>

                            <asp:Repeater runat="server" ID="rptAbout" OnItemDataBound="rptAbout_ItemDataBound">
                                <ItemTemplate>
                                    <li><span class="wsmenu-click"><i class="wsmenu-arrow fa fa-angle-down"></i></span>
                                        <a runat="server" id="hypName" rel="nofollow">
                                            <i class="fa fa-align-justify"></i>
                                            <asp:Literal runat="server" ID="ltrName"></asp:Literal>
                                            <span class="arrow hidden"></span>
                                        </a>
                                        <ul class="wsmenu-submenu" id="ulSub" visible="false" runat="server">
                                            <asp:Repeater runat="server" ID="rptAboutSub" OnItemDataBound="rptAboutSub_ItemDataBound">
                                                <ItemTemplate>
                                                    <li><a runat="server" id="hypNameSub2" rel="nofollow">
                                                        <i class="fa fa-angle-right"></i>
                                                        <asp:Literal runat="server" ID="ltrNameSub2"></asp:Literal></a></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>

                            <asp:Repeater runat="server" ID="rptResult" OnItemDataBound="rptResult_ItemDataBound">
                                <ItemTemplate>
                                    <li class="menu-item-depth-0">
                                        <%--  <span class="wsmenu-click"><i class="wsmenu-arrow fa fa-angle-down"></i></span>--%>
                                        <a runat="server" id="hypName">
                                            <asp:Literal runat="server" ID="ltrIcon"></asp:Literal>

                                            <asp:Literal runat="server" ID="ltrName"></asp:Literal>
                                            <span class="arrow" runat="server" id="divIconSub" style="display: none"></span></a>
                                        <div runat="server" id="divSub" visible="false">
                                            <asp:Repeater runat="server" ID="rptResultSub" OnItemDataBound="rptResultSub_ItemDataBound">
                                                <ItemTemplate>
                                                    <ul runat="server" id="ulSub">

                                                        <li class="category"><a runat="server" id="hypNameSub2">
                                                            <asp:Literal runat="server" ID="ltrNameSub2"></asp:Literal></a>
                                                            <img runat="server" id="img"></li>
                                                        <asp:Repeater runat="server" ID="rptResultSub1" OnItemDataBound="rptResultSub1_ItemDataBound">
                                                            <ItemTemplate>
                                                                <li class="cat-item cat-item-322">
                                                                    <a runat="server" id="hypTitle">
                                                                        <asp:Literal runat="server" ID="ltrTitle"></asp:Literal></a></li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>

                        </ul>
                    </nav>
                </div>
            </div>
        </div>
    </div>
</div>

<asp:HiddenField runat="server" ID="hddParentNameUrl" Visible="false" />

<!--/block_topmenu-->

