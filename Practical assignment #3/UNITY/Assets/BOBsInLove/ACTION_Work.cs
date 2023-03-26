using BTs;
using UnityEngine;

public class ACTION_Work : Action {

    public ACTION_Work() {
    }

    // FSM Instance
    private FSM_Work FSM;

    public override void OnInitialize() {
        FSM = ScriptableObject.CreateInstance<FSM_Work>();
        FSM.Construct(gameObject);
        FSM.OnEnter();
    }

    public override Status OnTick() {
        // FSM Update
        FSM.Update();

        // Success
        if (FSM.InSuccess())
            return Status.SUCCEEDED;

        // Failure
        if (FSM.InFailure())
            return Status.FAILED;
        
        // Running
        return Status.RUNNING;
    }

    public override void OnAbort() {
        FSM.OnExit();
    }

}
