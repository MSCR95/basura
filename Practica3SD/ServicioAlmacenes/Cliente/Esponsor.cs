using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    class Esponsor : MarshalByRefObject, ISponsor
    {
        private readonly double tiempoRenovacion = 10;
        private readonly bool renovar = true;
        public TimeSpan Renewal(ILease lease)
        {
            if (renovar)
                return TimeSpan.FromSeconds(tiempoRenovacion);
            else
                return TimeSpan.Zero;
        }
    }
}
