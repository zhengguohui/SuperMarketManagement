<?php
$title="设置公司超市信息";
$active_setsupermarket="active";
require_once("../include_config.php");
CheckLogin();
$error1="";
$sql='select id,smm_shop_name,smm_shop_address,smm_shop_keyword,smm_shop_about from smm_user where smm_email="'.$_SESSION["email"].'"';
$result=SQLRun($sql);
$_SESSION["signupuseremail1"]=SQLShow($result,0,"smm_shop_name");
$_SESSION["signupuserpassword1"]=SQLShow($result,0,"smm_shop_address");
$_SESSION["signupreuserpassword1"]=SQLShow($result,0,"smm_shop_keyword");
$_SESSION["textarea"]=SQLShow($result,0,"smm_shop_about");
if(isset($_POST["signupbutton"]))
{
	$_SESSION["signupuseremail1"]=$_POST["signupuseremail"];
	$_SESSION["signupuserpassword1"]=$_POST["signupuserpassword"];
	$_SESSION["signupreuserpassword1"]=$_POST["signupreuserpassword"];
	$_SESSION["textarea"]=$_POST["textarea"];
	if($_SESSION["signupuseremail1"]=="")
	{
		$error1='<p id="error1" class="alert alert-error">请输入超市名称！</p>';
	}
	else if($_SESSION["signupuserpassword1"]=="")
	{
		$error1='<p id="error1" class="alert alert-error">请输入超市顾客应用平台的网址！</p>';
	}
	else
	{
		if($_SESSION["tag"]=="1")
		{
			$sql='update smm_user set smm_shop_name="'.$_SESSION["signupuseremail1"].'",smm_shop_address="'.$_SESSION["signupuserpassword1"].'",smm_shop_keyword="'.$_SESSION["signupreuserpassword1"].'",smm_shop_about="'.$_SESSION["textarea"].'" where smm_email="'.$_SESSION["email"].'"';
			SQLRun($sql);
			$_SESSION["signupuseremail1"]="";
			$_SESSION["signupuserpassword1"]="";
			$_SESSION["signupreuserpassword1"]="";
			$_SESSION["textarea"]="";
			header('Location: '.SITE_LINK.'mysupermarket/');
		}
		else
		{
			$error1='<p id="error1" class="alert alert-error">权限不足！</p>';
		}
	}
}
require_once("../include_header.php");
?>
<div class="row">
	<div class="span2">&nbsp;</div>
	<div class="span8">
    	<div class="well">
        	<form id="formsignup" name="formsignup" method="post" action="">
				<div class="alert alert-info" style="padding:10px; text-align:center; margin-bottom:10px;"><h4>设置公司超市信息（超市编号：<?php echo SQLShow($result,0,"id"); ?>）</h4></div>
				<table border="0" cellspacing="0" cellpadding="0">
					<tr>
						<td>
							<p><input title="" style="width:170px;" name="signupuseremail" type="text" id="signupuseremail" value="<?php echo @$_SESSION["signupuseremail1"]; ?>" size="5" />&nbsp;&nbsp;名称&nbsp;&nbsp;*&nbsp;&nbsp;&nbsp;&nbsp;</p>
							<p><input title="" style="width:170px;" name="signupuserpassword" type="text" id="signupuserpassword" value="<?php echo @$_SESSION["signupuserpassword1"]; ?>" size="5" />&nbsp;&nbsp;网址&nbsp;&nbsp;*&nbsp;&nbsp;&nbsp;&nbsp;</p>
							<p><input title="" style="width:170px;" name="signupreuserpassword" type="text" id="signupreuserpassword" value="<?php echo @$_SESSION["signupreuserpassword1"]; ?>" size="5" />&nbsp;&nbsp;标签&nbsp;&nbsp;&nbsp;&nbsp;</p>
						</td>
						<td>
							<p><textarea name="textarea" id="textarea" cols="65" rows="6"><?php echo @$_SESSION["textarea"]; ?></textarea>&nbsp;&nbsp;简介</p>
						</td>
					</tr>
				</table>
				<p><input class="btn btn-primary" type="submit" name="signupbutton" id="signupbutton" value="设置" />
                <?php echo $error1; ?>
			</form>
        </div>
    </div>
	<div class="span2">&nbsp;</div>
</div>
<script language="javascript" type="text/javascript">
document.getElementById("signupuseremail").focus()();
</script>
<?php
require_once("../include_footer.php");
?>