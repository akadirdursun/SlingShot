using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlingShotProject
{
    public class SlingString : MonoBehaviour
    {
        [SerializeField] private Transform leftStringNode;
        [SerializeField] private Transform rightStringNode;
        [SerializeField] private Transform stringSeat;        

        private LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            DrawSlingString();
        }

        private void DrawSlingString()
        {
            lineRenderer.SetPositions(new Vector3[] { leftStringNode.position, stringSeat.position, rightStringNode.position });
        }
    }
}