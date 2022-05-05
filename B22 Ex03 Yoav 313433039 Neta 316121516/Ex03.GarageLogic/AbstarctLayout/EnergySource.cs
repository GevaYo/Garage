using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class EnergySource
    {
        protected float m_CurrentEnergyAmount = 0;
        protected readonly float r_MaxEnergyAmount;

        protected EnergySource(float i_MaxEnergyAmount)
        {
            r_MaxEnergyAmount = i_MaxEnergyAmount;
        }

        /*public float MaxEnergyAmount
        {
            get
            {
                return m_MaxEnergyAmount;
            }

            set
            {
                m_MaxEnergyAmount = value;
            }
        }*/

        public virtual void AddEnergyToVehicle(float i_EnergyToAdd) // list
        {
            m_CurrentEnergyAmount += i_EnergyToAdd;
        }
    }
}
