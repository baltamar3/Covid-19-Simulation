using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationController : MonoBehaviour
{
    // Start is called before the first frame update
    public static int healthy, recovered, sick;
    public Text labelHealthy, labelRecovered, labelSick;
    public static GameObject[] points;
    public static GameObject[] agents;

    void Awake()
    {
        points = GameObject.FindGameObjectsWithTag("Point");
        healthy = 109; //132
        recovered = 0;
        sick = 1;
    }

    void CreateAgents(int cant, string state)
    {
        for (int i = 0; i < cant; i++)
        {
            GameObject prefab = agents[Random.Range(0, agents.Length)];
            int value = Random.Range(0, SimulationController.points.Length);
            GameObject newAgent = Instantiate(prefab, points[value].transform.position, Quaternion.identity);
            var data = newAgent.GetComponent<AgentController>().data;
            data.state = state;
            data.age = Random.Range(0, 100);
            data.r0 = Random.Range(2,4);
        }
    }

    // Update is called once per frame
    void Update()
    {
        labelHealthy.text = healthy.ToString();
        labelRecovered.text = recovered.ToString();
        labelSick.text = sick.ToString();
    }
}
