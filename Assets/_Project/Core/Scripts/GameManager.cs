using UnityEngine;

namespace YenTu.Core
{
    /// <summary>
    /// Quản lý trạng thái chung của trò chơi, vòng đời màn chơi và quy trình khởi tạo.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Game State")]
        [SerializeField] private bool isPaused;

        public bool IsPaused => isPaused;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SetPause(bool pause)
        {
            isPaused = pause;
            Time.timeScale = pause ? 0f : 1f;
        }
    }
}

