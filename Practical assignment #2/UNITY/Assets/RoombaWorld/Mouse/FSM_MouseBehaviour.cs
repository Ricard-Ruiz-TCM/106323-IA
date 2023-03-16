using FSMs;
using Steerings;
using UnityEngine;

[CreateAssetMenu(fileName = "FSM_MouseBehaviour", menuName = "Finite State Machines/FSM_MouseBehaviour", order = 1)]
public class FSM_MouseBehaviour : FiniteStateMachine {

    /** Blackboard */
    private MOUSE_Blackboard blackboard;

    /** Variables */
    private GoToTarget goToTarget;

    private float elapsedTime;

    /** OnEnter */
    public override void OnEnter() {

        /** GetComponent */
        blackboard = GetComponent<MOUSE_Blackboard>();
        goToTarget = GetComponent<GoToTarget>();

        goToTarget.target = blackboard.RandomDestination();

        /** OnEnter */
        base.OnEnter();
    }

    /** OnExit */
    public override void OnExit() {
        /** OnExit */
        base.OnExit();
    }

    /** OnConstruction */
    public override void OnConstruction() {

        /** States */
        State reachDestination = new State("reachDestination", () => { }, () => { }, () => { });

        State poo = new State("poo",
            () => { elapsedTime = 0.0f; },
            () => { elapsedTime = elapsedTime + Time.deltaTime; },
            () => { blackboard.Crap(transform.position); });

        State findExit = new State("findExit",
            () => { goToTarget.target = blackboard.RandomExitPoint(); },
            () => { }, () => { });

        State wait = new State("wait", () => { }, () => { }, () => { });

        /** Transitions */
        Transition destinationReached = new Transition("destinationReached",
            () => { return goToTarget.routeTerminated(); },
            () => { });

        Transition poopDone = new Transition("poopDone",
            () => { return elapsedTime >= blackboard.crapTimeDuration; },
            () => { });

        Transition exitReached = new Transition("exitReached",
            () => { return goToTarget.routeTerminated(); },
            () => { blackboard.DestroyMe(); });


        /** FSM Set Up */
        AddStates(reachDestination, poo, findExit, wait);
        /** ------------------------------------ */
        AddTransition(reachDestination, destinationReached, poo);
        /** -------------------------------------------------- */
        AddTransition(poo, poopDone, findExit);
        /** -------------------------------- */
        AddTransition(findExit, exitReached, wait);
        /** ------------------------------------------------ */
        initialState = reachDestination;

    }
}
