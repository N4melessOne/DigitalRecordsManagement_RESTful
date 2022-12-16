<?php

include_once("./common.php");

$request = $_SERVER["REQUEST_METHOD"];

switch ($request) {
	case "POST":
		if (!empty($_POST["username"]) && !empty($_POST["password"])) {
			if (login($_POST["username"], $_POST["password"])) {
				echo json_encode(
					array(
						'error' => 0,
						'message' => 'Succesfully logged in!'
					)
				);
			} else {
				echo json_encode(
					array(
						'error' => 1,
						'message' => 'User does not exists!'
					)
				);
			}

		} else {
			echo json_encode(
				array(
					'error' => 1,
					'message' => 'Missing username or password!'
				)
			);
		}
		break;

    case 'PUT':
		$content = file_get_contents('php://input');
		$data = json_decode($content, true);
		if (empty($data["current_admin_name"]) ||
			empty($data["current_admin_password"]) ||
			!adminExists($data["current_admin_name"], $data["current_admin_password"])) {
            echo json_encode(
            array(
                'error' => 1,
                'message' => 'Login required! Missing or invalid username/password.'
            )
        );
			exit;
        }

		if (!empty($data["new_admin_name"]) && !empty($data["new_admin_password"])){
            update($data["current_admin_name"], $data["current_admin_password"], $data["new_admin_name"], $data["new_admin_password"]);
        }
		else{
            if (!empty($data["new_admin_name"])){
                update($data["current_admin_name"], $data["current_admin_password"], newUsername:$data["new_admin_name"]);
            }
            if (!empty($data["new_admin_password"])){
				update($data["current_admin_name"], $data["current_admin_password"], newPasswd:$data["new_admin_password"]);
            }
        }
		break;
	default:
		header('HTTP/1.1 405 Method Not Allowed');
		header('Allow: GET');
		break;
	}

	function login($username, $passwd)
	{
		global $connection;
		$query = "SELECT * FROM [dbo].Admins WHERE AdminName='{$username}' AND AdminPass='{$passwd}'";
		$statement = sqlsrv_query($connection, $query);
		if ($statement) {
			$rows = sqlsrv_has_rows($statement);
			if ($rows === true)
				return true;
			else
				return false;
		} else
			return false;

	}


	function update($currentUsername, $currentPassword, $newUsername = null, $newPasswd = null)
    {
        global $connection;
		$query  = "";
		$params = array();
		$options =  array( "Scrollable" => SQLSRV_CURSOR_KEYSET );

		if ($newUsername != null){
			$query = "UPDATE [dbo].Admins SET AdminName='{$newUsername}' WHERE AdminName='{$currentUsername}' AND AdminPass='{$currentPassword}'";
        }
		else if($newPasswd != null){
			$query = "UPDATE [dbo].Admins SET AdminPass='{$newPasswd}' WHERE AdminName='{$currentUsername}' AND AdminPass='{$currentPassword}'";
        }
		else{
			$query = "UPDATE [dbo].Admins SET AdminName='{$newUsername}', AdminPass='{$newPasswd}' WHERE AdminName='{$currentUsername}' AND AdminPass='{$currentPassword}'";
        }
		try {
            $statement = sqlsrv_query($connection, $query, $params, $options);

			if($statement){
                $row_count = sqlsrv_num_rows($statement);

                if($row_count) {
                    $response = array();
                    $response['Error'] = 0;
                    $response['Message'] = "Updated successfully!";
                    echo(json_encode($response));
                }
                else {
                    $response = array();
                    $response['Error'] = 1;
                    $response['Message'] = "Updated failed!";
                    echo(json_encode($response));
                }
            }
			else {
                $response = array();
                $response['Error'] = 1;
                $response['Message'] = "Updated failed!";
                echo(json_encode($response));
            }
        }
        catch(PDOException $e) {
            header("HTTP/1.1 400 Bad request");
            echo($e->getMessage());
        }
    }
?>