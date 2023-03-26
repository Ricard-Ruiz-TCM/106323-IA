using BTs;
using UnityEngine;

[CreateAssetMenu(fileName = "BT_Ambulance", menuName = "Behaviour Trees/BT_Ambulance", order = 1)]
public class BT_Ambulance : BehaviourTree {

    public BT_Ambulance() {
    }

    public override void OnConstruction() {

        root = new Sequence(
            new RepeatUntilSuccessDecorator(
                new Sequence(
                    new ACTION_WaitForSeconds("ambulanceWorkFrequency"),
                    new CONDITION_InstanceNear("personDetectionRadius", "KO", "false", "thePerson")
                )
            ),
            new ACTION_Arrive("thePerson"),
            new ACTION_Take("thePerson"),
            new ACTION_Arrive("theHospital")
        );
    }

}
