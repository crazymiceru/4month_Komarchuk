using UnityEngine;

namespace Hole
{
    public sealed class GameController : MonoBehaviour
    {
        [HideInInspector]
        internal bool isShowCheats = false;
        public Transform trash;
        internal static GameController inst;
        private Unit[] _unitInteractiveObjects;

        public static void SetTrash(GameObject go)
        {
            go.transform.SetParent(inst.trash);
        }

        private void Awake()
        {
            if (inst == null) inst = this;
            else Destroy(gameObject);

            _unitInteractiveObjects = FindObjectsOfType<Unit>();
        }

        private void Update()
        {
            for (var i = 0; i < _unitInteractiveObjects.Length; i++)
            {
                var interactiveObject = _unitInteractiveObjects[i];

                if (interactiveObject == null)
                {
                    continue;
                }

                if (interactiveObject is IUnitComponentControlKeyboard unitComponentControlKeyboard)
                {
                    unitComponentControlKeyboard.Update();
                }
                if (interactiveObject is IUnitComponentRBMove unitComponentMove)
                {
                    unitComponentMove.Update();
                }
                if (interactiveObject is IRotate rotate)
                {
                    rotate.Update();
                }
                if (interactiveObject is IEnemyEffects enemyEffects)
                {
                    enemyEffects.Update();
                }
                if (interactiveObject is IUnitInvulnerability unitInvulnerability)
                {
                    unitInvulnerability.Update();
                }
                if (interactiveObject is IUnitControlTraectory unitControlTraectory)
                {
                    unitControlTraectory.Update();
                }
            }
        }
    }
}