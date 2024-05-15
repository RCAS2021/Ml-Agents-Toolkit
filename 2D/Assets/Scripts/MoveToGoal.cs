using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    // Adicionando as posições das 4 paredes
    [SerializeField] private Transform wall1Transform;
    [SerializeField] private Transform wall2Transform;
    [SerializeField] private Transform wall3Transform;
    [SerializeField] private Transform wall4Transform;

    // Foram modificados os vetores, antes a aleatorização ocorria nos eixos X e Z, agora ocorrem nos eixos X e Y
    public override void OnEpisodeBegin(){
        transform.localPosition = new Vector3(Random.Range(2f, -6f), Random.Range(3.8f, -4.4f), 0);
        targetTransform.localPosition = new Vector3(Random.Range(2f, -6f), Random.Range(3.8f, -4.4f), 0);
    }

    // Como o agente observa o ambiente - não houveram modificações
    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);

        // Obtendo distância do alvo
        float distanceToTarget = Vector3.Distance(targetTransform.localPosition, transform.localPosition);

        // Adicionando observação de distância do alvo
        sensor.AddObservation(distanceToTarget);

        // Obtendo vetor para o alvo mais próximo
        Vector3 toTarget = targetTransform.localPosition - transform.localPosition;

        sensor.AddObservation(toTarget.normalized);

        // Obtendo distância das 4 paredes
        float distanceToWall1 = Vector3.Distance(wall1Transform.localPosition, transform.localPosition);
        float distanceToWall2 = Vector3.Distance(wall2Transform.localPosition, transform.localPosition);
        float distanceToWall3 = Vector3.Distance(wall3Transform.localPosition, transform.localPosition);
        float distanceToWall4 = Vector3.Distance(wall4Transform.localPosition, transform.localPosition);

        // Obtendo vetor para as paredes
        Vector3 toWall1 = wall1Transform.localPosition - transform.localPosition;
        Vector3 toWall2 = wall2Transform.localPosition - transform.localPosition;
        Vector3 toWall3 = wall3Transform.localPosition - transform.localPosition;
        Vector3 toWall4 = wall4Transform.localPosition - transform.localPosition;

        // Adicionando observações das distâncias até a parede
        sensor.AddObservation(toWall1.normalized);
        sensor.AddObservation(toWall2.normalized);
        sensor.AddObservation(toWall3.normalized);
        sensor.AddObservation(toWall4.normalized);
    }

    // Ações do agente - Foi modificado o vetor para utilizar a movimentação nos eixos X e Y ao invés dos eixos X e Z
    public override void OnActionReceived(ActionBuffers actions){
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        float moveSpeed = 3f;
        transform.localPosition += new Vector3(moveX, moveY, 0) * Time.deltaTime * moveSpeed;
    }

    // Teste Heurístico
    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    // Recompensas do agente - Foi modificado o método utilizado para a versão 2D, com o colisor 2D
    private void OnTriggerEnter2D(Collider2D other){
        if (other.TryGetComponent<Goal>(out Goal goal)){
            SetReward(+1f);
            Debug.Log("Collided with goal");
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall)){
            SetReward(-1f);
            Debug.Log("Collided with wall");
            EndEpisode();
        }
    }

    // Recompensas do agente - Foi modificado o método utilizado para a versão 2D, com o colisor 2D
    private void OnTriggerStay2D(Collider2D other){
        if (other.TryGetComponent<Goal>(out Goal goal)){
            SetReward(+1f);
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall)){
            SetReward(-1f);
            EndEpisode();
        }
    }
}

