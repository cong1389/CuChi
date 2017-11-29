<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_category.ascx.cs" Inherits="Cb.Web.Controls.block_category" %>

<!--block_category-->
<%@ Register Namespace="Cb.WebControls" Assembly="Cb.WebControls" TagPrefix="cc" %>
<%@ Register TagPrefix="dgc" TagName="top_menu" Src="~/Controls/top_menu.ascx" %>

<div class="header-outer-wrapper " runat="server" id="divBoxTop">
    <dgc:top_menu ID="top_menu" runat="server" />
    <asp:Literal runat="server" ID="ltrHeaderCategory"></asp:Literal>
</div>

<div class="content-outer-wrapper mb31">
    <div class="page-full-wrapper">
        <div class="gdl-page-item">

            <div class="container">
                <div class="row">
                    <div class="twelve columns title-item-class title-item-class-0 mb11">
                        <div class="title-item-wrapper text-center">

                            <div class="divH2 title-item-header text-center">
                                 <a runat="server" id="hypCateName">
                                  <asp:Literal runat="server" ID="ltrCateName"></asp:Literal></a>
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
            </div>

            <div class="container">
                <div class="row divCategory">
                    <div class="package-item-holder">

                        <asp:Repeater runat="server" ID="rptResult" OnItemDataBound="rptResult_ItemDataBound">
                            <ItemTemplate>
                                <div class=" col-sm-6 col-xs-6 col-md-3">
                                    <div class="row gdl-package-widget mb14">
                                        <div class="package-content-wrapper">
                                            <div class="package-media-wrapper gdl-image">
                                                <a runat="server" id="hypImg" class="hover-effect">
                                                    <img class="center-block" runat="server" id="img">
                                                    <asp:Literal runat="server" ID="ltrDiscountPercent"></asp:Literal>
                                                </a>
                                            </div>
                                            <div class="detailsbox">
                                                <a runat="server" id="hypTitle">
                                                    <div class="package-title">
                                                        <asp:Literal runat="server" ID="ltrTitle"></asp:Literal>
                                                    </div>
                                                    <asp:Literal runat="server" ID="ltrPrice"></asp:Literal>
                                                    <div class="package-date">
                                                        <i class="icon-time"></i>
                                                        <asp:Literal runat="server" ID="ltrDate"></asp:Literal>
                                                    </div>

                                                    <div class="package-content">
                                                        <asp:Literal runat="server" ID="ltrBrief"></asp:Literal>
                                                    </div>
                                                </a>
                                            </div>
                                            <div class="clear"></div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>

                    <!--Padding-->
                    <div class="gdl-pagination">
                        <cc:Pager ID="pager" runat="server" EnableViewState="true" OnCommand="pager_Command" CompactModePageCount="10" MaxSmartShortCutCount="0" RTL="False" PageSize="9" />

                    </div>
                </div>
                <div class="clear"></div>

            </div>
        </div>
    </div>
</div>

