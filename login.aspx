<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterLayout/MasterPage_login.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="container">

		<hgroup id="login-title" class="large-margin-bottom">
			<h1 class="login-title-image">IPMS 2.0 智慧型停車場管理系統</h1>
			<h5>&copy; 2021 Powered by 500net</h5>
		</hgroup>
 
			<ul class="inputs black-input large">
				<!-- The autocomplete="off" attributes is the only way to prevent webkit browsers from filling the inputs with yellow -->
				<li><span class="icon-user mid-margin-right"></span><input type="text" name="login" id="login" value="" class="input-unstyled" placeholder="Login" autocomplete="off"></li>
				<li><span class="icon-lock mid-margin-right"></span><input type="password" name="pass" id="pass" value="" class="input-unstyled" placeholder="Password" autocomplete="off"></li>
			</ul>

			<button type="submit" class="button glossy full-width huge" >Login</button>
	 
            
	</div>
	
</asp:Content>

