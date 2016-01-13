package com.Recursos;

import java.io.FileNotFoundException;

import android.content.Context;
import android.util.Log;
import android.widget.Toast;

import java.io.*;


public class lectorArchivos {


	public void vaciarFichero(String path, Context ctx)
	{
		FileOutputStream fos;
        try {
            fos = ctx.openFileOutput(path, Context.MODE_PRIVATE);
            fos.write(0);
            fos.close();
        } catch (FileNotFoundException e) {
            Log.e("Mi Aplicación",e.getMessage(),e);
        } catch (IOException e) {
            Log.e("Mi Aplicación",e.getMessage(),e);
        }
        
	}
	
	public void escribirFichero(String path, String data, Context ctx)
	{
        data = data + "\n";
        FileOutputStream fos;
        try {
        	   fos = ctx.openFileOutput(path,Context.MODE_APPEND);
			   fos.write(data.getBytes());
			   fos.close();
        } catch (FileNotFoundException e) {
               Log.e("Mi Aplicación",e.getMessage(),e);
        } catch (IOException e) {
               Log.e("Mi Aplicación",e.getMessage(),e);
        }
        
	}

	
}









/*
Thread nt = new Thread() {
	@Override
	public void run() {
		EditText nombre = (EditText) findViewById(R.id.et_nombre);
		EditText apellido = (EditText) findViewById(R.id.et_apellido);
		EditText edad = (EditText) findViewById(R.id.et_edad);
		CheckBox modo = (CheckBox) findViewById(R.id.ck_modo);

		try {
			final String res;
			if (modo.isChecked()) {
				res = enviarGet(nombre.getText().toString(), apellido
						.getText().toString(), edad.getText().toString());

			} else {
				res = enviarPost(nombre.getText().toString(), apellido
						.getText().toString(), edad.getText().toString());
			}

			runOnUiThread(new Runnable() {
				@Override
				public void run() {
					Toast.makeText(MainActivity.this, res,
							Toast.LENGTH_LONG).show();
				}
			});
		} catch (Exception e) {
			// TODO: handle exception
		}
	}
};
nt.start();*/





/*
public void leerFichero(View view)
{
	
	
	Thread nt = new Thread() {
		@Override
		public void run() {

			escribirFichero("AAAAA,AAsada,sda,sd");
			escribirFichero("AAAAA,AAsada,jaja,sd");
			int cantidad=0;
			
			try {
				
		         BufferedReader fin =
		                 new BufferedReader(
		                     new InputStreamReader(
		                         openFileInput("ficheroCodigos.txt")));
		          
		             
		         String texto = fin.readLine();
		         
		         	
		           while (texto != null & texto.getBytes().length != 1){
		        	   
		        	   	cantidad++;
		    			String delims = "[,]";
		    			
		    			String[] tokens = texto.split(delims);
		    			final boolean res=enviarGet(tokens[0],tokens[1],tokens[2]);
		    			final String a= tokens[2];
						runOnUiThread(new Runnable() {
							
							@Override
							public void run() {
								Toast.makeText(MainActivity.this,a,Toast.LENGTH_LONG).show();
			            		if (res==false)
			            		{
			            			Toast.makeText(MainActivity.this, "Error en la conexion! Asegurese de estar conectado a internet.",Toast.LENGTH_LONG).show();
			            			return;
			            		}
			            		
								
							}
						});
						Toast.makeText(MainActivity.this,a,Toast.LENGTH_LONG).show();
						texto = fin.readLine();
		           }
		           fin.close();
						
			} catch (Exception e) {
			}
		}
	};
	nt.start();
    
}*/
