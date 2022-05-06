using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Fuel : EnergySource
    {
        private enum eQuestionIndex
        {
            CURRENT_AMOUNT = 1,
            FUEL_TYPE,
            REFUEL_AMOUNT
        }

        private readonly eFuelType r_FuelType;

        static Fuel()
        {
            List<string> parameters = new List<string>();
            string className = typeof(Fuel).Name;

            parameters.Add("What is your current amount of fuel?");
            parameters.Add("What is your vehicle fuel type?");
            parameters.Add("How many liters would you like to add?");
            s_ListOfSpecificParamsToUser.Add(className, parameters);
        }

        public Fuel(float i_MaxEnergyAmount, eFuelType i_FuelType)
            : base(i_MaxEnergyAmount)
        {
            r_FuelType = i_FuelType;
        }

        public override void UpdateEnergyParameters(Dictionary<int, string> i_FuelDetails)
        {
            if (i_FuelDetails.ContainsKey((int)eQuestionIndex.FUEL_TYPE))
            {
                validateFuelType(i_FuelDetails[(int)eQuestionIndex.FUEL_TYPE]);
                validateAddedEnergyAmount(i_FuelDetails[(int)eQuestionIndex.REFUEL_AMOUNT]);
                ///
            }
            else
            {
                validateCurrentEnergyAmount(i_FuelDetails[(int)eQuestionIndex.CURRENT_AMOUNT]);

            }
        }

        private void validateFuelType(string i_FuelTypeToCheck)
        {
            eFuelType validFuelType;

            if (!Enum.TryParse<eFuelType>(i_FuelTypeToCheck, out validFuelType))
            {
                /// throw ...
            }
            else
            {
                if (!Enum.IsDefined(typeof(eFuelType), validFuelType))
                {
                    /// throw ...
                }
            }

            if(r_FuelType != validFuelType)
            {
                /// throw ...
            }
        }


        /* public override void AddEnergyToVehicle(float i_NumOfLitersToAdd)   // list
         {
             // , eFuelType i_FuelType
             // i_FuelType == r_FuelType
             // if yes => add fuel
         }*/
    }
}
