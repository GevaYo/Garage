using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Electric : EnergySource
    {
        public Electric(float i_MaxEnergyAmount)
            : base(i_MaxEnergyAmount) { }

        /*public override void AddEnergyToVehicle(float i_NumOfHoursToAdd)
        {

        }*/
    }
}
