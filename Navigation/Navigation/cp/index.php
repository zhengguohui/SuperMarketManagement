<?php
$title="修改密码";
$active_changepassword="active";
require_once("../include_config.php");
CheckLogin();
$error1="";
if(isset($_POST["signupbutton"]))
{
	$_SESSION["signupuseremail2"]=$_POST["signupuseremail"];
	$_SESSION["signupuserpassword2"]=$_POST["signupuserpassword"];
	$_SESSION["signupreuserpassword2"]=$_POST["signupreuserpassword"];
	if($_SESSION["signupuseremail2"]=="")
	{
		$error1='<p id="error1" class="alert alert-error">请输入原始密码！</p>';
	}
	else if($_SESSION["signupuserpassword2"]=="")
	{
		$error1='<p id="error1" class="alert alert-error">请输入新设密码！</p>';
	}
	else if($_SESSION["signupuserpassword2"]!=$_SESSION["signupreuserpassword2"])
	{
		$error1='<p id="error1" class="alert alert-error">两次密码输入不同！</p>';
	}
	else
	{
		$sql='select id from smm_user where smm_email="'.$_SESSION["email"].'" and smm_password="'.Password($_SESSION["signupuseremail2"]).'"';
		if(SQLRow(SQLRun($sql))<=0)
		{
			$error1='<p id="error1" class="alert alert-error">原始密码错误！</p>';
		}
		else
		{
			$sql='update smm_user set smm_password="'.Password($_SESSION["signupuserpassword2"]).'" where smm_email="'.$_SESSION["email"].'"';
			SQLRun($sql);
			$_SESSION["signupuseremail2"]="";
			$_SESSION["signupuserpassword2"]="";
			$_SESSION["signupreuserpassword2"]="";
			header('Location: '.SITE_LINK.'my/');
		}
	}
}
require_once("../include_header.php");
?>
<div class="row">
<div class="span3">&nbsp;</div>
<div class="span6">
<div class="box">
<form id="formsignup" name="formsignup" method="post" action="">
<div class="alert alert-info"
	style="padding: 10px; text-align: center; margin-bottom: 20px;">
<h4>修改密码</h4>
</div>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td align="left" valign="top" style="padding-right: 20px;"><img
			src="<?php echo SITE_LINK; ?>static/img/cp.png" /></td>
		<td align="left" valign="top">
		<p><input title="原始密码" style="width: 170px;" name="signupuseremail"
			type="password" id="signupuseremail"
			value="<?php echo @$_SESSION["signupuseremail2"]; ?>" size="5" />&nbsp;&nbsp;原始密码&nbsp;&nbsp;*</p>
		<p><input title="新设密码" style="width: 170px;" name="signupuserpassword"
			type="password" id="signupuserpassword"
			value="<?php echo @$_SESSION["signupuserpassword2"]; ?>" size="5" />&nbsp;&nbsp;新设密码&nbsp;&nbsp;*</p>
		<p><input title="密码重复" style="width: 170px;"
			name="signupreuserpassword" type="password" id="signupreuserpassword"
			value="<?php echo @$_SESSION["signupreuserpassword2"]; ?>" size="5" />&nbsp;&nbsp;密码重复&nbsp;&nbsp;*</p>
		</td>
	</tr>
</table>
<p class="line"></p>
<p style="text-align: center;"><input title="修改" class="btn btn-primary"
	type="submit" name="signupbutton" id="signupbutton" value="修改" />&nbsp;&nbsp;<a
	class="btn btn-primary" href="<?php echo SITE_LINK.'my/'; ?>"
	title="返回">返回</a></p>
<?php echo $error1; ?></form>
</div>
</div>
<div class="span3">&nbsp;</div>
</div>
<script language="javascript" type="text/javascript">
document.getElementById("signupuseremail").focus()();
</script>
<?php
require_once("../include_footer.php");
?>