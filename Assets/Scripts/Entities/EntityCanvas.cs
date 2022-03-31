using System;
using UnityEngine;

namespace Entities
{
    public class EntityCanvas : MonoBehaviour
    {
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void OnPointerEnter()
        {
            print("Hello");
            _canvas.enabled = true;
        }

        public void OnPointerExit()
        {
            _canvas.enabled = false;
        }
    }
}