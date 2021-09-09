/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

import java.rmi.Naming;
import java.rmi.RMISecurityManager;
import java.util.Random;
import java.util.Scanner;
import java.util.LinkedList;
/**
 *
 * @author jmmartin
 */
public class RMI_Calculadora_Cliente {

    /**
     * @param args the command line arguments
     */
    public static int MenuPrincipal()
    {
        Scanner Teclado=new Scanner(System.in);
        int Salida;
        do
        {
            System.out.println("\n****************************");
            System.out.println("**");
            System.out.println("** 1.- Sumar");
            System.out.println("** 2.- Restar");
            System.out.println("** 3.- Multiplicar");
            System.out.println("** 4.- Dividir");
            System.out.println("** 5.- Automático");
            System.out.println("** 6.- Sumar Vector de Operaciones");
            System.out.println("** 7.- Salir");
            System.out.println("**");            
            System.out.print("** Elige Opcion:");
            Salida=Teclado.nextInt();
        } while (Salida<1 || Salida>7);
        return Salida;
    };
    
    public static void main(String[] args) {
        
        try {
            int Puerto=0;
            String Host;
            Scanner Teclado=new Scanner(System.in);
            System.out.print("Introduce el nº de puerto para comunicarse: ");
            Puerto=Teclado.nextInt();
            System.out.print("Introduce el nombre del host: ");
            Host=Teclado.next();
        
            // Obtiene el stub del rmiregistry    
            Random rnd=new Random(System.nanoTime());
            Calculadora calcStub = (Calculadora) Naming.lookup("rmi://"+Host+":"+Puerto+"/Calculadora");
            
            boolean OtraOperacion=false;
            float a=0,b=0,resultado=0;
            int Nveces=0;
            char operacion=' ';
            String SN;
            int Opcion=0;    
            do
            {
                Opcion = MenuPrincipal();
                if (Opcion<5)
                {
                    System.out.print("Introduce el operando 1: ");
                    a=Teclado.nextFloat();
                    System.out.print("Introduce el operando 2: ");
                    b=Teclado.nextFloat();
                    switch (Opcion)
                    {
                        case 1: resultado=calcStub.sumar(a, b);
                                operacion='+';
                                break;
                        case 2: resultado=calcStub.restar(a, b);
                                operacion='-';
                                break;
                        case 3: resultado=calcStub.multiplicar(a, b);
                                operacion='*';
                                break;
                        case 4: resultado=calcStub.dividir(a, b);  
                                operacion='/';
                    }
                    System.out.println("CLIENTE dice: El resultado de "+a+operacion+b+"="+resultado);
                }
                else
                    if (Opcion<7)
                    {
                        LinkedList Lista=new LinkedList();
                        System.out.print("Introduce el de veces a realizar operaciones aleatorias: ");
                        Nveces=Teclado.nextInt();
                        for (int i=0; i<Nveces; i++)
                        {
                            a=rnd.nextFloat()*2000-1000;
                            b=rnd.nextFloat()*2000-1000;
                            switch(rnd.nextInt(4))
                            {
                                case 0: operacion='+'; break;
                                case 1: operacion='-'; break;
                                case 2: operacion='*'; break;
                                case 3: operacion='/'; break;
                            };
                            if (Opcion==5)
                            {
                                resultado=calcStub.Operacion(a, operacion, b);
                                System.out.println("CLIENTE dice "+i+": El resultado de "+a+operacion+b+"="+resultado);
                            }
                            else
                            {
                                Lista.add(a);
                                Lista.add(b);
                                Lista.add(operacion);
                                System.out.println("CLIENTE dice "+i+": ("+a+operacion+b+") + ....");
                            };        
                        }
                        if (Opcion==6)
                        {
                             System.out.println("---------------------------------------------------------");
                             System.out.println("CLIENTE dice: Suma Total = "+calcStub.Sumatorio(Lista));
                        }
                            
                    };
                
                
            } while (Opcion!=7);    
        
        } catch (Exception e) {
            System.out.println("Error: " + e);
        }
   /*     if (args.length != 1) {
	    System.out.println("Error en Calcualdora: USO: RMI_Calculadora_Cliente host");
	    System.exit(0);
        }
        java.util.Random v=new Random(System.nanoTime());
        
        try {
            String host = args[0];
            
            // Establecer gestor de seguridad 
	    // En este caso no es necesario: no se descargan bytecodes de localizaciones remotas
	    // System.setSecurityManager(new RMISecurityManager());
            
            // Obtiene el stub del rmiregistry
            Calculadora calcStub = (Calculadora) Naming.lookup("rmi://"+host+"/Calculadora");
            for (int i=0; i<100000; i++)
            {
            // Llamdas remotas
            System.out.println("Suma  30 + 5 = " + calcStub.sumar(30, 5));
            //calcStub.incrementarContadorPeticiones();
            System.out.println("Resta  30 - 5 = " + calcStub.restar(30, 5));
            //calcStub.incrementarContadorPeticiones();
            System.out.println("Multiplica  30 * 5 = " + calcStub.multiplicar(30, 5));
            //calcStub.incrementarContadorPeticiones();
            System.out.println("Divide  30 / 5 = " + calcStub.dividir(30, 5));
            //calcStub.incrementarContadorPeticiones();
            System.out.println("Contador peticiones: " + calcStub.getContadorPeticiones());
            Thread.sleep(v.nextInt(500)+500);
                    
            }
        } catch (Exception e) {
            System.out.println("Error: " + e);
        }
        */ 
    }
}
