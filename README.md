# Registro y control sistem�tico de stock de bobinas de papel

### Objetivo
El proyecto surge con el objetivo de resolver la problem�tica del control de bobinas de una papelera recicladora, CelulosaBaradero S.A. El proyecto se desarrollar� a medida de la empresa

### Requerimientos Funcionales

Introducci�n:
La papelera de reciclaje cuenta con tres plantas recicladoras:

1. La primer planta recicladora se encuentra situada en Caseros y ser� la que recibir� la materia prima a reciclar. La misma se encargar� de compactarla y luego de enviarla a la siguiente planta.

2. Esta planta se encuentra situada en Baradero y la misma se encarga de procesar la materia prima para sacar las bobinas de papel. Luego esas bobinas podr�n quedarse como stock en esta planta, ser vendidas directamente a un cliente o ser despachadas a la tercer planta recicladora para que queden como stock all�.
Proyecto de Trabajo Profesional
2

3. Esta planta tambi�n se ubica en Caseros y recibe bobinas de papel proveniente de la planta de Baradero. Las mismas pueden quedar como stock y luego ser vendidas directamente a un cliente o tambi�n desarrollar papel higi�nico.
Nuestro sistema a plantear se situar� en las plantas 2 y 3. Primeramente se pondr�n 2 servidores en paralelo. Uno se lo instalara en la planta 2 y el otro en la 3 ya que los registros de la planta 2 son independientes de la planta 3. Esta forma de almacenar los datos se planteo por el hecho de que la planta de Baradero sufre frecuentes cortes de internet y si no se hiciera de esta manera seria imposible el trabajo continuo.
En la planta 2 se ubicar� el Sistema de formulario para obreros el cual es el principal y �nico ingreso de bobinas al sistema. La forma que se ingresaran las bobinas a la planta 3 ser� mediante la Aplicaci�n de celular para el escaneo y control de las bobinas.
Para el control de las bases de datos se armar� un Sistema administrador para corroborar el stock de bobinas el cual estar� instalado tanto en la planta 2 como en la 3. Desde el mismo se puede acceder a cualquier base de datos de los 2 servidores existentes.

####Las principales funcionalidades serian:

1. Sistema de formulario para los obreros
El obrero de turno primeramente deber� elegir su nombre para ingresar al formulario. Cuando las bobinas est�n listas para ser pesadas, el sistema comienza captando el peso de la balanza y se lo plasmar�a en el formulario.
Una vez que el obrero ingrese los datos respectivos de la bobina (Gramaje,Formato,Material,Espesor y Cliente ) que se acaba de pesar, ingresar� la bobina al stock y saldr� impreso un rotulo con los datos de dicha bobina, este rotulo se pondr� por debajo del cobertor de la bobina que acaba de salir.
El rotulo impreso adem�s de contener los datos de la bobina respectiva tambi�n contendr�a un c�digo QR que avalar�a estos datos y el mismo ser� �til en la etapa de escaneo con el celular.
La comunicaci�n entre el formulario y la balanza ser� a trav�s de un puerto RS2
32. Se deber�n averiguar los datos en el fabricante de balanza para saber la frecuencia y la secuencia de bytes a recibir para ser captadas correctamente. Tambi�n algo a tener en cuenta para implementar es el hecho de que la planta en donde se cargan las bobinas se encuentra en Baradero y al ser un lugar aislado de la ciudad sufre
Proyecto de Trabajo Profesional
3
frecuentes cortes de luz y por las largas distancias de la planta suele ser frecuente la ca�da de se�al del router. Por lo tanto para afrontar esta problem�tica se planteo un modelo en el cual los datos se copien en 2 lugares distintos en la computadora. Una copia se har� en el disco principal y otra en un USB. Para seguir trabajando por m�s que no haya conexi�n con la base de datos, se har�n copias de las consultas que deb�an haberse realizado(a las direcciones de disco principal y usb).

2. Aplicaci�n de celular para el escaneo y control de las bobinas
El obrero debe ingresar su usuario en la aplicaci�n. Cuando este ingresado en el sistema aparecer�n las siguientes opciones:
a. Cargar c�digos al tel�fono
���� Se cargar�an y leer�an los c�digos QR de los r�tulos impresos de la etapa anterior cuando esto se lo requiera. Antes de ser escaneados se le deber� indicar a que cliente ser� enviada dicha bobina.
b. Historial
���� Se tendr�n los n�meros y a que clientes ir�n de las bobinas cargadas en la etapa anterior
c. Analizar c�digo
���� Ser� un modo libre de lectura de c�digo QR en donde se podr� usar QR para saber de antemano cual es el contenido del c�digo QR del rotulo.
d. Ingresar Bobina
���� Esta opci�n ser� �til en el caso de que el c�digo QR del rotulo este en muy malas condiciones y este ya no pueda ser le�do por el lector del celular, por lo tanto debe ser cargado a mano.
e. Descargar c�digos le�dos
���� Una vez que se hayan cargado todas las bobinas, debe asegurarse estar conectado a la red para poder descargar todas las bobinas le�das con el celular y actualizar la base de datos.
���� Esta descarga tendr� medidas de seguridad como el hecho de que si en la mitad de la descarga se corta la se�al al servidor no se descargaran las bobinas y se deber� comenzar nuevamente con dicha descarga.
Proyecto de Trabajo Profesional
4
���� Los c�digos no se descargan a medida que se escanea por la problem�tica descripta en el punto 1). Se deben descargar todos juntos.

