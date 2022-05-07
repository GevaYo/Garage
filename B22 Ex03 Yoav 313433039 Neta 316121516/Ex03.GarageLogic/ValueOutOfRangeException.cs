using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float? m_MaxValue;
        private float? m_MinValue;
        private const string m_ExceptionMessage = "The current value must be between {0} and {1}";

        public ValueOutOfRangeException(float? i_MinValue, float? i_MaxValue)
            : base(string.Format(m_ExceptionMessage, i_MinValue, i_MaxValue))
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }
    }
}
