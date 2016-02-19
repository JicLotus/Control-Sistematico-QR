======================
Configuración
======================

************************************
Archivo de configuración (config.ini)
************************************

Es el archivo de configuración del sistema, es el que permite mantener la persistencia de las diferentes opciones del mismo. Si el archivo no existe se crea un archivo default en el disco C.

Por ejemplo:

IP=127.0.0.1:Localhost
IP2=asd:asd
PUERTO=3306
PATH1=C:/
PATH2=C:/
MODO=0
MYSQLTIMEOUT=1000
FILTROFECHA=0
NROBOBINA=2618
FILTROESTADO=1
IDEESTADO=17
FILTROMAQUINISTA=0
IDMAQUINISTA=1
FILTROTIPOPAPEL=0
IDTIPOPAPEL=-1
FILTROCLIENTE=0
IDCLIENTE=2
CAMPOFECHA=1
PORCENTAJEREMITO=100


********************************
Servidores
********************************

Solamente se puede conectar a dos servidores. Los campos a modificar los existentes son los de IP e IP2.

Para modificar simplemente se debe poner direccionDelServidor:Etiqueta, por ejemplo:

IP=127.0.0.1:Localhost
IP2=192.168.1.100:Servidor Lan


********************************
Puerto
********************************

Es el puerto al cual responde la base de datos MYSQL. Normalmente el puerto suele ser 3306, el cual esta como default.

********************************
Directorios de copia
********************************

Son los directorios que se debe seleccionar para que la información de back Up se copia. En esos path se copiaran las bobinas que no se han grabado en el servidor, en caso de haber fallos de conexión.

También se almacenaran los Productos, Maquinistas , Clientes y Cantidad de Bobinas que se haya registrado en la base de datos por última vez. Esto hace que en fallos de conexión se puedan consultar dichos archivos.

Por ejemplo:

PATH1=C:/
PATH2=D:/

********************************
Mysql Time Out
********************************

Es el tiempo de espera a una conexión de la base de datos. Es bastante útil en el caso de conexiones con bastante delay y que las respuestas del servidor no son tan espontaneas

Por ejemplo:

MYSQLTIMEOUT=1

Está en milisegundos, en el ejemplo seria 1 milisegundo de espera.

********************************
Modos de apertura
********************************

Son los diferentes modos el cual puede abrir la aplicación.

Para abrir la aplicacion directamente como el menú del administrador:

MODO=0 

Para abrir la aplicación desde el menú inicial, permite la selección del servidor al cual quiere conectarse:

MODO=1

.. image:: /Imagenes/Programa/Menu/1.png


Para abrir la aplicación directamente desde el formulario del operador:

MODO=2

********************************
Filtros
********************************

Son índices de los filtros que se hayan aplicado por última vez. Esto permite la persistencia de los últimos filtros aplicados en el programa y que cada vez que se abra la aplicación se mantengan dichos filtros.
