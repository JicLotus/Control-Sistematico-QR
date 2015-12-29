# Registro y control sistem�tico de stock de bobinas de papel

#### Objetivo
El proyecto surge con el objetivo de resolver la problem�tica del control de bobinas de una papelera recicladora, CelulosaBaradero S.A. El proyecto se desarrollar� a medida de la empresa


#### Introducci�n:
La papelera de reciclaje cuenta con tres plantas recicladoras:

1. La primer planta recicladora se encuentra situada en Caseros y ser� la que recibir� la materia prima a reciclar. La misma se encargar� de compactarla y luego de enviarla a la siguiente planta.

2. Esta planta se encuentra situada en Baradero y la misma se encarga de procesar la materia prima para sacar las bobinas de papel. Luego esas bobinas podr�n quedarse como stock en esta planta, ser vendidas directamente a un cliente o ser despachadas a la tercer planta recicladora para que queden como stock all�.
Proyecto de Trabajo Profesional

3. Esta planta tambi�n se ubica en Caseros y recibe bobinas de papel proveniente de la planta de Baradero. Las mismas pueden quedar como stock y luego ser vendidas directamente a un cliente o tambi�n desarrollar papel higi�nico.
Nuestro sistema a plantear se situar� en las plantas 2 y 3. Primeramente se pondr�n 2 servidores en paralelo. Uno se lo instalara en la planta 2 y el otro en la 3 ya que los registros de la planta 2 son independientes de la planta 3. Esta forma de almacenar los datos se planteo por el hecho de que la planta de Baradero sufre frecuentes cortes de internet y si no se hiciera de esta manera seria imposible el trabajo continuo.
En la planta 2 se ubicar� el Sistema de formulario para obreros el cual es el principal y �nico ingreso de bobinas al sistema. La forma que se ingresaran las bobinas a la planta 3 ser� mediante la Aplicaci�n de celular para el escaneo y control de las bobinas.
Para el control de las bases de datos se armar� un Sistema administrador para corroborar el stock de bobinas el cual estar� instalado tanto en la planta 2 como en la 3. Desde el mismo se puede acceder a cualquier base de datos de los 2 servidores existentes.


#### Software

###### Cliente:

El cliente ser� Desktop y se desarrollara para el SO Windows.

###### Servidor:

Sera un servidor web, Apache utilizando MYSQL para el manejo de base de datos. Puede ser ejecutado desde Linux o Windows. Preferentemente y por cuestiones optimas, el servidor es mejor que este corriendo bajo un SO de Linux. Se utilizara Ubuntu Server como SO ya que es gratuito y con buen funcionamiento.


###### Aplicaci�n celular:

Sera desarrollado para el SO Android.


#### Documentacion

La documentacion esta desarrollada en Sphinx

#### Test




## Instalacion