using BTs;
using UnityEngine;

[CreateAssetMenu(fileName = "BT_WaitForBOB", menuName = "Behaviour Trees/BT_WaitForBOB", order = 1)]
public class BT_WaitForBOB : BehaviourTree {

    public BT_WaitForBOB() {
    }

    public override void OnConstruction() {

        root = new Sequence(
            // Set Tag to "WAITING"
            new LambdaAction(() => {
                gameObject.tag = "WAITING";
                return Status.SUCCEEDED;
            }),
            new ParallelAnd(
                // Saying utterances
                new RepeatForeverDecorator(
                    new Sequence(
                        new LambdaAction(() => {
                            string utterance = ((DAISY_Blackboard)blackboard).GetRandomUtterance();
                            ((DAISY_Blackboard)blackboard).Put("theUtterance", utterance);
                            return Status.SUCCEEDED;
                        }),
                        new ACTION_Speak("theUtterance"),
                        new ACTION_WaitForSeconds("5")
                    )
                ),
                ScriptableObject.CreateInstance<BT_WanderCorners>()
            )
        );
    }

}
