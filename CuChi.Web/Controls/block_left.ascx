<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_left.ascx.cs" Inherits="Cb.Web.Controls.block_left" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!--block_left-->
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        
    </ContentTemplate>
</asp:UpdatePanel>

<cc1:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel1"
    runat="server">
    <Animations>
        <OnUpdating>
            <Parallel duration="0">
                <%--<ScriptAction Script="OnUpdating();" />--%>
                <EnableAction AnimationTarget="chk_CheckedChanged" Enabled="false" />       
                                     
            </Parallel>
        </OnUpdating>    
        <OnUpdated>
            <Parallel duration="0">
                <%--<ScriptAction Script="OnUpdated();" />--%>
                <EnableAction AnimationTarget="chk_CheckedChanged" Enabled="true" />                    
            </Parallel>
        </OnUpdated>     
    </Animations>
</cc1:UpdatePanelAnimationExtender>
<!--/block_left-->
