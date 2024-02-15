<?
include "db.php";

if (!$_SESSION['admin']) {
    header("Location:/");
}
?>
Вы аминистратор
