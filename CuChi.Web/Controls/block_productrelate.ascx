<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_productrelate.ascx.cs"
    Inherits="Cb.Web.Controls.block_productrelate" %>
<!--block_productrelate-->
<div class="widget travelo-box">
    <h3 class="widgettitle"><i class="fa fa-random fa-fw"></i>Related Tours</h3>
    <div class="image-box style14">

        <asp:Repeater runat="server" ID="rptResult" OnItemDataBound="rptResult_ItemDataBound">
            <ItemTemplate>
                <article class="box">
                    <figure>
                        <a runat="server" id="hypImg">
                            <img width="298" height="199" runat="server" id="img"
                                class="attachment-thumbnail size-thumbnail wp-post-image">
                        </a>
                    </figure>
                    <div class="details">
                        <h3 class="title1">
                            <a runat="server" id="hypTitle">
                                <asp:Literal runat="server" ID="ltrTitle"></asp:Literal></a>
                        </h3>
                    </div>
                </article>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
