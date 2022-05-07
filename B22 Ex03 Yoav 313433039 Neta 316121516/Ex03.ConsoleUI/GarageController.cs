using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageController
    {
        private const string k_StallProgram = "Press any key to continue... ";
        private const int k_NumOfMenuChoices = 9;
        private readonly eMenuOptions[] r_MenuOptionsThatDoesntRequireLicensePlate = { eMenuOptions.SHOW_VEHICLE_LICENSE_PLATE_FULL_LIST, eMenuOptions.SHOW_FILLTERED_VEHICLE_LICENSE_PLATE_LIST, eMenuOptions.EXIT };
        private readonly Garage r_Garage = new Garage();
        private readonly StringBuilder r_TypesOfVehicles;
        private readonly StringBuilder r_PossibleVehicleStatuses;
        private readonly StringBuilder r_PossibleFuelTypes;
        private readonly StringBuilder r_MainMenu;
        private readonly int r_NumOfVehiclesTypes;
        private readonly int r_NumOfVehicleStatuses;

        public GarageController()
        {
            r_TypesOfVehicles = generateAvailableEnumsToMessage(typeof(eVehicleType), out r_NumOfVehiclesTypes);
            r_PossibleVehicleStatuses = generateAvailableEnumsToMessage(typeof(eVehicleStatus), out r_NumOfVehicleStatuses);
            r_PossibleFuelTypes = generateAvailableEnumsToMessage(typeof(eFuelType), out int numOfFuelTypes);
            r_MainMenu = generateMenu();
        }

        public void Run()
        {
            bool isTerminated = false;

            while (!isTerminated)
            {
                Console.Clear();
                Console.WriteLine(r_MainMenu);
                int menuChoice = getUserInputFromMenu(k_NumOfMenuChoices);

                if(r_Garage.IsEmptyGarage() && menuChoice != 1 && menuChoice != 9)
                {
                    string msg = string.Format("The garage is empty {0}{1}", Environment.NewLine, k_StallProgram);
                    stallClearScreen(msg);
                }
                else
                {
                    isTerminated = executeMenuAction(menuChoice);
                }
            }
        }

        private void addNewVehicleToGarage(string io_LicensePlate)
        {
            if (r_Garage.IsVehiclesInGarage(io_LicensePlate))
            {
                r_Garage.ChangeVehicleStatus(io_LicensePlate, eVehicleStatus.IN_REPAIR);
                string msg = string.Format("The vehicle is already in the garage {0}{1}", Environment.NewLine, k_StallProgram);

                stallClearScreen(msg);
            }
            else
            {
                Console.WriteLine("Please choose your vehicle type:");
                Console.WriteLine(r_TypesOfVehicles);
                int vehicleType = getUserInputFromMenu(r_NumOfVehiclesTypes);
                string customerName = getInputWithoutDependencyInAnObject("Please enter your name", 3);
                string customerPhone = getInputWithoutDependencyInAnObject("Please enter your phone number", 4);
                string vehicleModel = getInputWithoutDependencyInAnObject("What is your vehicle model?", 2);

                r_Garage.AddNewVehicleToGarage(vehicleType, io_LicensePlate, vehicleModel, customerName, customerPhone);
                Dictionary<string, Dictionary<int, string>> detailsToAskUser = r_Garage.GetListOfQuestionsToInitiateVehicle();

                foreach (KeyValuePair<string, Dictionary<int, string>> questionType in detailsToAskUser)
                {
                    foreach (KeyValuePair<int, string> question in questionType.Value)
                    {
                        getSpecificQuestionFromUser(question.Value, question.Key, questionType.Key);
                    }
                }
            }
        }

        private void getSpecificQuestionFromUser(string io_Question, int io_QuestionKey, string io_QuestionTypeKey)
        {
            try
            {
                Console.WriteLine(io_Question);
                string input = Console.ReadLine();
                r_Garage.ValidateInputInNewVehicle(input, io_QuestionTypeKey, io_QuestionKey);
            }
            catch (Exception exception)
            {
                executeCatch(exception.Message);
                getSpecificQuestionFromUser(io_Question, io_QuestionKey, io_QuestionTypeKey);
            }

            //Console.Clear();
        }

        private void showVehicleLicensePlateFullList()
        {
            string msg = string.Format("{0}{1}{2}", r_Garage.ShowLicensePlateList().ToString(), Environment.NewLine, k_StallProgram);

            stallClearScreen(msg);
        }

        private void showFillteredVehicleLicensePlateList()
        {
            eVehicleStatus status = askForVehicleStatus();
            string msg = string.Format("{0}{1}{2}", r_Garage.ShowLicensePlateList(status), Environment.NewLine, k_StallProgram);

            stallClearScreen(msg);
        }

        private void changeVehicleStatusInGarage(string io_LicensePlate)
        {
            try
            {
                eVehicleStatus status = askForVehicleStatus();
                r_Garage.ChangeVehicleStatus(io_LicensePlate, status);
            }
            catch(ArgumentException exception)
            {
                executeCatch(exception.Message);
                changeVehicleStatusInGarage(io_LicensePlate);
            }
        }

        private void inflateWheelsToMaxCapacity(string io_LicensePlate)
        {
            try
            {
                r_Garage.InflateVehicleWheelsToMax(io_LicensePlate);
                string msg = string.Format("Inflating is done {0}{1}", Environment.NewLine, k_StallProgram);

                stallClearScreen(msg);
            }
            catch (ArgumentException exception)
            {
                executeCatch(exception.Message);
                inflateWheelsToMaxCapacity(io_LicensePlate);
            }
        }

        private void fuelPoweredVehicleRefueling(string io_LicensePlate)
        {
            try
            {
                Console.WriteLine("What is your vehicle fuel type?");
                Console.WriteLine(r_PossibleFuelTypes);
                string fuelType = Console.ReadLine();

                Console.WriteLine("How many liters would you like to add?");
                string amountToAdd = Console.ReadLine();

                r_Garage.AddEnergyToVehicle(io_LicensePlate, amountToAdd, fuelType);
            }
            catch (Exception exception)
            {
                executeCatch(exception.Message);
                fuelPoweredVehicleRefueling(io_LicensePlate);
            }
        }

        private void electricPoweredVehicleCharge(string io_LicensePlate)
        {
            try
            {
                Console.WriteLine("How many minutes of charging would you like to add?");
                string amountToAdd = Console.ReadLine();

                r_Garage.AddEnergyToVehicle(io_LicensePlate, amountToAdd);
            }
            catch (Exception exception)
            {
                executeCatch(exception.Message);
                electricPoweredVehicleCharge(io_LicensePlate);
            }
        }

        private void showVehicleInfo(string io_LicensePlate)
        {
            try
            {
                string msg = string.Format("{0}{1}{2}", r_Garage.ShowVehicleFullDetails(io_LicensePlate), Environment.NewLine, k_StallProgram);

                stallClearScreen(msg);
            }
            catch (Exception exception)
            {
                executeCatch(exception.Message);
                showVehicleInfo(io_LicensePlate);
            }
        }

        private bool executeMenuAction(int i_MenuChoice)
        {
            bool isTerminated = false;
            string licensePlate = string.Empty;
            eMenuOptions choiceToEnum = (eMenuOptions)i_MenuChoice;

            if(!r_MenuOptionsThatDoesntRequireLicensePlate.Contains<eMenuOptions>(choiceToEnum))
            {
                 licensePlate = getInputWithoutDependencyInAnObject("Please enter your license plate", 1);
            }

            switch (choiceToEnum)
            {
                case eMenuOptions.ADD_NEW_VEHICLE_TO_GARAGE:
                    addNewVehicleToGarage(licensePlate);
                    break;
                case eMenuOptions.SHOW_VEHICLE_LICENSE_PLATE_FULL_LIST:
                    showVehicleLicensePlateFullList();
                    break;
                case eMenuOptions.SHOW_FILLTERED_VEHICLE_LICENSE_PLATE_LIST:
                    showFillteredVehicleLicensePlateList();
                    break;
                case eMenuOptions.CHANGE_VEHICLE_STATUS_IN_GARAGE:
                    changeVehicleStatusInGarage(licensePlate);
                    break;
                case eMenuOptions.INFLATE_WHEELS_TO_MAX_CAPACITY:
                    inflateWheelsToMaxCapacity(licensePlate);
                    break;
                case eMenuOptions.FUEL_POWERED_VEHICLE_REFUELING:
                    fuelPoweredVehicleRefueling(licensePlate);
                    break;
                case eMenuOptions.ELECTRIC_POWERED_VEHICLE_CHARGE:
                    electricPoweredVehicleCharge(licensePlate);
                    break;
                case eMenuOptions.SHOW_VEHICLE_INFO:
                    showVehicleInfo(licensePlate);
                    break;
                case eMenuOptions.EXIT:
                    isTerminated = true;
                    break;
            }

            return isTerminated;
        }

        private string getInputWithoutDependencyInAnObject(string io_Question, int io_QuestionKey)
        {
            Console.WriteLine(io_Question);
            string input = Console.ReadLine();

            try
            {
                r_Garage.ValidateInputWithoutDependencyInAnObject(input, io_QuestionKey);
            }
            catch (ArgumentException exception)
            {
                executeCatch(exception.Message);
                input = getInputWithoutDependencyInAnObject(io_Question, io_QuestionKey);
            }

            Console.Clear();

            return input;
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
                }
            }
            while (!isValid);

            Console.Clear();

            return menuChoice;
        }

        private bool isMenuInputValid(int i_MenuInput, int i_LastOptionInRange)
        {
            return i_MenuInput >= 1 && i_MenuInput <= i_LastOptionInRange;
        }

        private void stallClearScreen(string i_MsgToUser)
        {
            Console.WriteLine(i_MsgToUser);
            Console.ReadKey();
            Console.Clear();
        }

        private void executeCatch(string i_MsgToUser)
        {
            Console.WriteLine(i_MsgToUser);
            Thread.Sleep(2000);
            Console.Clear();
        }

        private eVehicleStatus askForVehicleStatus()
        {
            Console.WriteLine("Please enter which status you want to filter by:");
            Console.WriteLine(r_PossibleVehicleStatuses);
            int vehicleStatus = getUserInputFromMenu(r_NumOfVehicleStatuses);

            return (eVehicleStatus)vehicleStatus;
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
            mainMenu.AppendFormat("9. Exit {0}", Environment.NewLine);

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
