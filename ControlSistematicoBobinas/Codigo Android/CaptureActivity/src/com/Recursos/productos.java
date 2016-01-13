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

public class productos {
	
	
	public String ProductosPath = "ficheroProductos.txt";
	private Context ctx;
	
	public String ipPuerto;
	private lectorArchivos lectorMemInterna;
	private Hashtable<String, String> hashProductos;
	private Hashtable<String, String> hashIndices;
	
	public productos(Context ctxParam, String ipPuertoParam)
	{
		lectorMemInterna = new lectorArchivos();
		ctx = ctxParam;
		hashProductos = new Hashtable<String, String>();
		hashIndices = new Hashtable<String, String>();
		ipPuerto = ipPuertoParam;
	}
	
	public void leerProductos(ArrayList<String> datos)
	{
		try
        {
            BufferedReader fin =
                new BufferedReader(
                    new InputStreamReader(
                        ctx.openFileInput(ProductosPath)));
         
            String texto = fin.readLine();
            
            while (texto != null){
    			String delims = "[,]";
    			
    			texto = texto.replaceAll("\n", "");
    			
    			String[] tokens = texto.split(delims);
    			String producto= tokens[2] + " " + tokens[3];
    			
    			hashProductos.put(producto, tokens[1]);
    			hashIndices.put(tokens[1],producto);
    			
    			datos.add(producto);
            	texto = fin.readLine();
            }
            
            
            fin.close();
        }
        catch (Exception ex)
        {
        }	
	}
	
	public String leer(){
		HttpClient producto =new DefaultHttpClient();
		HttpContext contexto = new BasicHttpContext();
		HttpGet httpget = new HttpGet("http://"+ ipPuerto + "/Android/GetProductos.php");
		
		String resultado=null;
		try {
			HttpResponse response = producto.execute(httpget,contexto);
			HttpEntity entity = response.getEntity();
			resultado = EntityUtils.toString(entity, "UTF-8");
		} catch (Exception e) {
			// TODO: handle exception
		}
		return resultado;
	}
	
	public void actualizarProductos()
	{
		
		try
		{
			lectorMemInterna.vaciarFichero(ProductosPath, ctx);
			
			ArrayList<String> productos = obtDatosJSON(leer());
	
			for (int i=0; i<productos.size() ;i++)
			{
				//escribirFichero(clientesPath, clientes.get(i));
				lectorMemInterna.escribirFichero(ProductosPath, productos.get(i), ctx);
			}
			
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
						json.getJSONObject(i).getString("Tipo") + "," +
						json.getJSONObject(i).getString("Metros");
				listado.add(texto);
				
			}
		} catch (Exception e) {
			// TODO: handle exception
		}
		return listado;
	}
	
	public String getIndiceProducto(String key)
	{
		return hashProductos.get(key);
	}
	
	public String getNombreProducto(String key)
	{
		return hashIndices.get(key);
	}
	

}
