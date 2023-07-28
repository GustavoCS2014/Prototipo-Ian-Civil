using UnityEngine;

namespace Attributes
{
    public class ShowIfStringAttribute : PropertyAttribute
    {
        public readonly string conditionalFieldName;
        public readonly string targetValue;

        public ShowIfStringAttribute(string conditionalFieldName, string targetValue)
        {
            this.conditionalFieldName = conditionalFieldName;
            this.targetValue = targetValue;
        }
    }
}
