using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [Header("-----Camera-----")]
    [SerializeField]
    Camera m_cam;

    public void ShakeCamera(){
        m_cam.DOShakePosition(1, new Vector3(2, 2, 2));
    }
}
