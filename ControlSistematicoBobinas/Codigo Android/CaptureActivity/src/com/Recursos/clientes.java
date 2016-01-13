package com.Recursos;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Hashtable;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.protocol.BasicHttpContext;
import org.apache.http.protocol.HttpContext;
import org.apache.http.util.EntityUtils;
import org.json.JSONArray;

import android.content.Context;
import android.widget.Toast;

public class clientes {

	public String clientesPath = "ficheroClientes.txt";
	private Context ctx;
	
	public String ipPuerto;
	// = "192.168.1.138:80";
	//private String ipPuerto = "181.15.203.42:8181";
	//public String ipPuerto = "192.168.1.155:8181"; //LAN BARADERO
	//public String ipPuerto = "10.0.2.2";
	//public String ipPuerto = "192.168.1.190:8181"; //LAN CASEROS

	private lectorArchivos lectorMemInterna;
	private Hashtable<String, String> hashCliente;
	private Hashtable<String, String> hashIndices;
	
	public clientes(Context ctxParam, String ipPuertoParam)
	{
		lectorMemInterna = new lectorArchivos();
		ctx = ctxParam;
		hashCliente = new Hashtable<String, String>();
		hashIndices = new Hashtable<String, String>();
		ipPuerto = ipPuertoParam;
	}
	
	public void leerClientes(ArrayList<String> datos)
	{
		try
        {
            BufferedReader fin =
                new BufferedReader(
                    new InputStreamReader(
                        ctx.openFileInput(clientesPath)));
         
            String texto = fin.readLine();
            
            while (texto != null){
    			String delims = "[,]";
    			
    			texto = texto.replaceAll("\n", "");
    			
    			String[] tokens = texto.split(delims);
    			String cliente= tokens[2];
    			
    			hashCliente.put(cliente, tokens[1]);
    			hashIndices.put(tokens[1],cliente);
    			
    			datos.add(cliente);
            	texto = fin.readLine();
            }
            
            
            fin.close();
        }
        catch (Exception ex)
        {
        }	
	}
	
	public String leer(){
		HttpClient cliente =new DefaultHttpClient();
		HttpContext contexto = new BasicHttpContext();
		HttpGet httpget = new HttpGet("http://"+ ipPuerto + "/Android/GetClientes.php");
		
		String resultado=null;
		try {
			HttpResponse response = cliente.execute(httpget,contexto);
			HttpEntity entity = response.getEntity();
			resultado = EntityUtils.toString(entity, "UTF-8");
		} catch (Exception e) {
			// TODO: handle exception
		}
		return resultado;
	}
	
	public void actualizarClientes()
	{
		
		try
		{
			lectorMemInterna.vaciarFichero(clientesPath, ctx);
			
			ArrayList<String> clientes = obtDatosJSON(leer());
	
			for (int i=0; i<clientes.size() ;i++)
			{
				//escribirFichero(clientesPath, clientes.get(i));
				lectorMemInterna.escribirFichero(clientesPath, clientes.get(i), ctx);
			}
			//Toast.makeText(ctx, "Actualizado con exito!", Toast.LENGTH_SHORT).show();
		}
		catch (Exception e)
		{
			
		}
		
	}
	
	public ArrayList<String> obtDatosJSON(String response){
		ArrayList<String> listado= new ArrayList<String>();
		try {
			JSONArray json= new JSONArray(response);
			String texto="";
			for (int i=0; i<json.length();i++){
				texto = "," + json.getJSONObject(i).getString("Index") + "," +
						json.getJSONObject(i).getString("Cliente");
				listado.add(texto);
				
			}
		} catch (Exception e) {
			// TODO: handle exception
		}
		return listado;
	}
	
	public String getIndiceCliente(String key)
	{
		return hashCliente.get(key);
	}
	
	public String getNombreCliente(String key)
	{
		return hashIndices.get(key);
	}
}
