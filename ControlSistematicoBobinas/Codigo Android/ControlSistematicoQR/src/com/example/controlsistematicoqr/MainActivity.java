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

	
	private String nombreUsuario;
	public Activity actividad;
	public String ipPuerto;
	public String privilegio;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		

	}
	
	public void listadoOnClick(View view){
		
	}

	public void ingresarBobina(View view)
	{

	}
	
	public void obtenerHistorial(View view){

	}
	
	
	public void EnviarOnClik(View view) {

	}

	public void leerFichero(View view)
	{
		
	}
	

}
