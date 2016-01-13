<?php
	date_default_timezone_set('America/Argentina/Buenos_Aires');
	#CORREGIR FORMATO DE REGION YA QUE CAMBIA ANTES DE DIA
	#CORREGIR FORMATO DE REGION YA QUE CAMBIA ANTES DE DIA
	#CORREGIR FORMATO DE REGION YA QUE CAMBIA ANTES DE DIA
	#CORREGIR FORMATO DE REGION YA QUE CAMBIA ANTES DE DIA
	$fecha_scaneo = date("Y-m-d"); #CORREGIR FORMATO DE REGION YA QUE CAMBIA ANTES DE DIA 
	#CORREGIR FORMATO DE REGION YA QUE CAMBIA ANTES DE DIA
	#CORREGIR FORMATO DE REGION YA QUE CAMBIA ANTES DE DIA
	#CORREGIR FORMATO DE REGION YA QUE CAMBIA ANTES DE DIA
	#CORREGIR FORMATO DE REGION YA QUE CAMBIA ANTES DE DIA
	
	$nroBobina = $_REQUEST["bob"];
	$estado = $_REQUEST["estado"];
	$celu = $_REQUEST["tel"];
	$turno = $_REQUEST["turno"];
	$con = mysql_connect("localhost", "root","") or die("Sin conexion");
	mysql_select_db("lectorcodigo");
	$sql = "SELECT Numero_Bobina FROM `reg_2014` WHERE Numero_Bobina = $nroBobina";
	$rs = mysql_query($sql,$con);
	
	if (mysql_num_rows($rs)!=0)
	{
		$sql= "Update lectorcodigo.reg_2014 set FECHA_SCANEO='$fecha_scaneo' , estado_id=$estado , celular='$celu' where reg_2014.Numero_Bobina =$nroBobina limit 1";
		$rs = mysql_query($sql,$con);
	}
	else
	{
		$producto= $_REQUEST["prod"];
		$cliente= $_REQUEST["cli"];
		$maquinista= $_REQUEST["maq"];	
		$peso= $_REQUEST["peso"];
		$observacion= $_REQUEST["obs"];
		$gramaje= $_REQUEST["gram"];
		$espesor= $_REQUEST["esp"];
		$finBob= $_REQUEST["fbob"];
		$formato= $_REQUEST["form"];
		$fecha_fabricacion= $_REQUEST["fecha"];
		$sql= "INSERT INTO  `lectorcodigo`.`reg_2014` (`Numero_Bobina` ,`estado_id` ,
		`producto_id` ,`cliente_id` ,`maquinista_id` ,`Peso`,`Observacion` ,`Gramaje` ,
		`Espesor` ,`Fin_Bob` ,`Formato` , `FECHA_SCANEO` ,`FECHA_FABRICACION` ,`celular`,`turno`) VALUES ($nroBobina ,
		'$estado',  '$producto',  '$cliente',  '$maquinista',  '$peso',  '$observacion',
		'$gramaje',  '$espesor',  '$finBob','$formato', '$fecha_scaneo' ,'$fecha_fabricacion',  '$celu', '$turno')";

		$rs = mysql_query($sql,$con);
	}
	
	$sql="select Cliente from clientes where clientes.Index=$estado";
	$rs= mysql_query($sql,$con);
	$a=0;
	$nombre_estado=mysql_result($rs,$a);
	
	$sql= "Insert into lectorcodigo.historial_celular (`Index`,`Fecha`,`Usuario`,`Nro_Bobina`,`Estado`) VALUES (NULL,'$fecha_scaneo','$celu',$nroBobina,'$nombre_estado')";
	$rs = mysql_query($sql,$con);
	
	?>