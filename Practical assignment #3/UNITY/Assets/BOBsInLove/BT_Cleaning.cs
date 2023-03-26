using BTs;
using UnityEngine;

[CreateAssetMenu(fileName = "BT_Cleaning", menuName = "Behaviour Trees/BT_Cleaning", order = 1)]
public class BT_Cleaning : BehaviourTree {

    public BT_Cleaning() {

    }  

    public override void OnConstruction() {

        root = new Sequence(
            // Set Tag to "CLEANING"
            new LambdaAction(() => {
                gameObject.tag = "CLEANING";
                return Status.SUCCEEDED;
            }),
            new ACTION_Activate("theDuster"),
            new ACTION_Speak("Time to clean"),
            ScriptableObject.CreateInstance<BT_WanderCorners>()
        );
    }

}
