using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        //private readonly List<string> r_VehicleTypeParsedFromEnum = new List<string>();
        private Dictionary<string, CustomerTicket> m_VehiclesInGarage = new Dictionary<string, CustomerTicket>();
        private CustomerTicket m_NewCustomerInQueue = null;

        /*public Garage()
        {
            foreach(string vehicleType in Enum.GetNames(typeof(eVehicleType)))
            {
                r_VehicleTypeParsedFromEnum.Add(vehicleType.Replace("_", " ").ToLower());
            }
        }*/

        /*public List<string> VehicleTypeParsedFromEnum
        {
            get
            {
                return r_VehicleTypeParsedFromEnum;
            }
        }*/

        public bool IsVehiclesInGarage(string i_LicensePlate)
        {
            return m_VehiclesInGarage.ContainsKey(i_LicensePlate);
        }

        public void AddNewVehicleToGarage(int io_CustomerVehicleType, string io_CustomerName, string io_CustomerPhone)
        {
            eVehicleType customerVehicleType = (eVehicleType)io_CustomerVehicleType;
            Vehicle newVehicle = VehicleFactory.CreateNewVehicle(customerVehicleType);
            CustomerTicket newCustomer = new CustomerTicket(newVehicle, io_CustomerName, io_CustomerPhone);
            m_NewCustomerInQueue = newCustomer;
        }

        public void UpdateNewCustomerInQueue(Dictionary<string, string> i_CustomerDetails)
        {
            // ... update details in vehicle
            string licensePlate = i_CustomerDetails["license_plate"];
            m_VehiclesInGarage.Add(licensePlate, m_NewCustomerInQueue);
            m_NewCustomerInQueue.Vehicle.LicensePlateId = licensePlate;
            // m_NewCustomerInQueue = null ?
        }

        public StringBuilder ShowLicensePlateList()
        {
            StringBuilder response = new StringBuilder();
            int indexInList = 1;

            foreach(string licensePlate in m_VehiclesInGarage.Keys)
            {
                response.AppendFormat("{0}. {1}{2}", indexInList, licensePlate, Environment.NewLine);
                ++indexInList;
            }

            return response;
        }

        public StringBuilder ShowLicensePlateList(eVehicleStatus i_FilterByStatus)
        {
            StringBuilder response = new StringBuilder();
            int counter = 1;

            foreach (KeyValuePair<string, CustomerTicket> item in m_VehiclesInGarage)
            {
                if(item.Value.VehicleStatus == i_FilterByStatus)
                {
                    response.AppendFormat("{0}. {1}{2}", counter, item.Key, Environment.NewLine);
                    ++counter;
                }
            }

            return response;
        }

        public void ChangeVehicleStatus(string i_LicensePlate, eVehicleStatus i_NewStatus)
        {
            CustomerTicket customerTicket;

            m_VehiclesInGarage.TryGetValue(i_LicensePlate, out customerTicket);
            customerTicket.VehicleStatus = i_NewStatus;
        }

        public void InflateVehicleWheelsToMax(string i_LicensePlate)
        {
            List<Wheel> wheelsInVehicle = m_VehiclesInGarage[i_LicensePlate].Vehicle.WheelsInVehicle;

            foreach(Wheel wheel in wheelsInVehicle)
            {
                wheel.FillAirToMax();
            }
        }

        public void AddEnergyToVehicle(string i_LicensePlate, eFuelType i_FuelType, float i_AmountOfFuelToAdd)
        {
            EnergySource energySource = m_VehiclesInGarage[i_LicensePlate].Vehicle.EnergySource;
        }

        /*public void AddEnergyToElectricVehicle(string i_LicensePlate, float i_NumOfMinutesToCharge)
        {

        }*/

        public void ShowVehicleFullDetails(string i_LicensePlate)
        {

        }
    }
}
