using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageController
    {
        private enum eMenuOptions
        {
            ADD_NEW_VEHICLE_TO_GARAGE = 1,
            SHOW_VEHICLES_FULL_LIST,
            SHOW_FILLTERED_VEHICLES_LIST,
            CHANGE_VEHICLE_STATUS_IN_GARAGE,
            INFLATE_WHEELS_TO_MAX_CAPACITY,
            FUEL_POWERED_VEHICLE_REFUELING,
            ELECTRIC_POWERED_VEHICLE_CHARGE,
            SHOW_VEHICLE_INFO,
            EXIT,
        }

        private static readonly eMenuOptions[] sr_MenuOptionsThatDoesntRequireLicensePlate = { eMenuOptions.SHOW_VEHICLES_FULL_LIST, eMenuOptions.SHOW_FILLTERED_VEHICLES_LIST, eMenuOptions.EXIT };
        private readonly Garage r_Garage = new Garage();

        public void Run()
        {
            bool isTerminated = false;
            eMenuOptions choiceToEnum;

            while (!isTerminated)
            {
                Console.Clear();
                Console.WriteLine(MessageContainer.MainMenu);
                int menuChoice = getUserInputMenuChoice(MessageContainer.NumOfMenuChoices);
                choiceToEnum = (eMenuOptions)menuChoice;

                if (r_Garage.IsEmptyGarage() && choiceToEnum != eMenuOptions.ADD_NEW_VEHICLE_TO_GARAGE && choiceToEnum != eMenuOptions.EXIT)
                {
                    printMessageAndSleep(MessageContainer.EmptyGarage);
                }
                else
                {
                    string licensePlate = getLicensePlateIfNeeded(choiceToEnum);

                    if(validateAddEnargyAction(choiceToEnum, licensePlate))
                    {
                        isTerminated = executeMenuAction(choiceToEnum, licensePlate);
                    }
                    else
                    {
                        printMessageAndStallClearScreen(MessageContainer.RefillWrongEnergySource);
                    }
                }
            }
        }

        private void addNewVehicleToGarageWrapper(string io_LicensePlate)
        {
            if (r_Garage.IsVehicleInGarage(io_LicensePlate))
            {
                r_Garage.ChangeVehicleStatus(eVehicleStatus.IN_REPAIR);
                printMessageAndStallClearScreen(MessageContainer.VehicleInGarage);
            }
            else
            {
                addNewVehicleToGarage(io_LicensePlate);
            }
        }

        private void addNewVehicleToGarage(string io_LicensePlate)
        {
            Console.Clear();
            Console.WriteLine(MessageContainer.TypesOfVehicles);
            int vehicleType = getUserInputMenuChoice(MessageContainer.NumOfVehiclesTypes);
            string customerName = getUserInputStringByQuestion(eQuestionIndex.CUSTOMER_NAME);
            string customerPhone = getUserInputStringByQuestion(eQuestionIndex.CUSTOMER_PHONE);
            string vehicleModel = getUserInputStringByQuestion(eQuestionIndex.MODEL);

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
                printMessageAndSleep(exception.Message);
                getSpecificQuestionFromUser(io_Question, io_QuestionKey, io_QuestionTypeKey);
            }
        }

        private void showVehicleLicensePlateFullList()
        {
            string licensePlateList = r_Garage.ShowLicensePlateList().ToString();

            printMessageAndStallClearScreen(licensePlateList);
        }

        private void showFillteredVehicleLicensePlateList()
        {
            eVehicleStatus status = askForVehicleStatus();
            string licensePlateList = r_Garage.ShowLicensePlateList(status).ToString();

            printMessageAndStallClearScreen(licensePlateList);
        }

        private void changeVehicleStatusInGarage()
        {
            eVehicleStatus status = askForVehicleStatus();

            r_Garage.ChangeVehicleStatus(status);
        }

        private void inflateWheelsToMaxCapacity()
        {
            r_Garage.InflateVehicleWheelsToMax();

            printMessageAndStallClearScreen(MessageContainer.InflateDone);
        }

        private void fuelPoweredVehicleRefueling()
        {
            askForFuelType();
            addEnergyAmountToVehicle(MessageContainer.EnterRefuel);
        }

        private void askForFuelType()
        {
            try
            {
                Console.WriteLine(MessageContainer.EnterFuelType);
                Console.WriteLine(MessageContainer.PossibleFuelTypes);
                string fuelType = Console.ReadLine();

                r_Garage.ValidateRefuelType(fuelType);
            }
            catch (Exception exception)
            {
                printMessageAndSleep(exception.Message);
                askForFuelType();
            }
        }

        private void addEnergyAmountToVehicle(string i_Question)
        {
            try
            {
                Console.WriteLine(i_Question);
                string amountToAdd = Console.ReadLine();

                r_Garage.AddEnergyToVehicle(amountToAdd);
            }
            catch (Exception exception)
            {
                printMessageAndSleep(exception.Message);
                addEnergyAmountToVehicle(i_Question);
            }
        }

        private void showVehicleInfo()
        {
            string vehicleDetails = r_Garage.ShowVehicleFullDetails().ToString();

            printMessageAndStallClearScreen(vehicleDetails);
        }

        private bool executeMenuAction(eMenuOptions i_MenuChoice, string io_LicensePlate)
        {
            bool isTerminated = false;

            switch (i_MenuChoice)
            {
                case eMenuOptions.ADD_NEW_VEHICLE_TO_GARAGE:
                    addNewVehicleToGarageWrapper(io_LicensePlate);
                    break;
                case eMenuOptions.SHOW_VEHICLES_FULL_LIST:
                    showVehicleLicensePlateFullList();
                    break;
                case eMenuOptions.SHOW_FILLTERED_VEHICLES_LIST:
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
                    addEnergyAmountToVehicle(MessageContainer.EnterCharge);
                    break;
                case eMenuOptions.SHOW_VEHICLE_INFO:
                    showVehicleInfo();
                    break;
                case eMenuOptions.EXIT:
                    isTerminated = true;
                    break;
            }

            return isTerminated;
        }

        private string getLicensePlateIfNeeded(eMenuOptions i_MenuChoice)
        {
            string licensePlate = null;

            if (!sr_MenuOptionsThatDoesntRequireLicensePlate.Contains<eMenuOptions>(i_MenuChoice))
            {
                licensePlate = getUserInputStringByQuestion(eQuestionIndex.LICENSE_PLATE);
                if (i_MenuChoice != eMenuOptions.ADD_NEW_VEHICLE_TO_GARAGE)
                {
                    while (!r_Garage.IsVehicleInGarage(licensePlate))
                    {
                        Console.WriteLine(MessageContainer.VehicleNotInGarage);
                        licensePlate = getUserInputStringByQuestion(eQuestionIndex.LICENSE_PLATE);
                    }
                }
            }

            return licensePlate;
        }

        private bool validateAddEnargyAction(eMenuOptions i_MenuChoice, string i_LicensePlate)
        {
            bool isValid = true;

            if (i_LicensePlate != null && i_MenuChoice != eMenuOptions.ADD_NEW_VEHICLE_TO_GARAGE)
            {
                bool isElectric = r_Garage.isElectricVehicle();

                if ((isElectric && i_MenuChoice == eMenuOptions.FUEL_POWERED_VEHICLE_REFUELING) || (!isElectric && i_MenuChoice == eMenuOptions.ELECTRIC_POWERED_VEHICLE_CHARGE))
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        private bool validateInputByQuestionKey(string io_InputToCheck, eQuestionIndex i_QuestionNumber)
        {
            bool isValid = false;

            switch (i_QuestionNumber)
            {
                case eQuestionIndex.LICENSE_PLATE:
                    isValid = io_InputToCheck.All(char.IsDigit);
                    break;
                case eQuestionIndex.MODEL:
                    isValid = io_InputToCheck.All(char.IsLetterOrDigit);
                    break;
                case eQuestionIndex.CUSTOMER_NAME:
                    isValid = io_InputToCheck.All(char.IsLetter);
                    break;
                case eQuestionIndex.CUSTOMER_PHONE:
                    isValid = io_InputToCheck.All(char.IsDigit) && io_InputToCheck.Length == 10;
                    break;
            }

            return isValid;
        }

        private string getUserInputStringByQuestion(eQuestionIndex io_QuestionKey)
        {
            bool isValid = false;
            string questionToAsk = MessageContainer.GetQuestionIndex(io_QuestionKey);
            string errorMsg = MessageContainer.GetQuestionErrorIndex(io_QuestionKey);

            Console.WriteLine(questionToAsk);
            string inputString = Console.ReadLine();

            do
            {
                isValid = validateInputByQuestionKey(inputString, io_QuestionKey);
                if (!isValid)
                {
                    Console.Clear();
                    Console.WriteLine(MessageContainer.InvalidInput);
                    Console.WriteLine(errorMsg);
                    Console.WriteLine(questionToAsk);
                    inputString = Console.ReadLine();
                }
            }
            while (!isValid);

            return inputString;
        }

        private int getUserInputMenuChoice(int i_LastOptionInRange)
        {
            bool isValid = false;
            int menuChoice;

            Console.WriteLine(MessageContainer.ChooseAnOption);
            string inputString = Console.ReadLine();

            do
            {
                isValid = int.TryParse(inputString, out menuChoice);
                if (!isValid || !isMenuInputValid(menuChoice, i_LastOptionInRange))
                {
                    Console.WriteLine(MessageContainer.InvalidInput);
                    Console.WriteLine(MessageContainer.ChooseAnOption);
                    inputString = Console.ReadLine();
                    isValid = false;
                }
            }
            while (!isValid);

            return menuChoice;
        }

        private bool isMenuInputValid(int i_MenuInput, int i_LastOptionInRange)
        {
            return i_MenuInput >= 1 && i_MenuInput <= i_LastOptionInRange;
        }

        private eVehicleStatus askForVehicleStatus()
        {
            Console.WriteLine(MessageContainer.FilterByStatus);
            Console.WriteLine(MessageContainer.PossibleVehicleStatuses);
            int vehicleStatus = getUserInputMenuChoice(MessageContainer.NumOfVehicleStatuses);

            return (eVehicleStatus)vehicleStatus;
        }

        private void printMessageAndStallClearScreen(string i_MsgToUser)
        {
            Console.WriteLine(i_MsgToUser);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(MessageContainer.StallProgram);
            Console.ReadKey();
            Console.Clear();
        }

        private void printMessageAndSleep(string i_MsgToUser)
        {
            Console.WriteLine(i_MsgToUser);
            Thread.Sleep(2000);
            Console.Clear();
        }
    }
}
