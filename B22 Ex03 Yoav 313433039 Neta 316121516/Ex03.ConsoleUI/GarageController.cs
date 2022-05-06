using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageController
    {
        private const int k_NumOfMenuChoices = 8;
        private readonly Garage r_Garage = new Garage();
        private readonly StringBuilder r_TypesOfVehicles;
        private readonly StringBuilder r_PossibleVehicleStatuses;
        private readonly StringBuilder r_PossibleFuelTypes;
        private readonly StringBuilder r_MainMenu;
        private readonly int r_NumOfVehiclesTypes;
        private readonly int r_NumOfVehicleStatuses;
        private readonly int r_NumOfFuelTypes;
;

        public GarageController()
        {
            r_TypesOfVehicles = generateAvailableEnumsToMessage(typeof(eVehicleType), out r_NumOfVehiclesTypes);
            r_PossibleVehicleStatuses = generateAvailableEnumsToMessage(typeof(eVehicleStatus), out r_NumOfVehicleStatuses);
            r_PossibleFuelTypes = generateAvailableEnumsToMessage(typeof(eFuelType), out r_NumOfFuelTypes);
            r_MainMenu = generateMenu();
        }

        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine(r_MainMenu);
                int menuChoice = getUserInputFromMenu(k_NumOfMenuChoices);
                executeMenuAction(menuChoice);
            }
        }

        private void addNewVehicleToGarage()
        {
            // 1. get license plate
            string licensePlate = Console.ReadLine();

            if (r_Garage.IsVehiclesInGarage(licensePlate))
            {
                r_Garage.ChangeVehicleStatus(licensePlate, eVehicleStatus.IN_REPAIR);
                Console.WriteLine("The vehicle is already in the garage");
            }
            else
            {
                Console.WriteLine(r_TypesOfVehicles);
                int vehicleType = getUserInputFromMenu(r_NumOfVehiclesTypes);
                string customerName, customerPhone;

                getCustomerDetails(out customerName, out customerPhone);
                r_Garage.AddNewVehicleToGarage(vehicleType, customerName, customerPhone);
                Dictionary<string, Dictionary<int, string>> detailsToAskUser = r_Garage.GetListOfQuestionsToInitiateVehicle();

                /*
                 * 
                 * -- get customer name + phone number
                 * 1. print vehicle options
                 * 2. get user choice, returned as enum eVehicleType
                 * 3. get from garage List<eSpecificVehicleCharacteristics>
                 * 4. run in loop over the list
                 * 5. insert input to List<string> specificVehicleCharacteristics
                 * 6. send params to garage (garage create customerTicket & vehicle)
                 * 7. 
                 */
            }
        }

        private void getCustomerDetails(out string o_CustomerName, out string o_CustomerPhone)
        {
            // get name + phone number
            o_CustomerName = string.Empty;
            o_CustomerPhone = string.Empty;
        }

        private void showVehicleLicensePlateFullList()
        {

        }

        private void showFillteredVehicleLicensePlateList()
        {

        }

        private void changeVehicleStatusInGarage()
        {

        }

        private void inflateWheelsToMaxCapacity()
        {

        }

        private void fuelPoweredVehicleRefueling()
        {

        }

        private void electricPoweredVehicleCharge()
        {

        }

        private void showVehicleInfo()
        {

        }

        private void executeMenuAction(int i_MenuChoice)
        {
            eMenuOptions choiceToEnum = (eMenuOptions)i_MenuChoice;

            switch (choiceToEnum)
            {
                case eMenuOptions.ADD_NEW_VEHICLE_TO_GARAGE:
                    addNewVehicleToGarage();
                    break;
                case eMenuOptions.SHOW_VEHICLE_LICENSE_PLATE_FULL_LIST:
                    showVehicleLicensePlateFullList();
                    break;
                case eMenuOptions.SHOW_FILLTERED_VEHICLE_LICENSE_PLATE_LIST:
                    showFillteredVehicleLicensePlateList();
                    break;
                case eMenuOptions.CHANGE_VEHICLE_STATUS_IN_GARAGE:
                    changeVehicleStatusInGarage();
                    break;
                case eMenuOptions.INFLATE_WHEELS_TO_MAX_CAPACITY:
                    inflateWheelsToMaxCapacity();
                    break;
                case eMenuOptions.FUEL_POWERED_VEHICLE_REFUELING:
                    fuelPoweredVehicleRefueling();
                    break;
                case eMenuOptions.ELECTRIC_POWERED_VEHICLE_CHARGE:
                    electricPoweredVehicleCharge();
                    break;
                case eMenuOptions.SHOW_VEHICLE_INFO:
                    showVehicleInfo();
                    break;
            }
        }

        private int getUserInputFromMenu(int i_LastOptionInRange)
        {
            bool isValid = false;
            int menuChoice;
            Console.WriteLine("Please choose one of the above options: ");
            string inputString = Console.ReadLine();

            do
            {
                isValid = int.TryParse(inputString, out menuChoice);
                if (!isValid || !isMenuInputValid(menuChoice, i_LastOptionInRange))
                {
                    Console.WriteLine("Invalid input");
                    Console.WriteLine("Please choose one of the above options: ");
                    inputString = Console.ReadLine();
                    isValid = false;
                    // parse to int, using FormatException or ValueOutOfRangeException ?
                }
            }
            while (!isValid);

            return menuChoice;
        }

        private bool isMenuInputValid(int i_MenuInput, int i_LastOptionInRange)
        {
            return i_MenuInput >= 1 && i_MenuInput <= i_LastOptionInRange;
        }

        private StringBuilder generateMenu()
        {
            StringBuilder mainMenu = new StringBuilder();

            mainMenu.AppendFormat("Welcome! Please choose your desired action by entering the option number {0}{1}", Environment.NewLine, Environment.NewLine);
            mainMenu.AppendFormat("1. Enter a new vehicle to garage {0}", Environment.NewLine);
            mainMenu.AppendFormat("2. View the vehicle's list in the garage by license number {0}", Environment.NewLine);
            mainMenu.AppendFormat("3. View a filtered vehicle's list by status {0}", Environment.NewLine);
            mainMenu.AppendFormat("4. Change a vehicle's status {0}", Environment.NewLine);
            mainMenu.AppendFormat("5. Inflate a vehicle's wheels to the maximum {0}", Environment.NewLine);
            mainMenu.AppendFormat("6. Fuel-powered vehicle refueling  {0}", Environment.NewLine);
            mainMenu.AppendFormat("7. Electric-powered vehicle charge {0}", Environment.NewLine);
            mainMenu.AppendFormat("8. View entire vehicle details by license number {0}", Environment.NewLine);

            return mainMenu;
        }

        private string formatEnumStringToMenuOption(string io_Value)
        {
            StringBuilder format = new StringBuilder(io_Value.Replace("_", " ").ToLower());
            format[0] = char.ToUpper(format[0]);

            return format.ToString();
        }

        private StringBuilder generateAvailableEnumsToMessage(Type io_EnumType, out int o_NumOfEnums)
        {
            StringBuilder buildMag = new StringBuilder();
            int optionNumber = 1;

            foreach (string type in Enum.GetNames(io_EnumType))
            {
                string format = formatEnumStringToMenuOption(type);
                buildMag.AppendFormat("{0}. {1}{2}", optionNumber, format, Environment.NewLine);
                ++optionNumber;
            }

            o_NumOfEnums = optionNumber;

            return buildMag;
        }
    }
}