3. Sistema administrador para corroborar el stock de bobinas
Al igual que la aplicaci�n del celular el administrador de stock tendr� un loguin en el que pedir� usuario y contrase�a. El sistema administrador tendr� las principales categor�as:
a. Vista
1. Datos Cargados
���� Mediante los filtros seleccionados, en esta vista se mostraran las bobinas que est�n cargadas en el sistema. Las mismas se mostraran en una grilla de forma paginada, mostrando 50 bobinas por hoja.
���� Tambi�n se mostraran los pesos totales y la cantidad de bobinas dentro de esa selecci�n de filtrado.
���� Dependiendo del privilegio del administrador se dejara o no editar/eliminar las bobinas de los registros de la base de datos.
2. Historial Escaneo
���� Mostrara los escaneos realizados por el celular. Sera un historial para hacer un seguimiento de la carga de bobinas por parte de los celulares, mostrando la fecha y el lugar en donde se ha enviado a la bobina.
3. Observaciones Generales
���� Aqu� aparecer�n las observaciones generales puestas por los obreros en la carga de los formularios. Por ejemplo: En la maquina principal de Baradero hay una etapa de cambio de cuchillas y los obreros desde el formulario indican si tuvieron o no alg�n inconveniente y lo dejan registrado.
4. Productos
���� Se reflejar�n y podr�n editar los diferentes tipos de productos y calidad existentes en la empresa. Dependiendo del privilegio del administrador se dejara o no ingresar en esta secci�n.
5. Maquinistas
Proyecto de Trabajo Profesional
5
���� Sera el registro de los Obreros que trabajan en la planta. El mismo servir� a la hora de ingresar el obrero desde la planta. Dependiendo del privilegio del administrador se dejara o no ingresar en esta secci�n.
6. Clientes
���� Se mostraran los clientes que tiene la empresa. Dependiendo del privilegio del administrador se dejara o no ingresar en esta secci�n.
7. Usuarios
���� Se mostraran todos los usuarios que tiene el sistema de administraci�n en si junto a su privilegio y contrase�a. Estos usuarios responden tanto al sistema de administraci�n como al sistema de la aplicaci�n del celular. Dependiendo del privilegio del administrador se dejara o no ingresar en esta secci�n.
b. Filtros
���� Se plantearan los diferentes tipos de filtros que deber� tener el sistema para poder buscar una bobina o para poder armar los diferentes tipos de informes. Estos filtros ser�n reflejados en la grilla de Datos Cargados.
c. Imprimir
1. Remito
���� Dependiendo del filtro que se haya hecho se armara un remito para imprimir y archivar. Sirve para tener bien discriminado que cantidad de bobinas, tipo y peso han sido despachadas a los diferentes tipos de clientes por cami�n.
2. Parte Diario Maquinista
���� Con este informe se tendr�n los resultados de los rangos horarios, qu� cantidad y peso de bobinas fue en lo que estuvieron en lo que trabajando los obreros durante el d�a. Se podr� saber tambi�n las fallas que se produjeron durante el d�a.
3. Rotulo Bobina
���� Se podr�n imprimir los r�tulos de las bobinas seleccionadas desde el administrador. Llegar�a a ser �til en el caso de que llegara a romperse un rotulo de bobina.
Proyecto de Trabajo Profesional
6
d. Vista Previa
1. Remito
���� Se puede hacer una vista previa de los Remitos descriptos anteriormente sin tener que imprimirlos.
2. Rotulo Bobina
���� Se puede hacer una vista previa de los r�tulos de bobinas descriptos anteriormente. Sin tener que imprimirlos.
Requerimentos no Funcionales
Hardware
- Para el cliente en la parte de administrador har�n falta dos computadoras con Windows por lo menos. Una para el empleado administrador de la planta Baradero y otra la de Caseros.
- Para el cliente en la parte de ingreso de rotulo de bobina har� falta otra computadora mas con Windows para que los obreros puedan ingresar las bobinas.
- Se necesitara una impresora laser para la impresi�n de los r�tulos de Baradero.
- Se necesitara un adaptador RS232 a USB para la balanza de Baradero.
- Se necesitaran 2 celulares con buena resoluci�n para el escaneo de las bobinas. Uno para la planta de Baradero y otro de Caseros. Preferentemente que tengan linternas incorporadas ya que las plantas no son muy luminosas
