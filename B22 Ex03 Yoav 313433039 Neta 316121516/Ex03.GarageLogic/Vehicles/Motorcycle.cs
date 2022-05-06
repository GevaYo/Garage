using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        private enum eLicenseType
        {
            A = 1,
            A1,
            B1,
            BB
        }

        private enum eQuestionIndex
        {
            LICENSE_TYPE = 1,
            ENGINE_VOLUME
        }

        private int m_EngineVolume;
        private eLicenseType m_LicenseType;
        // TODO: private: static readonly / const for "2" (num of wheels), "31" max air pr..

        static Motorcycle()
        {
            List<string> parameters = new List<string>();
            string className = typeof(Motorcycle).Name;

            parameters.Add("What is your license type? (1- A ,2- A1 ,3- B1 ,4- BB)");
            parameters.Add("What is your engine volume?");
            s_ListOfSpecificParamsToUser.Add(className, parameters);
        }

        public Motorcycle(EnergySource i_EnergySource)
            : base(2, 31)
        {
            m_EnergySource = i_EnergySource;
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }
        }

        public string LicenseType
        {
            get
            {
                return m_LicenseType.ToString();
            }
        }

        public override void UpdateVehicleParameters(Dictionary<string, Dictionary<int, string>> i_CustomerDetails)
        {
            Dictionary<int, string> vehicleDetails = i_CustomerDetails[this.GetType().Name];

            validateLicenseType(vehicleDetails[(int)eQuestionIndex.LICENSE_TYPE]);
            validateEngineVolume(vehicleDetails[(int)eQuestionIndex.ENGINE_VOLUME]);

            base.UpdateVehicleParameters(i_CustomerDetails);

        }

        private void validateLicenseType(string i_LicenseTypeToCheck)
        {
            eLicenseType validLicenseType;

            if (!Enum.TryParse<eLicenseType>(i_LicenseTypeToCheck, out validLicenseType))
            {
                /// throw ...
            }
            else
            {
                if (!Enum.IsDefined(typeof(eLicenseType), validLicenseType))
                {
                    /// throw ...
                }
            }

            m_LicenseType = validLicenseType;
        }

        private void validateEngineVolume(string i_EngineVolumeToCheck)
        {
            int validEngineVolume;

            if (!int.TryParse(i_EngineVolumeToCheck, out validEngineVolume))
            {
                /// throw ...
            }

            m_EngineVolume = validEngineVolume;
        }
    }
}
