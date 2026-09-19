using UnityEngine;

namespace YenTu.Enemies
{
    /// <summary>
    /// Lớp cơ sở định nghĩa các thuộc tính, máu và hành vi chung của kẻ địch thường (Minions, U Ảnh).
    /// </summary>
    public abstract class EnemyBase : MonoBehaviour
    {
        [Header("Enemy Identity")]
        [SerializeField] protected string enemyId = "Enemy_Base";
        [SerializeField] protected string enemyDisplayName = "U Ảnh";

        [Header("Stats")]
        [SerializeField] protected float maxHealth = 100f;
        [SerializeField] protected float currentHealth;
        [SerializeField] protected float attackPower = 10f;
        [SerializeField] protected float moveSpeed = 3.5f;

        public bool IsDead => currentHealth <= 0f;

        protected virtual void Awake()
        {
            currentHealth = maxHealth;
        }

        public virtual void TakeDamage(float amount)
        {
            if (IsDead) return;

            currentHealth -= amount;
            if (currentHealth <= 0f)
            {
                currentHealth = 0f;
                OnDeath();
            }
        }

        protected virtual void OnDeath()
        {
            // Trạng thái tiêu biến / giải oan
            Destroy(gameObject, 1.5f);
        }
    }
}

