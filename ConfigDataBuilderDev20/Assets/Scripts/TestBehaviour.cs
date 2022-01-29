using ConfigDataDev;
using UnityEngine;

namespace ConfigDataBuilderDev
{
    public class TestBehaviour : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            foreach (var config in TestConfig.AllConfig()) {
                Debug.Log(config);
            }
        }

    }
}
