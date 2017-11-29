<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Blog.ascx.cs" Inherits="Cb.Web.Pages.BlogManagement.Blog" %>

<!--Blog-->
<%@ Register Namespace="Cb.WebControls" Assembly="Cb.WebControls" TagPrefix="cc" %>
<%@ Register TagPrefix="dgc" TagName="top_menu" Src="~/Controls/top_menu.ascx" %>

<div class="header-outer-wrapper " runat="server" id="divBoxTop">
    <dgc:top_menu ID="top_menu" runat="server" />
    <asp:Literal runat="server" ID="ltrHeaderCategory"></asp:Literal>

</div>

<div class="content-outer-wrapper divBlog">
    <div class="content-wrapper container main ">
        <div class="page-wrapper archive-page ">
            <div class="row gdl-page-row-wrapper">
                <div class="gdl-page-left mb0 twelve columns">
                    <div class="row">
                        <div class="gdl-page-item mb0 pb20 twelve columns">
                            <div id="blog-item-holder" class="blog-item-holder">
                                <div class="row">
                                    <asp:Repeater runat="server" ID="rptResult" OnItemDataBound="rptResult_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="three columns gdl-blog-widget">
                                                <div class="blog-content-wrapper">
                                                    <div class="blog-media-wrapper gdl-image">
                                                        <a runat="server" id="hypImg">
                                                            <img runat="server" id="img" style="opacity: 1;"></a>
                                                    </div>
                                                    <h2 class="blog-title"><a runat="server" id="hypTitle">
                                                        <asp:Literal runat="server" ID="ltrTitle"></asp:Literal></a></h2>
                                                    <div class="blog-info-wrapper">
                                                        <div class="blog-date">
                                                            <a runat="server" id="hypDate">
                                                                <asp:Literal runat="server" ID="ltrDate"></asp:Literal></a>

                                                        </div>
                                                        <div class="clear"></div>
                                                    </div>
                                                    <div class="blog-content">
                                                        <asp:Literal runat="server" ID="ltrBrief"></asp:Literal>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <div class="clear"></div>
                                </div>
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
                <div class="clear"></div>
            </div>
            <div class="clear"></div>
        </div>
        <!-- page wrapper -->
    </div>
    <!-- content wrapper -->
</div>
