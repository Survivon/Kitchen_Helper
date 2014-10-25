<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="khelper.aspx.cs" Inherits="KHelper.khelper" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="mainform.css" rel="stylesheet"/>
</head>
<body class="back">    
    <form id="form1" runat="server">
    <div class="div">
     <button value="Home" type="button" class="highbutton btn-6d marg">Home</button>                   
           <button value="Recipe" type="button" class="highbutton btn-6d">Recipe</button>
           <button value="Auth" type="button" class=" btn-6d auth">Enter</button>  
            </div>
            <p class="textunderbutton">Kitchen Helper - easy to cook!</p>         
            <button value="Registr" type="button" class="registrbutton btn">Registration</button>    
           <p class="textor">or<button value="next" type="button" class="enterbutton" id="clickenter" >Enter</button></p>    
    </form>
    
</body>
</html>
