namespace Ex03.GarageLogic
{
    internal class VehicleFactory
    {
        public static Vehicle CreateNewVehicle(eVehicleType io_VehicleType, string io_LicensePlate, string io_VehicleModel)
        {
            Vehicle vehicle = null;
            EnergySource energySource = getEnergySourceByVehicleType(io_VehicleType);

            switch (io_VehicleType)
            {
                case eVehicleType.ELECTRIC_MOTORCYCLE:
                case eVehicleType.FUEL_MOTORCYCLE:
                    vehicle = new Motorcycle(energySource, io_LicensePlate, io_VehicleModel);
                    break;
                case eVehicleType.ELECTRIC_CAR:
                case eVehicleType.FUEL_CAR:
                    vehicle = new Car(energySource, io_LicensePlate, io_VehicleModel);
                    break;
                case eVehicleType.TRUCK:
                    vehicle = new Truck(energySource, io_LicensePlate, io_VehicleModel);
                    break;
            }

            return vehicle;
        }

        private static EnergySource getEnergySourceByVehicleType(eVehicleType i_VehicleType)
        {
            EnergySource energySource = null;

            switch(i_VehicleType)
            {
                case eVehicleType.ELECTRIC_MOTORCYCLE:
                    energySource = new Electric(2.5f);
                    break;
                case eVehicleType.FUEL_MOTORCYCLE:
                    energySource = new Fuel(6.2f, eFuelType.Octan98);
                    break;
                case eVehicleType.ELECTRIC_CAR:
                    energySource = new Electric(3.3f);
                    break;
                case eVehicleType.FUEL_CAR:
                    energySource = new Fuel(38f, eFuelType.Octan95);
                    break;
                case eVehicleType.TRUCK:
                    energySource = new Fuel(120f, eFuelType.Soler);
                    break;
            }

            return energySource;
        }
    }
}
