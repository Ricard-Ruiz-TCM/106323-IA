using BTs;
using UnityEngine;

[CreateAssetMenu(fileName = "BT_BuyPresent", menuName = "Behaviour Trees/BT_BuyPresent", order = 1)]
public class BT_BuyPresent : BehaviourTree {

    public BT_BuyPresent() {
    }

    public override void OnConstruction() {

        // The sequence is slightly modified, we assume that if Bob arrives at the jewellery,
        // he will certainly buy the ring or the necklace. Because of that we have added the
        // "PayJewel" action in the main sequence, followed by the Selector.
        root = new Sequence(
            new ACTION_Arrive("theJewellery"),
            new Selector(
                // Ring Sequence
                new Sequence(
                    new LambdaCondition(() => {
                        GameObject ring = GameObject.FindGameObjectWithTag("RING");
                        return ring != null;
                    }),
                    new LambdaAction(() => {
                        ((BOB_Blackboard)blackboard).ActivateRing();
                        return Status.SUCCEEDED;
                    })
                ),
                // Necklace Sequence
                new Sequence(
                    new LambdaAction(() => {
                        ((BOB_Blackboard)blackboard).ActivateNecklace();
                        return Status.SUCCEEDED;
                    })
                )
            ),
            // Pay for the Jewel
            new LambdaAction(() => {
                ((BOB_Blackboard)blackboard).PayJewel();
                return Status.SUCCEEDED;
            })
        );
    }

}
