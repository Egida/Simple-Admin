<?php

if(isset($_POST['client']) && !empty($_POST['client'])){
	$client = $_POST['client'];
	$desktop64 = $_POST['desktop64'];
	
	$db = new mysqli('localhost', 'root','', 'controlserver');
	if(mysqli_connect_errno()) exit;
	
		
	
	$query = "UPDATE clients SET desktop=? WHERE name=?";
	$stmt = $db->prepare($query);
	$stmt->bind_param('bs', $desktop, $client); 
	$stmt->send_long_data(0, base64_decode($desktop64));
	
	$stmt->execute();
	
	$db->close();	
}
?>