using BTs;
using UnityEngine;

[CreateAssetMenu(fileName = "BT_MakeBouquet", menuName = "Behaviour Trees/BT_MakeBouquet", order = 1)]
public class BT_MakeBouquet : BehaviourTree {

    public BT_MakeBouquet() {
    }

    public override void OnConstruction() {

        DynamicSelector dynSelector = new DynamicSelector();

        dynSelector.AddChild(
            new CONDITION_EnoughFlowers(),
            // Activate Bouquet Action
            new LambdaAction(() => {
                ((BOB_Blackboard)blackboard).ActivateBouquet();
                return Status.SUCCEEDED;
            })
        );

        dynSelector.AddChild(
            new CONDITION_AlwaysTrue(),
            // Pick And Pee BT
            ScriptableObject.CreateInstance<BT_PickAndPee>()
        );

        root = dynSelector;
    }

}
