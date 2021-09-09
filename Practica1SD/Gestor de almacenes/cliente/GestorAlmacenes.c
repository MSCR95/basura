#include "GestorAlmacenes.h"
#include <stdlib.h>


//funcion para capturar por teclado cadenas complejas/con espacios
bool_t scanCadena(Cadena ret)	{
	fgets(ret, sizeof(Cadena)-1, stdin);
	//eliminar salto de linea, si se capturo
	if ((strlen(ret) > 0) && (ret[strlen(ret)-1] == '\n'))
		ret[strlen(ret)-1] = '\0';
	return (strlen(ret) > 0);
}

int main (int argc, char *argv[])   {

	char *host;
	TDatosAlmacen almAct;
	strcpy(almAct.Nombre, "");
	strcpy(almAct.Direccion, "");
	strcpy(almAct.Fichero, "");
	int indiceAlm = -1;
	char opc;

	if (argc < 2) {
		host = strdup("localhost");
	}
	else{
		host = argv[1];
	}

	printf("Conectando con %s...\n", host);
	
	CLIENT *clnt = clnt_create(host, SUPERMERCADO, SUPERMERCADO_VER, "tcp");

	if (clnt == NULL) {
		clnt_pcreateerror (host);
		return 1;
	}

	printf("Conectado al gestor de almacenes correctamente!\n");

	do	{
		system("clear");
		printf(" ----- Menu Almacenes ----- %s\n\n", almAct.Nombre);
		printf(	" 1.- Crear un almacen vacio\n"
				" 2.- Abrir un fichero de almacen\n"
				" 3.- Cerrar almacen\n"
				" 4.- Guardar datos\n"
				" 5.- Listar productos del almacen\n"
				" 6.- Anadir un producto\n"
				" 7.- Actualizar un producto\n"
				" 8.- Consultar un producto\n"
				" 9.- Eliminar un producto\n"
				" 0.- Salir\n"
				"\n Opcion: ");
		scanf("%c", &opc);
		getchar();	
		system("clear");
		switch(opc)	{
				case '0':	//Salir
					break;
				case '1':	//Crear un almacen vacio
				{	printf("----- Crear un almacen vacio -----\n\n");

					if (indiceAlm != -1)	{	//no hay almacen
						if (cerraralmacen_1(&indiceAlm, clnt) == (bool_t *)NULL)
							clnt_perror(clnt, "call failed");
						else	{
							indiceAlm = -1;
							strcpy(almAct.Nombre, "");
						}
					}

					printf("Intruduce nombre: ");
					scanCadena(almAct.Nombre);
					printf("Introduce direccion: ");
					scanCadena(almAct.Direccion);
					printf("Introduce nombre del fichero: ");
					scanCadena(almAct.Fichero);

					int *pt = crearalmacen_1(&almAct, clnt);
					indiceAlm = *pt;


					if (indiceAlm == -1)
						printf("Error No se ha podido crear el almacen.\n");
					else	{
						//Asegurarse de que el almacen concuerda, puede haber invocado un fichero distinto
						almAct = *datosalmacen_1(&indiceAlm, clnt);
						printf("Almacen abierto correctamente.\n");
					}
				} break;

				case '2':	//Abrir un fichero de almacen
				{	printf("----- Abrir un fichero de almacen -----\n\n");

					if (indiceAlm != -1)	{	
						if (cerraralmacen_1(&indiceAlm, clnt) == (bool_t *)NULL)	{	//si no se puede cerrar
							clnt_perror(clnt, "call failed");
							printf("Error No se ha podido cerrar el almacen, abortando...\n");
						} else {	
							indiceAlm = -1;
							strcpy(almAct.Nombre, "");

							Cadena fich;

							printf("Introduce nombre del fichero: ");
							scanCadena(fich);

							int *pt = abriralmacen_1(fich, clnt);
							indiceAlm = *pt;
							if (indiceAlm == -1)
								printf("Error No se ha podido crear el almacen.\n");
							else	{	//obtiene nombre del almacen
								almAct = *datosalmacen_1(&indiceAlm, clnt);
								printf("Almacen abierto correctamente.\n");
							}
						}
					} else {	//si no hace falta cerrar

						Cadena fich;

						printf("Introduce nombre del fichero: ");
						scanCadena(fich);

						int *pt = abriralmacen_1(fich, clnt);
						indiceAlm = *pt;
						if (indiceAlm == -1)
							printf("Error No se ha podido crear el almacen.\n");
						else	{	
							almAct = *datosalmacen_1(&indiceAlm, clnt);
							printf("Almacen abierto correctamente.\n");
						}
					}
				} break;

				case '3':	//Cerrar almacen
				{	printf("----- Cerrar un almacen -----\n\n");

					if (indiceAlm == -1)	{	
						printf("Error No hay ningun almacen abierto.\n");
					} else {
						bool_t *result;

						result = guardaralmacen_1(&indiceAlm, clnt);

						if (result == (bool_t *) NULL)	{	
							clnt_perror(clnt, "call failed");
							printf("Error No se han podido guardar los cambios, vuelva a intentarlo mas tarde.\n");
						} else	{
							if (*result == 1)	{	//si se ha guardado correctamente
								result = cerraralmacen_1(&indiceAlm, clnt);
								if (result == (bool_t *) NULL)	{
									clnt_perror(clnt, "call failed");
									printf("Error No se ha podido cerrar, pero los cambios han sido guardados.\n");
								} else {
									if (*result == 1)	{	//si se ha cerrado correctamente
										strcpy(almAct.Nombre, "");
										indiceAlm = -1;
										printf("Almacen cerrado con exito.\n");
									} else {	
										printf("Error No se ha podido cerrar, pero los cambios han sido guardados.\n");
									}
								}
							} else {	
								printf("Error No se han podido guardar los cambios, vuelva a intentarlo mas tarde.\n");
							}
						}
					}
				} break;

				case '4':	//Guardae datos
				{	printf("----- Guardar datos -----\n\n");

					if (indiceAlm == -1)	{	
						printf("Error No hay ningun almacen abierto.\n");
					} else {
						bool_t *result;

						result = guardaralmacen_1(&indiceAlm, clnt);

						if (result == (bool_t *) NULL)	{	
							clnt_perror(clnt, "call failed");
							printf("Error No se han podido guardar los datos, vuelva a intentarlo mas tarde\n");
						} else {
							if (*result == 1)
								printf("Se han guardado los datos con exito.\n");
							else
								printf("Error No se han podido guardar los datos, vuelva a intentarlo mas tarde\n");
						}
					}
				} break;

				case '5':	// Listar productos del almacen
				{	printf("----- Listar productos del almacen -----\n\n");

					if (indiceAlm == -1)	{	
						printf("Error No hay ningun almacen abierto.\n");
					} else {
						int *nProd = nproductos_1(&indiceAlm, clnt);
						if (nProd == (int *) NULL)	{	
							clnt_perror(clnt, "call failed");
							printf("Error No se ha podido obtener la informacion, vuelva a intentarlo mas tarde.\n");
						} else {	
							if (*nProd == -1)		//si invalido
								printf("Error No se ha podido obtener la informacion, vuelva a intentarlo mas tarde.\n");
							else if (*nProd == 0)	//si vacio
								printf("El almacen esta vacio.\n");
							else {
								//usando formato estandar de 80 columnas
								printf("Listado del almacen \"%s\" localizado en %s\n", almAct.Nombre, almAct.Direccion);
								printf("CODIGO|NOMBRE                        |PRECIO  |CANTIDAD    |FECHA Caducidad\n");

								TObtProd indiceProd;
								indiceProd.Almacen = indiceAlm;
								TProducto *producto;

								for(int i = 0; i < *nProd; i++)	{
									indiceProd.PosProducto = i;
									producto = obtenerproducto_1(&indiceProd, clnt);
									printf("%-6.6s|%-30.30s|%-8.2f|%-12d|%d/%d/%d\n", producto->CodProd, producto->NombreProd, producto->Precio, producto->Cantidad, producto->Caducicidad.Dia, producto->Caducicidad.Mes, producto->Caducicidad.Anyo);
								}
							}
						}
					}
				} break;

				case '6':	//Añadir un producto
				{	printf("----- Anadir un producto -----\n\n");

					if (indiceAlm == -1)	{	
						printf("Error No hay ningun almacen abierto.\n");
					} else {
						printf("-- Datos del producto --\n");
						printf("\tCodigo: ");

						TBusProd consulta;
						consulta.Almacen = indiceAlm;
						scanCadena(consulta.CodProducto);

						int *resultado = buscaproducto_1(&consulta, clnt);
						if (resultado == (int *) NULL)	{
							clnt_perror(clnt, "call failed");
							printf("Error No se ha podido verificar la existencia, vuelva a intentarlo mas tarde.\n");
						} else if (*resultado != -1)	{
							printf("Error Ya existe un articulo con el mismo codigo.\n");
						} else {	//no existe, sigue pidiendo datos...
							TActProd pro;
							pro.Almacen = indiceAlm;
							strcpy(pro.Producto.CodProd, consulta.CodProducto);
							printf("\tNombre: ");
							scanCadena(pro.Producto.NombreProd);
							printf("\tPrecio: ");
							scanf("%f", &pro.Producto.Precio);
							printf("\tCantidad: ");
							scanf("%d", &pro.Producto.Cantidad);
							printf("\tFecha de Caducidad (dd/mm/aa): ");
							scanf("%d/%d/%d", &pro.Producto.Caducicidad.Dia,&pro.Producto.Caducicidad.Mes,&pro.Producto.Caducicidad.Anyo);
							getchar();	//limpia el \n que se deja scanf en la entrada
							printf("\tBreve descripcion: ");
							scanCadena(pro.Producto.Descripcion);

							bool_t *res = anadirproducto_1(&pro, clnt);
							if (res == (bool_t *) NULL)	{
								clnt_perror(clnt, "call failed");
								printf("Error No se ha podido anadir correctamente, vuelva a intentarlo mas tarde.\n");
							} else if (*res == 1) {
								printf("Producto anadido correctamente\n");
							} else {
								printf("No se ha podido anadir el producto\n");
							}
						}
					}
				} break;

				case '7':	//Actualizar un producto
				{	printf("----- Actualizar un producto -----\n\n");

					if (indiceAlm == -1)	{	//si no hay almacen con el que trabajar...
						printf("Error No hay ningun almacen abierto.\n");
					} else {
						printf("-- Datos del producto --\n");
						printf("\tCodigo: ");

						//Primero se consulta
						TBusProd consulta;
						consulta.Almacen = indiceAlm;
						scanCadena(consulta.CodProducto);

						int *resultado = buscaproducto_1(&consulta, clnt);
						if (resultado == (int *) NULL)	{
							clnt_perror(clnt, "call failed");
							printf("Error No se ha podido verificar la existencia, vuelva a intentarlo mas tarde.\n");
						} else if (*resultado == -1)	{	
							printf("Error No existe ningun articulo con ese codigo.\n");
						} else {	//existe

							TObtProd indiceProd = {indiceAlm, *resultado};

							TProducto *prodOrig = obtenerproducto_1(&indiceProd, clnt);
							TActProd prodAct;
							prodAct.Almacen = indiceAlm;

							strcpy(prodAct.Producto.CodProd, prodOrig->CodProd);

							Cadena buf;	//para capturar la entrada del usuario

							printf("\tNombre [%s](dejar en blanco para no cambiarlo): ", prodOrig->NombreProd);
							if (scanCadena(buf))	{	//si no esta en blanco
								strcpy(prodAct.Producto.NombreProd, buf);
							} else {
								strcpy(prodAct.Producto.NombreProd, prodOrig->NombreProd);
							}
							printf("\tPrecio [%f](dejar en blanco para no cambiarlo): ", prodOrig->Precio);
							if (scanCadena(buf))	{	
								sscanf(buf, "%f", &prodAct.Producto.Precio);
							} else {
								prodAct.Producto.Precio = prodOrig->Precio;
							}
							printf("\tCantidad [%d](dejar en blanco para no cambiarlo): ", prodOrig->Cantidad);
							if (scanCadena(buf))	{	
								sscanf(buf, "%d", &prodAct.Producto.Cantidad);
							} else {
								prodAct.Producto.Cantidad = prodOrig->Cantidad;
							}
							printf("\tFecha de Caducidad [%d/%d/%d](dejar en blanco para no cambiarlo): ", prodOrig->Caducicidad.Dia, prodOrig->Caducicidad.Mes, prodOrig->Caducicidad.Anyo);
							if (scanCadena(buf))	{	
								sscanf(buf, "%d/%d/%d", &prodAct.Producto.Caducicidad.Dia, &prodAct.Producto.Caducicidad.Mes, &prodAct.Producto.Caducicidad.Anyo);
							} else {
								prodAct.Producto.Caducicidad.Dia = prodOrig->Caducicidad.Dia;
								prodAct.Producto.Caducicidad.Mes = prodOrig->Caducicidad.Mes;
								prodAct.Producto.Caducicidad.Anyo = prodOrig->Caducicidad.Anyo;
							}
							printf("\tBreve descripcion [%s](dejar en blanco para no cambiarlo): ", prodOrig->Descripcion);
							if (scanCadena(buf))	{	
								strcpy(prodAct.Producto.Descripcion, buf);
							} else {
								strcpy(prodAct.Producto.Descripcion, prodOrig->Descripcion);
							}


							bool_t *res = actualizarproducto_1(&prodAct, clnt);
							if (res == (bool_t *) NULL)	{
								clnt_perror(clnt, "call failed");
								printf("Error No se ha podido actualizar correctamente, vuelva a intentarlo mas tarde.\n");
							} else if (*res == 1) {
								printf("Producto actualizado correctamente\n");
							} else {
								printf("No se ha podido actualizar el producto\n");
							}
						}
					}
				} break;

				case '8':
				{	printf("----- Consultar un producto -----\n\n");

					if (indiceAlm == -1)	{	//si no hay almacen con el que trabajar...
						printf("Error No hay ningun almacen abierto.\n");
					} else {
						printf("\tCodigo a buscar: ");
a
						TBusProd consulta;
						consulta.Almacen = indiceAlm;
						scanCadena(consulta.CodProducto);

						int *resultado = buscaproducto_1(&consulta, clnt);
						if (resultado == (int *) NULL)	{
							clnt_perror(clnt, "call failed");
							printf("Error No se ha podido verificar la existencia, vuelva a intentarlo mas tarde.\n");
						} else if (*resultado == -1)	{	//no encontrado
							printf("No existe ningun articulo con ese codigo.\n");
						} else {
							TObtProd indiceProd = {indiceAlm, *resultado};

							TProducto *prod = obtenerproducto_1(&indiceProd, clnt);

							printf("-- Datos del producto --\n\n");
							printf("\tNombre: %s\n", prod->NombreProd);
							printf("\tPrecio: %f\n", prod->Precio);
							printf("\tCantidad: %d\n", prod->Cantidad);
							printf("\tFecha de Caducidad: %d/%d/%d\n", prod->Caducicidad.Dia, prod->Caducicidad.Mes, prod->Caducicidad.Anyo);
							printf("\tBreve descripcion: %s\n\n", prod->Descripcion);
						}
					}
				} break;

				case '9':
				{	printf("----- Eliminar un producto -----\n\n");

					if (indiceAlm == -1)	{	//si no hay almacen con el que trabajar...
						printf("Error No hay ningun almacen abierto.\n");
					} else {

						TBusProd prod;
						prod.Almacen = indiceAlm;

						printf("\tCodigo a eliminar (!ESTA ACCION ES IRREVERSIBLE!): ");
						scanCadena(prod.CodProducto);

						bool_t *res = eliminarproducto_1(&prod, clnt);
						if (res == (bool_t *) NULL)	{
							clnt_perror(clnt, "call failed");
							printf("Error No se ha podido eliminar, vuelva a intentarlo mas tarde.\n");
						} else if (*res == 1) {
							printf("Producto eliminado con exito.\n");
						} else {
							printf("No se ha podido eliminar, probablemente no exista el codigo introducido.\n");
						}
					}
				} break;

				default:
					printf("Error Valor introducido invalido: %c\n", opc);
					break;
		}
		if (opc != '0')	{
			printf("\nPulsa intro para continuar.\n");
			getchar();
		}
	} while (opc != '0');

	//cerrar almacen al salir!
	if (indiceAlm != -1)
		if (cerraralmacen_1(&indiceAlm, clnt) == (bool_t *)NULL)
			clnt_perror(clnt, "call failed");

	clnt_destroy(clnt);
    exit (0);
}
