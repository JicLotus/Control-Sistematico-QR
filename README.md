# Registro y control sistemático de stock de bobinas de papel

#### Objetivo
El proyecto surge con el objetivo de resolver la problemática del control de bobinas de una papelera recicladora, CelulosaBaradero S.A. El proyecto se desarrollará a medida de la empresa


#### Introducción:
La papelera de reciclaje cuenta con tres plantas recicladoras:

1. La primer planta recicladora se encuentra situada en Caseros y será la que recibirá la materia prima a reciclar. La misma se encargará de compactarla y luego de enviarla a la siguiente planta.

2. Esta planta se encuentra situada en Baradero y la misma se encarga de procesar la materia prima para sacar las bobinas de papel. Luego esas bobinas podrán quedarse como stock en esta planta, ser vendidas directamente a un cliente o ser despachadas a la tercer planta recicladora para que queden como stock allí.
Proyecto de Trabajo Profesional

3. Esta planta también se ubica en Caseros y recibe bobinas de papel proveniente de la planta de Baradero. Las mismas pueden quedar como stock y luego ser vendidas directamente a un cliente o también desarrollar papel higiénico.
Nuestro sistema a plantear se situará en las plantas 2 y 3. Primeramente se pondrán 2 servidores en paralelo. Uno se lo instalara en la planta 2 y el otro en la 3 ya que los registros de la planta 2 son independientes de la planta 3. Esta forma de almacenar los datos se planteo por el hecho de que la planta de Baradero sufre frecuentes cortes de internet y si no se hiciera de esta manera seria imposible el trabajo continuo.
En la planta 2 se ubicará el Sistema de formulario para obreros el cual es el principal y único ingreso de bobinas al sistema. La forma que se ingresaran las bobinas a la planta 3 será mediante la Aplicación de celular para el escaneo y control de las bobinas.
Para el control de las bases de datos se armará un Sistema administrador para corroborar el stock de bobinas el cual estará instalado tanto en la planta 2 como en la 3. Desde el mismo se puede acceder a cualquier base de datos de los 2 servidores existentes.


#### Software

###### Cliente:

El cliente será Desktop y se desarrollara para el SO Windows.

###### Servidor:

Sera un servidor web, Apache utilizando MYSQL para el manejo de base de datos. Puede ser ejecutado desde Linux o Windows. Preferentemente y por cuestiones optimas, el servidor es mejor que este corriendo bajo un SO de Linux. Se utilizara Ubuntu Server como SO ya que es gratuito y con buen funcionamiento.


###### Aplicación celular:

Sera desarrollado para el SO Android.


#### Documentacion

La documentacion esta desarrollada en Sphinx

#### Test




## Instalacion