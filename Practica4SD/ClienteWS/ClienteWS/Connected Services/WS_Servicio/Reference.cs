﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClienteWS.WS_Servicio {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TDatosAlmacen", Namespace="http://schemas.datacontract.org/2004/07/")]
    [System.SerializableAttribute()]
    public partial class TDatosAlmacen : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DireccionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FicheroField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NombreField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Direccion {
            get {
                return this.DireccionField;
            }
            set {
                if ((object.ReferenceEquals(this.DireccionField, value) != true)) {
                    this.DireccionField = value;
                    this.RaisePropertyChanged("Direccion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Fichero {
            get {
                return this.FicheroField;
            }
            set {
                if ((object.ReferenceEquals(this.FicheroField, value) != true)) {
                    this.FicheroField = value;
                    this.RaisePropertyChanged("Fichero");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nombre {
            get {
                return this.NombreField;
            }
            set {
                if ((object.ReferenceEquals(this.NombreField, value) != true)) {
                    this.NombreField = value;
                    this.RaisePropertyChanged("Nombre");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TProducto", Namespace="http://schemas.datacontract.org/2004/07/")]
    [System.SerializableAttribute()]
    public partial class TProducto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ClienteWS.WS_Servicio.TFecha CaducidadField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CantidadField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodProductoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescripcionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NombreProductoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float PrecioField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ClienteWS.WS_Servicio.TFecha Caducidad {
            get {
                return this.CaducidadField;
            }
            set {
                if ((object.ReferenceEquals(this.CaducidadField, value) != true)) {
                    this.CaducidadField = value;
                    this.RaisePropertyChanged("Caducidad");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Cantidad {
            get {
                return this.CantidadField;
            }
            set {
                if ((this.CantidadField.Equals(value) != true)) {
                    this.CantidadField = value;
                    this.RaisePropertyChanged("Cantidad");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CodProducto {
            get {
                return this.CodProductoField;
            }
            set {
                if ((object.ReferenceEquals(this.CodProductoField, value) != true)) {
                    this.CodProductoField = value;
                    this.RaisePropertyChanged("CodProducto");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Descripcion {
            get {
                return this.DescripcionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescripcionField, value) != true)) {
                    this.DescripcionField = value;
                    this.RaisePropertyChanged("Descripcion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NombreProducto {
            get {
                return this.NombreProductoField;
            }
            set {
                if ((object.ReferenceEquals(this.NombreProductoField, value) != true)) {
                    this.NombreProductoField = value;
                    this.RaisePropertyChanged("NombreProducto");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float Precio {
            get {
                return this.PrecioField;
            }
            set {
                if ((this.PrecioField.Equals(value) != true)) {
                    this.PrecioField = value;
                    this.RaisePropertyChanged("Precio");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TFecha", Namespace="http://schemas.datacontract.org/2004/07/")]
    [System.SerializableAttribute()]
    public partial class TFecha : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AnyoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int DiaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MesField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Anyo {
            get {
                return this.AnyoField;
            }
            set {
                if ((this.AnyoField.Equals(value) != true)) {
                    this.AnyoField = value;
                    this.RaisePropertyChanged("Anyo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Dia {
            get {
                return this.DiaField;
            }
            set {
                if ((this.DiaField.Equals(value) != true)) {
                    this.DiaField = value;
                    this.RaisePropertyChanged("Dia");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Mes {
            get {
                return this.MesField;
            }
            set {
                if ((this.MesField.Equals(value) != true)) {
                    this.MesField = value;
                    this.RaisePropertyChanged("Mes");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WS_Servicio.IGestionAlmacen")]
    public interface IGestionAlmacen {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/DatosAlmacen", ReplyAction="http://tempuri.org/IGestionAlmacen/DatosAlmacenResponse")]
        ClienteWS.WS_Servicio.TDatosAlmacen DatosAlmacen(int pAlmacen);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/DatosAlmacen", ReplyAction="http://tempuri.org/IGestionAlmacen/DatosAlmacenResponse")]
        System.Threading.Tasks.Task<ClienteWS.WS_Servicio.TDatosAlmacen> DatosAlmacenAsync(int pAlmacen);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/NProductos", ReplyAction="http://tempuri.org/IGestionAlmacen/NProductosResponse")]
        int NProductos(int pAlmacen);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/NProductos", ReplyAction="http://tempuri.org/IGestionAlmacen/NProductosResponse")]
        System.Threading.Tasks.Task<int> NProductosAsync(int pAlmacen);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/CrearAlmacen", ReplyAction="http://tempuri.org/IGestionAlmacen/CrearAlmacenResponse")]
        int CrearAlmacen(string pNombre, string pDireccion, string pNomFichero);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/CrearAlmacen", ReplyAction="http://tempuri.org/IGestionAlmacen/CrearAlmacenResponse")]
        System.Threading.Tasks.Task<int> CrearAlmacenAsync(string pNombre, string pDireccion, string pNomFichero);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/AbrirAlmacen", ReplyAction="http://tempuri.org/IGestionAlmacen/AbrirAlmacenResponse")]
        int AbrirAlmacen(string pNomFichero);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/AbrirAlmacen", ReplyAction="http://tempuri.org/IGestionAlmacen/AbrirAlmacenResponse")]
        System.Threading.Tasks.Task<int> AbrirAlmacenAsync(string pNomFichero);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/GuardarAlmacen", ReplyAction="http://tempuri.org/IGestionAlmacen/GuardarAlmacenResponse")]
        bool GuardarAlmacen(int pAlmacen);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/GuardarAlmacen", ReplyAction="http://tempuri.org/IGestionAlmacen/GuardarAlmacenResponse")]
        System.Threading.Tasks.Task<bool> GuardarAlmacenAsync(int pAlmacen);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/CerrarAlmacen", ReplyAction="http://tempuri.org/IGestionAlmacen/CerrarAlmacenResponse")]
        bool CerrarAlmacen(int pAlmacen);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/CerrarAlmacen", ReplyAction="http://tempuri.org/IGestionAlmacen/CerrarAlmacenResponse")]
        System.Threading.Tasks.Task<bool> CerrarAlmacenAsync(int pAlmacen);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/AlmacenAbierto", ReplyAction="http://tempuri.org/IGestionAlmacen/AlmacenAbiertoResponse")]
        bool AlmacenAbierto(int pAlmacen);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/AlmacenAbierto", ReplyAction="http://tempuri.org/IGestionAlmacen/AlmacenAbiertoResponse")]
        System.Threading.Tasks.Task<bool> AlmacenAbiertoAsync(int pAlmacen);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/BuscaProducto", ReplyAction="http://tempuri.org/IGestionAlmacen/BuscaProductoResponse")]
        int BuscaProducto(int pAlmacen, string pCodProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/BuscaProducto", ReplyAction="http://tempuri.org/IGestionAlmacen/BuscaProductoResponse")]
        System.Threading.Tasks.Task<int> BuscaProductoAsync(int pAlmacen, string pCodProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/ObtenerProducto", ReplyAction="http://tempuri.org/IGestionAlmacen/ObtenerProductoResponse")]
        ClienteWS.WS_Servicio.TProducto ObtenerProducto(int pAlmacen, int pPosProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/ObtenerProducto", ReplyAction="http://tempuri.org/IGestionAlmacen/ObtenerProductoResponse")]
        System.Threading.Tasks.Task<ClienteWS.WS_Servicio.TProducto> ObtenerProductoAsync(int pAlmacen, int pPosProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/AnadirProducto", ReplyAction="http://tempuri.org/IGestionAlmacen/AnadirProductoResponse")]
        bool AnadirProducto(int pAlmacen, ClienteWS.WS_Servicio.TProducto pProdNuevo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/AnadirProducto", ReplyAction="http://tempuri.org/IGestionAlmacen/AnadirProductoResponse")]
        System.Threading.Tasks.Task<bool> AnadirProductoAsync(int pAlmacen, ClienteWS.WS_Servicio.TProducto pProdNuevo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/ActualizarProducto", ReplyAction="http://tempuri.org/IGestionAlmacen/ActualizarProductoResponse")]
        bool ActualizarProducto(int pAlmacen, ClienteWS.WS_Servicio.TProducto pProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/ActualizarProducto", ReplyAction="http://tempuri.org/IGestionAlmacen/ActualizarProductoResponse")]
        System.Threading.Tasks.Task<bool> ActualizarProductoAsync(int pAlmacen, ClienteWS.WS_Servicio.TProducto pProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/EliminarProducto", ReplyAction="http://tempuri.org/IGestionAlmacen/EliminarProductoResponse")]
        bool EliminarProducto(int pAlmacen, string pCodProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGestionAlmacen/EliminarProducto", ReplyAction="http://tempuri.org/IGestionAlmacen/EliminarProductoResponse")]
        System.Threading.Tasks.Task<bool> EliminarProductoAsync(int pAlmacen, string pCodProducto);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGestionAlmacenChannel : ClienteWS.WS_Servicio.IGestionAlmacen, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GestionAlmacenClient : System.ServiceModel.ClientBase<ClienteWS.WS_Servicio.IGestionAlmacen>, ClienteWS.WS_Servicio.IGestionAlmacen {
        
        public GestionAlmacenClient() {
        }
        
        public GestionAlmacenClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GestionAlmacenClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GestionAlmacenClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GestionAlmacenClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ClienteWS.WS_Servicio.TDatosAlmacen DatosAlmacen(int pAlmacen) {
            return base.Channel.DatosAlmacen(pAlmacen);
        }
        
        public System.Threading.Tasks.Task<ClienteWS.WS_Servicio.TDatosAlmacen> DatosAlmacenAsync(int pAlmacen) {
            return base.Channel.DatosAlmacenAsync(pAlmacen);
        }
        
        public int NProductos(int pAlmacen) {
            return base.Channel.NProductos(pAlmacen);
        }
        
        public System.Threading.Tasks.Task<int> NProductosAsync(int pAlmacen) {
            return base.Channel.NProductosAsync(pAlmacen);
        }
        
        public int CrearAlmacen(string pNombre, string pDireccion, string pNomFichero) {
            return base.Channel.CrearAlmacen(pNombre, pDireccion, pNomFichero);
        }
        
        public System.Threading.Tasks.Task<int> CrearAlmacenAsync(string pNombre, string pDireccion, string pNomFichero) {
            return base.Channel.CrearAlmacenAsync(pNombre, pDireccion, pNomFichero);
        }
        
        public int AbrirAlmacen(string pNomFichero) {
            return base.Channel.AbrirAlmacen(pNomFichero);
        }
        
        public System.Threading.Tasks.Task<int> AbrirAlmacenAsync(string pNomFichero) {
            return base.Channel.AbrirAlmacenAsync(pNomFichero);
        }
        
        public bool GuardarAlmacen(int pAlmacen) {
            return base.Channel.GuardarAlmacen(pAlmacen);
        }
        
        public System.Threading.Tasks.Task<bool> GuardarAlmacenAsync(int pAlmacen) {
            return base.Channel.GuardarAlmacenAsync(pAlmacen);
        }
        
        public bool CerrarAlmacen(int pAlmacen) {
            return base.Channel.CerrarAlmacen(pAlmacen);
        }
        
        public System.Threading.Tasks.Task<bool> CerrarAlmacenAsync(int pAlmacen) {
            return base.Channel.CerrarAlmacenAsync(pAlmacen);
        }
        
        public bool AlmacenAbierto(int pAlmacen) {
            return base.Channel.AlmacenAbierto(pAlmacen);
        }
        
        public System.Threading.Tasks.Task<bool> AlmacenAbiertoAsync(int pAlmacen) {
            return base.Channel.AlmacenAbiertoAsync(pAlmacen);
        }
        
        public int BuscaProducto(int pAlmacen, string pCodProducto) {
            return base.Channel.BuscaProducto(pAlmacen, pCodProducto);
        }
        
        public System.Threading.Tasks.Task<int> BuscaProductoAsync(int pAlmacen, string pCodProducto) {
            return base.Channel.BuscaProductoAsync(pAlmacen, pCodProducto);
        }
        
        public ClienteWS.WS_Servicio.TProducto ObtenerProducto(int pAlmacen, int pPosProducto) {
            return base.Channel.ObtenerProducto(pAlmacen, pPosProducto);
        }
        
        public System.Threading.Tasks.Task<ClienteWS.WS_Servicio.TProducto> ObtenerProductoAsync(int pAlmacen, int pPosProducto) {
            return base.Channel.ObtenerProductoAsync(pAlmacen, pPosProducto);
        }
        
        public bool AnadirProducto(int pAlmacen, ClienteWS.WS_Servicio.TProducto pProdNuevo) {
            return base.Channel.AnadirProducto(pAlmacen, pProdNuevo);
        }
        
        public System.Threading.Tasks.Task<bool> AnadirProductoAsync(int pAlmacen, ClienteWS.WS_Servicio.TProducto pProdNuevo) {
            return base.Channel.AnadirProductoAsync(pAlmacen, pProdNuevo);
        }
        
        public bool ActualizarProducto(int pAlmacen, ClienteWS.WS_Servicio.TProducto pProducto) {
            return base.Channel.ActualizarProducto(pAlmacen, pProducto);
        }
        
        public System.Threading.Tasks.Task<bool> ActualizarProductoAsync(int pAlmacen, ClienteWS.WS_Servicio.TProducto pProducto) {
            return base.Channel.ActualizarProductoAsync(pAlmacen, pProducto);
        }
        
        public bool EliminarProducto(int pAlmacen, string pCodProducto) {
            return base.Channel.EliminarProducto(pAlmacen, pCodProducto);
        }
        
        public System.Threading.Tasks.Task<bool> EliminarProductoAsync(int pAlmacen, string pCodProducto) {
            return base.Channel.EliminarProductoAsync(pAlmacen, pCodProducto);
        }
    }
}