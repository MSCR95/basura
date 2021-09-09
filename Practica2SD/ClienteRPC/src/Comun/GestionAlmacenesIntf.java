/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Comun;

import java.rmi.*;
/**
 *
 * @author maria
 */
public interface GestionAlmacenesIntf extends Remote {
    
/*Dado una posición del almacén, devuelve sus datos */
TDatosAlmacen DatosAlmacen(int pAlmacen) throws RemoteException;

/*Dado una posición de almacén, devuelve el número de productos o -1 si no existe.*/
int NProductos(int pAlmacen) throws RemoteException;

/*Crea un almacén vacío de productos y devuelve la posición donde se encuentra. Si previamente ya
estaba creado y cargado en memoria, devolverá la posición donde se encuentra en memoria. */
int CrearAlmacen(String pNombre, String pDireccion, String pNomFichero) throws RemoteException;

/*Abre un fichero de almacén y lo carga en memoria y devuelve su posición. Si previamente ya estaba
cargado devuelve la posición donde se encuentra. */
int AbrirAlmacen(String pNomFichero) throws RemoteException;

/*Dado la posición de un almacén vuelca sus datos al fichero. Devuelve true si ha podido guardarlo.*/
boolean GuardarAlmacen(int pAlmacen) throws RemoteException;

/*Dado la posición de un almacén, vuelca sus datos al fichero y elimina la memoria dinámica asociada.
Si el Almacén es compartido, solo el último cliente es el que eliminará la memoria dinámica.*/
boolean CerrarAlmacen(int pAlmacen) throws RemoteException;

/* Dado la posición del almacén devuelve true si el almacén está cargado en memoria. */
boolean AlmacenAbierto(int pAlmacen) throws RemoteException;

/*Dado la posición del almacén y un código de producto, devuelve la posición dentro del vector de
dinámico donde se encuentra el producto. Si no lo encuentra devuelve -1. */
int BuscaProducto(int pAlmacen, String pCodProducto) throws RemoteException;

/*Dado la posición del almacén y la del producto, lo devuelve.*/
TProducto ObtenerProducto(int pAlmacen, int pPosProducto) throws RemoteException;

/*Dado la posición de un almacén y un producto lo añadirá a la memoria dinámica del almacén y devuelve
true. No puede existir dos productos con el mismo código en el almacén. */
boolean AnadirProducto(int pAlmacen, TProducto pProdNuevo) throws RemoteException;

/*Dado la posición de un almacén y un producto lo actualizará en la memoria dinámica del almacén. */
boolean ActualizarProducto(int pAlmacen, TProducto pProducto) throws RemoteException;

/*Dado la posición del almacén y un código de producto, lo elimina de la memoria dinámica y devuelve
true.*/
boolean EliminarProducto(int pAlmacen, String pCodProducto) throws RemoteException;
}