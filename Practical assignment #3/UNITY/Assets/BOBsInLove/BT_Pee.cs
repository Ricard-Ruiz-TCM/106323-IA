using BTs;
using UnityEngine;

[CreateAssetMenu(fileName = "BT_Pee", menuName = "Behaviour Trees/BT_Pee", order = 1)]
public class BT_Pee : BehaviourTree {

    public BT_Pee() {
    }

    public override void OnConstruction() {

        // Complete Pee Sequence
        root = new Sequence(
            new ACTION_Speak("Gotta take a leak"),
            new ACTION_Arrive("theToilet"),
            new LambdaAction(() => {
                ((BOB_Blackboard)blackboard).CloseDoor();
                return Status.SUCCEEDED;
            }),
            new ACTION_WaitForSeconds("4"),
            new LambdaAction(() => {
                ((BOB_Blackboard)blackboard).OpenDoor();
                return Status.SUCCEEDED;
            }),
            new ACTION_Speak("Oh!!! I needed this"),
            new ACTION_WaitForSeconds("2"),
            new ACTION_Quiet(),
            new LambdaAction(() => {
                ((BOB_Blackboard)blackboard).PeeAlarmOff();
                return Status.SUCCEEDED;
            })
        );
    }

}
