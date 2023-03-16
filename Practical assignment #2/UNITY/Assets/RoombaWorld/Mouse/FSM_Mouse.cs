using FSMs;
using Steerings;
using UnityEngine;

[CreateAssetMenu(fileName = "FSM_Mouse", menuName = "Finite State Machines/FSM_Mouse", order = 1)]
public class FSM_Mouse : FiniteStateMachine {

    /** Blackboard */
    private MOUSE_Blackboard blackboard;

    /** Variables */
    private GoToTarget goToTarget;
    private PathFollowing pathFollowing;

    private GameObject theRoomba;
    private SpriteRenderer renderer;

    /** SteeringContext */
    private SteeringContext context;

    /** OnEnter */
    public override void OnEnter() {

        /** GetComponent */
        blackboard = GetComponent<MOUSE_Blackboard>();
        goToTarget = GetComponent<GoToTarget>();
        pathFollowing = GetComponent<PathFollowing>();
        renderer = GetComponent<SpriteRenderer>();

        context = GetComponent<SteeringContext>();

        // Set the innitla position of the mouse
        transform.position = blackboard.RandomExitPoint().transform.position;

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

    /** OnConstruction */
    public override void OnConstruction() {

        /** FSM's */
        FiniteStateMachine mouseBehaviour = ScriptableObject.CreateInstance<FSM_MouseBehaviour>();
        /** ----------------------------------------------------------------------------------- */
        mouseBehaviour.Name = "mouseBehaviour";
        /** -------------------------------- */

        /** States */
        State reachExit = new State("reachExit",
            () => { goToTarget.target = blackboard.NearestExitPoint(); },
            () => { }, () => { });

        State wait = new State("wait", () => { }, () => { }, () => { });

        /** Transitions */
        Transition roombaDetected = new Transition("roombaDetected",
            () => {
                theRoomba = SensingUtils.FindInstanceWithinRadius(gameObject, "ROOMBA", blackboard.roombaDetectionRadius);
                return theRoomba != null;
            },
            () => {
                context.maxSpeed *= 2;
                context.maxAcceleration *= 4;
                renderer.color = Color.green;
            });

        Transition exitReached = new Transition("exitReached",
            () => { return goToTarget.routeTerminated(); },
            () => { blackboard.DestroyMe(); });

        /** FSM Set Up */
        AddStates(mouseBehaviour, reachExit, wait);
        /** ------------------------------ */
        AddTransition(mouseBehaviour, roombaDetected, reachExit);
        /** -------------------------------------------------- */
        AddTransition(reachExit, exitReached, wait);
        /** ----------------------------------------------- */
        initialState = mouseBehaviour;

    }
}
