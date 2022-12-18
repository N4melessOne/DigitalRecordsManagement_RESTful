<?php

include_once("./recordsManagementAPI.php");
include_once("./common.php");

$request = $_SERVER['REQUEST_METHOD'];


switch ($request){
    case "GET":
		if(!empty($_GET["recordid"])){
            getRecordById($_GET["recordid"]);
        }
		else{
            echo 
            getAllRecords();
        }
	break;

    case "POST":
		if (empty($_POST["current_admin_name"]) ||
			empty($_POST["current_admin_password"]) ||
			!adminExists($_POST["current_admin_name"], $_POST["current_admin_password"])) {
            echo json_encode(
            array(
                'error' => 1,
                'message' => 'Login required! Missing or invalid username/password.'
            )
        );
			exit;
		}

		if (!empty($_POST["new_record_performer"]) && !empty($_POST["new_record_title"])
			&& !empty($_POST["new_record_price"])) {

			if (empty($_POST["new_record_stock"])) {
                addRecord($_POST["new_record_performer"], $_POST["new_record_title"],
					$_POST["new_record_price"], 0);
            }
			else {
				addRecord($_POST["new_record_performer"], $_POST["new_record_title"],
					$_POST["new_record_price"], $_POST["new_record_stock"]);
			}
		}
		else {
			echo json_encode(array(
						'error' => 1,
						'message' => 'Missing parameters!'
					)
				);
		}
		break;

		case "PUT":
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

		//the client will handle the parameters, so they should never be empty!
		if (!empty($data["recordid"]) && !empty($data["new_record_performer"]) && !empty($data["new_record_title"]) &&
			!empty($data["new_record_price"]) && !empty($data["new_record_stock"])) {

				updateRecordByID($data["recordid"], $data["new_record_performer"], $data["new_record_title"],
					$data["new_record_price"], $data["new_record_stock"]);
			}

		else {
			echo json_encode(array(
						'error' => 1,
						'message' => 'Missing parameters!'
					)
				);
		}
		break;

		case "DELETE":
		$content = file_get_contents('php://input');
		$data = json_decode($content, true);

		if (empty($data["current_admin_name"]) ||
			empty($data["current_admin_password"]) ||
			!adminExists($data["current_admin_name"], $data["current_admin_password"])) {
				echo json_encode(
				array(
					'error' => 1,
					'message' => 'Login required! Missing or invalid username or password.'
				)
			);
			exit;
		}

		if (!empty($data["recordid_to_delete"]))
			deleteRecordByID($data["recordid_to_delete"]);
		break;

	default:
		header('HTTP/1.1 405 Method Not Allowed');
		header('Allow: GET, POST, PUT, DELETE');
		break;
}

?>
