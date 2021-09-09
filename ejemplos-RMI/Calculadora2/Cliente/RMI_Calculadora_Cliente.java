/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

import java.rmi.Naming;
import java.rmi.RMISecurityManager;
import java.util.LinkedList;
import java.util.Random;
import java.util.Scanner;

/**
 *
 * @author jmmartin
 */
public class RMI_Calculadora_Cliente {
    
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
    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        try {
            int Puerto=0;
            String Host;
            Scanner Teclado=new Scanner(System.in);
            System.out.print("Introduce el nº de puerto para comunicarse: ");
            Puerto=Teclado.nextInt();
            System.out.print("Introduce el nombre del host: ");
            Host=Teclado.next();            
            
            Random rnd=new Random(System.nanoTime());
            
            System.setSecurityManager(new RMISecurityManager());
            
            // Obtiene el stub del rmiregistry
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
    }
}
