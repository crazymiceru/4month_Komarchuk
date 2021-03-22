using System;
using UnityEngine;

namespace Hole
{
    internal sealed class ListControllers : IExecute, IInitialization, ILateExecute
    {
        internal static ListControllers inst;

        private event Action _init = delegate { };
        private event Action<float> _execute = delegate { };
        private event Action _lateExecute = delegate { };
        private bool isInitHasPassed = false;

        public static int countClass = 0;
        private int _countClass;
        public static int countAddListControllers = 0;

        public ListControllers()
        {
            countClass++;
            _countClass = countClass;
            Debug.Log($"Init ListControllers {countClass}");
            inst = this;
        }

        public void Execute(float deltaTime)
        {
            _execute(deltaTime);
        }

        public void Initialization()
        {
            isInitHasPassed = true;
            _init();
        }

        public void LateExecute()
        {
            _lateExecute();
        }

        internal void Add(IController controller, string name = "")
        {
            countAddListControllers++;
            if (controller is IInitialization init)
            {
                _init += init.Initialization;
                if (isInitHasPassed) init.Initialization();
            }
            if (controller is IExecute execute)
            {
                _execute += execute.Execute;
            }
            if (controller is ILateExecute lateExecute)
            {
                _lateExecute += lateExecute.LateExecute;
            }
            //Debug.Log($"Execute Delegats: {_execute.GetInvocationList().Length} name:{name} CurrentListController:{_countClass}");
        }

        internal void Delete(IController controller)
        {
            countAddListControllers--;
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
