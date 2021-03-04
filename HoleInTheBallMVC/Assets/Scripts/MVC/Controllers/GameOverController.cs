using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hole
{
    internal sealed class GameOverController : IController
    {
        private UnitM _unit;        
        internal GameOverController(UnitM unit)
        {
            _unit = unit;
            _unit.evtKill += GameOver;
        }

        void GameOver()
        {
            Debug.Log($"Game Over");
            var go = DataObjects.inst.GetValue<GameObject>("Util/GameOver");
            GameObject.Instantiate(go, Reference.inst.canvas.transform);
            var goRestart = DataObjects.inst.GetValue<GameObject>("Util/Restart");
            var goRestartInst=GameObject.Instantiate(goRestart, Reference.inst.canvas.transform);

            var button = goRestartInst.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(delegate { Restart(); });
            }
            
            _unit.evtKill -= GameOver;
        }

        void Restart()
        {
            Debug.Log($"Restart");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}