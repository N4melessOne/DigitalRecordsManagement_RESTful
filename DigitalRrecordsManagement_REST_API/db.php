<?php
Class dbObject {
    //The whole connection string from SQL Server Object Explorer
    var $connString = "Initial Catalog=RecordsManagementDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    var $connectionInfo = array("Database"=>"RecordsManagementDb");
    var $serverName = "(localdb)\MSSQLLocalDB";
    var $connection;

    function __construct(){
        $this->connection = $this->getConnection();
    }

    function getConnection()
    {
        $conn = sqlsrv_connect( $this->serverName, $this->connectionInfo);

        if( $conn ) {
            return $conn;
        }
        else{
            echo "Connection could not be established.<br />";
            die( print_r( sqlsrv_errors(), true));
        }
    }
}

?>