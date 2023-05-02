# AtencionClinica

Recordar el CustomerCliente 1 como cliente de contado


El servicio Baar debe ser el # 8

Antes de publucar
-Cambiar la ruta de los reportes en el reportService.js
-Cambiar cadena de conexion en el appsetting


[x] - Poder anular una orden de trabajo
[x] - El precio de venta se ponga automáticamente el 30 porciento


Una de las consultas que me esta pidiendo es un reporte donde puedo imprimir todas las recetas digitales por paciente por receta y por fecha

Lo que más me urge es la opción de compra con el usuario de farmacia y que el precio de venta se ponga automáticamente el 30 porciento y lo otro es poder anular una receta mal dictada esa opción esta si entra en servicio asegurado pero en la opción de admisión de hoy no

Quiero saber si tiene los cambios 1.  compra de farmacia
2. Anular receta mal elaborada 
3. Cuando pongamos el precio de compra se ponga automáticamente del precio de venta

[7:30 p. m., 8/2/2022] Mario Chinande: Ingeniero quiero saber cuando podemos tener consulta y reportes
[7:31 p. m., 8/2/2022] Mario Chinande: Una de las consulta que me están pidiendo y es ver las recetas despachadas y ver así cuál no hemos digitando y digitarla
[7:32 p. m., 8/2/2022] Mario Chinande: La consulta que me dejaste para realizar los ingresos no está funcionando para un producto no actualiza


Dos puntos importantes son 
1. Que pueda comprar medicamento del área de farmacia 
2. En los traslados y requisa no me deja guardar pq sale etapa 
3. A la hora de comprar el precio de venta me ponga por defecto el 30 porciento delnprecio de costonpero que se pueda editar
4. Cuando dijite una orden y me equivoqué o la dijite 2 veces poder modificarla



https://learn.microsoft.com/en-us/visualstudio/javascript/tutorial-nodejs-with-react-and-jsx?view=vs-2022

https://learn.microsoft.com/en-us/visualstudio/javascript/tutorial-create-react-app?view=vs-2022

https://medium.com/@woeterman_94/quickfix-npm-err-gyp-verb-check-python-checking-for-python-executable-python2-in-the-path-70aacdd9d7d3

https://stackoverflow.com/questions/74522956/python-is-not-a-valid-npm-option

set NODE_OPTIONS=--openssl-legacy-provider


19266925

Admin
15111983

1. Agregar el nombre de cliente en el reporte de detalle de factura
2. En reporte de inventario -> Descargue. El precio unitario que sale que sea el de costo no el de venta.
3. En el área de resultados de exámenes no me permite crear resultados en privados y convenio y ademas corregir la suma de los productos que se usan
	NOTA: Para entrar al menu tenes que ser usuario del area de laboratorio. Una vez que entres ahi -> Menu Servicios Asegurados
4. Crear impresión de voucher ademas de la de tamaño carta en los exámenes.
	NOTA: Menu Servicios Privados -> Convenio





Eliminar ingresos de clientes privados:
delete from PrivateSendTestDetails
delete from PrivateServiceTestBaarDetails
delete from PrivateServiceTestDetails
delete from PrivateServiceTests
delete from PrivateSendTests
delete from BillDetails
delete from PrivateWorkOrderDetails
delete from PrivateWorkOrders;
delete from PrivateWorkPreOrderDetails
delete from PrivateWorkPreOrders
delete from FollowsPrivates
delete from Bills