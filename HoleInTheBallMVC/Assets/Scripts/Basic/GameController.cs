using System.Linq;
using UnityEngine;
using TMPro;
using System;

namespace Hole
{

    internal sealed class GameController : MonoBehaviour
    {
        public Transform trash;
        [SerializeField] private TextMeshProUGUI _textScores;

        internal static GameController inst;
        private ListControllers _listControllers;
        private DataObjects _dataObjects;

        private FabricUnit _fabricUnit;
        private Reference _ref;
        private SaveDataRepository _saveGame;

        public static void SetTrash(GameObject go)
        {
            go.transform.SetParent(inst.trash);
        }

        private void Awake()
        {
            Time.timeScale = 1;
            GC.Collect();
            _listControllers = new ListControllers();
            _dataObjects = new DataObjects();
            _saveGame = new SaveDataRepository();

            if (inst == null) inst = this;
            else Destroy(gameObject);

            _ref = new Reference();
            _listControllers.Add(Reference.inst.radarController, "RadarController");

            var masMonoBehaviour = FindObjectsOfType<MonoBehaviour>().OfType<IUnit>();
            _fabricUnit = new FabricUnit (_listControllers, masMonoBehaviour.ToArray());
            
            new ScoresController(Reference.inst.playerData, _textScores);
            _listControllers.Add(new CameraController(Reference.inst.playerData, Reference.inst.Player.transform, Reference.inst.MainCamera.transform));
            _listControllers.Add(new CameraController(Reference.inst.playerData, Reference.inst.Player.transform, GameObject.FindGameObjectWithTag ("AudioListener").transform));
            _listControllers.Add(new LivesController(Reference.inst.playerData));
            _listControllers.Add(new GameOverController(Reference.inst.playerData));

            var maxScores = masMonoBehaviour.OfType<UnitView>().Where(x => x.GetTypeItem().type == TypeItem.Coin).Count();
            Debug.Log($"maxScores:{maxScores}");
            _listControllers.Add(new CongratulationsController(Reference.inst.playerData,maxScores));
        }

        private void Start()
        {
            _listControllers.Initialization();
        }

        private void Update()
        {
            _listControllers.Execute(Time.deltaTime);
        }

        private void LateUpdate()
        {
            _listControllers.LateExecute();
        }


    }
}