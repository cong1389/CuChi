<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Picture.ascx.cs" Inherits="Cb.Web.Pages.GalleryManagement.Picture" %>

<!--Gallery-->
<%@ Register Namespace="Cb.WebControls" Assembly="Cb.WebControls" TagPrefix="cc" %>
<%@ Register TagPrefix="dgc" TagName="top_menu" Src="~/Controls/top_menu.ascx" %>

<div class="header-outer-wrapper no-top-slider" runat="server" id="divBoxTop">
    <dgc:top_menu ID="top_menu" runat="server" />
    <asp:Literal runat="server" ID="ltrHeaderCategory"></asp:Literal>

</div>
<div class="content-outer-wrapper">
    <div class="page-full-wrapper">

        <div id="post-4312" class="post-4312 page type-page status-publish hentry">
            <div class="page-wrapper single-page ">
                <div class="gdl-page-item">
                    <div class="container">
                        <div class="row">
                            <div class="twelve columns mb0">
                                <div class="gdl-page-content"></div>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                    <div class="container">
                        <div class="row">

                            <h3 class="widgettitle col-xs-12">
                                <asp:Literal runat="server" ID="ltrHeader"></asp:Literal></h3>
                            <div class="clearfix"></div>
                            <div class="gallery1 mb17 photobox-lightbox">
                                <asp:Repeater runat="server" ID="rptImg" OnItemDataBound="rptImg_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="col-md-3 col-sm-3 wl-gallery">
                                            <div style="box-shadow: 0 0 6px rgba(0,0,0,.7);">
                                                <div class="b-link-flow b-animate-go">
                                                    <img runat="server" id="img" class="gall-img-responsive" alt="Kittiwakes">
                                                    <div class="b-wrapper">
                                                        <p class="b-from-right b-animate b-delay03">
                                                            <a runat="server" id="hypImgThumb" >
                                                                <i class="fa fa-play-circle fa-3x"></i>
                                                                <img runat="server" id="imgThumb" class="gall-img-responsive" style="display: none !important; visibility: hidden" alt="Kittiwakes">
                                                            </a>
                                                        </p>
                                                    </div>
                                                </div>
                                                <!--Gallery Label-->
                                                <div class="lbsp_home_portfolio_caption hidden">
                                                    <h3>
                                                        <asp:Literal runat="server" ID="ltrTitle"></asp:Literal>
                                                    </h3>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>

                            <!-- Photo box-->
                            <script>
                                $('.photobox-lightbox').photobox('a');
                                // or with a fancier selector and some settings, and a callback:
                                $('.photobox-lightbox').photobox('a:first', { thumbs: false, time: 0 }, imageLoaded);
                                function imageLoaded() {
                                    console.log('image has been loaded...');
                                }
                            </script>

                            <div class="clear"></div>
                            <h3 class="widgettitle col-xs-12">Similar Pictures</h3>

                            <asp:Repeater runat="server" ID="rptResult" OnItemDataBound="rptResult_ItemDataBound">
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
                                            <div class="detailsbox hidden">
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
                    </div>
                </div>
                <!-- gdl page item -->
                <div class="clear"></div>

                <!--Padding-->
                <cc:Pager ID="pager" runat="server" EnableViewState="true" OnCommand="pager_Command" CompactModePageCount="10" MaxSmartShortCutCount="0" RTL="False" PageSize="9" />

            </div>
            <!-- page wrapper -->
        </div>


    </div>
</div>
