using ClienteWS.WS_Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteWS
{
    class Cliente
    {
        private static String LeerDefecto(String concepto, String defecto)
        {
            Console.Write("{0} [{1}]: ", concepto, defecto);
            String userInput = Console.ReadLine();
            if (userInput.Trim().Length != 0)
            {
                return userInput;
            }
            return defecto;
        }

        private static int LeerDefectoInt(String concepto, int defecto)
        {
            try
            {
                return Int32.Parse(LeerDefecto(concepto, defecto.ToString()));
            }
            catch (Exception e)
            {
                return LeerDefectoInt(concepto, defecto);
            }
        }

        private static float LeerDefectoFloat(String concepto, float defecto)
        {
            try
            {
                return Single.Parse(LeerDefecto(concepto, defecto.ToString()));
            }
            catch (Exception e)
            {
                return LeerDefectoFloat(concepto, defecto);
            }
        }

        private static String Leer(String concepto)
        {
            Console.Write("{0}: ", concepto);
            return Console.ReadLine();
        }

        private static int LeerInt(String concepto)
        {
            try
            {
                return Int32.Parse(Leer(concepto));
            }
            catch (Exception e)
            {
                return LeerInt(concepto);
            }
        }

        private static float LeerFloat(String concepto)
        {
            try
            {
                return Single.Parse(Leer(concepto));
            }
            catch (Exception e)
            {
                return LeerFloat(concepto);
            }
        }
        static int Menu(String nombreAlmacen)
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("----- Menu Almacenes ----- {0} \n\n", nombreAlmacen);
                Console.WriteLine(" 1.- Crear un Almacen vacio.");
                Console.WriteLine(" 2.- Abrir un fichero de almacen.");
                Console.WriteLine(" 3.- Cerrar un almacen.");
                Console.WriteLine(" 4.- Guardar Datos.");
                Console.WriteLine(" 5.- Listar productos de almacen.");
                Console.WriteLine(" 6.- Añadir un producto.");
                Console.WriteLine(" 7.- Actualizar un producto.");
                Console.WriteLine(" 8.- Consultar un producto.");
                Console.WriteLine(" 9.- Eliminar un producto.");
                Console.WriteLine(" 0.- Salir.\n");
                int.TryParse(Console.ReadLine(), out opcion);
            }
            while (opcion < 0 || opcion > 9);
            return opcion;
        }

        static void Main(string[] args)
        {

            WS_Servicio.GestionAlmacenClient servicio = new GestionAlmacenClient();

            String nombre, direccion, fichero, descripcion, codPro, nombreAlmacen;
            int opcion, cantidad, dia, mes, anno, actual;
            float precio;
            actual = -1;
            nombreAlmacen = null;
            do
            {
                opcion = Menu(nombreAlmacen);
                switch (opcion)
                {
                    case 1:     //Crear un Almacen vacio
                        if (actual != -1) //si ya tenemos un almacen abierto
                        {
                            servicio.GuardarAlmacen(actual);
                            servicio.CerrarAlmacen(actual);
                            actual = -1;
                            nombreAlmacen = null;
                        }
                        nombre = Leer("Nombre");
                        direccion = Leer("Direccion");
                        fichero = Leer("Fichero");
                        actual = servicio.CrearAlmacen(nombre, direccion, fichero);
                        if (actual == -1)
                        {
                            Console.WriteLine("Error en la operacion");
                        }
                        else
                        {
                            nombreAlmacen = nombre;
                        }
                        break;
                    case 2:     //Abrir un fichero de almacen
                        if (actual != -1)
                        {
                            servicio.GuardarAlmacen(actual);
                            servicio.CerrarAlmacen(actual);
                            actual = -1;
                            nombreAlmacen = null;
                        }
                        fichero = Leer("Fichero");
                        actual = servicio.AbrirAlmacen(fichero);
                        if (actual == -1)
                        {
                            Console.WriteLine("Error en la operacion");
                        }
                        else
                        {
                            TDatosAlmacen datos = servicio.DatosAlmacen(actual);
                            if (datos == null)
                            {
                                Console.WriteLine("Error en la operacion");
                            }
                            else
                            {
                                nombreAlmacen = datos.Nombre;
                            }
                        }
                        break;
                    case 3:     //Cerrar un almacen
                        if (actual == -1)
                        {
                            Console.WriteLine("No hay ningun almacen bierto");
                        }
                        else
                        {
                            if (!servicio.CerrarAlmacen(actual))
                            {
                                Console.WriteLine("No se ha podido cerrar el almacen");
                            }
                            actual = -1;
                            nombreAlmacen = null;
                        }
                        break;
                    case 4:     //Guardar datos
                        if (actual == -1)
                        {
                            Console.WriteLine("No se ha abierto ningun almacen");
                        }
                        else
                        {
                            if (!servicio.GuardarAlmacen(actual))
                            {
                                Console.WriteLine("Error en la operacion");
                            }
                        }
                        break;
                    case 5:      //Listar productos de almacen
                        if (actual == -1)
                        {
                            Console.WriteLine("No se ha abierto ningun almacen");
                        }
                        else
                        {
                            int nProductos = servicio.NProductos(actual);
                            if (nProductos == 0)
                            {
                                Console.WriteLine("No hay productos en este almacen");
                            }
                            else
                            {
                                Console.WriteLine(String.Format("{0,10} {1,25} {2,10} {3,10} {4,16}", "CODIGO", "NOMBRE", "PRECIO", "CANTIDAD", "FECHA CADUCIDAD"));
                                for (int i = 0; i < nProductos; i++)
                                {
                                    TProducto producto = servicio.ObtenerProducto(actual, i);
                                    Console.WriteLine(String.Format("{0,10} {1,25} {2,10} {3,10} {4,16}", producto.CodProducto, producto.NombreProducto, producto.Precio, producto.Cantidad, String.Format("{0,2}/{1,2}/{2,4}", producto.Caducidad.Dia, producto.Caducidad.Mes, producto.Caducidad.Anyo)));
                                }
                            }
                        }
                        break;
                    case 6:     //Añadir un producto
                        if (actual == -1)
                        {
                            Console.WriteLine("No se ha abierto ningun almacen");
                        }
                        else
                        {
                            codPro = Leer("Codigo");
                            nombre = Leer("Nombre");
                            descripcion = Leer("Descripcion");
                            cantidad = LeerInt("Cantidad");
                            precio = LeerFloat("Precio");
                            dia = LeerInt("Caducidad dia");
                            mes = LeerInt("Caducidad mes");
                            anno = LeerInt("Caducidad anno");
                            TProducto aux = new TProducto();
                            aux.CodProducto = codPro;
                            aux.NombreProducto = nombre;
                            aux.Descripcion = descripcion;
                            aux.Precio = precio;
                            aux.Cantidad = cantidad;
                            TFecha aux2 = new TFecha();
                            aux2.Dia = dia;
                            aux2.Mes = mes;
                            aux2.Anyo = anno;
                            aux.Caducidad = aux2;


                            if (!servicio.AnadirProducto(actual, aux))
                            {
                                Console.WriteLine("Error en la operacion");
                            }
                        }
                        break;
                    case 7:     //Actualizar un producto
                        if (actual == -1)
                        {
                            Console.WriteLine("No hay ningun almacen abierto");
                        }
                        else
                        {
                            //introduzca el cod del producto
                            codPro = Leer("Codigo");
                            TProducto producto = servicio.ObtenerProducto(actual, servicio.BuscaProducto(actual, codPro));
                            if (producto == null)
                            {
                                Console.WriteLine("No se ha encontrado el producto");
                            }
                            else
                            {
                                nombre = LeerDefecto("Nombre", producto.NombreProducto);
                                descripcion = LeerDefecto("Descripcion", producto.Descripcion);
                                cantidad = LeerDefectoInt("Cantidad", producto.Cantidad);
                                precio = LeerDefectoFloat("Precio", producto.Precio);
                                dia = LeerDefectoInt("Caducidad dia", producto.Caducidad.Dia);
                                mes = LeerDefectoInt("Caducidad mes", producto.Caducidad.Mes);
                                anno = LeerDefectoInt("Caducidad anno", producto.Caducidad.Anyo);
                                TProducto aux = new TProducto();
                                aux.CodProducto = codPro;
                                aux.NombreProducto = nombre;
                                aux.Descripcion = descripcion;
                                aux.Precio = precio;
                                aux.Cantidad = cantidad;
                                TFecha aux2 = new TFecha();
                                aux2.Dia = dia;
                                aux2.Mes = mes;
                                aux2.Anyo = anno;
                                aux.Caducidad = aux2;

                                if (!servicio.ActualizarProducto(actual, aux))
                                {
                                    Console.WriteLine("Error en la operacion");
                                }
                            }
                        }
                        break;
                    case 8:     //Consultar un producto
                        if (actual == -1)
                        {
                            Console.WriteLine("No se ha abierto ningun almacen");
                        }
                        else
                        {
                            codPro = Leer("Codigo");
                            TProducto producto = servicio.ObtenerProducto(actual, servicio.BuscaProducto(actual, codPro));
                            if (producto == null)
                            {
                                Console.WriteLine("Error en la operacion");
                            }
                            else
                            {
                                Console.WriteLine("Nombre: {0}\nDescripcion: {1}\nPrecio: {2}\nCantidad: {3}\nCaducidad: {4}/{5}/{6}\n", producto.NombreProducto, producto.Descripcion, producto.Precio, producto.Cantidad, producto.Caducidad.Dia, producto.Caducidad.Mes, producto.Caducidad.Anyo);
                            }
                        }
                        break;
                    case 9:     // Eliminar un producto
                        if (actual == -1)
                        {
                            Console.WriteLine("No se ha abierto ningun almacen");
                        }
                        else
                        {
                            codPro = Leer("Codigo del producto a eliminar");
                            if (!servicio.EliminarProducto(actual, codPro))
                            {
                                Console.WriteLine("Error en la operacion");
                            }
                        }
                        break;
                    case 0:     //Salir
                        break;
                }
                if (opcion > 0 && opcion <= 9)
                {
                    Console.WriteLine("Pulse para continuar\n");
                    Console.ReadLine();
                }
            } while (opcion != 0);

            Console.WriteLine("Pulse Enter para salir...");
            Console.ReadLine();
        }
    }

}
