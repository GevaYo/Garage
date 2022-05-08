using System;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class CustomerTicket
    {
        private string m_CustomerName;
        private string m_CustomerPhoneNumber;
        private eVehicleStatus m_VehicleStatus = eVehicleStatus.IN_REPAIR;
        private Vehicle m_Vehicle;

        public CustomerTicket(Vehicle i_Vehicle, string i_CustomerName, string i_CustomerPhone)
        {
            m_CustomerName = i_CustomerName;
            m_CustomerPhoneNumber = i_CustomerPhone;
            m_Vehicle = i_Vehicle;
        }

        public string CustomerName
        {
            get
            {
                return m_CustomerName;
            }

            set
            {
                m_CustomerName = value;
            }
        }

        public string CustomerPhoneNumber
        {
            get
            {
                return m_CustomerPhoneNumber;
            }

            set
            {
                m_CustomerPhoneNumber = value;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }

            set
            {
                m_Vehicle = value;
            }
        }

        public StringBuilder VehicleFullDetails()
        {
            StringBuilder vehicleInfo = new StringBuilder();

            vehicleInfo.AppendFormat("License plate: {0}{1}", Vehicle.LicensePlateId, Environment.NewLine);
            vehicleInfo.AppendFormat("Model: {0}{1}", Vehicle.Model, Environment.NewLine);
            vehicleInfo.AppendFormat("Customer name: {0}{1}", CustomerName, Environment.NewLine);
            vehicleInfo.AppendFormat("Customer phone number: {0}{1}", CustomerPhoneNumber, Environment.NewLine);
            vehicleInfo.AppendFormat("Vehicle status: {0}{1}", VehicleStatus, Environment.NewLine);
            vehicleInfo.Append(Vehicle.GetWheelInfo());
            vehicleInfo.Append(Vehicle.EnergySource.GetEnergySourceInfo());

            return vehicleInfo;
        }
    }
}
