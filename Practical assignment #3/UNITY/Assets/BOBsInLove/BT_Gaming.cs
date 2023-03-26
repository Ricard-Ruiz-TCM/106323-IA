using BTs;
using UnityEngine;

[CreateAssetMenu(fileName = "BT_Gaming", menuName = "Behaviour Trees/BT_Gaming", order = 1)]
public class BT_Gaming : BehaviourTree {

    public BT_Gaming() {
    }

    public override void OnConstruction() {

        root = new Sequence(
            // Set Tag to "GAMING"
            new LambdaAction(() => {
                gameObject.tag = "GAMING";
                return Status.SUCCEEDED;
            }),
            new ACTION_Deactivate("theDuster"),
            new ACTION_Speak("Time to Play"),
            new ACTION_Arrive("theComputer"),
            new ACTION_RunForever()
        );
    }

}
