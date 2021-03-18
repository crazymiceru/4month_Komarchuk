using System.Linq;
using UnityEngine;

public class ShowLeaks : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            Debug.Log("***********************************");
            foreach (var item in ControlLeak.dataLeak.OrderByDescending(o=>o.Value))
            {
                Debug.Log($"{item.Key}:{item.Value}");
            }
        }
    }
}
