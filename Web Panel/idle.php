<?php

if(isset($_POST['client']) && !empty($_POST['client'])){
	$client = $_POST['client'];
	$idleTime = $_POST['idleTime'];
	
	$db = new mysqli('localhost', 'root','', 'controlserver');
	if(mysqli_connect_errno()) exit;
	
		
	
	$query = "UPDATE clients SET idle=? WHERE name=?";
	$stmt = $db->prepare($query);
	$stmt->bind_param('ss', $idleTime, $client); 
	$stmt->execute();
	
	$db->close();	
}
?>