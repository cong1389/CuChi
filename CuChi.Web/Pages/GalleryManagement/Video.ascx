<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Video.ascx.cs" Inherits="Cb.Web.Pages.GalleryManagement.Video" %>


<!--Video-->
<%@ Register Namespace="Cb.WebControls" Assembly="Cb.WebControls" TagPrefix="cc" %>
<%@ Register TagPrefix="dgc" TagName="top_menu" Src="~/Controls/top_menu.ascx" %>

<div class="header-outer-wrapper no-top-slider" runat="server" id="divBoxTop">
    <dgc:top_menu ID="top_menu" runat="server" />
    <asp:Literal runat="server" ID="ltrHeaderCategory"></asp:Literal>

</div>

<div class="">
    <div class="page-full-wrapper">

        <div id="divVideo" class="post-4312 page type-page status-publish hentry">
            <div class="page-wrapper single-page ">
                <div class="gdl-page-item">
                    <!--Video play-->
                    <div class="container">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="gdl-page-content mb31">
                                    <div class="fluid-width-video-wrapper">
                                        <iframe runat="server" id="ifrTop" width="100%" height="500" allowfullscreen></iframe>
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                    <!--/Video play-->

                    <!--Video release-->
                    <div class="container">
                        <div class="row">
                            <div class="divVideo mb31">
                                <div class="package-item-holder">

                                    <asp:Repeater runat="server" ID="rptVideo" OnItemDataBound="rptVideo_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="col-sm-6 col-md-4">
                                                <div class="videobox gdl-package-widget">
                                                    <div class="embed-responsive embed-responsive-16by9">
                                                        <a runat="server" id="hypImg">
                                                            <img runat="server" id="img">
                                                            <div class="package-ribbon-wrapper">
                                                                <div class="clear"></div>
                                                            </div>
                                                        </a>
                                                    </div>
                                                    <div class="detailsbox">
                                                        <a runat="server" id="hypTitle">
                                                            <h2 class="package-title">
                                                                <asp:Literal runat="server" ID="ltrTitle"></asp:Literal></h2>
                                                        </a>
                                                    </div>

                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>                                    
                                </div>
                                <div class="clear"></div>

                                <!--Padding-->
                                <div class="gdl-pagination">
                                    <cc:Pager ID="pager" runat="server" EnableViewState="true" OnCommand="pager_Command" CompactModePageCount="10" MaxSmartShortCutCount="0" RTL="False" PageSize="9" />
                                </div>

                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                    <!--/Video release-->

                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
</div>

