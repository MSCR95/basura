/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

import java.rmi.*;
import java.util.Scanner;

/**
 *
 * @author jmmartin
 */
public class RMI_Calculadora_Servidor {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        
        try {
            
            Calculadora calcStub = new CalculadoraImpl();
            int Puerto=0;
            Scanner Teclado=new Scanner(System.in);
            System.out.print("Introduce el nº de puerto para comunicarse: ");
            Puerto=Teclado.nextInt();    
            
            Naming.rebind("rmi://localhost:"+Puerto+"/Calculadora", calcStub);
            
            System.out.println("Servidor Calculadora esperando peticiones ... ");             
        } catch (Exception e) {
            System.out.println("Error en servidor Calculadora:"+e);
        } 
        
    }
  
}
