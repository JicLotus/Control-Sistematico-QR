package com.example.controlsistematicoqr;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayList;

import com.Recursos.clientes;
import com.example.controlsistematicoqr.R;

import android.app.Activity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.ArrayAdapter;
import android.widget.ListView;

public class HistorialActivity extends Activity {

	public String codigosPath = "ficheroCodigos.txt";
	public boolean sinCodigos=false;
	public String Numero_Bobina,estado;
	private clientes Clientes;
	
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_historial);
		
		Bundle extras = getIntent().getExtras();
		String ipPuerto = extras.getString("IPPUERTO");
		
		Clientes = new clientes(this.getApplicationContext(),ipPuerto);
		
		ArrayList<String> datos = new ArrayList<String>();
		Clientes.leerClientes(datos);
		this.cargaListado(leerHistorial());
	}
	

	
	private ArrayList<String> leerHistorial()
	{
		ArrayList<String> listado= new ArrayList<String>();
	    try
	    {
	        BufferedReader fin =
	            new BufferedReader(
	                new InputStreamReader(openFileInput(codigosPath)));
	     
	        
	        String texto = fin.readLine();
	       
	        sinCodigos=false;
	        if (texto.getBytes().length==1 & texto != null)sinCodigos=true;
	
	        
	        while (texto != null & texto.getBytes().length != 1){
				
	        	String delims = "[;]";
				
				texto = texto.replaceAll("\n", "");
				
				String[] tokens = texto.split(delims);
				Numero_Bobina= tokens[1];
				estado= Clientes.getNombreCliente(tokens[2]);
				listado.add("Bobina: " + Numero_Bobina + " Estado: " + estado);
				
				/*tel=tokens[3];
				prod=tokens[4];
				cli=tokens[5];
				maq=tokens[6];
				obs=tokens[7].replaceAll(" ", "-");
				//Toast.makeText(ctx, obs, Toast.LENGTH_LONG).show();
				gram=tokens[8].replace(',', '.');
				esp=tokens[9].replace(',', '.');
				finb=tokens[10];
				form=tokens[11].replace(',', '.');
				fecha=tokens[12];
				peso=tokens[13].replace(',', '.');*/
		
	        	texto = fin.readLine();
	        }
	        
	        fin.close();
	        
	    }
	    catch (Exception ex)
	    {
	    }
	    
	    if (sinCodigos || listado.size()==0){ listado.add("NO HAY CODIGOS CARGADOS.");}
	    
	    return listado;
	    
	}
	
	public void cargaListado(ArrayList<String> datos){
		ArrayAdapter<String> adaptador =
			new ArrayAdapter<String>(this,android.R.layout.simple_list_item_1,datos);
		ListView listado = (ListView) findViewById(R.id.listView1);
		listado.setAdapter(adaptador);
	}
	
}
