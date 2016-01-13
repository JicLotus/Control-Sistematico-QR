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

public class maquinistas {

	public String maquinistasPath = "ficheroMaquinistas.txt";
	private Context ctx;
	
	public String ipPuerto;
	private lectorArchivos lectorMemInterna;
	private Hashtable<String, String> hashMaquinistas;
	private Hashtable<String, String> hashIndices;
	
	public maquinistas(Context ctxParam, String ipPuertoParam)
	{
		lectorMemInterna = new lectorArchivos();
		ctx = ctxParam;
		hashMaquinistas = new Hashtable<String, String>();
		hashIndices = new Hashtable<String, String>();
		ipPuerto = ipPuertoParam;
	}
	
	public void leerMaquinistas(ArrayList<String> datos)
	{
		try
        {
            BufferedReader fin =
                new BufferedReader(
                    new InputStreamReader(
                        ctx.openFileInput(maquinistasPath)));
         
            String texto = fin.readLine();
            
            while (texto != null){
    			String delims = "[,]";
    			
    			texto = texto.replaceAll("\n", "");
    			
    			String[] tokens = texto.split(delims);
    			String maquinista= tokens[2];
    			
    			hashMaquinistas.put(maquinista, tokens[1]);
    			hashIndices.put(tokens[1],maquinista);
    			
    			datos.add(maquinista);
            	texto = fin.readLine();
            }
            
            
            fin.close();
        }
        catch (Exception ex)
        {
        }	
	}
	
	public String leer(){
		HttpClient maquinista =new DefaultHttpClient();
		HttpContext contexto = new BasicHttpContext();
		HttpGet httpget = new HttpGet("http://"+ ipPuerto + "/Android/GetMaquinistas.php");
		
		String resultado=null;
		try {
			HttpResponse response = maquinista.execute(httpget,contexto);
			HttpEntity entity = response.getEntity();
			resultado = EntityUtils.toString(entity, "UTF-8");
		} catch (Exception e) {
			// TODO: handle exception
		}
		return resultado;
	}
	
	public void actualizarMaquinistas()
	{
		
		try
		{
			lectorMemInterna.vaciarFichero(maquinistasPath, ctx);
			
			ArrayList<String> maquinistas = obtDatosJSON(leer());
	
			for (int i=0; i<maquinistas.size() ;i++)
			{
				//escribirFichero(clientesPath, clientes.get(i));
				lectorMemInterna.escribirFichero(maquinistasPath, maquinistas.get(i), ctx);
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
						json.getJSONObject(i).getString("Maquinista");
				listado.add(texto);
				
			}
		} catch (Exception e) {
			// TODO: handle exception
		}
		return listado;
	}
	
	public String getIndiceMaquinistas(String key)
	{
		return hashMaquinistas.get(key);
	}
	
	public String getNombreMaquinistas(String key)
	{
		return hashIndices.get(key);
	}
	
}
