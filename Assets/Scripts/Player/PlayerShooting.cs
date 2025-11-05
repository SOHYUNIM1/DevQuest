using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float range = 100f;           // 사거리
    public float damage = 10f;           // 데미지 (선택)
    public Camera fpsCam;                // 카메라 참조
    public GameObject hitEffectPrefab;   // 피격 이펙트 (선택)

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = fpsCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log($"Hit object: {hit.transform.name}");

            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Enemy hit detected!");

                // 피격 이펙트 (선택)
                if (hitEffectPrefab != null)
                    Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));

                // 넉백 효과
                Rigidbody enemyRb = hit.transform.GetComponent<Rigidbody>();
                if (enemyRb != null)
                {
                    Vector3 knockDir = (hit.transform.position - ray.origin).normalized;
                    enemyRb.AddForce(knockDir * 10f, ForceMode.Impulse);
                }
            }
        }
    }
}
