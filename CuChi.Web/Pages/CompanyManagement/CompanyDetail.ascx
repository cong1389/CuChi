<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyDetail.ascx.cs" Inherits="Cb.Web.Pages.CompanyManagement.CompanyDetail" %>

<!--CompanyDetail company_bg-->

<%@ Register TagPrefix="dgc" TagName="top_menu" Src="~/Controls/top_menu.ascx" %>

<div class="header-outer-wrapper no-top-slider">
    <dgc:top_menu ID="top_menu" runat="server" />
    <asp:Literal runat="server" ID="ltrHeaderCategory"></asp:Literal>
</div>

<div class="content-outer-wrapper mb31" id="divDetail">
    <div class="content-wrapper container main ">
        <div class="page-wrapper single-blog single-sidebar right-sidebar">
            <div class="row gdl-page-row-wrapper">
                <div class="gdl-page-left mb0 twelve columns ">
                    <div class="row">
                        <div class="gdl-page-item mb0 pb20 gdl-package-full twelve columns">
                            <div class="package-media-wrapper gdl-image">
                                <asp:Literal runat="server" ID="ltrDetail"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="clear"></div>
        </div>
    </div>
</div>
