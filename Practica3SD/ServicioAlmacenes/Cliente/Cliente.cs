using System;
using ServicioAlmacenes;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;

namespace Cliente
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

            RemotingConfiguration.Configure("Cliente.exe.config", false);
            //Tenemos que conceder el tiempo
            Servicio servicioProxy = new Servicio();
            ILease tiempo = (ILease)RemotingServices.GetLifetimeService(servicioProxy);
            Esponsor esponsor = new Esponsor();
            tiempo.Register(esponsor);
            
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
                        if (actual != -1) //Comprueba si tenemos abierto algun almacen
                        {
                            servicioProxy.GuardarAlmacen(actual);
                            servicioProxy.CerrarAlmacen(actual);
                            actual = -1;
                            nombreAlmacen = null;
                        }
                        nombre = Leer("Nombre");
                        direccion = Leer("Direccion");
                        fichero = Leer("Fichero");
                        actual = servicioProxy.CrearAlmacen(nombre, direccion, fichero);
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
                            servicioProxy.GuardarAlmacen(actual);
                            servicioProxy.CerrarAlmacen(actual);
                            actual = -1;
                            nombreAlmacen = null;
                        }
                        fichero = Leer("Fichero");
                        actual = servicioProxy.AbrirAlmacen(fichero);
                        if (actual == -1)
                        {
                            Console.WriteLine("Error en la operacion");
                        }
                        else
                        {
                            TDatosAlmacen datos = servicioProxy.DatosAlmacen(actual);
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
                            if (!servicioProxy.CerrarAlmacen(actual))
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
                            if (!servicioProxy.GuardarAlmacen(actual))
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
                            int nProductos = servicioProxy.NProductos(actual);
                            if (nProductos == 0)
                            {
                                Console.WriteLine("No hay productos en este almacen");
                            }
                            else
                            {
                                Console.WriteLine(String.Format("{0,10} {1,25} {2,10} {3,10} {4,16}", "CODIGO", "NOMBRE", "PRECIO", "CANTIDAD", "FECHA CADUCIDAD"));
                                for (int i = 0; i < nProductos; i++)
                                {
                                    TProducto producto = servicioProxy.ObtenerProducto(actual, i);
                                    Console.WriteLine(String.Format("{0,10} {1,25} {2,10} {3,10} {4,16}", producto.CodProd, producto.NombreProducto, producto.Precio, producto.Cantidad, String.Format("{0,2}/{1,2}/{2,4}", producto.Caducidad.getDia(), producto.Caducidad.getMes(), producto.Caducidad.getAnno())));
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
                            anno = LeerInt("Caducidad anho");
                            if (!servicioProxy.AnadirProducto(actual, new TProducto(codPro, nombre, descripcion,  cantidad, precio, new TFecha(dia, mes, anno))))
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
                            TProducto producto = servicioProxy.ObtenerProducto(actual, servicioProxy.BuscaProducto(actual, codPro));
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
                                dia = LeerDefectoInt("Caducidad dia", producto.Caducidad.getDia());
                                mes = LeerDefectoInt("Caducidad mes", producto.Caducidad.getMes());
                                anno = LeerDefectoInt("Caducidad anho", producto.Caducidad.getAnno());
                                if (!servicioProxy.ActualizarProducto(actual, new TProducto(codPro, nombre, descripcion, cantidad, precio, new TFecha(dia, mes, anno))))
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
                            TProducto producto = servicioProxy.ObtenerProducto(actual, servicioProxy.BuscaProducto(actual, codPro));
                            if (producto == null)
                            {
                                Console.WriteLine("Error en la operacion");
                            }
                            else
                            {
                                Console.WriteLine("Nombre: {0}\nDescripcion: {1}\nPrecio: {2}\nCantidad: {3}\nCaducidad: {4}/{5}/{6}\n", producto.NombreProducto, producto.Descripcion, producto.Precio, producto.Cantidad, producto.Caducidad.getDia(), producto.Caducidad.getMes(), producto.Caducidad.getAnno());
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
                            if (!servicioProxy.EliminarProducto(actual, codPro))
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

            tiempo.Unregister(esponsor);
        }
    }
}
