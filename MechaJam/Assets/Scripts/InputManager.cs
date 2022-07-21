using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace jam
{
    public class InputManager : MonoBehaviour
    {

        [SerializeField] Canvas _StartOverlay;
        [SerializeField] Canvas _ShoppingOverlay;
        [SerializeField] Canvas _PurchasedOverlay;

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
            _StartOverlay.enabled = true;
            _ShoppingOverlay.enabled = false;
            _PurchasedOverlay.enabled = false;
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

        public void OnEnterStore(InputValue value)
        {
            _bm.IsPaused = false;
            _StartOverlay.enabled = false;
            _ShoppingOverlay.enabled = true;
        }

        public void OnCancelPurchase(InputValue value)
        {
            _bm.IsPaused = true;
            _StartOverlay.enabled = true;
            _ShoppingOverlay.enabled = false;
        }

        public void OnConfirmPurchase(InputValue value)
        {
            _bm.IsPaused = true;
            _ShoppingOverlay.enabled = false;
            _PurchasedOverlay.enabled = true;
        }


        public void OnQuit(InputValue value)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

    }
}
