using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEvent : MonoBehaviour
{
    public delegate void GoalEventDelegate();
    public static GoalEventDelegate OnGoal;
}
