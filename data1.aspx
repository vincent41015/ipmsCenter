<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterLayout/MasterPage_data.master"
    AutoEventWireup="true" CodeFile="data1.aspx.cs" Inherits="data1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content_content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <hgroup id="main-title" class="thin">
			<h1>警報頁面</h1> 

		</hgroup>
    
    <div class="with-padding">
        <h3>
         <a href="Alarm/Default.aspx" class="button huge " ><span class="button-icon"><span class="icon-movie"></span></span>警報設備地圖</a> <br /> <br />

         <a href="Alarm/Default2.aspx" class="button huge " ><span class="button-icon"><span class="icon-movie"></span></span>觸發中警報設備</a> <br /> <br />
         
         </h3>    
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
