<?php
require_once("../include_config.php");
$_SESSION['login']='no';
$_SESSION['tag']='0';
header('Location: '.SITE_LINK);
?>