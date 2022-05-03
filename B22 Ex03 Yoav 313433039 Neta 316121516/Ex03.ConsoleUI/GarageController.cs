using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageController
    {
        private readonly Garage r_Garage = new Garage();

        public void Run()
        {
            bool exit = false;

            while(!exit)
            {
                printMenu();
                int menuChoice = getUserChoiceFromMenu();
                executeMenuAction(menuChoice);

            }
            

        }

        private void addNewVehicleToGarage()
        {
            // 1. get license plate
            string licensePlate = Console.ReadLine();

            if (r_Garage.IsVehiclesInGarage(licensePlate))
            {
                // update car status
                // print message
            }
            else
            {
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

        private void executeMenuAction(int io_MenuChoice)
        {
            eMenuOptions choiceToEnum = (eMenuOptions)io_MenuChoice;

            switch (choiceToEnum)
            {
                case eMenuOptions.ADD_NEW_VEHICLE_TO_GARAGE:
                    addNewVehicleToGarage();
                    break;
                case eMenuOptions.SHOW_VEHICLE_LICENSE_PLATE_LIST:
                    break;
                case eMenuOptions.CHANGE_VEHICLE_STATUS_IN_GARAGE:
                    break;
                case eMenuOptions.FILL_AIR_TO_MAX_CAPACITY:
                    break;
                case eMenuOptions.FILL_ENERGY_SOURCE:
                    break;
                case eMenuOptions.SHOW_VEHICLE_INFO:
                    break;
            }
        }

        private int getUserChoiceFromMenu()
        {
            // 1. readLine()
            // 2. validate int input
            //      2.1. phrse to int, using FormatException or ValueOutOfRangeException ?
            // 3. return int
            return 1;
        }

        private void printMenu()
        {

        }
    }
}
