using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class CustomerTicket
    {
        string m_CustomerName;
        string m_CustomerPhoneNumber;
        eVehicleStatus m_VehicleStatus;
        Vehicle m_Vehicle;

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
