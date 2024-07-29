using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, CustomerTicket> m_VehiclesInGarage = new Dictionary<string, CustomerTicket>();
        private CustomerTicket m_CurrentlyTreatedCustomer = null;

        public bool IsEmptyGarage()
        {
            return m_VehiclesInGarage.Count == 0;
        }

        public bool IsVehicleInGarage(string i_LicensePlate)
        {
            bool isVehicleInGarage = m_VehiclesInGarage.ContainsKey(i_LicensePlate);

            m_CurrentlyTreatedCustomer = isVehicleInGarage ? m_VehiclesInGarage[i_LicensePlate] : null;

            return isVehicleInGarage;
        }

        public void AddNewVehicleToGarage(int i_CustomerVehicleType, string io_LicensePlate, string io_VehicleModel, string io_CustomerName, string io_CustomerPhone)
        {
            eVehicleType customerVehicleType = (eVehicleType)i_CustomerVehicleType;
            Vehicle newVehicle = VehicleFactory.CreateNewVehicle(customerVehicleType, io_LicensePlate, io_VehicleModel);
            CustomerTicket newCustomer = new CustomerTicket(newVehicle, io_CustomerName, io_CustomerPhone);

            m_CurrentlyTreatedCustomer = newCustomer;
            m_VehiclesInGarage.Add(io_LicensePlate, newCustomer);
        }

        public Dictionary<string, Dictionary<int, string>> GetListOfQuestionsToInitiateVehicle()
        {
            Dictionary<string, Dictionary<int, string>> questionsToUser = new Dictionary<string, Dictionary<int, string>>();
            Vehicle vehicle = m_CurrentlyTreatedCustomer.Vehicle;
            EnergySource energySource = m_CurrentlyTreatedCustomer.Vehicle.EnergySource;
            Wheel wheel = m_CurrentlyTreatedCustomer.Vehicle.WheelsInVehicle[0];

            string srcType = vehicle.GetType().Name;
            Dictionary<int, string> questionsByType = vehicle.DictionaryOfSpecificParamsToUser(srcType);
            questionsToUser.Add(srcType, questionsByType);

            srcType = energySource.GetType().Name;
            questionsByType = energySource.DictionaryOfSpecificParamsToUser(srcType);
            questionsToUser.Add(srcType, questionsByType);

            srcType = wheel.GetType().Name;
            questionsByType = wheel.SpecificParamsToUser;
            questionsToUser.Add(srcType, questionsByType);

            return questionsToUser;
        }

        public void ValidateInputInNewVehicle(string io_InputStrFromUser, string i_ClassName, int io_QuestionNumber)
        {
            Vehicle currentVehicle = m_CurrentlyTreatedCustomer.Vehicle;

            if (i_ClassName == currentVehicle.GetType().Name)
            {
                currentVehicle.UpdateVehicleParameters(io_InputStrFromUser, io_QuestionNumber);
            }
            else if (i_ClassName == currentVehicle.EnergySource.GetType().Name)
            {
                currentVehicle.EnergySource.UpdateEnergyParameters(io_InputStrFromUser, io_QuestionNumber);
            }
            else
            {
                currentVehicle.UpdateWheelsParametersInVehicle(io_InputStrFromUser, io_QuestionNumber);
            }
        }

        public StringBuilder ShowLicensePlateList()
        {
            StringBuilder response = new StringBuilder();
            int indexInList = 1;

            foreach (string licensePlate in m_VehiclesInGarage.Keys)
            {
                response.AppendFormat("{0}. {1}{2}", indexInList, licensePlate, Environment.NewLine);
                ++indexInList;
            }

            return response;
        }

        public StringBuilder ShowLicensePlateList(eVehicleStatus i_FilterByStatus)
        {
            StringBuilder response = new StringBuilder();
            int counter = 1;

            foreach (KeyValuePair<string, CustomerTicket> item in m_VehiclesInGarage)
            {
                if (item.Value.VehicleStatus == i_FilterByStatus)
                {
                    response.AppendFormat("{0}. {1}{2}", counter, item.Key, Environment.NewLine);
                    ++counter;
                }
            }

            if(response.Length == 0)
            {
                response.Append("The list is empty");
            }

            return response;
        }

        public void ChangeVehicleStatus(eVehicleStatus i_NewStatus)
        {
            m_CurrentlyTreatedCustomer.VehicleStatus = i_NewStatus;
        }

        public void InflateVehicleWheelsToMax()
        {
            m_CurrentlyTreatedCustomer.Vehicle.InflateAllWheelsToMax();
        }

        public bool isElectricVehicle()
        {
            return m_CurrentlyTreatedCustomer.Vehicle.EnergySource is Electric;
        }

        public void ValidateRefuelType(string io_FuelType)
        {
            EnergySource energySource = m_CurrentlyTreatedCustomer.Vehicle.EnergySource;

            energySource.UpdateEnergyParameters(io_FuelType, (int)Fuel.eQuestionIndex.FUEL_TYPE);
        }

        public void AddEnergyToVehicle(string io_AmountToAdd)
        {
            EnergySource energySource = m_CurrentlyTreatedCustomer.Vehicle.EnergySource;
            int energyIndex = energySource is Electric ? (int)Electric.eQuestionIndex.RECHARGE_AMOUNT : (int)Fuel.eQuestionIndex.REFUEL_AMOUNT;

            energySource.UpdateEnergyParameters(io_AmountToAdd, energyIndex);
        }

        public StringBuilder ShowVehicleFullDetails()
        {
            StringBuilder vehicleInfo = m_CurrentlyTreatedCustomer.VehicleFullDetails();

            return vehicleInfo;
        }
    }
}
