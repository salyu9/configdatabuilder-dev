using System;
using ConfigDataDev;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ConfigDataBuilderDev
{
    public class TestBehaviour : MonoBehaviour
    {
        private readonly List<(string name, Action action)> _list;

        private TestBehaviour()
        {
            _list = new List<(string, Action)>();
            foreach (var method in typeof(TestBehaviour).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)) {
                var attr = method.GetCustomAttribute<DebugMenuAttribute>();
                if (attr != null) {
                    _list.Add((attr.Name ?? method.Name, (Action)method.CreateDelegate(typeof(Action), this)));
                }
            }
        }

        private class DebugMenuAttribute : Attribute
        {
            public string Name { get; }

            public DebugMenuAttribute(string name = null)
            {
                Name = name;
            }
        }

        [DebugMenu]
        private void ListTestConfig()
        {
            foreach (var config in TestConfig.AllConfig()) {
                Debug.Log(config);
            }
        }

        [DebugMenu]
        private void ListAssemblies()
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies()) {
                Debug.Log(asm.FullName);
            }
        }

        private void OnGUI()
        {
            var width = 400f;
            var height = 300f;

            var ratio = Mathf.Min(Screen.width / width, Screen.height / height);

            GUI.matrix = Matrix4x4.TRS(new Vector3(20, 20, 0), Quaternion.identity, new Vector3(ratio, ratio, 1));
            
            GUILayout.BeginVertical(GUILayout.Width(100));

            foreach (var action in _list) {
                if (GUILayout.Button(action.name)) {
                    action.action();
                }
            }
            
        }
    }
}
