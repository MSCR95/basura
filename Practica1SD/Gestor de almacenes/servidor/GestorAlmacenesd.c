#include "GestorAlmacenes.h"
#include <string.h>


struct almacen {
	int nClnt;
	TDatosAlmacen propiedades;
	int nProds;
	TProducto **prods;	//el tamano de productos siempre coincidira con el num de productos
};

									
int nAlmacenes = 0;					//num almacenes
struct almacen **vAlmacenes = NULL;	//El vector dinamico para los almacenes aumenta de 10 en 10 de cada vez que se queda sin espacios ocupados

static bool_t falso = 0;
static bool_t verdadero = 1;

TDatosAlmacen * datosalmacen_1_svc(int *argp, struct svc_req *rqstp)	{

	static TDatosAlmacen result;

	//respuesta vacia por si no encuentra nada
	strcpy(result.Nombre, "");
	strcpy(result.Direccion, "");
	strcpy(result.Fichero, "");

	//consulta si es un indice valido
	if (*argp < nAlmacenes)						//si es un indice existente
		if (vAlmacenes[*argp] != NULL)			//si el almacen esta abierto
			result = vAlmacenes[*argp]->propiedades;

	return &result;
}

int * nproductos_1_svc(int *argp, struct svc_req *rqstp)				{

	static int result;

	result = -1;

	//consulta si es un indice valido
	if (*argp < nAlmacenes)						//si es un indice existente
		if (vAlmacenes[*argp] != NULL)			//si el almacen esta abierto
			result = vAlmacenes[*argp]->nProds;

	return &result;
}

int * crearalmacen_1_svc(TDatosAlmacen *argp, struct svc_req *rqstp)	{

	static int  result;

	result = -1;

	//busca si ya existe un almacen en memoria con el mismo fichero
	int i = 0;;
	while (i < nAlmacenes && result == -1) {
		if (vAlmacenes[i] != NULL)	{	//comprobar que esta cargado/abierto
			if (strcmp(vAlmacenes[i]->propiedades.Fichero, argp->Fichero) == 0)	//si los ficheros coinciden y esta activo
				result = i;
			else i++;
		}
	}

	if (result == -1)	{	//si no se encuentra en memoria, cargar...

		//Crea un fichero nuevo
		FILE *arch = fopen(argp->Fichero, "wb");

		if (arch == NULL) return &result;	//hay error y no se puede crear (return -1)

		int cero = 0;	//variable para almacenar la constante cero, no se pueden escribir constantes con fwrite
		fwrite(&cero, sizeof(int), 1, arch);
		fwrite(argp->Nombre, sizeof(Cadena), 1, arch);
		fwrite(argp->Direccion, sizeof(Cadena), 1, arch);
		fclose(arch);

		//Cargar en memoria

		//control preventivo de tamano del vector de almacenes si esta vacio
		if (nAlmacenes == 0)	{	//si el vector esta vacio
			vAlmacenes = malloc(sizeof(struct almacen *));
			vAlmacenes[0] = NULL;
			nAlmacenes++;
		}

		//busca el primer almacen en memoria disponible y lo almacena en i
		i = 0;
		while (i < nAlmacenes && vAlmacenes[i] != NULL) i++;
		result = i;

		if (result == nAlmacenes)	{	//si no ha encontrado hueco, extender el vector para hacer hueco
			vAlmacenes = realloc(vAlmacenes, sizeof(struct almacen *) * (nAlmacenes++));
		}

		//una vez encontrado el hueco, rellenar...
		vAlmacenes[result] = malloc(sizeof(struct almacen));
		vAlmacenes[result]->nClnt = 1;
		vAlmacenes[result]->propiedades = *argp;
		vAlmacenes[result]->nProds = 0;
		vAlmacenes[result]->prods = NULL;

	} else vAlmacenes[result]->nClnt++;

	return &result;
}

