using System.Collections.Generic;
using UnityEngine;

public class RollDice : MonoBehaviour
{
    [SerializeField] private Transform diceParent = null;
    
    private Transform dice = null;
    private Dictionary<int, PositionAndRotation> startingPropertiesForNumber = new Dictionary<int, PositionAndRotation>();

    private void Awake()
    {
        dice = diceParent.GetChild(0);

        startingPropertiesForNumber.Add(1, // DONE
            new PositionAndRotation(new Vector3(-0.000000556143277f, 2.0895327e-06f, 0.0147647019f), 
                Quaternion.Euler(-180f, 0f, 0f)));

        startingPropertiesForNumber.Add(2, // DONE
            new PositionAndRotation(new Vector3(0.000049948223f, 0.0000723293197f, 0.00000100458169f),
                Quaternion.Euler(0f, 0f, -90f)));

        startingPropertiesForNumber.Add(3, // DONE
            new PositionAndRotation(new Vector3(0.000000191780074f, -0.00738129439f, 0.00738169393f),
                Quaternion.Euler(-90f, 0f, 0f)));

        startingPropertiesForNumber.Add(4, // DONE
            new PositionAndRotation(new Vector3(-0.0000013184407f, 0.00738420524f, 0.00738321571f),
                Quaternion.Euler(-270f, 0f, 0f)));

        startingPropertiesForNumber.Add(5, // DONE
            new PositionAndRotation(new Vector3(0.0000509948695f,  -0.0000496434259f, 0.0160866845f), 
                Quaternion.Euler(180f, 0f, -90f)));

        startingPropertiesForNumber.Add(6, // DONE
            new PositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f)));
    }

    private void OnEnable()
    {
        RNGMechanic.ReRollEvent.AddListener(ShowUIOnRoll);
    }

    private void OnDisable()
    {
        RNGMechanic.ReRollEvent.RemoveListener(ShowUIOnRoll);
    }

    private void ShowUIOnRoll(int result)
    {
        if (startingPropertiesForNumber.ContainsKey(result))
        {
            PositionAndRotation localPositionAndRotation = startingPropertiesForNumber[result];
            dice.GetComponent<Rigidbody>().velocity = Vector3.zero;
            dice.SetPositionAndRotation(diceParent.position + localPositionAndRotation.Position,
                diceParent.rotation * localPositionAndRotation.Rotation);
        }
    }
}
