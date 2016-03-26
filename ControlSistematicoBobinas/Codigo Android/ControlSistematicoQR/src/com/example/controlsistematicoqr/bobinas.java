package com.example.controlsistematicoqr;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;


import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.params.BasicHttpParams;
import org.apache.http.params.HttpConnectionParams;
import org.apache.http.params.HttpParams;
import org.apache.http.protocol.BasicHttpContext;
import org.apache.http.protocol.HttpContext;

import com.Recursos.lectorArchivos;

import android.content.Context;
import android.os.Handler;
import android.view.View;
import android.widget.Toast;

public class bobinas {
	private lectorArchivos lectorMemInterna;
	public String codigosPath = "ficheroCodigos.txt";
	private Context ctx;
	public String Numero_Bobina,estado,tel,prod,cli,maq,peso,obs,gram,esp,finb,form,fecha,turno;
	public boolean waitConexion=false;
	public boolean conexionAceptada;
	public MainActivity main;
	
	//public String ipPuerto = "192.168.1.138:80";
	//public String ipPuerto = "192.168.1.155:8181"; // LAN BARADERO
	//public String ipPuerto = "10.0.2.2";
	//public String ipPuerto = "192.168.1.190:8181"; // LAN CASEROS
	public String ipPuerto;
	
	private boolean primeraVez = true;
	
	public boolean sinCodigos=false;
	
	public bobinas(Context ctxParam, MainActivity mainactivity,String ipPuertoParam)
	{
		lectorMemInterna = new lectorArchivos();
		ctx= ctxParam;
		main= mainactivity;
		ipPuerto=ipPuertoParam;
	}

	
	public boolean enviarGet(String a, String n, String n2, String n3, String n4, String n5, String n6, String n7, String n8, String n9, String n10, String n11, String n12,String n13) {

		HttpParams httpParameters = new BasicHttpParams();
		// Set the timeout in milliseconds until a connection is established.
		// The default value is zero, that means the timeout is not used. 
		int timeoutConnection = 30000;
		
		HttpConnectionParams.setConnectionTimeout(httpParameters, timeoutConnection);
		// Set the default socket timeout (SO_TIMEOUT) 
		// in milliseconds which is the timeout for waiting for data.
		int timeoutSocket = 50000;
		
		HttpConnectionParams.setSoTimeout(httpParameters, timeoutSocket);
		 
		HttpClient httpClient = new DefaultHttpClient(httpParameters);
		HttpContext localContext = new BasicHttpContext();
		
		HttpResponse response = null;
		//String parametros = "?nombre=" + n + "&apellido=" + a+ "&edad=" + ee + "&modo=GET";
		
		String parametros = "?bob=" + Numero_Bobina + "&estado=" + estado + "&tel=" + tel+"&prod=" + prod +"&cli=" + cli + "&maq=" + maq + "&peso=" + peso + "&obs=" + obs + "&gram=" + gram + "&esp=" + esp + "&fbob=" + finb + "&form=" + form + "&fecha=" + fecha + "&turno=" + turno;
		HttpGet httpget = new HttpGet("http://"+ ipPuerto +"/Android/SetEstado.php" + parametros);
		//HttpGet httpget = new HttpGet("http://181.15.203.42:/Android/PutData.php" + parametros);
		
		
		try {
			response = httpClient.execute(httpget, localContext);
			
		} catch (Exception e) {
			return false;
		}
		return true;
	}
	
	
	public int cantLineas()
	{
		int cantLineas=0;
        try
        {
        	
    		BufferedReader finCantLineas =
                new BufferedReader(
                    new InputStreamReader(
                        ctx.openFileInput(codigosPath)));
         
            
            String linea = finCantLineas.readLine();
            
            while (linea!= null)
            {
            	cantLineas++;
            	linea = finCantLineas.readLine();
            }
            
            finCantLineas.close();
            
            return cantLineas;
            
        }
        catch (Exception ex)
        {
        	return cantLineas;
        }
        
	}
	
