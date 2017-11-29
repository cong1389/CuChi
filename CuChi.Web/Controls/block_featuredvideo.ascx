<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_featuredvideo.ascx.cs" Inherits="Cb.Web.Controls.block_featuredvideo" %>

<!--block_featuredvideo-->
<!-- video -->
<div class=" color-open-section divfeaturedvideo">

    <div class="">
        <div class="gdl-header-wrapper navigation-on">
            <i class="icon-comment-alt"></i>
            <h3 class="gdl-header-title">Travel inspiration</h3>
        </div>
        <%-- <div class="">
            <div class="col-md-12 gdl-header-wrapper ">
                <h3 class="hometitle2"><i class="fa fa-file-video-o"></i>Travel inspiration</h3>
            </div>
        </div>--%>
        <div class=" homevideo">

            <asp:Repeater runat="server" ID="rptVideoTop" OnItemDataBound="rptVideoTop_ItemDataBound">
                <ItemTemplate>
                    <div class="item  ">
                        <div class="embed-responsive embed-responsive-16by9 videobox">
                            <iframe runat="server" id="ifrVideo" class="embed-responsive-item" src="https://www.youtube.com/embed/zJJ_tz-_cX4" frameborder="0" allowfullscreen="" height="150"></iframe>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
    </div>
</div>

<!-- /video -->


<%--<div class="container hidden">
    <div class="row">
        <div class="six columns feature-media-item-class feature-media-item-class-4">
            <div class="gdl-header-wrapper ">
                <i class="icon-facetime-video"></i>
                <h3 class="gdl-header-title">Featured Video</h3>
            </div>
            <div class="clear"></div>
            <div class="feature-media-wrapper">
                <div class="feature-media-thumbnail">
                    <div class="fluid-width-video-wrapper">

                        <iframe runat="server" id="ifrTop" width="100%" height="250px" allowfullscreen="true" src="http://www.youtube.com"></iframe>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="feature-media-content-wrapper">
                    <h4 class="feature-media-title">More Cycling Adventure Videos</h4>
                    <div class="feature-media-content">
                        <asp:Repeater runat="server" ID="rptVideo" OnItemDataBound="rptVideo_ItemDataBound">
                            <ItemTemplate>
                                <li><i class="fa fa-angle-double-right"></i><a runat="server" id="hypTitle" target="_blank">
                                    <asp:Literal runat="server" ID="ltrTitle"></asp:Literal>
                                </a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

            </div>
        </div>
        <div class="six columns blog-item-class blog-item-class-5 mb40 divTipTravel">
            <div class="gdl-header-wrapper navigation-on">
                <i class="icon-th-list"></i>
                <h3 class="gdl-header-title">Tips Before Travel</h3>
            </div>
            <div class="blog-item-holder">
                <asp:Repeater runat="server" ID="rptBlog" OnItemDataBound="rptBlog_ItemDataBound">
                    <ItemTemplate>
                        <div class="gdl-blog-list">
                            <div class="blog-medium-media-wrapper">
                                <div class="blog-media-wrapper gdl-image">
                                    <a runat="server" id="hypImg">
                                        <img runat="server" id="img" /></a>
                                </div>
                            </div>
                            <div class="blog-content-wrapper">
                                <h2 class="blog-title"><a runat="server" id="hypTitle">
                                    <asp:Literal runat="server" ID="ltrTitle"></asp:Literal></a></h2>
                                <div class="blog-date">
                                    <a runat="server" id="hypDate"><span class="head">Posted On </span>
                                        <asp:Literal runat="server" ID="ltrDate"></asp:Literal></a>
                                </div>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
            <div class="clear"></div>
        </div>
        <div class="clear"></div>
    </div>
</div>--%>

<!--/block_featuredvideo-->
