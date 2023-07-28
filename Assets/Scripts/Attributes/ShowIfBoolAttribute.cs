using UnityEngine;

namespace Attributes
{
    public class ShowIfBoolAttribute : PropertyAttribute
    {
        public readonly string conditionFieldName;

        public ShowIfBoolAttribute(string conditionFieldName)
        {
            this.conditionFieldName = conditionFieldName;
        }
    }
}
