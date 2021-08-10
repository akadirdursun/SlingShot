using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlingShotProject
{
    public class SlingString : MonoBehaviour
    {
        [SerializeField] private Transform leftPoint;
        [SerializeField] private Transform rightPoint;
        [SerializeField] private Transform stringSeat;
        private LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        private void Update()
        {
            lineRenderer.SetPositions(new Vector3[] { leftPoint.position, stringSeat.position, rightPoint.position });
        }
    }
}