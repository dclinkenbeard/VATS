using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public List<Vector3> spherePoints;
    public FlockBehavior behavior;

    [Range(1, 500)]
    public int startingCount = 250;

    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;

    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;

    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        spherePoints = GenerateSpherePoints();
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++) {
            FlockAgent newAgent = Instantiate(agentPrefab,
                transform.position + Random.insideUnitSphere * agentPrefab.transform.localScale.x * startingCount * AgentDensity,
                Quaternion.Euler(new Vector3(Random.Range(0f,360f), Random.Range(0f, 360f), Random.Range(0f, 360f))), transform);

            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockAgent agent in agents) {
            List<Transform> context = GetNearbyObjects(agent);

            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed) {
                move = move.normalized * maxSpeed;
            }

            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent) {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);

        foreach (Collider c in contextColliders) {
            if (c != agent.AgentCollider) {
                context.Add(c.transform);
            }
        }
        return context;
    }

    public List<Vector3> GenerateSpherePoints() {

        List<Vector3> spherePointsList = new List<Vector3>();
        int numPoints = 1000;
        float turnFraction = 0.618033f;
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (numPoints - 1f);
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = 2 * Mathf.PI * turnFraction * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);

            spherePointsList.Add(new Vector3(x, y, z));
        }

        return spherePointsList; 
    }
}

