using BTs;

public class CONDITION_NeedToPee : Condition {

    public CONDITION_NeedToPee() {

    }

    public override bool Check() {
        return ((BOB_Blackboard)blackboard).PeeAlarmIsOn();
    }
}
