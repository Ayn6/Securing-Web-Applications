<?php
include "db.php";

$login = $_POST['login'];
$pass = $_POST['pass'];

$str = "SELECT * FROM `user` WHERE `login` = '$login' AND `password` = '$pass'";
echo $str;

$run = mysqli_query($connect, $str);
$user = mysqli_fetch_array($run);

if ($out['role'] == 1) {
    $_SESSION['user'];
    header("Location:/user.php");
}
else {
    $_SESSION['admin'];
    header("Location:/admin.php");
}

?>