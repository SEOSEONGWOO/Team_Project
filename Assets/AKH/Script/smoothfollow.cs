using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace UnityStandardAssets.Utility
{
    public class smoothfollow : MonoBehaviour
    {
        [SerializeField]
        public Transform target;

        [SerializeField]
        private float distance = 10.0f;
    }
}
