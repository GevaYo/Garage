using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        Dictionary<string, CustomerTicket> m_VehiclesInGarage;

        public bool IsVehiclesInGarage(string i_LicensePlate)
        {
            return m_VehiclesInGarage.ContainsKey(i_LicensePlate);
        }

        public void AddVehicleToGarage()
        {

        }

		public void ShowLicensePlateList()
        {

        }

		public void ShowLicensePlateList(eVehicleStatus i_FilterByStatus)
        {

        }

		public void ChangeVehicleStatus(string i_LicensePlate, eVehicleStatus i_NewStatus)
        {

        }

		public void FillVehicleWheelsToMax()
        {

        }

		public void AddFuelToVehicle(string i_LicensePlate, eFuelType i_FuelType, float i_AmountOfFuelToAdd)
        {

        }

		public void AddEnergyToElectricVehicle(string i_LicensePlate, float i_NumOfMinutesToCharge)
        {

        }

		public void ShowVehicleFullDetails(string i_LicensePlate)
        {

        }
	}
}
