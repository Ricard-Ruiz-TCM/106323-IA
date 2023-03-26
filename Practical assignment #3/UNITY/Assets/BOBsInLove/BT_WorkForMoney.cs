using BTs;
using UnityEngine;

[CreateAssetMenu(fileName = "BT_WorkForMoney", menuName = "Behaviour Trees/BT_WorkForMoney", order = 1)]
public class BT_WorkForMoney : BehaviourTree {

    public BT_WorkForMoney() {
    }

    public override void OnConstruction() {

        root = new RepeatUntilSuccessDecorator(
            new Selector(
                new CONDITION_EnoughMoney(),
                // FSM Work Working (RUS)
                new Sequence(
                    new RepeatUntilSuccessDecorator(new ACTION_Work()),
                    new LambdaAction(() => {
                        ((BOB_Blackboard)blackboard).GetPaid();
                        return Status.SUCCEEDED;
                    }),
                    new ACTION_Fail()
                )
            )
        );
    }

}
