<?php
include_once("./common.php");

function getAllRecords(){
    global $connection;
    $response = array();
    $record = array();
    $query = "SELECT * FROM [dbo].Record;";
    $result = sqlsrv_query($connection, $query);

    while($row = sqlsrv_fetch_object($result)) {

        $record['Id'] = $row->Id;
        $record['Performer'] = $row->Performer;
        $record['Title'] = $row->Title;
        $record['Price'] = $row->Price;
        $record['StockCount'] = $row->StockCount;

        $response[] = $record;
    }

    echo json_encode($response);
}


function getRecordById($id){
    global $connection;
    $record = array();
    $response = array();

    $query = "SELECT * FROM [dbo].Record WHERE Id = $id";
    $result = sqlsrv_query($connection, $query);

    while($row = sqlsrv_fetch_object($result)) {

        $record['Id'] = $row->Id;
        $record['Performer'] = $row->Performer;
        $record['Title'] = $row->Title;
        $record['Price'] = $row->Price;
        $record['StockCount'] = $row->StockCount;

        $response[] = $record;
    }
    echo json_encode($response);
}


function addRecord($performer, $title, $price, $stock){
    global $connection;
    $params = array($performer, $title, $price, $stock);
    $options =  array( "Scrollable" => SQLSRV_CURSOR_KEYSET );
    $query = "INSERT INTO [dbo].Record (Performer, Title, Price, StockCount) VALUES (?,?,?,?)";


    try{
        $statement = sqlsrv_query($connection, $query, $params, $options);
        $row_count = sqlsrv_num_rows($statement);
        if($row_count !== false)
        {
            $response = array();
            $response['Error'] = 0;
            $response['Message']='New record inserted successfully!';
            echo(json_encode($response));
        }
        else{
            $response = array();
            $response['Error'] = 1;
            $response['Message'] = "Insert failed!";
            echo(json_encode($response));

        }
    }
    catch(PDOException $e) {
        header("HTTP/1.1 400 Bad request");
        echo($e->getMessage());
    }
}


function deleteRecordByID($id){
    global $connection;
    $query = "DELETE FROM [dbo].Record WHERE id=$id";
    $params = array();
    $options =  array( "Scrollable" => SQLSRV_CURSOR_KEYSET );

    try {
        $statement = sqlsrv_query( $connection, $query , $params, $options );

        $row_count = sqlsrv_num_rows( $statement );
        if($row_count) {
            $response = array();
            $response['Error'] = 0;
            $response['Message'] = "Deleted successfully!";

            echo(json_encode($response));
        }
        else {
            $response = array();
            $response['Error'] = 1;
            $response['Message'] = "Delete failed!";
            echo(json_encode($response));
        }
    }
    catch(Exception $e) {
        header("HTTP/1.1 400 Bad request");
        echo($e->getMessage());
    }
}


function updateRecordByID($id, $performer, $title, $price, $stock){
    global $connection;
    $params = array($performer, $title, $price, $stock, $id);
    $options =  array( "Scrollable" => SQLSRV_CURSOR_KEYSET );
    $query = "UPDATE [dbo].Record SET Performer=?, Title=?, Price=?, StockCount=? WHERE Id=?";
    try {
        $statement = sqlsrv_query($connection, $query, $params, $options);
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
    catch(PDOException $e) {
        header("HTTP/1.1 400 Bad request");
        echo($e->getMessage());
    }
}

?>