	public void leerFichero()
	{
		/*lectorMemInterna.vaciarFichero(codigosPath, ctx);
		String textoAGrabar= ";11;3;9500;1;1;1;asdasdasdas daa;21,0;55;10:28;2,20;2014-08-12;550,00;1";
	    int nro=3000;
		for (int i=0;i<80;i++){
			textoAGrabar= ";"+Integer.toString(nro+i)+";3;9500;1;1;1;asdasdasdas daa;21,0;55;10:28;2,20;2014-08-12;550,00;1";
			lectorMemInterna.escribirFichero(codigosPath, textoAGrabar, ctx);
		}
		*/
		int cantidad=0;
        try
        {	
        	
            BufferedReader fin =
                new BufferedReader(
                    new InputStreamReader(
                        ctx.openFileInput(codigosPath)));
         
            
            String texto = fin.readLine();
           
            waitConexion=false;
            conexionAceptada=false;
           
            sinCodigos=false;
            if (texto.getBytes().length ==1 & texto != null)sinCodigos=true;

            
            while (texto != null & texto.getBytes().length != 1){
            	cantidad++;
    			
            	String delims = "[;]";
    			
    			texto = texto.replaceAll("\n", "");
    			
    			String[] tokens = texto.split(delims);
    			Numero_Bobina= tokens[1];
    			estado= tokens[2];
    			tel=tokens[3];
    			prod=tokens[4];
    			cli=tokens[5];
    			maq=tokens[6];
    			obs=tokens[7].replaceAll(" ", "-");
    			gram=tokens[8].replace(',', '.');
    			esp=tokens[9].replace(',', '.');
    			finb=tokens[10].replaceAll(" ","");
    			form=tokens[11].replace(',', '.');
    			fecha=tokens[12];
    			peso=tokens[13].replace(',', '.');
    			turno=tokens[14];
    			
    			eltreatloco();
            	while(!waitConexion){}
            	if (conexionAceptada==false)break;
            	
            	waitConexion=false;
            	
            	texto = fin.readLine();
            }
                
            fin.close();
            
        }
        catch (Exception ex)
        {
        }

        int cantLineas=this.cantLineas();
        
        if (sinCodigos){
        	Toast.makeText(ctx, "No hay codigos para descargar.",Toast.LENGTH_LONG).show();}
        else if ((conexionAceptada==false & primeraVez==false) || (cantLineas != cantidad))
        {
        	Toast.makeText(ctx, "Error en la conexion! Asegurese de estar conectado a internet.",Toast.LENGTH_LONG).show();
		}else{
			if (cantidad >0){
        	Toast.makeText(ctx, "Cantidad registros subidos: " + cantidad,Toast.LENGTH_LONG).show();
			}else{Toast.makeText(ctx, "No hay codigos para descargar!",Toast.LENGTH_LONG).show();}
			lectorMemInterna.vaciarFichero(codigosPath, ctx);
		}
        
        if (primeraVez){primeraVez = false;}
        
	}

	
	
	public void mensajeCargaBobina(int cantidad)
	{
        final Toast toast = Toast.makeText(ctx, "Descargando codigo: " + cantidad, Toast.LENGTH_SHORT);
        toast.show();

        Handler handler = new Handler();
            handler.postDelayed(new Runnable() {
               @Override
               public void run() {
                   toast.cancel(); 
               }
        }, 1000);
            
	}
	
	public void eltreatloco()
	{
		
		
		Thread nt = new Thread() {
			@Override
			public void run() {
				conexionAceptada=enviarGet(Numero_Bobina,estado,tel,prod,cli,maq,peso,obs,gram,esp,finb,form,fecha,turno);
				waitConexion=true;
				try {
					main.runOnUiThread(new Runnable() {
						@Override
						public void run()
						{
							
						}
					});
				} catch (Exception e) {
					// TODO: handle exception
				}
			}
		};
		nt.start();
	}
	
	
	public void subirFicherosDB()
	{

		lectorMemInterna.escribirFichero(codigosPath,"AAAAA,AAsada,sda,sd",ctx);

		int cantidad=0;
        try
        {
            BufferedReader fin =
                new BufferedReader(
                    new InputStreamReader(
                        ctx.openFileInput("ficheroCodigos.txt")));
         
            
            String texto = fin.readLine();

            while (texto != null & texto.getBytes().length != 1){
            	cantidad++;
    			String delims = "[,]";
    			
    			String[] tokens = texto.split(delims);
    			Toast.makeText(ctx, tokens[0]+tokens[1]+tokens[2],Toast.LENGTH_LONG).show();
    			if (enviarGet(tokens[0],tokens[1],tel,prod,cli,maq,peso,obs,gram,esp,finb,form,fecha,turno)==false) {
            		Toast.makeText(ctx, "Error en la conexion! Asegurese de estar conectado a internet.",Toast.LENGTH_LONG).show();
            		return;
            	}
            	texto = fin.readLine();
            }
            
            
            fin.close();
            
        }
        catch (Exception ex)
        {
        }
        
        
        if (cantidad >0){
        	Toast.makeText(ctx, "Cantidad registros subidos: " + cantidad,Toast.LENGTH_LONG).show();
        }else{Toast.makeText(ctx, "No hay codigos para descargar!",Toast.LENGTH_LONG).show();}
        lectorMemInterna.vaciarFichero(codigosPath, ctx);

	}
}
