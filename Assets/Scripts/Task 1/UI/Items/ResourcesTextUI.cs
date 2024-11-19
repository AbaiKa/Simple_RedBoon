using TMPro;
using UnityEngine;

public class ResourcesTextUI : MonoBehaviour
{
    [SerializeField] private string resourceId;
    [SerializeField] private TextMeshProUGUI textComponent;

    public void UpdateText(string key, int count)
    {
        if (resourceId.Contains(key))
        {
            textComponent.text = count.ToString();
        }
    }
}
