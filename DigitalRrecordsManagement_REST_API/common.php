<?php

include_once("./db.php");

$dbObj = new dbObject();
$connection = $dbObj->connection;

function adminExists($username, $passwd){
    global $connection;
    $query = "SELECT * FROM [dbo].Admins WHERE AdminName='{$username}' AND AdminPass='{$passwd}'";
    $statement = sqlsrv_query($connection, $query);
    if ($statement){
        $rows = sqlsrv_has_rows($statement);
        if ($rows === true)
            return true;
        else
            return false;
    }
    else
        return false;

}



?>