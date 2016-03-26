package com.example.controlsistematicoqr;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.protocol.BasicHttpContext;
import org.apache.http.protocol.HttpContext;
import org.apache.http.util.EntityUtils;
import org.json.JSONArray;

import android.R.integer;
import android.content.Context;
import android.widget.Toast;

public class usuarios {

	//public String ipPuerto = "192.168.1.138:80";
	//private String ipPuerto="10.0.2.2";
	//private String ipPuerto="192.168.1.155:8181"; // LAN BARADERO
	//public String ipPuerto = "192.168.1.190:8181"; // LAN CASEROS
	public String ipPuerto;
	
	private Context ctx;
	private String privilegio="";
	
	public usuarios(Context ctxParam,String ipPuertoParam)
	{
		ctx=ctxParam;
		ipPuerto=ipPuertoParam;
	}
	
	public boolean login(String user, String pass)
	{
		return loginUser(this.leer(), user, pass);
	}
	
	private String leer(){
		HttpClient cliente =new DefaultHttpClient();
		HttpContext contexto = new BasicHttpContext();
		HttpGet httpget = new HttpGet("http://"+ ipPuerto + "/Android/GetUsuarios.php");
		
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
	
	private boolean loginUser(String response, String userParam, String passParam)
	{
		
		
		try {
			
			JSONArray json= new JSONArray(response);
			
			
			for (int i=0; i<json.length();i++){
				String usuario = json.getJSONObject(i).getString("Nombre");
				String pass = json.getJSONObject(i).getString("Password");
				String privilegioParam = json.getJSONObject(i).getString("Privilegio");
				if (usuario.toUpperCase().equalsIgnoreCase(userParam.toUpperCase()))
				{					
					if(pass.equalsIgnoreCase(passParam))
					{
						privilegio = privilegioParam;
						return true;
					}	
				}
				
				
			}
		} catch (Exception e) {
			return false;
		}
	
		return false;
	}
	
	public String getPrivilegio()
	{
		return privilegio;
	}
	
}
