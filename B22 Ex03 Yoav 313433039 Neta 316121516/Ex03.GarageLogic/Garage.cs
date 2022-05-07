using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool IsVehiclesInGarage(string i_LicensePlate)
        {
            bool isVehicleInGarage = m_VehiclesInGarage.ContainsKey(i_LicensePlate);
            m_CurrentlyTreatedCustomer = isVehicleInGarage ? m_VehiclesInGarage[i_LicensePlate] : null;

            return isVehicleInGarage;
        }

        public void AddNewVehicleToGarage(int io_CustomerVehicleType, string io_LicensePlate, string io_VehicleModel, string io_CustomerName, string io_CustomerPhone)
        {
            eVehicleType customerVehicleType = (eVehicleType)io_CustomerVehicleType;
            Vehicle newVehicle = VehicleFactory.CreateNewVehicle(customerVehicleType, io_LicensePlate, io_VehicleModel);
            CustomerTicket newCustomer = new CustomerTicket(newVehicle, io_CustomerName, io_CustomerPhone);
            m_CurrentlyTreatedCustomer = newCustomer;
            m_VehiclesInGarage.Add(io_LicensePlate, newCustomer);
        }

        public Dictionary<string, Dictionary<int, string>> GetListOfQuestionsToInitiateVehicle()
        {
            Dictionary<string, Dictionary<int, string>> questionsToUser = new Dictionary<string, Dictionary<int, string>>();

            Vehicle vehicle = m_CurrentlyTreatedCustomer.Vehicle;
            Type srcType = vehicle.GetType();
            Dictionary<int, string> questionsByType = vehicle.DictionaryOfSpecificParamsToUser(srcType.Name);
            questionsToUser.Add(srcType.Name, questionsByType);

            EnergySource energySource = m_CurrentlyTreatedCustomer.Vehicle.EnergySource;
            srcType = energySource.GetType();
            questionsByType = energySource.DictionaryOfSpecificParamsToUser(srcType.Name);
            questionsToUser.Add(srcType.Name, questionsByType);

            Wheel wheel = m_CurrentlyTreatedCustomer.Vehicle.WheelsInVehicle[0];
            srcType = wheel.GetType();
            questionsByType = wheel.DictionaryOfSpecificParamsToUser(srcType.Name);
            questionsToUser.Add(srcType.Name, questionsByType);

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

        /*
         * 1. license plate
         * 2. model
         * 3. customer name
         * 4. customer phone
         */
        public string ValidateInputWithoutDependencyInAnObject(string io_InputStrFromUser, int io_QuestionNumber)
        {
            if (io_QuestionNumber == 1)
            {
                validatedLicensePlate(io_InputStrFromUser);
            }
            else if (io_QuestionNumber == 2)
            {
                validatedModel(io_InputStrFromUser);
            }
            else if (io_QuestionNumber == 3)
            {
                validateCustomerName(io_InputStrFromUser);
            }
            else
            {
                validateCustomerPhone(io_InputStrFromUser);
            }

            return io_InputStrFromUser;
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

            return response;
        }

        public void ChangeVehicleStatus(string io_LicensePlate, eVehicleStatus i_NewStatus)
        {
            if (!IsVehiclesInGarage(io_LicensePlate))
            {
                throw new ArgumentException("The vehicle is not in the garage");
            }
            else
            {
                m_CurrentlyTreatedCustomer.VehicleStatus = i_NewStatus;
            }
        }

        public void InflateVehicleWheelsToMax(string io_LicensePlate)
        {
            if (!IsVehiclesInGarage(io_LicensePlate))
            {
                throw new ArgumentException("The vehicle is not in the garage");
            }
            else
            {
                m_CurrentlyTreatedCustomer.Vehicle.InflateAllWheelsToMax();
            }
        }

        public void AddEnergyToVehicle(string io_LicensePlate, params string[] io_ParamsToUpdate)
        {
            if (!IsVehiclesInGarage(io_LicensePlate))
            {
                throw new ArgumentException("The vehicle is not in the garage");
            }
            else
            {
                if(m_CurrentlyTreatedCustomer.Vehicle.EnergySource is Electric && io_ParamsToUpdate.Length == 1)
                {
                    chargeElectricVehicle(io_ParamsToUpdate[0]);
                }
                else if(m_CurrentlyTreatedCustomer.Vehicle.EnergySource is Fuel && io_ParamsToUpdate.Length == 2)
                {
                    refuelVehicle(io_ParamsToUpdate[0], io_ParamsToUpdate[1]);
                }
                else
                {
                    throw new ArgumentException("Your energy source is not compatible to the action you've tried to execute");
                }
            }
        }

        private void chargeElectricVehicle(string io_MinutesToAdd)
        {
            EnergySource energySource = m_CurrentlyTreatedCustomer.Vehicle.EnergySource;
            energySource.UpdateEnergyParameters(io_MinutesToAdd, (int)Electric.eQuestionIndex.RECHARGE_AMOUNT);
        }

        private void refuelVehicle(string io_RefuelAmount, string io_FuelType)
        {
            EnergySource energySource = m_CurrentlyTreatedCustomer.Vehicle.EnergySource;
            energySource.UpdateEnergyParameters(io_FuelType, (int)Fuel.eQuestionIndex.FUEL_TYPE);
            energySource.UpdateEnergyParameters(io_RefuelAmount, (int)Fuel.eQuestionIndex.REFUEL_AMOUNT);
        }

        public StringBuilder ShowVehicleFullDetails(string io_LicensePlate)
        {
            StringBuilder vehicleInfo = new StringBuilder();

            if (!IsVehiclesInGarage(io_LicensePlate))
            {
                throw new ArgumentException("The vehicle is not in the garage");
            }
            else
            {
                vehicleInfo = m_CurrentlyTreatedCustomer.VehicleFullDetails();
            }

            return vehicleInfo;
        }

        private void validatedLicensePlate(string i_LicensePlate)
        {
            if (!i_LicensePlate.All(char.IsDigit))
            {
                throw new ArgumentException("The license plate must contain only digits");
            }
        }

        private void validatedModel(string i_Model)
        {
            if (!i_Model.All(char.IsLetterOrDigit))
            {
                throw new ArgumentException("The model must contain letters or digits");
            }
        }

        private void validateCustomerName(string i_NameToCheck)
        {
            if (!i_NameToCheck.All(char.IsLetter))
            {
                throw new ArgumentException("The name must contain only letters");
            }
        }

        private void validateCustomerPhone(string i_PhoneToCheck)
        {
            if (!i_PhoneToCheck.All(char.IsDigit) || i_PhoneToCheck.Length != 10)
            {
                throw new ArgumentException("The phone number must contain only digits not longer than 10 characters");
            }
        }
    }
}
