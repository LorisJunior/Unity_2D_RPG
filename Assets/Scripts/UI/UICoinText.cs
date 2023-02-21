using TMPro;
using UnityEngine;

public class UICoinText : MonoBehaviour
{
    TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    public void IncreaseCoinUI(int amount)
    {
        tmp.text = "x " + amount; 
    }
}
