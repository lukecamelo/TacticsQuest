using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 randomizeOffset;
    [SerializeField] private Color damageColor;

    private TextMeshPro damageText;

    void Awake() {
        damageText = GetComponent<TextMeshPro>();
        transform.localPosition += offset;

        transform.localPosition += new Vector3(
            Random.Range(-randomizeOffset.x, randomizeOffset.x),
            Random.Range(-randomizeOffset.y, randomizeOffset.y),
            Random.Range(-randomizeOffset.z, randomizeOffset.z)
        );
        
        Destroy(gameObject, destroyTime);
    }

    public void Initialize(int damageValue) {
        damageText.text = damageValue.ToString();
    }
}
