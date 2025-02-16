﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class Vehicle
    {
        protected static Dictionary<string, Dictionary<int, string>> s_SpecificParamsToUser = new Dictionary<string, Dictionary<int, string>>();
        protected readonly List<Wheel> r_WheelsInVehicle;
        protected readonly string r_Model;
        protected readonly string r_LicensePlateId;
        protected EnergySource m_EnergySource;

        protected Vehicle(int i_NumOfWheels, float io_MaxAirPressure, string i_LicensePlate, string i_VehicleModel)
        {
            r_LicensePlateId = i_LicensePlate;
            r_Model = i_VehicleModel;
            r_WheelsInVehicle = new List<Wheel>(i_NumOfWheels);
            for (int i = 0; i < i_NumOfWheels; ++i)
            {
                r_WheelsInVehicle.Add(new Wheel(io_MaxAirPressure));
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
                return r_LicensePlateId;
            }
        }

        public string Model
        {
            get
            {
                return r_Model;
            }
        }

        public Dictionary<int, string> DictionaryOfSpecificParamsToUser(string i_Key)
        {
            return s_SpecificParamsToUser[i_Key];
        }

        public abstract void UpdateVehicleParameters(string io_Response, int i_VehicleQuestion);

        public void UpdateWheelsParametersInVehicle(string io_Response, int io_WheelQuestion)
        {
            foreach (Wheel wheel in r_WheelsInVehicle)
            {
                wheel.UpdateWheelParameters(io_Response, io_WheelQuestion);
            }
        }

        public void InflateAllWheelsToMax()
        {
            foreach (Wheel wheel in WheelsInVehicle)
            {
                wheel.FillAirToMax();
            }
        }

        public StringBuilder GetWheelInfo()
        {
            StringBuilder info = new StringBuilder();
            Wheel wheel = r_WheelsInVehicle[0];

            info.AppendFormat("Wheel manufacturer name: {0}{1}", wheel.ManufacturerName, Environment.NewLine);
            info.AppendFormat("Wheel current air pressure: {0}{1}", wheel.CurrentAirPressure, Environment.NewLine);

            return info;
        }

        public abstract StringBuilder GetVehicleInfo();
    }
}
