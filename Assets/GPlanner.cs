﻿using System.Collections.Generic;
using UnityEngine;

public class NodeG{

    public NodeG parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;

    // Constructor
    public NodeG(NodeG parent, float cost, Dictionary<string, int> allStates, GAction action) {

        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allStates);
        this.action = action;
    }
}

public class GPlanner {

    public Queue<GAction> plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates states) {

        List<GAction> usableActions = new List<GAction>();

        foreach (GAction a in actions) {

            if (a.IsAchievable()) {

                usableActions.Add(a);
            }
        }

        List<NodeG> leaves = new List<NodeG>();
        NodeG start = new NodeG(null, 0.0f, GWorld.Instance.GetWorld().GetStates(), null);

        bool success = BuildGraph(start, leaves, usableActions, goal);

        if (!success) {

            Debug.Log("NO PLAN");
            return null;
        }

        NodeG cheapest = null;
        foreach (NodeG leaf in leaves) {

            if (cheapest == null) {

                cheapest = leaf;
            } else if (leaf.cost < cheapest.cost) {

                cheapest = leaf;
            }
        }
        List<GAction> result = new List<GAction>();
        NodeG n = cheapest;

        while (n != null) {

            if (n.action != null) {

                result.Insert(0, n.action);
            }

            n = n.parent;
        }

        Queue<GAction> queue = new Queue<GAction>();

        foreach (GAction a in result) {

            queue.Enqueue(a);
        }

        Debug.Log("The Plan is: ");
        foreach (GAction a in queue) {

            Debug.Log("Q: " + a.actionName);
        }

        return queue;
    }

    private bool BuildGraph(NodeG parent, List<NodeG> leaves, List<GAction> usableActions, Dictionary<string, int> goal) {

        bool foundPath = false;
        foreach (GAction action in usableActions) {

            if (action.IsAchievableGiven(parent.state)) {

                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);

                foreach (KeyValuePair<string, int> eff in action.effects) {

                    if (!currentState.ContainsKey(eff.Key)) {

                        currentState.Add(eff.Key, eff.Value);
                    }
                }

                NodeG node = new NodeG(parent, parent.cost + action.cost, currentState, action);

                if (GoalAchieved(goal, currentState)) {

                    leaves.Add(node);
                    foundPath = true;
                } else {

                    List<GAction> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);

                    if (found) {

                        foundPath = true;
                    }
                }
            }
        }
        return foundPath;
    }

    private List<GAction> ActionSubset(List<GAction> actions, GAction removeMe) {

        List<GAction> subset = new List<GAction>();

        foreach (GAction a in actions) {

            if (!a.Equals(removeMe)) {

                subset.Add(a);
            }
        }
        return subset;
    }

    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state) {

        foreach (KeyValuePair<string, int> g in goal) {

            if (!state.ContainsKey(g.Key)) {

                return false;
            }
        }
        return true;
    }
}