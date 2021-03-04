using System.Linq;
using UnityEngine;
using TMPro;

namespace Hole
{

    internal sealed class GameController : MonoBehaviour
    {
        public Transform trash;
        [SerializeField] private TextMeshProUGUI _textScores;

        internal static GameController inst;
        private ListControllers _listControllers = new ListControllers();
        private DataObjects DataObjects = new DataObjects();

        private FabricUnit _fabricUnit;
        private Reference _ref;

        public static void SetTrash(GameObject go)
        {
            go.transform.SetParent(inst.trash);
        }

        private void Awake()
        {
            if (inst == null) inst = this;
            else Destroy(gameObject);

            _ref = new Reference();
            _fabricUnit =new FabricUnit (_listControllers, FindObjectsOfType<MonoBehaviour>().OfType<IUnit>().ToArray());
            
            new ScoresController(Reference.inst.playerData, _textScores);
            _listControllers.Add(new CameraController(Reference.inst.playerData, Reference.inst.Player.transform, Reference.inst.MainCamera.transform));
            _listControllers.Add(new CameraController(Reference.inst.playerData, Reference.inst.Player.transform, GameObject.FindGameObjectWithTag ("AudioListener").transform));

            _listControllers.Add(new LivesController(Reference.inst.playerData));
            _listControllers.Add(new GameOverController(Reference.inst.playerData));
            _listControllers.Add(new CongratulationsController(Reference.inst.playerData));
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