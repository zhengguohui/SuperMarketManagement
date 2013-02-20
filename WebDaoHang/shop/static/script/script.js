// JavaScript Document
document.getElementById('loginuseremail').focus();

function CheckLogin()
{
	document.getElementById('emailerror').style.display="none";
	document.getElementById('passworderror').style.display="none";
	if(document.getElementById('loginuseremail').value=="")
	{
		document.getElementById('emailerror').style.display="block";
		document.getElementById('loginuseremail').focus();
		return false;
	}
	if(document.getElementById('loginuserpassword').value=="")
	{
		document.getElementById('passworderror').style.display="block";
		document.getElementById('loginuserpassword').focus();
		return false;
	}
}
function CheckSignup()
{
	document.getElementById('emailerror1').style.display="none";
	document.getElementById('passworderror1').style.display="none";
	document.getElementById('passworderror2').style.display="none";
	if(document.getElementById('signupuseremail').value=="")
	{
		document.getElementById('emailerror1').style.display="block";
		document.getElementById('signupuseremail').focus();
		return false;
	}
	if(document.getElementById('signupuserpassword').value=="")
	{
		document.getElementById('passworderror1').style.display="block";
		document.getElementById('signupuserpassword').focus();
		return false;
	}
	if(document.getElementById('signupuserpassword').value!=document.getElementById('signupreuserpassword').value)
	{
		document.getElementById('passworderror2').style.display="block";
		document.getElementById('signupreuserpassword').focus();
		return false;
	}
}