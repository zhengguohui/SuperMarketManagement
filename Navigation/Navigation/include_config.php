<?php
//设置数据库参数
define('DATA_URL','127.0.0.1');
define('DATA_NAME','mysupermarket');
define('DATA_USER','root');
define('DATA_PASS','');
//设置网站参数
define('SITE_DOMAIN','localhost');
define('SITE_ADDRESS','MyShop');
define('SITE_LINK','http://'.SITE_DOMAIN.'/'.SITE_ADDRESS.'/');
define('SITE_TITLE','超市顾客应用平台导航');
define('PASSWORD_STRING','Candison');
//初始化参数
@header("Content-type:text/html; charset=utf-8");
@date_default_timezone_set('PRC');
@session_start();
//设置301转向
if(strtolower($_SERVER['HTTP_HOST'])!=SITE_DOMAIN)
{
	header('HTTP/1.1 301 Moved Permanently');
	header('Location: http://'.SITE_DOMAIN.$_SERVER['REQUEST_URI']);
	exit();
}
/*函数***********************/
//画线
function Line()
{
	return '&nbsp;&nbsp;<span style="color:#ccc;">|</span>&nbsp;&nbsp;';
}
//判断是否登录
function CheckLogin()
{
	if(!isset($_SESSION["login"]))
	{
		$menu='<li class="<?php echo @$active_index; ?>"><a href="<?php echo SITE_LINK; ?>" title="登录或注册">登录或注册</a></li>';
		header('Location: '.SITE_LINK);
	}
	else
	{
		if($_SESSION["login"]!="yes")
		{
			$menu='<li class="<?php echo @$active_index; ?>"><a href="<?php echo SITE_LINK; ?>" title="登录或注册">登录或注册</a></li>';
			header('Location: '.SITE_LINK);
		}
		else
		{
			$menu='<li class="<?php echo @$active_index; ?>"><a href="<?php echo SITE_LINK; ?>" title="退出">退出</a></li>';
			$_SESSION["redirect"]='http://'.SITE_DOMAIN.$_SERVER['REQUEST_URI'];
		}
	}
}
//返回加密后的密码
function Password($password)
{
	return md5($password.PASSWORD_STRING);
}
//返回当前时间
function TimeNow()
{
	return date("Y-m-d H:i:s");
}
//返回标题
function Title($title)
{
	if($title=='')
	{
		$title=SITE_TITLE;
	}
	else
	{
		$title=$title.' - '.SITE_TITLE;
	}
	return $title;
}
//返回SQL执行结果
function SQLRun($sql)
{
	$link = mysql_connect(DATA_URL, DATA_USER, DATA_PASS) or die('Could not connect to mysql server!');
	$select = mysql_select_db(DATA_NAME, $link) or die('Could not connect to mysql server!');
	mysql_query("SET NAMES utf8");
	$result = mysql_query($sql);
	$close = @mysql_close($link);
	return $result;
}
//返回SQL结果行数
function SQLRow($result)
{
	return mysql_numrows($result);
}
//返回SQL结果内容
function SQLShow($result,$index,$ziduan)
{
	return mysql_result($result, $index, $ziduan);
}
?>