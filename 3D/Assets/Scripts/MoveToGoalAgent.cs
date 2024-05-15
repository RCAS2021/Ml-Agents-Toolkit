using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;

    public override void OnEpisodeBegin(){
        transform.localPosition = new Vector3(Random.Range(3f, -4f), 0, Random.Range(5f, -2f));
        targetTransform.localPosition = new Vector3(Random.Range(3f, -4f), 0, Random.Range(5f, -2f));
    }

    // How the agent observes the environment
    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);

        // Getting the distance to target
        float distanceToTarget = Vector3.Distance(targetTransform.localPosition, transform.localPosition);

        // Add observation for distance to nearest target
        sensor.AddObservation(distanceToTarget);

        // Getting vector to nearest target
        Vector3 toTarget = targetTransform.localPosition - transform.localPosition;

        sensor.AddObservation(toTarget.normalized);
    }

    // Agent actions
    public override void OnActionReceived(ActionBuffers actions){
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 3f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    // Heuristic Testing
    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    // Agent rewards
    private void OnTriggerEnter(Collider other){
        if (other.TryGetComponent<Goal>(out Goal goal)){
            SetReward(+1f);
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall)){
            SetReward(-1f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }
    }

    private void OnTriggerStay(Collider other){
        if (other.TryGetComponent<Goal>(out Goal goal)){
            SetReward(+1f);
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall)){
            SetReward(-1f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }
    }
}
