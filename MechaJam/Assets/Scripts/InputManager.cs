using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace jam
{
    public class InputManager : MonoBehaviour
    {

        PlayerInput _inputs;
        [SerializeField] BasesManager _bm;

        [SerializeField] float _cooldownDelay = 5f;

        bool isCoolingDown = false;
        float _cooldown;

        private void Awake()
        {
            _inputs = GetComponent<PlayerInput>();
        }

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (!isCoolingDown)
            {
                return;
            }

            _cooldown -= Time.deltaTime;
            if (_cooldown <= 0f)
            {
                isCoolingDown = false;
            }
        }

        public void OnMoveFocus(InputValue value)
        {
            if (!isCoolingDown)
            {
                Vector2 v = value.Get<Vector2>();
                Debug.Log($"MoveFocus: {v}");
                Vector3 w;
                if (_bm.GetTargetPosition(transform.position, v, out w))
                {
                    Debug.Log($"New position: {w}");
                    transform.position = w;
                }
                else
                {
                    Debug.Log("No movement");
                }
                _cooldown = _cooldownDelay;
            }
        }

    }
}
