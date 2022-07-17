using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bootstrap
{
    public class GameLoader : MonoBehaviour
    {
        [SerializeField] [Min(0)] private int _gameSceneIndex = 1;

        private void Start()
        {
            SceneManager.LoadScene(_gameSceneIndex);
        }
    }
}