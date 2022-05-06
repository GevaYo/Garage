using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Electric : EnergySource
    {
        static Electric()
        {
            List<string> parameters = new List<string>();
            string className = typeof(Electric).Name;

            parameters.Add("What is your current energy percentage?");
            s_ListOfSpecificParamsToUser.Add(className, parameters);
        }

        public Electric(float i_MaxEnergyAmount)
            : base(i_MaxEnergyAmount) { }

        /*public override void AddEnergyToVehicle(float i_NumOfHoursToAdd)
        {

        }*/
    }
}
