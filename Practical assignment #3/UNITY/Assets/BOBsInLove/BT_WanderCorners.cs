using BTs;
using UnityEngine;

[CreateAssetMenu(fileName = "BT_WanderCorners", menuName = "Behaviour Trees/BT_WanderCorners", order = 1)]
public class BT_WanderCorners : BehaviourTree {

    public BT_WanderCorners() {
    }

    public override void OnConstruction() {

        root = new RepeatForeverDecorator(
            new Sequence(
                new LambdaAction(() => {
                    GameObject corner = ((DAISY_Blackboard)blackboard).GetRandomCorner();
                    ((DAISY_Blackboard)blackboard).Put("theCorner", corner);
                    return Status.SUCCEEDED;
                }
                ),
                new ACTION_Arrive("theCorner")
            )
        );
    }

}
