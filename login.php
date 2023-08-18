<style>
<?php include 'style.css'; ?>
</style>

<?php

$connection = new mysqli('localhost', 'covisiantest', '', 'my_covisiantest');
if($connection === false) 
{
die("Errore di connessione: " . $mysqli->connect_error);
}

$username = $_POST['username'];
$password = $_POST['password'];

$sql = "SELECT username FROM users WHERE username='$username' AND password='$password'";
if($result = $connection->query($sql))
{
	if($result->num_rows > 0)
    {
        echo "<iframe src='./build/build.html?param=$username' class='unityFrame'></iframe>";
    }
    else
    {
    	echo "Invalid username or password.";
    }
}
else{
	echo "Connection failed: " . $connection->error;
}


$connection->close();
?>