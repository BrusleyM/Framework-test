using System;
using UnityEngine;

namespace FrameworkTest
{
    [Serializable]
    public class PlayerData
    {
        #region Editor Fields
        [SerializeField]
        private string _name;
        [SerializeField]
        private int _health;
        [SerializeField]
        private int _score;
        [SerializeField]
        private Animator _animator;
        #endregion

        #region
        public string Name => _name;
        public int Health => _health;
        public int Score => _score;
        public Animator Animator => _animator;
        #endregion

        public PlayerData(string name, int health, int score, Animator animator)
        {
            _name = name;
            _health = health;
            _score = score;
            _animator = animator;
        }
    }
}
