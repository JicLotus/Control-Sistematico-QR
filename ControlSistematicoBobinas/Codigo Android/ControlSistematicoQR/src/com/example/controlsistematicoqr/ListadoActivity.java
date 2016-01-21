package com.example.controlsistematicoqr;

import java.util.ArrayList;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.protocol.BasicHttpContext;
import org.apache.http.protocol.HttpContext;
import org.apache.http.util.EntityUtils;

import com.Recursos.clientes;
import com.Recursos.maquinistas;
import com.Recursos.productos;
import com.google.zxing.client.android.CaptureActivity;

import android.os.Bundle;
import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.view.Menu;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;

@SuppressLint("InlinedApi") public class ListadoActivity extends Activity {

	private maquinistas Maquinistas;
	private productos Productos;
	private clientes Clientes;
	
	private Context ctx;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_listado);
		
		ctx = this.getApplicationContext();
		
		Maquinistas = new maquinistas(this.getApplicationContext(),"");
		Productos = new productos(this.getApplicationContext(),"");
		Clientes = new clientes(this.getApplicationContext(),"");
		ArrayList<String> datos = new ArrayList<String>();
		Clientes.leerClientes(datos);
		Productos.leerProductos(datos);
		Maquinistas.leerMaquinistas(datos);
		configureButtonReader();
	}

	
	private void configureButtonReader() {  

		Intent intent = new Intent("com.google.zxing.client.android.SCAN");
		intent.putExtra("SCAN_MODE", "QR_CODE_MODE");
		startActivityForResult(intent, 0);
	}  
	
	
    public void onActivityResult(int requestCode, int resultCode, Intent intent) {  
        
    	   if (requestCode == 0) {
		      if (resultCode == RESULT_OK) {
		         String contents = intent.getStringExtra("SCAN_RESULT");
		         String format = intent.getStringExtra("SCAN_RESULT_FORMAT");
		         cargaListado(obtDatosJSON(contents));
		         intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TASK);
		         // Handle successful scan
		      } else if (resultCode == RESULT_CANCELED) {
		         // Handle cancel
		      }
    	   }
    	
    }  
	
	/*
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.listado, menu);
		return true;
	}*/
	
	public void cargaListado(ArrayList<String> datos){
		ArrayAdapter<String> adaptador =
			new ArrayAdapter<String>(this,android.R.layout.simple_list_item_1,datos);
		ListView listado = (ListView) findViewById(R.id.listView1);
		listado.setAdapter(adaptador);
	}
	
	
	public ArrayList<String> obtDatosJSON(String response){
		ArrayList<String> listado= new ArrayList<String>();
		try {

			if (response.length()<=130)
			{
				String delims = "[;]";
				String[] tokens = response.split(delims);
				String peso, nroBobina,producto,cliente,maquinista,obs,gram,espesor,finBob,form,fecha,turno;
				
				nroBobina=tokens[0];
				gram=tokens[1];
				espesor=tokens[2];
				peso=tokens[3];
				finBob=tokens[4].replaceAll(" p.m.","");
		    	finBob=finBob.replaceAll(" a.m.","");
		    	form=tokens[5];
		    	obs=tokens[6].replaceAll("\n","");
		    	fecha=tokens[7];
		    	cliente=tokens[8];
		    	producto=tokens[9];
		    	maquinista=tokens[10];
	    		turno =tokens[11];
				
	
				String[] tokens2= fecha.split("[-=]");
				listado.add("Fecha:" + tokens2[2] +"-"+ tokens2[1] +"-" + tokens2[0]);
	    		
	    		listado.add("Nro Bobina:" + nroBobina);
	    		listado.add("Gramaje:" + gram);
	    		listado.add("Espesor:" + espesor);
	    		listado.add("Peso:" + peso);
	    		listado.add("Fin Bob:" + finBob);
	    		listado.add("Formato:" + form);
	    		listado.add("Observacion:" + obs);
	    		listado.add("Cliente:" + Clientes.getNombreCliente(cliente));
	    		listado.add("Producto:" + Productos.getNombreProducto(producto));
	    		listado.add("Maquinista:" + Maquinistas.getNombreMaquinistas(maquinista));
			}
			else
			{
				String delims = "[;]";
				String[] tokens = response.split(delims);
				for (int i=0; i< tokens.length-3;i++)
				{
					listado.add(tokens[i]);
				}
				
				String fecha= listado.get(9);
				String[] tokens2= fecha.split("[-=]");
				listado.set(9, tokens2[0] + "=" + tokens2[3] +"-"+ tokens2[2] +"-" + tokens2[1]);
			}
			
		} catch (Exception e) {
			// TODO: handle exception
		}
		return listado;
	}

}