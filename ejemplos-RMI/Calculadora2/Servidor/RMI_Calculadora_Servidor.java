/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

import java.rmi.RemoteException;
import java.rmi.server.*;
import java.rmi.registry.*;
import java.util.Scanner;



/**
 *
 * @author jmmartin
 */
public class RMI_Calculadora_Servidor {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) throws RemoteException {
       
       
        try {  
            int Puerto = 0;  
            Scanner Teclado=new Scanner(System.in);
            System.out.print("Introduce el nº de puerto para comunicarse: ");
            Puerto=Teclado.nextInt();
            
            Registry registry = LocateRegistry.createRegistry(Puerto);  
            CalculadoraImpl  obj = new CalculadoraImpl();  
            
            Calculadora stub = (Calculadora) UnicastRemoteObject.exportObject(obj,Puerto);  
  
            registry = LocateRegistry.getRegistry(Puerto);  
            registry.bind("Calculadora", stub);  
  
            System.out.println("Servidor Calculadora esperando peticiones ... ");             
        } catch (Exception e) {
            System.out.println("Error en servidor Calculadora:"+e);
        } 
        
/**        
try {
            System.out.println("Servidor crea calculadora remota");
            Calculadora calcStub = new CalculadoraImpl();
                         
            System.out.println("Servidor registra calculadora remota");
            Naming.rebind("rmi://localhost/Calculadora", calcStub);
            
            System.out.println("Servidor entra en espera ... ");             
        } catch (Exception e) {
            System.out.println("Error en servidor:"+e);
        } 
        */
    }
  
}
