using UnityEngine;

public class ChangeWeaponModel : MonoBehaviour
{
    [SerializeField] private GameObject[] armory;
    

    private void Awake()
    {
        RNGMechanic.ReRollEvent.AddListener(ChangeModel);
    }

    private void ChangeModel(int number)
    {
        for (int i = 0; i < armory.Length; i++)
            armory[i].SetActive(i == number % armory.Length);
    }
}
