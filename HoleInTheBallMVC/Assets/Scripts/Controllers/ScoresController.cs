using TMPro;

namespace Hole
{
    internal sealed class ScoresController
    {
        UnitM _playerData;
        TextMeshProUGUI _textScores;
        private ControlLeak _controlLeak = new ControlLeak("Score");

        internal ScoresController(UnitM playerData, TextMeshProUGUI textScores)
        {
            _playerData = playerData;
            _playerData.evtScores += UpdateScores;
            _textScores = textScores;
        }

        private void UpdateScores()
        {
            _textScores.text = _playerData.Scores.ToString();
        }

    }
}
