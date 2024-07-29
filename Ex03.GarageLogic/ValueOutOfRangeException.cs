using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private const string k_ExceptionMessage = "The current value must be between {0} and {1}";
        private float? m_MaxValue;
        private float? m_MinValue;

        public ValueOutOfRangeException(float? i_MinValue, float? i_MaxValue)
            : base(string.Format(k_ExceptionMessage, i_MinValue, i_MaxValue))
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }
    }
}
