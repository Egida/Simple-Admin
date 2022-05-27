<?php
if(isset($_POST['client']) && !empty($_POST['client'])) {
	$client = $_POST['client'];
	$pclist = $_POST['pclist'];
	
	$db = new mysqli('localhost', 'root', '', 'controlserver');
	if(mysqli_connect_errno()) exit;
	
	
	$query = "UPDATE clients SET pclist=? WHERE name=?";
	$stmt = $db->prepare($query);
	$stmt->bind_param('ss', $pclist, $client);
	$stmt->execute();
	
	$db->close();
}


?>