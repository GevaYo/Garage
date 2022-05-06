using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class Vehicle
    {
        protected static Dictionary<string, List<string>> s_ListOfSpecificParamsToUser = new Dictionary<string, List<string>>();
        protected readonly List<Wheel> r_WheelsInVehicle;
        protected EnergySource m_EnergySource;
        protected string m_Model;
        protected string m_LicensePlateId;
        protected float m_EnergySourcePercentage;

        protected Vehicle(int i_NumOfWheels, float i_MaxAirPressure)
        {
            r_WheelsInVehicle = new List<Wheel>(i_NumOfWheels);
            for (int i = 0; i < r_WheelsInVehicle.Count; ++i)
            {
                r_WheelsInVehicle.Add(new Wheel(i_MaxAirPressure));
            }
        }

        public List<Wheel> WheelsInVehicle
        {
            get
            {
                return r_WheelsInVehicle;
            }
        }

        public EnergySource EnergySource
        {
            get
            {
                return m_EnergySource;
            }
        }

        public string LicensePlateId
        {
            get
            {
                return m_LicensePlateId;
            }

            set
            {
                m_LicensePlateId = value;
            }
        }

        public string Model
        {
            get
            {
                return m_Model;
            }

            set
            {
                m_Model = value;
            }
        }

        public virtual void UpdateVehicleParameters(Dictionary<string, Dictionary<int, string>> i_CustomerDetails)
        {
            Dictionary<int, string> energySourceDetails = i_CustomerDetails[EnergySource.GetType().Name];
            Dictionary<int, string> wheelDetails = i_CustomerDetails[WheelsInVehicle[0].GetType().Name];


        }

        private void updateWheelsParameters()
        {

        }


        /*public static bool operator ==(Vehicle obj1, Vehicle obj2)
        {
            bool areEqual = ReferenceEquals(obj1, obj2);

            if (!areEqual)
            {
                areEqual = (ReferenceEquals(obj1, null) || ReferenceEquals(obj2, null)) ? false : obj1.Equals(obj2);
            }

            return areEqual;
        }

        public static bool operator !=(Vehicle obj1, Vehicle obj2)
        {
            return !(obj1 == obj2);
        }

        public override bool Equals(object obj)
        {
            bool equals = false;
            Vehicle compareTo = obj as Vehicle;

            if(compareTo != null)
            {
                equals = LicensePlateId == compareTo.LicensePlateId;
            }

            return equals;
        }

        public override int GetHashCode()
        {
            return LicensePlateId.GetHashCode();
        }*/
    }
}
