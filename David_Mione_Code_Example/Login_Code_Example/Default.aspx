<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Login_Code_Example.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>

    <link href="css/styles.css" rel="stylesheet" />

</head>

<script src="Scripts/jquery-3.3.1.js"></script>


<body>
    <form id="form1" runat="server">

    <nav>
			<a href="#">Home</a>
			<a href="About.aspx">About</a>
			<a href="Contact.aspx">Contact</a>
    </nav>

    <div id="buffer1"></div>

<div id="section0" style="background-color:aliceblue; position:relative; height:100%; width:100%;">

    <div class="center text">

        <div class="divRow0">
            <h3 style="margin:0; Calibri; color:gray; ">Account Login</h3>
        </div>


        <div  class="divRow1">

            <div class="flex-container">

                <div class="flex-left" style="padding-top:3px;">
                    <asp:Label ID="lblEmailAdd" runat="server" Text="Email Address" Font-Size="Large"></asp:Label>
                </div>

                <div class="flex-right">
                    <asp:TextBox ID="txtEmailAddress" runat="server" Height="21px" Width="310px" CssClass="textboxesrounded" Wrap="False"></asp:TextBox>
                </div>
            </div>
        </div>


        <div class="divRow2">

            <div class="flex-container">

                <div class="flex-left" style="padding-top:3px;">
                    <asp:Label ID="lblPassword" runat="server" Text="Password" Font-Size="Large"></asp:Label>
                </div>

                <div class="flex-right">
                    <asp:TextBox ID="txtPassword" runat="server" Height="21px" Width="310px" CssClass="textboxesrounded" Wrap="False" TextMode="Password"></asp:TextBox>
                </div>

            </div>

        </div>




         <div class="divRow3">

            <div class="flex-container">
            
                <div class="flex-left" style="text-align:left; width:25%; padding:0 0 0 18px;">
                     <asp:Button ID="btnClear" runat="server" Text="Clear" BorderStyle="Solid" Height="29px" Width="88px" CssClass="LoginResetButton" Font-Size="Large" OnClick="btnClear_Click" CausesValidation="False" />
                </div>

                <div class="flex-right" style="width:75%;">
                     <asp:Button ID="btnLogin" runat="server" Text="Login" BorderStyle="Solid" Height="29px" Width="100%" CssClass="LoginResetButton" Font-Size="Large" OnClick="btnLogin_Click" />
                </div>

            </div>
             
        </div>
   </div>
</div>





<div id="section1" style="background-color:blanchedalmond; position:relative; height:100%; width:100%;" hidden="hidden">

    <div class="center text">

        <div class="divRow0">
            <h3 style="margin:0; Calibri; color:gray; ">Account Sign Up</h3>
        </div>

        <div  class="divRow1">

            <div class="flex-container">

                <div class="flex-left" style="padding-top:3px;">
                    <asp:Label ID="lblEmail2" runat="server" Text="Email Address" Font-Size="Large"></asp:Label>
                </div>

                <div class="flex-right">
                    <asp:TextBox ID="txtCreateEmail" runat="server" Height="21px" Width="310px" CssClass="textboxesrounded" Wrap="False"></asp:TextBox>
                </div>

            </div>

        </div>

        <div class="divRow2">

            <div class="flex-container">

                <div class="flex-left" style="padding-top:3px;">
                    <asp:Label ID="password2" runat="server" Text="Password" Font-Size="Large"></asp:Label>
                </div>

                <div class="flex-right">
                    <asp:TextBox ID="txtCreatePassword" runat="server" Height="21px" Width="310px" CssClass="textboxesrounded" Wrap="False" TextMode="Password"></asp:TextBox>
                </div>

            </div>

        </div>
        
         <div class="divRow3">

            <div class="flex-container">
            
                <div class="flex-left" style="text-align:left; width:25%; padding:0 0 0 18px;">
                     </div>

                <div class="flex-right" style="width:75%;">
                     <asp:Button ID="btnSubmitCreateAccount" runat="server" Text="Submit" BorderStyle="Solid" Height="29px" Width="100%" CssClass="LoginResetButton" Font-Size="Large" OnClick="btnSubmitCreateAccount_Click" />
                </div>

            </div>
             
        </div>
   </div>
</div>






    <div id="buffer2"></div>

    <footer class="footer">
				<p>© Thank you for reviewing my sample code - David Mione . <a href="#">Privacy</a> . <a href="#">Terms</a></p>
	</footer>
			
			

    </form>

        <div style="position:absolute; top:180px; left:160px;">
                 <button id="btnSignUp" class="LoginResetButton">Sign Up</button>
        </div>

        <div style="position:absolute; top:180px; left:160px;">
                 <button id="btnGoToLogin" hidden="hidden" class="LoginResetButton">Go To Login</button>
        </div>
    
<script>
    $("#btnSignUp").click(function () {
        $("#section0").hide();
        $("#section1").show();
        $("#btnSignUp").hide();
        $("#btnGoToLogin").show();
    });

    $("#btnGoToLogin").click(function () {
        $("#section1").hide();
        $("#section0").show();
        $("#btnSignUp").show();
        $("#btnGoToLogin").hide();
    });

</script> 

</body>
</html>
