using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
namespace Ekssammen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public EkssammenFunc func = new EkssammenFunc();
        public List<Containere> containeres = new List<Containere>();
        public List<Plane> planes = new List<Plane>();
        public List<Transport> transports = new List<Transport>();
        public MainWindow()
        {
            InitializeComponent();
            func.ReadContainere(containeres);
            func.ReadPlanes(planes);
            func.ReadTransport(planes, containeres, transports);
            DG_Container.ItemsSource = containeres;
            DG_Plane.ItemsSource = planes;
            DG_Transport.ItemsSource = transports;

        }

        private void btn_CtnSave_Click(object sender, RoutedEventArgs e) // Button to save Container
        {
            try // Try catch
            {
                int Weight = Convert.ToInt32(tb_CtnWeigt.Text); // Convert string to int, so we can use Weight.
                string Airport = tb_CtnAirport.Text;
                containeres.Add(new Containere(Weight, Airport)); // Add to container list
                func.CreateContainer(containeres[containeres.Count - 1]); // Call function to CreateContainer in Database
                func.ReadContainere(containeres); // Read all containers
                RefreshGrid(); // Refresh Datagrid
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en fejl, vær sikker på du kun har indtastet tal i vægt.");

            }
        }

        private void btn_CtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Containere containere = (DG_Container.SelectedItem as Containere); // 
                func.DeleteContainer(containere); // Call DeleteContainer func
                func.ReadContainere(containeres);
                RefreshGrid();

            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en fejl, vær sikker på du ikke sletter en container der er med i et fly."); // Throw exception when error happens
            }
        }


        private void btn_CtnEdit_Click(object sender, RoutedEventArgs e) // Container delete button
        {
            try
            {
                Containere containere = (DG_Container.SelectedItem as Containere);
                Containere UpdateContainer = new Containere(Convert.ToInt32(tb_CtnId.Text), Convert.ToInt32(tb_CtnWeigt.Text), tb_CtnAirport.Text);
                func.UpdateContainer(UpdateContainer);
                func.ReadContainere(containeres);
                RefreshGrid();
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en fejl, vær sikker på du ikke sletter en container der er med i et fly.");
            }
        }

        private void DG_Container_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Containere containere = DG_Container.SelectedItem as Containere;
                if (containere != null)
                {
                    tb_TransContainerId.Text = Convert.ToString(containere.Id);
                    tb_CtnId.Text = Convert.ToString(containere.Id);
                    tb_CtnWeigt.Text = Convert.ToString(containere.Weight);
                    tb_CtnAirport.Text = containere.Airport;
                }

            }
            catch (Exception)
            {
            }


        }

        private void btn_FlySave_Click(object sender, RoutedEventArgs e)
        {
            try // Try catch
            {
                int capacity = Convert.ToInt32(tb_FlyWeight.Text);
                string regnumber = tb_FlyRegistration.Text;
                planes.Add(new Plane(capacity, regnumber));
                func.CreatePlane(planes[planes.Count - 1]);
                func.ReadPlanes(planes);
                RefreshGrid();
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en fejl, vær sikker på du kun har indtastet tal i Kapacitet.");
            }
        }

        private void btn_FlyDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Plane plane = (DG_Plane.SelectedItem as Plane);
                func.DeletePlane(plane);
                func.ReadPlanes(planes);
                RefreshGrid();

            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en fejl, vær sikker på du ikke sletter en container der er med i et fly.");
            }
        }

        private void btn_FlyEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Plane plane = (DG_Plane.SelectedItem as Plane);
                Plane editplane = new Plane(Convert.ToInt32(tb_FlyId.Text), Convert.ToInt32(tb_FlyWeight.Text), tb_FlyRegistration.Text);
                func.UpdatePlane(editplane);
                func.ReadPlanes(planes);
                RefreshGrid();
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en fejl, vær sikker på du ikke redigere en container der er med i et fly.");
            }
        }

        private void DG_Plane_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Plane plane = DG_Plane.SelectedItem as Plane;
                if (plane != null)
                {
                    tb_TransFlightId.Text = Convert.ToString(plane.Id);
                    tb_FlyId.Text = Convert.ToString(plane.Id);
                    tb_FlyWeight.Text = Convert.ToString(plane.MaxWeight);
                    tb_FlyRegistration.Text = plane.Registration;
                }

            }
            catch (Exception)
            {
            }
        }

        private void btn_TransSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    Containere containere = (DG_Container.SelectedItem as Containere);
                Plane plane = (DG_Plane.SelectedItem as Plane);
                if (plane != null && containere != null) {
                    int ContainerWeight = containere.Weight;
                    string Date = tb_TransDate.Text;
                    #region Fejlhåndtering
                    List<Transport> temp = new List<Transport>();
                    foreach (Transport transport in transports)
                    {
                        if (transport.Date == Date && transport.Plane.Id == plane.Id)
                        {
                            
                            if(transport.Containere.Airport == containere.Airport)
                            {
                                temp.Add(transport);
                            } else
                            {
                                throw new Exception("Flyet flyver allerede til en anden destination på den dato");
                            }
                        }
                    }
                    int currentweight = 0;
                    foreach(Transport transport in temp)
                    {
                        currentweight += transport.Containere.Weight;
                    }
                    if(currentweight + ContainerWeight > plane.MaxWeight)
                    {
                        throw new Exception("Kan ikke tilføje containeren, det overstiger max vægt.");
                    }
                    #endregion
                    if (ContainerWeight <= plane.MaxWeight)
                    {
                        transports.Add(new Transport(Date, plane, containere));
                        func.CreateTransport(transports[transports.Count - 1]);
                        func.ReadTransport(planes, containeres, transports);
                        RefreshGrid();
                    }
                    else
                    {
                        throw new Exception("Containeren vejer for meget");
                    }
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_TransDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Transport transport = (DG_Transport.SelectedItem as Transport);
                func.DeleteTransport(transport);
                func.ReadTransport(planes,containeres,transports);
                RefreshGrid();
            } catch(Exception)
            {

            }
        }
        /// <summary>
        /// Refresh all datagrid's
        /// </summary>
        private void RefreshGrid()
        {
            DG_Container.Items.Refresh();
            DG_Plane.Items.Refresh();
            DG_Transport.Items.Refresh();
        }

        private void btn_TransEdit_Click(object sender, RoutedEventArgs e)
        {
            Transport transport = (DG_Transport.SelectedItem as Transport);
            Containere containere = (DG_Container.SelectedItem as Containere);
            Plane plane = (DG_Plane.SelectedItem as Plane);
            if (plane != null && containere != null)
            {
                string Date = tb_TransDate.Text;

                Transport edittransport = new Transport(transport.Id,Date, plane, containere);

                func.UpdateTransport(edittransport);
                func.ReadTransport(planes,containeres,transports);
                RefreshGrid();
            }
        }

        private void DG_Transport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Transport transport = DG_Transport.SelectedItem as Transport;
                if (transport != null)
                {
                    tb_TransId.Text = Convert.ToString(transport.Id);
                    tb_TransDate.Text = transport.Date;
                    tb_TransFlightId.Text = Convert.ToString(transport.Plane.Id);
                    tb_TransContainerId.Text = Convert.ToString(transport.Containere.Id);
                }

            }
            catch (Exception)
            {
            }
        }
    }
}
