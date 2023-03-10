using FSMs;
using Steerings;
using UnityEngine;

[CreateAssetMenu(fileName = "P1_FSM_Worker_AntKiller", menuName = "Finite State Machines/P1_FSM_Worker_AntKiller", order = 1)]
public class P1_FSM_Worker_AntKiller : FiniteStateMachine {

    /** Blackboard */
    private P1_Worker_Blackboard blackboard;

    /** Variables */
    private Arrive arrive;
    private float elapsedTime;

    /** OnEnter */
    public override void OnEnter() {

        /** GetComponent */
        arrive = GetComponent<Arrive>();
        blackboard = GetComponent<P1_Worker_Blackboard>();

        /** OnEnter */
        base.OnEnter();
    }

    /** OnExit */
    public override void OnExit() {

        /** DisableSteerings */
        base.DisableAllSteerings();
        /** OnExit */
        base.OnExit();
    }

    public override void OnConstruction() {

        /** States */
        State reachAnt = new State("reachAnt",
            () => {
                arrive.enabled = true;
                arrive.target = blackboard.theAnt;
            },
            () => { },
            () => { arrive.enabled = false; });

        State killAnt = new State("killAnt",
            () => { elapsedTime = 0.0f; },
            () => { elapsedTime += Time.deltaTime; },
            () => { blackboard.KillAnt(); });

        /** Transitions */
        Transition antReached = new Transition("antDetected",
            () => {
                return SensingUtils.DistanceToTarget(gameObject, blackboard.theAnt) < blackboard.antReachDistance;
            }, () => { });

        Transition antKilled = new Transition("antNotDetected",
            () => {
                return elapsedTime > blackboard.killAntTime;
            }, () => { });

        /** FSM Set Up */
        AddStates(reachAnt, killAnt);
        /** ---------------------- */
        AddTransition(killAnt, antKilled, reachAnt);
        /** ------------------------------------- */
        AddTransition(reachAnt, antReached, killAnt);
        /** -------------------------------------- */
        initialState = reachAnt;
    }

}
