<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterLayout/MasterPage_data.master"
    AutoEventWireup="true" CodeFile="data3.aspx.cs" Inherits="data3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content_content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <hgroup id="main-title" class="thin">
        <h1>廠商門禁管理系統</h1>
    </hgroup>
    <%--<p class="wrapped left-icon icon-info-round">
        廠商門禁管理系統
    </p>--%>
    <div class="with-padding">
        
            <a href="#" onclick="menulink('System_Vendor/VendorMember.aspx');" class="button huge" title="廠商管理">
                <span class="button-icon"><span class="icon-page"></span></span>廠商管理</a><br />
            <br />
            <a href="#" onclick="menulink('System_Vendor/VendorBlueCard.aspx');" class="button huge" title="藍卡進出紀錄">
                <span class="button-icon"><span class="icon-page"></span></span>藍卡進出紀錄</a><br />
            <br />
            <a href="#" onclick="menulink('System_Vendor/VendorError.aspx');" class="button huge" title="廠商異常進出紀錄">
                <span class="button-icon"><span class="icon-page"></span></span>廠商異常進出紀錄</a><br />
            <br />
			<a href="#" onclick="menulink('System_Vendor/VendorCounter.aspx');" class="button huge" title="使用天數">
                <span class="button-icon"><span class="icon-page"></span></span>使用天數</a><br />
            <br />
			<a href="#" onclick="menulink('System_Vendor/VendorOTCRReport.aspx');" class="button huge" title="逾時">
                <span class="button-icon"><span class="icon-page"></span></span>汽車逾時</a><br />
            <br />
			<a href="#" onclick="menulink('System_Vendor/VendorIllegalReport.aspx');" class="button huge" title="違停">
                <span class="button-icon"><span class="icon-page"></span></span>汽車違停</a><br />
            <br />
            <a href="#" onclick="menulink('System_Vendor/VendorOTCRReportMoto.aspx');" class="button huge" title="逾時">
                <span class="button-icon"><span class="icon-page"></span></span>機車逾時</a><br />
            <br />
			<a href="#" onclick="menulink('System_Vendor/VendorIllegalReportMoto.aspx');" class="button huge" title="違停">
                <span class="button-icon"><span class="icon-page"></span></span>機車違停</a><br />
            <br />
            <a href="#" onclick="menulink('System_Vendor/InOutDetailCar.aspx');" class="button huge" title="違停">
                <span class="button-icon"><span class="icon-page"></span></span>廠商汽車出入資料查詢</a><br />
            <br />
            <a href="#" onclick="menulink('System_Vendor/InOutDetailMoto.aspx');" class="button huge" title="違停">
                <span class="button-icon"><span class="icon-page"></span></span>廠商機車出入資料查詢</a><br />
            <br />
            <a href="#" onclick="menulink('System_Vendor/VendorLimitTime.aspx');" class="button huge"
                title="尖峰時段設定"><span class="button-icon"><span class="icon-page"></span></span>尖峰時段設定</a><br />
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
