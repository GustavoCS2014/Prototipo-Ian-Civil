using System;
using UnityEngine;

namespace Attributes
{
    public class CallerAttribute : PropertyAttribute
    {
        public readonly Type scriptType;

        public CallerAttribute(Type scriptType)
        {
            this.scriptType = scriptType;
        }
    }
}
