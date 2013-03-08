<?php
$title="登录或注册";
$active_index="active";
require_once("include_config.php");
$error1="";
$error2="";
if(isset($_POST["signupbutton"]))
{
	$_SESSION["signupuseremail"]=$_POST["signupuseremail"];
	$_SESSION["signupuserpassword"]=$_POST["signupuserpassword"];
	$_SESSION["signupreuserpassword"]=$_POST["signupreuserpassword"];
	$_SESSION["signupreusertag"]=$_POST["radio"];
	if($_SESSION["signupuseremail"]=="")
	{
		$error1='<p id="error1" class="alert alert-error">请输入正确的邮箱地址！</p>';
	}
	else if($_SESSION["signupuserpassword"]=="")
	{
		$error1='<p id="error1" class="alert alert-error">请输入密码！</p>';
	}
	else if($_SESSION["signupuserpassword"]!=$_SESSION["signupreuserpassword"])
	{
		$error1='<p id="error1" class="alert alert-error">两次密码输入不同！</p>';
	}
	else
	{
		$sql='select id from smm_user where smm_email="'.$_SESSION["signupuseremail"].'"';
		if(SQLRow(SQLRun($sql))>0)
		{
			$error1='<p id="error1" class="alert alert-error">该账户已存在！</p>';
		}
		else
		{
			$sql='insert into smm_user(smm_email,smm_password,smm_tag) values("'.$_SESSION["signupuseremail"].'","'.Password($_SESSION["signupuserpassword"]).'",'.$_SESSION["signupreusertag"].')';
			SQLRun($sql);
			$_SESSION["login"]="yes";
			$_SESSION["email"]=$_SESSION["signupuseremail"];
			$_SESSION["tag"]=$_SESSION["signupreusertag"];
			$_SESSION["signupuseremail"]="";
			$_SESSION["signupuserpassword"]="";
			$_SESSION["signupreuserpassword"]="";
			$_SESSION["signupreusertag"]="";
			if(isset($_SESSION["redirect"]))
			{
				header('Location: '.$_SESSION["redirect"]);
			}
			else
			{
				header('Location: '.SITE_LINK.'my/');
			}
		}
	}
}
if(isset($_POST["buttonlogin"]))
{
	$_SESSION["loginuseremail"]=$_POST["loginuseremail"];
	$_SESSION["loginuserpassword"]=$_POST["loginuserpassword"];
	if($_SESSION["loginuseremail"]=="")
	{
		$error2='<p id="error2" class="alert alert-error">请输入正确的邮箱地址！</p>';
	}
	else if($_SESSION["loginuserpassword"]=="")
	{
		$error2='<p id="error2" class="alert alert-error">请输入密码！</p>';
	}
	else
	{
		$sql='select smm_tag from smm_user where smm_email="'.$_SESSION["loginuseremail"].'" and smm_password="'.Password($_SESSION["loginuserpassword"]).'"';
		$r=SQLRun($sql);
		if(SQLRow($r)>0)
		{
			$_SESSION["login"]="yes";
			$_SESSION["email"]=$_SESSION["loginuseremail"];
			$_SESSION["tag"]=SQLShow($r,0,"smm_tag");
			$_SESSION["loginuseremail"]="";
			$_SESSION["loginuserpassword"]="";
			header('Location: '.SITE_LINK.'my/');
		}
		else
		{
			$error2='<p id="error2" class="alert alert-error">邮箱或者密码错误！</p>';
		}
	}
}
require_once("include_header.php");
?>
<div class="row">
	<div class="span8">
    	<div class="box">
        	<form id="formsignup" name="formsignup" method="post" action="">
				<div class="alert alert-info" style="padding:10px; text-align:center; margin-bottom:10px;"><h4>注册</h4></div>
				<p><input title="邮箱" style="width:170px;" name="signupuseremail" type="text" id="signupuseremail" value="<?php echo @$_SESSION["signupuseremail"]; ?>" size="5" />&nbsp;&nbsp;邮箱&nbsp;&nbsp;*&nbsp;&nbsp;&nbsp;&nbsp;用户类型：&nbsp;&nbsp;<input name="radio" type="radio" id="tag" value="0" checked="checked" title="普通用户"/>&nbsp;&nbsp;普通用户&nbsp;&nbsp;<input type="radio" name="radio" id="tag2" value="1" title="超市用户" />&nbsp;&nbsp;超市用户&nbsp;&nbsp;</p>
				<p><input title="密码" style="width:170px;" name="signupuserpassword" type="password" id="signupuserpassword" value="<?php echo @$_SESSION["signupuserpassword"]; ?>" size="5" />&nbsp;&nbsp;密码&nbsp;&nbsp;*&nbsp;&nbsp;&nbsp;&nbsp;<input title="密码重复" style="width:170px;" name="signupreuserpassword" type="password" id="signupreuserpassword" value="<?php echo @$_SESSION["signupreuserpassword"]; ?>" size="5" />&nbsp;&nbsp;密码重复&nbsp;&nbsp;*</p>
                <p class="line"></p>
				<p><input title="注册" class="btn btn-primary" type="submit" name="signupbutton" id="signupbutton" value="注册" />
				<p id="passworderror1" class="alert alert-error" style="display:none;">请输入密码！</p>
				<p id="emailerror1" class="alert alert-error" style="display:none;">请输入邮箱！</p>
                <p id="passworderror2" class="alert alert-error" style="display:none;">两次密码输入不同！</p>
                <?php echo $error1; ?>
			</form>
        </div>
    </div>
    <div class="span4">
    	<div class="box">
        	<form id="formlogin" name="formlogin" method="post" action="">
				<div class="alert alert-info" style="padding:10px; text-align:center; margin-bottom:10px;"><h4>登录</h4></div>
				<p><input title="邮箱" style="width:170px;" name="loginuseremail" type="text" id="loginuseremail" value="<?php echo @$_SESSION["loginuseremail"]; ?>" size="5" />&nbsp;&nbsp;邮箱&nbsp;&nbsp;*</p>
				<p><input title="密码" style="width:170px;" name="loginuserpassword" type="password" id="loginuserpassword" value="<?php echo @$_SESSION["loginuserpassword"]; ?>" size="5" />&nbsp;&nbsp;密码&nbsp;&nbsp;*</p>
				<p class="line"></p>
				<p><input title="登录" class="btn btn-primary" type="submit" name="buttonlogin" id="buttonlogin" value="登录" />&nbsp;&nbsp;<?php echo Line(); ?><a title="找回密码" href="../repassword/">找回密码</a></p>
				<p id="passworderror" class="alert alert-error" style="display:none;">请输入密码！</p>
				<p id="emailerror" class="alert alert-error" style="display:none;">请输入邮箱！</p>
                <?php echo $error2; ?>
			</form>
        </div>
    </div>
</div>
<?php
require_once("include_footer.php");
?>