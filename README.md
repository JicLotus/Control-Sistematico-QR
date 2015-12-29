# Registro y control sistemático de stock de bobinas de papel

### Objetivo
El proyecto surge con el objetivo de resolver la problemática del control de bobinas de una papelera recicladora, CelulosaBaradero S.A. El proyecto se desarrollará a medida de la empresa

### Requerimientos Funcionales

Introducción:
La papelera de reciclaje cuenta con tres plantas recicladoras:

1. La primer planta recicladora se encuentra situada en Caseros y será la que recibirá la materia prima a reciclar. La misma se encargará de compactarla y luego de enviarla a la siguiente planta.

2. Esta planta se encuentra situada en Baradero y la misma se encarga de procesar la materia prima para sacar las bobinas de papel. Luego esas bobinas podrán quedarse como stock en esta planta, ser vendidas directamente a un cliente o ser despachadas a la tercer planta recicladora para que queden como stock allí.
Proyecto de Trabajo Profesional
2

3. Esta planta también se ubica en Caseros y recibe bobinas de papel proveniente de la planta de Baradero. Las mismas pueden quedar como stock y luego ser vendidas directamente a un cliente o también desarrollar papel higiénico.
Nuestro sistema a plantear se situará en las plantas 2 y 3. Primeramente se pondrán 2 servidores en paralelo. Uno se lo instalara en la planta 2 y el otro en la 3 ya que los registros de la planta 2 son independientes de la planta 3. Esta forma de almacenar los datos se planteo por el hecho de que la planta de Baradero sufre frecuentes cortes de internet y si no se hiciera de esta manera seria imposible el trabajo continuo.
En la planta 2 se ubicará el Sistema de formulario para obreros el cual es el principal y único ingreso de bobinas al sistema. La forma que se ingresaran las bobinas a la planta 3 será mediante la Aplicación de celular para el escaneo y control de las bobinas.
Para el control de las bases de datos se armará un Sistema administrador para corroborar el stock de bobinas el cual estará instalado tanto en la planta 2 como en la 3. Desde el mismo se puede acceder a cualquier base de datos de los 2 servidores existentes.

####Las principales funcionalidades serian:

1. Sistema de formulario para los obreros
El obrero de turno primeramente deberá elegir su nombre para ingresar al formulario. Cuando las bobinas estén listas para ser pesadas, el sistema comienza captando el peso de la balanza y se lo plasmaría en el formulario.
Una vez que el obrero ingrese los datos respectivos de la bobina (Gramaje,Formato,Material,Espesor y Cliente ) que se acaba de pesar, ingresará la bobina al stock y saldrá impreso un rotulo con los datos de dicha bobina, este rotulo se pondrá por debajo del cobertor de la bobina que acaba de salir.
El rotulo impreso además de contener los datos de la bobina respectiva también contendría un código QR que avalaría estos datos y el mismo será útil en la etapa de escaneo con el celular.
La comunicación entre el formulario y la balanza será a través de un puerto RS2
32. Se deberán averiguar los datos en el fabricante de balanza para saber la frecuencia y la secuencia de bytes a recibir para ser captadas correctamente. También algo a tener en cuenta para implementar es el hecho de que la planta en donde se cargan las bobinas se encuentra en Baradero y al ser un lugar aislado de la ciudad sufre
Proyecto de Trabajo Profesional
3
frecuentes cortes de luz y por las largas distancias de la planta suele ser frecuente la caída de señal del router. Por lo tanto para afrontar esta problemática se planteo un modelo en el cual los datos se copien en 2 lugares distintos en la computadora. Una copia se hará en el disco principal y otra en un USB. Para seguir trabajando por más que no haya conexión con la base de datos, se harán copias de las consultas que debían haberse realizado(a las direcciones de disco principal y usb).

2. Aplicación de celular para el escaneo y control de las bobinas
El obrero debe ingresar su usuario en la aplicación. Cuando este ingresado en el sistema aparecerán las siguientes opciones:
a. Cargar códigos al teléfono
ÎõÎé Se cargarían y leerían los códigos QR de los rótulos impresos de la etapa anterior cuando esto se lo requiera. Antes de ser escaneados se le deberá indicar a que cliente será enviada dicha bobina.
b. Historial
ÎõÎé Se tendrán los números y a que clientes irán de las bobinas cargadas en la etapa anterior
c. Analizar código
ÎõÎé Será un modo libre de lectura de código QR en donde se podrá usar QR para saber de antemano cual es el contenido del código QR del rotulo.
d. Ingresar Bobina
ÎõÎé Esta opción será útil en el caso de que el código QR del rotulo este en muy malas condiciones y este ya no pueda ser leído por el lector del celular, por lo tanto debe ser cargado a mano.
e. Descargar códigos leídos
ÎõÎé Una vez que se hayan cargado todas las bobinas, debe asegurarse estar conectado a la red para poder descargar todas las bobinas leídas con el celular y actualizar la base de datos.
ÎõÎé Esta descarga tendrá medidas de seguridad como el hecho de que si en la mitad de la descarga se corta la señal al servidor no se descargaran las bobinas y se deberá comenzar nuevamente con dicha descarga.
Proyecto de Trabajo Profesional
4
ÎõÎé Los códigos no se descargan a medida que se escanea por la problemática descripta en el punto 1). Se deben descargar todos juntos.

