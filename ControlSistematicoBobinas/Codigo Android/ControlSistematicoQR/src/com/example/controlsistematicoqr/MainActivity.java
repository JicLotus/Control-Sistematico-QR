package com.example.controlsistematicoqr;

import java.io.IOException;
import java.text.NumberFormat;
import java.text.ParseException;
import java.util.Locale;

import com.Recursos.clientes;
import com.Recursos.lectorArchivos;
import com.Recursos.maquinistas;
import com.Recursos.productos;
import com.example.controlsistematicoqr.R;
import com.parceleable.ParcelableObject;

import android.os.Bundle;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.telephony.TelephonyManager;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.View;
import android.widget.Toast;


public class MainActivity extends Activity {

	
	private clientes Clientes;
	private bobinas Bobinas;
	private maquinistas Maquinistas;
	private productos Productos;
	private usuarios Usuarios;
	
	private String nombreUsuario;
	
	public Activity actividad;
	public String ipPuerto;
	public String privilegio;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		
		Bundle extras = getIntent().getExtras();
		nombreUsuario = extras.getString("NOMBRE_USUARIO");
		ipPuerto = extras.getString("IPPUERTO");
		privilegio = extras.getString("PRIVILEGIO");
		
		Intent intent = getIntent();
		ParcelableObject parcelableObject = (ParcelableObject) intent.getParcelableExtra("USUARIOS");

		/////////Creamos todos los recursos para hacer la unica conexion///////////////////
		Bobinas = new bobinas(this.getApplicationContext(), MainActivity.this,ipPuerto);
		Clientes = new clientes(this.getApplicationContext(), ipPuerto);
		Usuarios = (usuarios)parcelableObject.getParceleableObject();
		Maquinistas = new maquinistas(this.getApplicationContext(),ipPuerto);
		Productos = new productos(this.getApplicationContext(),ipPuerto);
		///////////////////////////////////////////////////////////////////////////////////
		
		Clientes.actualizarClientes();
		Maquinistas.actualizarMaquinistas();
		Productos.actualizarProductos();
		
		actividad=this;
	}
	
	public void listadoOnClick(View view){
		startActivity(new Intent(this, ListadoActivity.class));
	}

	public void ingresarBobina(View view)
	{
		if (Integer.parseInt(privilegio) == 1 || Integer.parseInt(privilegio) == 3)
		{
			Intent intent = new Intent(this, IngresoBobinaActivity.class);
			intent.putExtra("NOMBRE_USUARIO", nombreUsuario);
			startActivity(intent);
		}
		else
		{
			Toast.makeText(getApplicationContext(), "No tienes suficiente privilegio.", Toast.LENGTH_SHORT).show();
		}
		
	}
	
	public void obtenerHistorial(View view){
		Intent intent = new Intent(this, HistorialActivity.class);
		intent.putExtra("IPPUERTO", ipPuerto);
		startActivity(intent);
	}
	
	
	public void EnviarOnClik(View view) {
		Intent intent = new Intent("com.google.zxing.client.android.SCAN");
		intent.putExtra("SCAN_MODE", "QR_CODE_MODE");
		intent.putExtra("CARGAR_CODIGO","SI");
		intent.putExtra("NOMBRE_USUARIO", nombreUsuario);
		intent.putExtra("IPPUERTO",ipPuerto);
		startActivityForResult(intent, 0);
	}

	public void leerFichero(View view)
	{
		Bobinas.leerFichero();
		
  		lectorArchivos lectorMemInterna = new lectorArchivos();
  		lectorMemInterna.vaciarFichero("clienteElegido.txt", getApplicationContext());
	}
	
	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {
	   
	  if (keyCode == KeyEvent.KEYCODE_BACK) {
		  
	    new AlertDialog.Builder(this)
	      .setIcon(android.R.drawable.ic_dialog_alert)
	      .setTitle("Cerrar sesion")
	      .setMessage("Estás seguro?")
	      .setNegativeButton(android.R.string.cancel, null)//sin listener
	      .setPositiveButton(android.R.string.ok, new DialogInterface.OnClickListener() {//un listener que al pulsar, cierre la aplicacion
	        @Override
	        public void onClick(DialogInterface dialog, int which){
	          //Salir
	        	actividad.finish(); 
	        }
	      })
	      .show();
	    // Si el listener devuelve true, significa que el evento esta procesado, y nadie debe hacer nada mas
	    //return true;
	  }
	//para las demas cosas, se reenvia el evento al listener habitual
	  return super.onKeyDown(keyCode, event);
	} 
	

}
