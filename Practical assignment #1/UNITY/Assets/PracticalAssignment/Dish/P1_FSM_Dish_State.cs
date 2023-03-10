using FSMs;
using UnityEngine;

[CreateAssetMenu(fileName = "P1_FSM_Dish_State", menuName = "Finite State Machines/P1_FSM_Dish_State", order = 1)]
public class P1_FSM_Dish_State : FiniteStateMachine {

    /** Blackboard */
    private P1_DishController blackboard;

    /** Variables */
    private SpriteRenderer renderer;

    /** OnEnter */
    public override void OnEnter() {

        /** GetComponent */
        renderer = GetComponent<SpriteRenderer>();
        blackboard = GetComponent<P1_DishController>();

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
        State clean = new State("clean",
            () => {
                gameObject.tag = "DISH_CLEAN";
                renderer.sprite = blackboard.cleanSprite;
            }, () => { }, () => { });

        State withFood = new State("withFood",
            () => {
                gameObject.tag = "DISH_IN_USE";
                renderer.sprite = blackboard.dirtySprite;
            }, () => { }, () => { });

        State dirty = new State("dirty",
            () => { gameObject.tag = "DISH_DIRTY"; },
            () => { }, () => { });

        State picked = new State("picked",
            () => { gameObject.tag = "DISH_PICKED"; },
            () => { }, () => { });

        /** Transitions */
        Transition some1UsedMe = new Transition("some1UsedMe",
            () => { return blackboard.state.Equals(DishState.withFood); },
            () => { });

        Transition some1EatAll = new Transition("some1EatAll",
            () => {
                return blackboard.state.Equals(DishState.dirty);
            }, () => { });

        Transition some1CleanedMe = new Transition("some1CleanedMe",
            () => {
                return blackboard.state.Equals(DishState.clean);
            }, () => { });

        Transition some1PickedMe = new Transition("some1PickedMe",
            () => {
                return blackboard.state.Equals(DishState.picked);
            }, () => { });

        /** FSM Set Up */
        AddStates(clean, withFood, dirty, picked);
        /** ------------------------------------- */
        AddTransition(clean, some1UsedMe, withFood);
        AddTransition(withFood, some1EatAll, dirty);
        AddTransition(dirty, some1PickedMe, picked);
        AddTransition(picked, some1CleanedMe, clean);
        /** -------------------------------------- */
        initialState = clean;

    }

}