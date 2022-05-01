using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class Loading : MonoBehaviour
    {
        private const float Speed = 60;
        private static Loading _instance; // singleton

        [SerializeField] private RectTransform image;
        private Canvas _canvas;

        private void Awake()
        {
            // destroy if clone
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            // make singleton
            _instance = this;
            DontDestroyOnLoad(gameObject);
            _canvas = GetComponent<Canvas>();
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void Update()
        {
            image.Rotate(Time.deltaTime * Speed * Vector3.back);
        }

        private void OnSceneChanged(Scene arg0, Scene arg1)
        {
            // find the main camera of the new level
            _canvas.worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            _canvas.enabled = false;
        }

        public static void Load()
        {
            _instance._canvas.enabled = true;
        }
    }
}