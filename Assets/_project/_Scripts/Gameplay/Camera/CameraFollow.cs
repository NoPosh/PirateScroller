using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Gameplay.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera _virtualCam;

        [SerializeField] Transform LFarPoint;
        [SerializeField] Transform LPoint;
        [SerializeField] Transform RFarPoint;
        [SerializeField] Transform RPoint;

        //Суть в том, что этот объект начинает двигаться вслед за персонажем при пересечении точек (линий)
        //Например: При пересечении самой левой -> капера перелетает на правую ближнюю и двигается вместе с персонажем
        //С другой наоборот

        //Как детектить то, что персонаж пересек линии? Мб это просто точки в пространстве? 
        //Также надо, чтобы можно было ограничивать передвижение камеры (влево - вправо, вверх - вниз)

    }
}