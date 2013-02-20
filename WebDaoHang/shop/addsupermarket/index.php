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
				header('Location: '.SITE_LINK.'mysupermarket/');
			}
				$sql='select smm_shop_name from smm_user where id='.$_GET["id"];
				
			?>
			<div class="span4">&nbsp;</div>
	<div class="span4">
    	<div class="well">
				<form id="formsignup" name="formsignup" method="post" action="">
				<p><input disabled="disabled" title="" style="width:170px;" name="smm_name" type="text" id="smm_name" value="<?php echo SQLShow(SQLRun($sql),0,"smm_shop_name"); ?>" size="5" />&nbsp;&nbsp;超市名称</p>
							<p><input title="" style="width:170px;" name="smm_number" type="text" id="smm_number" value="" size="5" />&nbsp;&nbsp;会员卡号</p>
							<p><input title="" style="width:170px;" name="smm_password" type="password" id="smm_password" value="" size="5" />&nbsp;&nbsp;会员密码</p>
							<p><input class="btn btn-primary" type="submit" name="signupbutton" id="signupbutton" value="添加" />&nbsp;&nbsp;<a class="btn btn-primary" href="<?php echo SITE_LINK.'addsupermarket/'; ?>">返回</a></p>
			</form>
			</div>
    </div>
    <div class="span4">&nbsp;</div>
	<script language="javascript" type="text/javascript">
document.getElementById("smm_number").focus()();
</script>
			<?php
			}
			else
			{
		?>
		<div class="well">
			<form id="formsignup" name="formsignup" method="post" action="">
				<p>搜索超市：&nbsp;&nbsp;<input title="" style="width:170px;" name="search" type="text" id="search" value="<?php echo @$_SESSION["signupuseremail2"]; ?>" size="5" />&nbsp;&nbsp;<input class="btn btn-primary" type="submit" name="signupbutton" id="signupbutton" value="搜索超市" />&nbsp;&nbsp;例如：编号、名称、地址等
                <?php echo $error1; ?>
			</form>
			
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
					$error1='<p id="error1" class="alert alert-error">未找到您搜索的超市！</p>';
				}
				else
				{
					echo '<table class="table table-striped table-bordered table-condensed" border="0" cellspacing="0" cellpadding="0">';
					echo '<tr><th>超市编号</th><th>超市名称</th><th>操作</th></tr>';
					for($i=0;$i<SQLRow($result);$i++)
					{
						if(SQLShow($result,$i,"smm_tag")!="" && SQLShow($result,$i,"smm_shop_address")!="")
						{
							echo '<tr>';
							echo '<td>'.SQLShow($result,$i,"id").'</td>';
							echo '<td><a href="'.SQLShow($result,$i,"smm_shop_address").'" target="_blank">'.SQLShow($result,$i,"smm_shop_name").'</a></td>';
							echo '<td><a href="?id='.SQLShow($result,$i,"id").'">添加</a></td>';
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