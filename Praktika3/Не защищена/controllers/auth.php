<?php
include "db.php";

$login = $_POST['login'];
$pass = $_POST['pass'];

$str = "SELECT * FROM `user` WHERE `login` = '$login' AND `password` = '$pass'";
echo $str;

$run = mysqli_query($connect, $str);

if ($run) {
    $user = mysqli_fetch_assoc($run);
    print_r($user);
}

// if ($out['role'] == 1) {
//     $_SESSION['user'];
//     header("Location:/user.php");
// }
// else {
//     $_SESSION['admin'];
//     header("Location:/admin.php");
// }

?>