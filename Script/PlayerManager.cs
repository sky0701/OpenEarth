using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();  // 해당 컴포넌트의 PhotonView를 가져와라
    }

    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }

    }

    void CreateController()
    {
        Debug.Log("Instantiated Player");
    }

}
