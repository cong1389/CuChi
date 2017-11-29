<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_right.ascx.cs" Inherits="Cb.Web.Controls.block_right" %>

<!--block_right--->
<!--Customize Tours-->
<div class="contact-box widget travelo-box" runat="server" id="divCustomTour" style="display: none">
    <h3 class="widgettitle"><i class="fa fa-cubes fa-fw"></i>Customize Tours</h3>
    <p>
        <asp:Literal runat="server" ID="ltrCustomTours"></asp:Literal>
    </p>
</div>
<!--/Customize Tours-->

<!--Need Help--->
<div class="contact-box widget travelo-box">
    <h3 class="widgettitle"><i class="fa fa-umbrella fa-fw"></i>Need Help?</h3>
    <p>We would be more than happy to help you. Our team advisor are 24/7 at your service to help you.</p>
    <div class="faq-box">
        <i class="fa fa-crosshairs" style="color: #cc0033;"></i>
        <a href="/contact/vn" rel="nofollow">F.A.Qs</a>
    </div>
    <address class="contact-details">
        <span class="contact-phone"><span class="fa fa-headphones " style="color: #cc0033;"></span>
            <asp:Literal runat="server" ID="ltrPhoneValue"></asp:Literal></span><br>
        <a class="contact-email" runat="server" id="hypEmail" rel="nofollow">
            <span class="fa fa-envelope " style="color: #cc0033;"></span>
            <asp:Literal runat="server" ID="ltrEmail"></asp:Literal></a>
    </address>
</div>
<!--/Need Help--->

<div class="contact-box widget travelo-box">
    <h3 class="widgettitle"><i class="fa fa-book fa-fw"></i>How to book tour ?</h3>
    <p>
        <asp:Literal runat="server" ID="ltrHowToBook"></asp:Literal>
    </p>
</div>

<!--/block_right--->
