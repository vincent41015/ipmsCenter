<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterLayout/MasterPage_data.master"
    AutoEventWireup="true" CodeFile="data6.aspx.cs" Inherits="data6" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content_content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <hgroup id="main-title" class="thin">
        <h1>
            權限管理</h1>
    </hgroup>
    <div class="with-padding">
        <h3>
            <a href="#" onclick="menulink('data6_1.aspx');" class="button huge" title="使用者編輯"><span class="button-icon"><span
                class="icon-page"></span></span>
                使用者編輯</a><br /><br />
            <a href="#" onclick="menulink('data6_2.aspx');" class="button huge" title="角色編輯"><span class="button-icon"><span
                class="icon-page"></span></span>
                角色編輯</a><br />
        </h3>
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
    <!-- This is optional -->
    <footer id="menu-footer" style="display:none" >
        <p class="button-height">
            <input type="checkbox" name="auto-refresh" id="auto-refresh" checked="checked" class="switch float-right">
            <label for="auto-refresh">
                Auto-refresh</label>
        </p>
    </footer>
</asp:Content>
