using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class Range : Entity
    {
        [SerializeField] private int seed;
        private Decision _decisionMaker;


        protected override void Awake()
        {
            base.Awake();
            _decisionMaker = FindObjectOfType<Decision>();
            MovementEnded += EndTurn;
        }

        public override void OnTurn()
        {
            base.OnTurn();
            _decisionMaker.AIDecision(this);
        }
    }
}
