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
public class TProducto implements Serializable {

    final String codProd;
    final String nombreProd;
    final String descripcion;
    final int cantidad;
    final float precio;
    final TFecha caducidad;

    public TProducto(String codProd, String nombreProd, String descripcion, int cantidad, float precio, TFecha caducidad) {
        this.codProd = codProd;
        this.nombreProd = nombreProd;
        this.descripcion = descripcion;
        this.cantidad = cantidad;
        this.precio = precio;
        this.caducidad = caducidad;
    }

    public String getCodProd() {
        return codProd;
    }

    public int getCantidad() {
        return cantidad;
    }

    public String getNombreProd() {
        return nombreProd;
    }

    public float getPrecio() {
        return precio;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public TFecha getCaducidad() {
        return caducidad;
    }

}
