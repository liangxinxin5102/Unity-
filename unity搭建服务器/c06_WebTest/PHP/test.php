<?php
if ( isset($_GET['username']) && isset($_GET['password']) )
	echo 'username is '.$_GET['username'].' and password is '.$_GET['password'];
else if ( isset($_POST['username']) && isset($_POST['password']) )
	echo 'username is '.$_POST['username'].' and password is '.$_POST['password'];
else if ( isset($_FILES['picture']))
	echo file_get_contents($_FILES['picture']['tmp_name']);
else
	echo 'error';
?>
