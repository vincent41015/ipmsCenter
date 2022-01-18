<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterLayout/MasterPage_data.master"
    AutoEventWireup="true" CodeFile="data5.aspx.cs" Inherits="data5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content_content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <hgroup id="main-title" class="thin">
        <h1>
            管理報表</h1>
    </hgroup>
    <div class="with-padding">
        
        <a href="System_System/Report3.aspx" class="button huge "><span class="button-icon">
            <span class="icon-movie"></span></span>系 統 操 作 查 詢</a>
        <br />
        <br />
        <a href="System_System/Report4.aspx" class="button huge "><span class="button-icon">
            <span class="icon-movie"></span></span>設 備 運 作 狀 態</a>
        <br />
        <br />
        <a href="System_System/EmailSetting.aspx" class="button huge "><span class="button-icon">
            <span class="icon-movie"></span></span>Email 設定</a>
        <br />
        <br />
        
        <a   href="javascript:history.back()" class="button icon-reply" >上一頁</a>  <a href="javascript:window.history.forward()" class="button icon-fwd" >下一頁</a>  
    </div>
</asp:Content>
<asp:Content ID="Content_shortcut" ContentPlaceHolderID="ContentPlaceHolder_shortcut"
    runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" runat="Server">

</asp:Content>
