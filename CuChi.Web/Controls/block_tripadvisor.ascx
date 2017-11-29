<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_tripadvisor.ascx.cs" Inherits="Cb.Web.Controls.block_tripadvisor" %>

<!--block_tripadvisor-->
<%@ Register TagPrefix="dgc" TagName="block_googlemap" Src="~/Controls/block_googlemap.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_featuredvideo" Src="~/Controls/block_featuredvideo.ascx" %>


<div class="color-open-section divtripadvisor" id="color-open-section-1" style="background-color: #fff; border-top: 1px solid #ebebeb; border-bottom: 1px solid #ebebeb; padding-top: 30px;">
    <div class="container">
        <div class="row">
            <div class="col-sm-3 columns feature-media-item-class feature-media-item-class-2 ">
                <div class="gdl-header-wrapper ">
                    <i class="fa fa-tripadvisor"></i>
                    <h3 class="gdl-header-title">Tripadvisor</h3>
                </div>
                <div class="feature-media-wrapper">
                    <div class="feature-media-thumbnail">
                        <div id="TA_selfserveprop468" class="TA_selfserveprop">
                            <ul id="M78OzfLSfL" class="TA_links rW5y5p">
                                <li id="FnJhA5E" class="3uMPqhLD5YzE">
                                    <a target="_blank" href="https://www.tripadvisor.co.uk/">
                                        <img src="https://www.tripadvisor.co.uk/img/cdsi/img2/branding/150_logo-11900-2.png" alt="TripAdvisor" /></a>
                                </li>
                            </ul>
                        </div>
                        <script src="https://www.jscache.com/wejs?wtype=selfserveprop&amp;uniq=468&amp;locationId=2005826&amp;lang=en_UK&amp;rating=true&amp;nreviews=5&amp;writereviewlink=true&amp;popIdx=true&amp;iswide=false&amp;border=true&amp;display_version=2"></script>


                        <%-- <a href="https://www.tripadvisor.co.uk/Attraction_Review-g293925-d2005826-Reviews-Cu_Chi_Tunnels-Ho_Chi_Minh_City.html" target="_blank">
                            <img src="/Images/coe_certificate_0.png" alt="Tripadvisor winner" /></a>--%>
                    </div>
                    <%--<div class="feature-media-content-wrapper">
                        <h4 class="feature-media-title"></h4>
                        <div class="feature-media-content">
                            <ul class="shortcode-list">
                                <asp:Repeater runat="server" ID="rptLeft">
                                    <ItemTemplate>
                                        <li><i class="fa fa-tag"></i><a runat="server" id="hypTitle">
                                            <asp:Literal runat="server" ID="ltrTitle"></asp:Literal></a> </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>--%>
                    <div class="clear"></div>
                </div>
            </div>
            <div class="col-sm-9 columns testimonial-item-class testimonial-item-class-3 divTrips">

                <div class="gdl-header-wrapper navigation-on">
                    <i class="fa fa-map-signs"></i>
                    <h3 class="gdl-header-title">Googlemap</h3>
                </div>
                <div class="gdl-carousel-testimonial mb30">
                    <div class="testimonial-item-wrapper">
                        <div class="w-map">
                            <dgc:block_googlemap ID="block_googlemap" runat="server" />
                        </div>
                    </div>
                </div>

                <div class="col-sm-6">
                    <div class="gdl-header-wrapper navigation-on">
                        <i class="icon-comment-alt"></i>
                        <h3 class="gdl-header-title">What Client Say</h3>
                    </div>

                    <div class="gdl-carousel-testimonial">
                        <div class="testimonial-item-wrapper">
                            <asp:Repeater runat="server" ID="rptRight" OnItemDataBound="rptRight_ItemDataBound">
                                <ItemTemplate>
                                    <div class="testimonial-item">
                                        <div class="testimonial-content">
                                            <div class="testimonial-inner-content">
                                                <div class="quote">
                                                    <span style="color: #194eb0;"><em>
                                                        <asp:Literal runat="server" ID="ltrTitle"></asp:Literal>”</em></span>
                                                </div>
                                                <div class="rating reviewItemInline">
                                                    <span class="ratingDate">
                                                        <asp:Literal runat="server" ID="ltrDate"></asp:Literal></span>
                                                </div>
                                                <div class="entry">
                                                    <p id="review_254623948">
                                                        <asp:Literal runat="server" ID="ltrBrief"></asp:Literal>
                                                    </p>
                                                    <p>
                                                        <a runat="server" id="hypTitle" class="more-link">(more&hellip;)
                                                        </a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="testimonial-gimmick"></div>
                                        <div class="clear"></div>
                                        <div class="testimonial-info">
                                            <div class="testimonial-navigation"></div>
                                            <span class="testimonial-author hidden">Mark Clyde</span>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                        </div>
                    </div>
                </div>

                <!--Video--->
                <div class="col-sm-6">
                    <dgc:block_featuredvideo ID="block_featuredvideo" runat="server" />
                </div>
                <!--Video--->


            </div>
          
            <div class="clear"></div>
        </div>
    </div>
    <div class="clear"></div>
</div>
<!--/block_tripadvisor-->
