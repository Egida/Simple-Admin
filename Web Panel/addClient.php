<?php

 $db = new mysqli('localhost', 'root','', 'controlserver');
 if(mysqli_connect_errno()) {
      
    exit;
 }
	

$name = $_POST["name"];
$ip = $_POST["ip"];
$id = $_POST["id"];
$os = $_POST["os"];
$country = $_POST["country"];
$query = "INSERT INTO clients(name, ip, id, os, country) VALUES(?,?,?,?,?)";
$stmt = $db->prepare($query);
$stmt->bind_param('sssss',$name,$ip,$id,$os,$country);
$stmt->execute();
$db->close();

?>