using System;
using System.Collections.Generic;
using System.Linq;
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

        /*public static bool operator ==(CustomerTicket obj1, CustomerTicket obj2)
        {
            bool areEqual = ReferenceEquals(obj1, obj2);

            if (!areEqual)
            {
                areEqual = (ReferenceEquals(obj1, null) || ReferenceEquals(obj2, null)) ? false : obj1.Equals(obj2);
            }

            return areEqual;
        }

        public static bool operator !=(CustomerTicket obj1, CustomerTicket obj2)
        {
            return !(obj1 == obj2);
        }

        public override bool Equals(object obj)
        {
            bool equals = false;
            CustomerTicket compareTo = obj as CustomerTicket;

            if (compareTo != null)
            {
                equals = CustomerPhoneNumber == compareTo.CustomerPhoneNumber;
            }

            return equals;
        }

        public override int GetHashCode()
        {
            return CustomerPhoneNumber.GetHashCode();
        }*/
    }
}
