using UnityEngine;

namespace YenTu.Bosses
{
    /// <summary>
    /// Lớp cơ sở cho các Hộ Thần và Trùm (Bạch Ngạch Hổ Thần, Linh Mộc Mãng Xà, Kim Giáp Quy Thần, Phượng Hoàng, Mộc Ma).
    /// Hỗ trợ cơ chế scaling theo số lượng người chơi (Valheim-like) và hệ thống Thanh Tẩy (Purification).
    /// </summary>
    public abstract class BossBase : MonoBehaviour
    {
        [Header("Boss Identity")]
        [SerializeField] protected string bossName = "Hộ Thần";
        [SerializeField] protected int currentPhase = 1;

        [Header("Health & Scaling (Valheim-like)")]
        [SerializeField] protected float baseMaxHealth = 1000f;
        [SerializeField] protected float currentHealth;
        [SerializeField] protected float baseAttackPower = 50f;
        [SerializeField] protected float currentAttackPower;
        [SerializeField] protected float baseStaggerResistance = 100f;
        [SerializeField] protected float currentStaggerResistance;

        [Header("Purification State")]
        [SerializeField] protected bool isPurified = false;

        public bool IsPurified => isPurified;
        public int CurrentPhase => currentPhase;

        protected virtual void Awake()
        {
            ApplyPlayerScaling(1);
        }

        /// <summary>
        /// Scale thông số theo số lượng người chơi (1-4 players) theo triết lý Valheim-like:
        /// HP: 100% -> 150% -> 200% -> 250%
        /// Damage: 100% -> 103% -> 106% -> 110%
        /// </summary>
        public virtual void ApplyPlayerScaling(int playerCount)
        {
            playerCount = Mathf.Clamp(playerCount, 1, 4);

            float hpMultiplier = 1.0f + (playerCount - 1) * 0.5f; // 1: 100%, 2: 150%, 3: 200%, 4: 250%
            float dmgMultiplier = 1.0f + (playerCount - 1) * 0.033f; // 1: 100%, 4: ~110%
            float staggerMultiplier = 1.0f + (playerCount - 1) * 0.25f;

            currentHealth = baseMaxHealth * hpMultiplier;
            currentAttackPower = baseAttackPower * dmgMultiplier;
            currentStaggerResistance = baseStaggerResistance * staggerMultiplier;
        }

        public virtual void TakeDamage(float amount)
        {
            if (isPurified) return;

            currentHealth -= amount;
            if (currentHealth <= 0f)
            {
                currentHealth = 0f;
                OnHealthDepleted();
            }
        }

        protected abstract void OnHealthDepleted();

        /// <summary>
        /// Hộ Thần không bị tiêu diệt vĩnh viễn mà được thanh tẩy để giải thoát Tà Ấn.
        /// </summary>
        public virtual void Purify()
        {
            isPurified = true;
            Debug.Log($"[BossBase] {bossName} đã được thanh tẩy thành công!");
        }
    }
}

