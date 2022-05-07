﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class EnergySource
    {
        protected static Dictionary<string, Dictionary<int, string>> s_SpecificParamsToUser = new Dictionary<string, Dictionary<int, string>>();
        protected readonly float r_MaxEnergyAmount;
        protected float m_CurrentEnergyAmount = 0;

        protected EnergySource(float i_MaxEnergyAmount)
        {
            r_MaxEnergyAmount = i_MaxEnergyAmount;
        }

        public float CurrentEnergyAmount
        {
            get
            {
                return m_CurrentEnergyAmount;
            }
        }

        public virtual StringBuilder GetEnergySourceInfo()
        {
            StringBuilder info = new StringBuilder();

            info.AppendFormat("Max energy amount: {0}{1}", r_MaxEnergyAmount, Environment.NewLine);
            info.AppendFormat("Current energy amount: {0}{1}", m_CurrentEnergyAmount, Environment.NewLine);

            return info;
        }

        public Dictionary<int, string> DictionaryOfSpecificParamsToUser(string i_Key)
        {
            return s_SpecificParamsToUser[i_Key];
        }

        public abstract void UpdateEnergyParameters(string io_Response, int i_EnergySourceQuestion);

        protected void validateCurrentEnergyAmount(string i_CurrentEnergyAmount)
        {
            float validAmount;

            if(!float.TryParse(i_CurrentEnergyAmount, out validAmount))
            {
                throw new FormatException("Please enter a float value");
            }
            else
            {
                if (validAmount < 0 || validAmount > r_MaxEnergyAmount)
                {
                    throw new ValueOutOfRangeException(0, r_MaxEnergyAmount);
                }
            }

            m_CurrentEnergyAmount = validAmount;
        }
    }
}