int * abriralmacen_1_svc(char *argp, struct svc_req *rqstp) 			{

	static int  result;

	result = -1;

	//busca si ya existe un almacen en memoria con el mismo fichero
	int i = 0;

	while (i < nAlmacenes && result == -1) {
		if (vAlmacenes[i] != NULL)	{	//comprobar que esta cargado/abierto
			if (strcmp(vAlmacenes[i]->propiedades.Fichero, argp) == 0)	//si los ficheros coinciden y esta activo
				result = i;
			else i++;
		}
	}

	if (result == -1)	{	//si no se encuentra en memoria, cargar...

		//Crea un fichero nuevo
		FILE *arch = fopen(argp, "rb");

		if (arch == NULL) return &result;	//hay error y no se puede cargar (return -1)

		//Cargar en memoria

		//control preventivo de tamano del vector de almacenes si esta vacio
		if (nAlmacenes == 0)	{	//si el vector esta vacio
			vAlmacenes = malloc(sizeof(struct almacen *));
			vAlmacenes[0] = NULL;
			nAlmacenes++;
		}

		//busca el primer almacen en memoria disponible y lo almacena en i
		i = 0;
		while (i < nAlmacenes && vAlmacenes[i] != NULL) i++;
		result = i;

		if (result == nAlmacenes)	{	//si no ha encontrado hueco, extender el vector para hacer hueco
			vAlmacenes = realloc(vAlmacenes, sizeof(struct almacen *) * (nAlmacenes++));
		}

		//una vez encontrado el hueco, rellenar...
		vAlmacenes[result] = malloc(sizeof(struct almacen));

		vAlmacenes[result]->nClnt = 1;
		fread(&vAlmacenes[result]->nProds, sizeof(int), 1, arch);
		fread(vAlmacenes[result]->propiedades.Nombre, sizeof(Cadena), 1, arch);
		fread(vAlmacenes[result]->propiedades.Direccion, sizeof(Cadena), 1, arch);
		strcpy(vAlmacenes[result]->propiedades.Fichero, argp);

		//carga de productos
		if (vAlmacenes[result]->nProds > 0)	{	//si hay productos que cargar...
			vAlmacenes[result]->prods = malloc(sizeof(struct TProducto *) * vAlmacenes[result]->nProds);
			for(i = 0; i < vAlmacenes[result]->nProds; i++)	{
				vAlmacenes[result]->prods[i] = malloc(sizeof(struct TProducto));
				fread(vAlmacenes[result]->prods[i], sizeof(struct TProducto), 1, arch);
			}
		} else vAlmacenes[result]->prods = NULL;

		fclose(arch);

	} else vAlmacenes[result]->nClnt++;

	return &result;
}

bool_t * guardaralmacen_1_svc(int *argp, struct svc_req *rqstp) 		{

	//comprueba que el almacen es valido (activo)
	if (*argp > nAlmacenes)
		return &falso;
	else if (vAlmacenes[*argp] == NULL)
		return &falso;

	FILE *arch = fopen(vAlmacenes[*argp]->propiedades.Fichero, "wb");
	if (arch == NULL) return &falso;	//si hay error y no se puede crear

	fwrite(&vAlmacenes[*argp]->nProds, sizeof(int), 1, arch);
	fwrite(vAlmacenes[*argp]->propiedades.Nombre, sizeof(Cadena), 1, arch);
	fwrite(vAlmacenes[*argp]->propiedades.Direccion, sizeof(Cadena), 1, arch);

	for(int i = 0; i < vAlmacenes[*argp]->nProds; i++)
		fwrite(vAlmacenes[*argp]->prods[i], sizeof(struct TProducto), 1, arch);

	fclose(arch);
	return &verdadero;
}

bool_t * cerraralmacen_1_svc(int *argp, struct svc_req *rqstp)			{

	//igual que guardar
	//comprueba que el almacen es valido (activo)
	if (*argp > nAlmacenes)
		return &falso;
	else if (vAlmacenes[*argp] == NULL)
		return &falso;

	FILE *arch = fopen(vAlmacenes[*argp]->propiedades.Fichero, "wb");
	if (arch == NULL) return &falso;	//si hay error y no se puede crear

	fwrite(&vAlmacenes[*argp]->nProds, sizeof(int), 1, arch);
	fwrite(vAlmacenes[*argp]->propiedades.Nombre, sizeof(Cadena), 1, arch);
	fwrite(vAlmacenes[*argp]->propiedades.Direccion, sizeof(Cadena), 1, arch);

	for(int i = 0; i < vAlmacenes[*argp]->nProds; i++)
		fwrite(vAlmacenes[*argp]->prods[i], sizeof(struct TProducto), 1, arch);

	fclose(arch);

	//parte extra de cerrar
	vAlmacenes[*argp]->nClnt--;

	if (vAlmacenes[*argp]->nClnt <= 0)	{	//si es el ultimo, libera memoria
		if (vAlmacenes[*argp]->prods != NULL)
			free(vAlmacenes[*argp]->prods);
		free(vAlmacenes[*argp]);
		vAlmacenes[*argp] = NULL;
	}

	return &verdadero;
}

bool_t * almacenabierto_1_svc(int *argp, struct svc_req *rqstp)			{

	static bool_t result;

	result = falso;	//por defecto, falso

	if (*argp < nAlmacenes)						//asegurarse primero que esta dentro de los limites del vector
		result = (vAlmacenes[*argp] != NULL);	//comprueba que esta activo

	return &result;
}

int * buscaproducto_1_svc(TBusProd *argp, struct svc_req *rqstp)		{

	static int result;

	result = -1;

	//consulta si es un indice valido
	if (argp->Almacen < nAlmacenes)	{					//si es un indice existente
		if (vAlmacenes[argp->Almacen] != NULL)	{		//si el indice esta cargado en memoria
			if (vAlmacenes[argp->Almacen]->nProds > 0) {//si tiene productos
				int i = 0;
				do {
					if (strcmp(vAlmacenes[argp->Almacen]->prods[i]->CodProd, argp->CodProducto) == 0)	//si los codigos coinciden
						result = i;
					else i++;
				} while (i < vAlmacenes[argp->Almacen]->nProds && result == -1);
			}
		}
	}
	return &result;
}

