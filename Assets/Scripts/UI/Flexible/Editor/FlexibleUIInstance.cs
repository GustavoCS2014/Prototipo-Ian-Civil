using UnityEditor;
using UnityEngine;

namespace UI.Flexible.Editor
{
    public class FlexibleUIInstance : UnityEditor.Editor
    {
        private static GameObject clickedObject;

        [MenuItem("GameObject/Flexible/Button", priority = 0)]
        public static void AddButton()
        {
            Create("Button");
        }

        private static GameObject Create(string objectName)
        {
            var instance = Instantiate(Resources.Load<GameObject>(objectName));
            instance.name = objectName;
            clickedObject = Selection.activeContext as GameObject;

            if (clickedObject)
                instance.transform.SetParent(clickedObject.transform, false);

            return instance;
        }
    }
}
