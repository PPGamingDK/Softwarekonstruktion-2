using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ekssammen.EkssammenDatasetTableAdapters;
using System.Windows;
using static Ekssammen.EkssammenDataset;
namespace Ekssammen
{
    public class EkssammenData : Window
    {
        EkssammenDataset EkssammenDataset = new EkssammenDataset(); // Benyttes for at kalde metoder i klassen EkssammenDataset herfra
        TableAdapterManager AdapterManager = new TableAdapterManager(); // Benyttes for at kalde metoder i klassen TableAdapterManager herfra.

        public EkssammenData()  //Constructor.Benyttes til at oprette instanser(objekter) af vores adapters.
        {
            AdapterManager.ContainereTableAdapter = new ContainereTableAdapter();
            AdapterManager.PlaneTableAdapter = new PlaneTableAdapter();
            AdapterManager.TransportTableAdapter = new TransportTableAdapter();
        }

        public void ReadContainere(List<Containere> containeres) // 
        {
            containeres.Clear(); // Clears the Containers list
            ContainereDataTable containereRows = new ContainereDataTable();
            AdapterManager.ContainereTableAdapter.Fill(containereRows);
            foreach (ContainereRow row in containereRows) // Loop through database and add containers to list.
            {
                Containere containere = new Containere(row.Id, row.Weight, row.Airport); // Add to class.
                containeres.Add(containere); // Add container to list
            }
        }

        public void ReadPlanes(List<Plane> planes)
        {
            planes.Clear();
            PlaneDataTable planeRows = new PlaneDataTable();
            AdapterManager.PlaneTableAdapter.Fill(planeRows);
            foreach (PlaneRow row in planeRows)
            {
                Plane plane = new Plane(row.Id, row.Maxweight, row.Registration);
                planes.Add(plane);
            }
        }

        public void ReadTransport(List<Plane> planes, List<Containere> containeres, List<Transport> transports)
        {
            transports.Clear();
            TransportDataTable transportRows = new TransportDataTable();
            AdapterManager.TransportTableAdapter.Fill(transportRows);
            foreach (TransportRow row in transportRows)
            {
                Transport transport = new Transport(row.Id, row.Date, GetPlanes(planes, row.FlightId), GetContainere(containeres, row.ContainerId));
                transports.Add(transport);
            }
        }

        public Plane GetPlanes(List<Plane> planes, int PlaneId)
        {
            foreach (Plane plane in planes)
            {
                if(plane.Id == PlaneId)
                {
                    return plane;
                }
            }
            return null;
        }

        public Containere GetContainere(List<Containere> containeres, int ContainerId)
        {
            foreach (Containere containere in containeres)
            {
                if(containere.Id == ContainerId)
                {
                    return containere;
                }
            }
            return null;
        }

        public void CreatePlane(Plane plane)
        {
            PlaneRow row = EkssammenDataset.Plane.NewPlaneRow();
            row.Maxweight = plane.MaxWeight;
            row.Registration = plane.Registration;
            RækkeTilDatabase(row);
        }

        public void CreateContainer(Containere containere)
        {
            ContainereRow row = EkssammenDataset.Containere.NewContainereRow();
            row.Weight = containere.Weight;
            row.Airport = containere.Airport;
            RækkeTilDatabase(row);
        }

        public void CreateTransport(Transport transport) // CreateTransport with data from func
        {
            TransportRow row = EkssammenDataset.Transport.NewTransportRow();
            row.Date = transport.Date;
            row.FlightId = transport.Plane.Id;
            row.ContainerId = transport.Containere.Id;
            RækkeTilDatabase(row); // Create in Database
        }

        private void RækkeTilDatabase(ContainereRow row) // Oprettelse af containere i database
        {
            EkssammenDataset.Containere.Rows.Add(row);
            AdapterManager.ContainereTableAdapter.Update(EkssammenDataset.Containere);
        }

        private void RækkeTilDatabase(PlaneRow row) // Overloading - Oprettelse af Fly i databasen. 
        {
            EkssammenDataset.Plane.Rows.Add(row);
            AdapterManager.PlaneTableAdapter.Update(EkssammenDataset.Plane);
        }

        private void RækkeTilDatabase(TransportRow row) // Overloading - Oprettelse af transport i databasen. 
        {
            EkssammenDataset.Transport.Rows.Add(row);
            AdapterManager.TransportTableAdapter.Update(EkssammenDataset.Transport);
        }

        public void DeleteContainer(Containere containere)
        {
            AdapterManager.ContainereTableAdapter.Delete(containere.Id, containere.Weight, containere.Airport);
        }

        public void DeletePlane(Plane plane)
        {
            AdapterManager.PlaneTableAdapter.Delete(plane.Id, plane.MaxWeight, plane.Registration);
        }

        public void DeleteTrans(Transport transport)
        {
            if (transport != null) { 
                AdapterManager.TransportTableAdapter.Delete(transport.Id, transport.Date, transport.Plane.Id, transport.Containere.Id);
            }
        }

        public void UpdateContainer(Containere containere)
        {
            try
            {
                ContainereDataTable containereRows = new ContainereDataTable();
                AdapterManager.ContainereTableAdapter.Fill(containereRows);
                ContainereRow row = containereRows.FindById(containere.Id);
                if (row != null)
                {
                    row.Weight = containere.Weight;
                    row.Airport = containere.Airport;
                    AdapterManager.ContainereTableAdapter.Update(containereRows);
                }
            } catch(Exception)
            {
                MessageBox.Show("Der opstod en fejl ved redigering i databasen");
            }

        }

        public void UpdateTransport(Transport transport)
        {
            try
            {
                TransportDataTable transportRows = new TransportDataTable();
                AdapterManager.TransportTableAdapter.Fill(transportRows);
                TransportRow row = transportRows.FindById(transport.Id);
                if (row != null)
                {
                    row.Date = transport.Date;
                    row.FlightId = transport.Plane.Id;
                    row.ContainerId = transport.Containere.Id;
                    AdapterManager.TransportTableAdapter.Update(transportRows);
                }


            }catch(Exception)
            {
                MessageBox.Show("Der opstod en fejl ved redigering i databasen");
            }
        }

        public void UpdatePlane(Plane plane)
        {
            try
            {
                PlaneDataTable planeRows = new PlaneDataTable();
                AdapterManager.PlaneTableAdapter.Fill(planeRows);
                PlaneRow row = planeRows.FindById(plane.Id);
                if (row != null)
                {
                    row.Maxweight = plane.MaxWeight;
                    row.Registration = plane.Registration;
                    AdapterManager.PlaneTableAdapter.Update(planeRows);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Der opstod en fejl ved redigering i databasen");
            }

        }

    }
}
