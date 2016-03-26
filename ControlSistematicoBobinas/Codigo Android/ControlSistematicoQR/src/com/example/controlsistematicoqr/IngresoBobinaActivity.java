package com.example.controlsistematicoqr;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Locale;

import com.Recursos.clientes;
import com.Recursos.lectorArchivos;
import com.Recursos.maquinistas;
import com.Recursos.productos;

import android.app.Activity;
import android.content.res.Configuration;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.DatePicker;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.TimePicker;
import android.widget.Toast;

public class IngresoBobinaActivity extends Activity {

	Spinner spTurnos;
	Spinner spClientes;
	Spinner spMaquinistas;
	Spinner spProductos;
	Spinner spEstado;
	private String nombreUsuario;
	private String codigosPath = "ficheroCodigos.txt";
	private lectorArchivos lectorMemInterna;
	
	private clientes Clientes;
	private maquinistas Maquinistas;
	private productos Productos;
	
	private TimePicker tpHora;
	private DatePicker dpFecha;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_ingreso_bobina);
		
		
		dpFecha = (DatePicker)this.findViewById(R.id.dpFecha);
	    tpHora = (TimePicker) this.findViewById(R.id.tpHora);
	    tpHora.setIs24HourView(true);

	    Calendar calendar = Calendar.getInstance();

	    int h = calendar.get(Calendar.HOUR_OF_DAY);
	    int m = calendar.get(Calendar.MINUTE);
	    tpHora.setCurrentHour(h);
	    tpHora.setCurrentMinute(m);
	    
	    
		Bundle extras = getIntent().getExtras();
		nombreUsuario = extras.getString("NOMBRE_USUARIO");
	    
	    lectorMemInterna = new lectorArchivos();
	    
	    this.llenarTurnos();
	    this.llenarClientesyDestino();
	    this.llenarMaquinistas();
	    this.llenarProductos();
	}

	private void llenarProductos()
	{
		spProductos = (Spinner)this.findViewById(R.id.spCalidad);
		Productos = new productos(this.getApplicationContext(),"");
	
		ArrayList<String> datos = new ArrayList<String>();
		
		Productos.leerProductos(datos);
		
		ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item,datos);
		dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spProductos.setAdapter(dataAdapter);
	}
	
	private void llenarMaquinistas()
	{
		spMaquinistas = (Spinner)this.findViewById(R.id.spMaquinistas);
		Maquinistas = new maquinistas(this.getApplicationContext(),"");
		
		ArrayList<String> datos = new ArrayList<String>();
		
		Maquinistas.leerMaquinistas(datos);
		
		ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item,datos);
		dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spMaquinistas.setAdapter(dataAdapter);
	}
	
	private void llenarClientesyDestino()
	{
		spClientes = (Spinner)this.findViewById(R.id.spClientes);
		spEstado = (Spinner)this.findViewById(R.id.spEstado);
		Clientes = new clientes(this.getApplicationContext(), "");
		
		ArrayList<String> datos = new ArrayList<String>();
		
		Clientes.leerClientes(datos);
		
		ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item,datos);
		dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spClientes.setAdapter(dataAdapter);
		spEstado.setAdapter(dataAdapter);
	}
	
	private void llenarTurnos()
	{
	    spTurnos = (Spinner)this.findViewById(R.id.spTurnos);
	    
		ArrayList<String> datos = new ArrayList<String>();
		
		datos.add("21 a 5");
		datos.add("5 a 13");
		datos.add("13 a 21");
		
		ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item,datos);
		dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spTurnos.setAdapter(dataAdapter);
	}
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
	
	public void ingresarBobina(View view)
	{
		TextView txtNroBobina = (TextView) this.findViewById(R.id.txtNroBobina);
		TextView txtEspesor = (TextView)this.findViewById(R.id.txtEspesor);
		TextView txtFormato = (TextView)this.findViewById(R.id.txtFormato);
		TextView txtGramaje= (TextView)this.findViewById(R.id.txtGramaje);
		TextView txtPeso= (TextView)this.findViewById(R.id.txtPeso);
		TextView txtObs= (TextView)this.findViewById(R.id.txtObs);
		
		String nroBobina=txtNroBobina.getText().toString();
		String idCliente=Clientes.getIndiceCliente(spClientes.getSelectedItem().toString());
		String idProducto=Productos.getIndiceProducto(spProductos.getSelectedItem().toString());
		String idEstado=Clientes.getIndiceCliente(spEstado.getSelectedItem().toString());
		String idMaquinista =Maquinistas.getIndiceMaquinistas(spMaquinistas.getSelectedItem().toString());;
		String obs="*"+ txtObs.getText().toString();
		String gram=txtGramaje.getText().toString();
		String espesor=txtEspesor.getText().toString();
		String finBob=tpHora.getCurrentHour() + ":" + tpHora.getCurrentMinute();
		String form=txtFormato.getText().toString();
		
		int month=dpFecha.getMonth()+1;
		String fecha= dpFecha.getYear() + "-" + month + "-" + dpFecha.getDayOfMonth();
		String peso=txtPeso.getText().toString();
		String turno="1";
		
        if (spTurnos.getSelectedItemId()==1)
        {
            turno = "2";
        }
        else if (spTurnos.getSelectedItemId()==2)
        {
            turno = "3";
        }
		
	    
	    String textoAGrabar= ";" + nroBobina + ";" + idEstado;
	    textoAGrabar+=";" + nombreUsuario + ";" + idProducto + ";" + idCliente + ";" + idMaquinista + ";" + obs + ";" + gram + ";" + espesor;
	    textoAGrabar+=";" + finBob + ";" + form + ";" + fecha + ";" + peso + ";" + turno;
	    
	    lectorMemInterna.escribirFichero(codigosPath, textoAGrabar, this.getApplicationContext());
	    
	    Toast.makeText(getApplicationContext(), "Bobina cargada con exito!", Toast.LENGTH_SHORT).show();
	}

}
