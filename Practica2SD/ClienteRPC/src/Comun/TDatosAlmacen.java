/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Comun;

import java.io.Serializable;

/**
 *
 * @author maria
 */
public class TDatosAlmacen implements Serializable {

    final String nombre;
    final String direccion;
    final String fichero;

    public TDatosAlmacen(String nombre, String direccion, String fichero) {
        this.nombre = nombre;
        this.direccion = direccion;
        this.fichero = fichero;
    }

    public String getNombre() {
        return nombre;
    }

    public String getDireccion() {
        return direccion;
    }

    public String getFichero() {
        return fichero;
    }
}
