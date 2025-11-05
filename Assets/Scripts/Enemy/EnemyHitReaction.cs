using UnityEngine;

public class EnemyHitReaction : MonoBehaviour
{
    public EnemyData enemyData; // ScriptableObject 참조
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // ScriptableObject 데이터 기반으로 초기화
        if (enemyData != null)
        {
            rb.mass = enemyData.mass;

            Collider col = GetComponent<Collider>();
            if (col != null && enemyData.physicMaterial != null)
                col.material = enemyData.physicMaterial;
        }
    }

    public void Knockback(Vector3 direction, float force)
    {
        if (rb == null || enemyData == null) return;

        // 넉백 저항 반영
        float effectiveForce = force / Mathf.Max(1f, enemyData.knockbackResistance);
        rb.AddForce(direction * effectiveForce, ForceMode.Impulse);
    }

    // (선택) 체력 감소 확장
    public void TakeDamage(float amount)
    {
        if (enemyData != null)
        {
            enemyData.health -= amount;
            if (enemyData.health <= 0)
            {
                gameObject.SetActive(false); // 풀링 고려 시 Destroy 대신 비활성화
            }
        }
    }
}
