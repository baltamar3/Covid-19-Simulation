using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class SimulationController : MonoBehaviour
{
    // Start is called before the first frame update
    public static int healthy, recovered, sick, dead;
    public Text labelHealthy, labelRecovered, labelSick, labelDead, LabelTime;
    public static GameObject[] points;
    public GameObject[] agents;
    public static JSONNode items;
    float totalTime = 0.0f;

    void Awake()
    {
        points = GameObject.FindGameObjectsWithTag("Point");
        ReadJson();
        healthy = 109; //132
        recovered = 0;
        sick = 1;
        dead = 0;
        CreateAgents(109, "Healthy");
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
        Timer();
        labelHealthy.text = healthy.ToString();
        labelRecovered.text = recovered.ToString();
        labelSick.text = sick.ToString();
        labelDead.text = dead.ToString();
    }

    void Timer()
    {
        totalTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(totalTime / 60f);
        int seconds = Mathf.FloorToInt(totalTime - (minutes * 60));

        string niceTime = string.Format("{0:0}:{01:00}", minutes, seconds);
        LabelTime.text = niceTime;
    }  

    void ReadJson()
    {
        string data = Resources.Load<TextAsset>("mortality").text;
        items = JSONNode.Parse(data)["items"];
    }  
}
