using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hole
{
    internal sealed class CongratulationsController : IController
    {
        private UnitM _unit;
        private ControlLeak _controlLeak = new ControlLeak("Congratulations");
        private int _maxScores;

        internal CongratulationsController(UnitM unit,int maxScores)
        {
            _unit = unit;
            _unit.evtScores += CompareScores;
            _maxScores = maxScores;
        }

        void CompareScores()
        {
            if (_unit.Scores > _maxScores)
            {
                Debug.Log($"Win");
                var go = DataObjects.inst.GetValue<GameObject>("Util/Congratulations");
                GameObject.Instantiate(go, Reference.inst.canvas.transform);
                var goRestart = DataObjects.inst.GetValue<GameObject>("Util/Restart");
                var goRestartInst = GameObject.Instantiate(goRestart, Reference.inst.canvas.transform);

                var button = goRestartInst.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.AddListener(delegate { Restart(); });
                }

                _unit.evtScores -= CompareScores;
                Time.timeScale = 0;
            }
        }

        void Restart()
        {
            Debug.Log($"Restart");            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}