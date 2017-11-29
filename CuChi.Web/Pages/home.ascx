<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="home.ascx.cs" Inherits="Cb.Web.Pages.home" %>

<!--home-->
<%@ Register TagPrefix="dgc" TagName="block_category" Src="~/Controls/block_category.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_category_Packages" Src="~/Controls/block_category.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_category_Tips" Src="~/Controls/block_category.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_category_package" Src="~/Controls/block_category.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_tripadvisor" Src="~/Controls/block_tripadvisor.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_featuredvideo" Src="~/Controls/block_featuredvideo.ascx" %>
<%@ Register TagPrefix="dgc" TagName="top_menu" Src="~/Controls/top_menu.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_slide" Src="~/Controls/block_slide.ascx" %>

<div class="header-outer-wrapper full-slider">
    <dgc:top_menu ID="top_menu" runat="server" />
    <dgc:block_slide ID="block_slide" runat="server" />
</div>

<div class="content-outer-wrapper divHome">
    <div class="page-full-wrapper">
        <div id="post-3896" class="post-3896 page type-page status-publish hentry">
            <div class="page-wrapper single-page ">
                <div class="gdl-page-item">
                    <div class="container hidden">
                        <div class="row1">
                            <div class="twelve columns mb45">
                                <div class="gdl-page-content">
                                    <asp:Literal runat="server" ID="ltrWellCome"></asp:Literal>
                                </div>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>

                    <!--Most Popular Daily Tours-->
                    <%--<div class="container">
                        <div class="row">
                            <div class="twelve columns title-item-class title-item-class-0 mb11">
                                <div class="title-item-wrapper text-center">

                                    <div class="divH2 title-item-header text-center">
                                        <a href="/most-popular-daily-big-group-tours/vn">Most Popular Daily Big Group Tours</a>
                                    </div>
                                    <div class="wpb_single_image wpb_content_element vc_align_center">
                                        <div class="wpb_wrapper">
                                            <div class="divBoxImg">

                                                <img width="64" height="" src="/Images/iconBus.png"
                                                    data-lazy-src="/Images/iconBus.png"
                                                    class="vc_single_image-img attachment-full     lazyloaded" alt="icon_dichvu">
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>--%>
                    <div class="container">
                        <div class="row">
                            <dgc:block_category ID="block_category_Daily" runat="server" />
                            <div class="clear"></div>
                        </div>
                    </div>
                    <!--/Most Popular Daily Tours-->

                    <!--Most Popular Packages Tours-->
                    <%--<div class="container">
                        <div class="row">
                            <div class="twelve columns title-item-class title-item-class-0 mb11">
                                <div class="title-item-wrapper text-center">

                                    <div class="divH2 title-item-header text-center">
                                        <a href="/most-popular-luxury-group-tours/vn">Most Popular Luxury Group Tours</a>
                                    </div>
                                    <div class="wpb_single_image wpb_content_element vc_align_center">
                                        <div class="wpb_wrapper">
                                            <div class="divBoxImg">

                                                <img width="64" height="" src="/Images/big-binoculars.png"
                                                    data-lazy-src="/Images/big-binoculars.png"
                                                    class="vc_single_image-img attachment-full     lazyloaded" alt="icon_dichvu">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>--%>
                    <div class="container">
                        <div class="row">
                            <dgc:block_category_Packages ID="block_category_Packages" runat="server" />
                            <div class="clear"></div>
                        </div>
                    </div>
                    <!--/Most Popular Packages Tours-->

                    <!--Tips Before Travel-->
                    <%--<div class="container">
                        <div class="row">
                            <div class="twelve columns title-item-class title-item-class-0 mb11">
                                <div class="title-item-wrapper text-center">

                                    <div class="divH2 title-item-header text-center"><a href="/most-popular-private-tours/vn">Most Popular Private Tours</a></div>
                                    <div class="wpb_single_image wpb_content_element vc_align_center">
                                        <div class="wpb_wrapper">
                                            <div class="divBoxImg">

                                                <img width="64" height="" src="/Images/family-of-mother-and-father-with-a-daughter.png"
                                                    data-lazy-src="/Images/family-of-mother-and-father-with-a-daughter.png"
                                                    class="vc_single_image-img attachment-full     lazyloaded" alt="icon_dichvu">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>--%>
                    <div class="container">
                        <div class="row">
                            <dgc:block_category_Tips ID="block_category_Tips" runat="server" CategoryIdByPass="122" />
                            <div class="clear"></div>
                        </div>
                    </div>
                    <!--/Tips Before Travel-->

                    <!--Package tour-->
                   <%-- <div class="container">
                        <div class="row">
                            <div class="twelve columns title-item-class title-item-class-0 mb11">
                                <div class="title-item-wrapper text-center">

                                    <div class="divH2 title-item-header text-center"><a href="/most-popular-private-tours/vn">Package Tour</a></div>
                                    <div class="wpb_single_image wpb_content_element vc_align_center">
                                        <div class="wpb_wrapper">
                                            <div class="divBoxImg">

                                                <img width="64" height="" src="/Images/family-of-mother-and-father-with-a-daughter.png"
                                                    data-lazy-src="/Images/family-of-mother-and-father-with-a-daughter.png"
                                                    class="vc_single_image-img attachment-full     lazyloaded" alt="icon_dichvu">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>--%>
                    <div class="container">
                        <div class="row">
                            <dgc:block_category_package ID="block_category_package" runat="server" CategoryIdByPass="80" />
                            <div class="clear"></div>
                        </div>
                    </div>
                    <!--/Package tour-->

                    <dgc:block_tripadvisor ID="block_tripadvisor" runat="server" />
                    <%--<dgc:block_featuredvideo ID="block_featuredvideo" runat="server" />--%>


                    <!-- gdl page item -->
                    <div class="clear"></div>
                </div>
                <!-- page wrapper -->
            </div>

        </div>
    </div>
</div>
