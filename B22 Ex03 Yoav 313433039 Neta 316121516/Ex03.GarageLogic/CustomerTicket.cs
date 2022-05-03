using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class CustomerTicket
    {
        private string m_CustomerName;
        private string m_CustomerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;
        private Vehicle m_Vehicle;

        /*
         // getHashCode --> by r_LicensePlateId
        // need to override == and !=
        public override bool Equals(object obj)
        {
            return Equals(obj as Vehicle);
        }

        public bool Equals(ImaginaryNumber other)
        {
            return other != null &&
                   RealNumber == other.RealNumber &&
                   ImaginaryUnit == other.ImaginaryUnit;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RealNumber, ImaginaryUnit);
        }
         */
    }
}
