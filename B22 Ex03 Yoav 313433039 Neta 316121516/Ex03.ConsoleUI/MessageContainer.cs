using System;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public enum eQuestionIndex
    {
        LICENSE_PLATE = 1,
        MODEL,
        CUSTOMER_NAME,
        CUSTOMER_PHONE,
    }

    public class MessageContainer
    {
        private const string k_VehicleNotInGarage = "The vehicle is not in the garage";
        private const string k_RefillWrongEnergySource = "Your energy source is not compatible with the action you've tried to execute";
        private const string k_StallProgram = "Press any key to continue... ";
        private const string k_EmptyGarage = "The garage is empty";
        private const string k_VehicleInGarage = "The vehicle is already in the garage";
        private const string k_ChooseAnOption = "Please choose one of the above options:";
        private const string k_LicensePlateFormatError = "The license plate can contain only digits";
        private const string k_ModelFormatError = "The model can contain letters or digits";
        private const string k_NameFormatError = "The name can contain only letters";
        private const string k_PhoneFormatError = "The phone number can contain only digits and is exactly 10 characters long";
        private const string k_EnterLicensePlate = "Please enter your license plate";
        private const string k_EnterName = "Please enter your name";
        private const string k_EnterPhone = "Please enter your phone number";
        private const string k_EnterModel = "What is your vehicle model?";
        private const string k_EnterFuelType = "What is your vehicle fuel type?";
        private const string k_EnterRefuel = "How many liters would you like to add?";
        private const string k_EnterCharge = "How many minutes of charging would you like to add?";
        private const string k_FilterByStatus = "Please enter which status you want to filter by:";
        private const string k_InflateDone = "Inflating is done";
        private const string k_InvalidInput = "Invalid input";
        private static string[] s_QuestionsToUserByQuestionIndex = { k_EnterLicensePlate, k_EnterModel, k_EnterName, k_EnterPhone };
        private static string[] s_ErrorToUserByQuestionIndex = { k_LicensePlateFormatError, k_ModelFormatError, k_NameFormatError, k_PhoneFormatError };
        private static StringBuilder s_TypesOfVehicles;
        private static StringBuilder s_PossibleVehicleStatuses;
        private static StringBuilder s_PossibleFuelTypes;
        private static StringBuilder s_MainMenu;
        private static int s_NumOfVehiclesTypes;
        private static int s_NumOfVehicleStatuses;
        private static int s_NumOfMenuChoices;

        static MessageContainer()
        {
            s_TypesOfVehicles = generateAvailableEnumsToMessage(typeof(eVehicleType), out s_NumOfVehiclesTypes);
            s_PossibleVehicleStatuses = generateAvailableEnumsToMessage(typeof(eVehicleStatus), out s_NumOfVehicleStatuses);
            s_PossibleFuelTypes = generateAvailableEnumsToMessage(typeof(eFuelType), out int numOfFuelTypes);
            s_MainMenu = generateMenu(out s_NumOfMenuChoices);
        }

        public static string VehicleNotInGarage
        {
            get
            {
                return k_VehicleNotInGarage;
            }
        }

        public static string RefillWrongEnergySource
        {
            get
            {
                return k_RefillWrongEnergySource;
            }
        }

        /*public static string LicensePlateFormatError
        {
            get
            {
                return k_LicensePlateFormatError;
            }
        }

        public static string ModelFormatError
        {
            get
            {
                return k_ModelFormatError;
            }
        }

        public static string NameFormatError
        {
            get
            {
                return k_NameFormatError;
            }
        }

        public static string PhoneFormatError
        {
            get
            {
                return k_PhoneFormatError;
            }
        }*/

        public static string StallProgram
        {
            get
            {
                return k_StallProgram;
            }
        }

        public static string EmptyGarage
        {
            get
            {
                return k_EmptyGarage;
            }
        }

        public static string VehicleInGarage
        {
            get
            {
                return k_VehicleInGarage;
            }
        }

        public static string ChooseAnOption
        {
            get
            {
                return k_ChooseAnOption;
            }
        }

        /*public static string EnterLicensePlate
        {
            get
            {
                return k_EnterLicensePlate;
            }
        }

        public static string EnterName
        {
            get
            {
                return k_EnterName;
            }
        }

        public static string EnterPhone
        {
            get
            {
                return k_EnterPhone;
            }
        }

        public static string EnterModel
        {
            get
            {
                return k_EnterModel;
            }
        }*/

        public static string EnterFuelType
        {
            get
            {
                return k_EnterFuelType;
            }
        }

        public static string EnterRefuel
        {
            get
            {
                return k_EnterRefuel;
            }
        }

        public static string EnterCharge
        {
            get
            {
                return k_EnterCharge;
            }
        }

        public static string FilterByStatus
        {
            get
            {
                return k_FilterByStatus;
            }
        }

        public static string InflateDone
        {
            get
            {
                return k_InflateDone;
            }
        }

        public static string InvalidInput
        {
            get
            {
                return k_InvalidInput;
            }
        }

        public static StringBuilder TypesOfVehicles
        {
            get
            {
                return s_TypesOfVehicles;
            }
        }

        public static StringBuilder PossibleVehicleStatuses
        {
            get
            {
                return s_PossibleVehicleStatuses;
            }
        }

        public static StringBuilder PossibleFuelTypes
        {
            get
            {
                return s_PossibleFuelTypes;
            }
        }

        public static StringBuilder MainMenu
        {
            get
            {
                return s_MainMenu;
            }
        }

        public static int NumOfVehiclesTypes
        {
            get
            {
                return s_NumOfVehiclesTypes;
            }
        }

        public static int NumOfVehicleStatuses
        {
            get
            {
                return s_NumOfVehicleStatuses;
            }
        }

        public static int NumOfMenuChoices
        {
            get
            {
                return s_NumOfMenuChoices;
            }
        }

        public static string GetQuestionIndex(eQuestionIndex i_Index)
        {
            int index = (int)i_Index - 1;
            return s_QuestionsToUserByQuestionIndex[index];
        }

        public static string GetQuestionErrorIndex(eQuestionIndex i_Index)
        {
            int index = (int)i_Index - 1;
            return s_ErrorToUserByQuestionIndex[index];
        }

        private static StringBuilder generateMenu(out int i_NumOfMenuChoices)
        {
            StringBuilder mainMenu = new StringBuilder();

            i_NumOfMenuChoices = 9;
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

        private static string formatEnumStringToMenuOption(string io_Value)
        {
            StringBuilder format = new StringBuilder(io_Value.Replace("_", " ").ToLower());
            format[0] = char.ToUpper(format[0]);

            return format.ToString();
        }

        private static StringBuilder generateAvailableEnumsToMessage(Type io_EnumType, out int o_NumOfEnums)
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
