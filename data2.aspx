<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterLayout/MasterPage_data.master"
    AutoEventWireup="true" CodeFile="data2.aspx.cs" Inherits="data2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <hgroup id="main-title" class="thin">
        <h1>
            員工資料</h1>
    </hgroup>
    <div class="with-padding"  >
        <a href="System_employee/Default.aspx" class="button huge "><span class="button-icon">
            <span class="icon-movie"></span></span>員工資料</a>
        <br />
        <br />
        <a href="System_employee/Default2.aspx" class="button huge "><span class="button-icon">
            <span class="icon-movie"></span></span>員工出入資料</a>
        <br />
        <br />
        <a href="System_employee/Default3.aspx" class="button huge "><span class="button-icon">
            <span class="icon-movie"></span></span>員工車輛位置查詢</a>
        <br />
        <br />
        <a href="System_employee/OTCRReport.aspx" class="button huge "><span class="button-icon">
            <span class="icon-movie"></span></span>員工逾時查詢</a>
        <br />
        <br />
        <a   href="javascript:history.back()" class="button icon-reply" >上一頁</a>  <a href="javascript:window.history.forward()" class="button icon-fwd" >下一頁</a>  
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_shortcut" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder4" runat="Server">
</asp:Content>
