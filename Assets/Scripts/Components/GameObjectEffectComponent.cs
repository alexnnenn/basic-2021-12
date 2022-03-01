using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public sealed class GameObjectEffectComponent : BaseEffectComponent
    {
        [SerializeField]
        private GameObject _prototype;

        [SerializeField]
        private float _ttl;

        private Queue<GameObject> _pool = new Queue<GameObject>();

        public override void Play()
        {
            var obj = GetOrCreate();

            obj.SetActive(true);
            obj.transform.SetParent(transform, false);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
            StartCoroutine(ReturnToPool(obj));
        }

        private IEnumerator ReturnToPool(GameObject obj)
        {
            yield return new WaitForSeconds(_ttl);
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }

        private GameObject GetOrCreate()
        {
            if(_pool.Count > 0)
            {
                return _pool.Dequeue();
            }

            return Instantiate(_prototype);
        }
    }
}
