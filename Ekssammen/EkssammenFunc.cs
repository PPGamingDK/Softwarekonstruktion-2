using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekssammen
{
  public class EkssammenFunc
    {
        public EkssammenData data = new EkssammenData(); // Field. Benyttes for at kalde metoder i klassen IsButikData herfra.
        /// <summary>
        /// Create Container in database
        /// </summary
        public void CreateContainer(Containere containere)
        {
            data.CreateContainer(containere);
        }
        /// <summary>
        /// Create Plane in database
        /// </summary
        public void CreatePlane(Plane plane)
        {
            data.CreatePlane(plane);
        }

        /// <summary>
        /// Create transport in database
        /// </summary
        public void CreateTransport(Transport transport)
        {
            data.CreateTransport(transport);
        }
        /// <summary>
        /// Read all containers from the Database
        /// </summary>
        public void ReadContainere(List<Containere> containeres)
        {
            data.ReadContainere(containeres);
        }
        /// <summary>
        /// Read all planes from the Database
        /// </summary>
        public void ReadPlanes(List<Plane> planes)
        {
            data.ReadPlanes(planes);
        }
        /// <summary>
        /// Read all transports from the Database
        /// </summary>
        public void ReadTransport(List<Plane> planes, List<Containere> containeres, List<Transport> transports)
        {
            data.ReadTransport(planes, containeres, transports);
        }
        /// <summary>
        /// Delete container in database
        /// </summary
        public void DeleteContainer(Containere containere)
        {
            data.DeleteContainer(containere);
        }

        /// <summary>
        /// Delete transport in database
        /// </summary
        public void DeleteTransport(Transport transport)
        {
            data.DeleteTrans(transport);
        }
        /// <summary>
        /// Update Container in database
        /// </summary
        public void UpdateContainer(Containere containere)
        {
            data.UpdateContainer(containere);
        }
        /// <summary>
        /// Update Plane in database
        /// </summary
        public void UpdatePlane(Plane plane)
        {
            data.UpdatePlane(plane);
        }
        /// <summary>
        /// Update transport in database
        /// </summary
        public void UpdateTransport(Transport transport)
        {
            data.UpdateTransport(transport);
        }
        /// <summary>
        /// Delete Plane in database
        /// </summary
        public void DeletePlane(Plane plane)
        {
            data.DeletePlane(plane);
        }
    }
}
