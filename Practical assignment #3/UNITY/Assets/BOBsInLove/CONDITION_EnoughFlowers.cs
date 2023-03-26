using BTs;

public class CONDITION_EnoughFlowers : Condition {

    public CONDITION_EnoughFlowers() {
    }

    public override bool Check() {
        // Check the condition
        return ((BOB_Blackboard)blackboard).EnoughFlowers();
    }

}
