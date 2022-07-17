using TMPro;
using UnityEngine;

public class ShowRollOnUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void OnEnable()
    {
        RNGMechanic.ReRollEvent.AddListener(UpdateText);
    }

    private void OnDisable()
    {
        RNGMechanic.ReRollEvent.RemoveListener(UpdateText);
    }

    private void UpdateText(int result)
    {
        text.text = (result + 1).ToString();
    }
}
