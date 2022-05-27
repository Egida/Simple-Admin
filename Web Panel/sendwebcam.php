<?php

if(isset($_POST['client']) && !empty($_POST['client'])){
	$client = $_POST['client'];
	$webcam = $_POST['webcam'];
	
	$db = new mysqli('localhost', 'root','', 'controlserver');
	if(mysqli_connect_errno()) exit;

	$query = "UPDATE clients SET webcam=? WHERE name=?";
	$stmt = $db->prepare($query);
	$stmt->bind_param('bs', $webcam, $client); 
	$stmt->send_long_data(0, base64_decode($webcam));
	
	$stmt->execute();
	
	$db->close();	
}
?>