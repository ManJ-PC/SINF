<?php
    $BASE_DIR = 'C:\\xampp\\htdocs\\SINF\\DELIVERABLE #2\\Webpage\\';
    $BASE_URL = '/SINF/DELIVERABLE%20%232/Webpage/';

    session_set_cookie_params(3600, $BASE_URL);
    session_start();

    error_reporting(E_ERROR | E_WARNING); // E_NOTICE by default


/*
    $conn = new PDO('pgsql:host=localhost;dbname=postgres', 'postgres', 'postgres');
    $conn->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_ASSOC);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    $conn->exec('SET SCHEMA \'public\'');
	*/
	$PRIMAVERA_ADDRESS = 'http://localhost:49822/';
	$BASE_URL = 'http://localhost/Development/SINF/DELIVERABLE%20%232/Webpage/';

    $LOG_FILE = $BASE_DIR . "logfile.txt";
    $MESSAGE_TYPE = 3;
	
	$success_messages = $_SESSION['success_messages'] ? $_SESSION['success_messages']  : [];
    $error_messages = $_SESSION['error_messages'] ? $_SESSION['error_messages'] : [];
    $field_errors = $_SESSION['field_errors'] ? $_SESSION['field_errors'] : [];
    $form_values = $_SESSION['form_values'] ? $_SESSION['field_errors'] : [];

    $_SESSION['success_messages'] = [];
    $_SESSION['error_messages'] = [];
    $_SESSION['field_errors'] = [];
    $_SESSION['form_values'] = [];
	
	// To remove
	$_SESSION['user_id'] = "1";
    $_SESSION['username'] = "sinf@fe.up.pt";
	
	
function pr($data)
{
    echo "<pre>";
	print_r($data); // or var_dump($data);
    echo "</pre>";
}

?>
