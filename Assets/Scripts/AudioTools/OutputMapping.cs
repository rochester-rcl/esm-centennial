using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace AudioTools
{
    public class OutputMapping : MonoBehaviour
    {
        public GameObject input;
        public int selectedComponentIndex;
        public int selectedPropIndex;
        public Component selectedComponent;
        public string selectedProp;
        public string propType;
        private PropertyInfo output;
        private System.Type pType;

        // Start is called before the first frame update
        void Awake()
        {
            SetOutput();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void SetOutput()
        {
            output = selectedComponent.GetType().GetProperty(selectedProp);
            pType = System.Type.GetType(propType);
        }

        public object GetOutput()
        {
            return output;
        }

        public T GetOutputAs<T>()
        {
            System.Type t = typeof(T);
            object value = output.GetValue(selectedComponent);
            if (pType.Equals(t))
            {
                return (T)value;
            }
            throw new System.Exception(string.Format("Cannot Cast output to {0}", t.Name));
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(OutputMapping))]
    public class OutputMappingEditorLayout : Editor
    {
        private SerializedProperty input;
        private SerializedProperty selectedComponentIndex;
        private SerializedProperty selectedPropIndex;
        private SerializedProperty selectedComponent;
        private SerializedProperty selectedProp;
        private SerializedProperty propType;
        private Dictionary<string, string[]> componentProps;
        private string[] componentNames;
        public void OnEnable()
        {
            input = serializedObject.FindProperty("input");
            componentProps = new Dictionary<string, string[]>();
            selectedComponentIndex = serializedObject.FindProperty("selectedComponentIndex");
            selectedPropIndex = serializedObject.FindProperty("selectedPropIndex");
            selectedComponent = serializedObject.FindProperty("selectedComponent");
            selectedProp = serializedObject.FindProperty("selectedProp");
            propType = serializedObject.FindProperty("propType");
            GetInputComponents();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(input);
            if (input.objectReferenceValue != null)
            {
                selectedComponentIndex.intValue = EditorGUILayout.Popup("Component", selectedComponentIndex.intValue, componentNames);
                SetSelectedComponent(componentNames[selectedComponentIndex.intValue]);
                SelectComponentProp();
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void SetSelectedComponent(string componentType)
        {
            Component component = (input.objectReferenceValue as GameObject).GetComponent(componentType);
            if (component)
            {
                selectedComponent.objectReferenceValue = component;
            }
            else
            {
                throw new System.Exception(string.Format("No {0} Component Found on GameObject {1}", componentType, input.objectReferenceValue.name));
            }
        }

        private void SelectComponentProp()
        {
            Component component = selectedComponent.objectReferenceValue as Component;
            System.Type componentType = component.GetType();
            if (!componentProps.ContainsKey(componentType.Name))
            {
                GetComponentFields(componentType);
            }
            selectedPropIndex.intValue = EditorGUILayout.Popup("Property", selectedPropIndex.intValue, componentProps[componentType.Name]);
            SetSelectedProp(componentProps[componentType.Name][selectedPropIndex.intValue]);
        }

        private void SetSelectedProp(string propName)
        {
            selectedProp.stringValue = propName;
            Component c = selectedComponent.objectReferenceValue as Component;
            PropertyInfo info = c.GetType().GetProperty(propName);
            propType.stringValue = string.Format("{0}, UnityEngine", info.PropertyType.ToString());
        }

        private void GetComponentFields(System.Type componentType)
        {
            PropertyInfo[] props = componentType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            componentProps[componentType.Name] = props.Select(prop => prop.Name).ToArray();
        }

        private void GetInputComponents()
        {
            Component[] components = (input.objectReferenceValue as GameObject).GetComponents<Component>();
            componentNames = components.Select(component => component.GetType().Name).ToArray();
        }
    }
#endif
}



