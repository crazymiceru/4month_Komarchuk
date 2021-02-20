using UnityEngine;

namespace Hole
{
    public sealed class PlayerController : UnitRB,IUnitComponentControlKeyboard, IUnitInvulnerability
    {
        [SerializeField] private GameObject _viewInvulnerability;
        [SerializeField] private IntVariable _liveGlobal;
        [SerializeField] private AddListener _listenerCanvas;
        [SerializeField] private IntVariable _scoreGlobal;

        void IUnitInvulnerability.Update()
        {
            if (_timeInvulnerability < Time.time)
            {
                if (_viewInvulnerability != null && _viewInvulnerability.activeSelf) _viewInvulnerability.SetActive(false);
            }
            if (_viewInvulnerability != null && _timeInvulnerability > Time.time && !_viewInvulnerability.activeSelf) _viewInvulnerability.SetActive(true);
        }

        protected override void UpdateParameters()
        {
            base.UpdateParameters();
            _liveGlobal.Value = lives;
            _scoreGlobal.Value = scores;
            _listenerCanvas.Notify();
        }

        void IUnitComponentControlKeyboard.Update()
        {
            _h = Input.GetAxis("Horizontal");
            _v = Input.GetAxis("Vertical");
            _jmp = Input.GetButtonDown("Jump");
        }
    }
}