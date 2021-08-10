using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlingShotProject
{
    public class SlingManager : MonoBehaviour
    {
        [SerializeField] private TrajectoryPrediction trajectoryPrediction;
        [Header("Positions")]
        [SerializeField] private float forceValue = 5f;
        [SerializeField] private float maxStretchZ = 1.5f;
        [SerializeField] private float maxStretchX = 0.5f;
        [SerializeField] private Transform slingSeat;
        [SerializeField] private Transform IdlePos;

        [Header("DataSO")]
        [SerializeField] private InputManager inputManager;
        [SerializeField] private PoolObjects objectPool;

        private Vector3 defaultSeatPos;
        private Vector3 seatOffset = new Vector3(0, -0.5f, -0.35f);
        private Vector3 characterOffset = new Vector3(0, -0.40f, 0.175f);
        private Vector3 shootYOffset = new Vector3(0, 0.5f, 0);
        private float stretch;
        private Character currentCharacter;

        private void Start()
        {
            defaultSeatPos = slingSeat.position;
            SetTheCharacterObject();
        }

        private void OnEnable()
        {
            inputManager.onTouchDrag += OnTouchDrag;
            inputManager.onTouchPressStarted += OnTouchPressStarted;
            inputManager.onTouchPressCanceled += OnTouchPressCanceled;
        }

        private void OnDisable()
        {
            inputManager.onTouchDrag -= OnTouchDrag;
            inputManager.onTouchPressStarted -= OnTouchPressStarted;
            inputManager.onTouchPressCanceled -= OnTouchPressCanceled;
        }

        private void SetTheCharacterObject()
        {
            currentCharacter = objectPool.GetNextObject().GetComponent<Character>();
            currentCharacter.transform.SetParent(slingSeat);
            currentCharacter.transform.position = IdlePos.position;
            currentCharacter.gameObject.SetActive(true);
        }
        private void OnTouchPressStarted()
        {
            slingSeat.position += seatOffset;
            currentCharacter.PutOnSeat(characterOffset + slingSeat.position);
        }

        private void OnTouchDrag(Vector2 position)
        {
            slingSeat.position += new Vector3(position.x, 0, position.y);
            float seatPosX = Mathf.Clamp(slingSeat.position.x, defaultSeatPos.x - maxStretchX, defaultSeatPos.x + maxStretchX);
            float seatPosZ = Mathf.Clamp(slingSeat.position.z, defaultSeatPos.z - maxStretchZ, defaultSeatPos.z + seatOffset.z);
            slingSeat.position = new Vector3(seatPosX, slingSeat.position.y, seatPosZ);
            DrawTrajectoryPrediction();
        }


        private void OnTouchPressCanceled(Vector2 pos)
        {
            Vector3 velocity = CalculateVelocity();
            if (stretch < 0.5f)
            {
                slingSeat.position = defaultSeatPos;
                currentCharacter.LiftOffSeat(IdlePos.position);
                return;
            }            
            currentCharacter.Shoot(velocity);
            slingSeat.position = defaultSeatPos;
            SetTheCharacterObject();
        }

        private Vector3 CalculateVelocity()
        {
            stretch = defaultSeatPos.z - slingSeat.position.z;
            Vector3 dir = (defaultSeatPos - slingSeat.position + shootYOffset).normalized;            
            return dir * stretch * forceValue;
        }
    
        private void DrawTrajectoryPrediction()
        {
            Vector3 velocity = CalculateVelocity();
            if (stretch < 0.5f)
            {
                trajectoryPrediction.ClearPrediction();
                return;
            }
            trajectoryPrediction.DrawPrediction(velocity);
        }
    }
}