using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
[RequireComponent(typeof(NavMeshAgent))]
public class Agent : MonoBehaviour
{
    [System.Serializable]
    public enum State{ LookForObjective, LookForGate, LookForLever}

    public List<Waypoint> allWaypoints;
    public List<Waypoint> allGateWaypoints = new List<Waypoint>();
    public List<Waypoint> allLeverWaypoints = new List<Waypoint>();
    public Waypoint objectiveWaypoint;

    public NavMeshPath path;

    public Waypoint currWayPoint;
    public bool decisionMade = false;
    public bool reachStatus;
    public State currState = State.LookForObjective;

    NavMeshAgent agent;

    [SerializeField] TextMeshProUGUI stateText;
    

    // Start is called at the start of the frame
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        foreach(Waypoint w in allWaypoints)
        {
            if(w.gameObject.tag.Contains("Lever"))
            {
                allLeverWaypoints.Add(w);
            }
            else if(w.gameObject.tag.Contains("Gate"))
            {
                allGateWaypoints.Add(w);
            }
            else if(w.gameObject.tag.Contains("Objective"))
            {
                objectiveWaypoint = w;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        stateText.text = currState.ToString();

        if(!decisionMade)
        {
            RunStateMachine();
        }
        // If waypoint is reached choose a new waypoint
        if(reachStatus)
        {
            reachStatus = false;

            switch(currWayPoint.tag)
            {
                case "LeverWaypoint":
                    allLeverWaypoints.Remove(currWayPoint);
                    currWayPoint = null;
                    break;
                case "GateWaypoint":
                    allGateWaypoints.Remove(currWayPoint);
                    currWayPoint = null;
                    break;
            }
            
            switch(currState)
            {
                case State.LookForGate:
                    currState = State.LookForObjective;
                    break;
                case State.LookForLever:
                    currState = State.LookForGate;
                    break;
            }
            

            //currState = State.LookForObjective;

            decisionMade = false;
            RunStateMachine();
        }

        // If close to enough to waypoint then set reachStatus to true
        if (currWayPoint != null && !this.GetComponent<NavMeshAgent>().pathPending && this.GetComponent<NavMeshAgent>().remainingDistance < 0.1f)
        {
            reachStatus = true;
        }
    }

    
    private void RunStateMachine()
    {
        switch(currState)
        {
            case State.LookForObjective:
                if(!GoToObjective())
                {
                    currState = State.LookForGate;
                }
                else
                {
                    currState = State.LookForObjective;
                    decisionMade = true;
                }
                break;
            case State.LookForGate:
                if(!GoToGate())
                {
                    currState = State.LookForLever;
                }
                else
                {
                    currState = State.LookForGate;
                    decisionMade = true;
                }
                break;
            case State.LookForLever:
                if (GoToLever())
                {
                    decisionMade = true;
                    currState = State.LookForLever;
                }
                else
                {
                    Debug.LogWarning("No Lever Found");
                }
                break;
        }
    }

    private bool GoToObjective()
    {
        currWayPoint = objectiveWaypoint;
        agent.CalculatePath(currWayPoint.transform.position, path);
        agent.destination = currWayPoint.transform.position;
        if (path.status == NavMeshPathStatus.PathPartial)
        {
            return false;
        }
        else return true;
    }

    private bool GoToLever()
    {
        foreach(Waypoint w in allLeverWaypoints)
        {
          
            currWayPoint = w;
            agent.CalculatePath(currWayPoint.transform.position, path);
            
            Debug.Log(w.name + " Path Status: " + path.status.ToString());
            if(path.status == NavMeshPathStatus.PathComplete)
            {
                agent.destination = currWayPoint.transform.position;
                return true;
            }
            
        }
        return false;
    }

    private bool GoToGate()
    {
        foreach (Waypoint w in allGateWaypoints)
        {
            currWayPoint = w;
            agent.CalculatePath(currWayPoint.transform.position, path);

            Debug.Log(w.name + " Path Status: " + path.status.ToString());
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                agent.destination = currWayPoint.transform.position;
                return true;
            }
            
        }
        return false;
    }

}
