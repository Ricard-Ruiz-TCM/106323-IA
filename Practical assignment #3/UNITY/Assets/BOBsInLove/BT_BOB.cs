using BTs;
using UnityEngine;

[CreateAssetMenu(fileName = "BT_BOB", menuName = "Behaviour Trees/BT_BOB", order = 1)]
public class BT_BOB : BehaviourTree {
       
    public BT_BOB() {
    }

    public override void OnConstruction() {

        root = new Sequence(
            ScriptableObject.CreateInstance<BT_WorkForMoney>(),
            ScriptableObject.CreateInstance<BT_MakeBouquet>(),
            ScriptableObject.CreateInstance<BT_BuyPresent>(),

            new ACTION_Arrive("daisys"),

            // Wait for daisy answer
            new RepeatUntilSuccessDecorator(
                new Sequence(
                    new Selector(
                        new CONDITION_InstanceNear("55", "NO"),
                        new CONDITION_InstanceNear("55", "YES")
                    )
                )
            ),

            // Left the ring or necklace on daisy rooms and go to drink
            new LambdaAction(() => {
                ((BOB_Blackboard)blackboard).DeActivateNecklace();
                return Status.SUCCEEDED;
            }),
            new LambdaAction(() => {
                ((BOB_Blackboard)blackboard).DeActivateRing();
                return Status.SUCCEEDED;
            }),

            new ACTION_Arrive("pub"),
            ScriptableObject.CreateInstance<BT_DrinkToFailure>()
        );
    }

}
