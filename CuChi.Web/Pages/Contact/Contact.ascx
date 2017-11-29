<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contact.ascx.cs" Inherits="Cb.Web.Pages.Contact.Contact" %>

<!--Contact-->
<%@ Register TagPrefix="dgc" TagName="block_googlemap" Src="~/Controls/block_googlemap.ascx" %>
<%@ Register TagPrefix="dgc" TagName="top_menu" Src="~/Controls/top_menu.ascx" %>

<div class="header-outer-wrapper " runat="server" id="divBoxTop">
    <dgc:top_menu ID="top_menu" runat="server" />
    <asp:Literal runat="server" ID="ltrHeaderCategory"></asp:Literal>
</div>

<div class="content-outer-wrapper mb31" id="divDetail">
    <div class="content-wrapper container ">
        <div class="page-wrapper single-blog single-sidebar right-sidebar">
            <div class="row gdl-page-row-wrapper">
                <div class="gdl-page-left mb0 twelve columns ">
                    <div class="row">
                        <div class="gdl-page-item mb0 pb20 gdl-package-full twelve columns">
                            <div class="package-media-wrapper gdl-image">
                                <h3 class="heading">Contact with us</h3>
                            </div>
                        </div>
                    </div>

                    <div class="w-map mb40">
                        <dgc:block_googlemap ID="block_googlemap" runat="server" />
                    </div>


                    <div class="row">
                        <div class="twelve columns package-item-class package-item-class-0 mb40 divCategory">
                            <div class="package-item-holder">
                              

                                    <section id="layers-widget-column-40" class="widget layers-content-widget row content-vertical-massive lien-he-form ">
                                        <div class=" clearfix">
                                            <div class="section-title clearfix medium text-left ">
                                            </div>
                                        </div>
                                        <div class="list-grid">

                                            <div id="layers-widget-column-40-877" class="six layers-masonry-column layers-widget-column-877 span-4  column ">

                                                <div class="media image-top medium">

                                                    <div class="media-body text-left">
                                                        <div class="excerpt">
                                                            <div class="content-contact">
                                                                <asp:Literal runat="server" ID="ltrAddress"></asp:Literal>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="layers-widget-column-40-248" class="six layers-masonry-column layers-widget-column-248 span-8  column ">

                                                <div class="media image-top medium">

                                                    <div class="media-body text-left">
                                                        <div class="excerpt">
                                                            <div role="form" class="wpcf7" id="wpcf7-f137-o1" lang="vi" dir="ltr">
                                                                <div class="screen-reader-response"></div>


                                                                <p class="contact-text">
                                                                    <span class="wpcf7-form-control-wrap your-name">
                                                                        <input runat="server" id="txtFullName" type="text" name="your-name" value="" size="40" class="form-control wpcf7-text wpcf7-validates-as-required" aria-required="true" aria-invalid="false" placeholder="Full Name"></span>
                                                                </p>
                                                                <p class="contact-text">
                                                                    <span class="wpcf7-form-control-wrap your-email">
                                                                        <input runat="server" id="txtEmail" name="your-email" value="" size="40" class="form-control wpcf7-text wpcf7-email wpcf7-validates-as-required wpcf7-validates-as-email" aria-required="true" aria-invalid="false" placeholder="Email"></span>
                                                                </p>
                                                                <p class="contact-text">
                                                                    <span class="wpcf7-form-control-wrap your-subject">
                                                                        <input type="text" name="your-subject" value="" size="40" class="form-control wpcf7-text" aria-invalid="false" placeholder="Subject"></span>
                                                                </p>
                                                                <p class="contact-text-area">
                                                                    <span class="wpcf7-form-control-wrap your-message">
                                                                        <textarea runat="server" id="txtMessage" cols="40" rows="10" maxlength="1000" minlength="10" class="form-control wpcf7-textarea" aria-invalid="false" placeholder="Content"></textarea></span>
                                                                </p>
                                                                <p>
                                                                    <asp:Button ID="btnSend" runat="server" CssClass="wpcf7-form-control wpcf7-submit" Text="Send contact"
                                                                        OnClick="btnSend_ServerClick" />

                                                                </p>


                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </section>

                               

                            </div   >
                            <div class="clear"></div>
                        </div>
                        <div class="clear"></div>
                    </div>
                </div>

            </div>
            <div class="clear"></div>
        </div>
    </div>
</div>


<div class="content-outer-wrapper">
    <div class="page-full-wrapper">

        <div id="post-4312" class="post-4312 page type-page status-publish hentry">
            <div class="page-wrapper single-page ">
                <div class="">

                    <div class="container">
                    </div>
                </div>
                <!-- gdl page item -->

            </div>
            <!-- page wrapper -->
        </div>

    </div>
</div>