TProducto * obtenerproducto_1_svc(TObtProd *argp, struct svc_req *rqstp){

	static TProducto result;

	//por defecto producto invalido
	strcpy(result.CodProd, "!ERR");
	strcpy(result.NombreProd, "!ERR");
	strcpy(result.Descripcion, "!ERR");
	result.Cantidad = -1;
	result.Caducicidad = (TFecha){0, 0, 0};
	result.Precio = -1;

	if (argp->Almacen < nAlmacenes)	{										//si es un indice de almacen existente
		if (vAlmacenes[argp->Almacen] != NULL)	{							//si esta cargado en memoria
			if (vAlmacenes[argp->Almacen]->nProds > argp->PosProducto)	{	//si el producto esta en una posicion valida
				result = *(vAlmacenes[argp->Almacen]->prods[argp->PosProducto]);
			}
		}
	}
	return &result;
}

bool_t * anadirproducto_1_svc(TActProd *argp, struct svc_req *rqstp)	{

	//comprueba que el almacen es valido
	if (argp->Almacen > nAlmacenes)
		return &falso;
	else if (vAlmacenes[argp->Almacen] == NULL)
		return &falso;

	//comprueba que no hay otro producto con el mismo codigo
	if (vAlmacenes[argp->Almacen]->nProds > 0)	{	//si tiene productos
		for (int i = 0; i < vAlmacenes[argp->Almacen]->nProds; i++)
			if (strcmp(vAlmacenes[argp->Almacen]->prods[i]->CodProd, argp->Producto.CodProd) == 0)	//si los codigos coinciden
				return &falso;	//aborta
	}

	//aumentar el vector de productos
	if (vAlmacenes[argp->Almacen]->prods == NULL)	{	//si vacio
		vAlmacenes[argp->Almacen]->prods = malloc(sizeof(struct TProducto *));
	} else {											//si ocupado
		vAlmacenes[argp->Almacen]->prods = realloc(vAlmacenes[argp->Almacen]->prods, sizeof(struct TProducto *) * (vAlmacenes[argp->Almacen]->nProds + 1));
	}

	//anade el nuevo producto al final del vector
	vAlmacenes[argp->Almacen]->prods[vAlmacenes[argp->Almacen]->nProds] = malloc(sizeof(struct TProducto));
	*(vAlmacenes[argp->Almacen]->prods[vAlmacenes[argp->Almacen]->nProds]) = argp->Producto;

	vAlmacenes[argp->Almacen]->nProds++;
	return &verdadero;
}

bool_t * actualizarproducto_1_svc(TActProd *argp, struct svc_req *rqstp){

	//comprueba que el almacen es valido
	if (argp->Almacen > nAlmacenes)
		return &falso;
	else if (vAlmacenes[argp->Almacen] == NULL)
		return &falso;

	//comprueba que existe un producto con el mismo codigo
	if (vAlmacenes[argp->Almacen]->nProds > 0)	{	//si tiene productos
		for (int i = 0; i < vAlmacenes[argp->Almacen]->nProds; i++)	{
			if (strcmp(vAlmacenes[argp->Almacen]->prods[i]->CodProd, argp->Producto.CodProd) == 0)	{	//si los codigos coinciden
				*(vAlmacenes[argp->Almacen]->prods[i]) = argp->Producto;
				return &verdadero;
			}
		}
	}
	//si ha llegado hasta aqui es porque no ha encontrado ningun producto
	return &falso;
}

bool_t * eliminarproducto_1_svc(TBusProd *argp, struct svc_req *rqstp)	{

	//comprueba que el almacen es valido
	if (argp->Almacen > nAlmacenes)
		return &falso;
	else if (vAlmacenes[argp->Almacen] == NULL)
		return &falso;

	//comprueba que existe un producto con el mismo codigo
	if (vAlmacenes[argp->Almacen]->nProds > 0)	{	//si tiene productos, buscalo
		for (int i = 0; i < vAlmacenes[argp->Almacen]->nProds; i++)	{
			if (strcmp(vAlmacenes[argp->Almacen]->prods[i]->CodProd, argp->CodProducto) == 0)	{	//si los codigos coinciden
				//libera la memoria del producto a eliminar
				free(vAlmacenes[argp->Almacen]->prods[i]);
				//arrastra a los siguientes productos 1 posicion a la izquierda
				for(int j = i; i < vAlmacenes[argp->Almacen]->nProds - 1; i++)
					vAlmacenes[argp->Almacen]->prods[j] = vAlmacenes[argp->Almacen]->prods[j+1];

				//encoge el vector de productos
				vAlmacenes[argp->Almacen]->nProds--;
				vAlmacenes[argp->Almacen]->prods = realloc(vAlmacenes[argp->Almacen]->prods, sizeof(struct TProducto) * vAlmacenes[argp->Almacen]->nProds);

				return &verdadero;
			}
		}
	}
	//si ha llegado hasta aqui es porque no ha encontrado el producto
	return &falso;
}
