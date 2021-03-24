using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hole
{
    internal sealed class ListControllers : IExecute, IInitialization, ILateExecute
    {
        internal static ListControllers inst;

        private event Action _init = delegate { };
        private event Action<float> _execute = delegate { };
        private event Action _lateExecute = delegate { };
        private List<Func<DataGameForSave>> _save = new List<Func<DataGameForSave>>();
        private List<Action<DataGameForSave>> _load = new List<Action<DataGameForSave>>();
        private event Action<TypeItem> _destroy = delegate { };

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

        public void Destroy(TypeItem type = 0)
        {
            _destroy(type);
        }

        public List<DataGameForSave> Save()
        {
            var dataMas = new List<DataGameForSave>();
            for (int i = 0; i < _save.Count; i++)
            {
                dataMas.Add(_save[i].Invoke());
            }
            return dataMas;
        }

        public void Load(DataGameForSave data)
        {
            for (int i = 0; i < _load.Count; i++)
            {
                _load[i](data);
            }
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
            if (controller is IDestroy destroy)
            {
                _destroy += destroy.Destroy;
            }
            if (controller is ICntrSave cntrSave)
            {
                _save.Add(cntrSave.Save);
                _load.Add(cntrSave.Load);
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
            if (controller is IDestroy destroy)
            {
                _destroy -= destroy.Destroy;
            }
            if (controller is ICntrSave cntrSave)
            {
                _save.Remove(cntrSave.Save);
                _load.Remove(cntrSave.Load);
            }
        }
    }
}
