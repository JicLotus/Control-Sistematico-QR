<?php
	$con = mysql_connect("localhost", "root","") or die("Sin conexion");
	mysql_select_db("lectorcodigo");
	$sql= "Select * from clientes";
	
	$rs = mysql_query($sql,$con);
	
	while($row=mysql_fetch_object($rs)){
		$datos[] = $row;
	}
	
	echo json_encode($datos);
?>