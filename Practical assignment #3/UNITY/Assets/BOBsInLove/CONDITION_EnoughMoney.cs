using BTs;

public class CONDITION_EnoughMoney : Condition {

    public CONDITION_EnoughMoney() {
    }

    public override bool Check() {
        return ((BOB_Blackboard)blackboard).EnoughMoney();
    }

}
