/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Cliente;

import Comun.GestionAlmacenesIntf;
import Comun.TDatosAlmacen;
import Comun.TFecha;
import Comun.TProducto;
import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.util.Scanner;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author maria
 */
public class Cliente {

    private static String leerDefecto(String concepto, String defecto) {
        System.out.println(concepto + " " + defecto);
        Scanner Teclado = new Scanner(System.in);
        defecto = Teclado.next();
        return defecto;
    }

    private static int leerDefectoInt(String concepto, int defecto) {
        System.out.println(concepto + " " + defecto);
        Scanner Teclado = new Scanner(System.in);
        defecto = Teclado.nextInt();
        return defecto;
    }

    private static float leerDefectoFloat(String concepto, float defecto) {
        System.out.println(concepto + " " + defecto);
        Scanner Teclado = new Scanner(System.in);
        defecto = Teclado.nextFloat();
        return defecto;
    }

    private static String leer(String concepto) {

        String tc;
        System.out.println(concepto);
        Scanner Teclado = new Scanner(System.in);
        tc = Teclado.next();
        return tc;
    }

    private static int leerInt(String concepto) {
        int tc;
        System.out.println(concepto);
        Scanner Teclado = new Scanner(System.in);
        tc = Teclado.nextInt();
        return tc;
    }

    private static float leerFloat(String concepto) {
        float tc;
        System.out.println(concepto);
        Scanner Teclado = new Scanner(System.in);
        tc = Teclado.nextFloat();
        return tc;
    }

    static int Menu(String nombreAlmacen) {
        Scanner Teclado = new Scanner(System.in);
        int opcion;
        do {
            System.out.println("----- Menu Almacenes -----" + nombreAlmacen);
            System.out.println(" 1.- Crear un Almacen vacio.");
            System.out.println(" 2.- Abrir un fichero de almacen.");
            System.out.println(" 3.- Cerrar un almacen.");
            System.out.println(" 4.- Guardar Datos.");
            System.out.println(" 5.- Listar productos de almacen.");
            System.out.println(" 6.- Añadir un producto.");
            System.out.println(" 7.- Actualizar un producto.");
            System.out.println(" 8.- Consultar un producto.");
            System.out.println(" 9.- Eliminar un producto.");
            System.out.println(" 0.- Salir.");
            opcion = Teclado.nextInt();
        } while (opcion < 0 || opcion > 9);
        return opcion;
    }

