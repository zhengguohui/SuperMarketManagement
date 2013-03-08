<?php
$title="添加超市";
$active_addsupermarket="active";
require_once("../include_config.php");
CheckLogin();
$error1="";
require_once("../include_header.php");
?>
<div class="row">
	<div class="span12">
    	
		<?php
			if(isset($_GET["id"]))
			{
			?>
			
			
			<?php
			if(isset($_POST["smm_number"]))
			{
			$sql2='select id from smm_user where smm_email="'.$_SESSION["email"].'"';
			$a=SQLShow(SQLRun($sql2),0,"id");
				$sql1='insert into smm_market(smm_user,smm_shop,smm_username,smm_password) values ('.$a.','.$_GET["id"].',"'.$_POST["smm_number"].'","'.$_POST["smm_password"].'")';
				SQLRun($sql1);
				header('Location: '.SITE_LINK.'my/');
			}
				$sql='select smm_shop_name from smm_user where id='.$_GET["id"];
				
			?>
			<div class="span3">&nbsp;</div>
	<div class="span6">
    	<div class="box">
				<form id="formsignup" name="formsignup" method="post" action="">
				<div class="alert alert-info" style="padding:10px; text-align:center; margin-bottom:20px;"><h4>添加超市</h4></div>
				<table border="0" cellspacing="0" cellpadding="0">
					<tr>
						<td align="left" valign="top" style="padding-right:20px;">
							<img src="<?php echo SITE_LINK; ?>static/img/add.png" />
						</td>
						<td align="left" valign="top">
				<p><input disabled="disabled" title="<?php echo SQLShow(SQLRun($sql),0,"smm_shop_name"); ?>" style="width:170px;" name="smm_name" type="text" id="smm_name" value="<?php echo SQLShow(SQLRun($sql),0,"smm_shop_name"); ?>" size="5" />&nbsp;&nbsp;超市名称</p>
							<p><input title="会员卡号" style="width:170px;" name="smm_number" type="text" id="smm_number" value="" size="5" />&nbsp;&nbsp;会员卡号</p>
							<p><input title="会员密码" style="width:170px;" name="smm_password" type="password" id="smm_password" value="" size="5" />&nbsp;&nbsp;会员密码</p>
							</td>
					</tr>
				</table>
				<p class="line"></p>
				<p style="text-align:center;"><input class="btn btn-primary" type="submit" name="signupbutton" id="signupbutton" value="添加" title="添加" />&nbsp;&nbsp;<a class="btn btn-primary" title="返回" href="<?php echo SITE_LINK.'add/'; ?>">返回</a></p>
			</form>
			</div>
    </div>
    <div class="span3">&nbsp;</div>
	<script language="javascript" type="text/javascript">
document.getElementById("smm_number").focus()();
</script>
			<?php
			}
			else
			{
		?>
		<div class="well1">
			<form id="formsignup" name="formsignup" method="post" action="" class="form-search" style="text-align:center;">
			<div class="input-append">
				<input class="search-query" placeholder="例如：编号、名称、地址等" title="在此输入要搜索的内容" style="width:270px;" name="search" type="text" id="search" value="<?php echo @$_SESSION["signupuseremail2"]; ?>" size="5" />
	<input class="btn" type="submit" name="signupbutton" id="signupbutton" value="搜索超市" title="搜索超市" />
                
                </div>
			</form>
			
			<?php echo $error1; ?>
			<?php
			if(!isset($_POST["search"]))
			{
				$_POST["search"]="";
			}
			if(isset($_POST["search"]))
			{
				$sql='select id,smm_tag,smm_shop_name,smm_shop_address from smm_user where smm_shop_name like "%'.$_POST["search"].'%" or id like "%'.$_POST["search"].'%" or smm_shop_keyword like "%'.$_POST["search"].'%" or smm_shop_about like "%'.$_POST["search"].'%"';
				$result=SQLRun($sql);
				if(SQLRow($result)<=0)
				{
					$error1='<p id="error1" class="alert alert-error" style="margin-bottom:0px;">未找到您搜索的超市！</p>';
				}
				else
				{
					echo '<table class="table table-striped table-bordered table-condensed" border="0" cellspacing="0" cellpadding="0" style="margin-bottom:0px;">';
					echo '<tr><th>超市编号</th><th>超市名称</th><th>操作</th></tr>';
					for($i=0;$i<SQLRow($result);$i++)
					{
						if(SQLShow($result,$i,"smm_tag")!="" && SQLShow($result,$i,"smm_shop_address")!="")
						{
							echo '<tr>';
							echo '<td>'.SQLShow($result,$i,"id").'</td>';
							echo '<td><a title="'.SQLShow($result,$i,"smm_shop_name").'" href="'.SQLShow($result,$i,"smm_shop_address").'" target="_blank">'.SQLShow($result,$i,"smm_shop_name").'</a></td>';
							echo '<td><a title="添加" href="?id='.SQLShow($result,$i,"id").'">添加</a></td>';
							echo '</tr>';
						}
					}
					echo '</table>';
				}
			}
			?>
			<?php echo $error1; ?>
			</div>
			<script language="javascript" type="text/javascript">
document.getElementById("search").focus()();
</script>
			<?php } ?>
		
    </div>
</div>

<?php
require_once("../include_footer.php");
?>