using TMPro;
using UnityEngine;

namespace Win
{
    public class WinScreenView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private string _format = "{0} won!";

        public void Open(string winnerName)
        {
            _text.text = string.Format(_format, winnerName);
            gameObject.SetActive(true);
        }
    }
}