    public static void main(String[] args) {

        try {
            int puerto;
            String host;
            puerto = 6685;
            host = "localhost";
            GestionAlmacenesIntf stub = (GestionAlmacenesIntf) Naming.lookup("rmi://" + host + ":" + puerto + "/GestionAlmacenes");
            String nombre, direccion, fichero, descripcion, codPro, nombreAlmacen;
            int opcion, cantidad, dia, mes, anno, actual;
            float precio;
            nombre="";
            actual = -1;
            nombreAlmacen = null;
            do {
                opcion = Menu(nombreAlmacen);
                switch (opcion) {
                    case 1:     //Crear un Almacen vacio
                        if (actual != -1) //si ya tenemos un almacen abierto
                        {
                            stub.GuardarAlmacen(actual);
                            stub.CerrarAlmacen(actual);
                            actual = -1;
                            nombreAlmacen = null;
                        }
                        nombre = leer("Nombre");
                        direccion = leer("Direccion");
                        fichero = leer("Fichero");
                        actual = stub.CrearAlmacen(nombre, direccion, fichero);
                        if (actual == -1) {
                            System.out.println("Error en la operacion 1");
                        } else {
                            nombreAlmacen = nombre;
                        }
                        break;
                    case 2:     //Abrir un fichero de almacen
                        if (actual != -1) {
                            stub.GuardarAlmacen(actual);
                            stub.CerrarAlmacen(actual);
                            actual = -1;
                            nombreAlmacen = null;
                        }
                        fichero = leer("Fichero");
                        actual = stub.AbrirAlmacen(fichero);
                        if (actual == -1) {
                            System.out.println("Error en la operacion 2");
                        } else {
                            TDatosAlmacen datos = stub.DatosAlmacen(actual);
                            if (datos == null) {
                                System.out.println("Error en la operacion 2");
                            } else {
                                nombreAlmacen = datos.getNombre();
                            }
                        }
                        break;
                    case 3:     //Cerrar un almacen
                        if (actual == -1) {
                            System.out.println("No hay ningun almacen abierto");
                        } else {
                            if (!stub.CerrarAlmacen(actual)) {
                                System.out.println("No se ha podido cerrar el almacen");
                            }
                            actual = -1;
                            nombreAlmacen = null;
                        }
                        break;
                    case 4:     //Guardar datos
                        if (actual == -1) {
                            System.out.println("No se ha anierto ningun almacen");
                        } else {
                            if (!stub.GuardarAlmacen(actual)) {
                                System.out.println("Error en la operacion 4");
                            }
                        }
                        break;
                    case 5:      //Listar productos de almacen
                        if (actual == -1) {
                            System.out.println("No se ha abierto ningun almacen");
                        } else {
                            int nProductos = stub.NProductos(actual);
                            if (nProductos == 0) {
                                System.out.println("No hay productos en este almacen");
                            } else {
                                System.out.println(" CODIGO         NOMBRE          PRECIO          CANTIDAD        FECHA           CADUCIDAD");
                                for (int i = 0; i < nProductos; i++) {
                                    TProducto producto = stub.ObtenerProducto(actual, i);
                                    System.out.println(" " + producto.getCodProd() + "        " + producto.getNombreProd() + "       " + producto.getPrecio() + "       " + producto.getCantidad() + "       " + producto.getCaducidad().getDia() + "/" + producto.getCaducidad().getMes() + "/" + producto.getCaducidad().getAnno());
                                }
                            }
                        }
                        break;
                    case 6:     //Añadir un producto
                        if (actual == -1) {
                            System.out.println("No se ha abierto ningun almacen");
                        } else {
                            codPro = leer("Codigo");
                            nombre = leer("Nombre");
                            descripcion = leer("Descripcion");
                            cantidad = leerInt("Cantidad");
                            precio = leerFloat("Precio");
                            dia = leerInt("Caducidad dia");
                            mes = leerInt("Caducidad mes");
                            anno = leerInt("Caducidad anho");
                            if (!stub.AnadirProducto(actual, new TProducto(codPro, nombre, descripcion, cantidad, precio, new TFecha(dia, mes, anno)))) {
                                System.out.println("Error en la opercacion 6");
                            }
                        }
                        break;
                    case 7:     //Actualizar un producto
                        if (actual == -1) {
                            System.out.println("No hay ningun almacen abierto");
                        } else {
                            //introduzca el cod del producto
                            codPro = leer("Codigo");
                            TProducto producto = stub.ObtenerProducto(actual, stub.BuscaProducto(actual, codPro));
                            if (producto == null) {
                                System.out.println("No se ha encontrado el producto");
                            } else {
                                nombre = leerDefecto("Nombre", producto.getNombreProd());
                                descripcion = leerDefecto("Descripcion", producto.getDescripcion());
                                cantidad = leerDefectoInt("Cantidad", producto.getCantidad());
                                precio = leerDefectoFloat("Precio", producto.getPrecio());
                                dia = leerDefectoInt("Caducidad dia", producto.getCaducidad().getDia());
                                mes = leerDefectoInt("Caducidad mes", producto.getCaducidad().getMes());
                                anno = leerDefectoInt("Caducidad anho", producto.getCaducidad().getAnno());
                                if (!stub.ActualizarProducto(actual, new TProducto(codPro, nombre, descripcion, cantidad, precio, new TFecha(dia, mes, anno)))) {
                                    System.out.println("Error en la operacion 7");
                                }
                            }
                        }
                        break;
                    case 8:     //Consultar un producto
                        if (actual == -1) {
                            System.out.println("No se ha anierto ningun almacen");
                        } else {
                            codPro = leer("Codigo");
                            TProducto producto = stub.ObtenerProducto(actual, stub.BuscaProducto(actual, codPro));
                            if (producto == null) {
                                System.out.println("Error n la opercacion 8");
                            } else {
                                System.out.println("Nombre: " + producto.getNombreProd());
                                System.out.println("Precio: " + producto.getPrecio());
                                System.out.println("Cantidad: " + producto.getCantidad());
                                System.out.println("Caducidad: " + producto.getCaducidad().getDia() + "/" + producto.getCaducidad().getMes() + "/" + producto.getCaducidad().getAnno());

                            }
                        }
                        break;
                    case 9:     // Eliminar un producto
                        if (actual == -1) {
                            System.out.println("No se ha habierto ningun almcen");
                        } else {
                            codPro = leer("Codigo del producto a eliminar");
                            if (!stub.EliminarProducto(actual, codPro)) {
                                System.out.println("Error en la operacion 9");
                            }
                        }
                        break;
                    case 0:     //Salir
                        System.exit(0);
                        break;
                }
                if (opcion > 0 && opcion <= 9) {
                    System.out.println("pulse para continuar\n");
                }
            } while (opcion != 0);
            System.exit(0);

        } catch (NotBoundException | MalformedURLException | RemoteException ex) {
            Logger.getLogger(Cliente.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}
