using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class VehicleFactory
    {
        public Vehicle CreateNewVehicle(eVehicleType i_VehicleType)
        {
            Vehicle vehicle = null;
            EnergySource energySource = getEnergySourceByVehicleType(i_VehicleType);

            switch (i_VehicleType)
            {
                case eVehicleType.ElectricMotorcycle:
                case eVehicleType.FuelMotorcycle:
                    vehicle = new Motorcycle(energySource);
                    break;
                case eVehicleType.ElectricCar:
                case eVehicleType.FuelCar:
                    vehicle = new Car(energySource);
                    break;
                case eVehicleType.Truck:
                    vehicle = new Truck(energySource);
                    break;
            }

            return vehicle;
        }
        
        private EnergySource getEnergySourceByVehicleType(eVehicleType i_VehicleType)
        {
            EnergySource energySource = null;

            switch(i_VehicleType)
            {
                case eVehicleType.ElectricMotorcycle:
                    energySource = new Electric(2.5f);
                    break;
                case eVehicleType.FuelMotorcycle:
                    energySource = new Fuel(6.2f, eFuelType.Octan98);
                    break;
                case eVehicleType.ElectricCar:
                    energySource = new Electric(3.3f);
                    break;
                case eVehicleType.FuelCar:
                    energySource = new Fuel(38f, eFuelType.Octan95);
                    break;
                case eVehicleType.Truck:
                    energySource = new Fuel(120f, eFuelType.Soler);
                    break;
            }

            return energySource;
        }
    }
}
