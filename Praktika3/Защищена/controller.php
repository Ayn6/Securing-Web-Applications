<?php
error_reporting(0);
// Параметры подключения к базе данных
$host = 'localhost';
$dbname = 'root';
$username = 'root';
$password = 'root';

try {
    // Подключение к базе данных
    $pdo = new PDO("mysql:host=$host;dbname=$dbname", $username, $password);

    // Запрос
    $sql = "SELECT * FROM user WHERE login = :login AND password = :password";
    $stmt = $pdo->prepare($sql);

    // Передача параметров запроса
    $stmt->bindParam(':login', $_POST['login'], PDO::PARAM_STR);
    $stmt->bindParam(':password', $_POST['pass'], PDO::PARAM_STR);

    // Выполнение запроса
    $stmt->execute();

    // Получение результатов
    $user = $stmt->fetch(PDO::FETCH_ASSOC);

    // Вывод результатов
    if ($user) {
        echo "Пользователь найден: " . $user['login'];
    } else {
        echo "Пользователь не найден";
    }
} catch (PDOException $e) {
    echo "Ошибка: " . $e->getMessage();
}
?>