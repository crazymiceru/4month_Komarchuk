using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hole
{
    internal sealed class GetEnvironmentController : IController, IExecute
    {
        #region Init

        private UnitM _unit;
        private UnitView _unitView;
        private ControlLeak _controlLeak = new ControlLeak("GetEnvironment");
        private Dictionary<TypeItem, List<EnvironmentData>> _listEnv = new Dictionary<TypeItem, List<EnvironmentData>>();
        private Rigidbody _rigidBody;
        private Dictionary<TypeItem, Action<EnvironmentData>> _listProcedure;

        internal GetEnvironmentController(UnitM unit, UnitView unitView)
        {
            _unit = unit;
            _unitView = unitView;
            _unit.evtKill += Kill;
            _unitView.evtInInteractive += InInteractive;
            _rigidBody = unitView.GetComponent<Rigidbody>();

            _listProcedure = new Dictionary<TypeItem, Action<EnvironmentData>>
            {
                [TypeItem.EnvSlow] = EnvSlow,
                [TypeItem.EnvCollapse] = EnvCollapse
            };

            foreach (var item in _listProcedure)
            {
                _listEnv.Add(item.Key,new List<EnvironmentData>());
            }
        }

        private void Kill()
        {
            ListControllers.inst.Delete(this);
        }

        public void Execute(float deltaTime)
        {
            foreach (var item in _listEnv)
            {
                if (item.Value.Count > 0)
                {
                    _listProcedure[item.Key].Invoke(item.Value[item.Value.Count-1]);
                }
            }
        }

        private void InInteractive(PackInteractiveData pack, bool isEnter)
        {
            if (_listProcedure.ContainsKey(pack.typeItem))
            {
                //Debug.Log($"Attack EnvironmentData: {((EnvironmentData)pack.obj).maxSqrSlowSpeed}");
                if (isEnter) _listEnv[pack.typeItem].Add((EnvironmentData)pack.obj);
                else _listEnv[pack.typeItem].Remove((EnvironmentData)pack.obj);
            }
        }

        #endregion


        #region Actions

        private void EnvSlow(EnvironmentData environmentData)
        {
            if (_rigidBody.velocity.sqrMagnitude > environmentData.maxSqrSlowSpeed) _rigidBody.velocity = _rigidBody.velocity.normalized * environmentData.maxSqrSlowSpeed;
        }

        private void EnvCollapse(EnvironmentData environmentData)
        {
            _rigidBody.velocity = _rigidBody.velocity + _rigidBody.velocity.normalized * environmentData.powerCollapseSpeed * Time.deltaTime;
        }

        #endregion
    }
}