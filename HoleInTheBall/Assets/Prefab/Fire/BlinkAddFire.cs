using System.Collections;
using UnityEngine;

namespace Hole
{
    public class BlinkAddFire : MonoBehaviour
    {
        private BlinkData _d;

        private void Awake()
        {
            _d = GetComponent<BlinkData>();
        }

        private void Start()
        {
            StartCoroutine(Add());
        }

        IEnumerator Add()
        {
            while (true)
            {
                var r = Random.Range(0f, 1f);
                if (r < _d.chanceAdd * _d.powerLight * _d.powerLight)
                {
                    var go = Instantiate(gameObject);
                    go.transform.position = transform.position;
                    GameController.SetTrash(go);
                    var dt = go.GetComponent<BlinkData>();
                    dt.powerLight = Mathf.Clamp(dt.powerLight + Random.Range(-_d.decLightAdd, _d.addLightAdd), 0, 1);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}