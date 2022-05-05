using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Fuel : EnergySource
    {
        private readonly eFuelType r_FuelType;

        public Fuel(float i_MaxEnergyAmount, eFuelType i_FuelType) : base(i_MaxEnergyAmount)
        {
            r_FuelType = i_FuelType;
        }

        public override void AddEnergyToVehicle(float i_NumOfLitersToAdd)   // list
        {
            // , eFuelType i_FuelType
            // i_FuelType == r_FuelType
            // if yes => add fuel
        }
    }
}
