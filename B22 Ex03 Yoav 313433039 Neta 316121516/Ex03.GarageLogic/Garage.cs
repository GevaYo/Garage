using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        //private static Dictionary<string, List<string>> s_ListOfSpecificParamsToUser;
        private Dictionary<string, CustomerTicket> m_VehiclesInGarage = new Dictionary<string, CustomerTicket>();
        private CustomerTicket m_CurrentlyTreatedCustomer = null;

        /*static Garage()
        {
            appendQuestionsDictionaryToContainerDictionary(EnergySource.ListOfSpecificParamsToUser());
            appendQuestionsDictionaryToContainerDictionary(Wheel.ListOfSpecificParamsToUser());
            appendQuestionsDictionaryToContainerDictionary(Vehicle.ListOfSpecificParamsToUser());
        }*/

        /*private static void appendQuestionsDictionaryToContainerDictionary(Dictionary<string, List<string>> i_DictToAppend)
        {
            foreach (KeyValuePair<string, List<string>> item in i_DictToAppend)
            {
                s_ListOfSpecificParamsToUser.Add(item.Key, item.Value);
            }
        }*/

        public bool IsVehiclesInGarage(string i_LicensePlate)
        {
            return m_VehiclesInGarage.ContainsKey(i_LicensePlate);
        }

        public void AddNewVehicleToGarage(int io_CustomerVehicleType, string io_CustomerName, string io_CustomerPhone)
        {
            eVehicleType customerVehicleType = (eVehicleType)io_CustomerVehicleType;
            Vehicle newVehicle = VehicleFactory.CreateNewVehicle(customerVehicleType);
            CustomerTicket newCustomer = new CustomerTicket(newVehicle, io_CustomerName, io_CustomerPhone);
            m_CurrentlyTreatedCustomer = newCustomer;
        }

        public Dictionary<string, Dictionary<int, string>> GetListOfQuestionsToInitiateVehicle()
        {
            Dictionary<string, Dictionary<int, string>> questionsToUser = new Dictionary<string, Dictionary<int, string>>();
            Dictionary<int, string> dictForType = new Dictionary<int, string>();
            EnergySource src = m_CurrentlyTreatedCustomer.Vehicle.EnergySource;
            Type srcType = src.GetType();
            List<string> questionsByType = src.ListOfSpecificParamsToUser(srcType.Name);
            int questionNumber = 3;

            dictForType.Add(1, "license_plate");
            dictForType.Add(2, "vehicle model");
            questionsToUser.Add("customerDetails", dictForType);
            dictForType.Clear();

            foreach (string question in questionsByType)
            {
                dictForType.Add(questionNumber, question);
                ++questionNumber;
            }

            questionsToUser.Add(srcType.Name, dictForType);

            return questionsToUser;
        }

        //private void addQuestionsToDictionaryCollection(Type )
        public void UpdateNewCustomerInQueue(Dictionary<string, Dictionary<int, string>> i_CustomerDetails)
        {
            // ... update details in vehicle
            Dictionary<int, string> customerDetails = i_CustomerDetails["customerDetails"];
            string licensePlate = customerDetails[1];
            string model = customerDetails[2];

            m_VehiclesInGarage.Add(licensePlate, m_CurrentlyTreatedCustomer);
            m_CurrentlyTreatedCustomer.Vehicle.LicensePlateId = licensePlate;
            m_CurrentlyTreatedCustomer.Vehicle.Model = model;
            m_CurrentlyTreatedCustomer.Vehicle.Model = model;
            Vehicle vehicle = m_CurrentlyTreatedCustomer.Vehicle;
            vehicle.UpdateVehicleParameters(i_CustomerDetails);


            // m_CurrentlyTreatedCustomer = null;
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

        public void ChangeVehicleStatus(string i_LicensePlate, eVehicleStatus i_NewStatus)
        {
            CustomerTicket customerTicket;

            m_VehiclesInGarage.TryGetValue(i_LicensePlate, out customerTicket);
            customerTicket.VehicleStatus = i_NewStatus;
        }

        public void InflateVehicleWheelsToMax(string i_LicensePlate)
        {
            List<Wheel> wheelsInVehicle = m_VehiclesInGarage[i_LicensePlate].Vehicle.WheelsInVehicle;

            foreach (Wheel wheel in wheelsInVehicle)
            {
                wheel.FillAirToMax();
            }
        }

        public void AddEnergyToVehicle(string i_LicensePlate, eFuelType i_FuelType, float i_AmountOfFuelToAdd)
        {
            EnergySource energySource = m_VehiclesInGarage[i_LicensePlate].Vehicle.EnergySource;
        }

        /*public void AddEnergyToElectricVehicle(string i_LicensePlate, float i_NumOfMinutesToCharge)
        {

        }*/

        public void ShowVehicleFullDetails(string i_LicensePlate)
        {

        }
    }
}
