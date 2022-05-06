using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class EnergySource
    {
        protected static Dictionary<string, List<string>> s_ListOfSpecificParamsToUser = new Dictionary<string, List<string>>();
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

        public List<string> ListOfSpecificParamsToUser(string i_Key)
        {
            return s_ListOfSpecificParamsToUser[i_Key];
        }

        public abstract void UpdateEnergyParameters(Dictionary<int, string> i_EnergySourceDetails);

        protected void validateCurrentEnergyAmount(string i_CurrentEnergyAmount)
        {
            float validAmount;
            if(!float.TryParse(i_CurrentEnergyAmount, out validAmount))
            {
                /// throw...
            }

            m_CurrentEnergyAmount = validAmount;
        }

        protected void validateAddedEnergyAmount(string i_AddedAmountToCheck)
        {
            float validAmount;

            if (!float.TryParse(i_AddedAmountToCheck, out validAmount))
            {
                /// throw...
            }
            else
            {
                if (CurrentEnergyAmount + validAmount > r_MaxEnergyAmount)
                {
                    /// throw...
                }
            }

            m_CurrentEnergyAmount += validAmount;
        }

        /* public virtual void AddEnergyToVehicle(float i_EnergyToAdd) // list
         {
             m_CurrentEnergyAmount += i_EnergyToAdd;
         }*/
    }
}