3. Sistema administrador para corroborar el stock de bobinas
Al igual que la aplicación del celular el administrador de stock tendrá un loguin en el que pedirá usuario y contraseña. El sistema administrador tendrá las principales categorías:
a. Vista
1. Datos Cargados
ÎõÎé Mediante los filtros seleccionados, en esta vista se mostraran las bobinas que están cargadas en el sistema. Las mismas se mostraran en una grilla de forma paginada, mostrando 50 bobinas por hoja.
ÎõÎé También se mostraran los pesos totales y la cantidad de bobinas dentro de esa selección de filtrado.
ÎõÎé Dependiendo del privilegio del administrador se dejara o no editar/eliminar las bobinas de los registros de la base de datos.
2. Historial Escaneo
ÎõÎé Mostrara los escaneos realizados por el celular. Sera un historial para hacer un seguimiento de la carga de bobinas por parte de los celulares, mostrando la fecha y el lugar en donde se ha enviado a la bobina.
3. Observaciones Generales
ÎõÎé Aquí aparecerán las observaciones generales puestas por los obreros en la carga de los formularios. Por ejemplo: En la maquina principal de Baradero hay una etapa de cambio de cuchillas y los obreros desde el formulario indican si tuvieron o no algún inconveniente y lo dejan registrado.
4. Productos
ÎõÎé Se reflejarán y podrán editar los diferentes tipos de productos y calidad existentes en la empresa. Dependiendo del privilegio del administrador se dejara o no ingresar en esta sección.
5. Maquinistas
Proyecto de Trabajo Profesional
5
ÎõÎé Sera el registro de los Obreros que trabajan en la planta. El mismo servirá a la hora de ingresar el obrero desde la planta. Dependiendo del privilegio del administrador se dejara o no ingresar en esta sección.
6. Clientes
ÎõÎé Se mostraran los clientes que tiene la empresa. Dependiendo del privilegio del administrador se dejara o no ingresar en esta sección.
7. Usuarios
ÎõÎé Se mostraran todos los usuarios que tiene el sistema de administración en si junto a su privilegio y contraseña. Estos usuarios responden tanto al sistema de administración como al sistema de la aplicación del celular. Dependiendo del privilegio del administrador se dejara o no ingresar en esta sección.
b. Filtros
ÎõÎé Se plantearan los diferentes tipos de filtros que deberá tener el sistema para poder buscar una bobina o para poder armar los diferentes tipos de informes. Estos filtros serán reflejados en la grilla de Datos Cargados.
c. Imprimir
1. Remito
ÎõÎé Dependiendo del filtro que se haya hecho se armara un remito para imprimir y archivar. Sirve para tener bien discriminado que cantidad de bobinas, tipo y peso han sido despachadas a los diferentes tipos de clientes por camión.
2. Parte Diario Maquinista
ÎõÎé Con este informe se tendrán los resultados de los rangos horarios, qué cantidad y peso de bobinas fue en lo que estuvieron en lo que trabajando los obreros durante el día. Se podrá saber también las fallas que se produjeron durante el día.
3. Rotulo Bobina
ÎõÎé Se podrán imprimir los rótulos de las bobinas seleccionadas desde el administrador. Llegaría a ser útil en el caso de que llegara a romperse un rotulo de bobina.
Proyecto de Trabajo Profesional
6
d. Vista Previa
1. Remito
ÎõÎé Se puede hacer una vista previa de los Remitos descriptos anteriormente sin tener que imprimirlos.
2. Rotulo Bobina
ÎõÎé Se puede hacer una vista previa de los rótulos de bobinas descriptos anteriormente. Sin tener que imprimirlos.
Requerimentos no Funcionales
Hardware
- Para el cliente en la parte de administrador harán falta dos computadoras con Windows por lo menos. Una para el empleado administrador de la planta Baradero y otra la de Caseros.
- Para el cliente en la parte de ingreso de rotulo de bobina hará falta otra computadora mas con Windows para que los obreros puedan ingresar las bobinas.
- Se necesitara una impresora laser para la impresión de los rótulos de Baradero.
- Se necesitara un adaptador RS232 a USB para la balanza de Baradero.
- Se necesitaran 2 celulares con buena resolución para el escaneo de las bobinas. Uno para la planta de Baradero y otro de Caseros. Preferentemente que tengan linternas incorporadas ya que las plantas no son muy luminosas
