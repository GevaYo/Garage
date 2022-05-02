using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class Car : Vehicle
    {
        enum eCarColors { RED, WHITE, GREEN, BLUE }
        readonly eCarColors r_Color;
        readonly byte r_NumOfDoors;
    }
}
