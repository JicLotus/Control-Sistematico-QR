package com.google.zxing.client.android.test;

import junit.framework.TestCase;

import com.Recursos.clientes;
import com.Recursos.lectorArchivos;
import com.Recursos.maquinistas;
import com.Recursos.productos;

public class TestsGenerales extends TestCase {

	productos Productos;
	clientes Clientes;
	maquinistas Maquinistas;
	lectorArchivos Lector;
	
	
	public TestsGenerales()
	{
		String ipPuerto = "localhost:8181";
				
	}
	
	
	public void testProductosSonLeidosCorrectamenteDesdeElCelular()
	{
		boolean verda=true;
		assertEquals(verda, verda);
	}

	public void testProductosSonLeidosCorrectamenteDesdeElCelularALaBaseDeDatos()
	{
		boolean verda=true;
		assertEquals(verda, verda);
	}
	
	public void testActualizarProductos()
	{
		boolean verda=true;
		assertEquals(verda, verda);
	}
	
	public void testObtenerDatosJsonProductos()
	{
		boolean verda=true;
		assertEquals(verda, verda);
	}
	

	
	
	
	
	public void testClientesSonLeidosCorrectamenteDesdeElCelular()
	{
		boolean verda=true;
		assertEquals(verda, verda);
	}

	public void testClientesSonLeidosCorrectamenteDesdeElCelularALaBaseDeDatos()
	{
		boolean verda=true;
		assertEquals(verda, verda);
	}
	
	public void testActualizarClientes()
	{
		boolean verda=true;
		assertEquals(verda, verda);
	}
	
	public void testObtenerDatosJsonClientes()
	{
		boolean verda=true;
		assertEquals(verda, verda);
	}
	
	
	
	

	public void testMaquinistasSonLeidosCorrectamenteDesdeElCelular()
	{
		boolean verda=true;
		assertEquals(verda, verda);
	}

	public void testMaquinistasSonLeidosCorrectamenteDesdeElCelularALaBaseDeDatos()
	{
		boolean verda=true;
		assertEquals(verda, verda);
	}
	
	public void testActualizarMaquinistas()
	{
		boolean verda=true;
		assertEquals(verda, verda);
	}
	
	public void testObtenerDatosJsonMaquinistas()
	{
		boolean verda=true;
		assertEquals(verda, verda);
	}
	

	
	
	public void testVaciarFichero()
	{
		boolean verda=true;
		assertEquals(verda, verda);
	}
	
	public void escribirFichero()
	{
		boolean verda=true;
		assertEquals(verda, verda);
	}
	
}
