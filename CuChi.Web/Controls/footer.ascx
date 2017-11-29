<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="footer.ascx.cs" Inherits="Cb.Web.Controls.footer" %>

<!--footer-->
<footer class="footer">
    <!--footer-->
    <div class="why hidden-xs">
        <div class="container">
            <div class="row">
                <div class="col-md-12 text-center">
                    <div class="footertitle">WHY TRAVEL WITH US?
                    </div>
                </div>
                <div class="homewhy">
                    <asp:Literal runat="server" ID="ltrWhy"> </asp:Literal>

                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="container footer-line">
        <div class="">
            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 footer-line1"></div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 footer-line1"></div>
        </div>
    </div>

    <div class="container">
        <div class="row hidden-xs">
            <div class="col-md-12">

                <div class="footertitle text-uppercase">
                    Our partner
                </div>
                <div class="connectlink mb30 text-center">
                    <asp:Repeater runat="server" ID="rptPartner" OnItemDataBound="rptPartner_ItemDataBound">
                        <ItemTemplate>
                            <a rel="nofollow" target="_blank" runat="server" id="hypImg">
                                <img runat="server" id="img"></a>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </div>
        </div>

        <div class="row subform mb30 hidden-xs">
            <div class="col-md-7 col-sm-5 ">
                <div class="title text-left">
                    LOOKING FOR INSPIRATION?
                </div>
                <div class="row">

                    <div class="col-md-6 col-sm-8">
                        <input name="ctl04$MenuBottom1$txtEmail" type="text" id="ctl04_MenuBottom1_txtEmail" class="textbox" placeholder="Email" style="width: 100%;">
                    </div>
                    <div class="col-md-6 col-sm-4">
                        <a id="ctl04_MenuBottom1_btnSend" class="button" href="javascript:__doPostBack('ctl04$MenuBottom1$btnSend','')">Send</a>
                    </div>
                </div>
            </div>
            <div class="col-md-5 col-sm-7 text-right">
                <hr class="line2 visible-xs">
                <div class="title text-right">
                    FOLLOW US
                </div>

                <div class="">
                    <div class="mb11">
                        <a href="http://www.facebook.com/cuchitunnelsdailytours/" target="_blank" title="facebook">
                            <img src="/assets/wp-content/themes/bdltourpackage/images/icon/social-icon/facebook.png" alt="facebook" width="40" height="40">
                        </a>
                        <%-- <a href="https://www.flickr.com/photos/VietnamTravelGroup/albums" title="flickr">
                            <img src="/assets/wp-content/themes/bdltourpackage/images/icon/social-icon/flickr.png" alt="flickr" width="40" height="40"></a>--%>

                        <a href="http://twitter.com/CuChiTunnels" target="_blank" title="flickr">
                            <img src="/assets/wp-content/themes/bdltourpackage/images/icon/social-icon/twitter.png" alt="twitter" width="40" height="40">
                        </a>
                        <a href="https://www.youtube.com/channel/UC0BMXIbZ-98L7rQvi7YDE4w" target="_blank" title="youtube">
                            <img src="/assets/wp-content/themes/bdltourpackage/images/icon/social-icon/youtube.png" alt="youtube" width="40" height="40"></a>

                        <a href="https://plus.google.com/u/0/108671436826949868040" target="_blank" title="google_plus">
                            <img src="/assets/wp-content/themes/bdltourpackage/images/icon/social-icon/google-plus.png" alt="google_plus" width="40" height="40">
                        </a>

                        <a href="https://www.pinterest.com/cuchitunnels/" target="_blank" title="pinterest">
                            <img src="/assets/wp-content/themes/bdltourpackage/images/icon/social-icon/pinterest.png" alt="pinterest" width="40" height="40"></a>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-5 col-sm-4 contactinfo">

                <div class="footerlogo text-left">
                    <div class="mb17">
                        <a runat="server" id="hypImgHomePage" class="hidden">
                            <img runat="server" id="imgLogo"></a>
                        <asp:Literal runat="server" ID="ltrAddressFooter"></asp:Literal>
                    </div>
                    <%--  <a href="#">
                        <img src="/images/ISO.png" align="absmiddle"></a>
                    <br />
               <a href="http://www.dmca.com/Protection/Status.aspx?ID=885dc2a8-10ed-45b6-b578-86e112d659ec" target="_blank" title="DMCA.com Protection Status" class="dmca-badge">
                        <img src="//images.dmca.com/Badges/dmca_protected_sml_120n.png?ID=885dc2a8-10ed-45b6-b578-86e112d659ec" alt="DMCA.com Protection Status"></a>--%>
                </div>

            </div>
            <div class="col-md-7 col-sm-8">
                <hr class="line2 visible-xs">
                <div class="row menubottom">

                    <div class="clearfix visible-xs"></div>

                    <asp:Repeater runat="server" ID="rptResult" OnItemDataBound="rptResult_ItemDataBound">
                        <ItemTemplate>
                            <div class="col-md-4 col-sm-6 col-xs-6 col col-md-15">
                                <a runat="server" id="hypName" class="top">
                                    <asp:Literal runat="server" ID="ltrName"></asp:Literal></a>
                                <ul>
                                    <asp:Repeater runat="server" ID="rptSub" OnItemDataBound="rptSub_ItemDataBound">
                                        <ItemTemplate>
                                            <li><a runat="server" id="hypName">
                                                <asp:Literal runat="server" ID="ltrName"></asp:Literal></a>
                                                <ul class="ulSub1">
                                                    <asp:Repeater runat="server" ID="rptSub1" OnItemDataBound="rptSub1_ItemDataBound">
                                                        <ItemTemplate>
                                                            <li><a runat="server" id="hypName">
                                                                <i class="fa fa-angle-right"></i>
                                                                <asp:Literal runat="server" ID="ltrName"></asp:Literal></a></li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </li>

                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </div>
        </div>
        <div class="clear"></div>

    </div>
    <div class="container">
        <hr class="line2">
        <div class="row">

            <div class="text-center copyright2">
                <asp:Literal runat="server" ID="ltrFooter"></asp:Literal>
            </div>
        </div>
    </div>
</footer>

<asp:HiddenField runat="server" ID="hddParentNameUrlFooter" Visible="false" />
