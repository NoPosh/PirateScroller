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

        //���� � ���, ��� ���� ������ �������� ��������� ����� �� ���������� ��� ����������� ����� (�����)
        //��������: ��� ����������� ����� ����� -> ������ ���������� �� ������ ������� � ��������� ������ � ����������
        //� ������ ��������

        //��� ��������� ��, ��� �������� ������� �����? �� ��� ������ ����� � ������������? 
        //����� ����, ����� ����� ���� ������������ ������������ ������ (����� - ������, ����� - ����)

    }
}