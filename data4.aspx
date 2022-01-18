<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterLayout/MasterPage_data.master"
    AutoEventWireup="true" CodeFile="data4.aspx.cs" Inherits="data4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content_content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <hgroup id="main-title" class="thin">
        <h1>
            停車場系統</h1>
    </hgroup>
    <div class="with-padding">
	<a href="#" onclick="menulink('System_Park/Template_Map_Plane.aspx?t=停車場在席平面圖&c=平面圖&sc=底圖');" class="button huge" title="停車場在席平面圖">
            <span class="button-icon"><span class="icon-page"></span></span>停車場在席平面圖</a><br />
        <br />
        <a href="#" onclick="menulink('System_Park/Default.aspx');" class="button huge" title="停車場監控">
            <span class="button-icon"><span class="icon-page"></span></span>停車場監控</a><br />
        <br />
		<a href="#" onclick="menulink('System_Park/FlowDay.aspx');" class="button huge"
            title="流量日報表"><span class="button-icon"><span class="icon-page"></span></span>
            流量日報表</a><br />
        <br />
		<a href="#" onclick="menulink('System_Park/FlowMonth.aspx');" class="button huge"
            title="流量月報表"><span class="button-icon"><span class="icon-page"></span></span>
            流量月報表</a><br />
        <br />
		<a href="#" onclick="menulink('System_Park/Flowyear.aspx');" class="button huge"
            title="流量年報表"><span class="button-icon"><span class="icon-page"></span></span>
            流量年報表</a><br />
        <br />
		
        
        <a href="#" onclick="menulink('System_Park/FlowDayinfrared.aspx');" class="button huge"
            title="汽車-紅外線流量統計"><span class="button-icon"><span class="icon-page"></span></span>
            汽車-紅外線流量統計</a><br />
        <br />
        <a href="#" onclick="menulink('System_Park/FlowDayETag.aspx');" class="button huge"
            title="汽車-ETag流量統計"><span class="button-icon"><span class="icon-page"></span></span>
            汽車-ETag流量統計</a><br />
        <br />
        <a href="#" onclick="menulink('System_Park/FlowDayETagMoto.aspx');" class="button huge"
            title="廠商機車ETag流量統計"><span class="button-icon"><span class="icon-page"></span></span>
            廠商機車ETag流量統計</a><br />
        <br />
        <a href="#" onclick="menulink('System_Park/Flow_month_infrared.aspx');" class="button huge"
            title="汽車-紅外線月流量統計"><span class="button-icon"><span class="icon-page"></span></span>
            汽車-紅外線月流量統計</a><br />
        <br />
        <a href="#" onclick="menulink('System_Park/Flow_month_eTag.aspx');" class="button huge"
            title="汽車-ETag月流量統計"><span class="button-icon"><span class="icon-page"></span></span>
            汽車-ETag月流量統計</a><br />
        <br />
        <a href="#" onclick="menulink('System_Park/UnRegisterTag.aspx');" class="button huge"
            title="未登錄eTag紀錄"><span class="button-icon"><span class="icon-page"></span></span>
            未登錄eTag紀錄</a><br />
        <br />
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
    <%-- <footer id="menu-footer">
			<p class="button-height">
				<input type="checkbox" name="auto-refresh" id="auto-refresh" checked="checked" class="switch float-right">
				<label for="auto-refresh">Auto-refresh</label>
			</p>
		</footer>--%>
</asp:Content>
