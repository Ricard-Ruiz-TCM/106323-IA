using BTs;
using UnityEngine;

[CreateAssetMenu(fileName = "BT_DrinkToFailure", menuName = "Behaviour Trees/BT_DrinkToFailure", order = 1)]
public class BT_DrinkToFailure : BehaviourTree {
  
    public BT_DrinkToFailure() {
    }

    public override void OnConstruction() {

        root = new RepeatUntilSuccessDecorator(
            new Selector(
                new Sequence(
                    new LambdaCondition(() => {
                        if (Random.Range(0f, 100f) < 15f) {
                            return true;
                        }
                        return false;
                    }),
                    new LambdaAction(() => {
                        gameObject.tag = "KO";
                        return Status.SUCCEEDED;
                    })
                ),
                new Sequence(
                    new ACTION_Speak("Another one master"),
                    new ACTION_WaitForSeconds("2"),
                    new ACTION_Quiet(),
                    new ACTION_WaitForSeconds("drinkTime"),
                    new ACTION_Somersault(),
                    new ACTION_Fail()
                )
            )
        );
    }

}
