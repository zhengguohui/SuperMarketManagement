<!--公共头部-->
<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="keywords" content="超市顾客应用平台导航" />
<meta name="description" content="超市顾客应用平台导航" />
<meta name="author" content="kandisheng" />
<title><?php echo Title($title); ?></title>
<link rel="shortcut icon" type="image/vnd.microsoft.icon" href="<?php echo SITE_LINK; ?>favicon.ico">
<link rel="stylesheet" type="text/css" href="<?php echo SITE_LINK; ?>static/style/style.css" />
<link rel="stylesheet" type="text/css" href="<?php echo SITE_LINK; ?>static/framework/bootstrap/css/bootstrap.min.css" />
</head>
<body>
<!--IE浏览器兼容提示-->
<!--[if lte IE 6]>
<div class="alert alert-error" style="text-align:center;">本网站不支持IE6及以下版本的浏览器，请及时<a href="http://chrome.google.com" target="_blank" title="升级您的浏览器">升级您的浏览器</a>。</div>
<![endif]-->
<!--导航条-->
<div class="navbar navbar-fixed-top"><div class="navbar-inner"><div class="container">
<ul class="nav">
<a class="brand" href="<?php echo SITE_LINK; ?>mysupermarket/" title="超市顾客应用平台导航" style="color:#fff;padding-top:11px;">超市顾客应用平台导航</a>
<li class="divider-vertical"></li>
<li class="<?php echo @$active_mysupermarket; ?>"><a href="<?php echo SITE_LINK; ?>mysupermarket/" title="我的超市">我的超市</a></li>
<li class="<?php echo @$active_addsupermarket; ?>"><a href="<?php echo SITE_LINK; ?>addsupermarket/" title="添加超市">添加超市</a></li>
<?php
if(isset($_SESSION["tag"]))
{
	if($_SESSION["tag"]=="1")
	{
		echo '<li class="'.@$active_setsupermarket.'"><a href="'.SITE_LINK.'setsupermarket/" title="设置公司超市信息">设置公司超市信息</a></li>';
	}
}
?>
</ul>
<ul class="nav pull-right">
<?php
if(!isset($_SESSION["login"]))
{
	$menu='<li class="'.@$active_index.'"><a href="'.SITE_LINK.'" title="登录或注册">登录或注册</a></li>';
}
else
{
	if($_SESSION["login"]!="yes")
	{
		$menu='<li class="'.@$active_index.'"><a href="'.SITE_LINK.'" title="登录或注册">登录或注册</a></li>';
		
	}
	else
	{
		$menu='';
		$menu.='<li class="'.@$active_changepassword.'"><a href="'.SITE_LINK.'changepassword/" title="修改密码">修改密码</a></li>';
		$menu.='<li><a href="'.SITE_LINK.'logout/" title="退出">退出</a></li>';
	}
}
echo $menu;
?>
</ul>
</div></div></div>
<!--主要内容-->
<div class="container">
<div style="margin-top:60px;"></div>
<!--各页面内容-->
