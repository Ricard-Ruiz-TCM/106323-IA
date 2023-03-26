using BTs;
using UnityEngine;

[CreateAssetMenu(fileName = "BT_Daisy", menuName = "Behaviour Trees/BT_Daisy", order = 1)]
public class BT_Daisy : BehaviourTree {

    public BT_Daisy() {
    }

    public override void OnConstruction() {

        DynamicSelector dynSelector = new DynamicSelector();

        // BOB arribes
        dynSelector.AddChild(
            new CONDITION_InstanceNear("45", "BOUQUET", "true", "theBouquet"),
            new Sequence(
                new ACTION_Deactivate("theBouquet"),
                new ACTION_Deactivate("theDuster"),
                new Selector(
                    // YES TO BOB
                    new Sequence(
                        new LambdaCondition(() => {
                            if (gameObject.tag.Equals("WAITING"))
                                return true;

                            if ((gameObject.tag.Equals("CLEANING")) && ((Random.Range(0f, 100f) > 50f)))
                                return false;

                            return false;
                        }),
                        new ACTION_Speak("Finally here!"),
                        new ACTION_WaitForSeconds("5"),
                        new ACTION_Quiet(),
                        new ACTION_Activate("hearts"),
                        new LambdaAction(() => { ((DAISY_Blackboard)blackboard).theAnswer.tag = "YES"; return Status.SUCCEEDED; }),
                        new LambdaAction(() => { ((DAISY_Blackboard)blackboard).CloseDoor(); return Status.SUCCEEDED; })
                    ),
                    // NO TO BOB
                    new Sequence(
                        new ACTION_Speak("Leave me alone!"),
                        new ACTION_WaitForSeconds("5"),
                        new ACTION_Quiet(),
                        new LambdaAction(() => { ((DAISY_Blackboard)blackboard).theAnswer.tag = "NO"; return Status.SUCCEEDED; }),
                        new LambdaAction(() => { ((DAISY_Blackboard)blackboard).CloseDoor(); return Status.SUCCEEDED; })
                    )
                )
            )
        );

        // Cleaning
        dynSelector.AddChild(
            new LambdaCondition(() => { return (Time.time % 60f < 8f); }),
            ScriptableObject.CreateInstance<BT_Cleaning>()
        );

        // Gaming
        dynSelector.AddChild(
            new LambdaCondition(() => { return (Time.time % 60f < 32f); }),
            ScriptableObject.CreateInstance<BT_Gaming>()
        );

        // Wait for BOB
        dynSelector.AddChild(
            new LambdaCondition(() => { return (Time.time % 60f < 60f); }),
            ScriptableObject.CreateInstance<BT_WaitForBOB>()
        );

        root = dynSelector;
    }
}
