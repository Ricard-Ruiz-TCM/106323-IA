using BTs;
using UnityEngine;

[CreateAssetMenu(fileName = "BT_PickAndPee", menuName = "Behaviour Trees/BT_PickAndPee", order = 1)]
public class BT_PickAndPee : BehaviourTree {

    public BT_PickAndPee() {
    }

    public override void OnConstruction() {

        DynamicSelector dynSelector = new DynamicSelector();

        dynSelector.AddChild(
            new CONDITION_NeedToPee(),
            // Pee BT on a Sequence
            new Sequence(
                ScriptableObject.CreateInstance<BT_Pee>(),
                new ACTION_RunForever()
            )
        );

        dynSelector.AddChild(
            new CONDITION_AlwaysTrue(),
            // Pick Flowers BT
            ScriptableObject.CreateInstance<BT_PickFlowers>()
        );

        root = dynSelector;
    }

}
