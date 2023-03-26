using BTs;
using UnityEngine;

[CreateAssetMenu(fileName = "BT_PickFlowers", menuName = "Behaviour Trees/BT_PickFlowers", order = 1)]
public class BT_PickFlowers : BehaviourTree {

    public BT_PickFlowers() {
    }

    public override void OnConstruction() {

        DynamicSelector dynSelector = new DynamicSelector();
    
        dynSelector.AddChild(
            new CONDITION_InstanceNear("flowerDetectionRadius", "FLOWER", "false", "theFlower"),
            // PickUpFlower Sequence
            new Sequence(
                    new ACTION_Arrive("theFlower"),
                    new ACTION_Deactivate("theFlower"),
                    new LambdaAction(() => {
                        ((BOB_Blackboard)blackboard).CountFlower();
                        return Status.SUCCEEDED;
                    })
                )
        );

        dynSelector.AddChild(
            new CONDITION_AlwaysTrue(),
            // Constrained Wander
            new ACTION_CWander("thePark", "80", "40", "0.2", "0.8")
        );

        root = new RepeatForeverDecorator(dynSelector);
    }

}
