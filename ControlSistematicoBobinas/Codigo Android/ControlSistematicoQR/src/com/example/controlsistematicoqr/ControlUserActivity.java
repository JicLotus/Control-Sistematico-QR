package com.example.controlsistematicoqr;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.os.Parcelable;
import android.text.Editable;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayList;

import com.Recursos.lectorArchivos;
import com.parceleable.ParcelableObject;

public class ControlUserActivity extends Activity {

	
	usuarios Usuarios;
	String resultado;
	public Activity actividad;
    private Spinner spinner1;
    Context ctx;
    String ip;
    
	@Override
	protected void onCreate(Bundle savedInstanceState) 
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_control_user);
		spinner1 = (Spinner) findViewById(R.id.spMaquinistas);
		ctx=this.getApplicationContext();
		cargarServidores();
		
		Usuarios = new usuarios(getApplicationContext(),ip);
		
		actividad=this;
	}
	
	
	public void clickIngresar(View view)
	{
		
		EditText txtUser = (EditText)findViewById(R.id.txtFormato);
		EditText txtPass = (EditText)findViewById(R.id.txtNroBobina);
		
		lectorArchivos lectorMemInterna = new lectorArchivos();
		lectorMemInterna.vaciarFichero("servidores.txt", ctx);
		lectorMemInterna.escribirFichero("servidores.txt", "," +Integer.toString(spinner1.getSelectedItemPosition()), ctx);
		
		if (!Usuarios.login(txtUser.getText().toString(), txtPass.getText().toString()))
		{
			Toast.makeText(getApplicationContext(), "Usuario o contraseña incorrecta.", Toast.LENGTH_LONG).show();
		}else
		{
			Intent intent = new Intent(this, MainActivity.class);
			intent.putExtra("NOMBRE_USUARIO", txtUser.getText().toString());
			intent.putExtra("IPPUERTO", ip);
			intent.putExtra("PRIVILEGIO", Usuarios.getPrivilegio());
			
			ParcelableObject parcelableObject = new ParcelableObject(Usuarios);
			intent.putExtra("USUARIOS",parcelableObject);
			
			startActivity(intent);
		}
		
		
	}
	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {
	   
	  if (keyCode == KeyEvent.KEYCODE_BACK) {
		  
	    new AlertDialog.Builder(this)
	      .setIcon(android.R.drawable.ic_dialog_alert)
	      .setTitle("Salir")
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
	    return true;
	  }
	//para las demas cosas, se reenvia el evento al listener habitual
	  return super.onKeyDown(keyCode, event);
	}
	
	private void cargarServidores()
	{
		
		ArrayList datos = new ArrayList<String>();
		
		datos.add("Baradero");
		datos.add("Caseros");
		
		ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item,datos);
		dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spinner1.setAdapter(dataAdapter);
		
		try
        {
            BufferedReader fin =
                new BufferedReader(
                    new InputStreamReader(
                        ctx.openFileInput("servidores.txt")));
         
            String primeraLinea = fin.readLine().replaceAll("\n", "");
            primeraLinea = primeraLinea.replaceAll(" ","");
            primeraLinea= primeraLinea.split(",")[1];
            spinner1.setSelection(Integer.parseInt(primeraLinea.toString()));
            
            fin.close();
        }
        catch (Exception ex)
        {
        	spinner1.setSelection(0);
        }	
		
		//BARADERO LAN
		if (spinner1.getSelectedItemPosition()==0)
		{
			ip="192.168.1.155:8181";
		}
		//CASEROS LAN
		else
		{
			ip="192.168.1.190:8181";
		}
		
		ip = "192.168.0.4:8181";
	}
	
}
