using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[System.Serializable]
public class AgentData
{
    public string state;
    public int r0 = 3;
    public float time = 30f;
    public int age = 20;
}

public class AgentController : MonoBehaviour
{
    // Start is called before the first frame update
    public AgentData data;
    NavMeshAgent myNavMeshAgent;
    public GameObject[] points;
    private Image circle;
    int value = 0;
    void Start()
    {
        points = GameObject.FindGameObjectsWithTag("Point");
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        value = Random.Range(0, points.Length);
        data.r0 = Random.Range(2,4);
        data.age = Random.Range(0,100);
        foreach (Transform child in this.gameObject.transform)
        {
            if (child.name == "Area")
            {
                circle = child.GetChild(0).GetComponent<Image>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (data.state == "Sick")
        {
            data.time -= Time.deltaTime;
            if (data.time <= 0.0f)
            {
                data.state = "Recovered";
                circle.color = GetColorRGBA("#2B7800");
                // SimulationController1.recovered += 1;
            }
        }
        myNavMeshAgent.SetDestination(points[value].transform.position);
        float distanceToTarget = Vector3.Distance(transform.position, points[value].transform.position);
        if (distanceToTarget <= 0.6f){
            value = Random.Range(0, points.Length);
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Agent")
        {
            var agent = other.GetComponent<AgentController>().data;
            var state = agent.state;
            if ((state == "Sick") && (data.state == "Healthy") && (agent.r0 > 0))
            {
                data.state = "Sick";
                circle.color = GetColorRGBA("#730005");
                agent.r0 -= 1;
                // SimulationController1.sick += 1;
                // SimulationController1.healthy -= 1;
            }
        }
    }

    Color GetColorRGBA(string hex_color){
        Color new_color;
        ColorUtility.TryParseHtmlString(hex_color, out new_color);
        return new_color;
    }
}
