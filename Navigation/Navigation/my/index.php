<?php
$title="我的超市";
$active_mysupermarket="active";
require_once("../include_config.php");
CheckLogin();
require_once("../include_header.php");
?>
<div class="row"><?php
if(!isset($_GET["a"]))
{
	?>
<div class="span12">
<div class="well1"><?php
$sql3='select id from smm_user where smm_email="'.$_SESSION["email"].'"';
$a=SQLShow(SQLRun($sql3),0,"id");
$sql='select id,smm_shop,smm_username,smm_password from smm_market where smm_user='.$a;
$result=SQLRun($sql);
if(SQLRow($result)<=0)
{
	echo '<p class="alert alert-error" style="margin-bottom:0px;">您暂未添加超市信息，请点击<a title="添加超市信息" href="'.SITE_LINK.'add/'.'">这里</a>添加。</p>';
}
else
{
	echo '<table class="table table-striped table-bordered table-condensed" border="0" cellspacing="0" cellpadding="0" style="margin-bottom:0px;">';
	echo '<tr><th>超市编号</th><th>超市名称</th><th>会员卡号</th><th>操作</th></tr>';
	for($i=0;$i<SQLRow($result);$i++)
	{
		$sql2='select smm_shop_name,smm_shop_address from smm_user where id='.SQLShow($result,$i,"smm_shop");
		$r=SQLRun($sql2);
		echo '<tr>';
		echo '<td>'.SQLShow($result,$i,"smm_shop").'</td>';
		echo '<td><a href="'.SQLShow($r,0,"smm_shop_address").'login.aspx?u='.SQLShow($result,0,"smm_username").'&p='.SQLShow($result,0,"smm_password").'" target="_blank" title="'.SQLShow($r,0,"smm_shop_name").'">'.SQLShow($r,0,"smm_shop_name").'</a></td>';
		echo '<td>'.SQLShow($result,$i,"smm_username").'</td>';
		echo '<td><a title="查看商品" href="'.SQLShow($r,0,"smm_shop_address").'/allproduct">查看商品</a>&nbsp;&nbsp;<a title="我的账户" href="'.SQLShow($r,0,"smm_shop_address").'/allproduct">我的账户</a>&nbsp;&nbsp;<a title="修改" href="?a=0&id='.SQLShow($result,$i,"id").'">修改</a>&nbsp;&nbsp;<a title="删除" href="?a=1&id='.SQLShow($result,$i,"id").'">删除</a></td>';
		echo '</tr>';
	}
	echo '</table>';
}
?></div>
</div>
<?php
}else if($_GET["a"]=="0")
{
	if(isset($_POST["smm_number"]))
	{
		$sql6='update smm_market set smm_username="'.$_POST["smm_number"].'",smm_password="'.$_POST["smm_password"].'" where id='.$_GET["id"];
		SQLRun($sql6);
		header('Location: '.SITE_LINK.'my/');
			
	}
	$sql4='select smm_shop,smm_username,smm_password from smm_market where id='.$_GET["id"];
	$s=SQLShow(SQLRun($sql4),0,"smm_shop");
	$sql='select smm_shop_name from smm_user where id='.$s;

	?>
<div class="span3">&nbsp;</div>
<div class="span6">
<div class="box">

<form id="formsignup" name="formsignup" method="post" action="">
<div class="alert alert-info" style="padding:10px; text-align:center; margin-bottom:20px;"><h4>修改超市</h4></div>
<table border="0" cellspacing="0" cellpadding="0">
					<tr>
						<td align="left" valign="top" style="padding-right:20px;">
							<img src="<?php echo SITE_LINK; ?>static/img/add.png" />
						</td>
						<td align="left" valign="top">
<p><input disabled="disabled" title="<?php echo SQLShow(SQLRun($sql),0,"smm_shop_name"); ?>" style="width: 170px;"
	name="smm_name" type="text" id="smm_name"
	value="<?php echo SQLShow(SQLRun($sql),0,"smm_shop_name"); ?>" size="5" />&nbsp;&nbsp;超市名称</p>
<p><input title="会员卡号" style="width: 170px;" name="smm_number" type="text"
	id="smm_number"
	value="<?php echo @SQLShow(SQLRun($sql4),0,"smm_username"); ?>"
	size="5" />&nbsp;&nbsp;会员卡号</p>
<p><input title="会员密码" style="width: 170px;" name="smm_password"
	type="password" id="smm_password"
	value="<?php echo @SQLShow(SQLRun($sql4),0,"smm_password"); ?>"
	size="5" />&nbsp;&nbsp;会员密码</p></td>
					</tr>
				</table>
				<p class="line"></p>
				<p style="text-align:center;"><input title="修改" class="btn btn-primary" type="submit" name="signupbutton"
	id="signupbutton" value="修改" />&nbsp;&nbsp;<a class="btn btn-primary" title="返回"
	href="<?php echo SITE_LINK.'my/'; ?>">返回</a></p>
</form>
</div>
</div>
<div class="span3">&nbsp;</div>
<script language="javascript" type="text/javascript">
document.getElementById("smm_number").focus()();
</script> <?php
}
else
{
	if(isset($_POST["signupbutton1"]))
	{
		$sql6='delete from smm_market where id='.$_GET["id"];
		SQLRun($sql6);
		header('Location: '.SITE_LINK.'my/');
			
	}

	?>
<div class="span3">&nbsp;</div>
<div class="span6">
<div class="box">

<form id="formsignup" name="formsignup" method="post" action="">
<div class="alert alert-info" style="padding:10px; text-align:center; margin-bottom:20px;"><h4>删除超市</h4></div>
<table border="0" cellspacing="0" cellpadding="0">
					<tr>
						<td align="left" valign="top" style="padding-right:20px;">
							<img src="<?php echo SITE_LINK; ?>static/img/del.png" />
						</td>
						<td align="left" valign="top">
<p style="padding-top:50px;">确定要删除吗？</p></td>
					</tr>
				</table>
				<p class="line"></p>
				<p style="text-align:center;"><input title="确定" class="btn btn-primary" type="submit" name="signupbutton1"
	id="signupbutton1" value="确定" />&nbsp;&nbsp;<a class="btn btn-primary" title="返回"
	href="<?php echo SITE_LINK.'my/'; ?>">返回</a></p>
</form>
</div>
</div>
<div class="span3">&nbsp;</div>
	<?php
}
?></div>
<?php
require_once("../include_footer.php");
?>