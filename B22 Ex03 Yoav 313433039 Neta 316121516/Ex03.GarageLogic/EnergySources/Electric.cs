using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Electric : EnergySource
    {
        public enum eQuestionIndex
        {
            CURRENT_AMOUNT = 1,
            RECHARGE_AMOUNT,
        }

        static Electric()
        {
            Dictionary<int, string> newParameters = new Dictionary<int, string>();
            string className = typeof(Electric).Name;

            newParameters.Add((int)eQuestionIndex.CURRENT_AMOUNT, "What is your current energy percentage (in hours)?");
            s_SpecificParamsToUser.Add(className, newParameters);
        }

        public Electric(float i_MaxEnergyAmount)
            : base(i_MaxEnergyAmount)
        {
        }

        public override void UpdateEnergyParameters(string io_Response, int i_ElectricQuestion)
        {
            float vaildAmount = validateParseToFloat(io_Response);

            if (i_ElectricQuestion == (int)eQuestionIndex.RECHARGE_AMOUNT)
            {
                vaildAmount /= 60;
                validateAddedEnergyAmount(vaildAmount, (r_MaxEnergyAmount - CurrentEnergyAmount) * 60);
            }
            else
            {
                validateCurrentEnergyAmount(vaildAmount);
            }
        }
    }
}
