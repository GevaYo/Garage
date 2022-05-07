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
            Dictionary<int, string> parameters = new Dictionary<int, string>();
            string className = typeof(Motorcycle).Name;

            parameters.Add((int)eQuestionIndex.LICENSE_TYPE, "What is your license type? (1- A ,2- A1 ,3- B1 ,4- BB)");
            parameters.Add((int)eQuestionIndex.ENGINE_VOLUME, "What is your engine volume?");
            s_SpecificParamsToUser.Add(className, parameters);
        }

        public Motorcycle(EnergySource i_EnergySource, string io_LicensePlate, string io_VehicleModel)
            : base(2, 31, io_LicensePlate, io_VehicleModel)
        {
            m_EnergySource = i_EnergySource;
        }

        /*public int EngineVolume
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
        }*/

        public override StringBuilder GetVehicleInfo()
        {
            StringBuilder info = new StringBuilder();

            info.AppendFormat("Engine volume: {0}{1}", m_EngineVolume, Environment.NewLine);
            info.AppendFormat("License type: {0}{1}", m_LicenseType, Environment.NewLine);

            return info;
        }

        public override void UpdateVehicleParameters(string io_Response, int i_MotorcycleQuestion)
        {
            if (i_MotorcycleQuestion == (int)eQuestionIndex.LICENSE_TYPE)
            {
                validateLicenseType(io_Response);
            }
            else
            {
                validateEngineVolume(io_Response);
            }
        }

        private void validateLicenseType(string i_LicenseTypeToCheck)
        {
            int validLicenseType;

            if (!int.TryParse(i_LicenseTypeToCheck, out validLicenseType))
            {
                throw new FormatException("Please enter an integer");
            }
            else
            {
                if (!Enum.IsDefined(typeof(eLicenseType), validLicenseType))
                {
                    throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eLicenseType)).Length);
                }
            }

            m_LicenseType = (eLicenseType)validLicenseType;
        }

        private void validateEngineVolume(string i_EngineVolumeToCheck)
        {
            int validEngineVolume;

            if (!int.TryParse(i_EngineVolumeToCheck, out validEngineVolume))
            {
                throw new FormatException("Please enter an integer");
            }

            m_EngineVolume = validEngineVolume;
        }
    }
}
