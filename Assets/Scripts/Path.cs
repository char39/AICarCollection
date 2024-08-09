using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private Color lineColor;
    [SerializeField] private List<Transform> Nodes = new();

    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;                                               // Path의 색상을 설정합니다.
        Transform[] pathTransforms = GetComponentsInChildren<Transform>();      // Path의 모든 자식 오브젝트를 가져옵니다.
        Nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)             // Path의 모든 자식 오브젝트를 순회합니다.
        {
            if (pathTransforms[i] != transform)                         // 만약 현재 오브젝트가 Path 오브젝트가 아니라면
            {
                Nodes.Add(pathTransforms[i]);                               // Path의 노드로 추가합니다.
            }
        }

        for (int i = 0; i < Nodes.Count; i++)                           // Path의 모든 노드를 순회합니다.
        {
            Vector3 currentNode = Nodes[i].position;                        // 현재 노드의 위치를 가져옵니다.
            Vector3 previousNode = Vector3.zero;                            // 이전 노드의 위치를 초기화합니다.
            if (i > 0)                                                      // 만약 현재 노드가 첫 번째 노드가 아니라면
            {
                previousNode = Nodes[i - 1].position;                           // 이전 노드의 위치를 가져옵니다.
            }
            else if (i == 0 && Nodes.Count > 1)                             // 만약 현재 노드가 첫 번째 노드이고 노드의 개수가 1개 이상이라면
            {
                previousNode = Nodes[Nodes.Count - 1].position;                 // 이전 노드의 위치를 마지막 노드의 위치로 설정합니다.
            }
            Gizmos.DrawLine(previousNode, currentNode);                     // 이전 노드와 현재 노드를 선으로 연결합니다.
            Gizmos.DrawWireSphere(currentNode, 0.3f);                       // 현재 노드를 원으로 그립니다.
        }
    }


}
