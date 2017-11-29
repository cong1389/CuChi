<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogDetail.ascx.cs" Inherits="Cb.Web.Pages.BlogManagement.BlogDetail" %>

<!--BlogDetail-->
<%@ Register TagPrefix="dgc" TagName="top_menu" Src="~/Controls/top_menu.ascx" %>

<div class="header-outer-wrapper " runat="server" id="divBoxTop">
    <dgc:top_menu ID="top_menu" runat="server" />
    <asp:Literal runat="server" ID="ltrHeaderCategory"></asp:Literal>

</div>

<div class="content-outer-wrapper">
    <div class="content-wrapper container main ">
        <div id="post-97" class="post-97 post type-post status-publish format-standard has-post-thumbnail hentry category-blog category-central-coast category-central-highland category-conference category-hanoi-and-the-northern-mountains category-ho-chi-minh-city-surroundings category-laos category-mekong-delta category-tips-before-travel tag-biking-vietnam tag-blog tag-cycling-vietnam tag-cycling-vietnam-routes tag-vietnam-bicycle-tours tag-vietnam-bike-tours">
            <div class="page-wrapper single-blog single-sidebar right-sidebar">
                <div class="row gdl-page-row-wrapper">
                    <div class="gdl-page-left mb0 twelve columns">

                        <div class="row">
                            <div class="gdl-page-item mb0 pb20 gdl-blog-full twelve columns">

                                <div class="blog-content-wrapper">
                                    <h1 class="blog-title">
                                        <asp:Literal runat="server" ID="ltrTitle"></asp:Literal>
                                    </h1>
                                    <div class="blog-info-wrapper">
                                        <div class="blog-date">
                                            <i class="icon-calendar"></i><a href="#">
                                                <asp:Literal runat="server" ID="ltrDate"></asp:Literal></a>
                                        </div>
                                        <div class="clear"></div>
                                    </div>

                                    <div class="blog-media-wrapper gdl-image">
                                        <div class="flexslider gdl-slider">
                                            <ul class="slides">

                                                <asp:Repeater runat="server" ID="rptImg" OnItemDataBound="rptImg_ItemDataBound">
                                                    <ItemTemplate>
                                                        <li class="" style="width: 100%; float: left; margin-right: -100%; position: relative; display: none;">
                                                            <img runat="server" id="img"></li>
                                                    </ItemTemplate>
                                                </asp:Repeater>


                                            </ul>
                                            <ul class="flex-direction-nav">
                                                <li><a class="flex-prev" href="#">Previous</a></li>
                                                <li><a class="flex-next" href="#">Next</a></li>
                                            </ul>
                                        </div>
                                    </div>

                                    <div class="blog-content">
                                        <div class="item-page">
                                            <asp:Literal runat="server" ID="ltrDetail"></asp:Literal>
                                        </div>
                                        <div class="clear"></div>
                                    </div>

                                </div>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>

                    <div class="clear"></div>
                </div>
                <div class="clear"></div>
            </div>
            <!-- page wrapper -->
        </div>
        <!-- post class -->
    </div>
    <!-- content wrapper -->
</div>
