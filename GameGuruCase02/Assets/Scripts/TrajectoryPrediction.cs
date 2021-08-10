using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlingShotProject
{
    public class TrajectoryPrediction : MonoBehaviour
    {
        [SerializeField] private int numOfPoints = 30;
        [SerializeField] private float timeBetweenPoints = 0.1f;
        [SerializeField] private LayerMask collidableLayers;
        private LineRenderer lineRenderer;
        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public void DrawPrediction(Vector3 shootVelocity)
        {
            lineRenderer.positionCount = numOfPoints;
            List<Vector3> points = new List<Vector3>();
            Vector3 startPos = transform.position;
            for (float i = 0; i < numOfPoints; i += timeBetweenPoints)
            {
                Vector3 newPoint = startPos + i * shootVelocity;
                newPoint.y = startPos.y + shootVelocity.y * i + Physics.gravity.y / 2f * i * i;
                points.Add(newPoint);

                if (Physics.OverlapSphere(newPoint, 2, collidableLayers).Length > 0)
                {
                    lineRenderer.positionCount = points.Count;
                    break;
                }
            }
            lineRenderer.SetPositions(points.ToArray());
        }

        public void ClearPrediction()
        {
            lineRenderer.positionCount = 0;
        }
    }
}