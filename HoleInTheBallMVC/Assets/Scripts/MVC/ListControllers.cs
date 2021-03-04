using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hole
{
    internal sealed class ListControllers : IExecute, IInitialization, ILateExecute
    {
        internal static ListControllers inst;

        private Action _init = delegate { };
        private Action<float> _execute = delegate { };
        private Action _lateExecute = delegate { };

        public ListControllers()
        {
            inst = this;
        }

        public void Execute(float deltaTime)
        {
            _execute(deltaTime);
        }

        public void Initialization()
        {
            _init();
        }

        public void LateExecute()
        {
            _lateExecute();
        }

        internal void Add(IController controller)
        {
            if (controller is IInitialization init)
            {
                _init += init.Initialization;
            }
            if (controller is IExecute execute)
            {
                _execute += execute.Execute;
            }
            if (controller is ILateExecute lateExecute)
            {
                _lateExecute += lateExecute.LateExecute;
            }
            //Debug.Log($"Execute Delegats: {_execute.GetInvocationList().Length}"); 
        }

        internal void Delete(IController controller)
        {
            if (controller is IInitialization init)
            {
                _init -= init.Initialization;
            }
            if (controller is IExecute execute)
            {
                _execute -= execute.Execute;
            }
            if (controller is ILateExecute lateExecute)
            {
                _lateExecute -= lateExecute.LateExecute;
            }
        }
    }
}
