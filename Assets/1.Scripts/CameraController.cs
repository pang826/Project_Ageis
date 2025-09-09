using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _plane;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _minFov;
    [SerializeField] private float _maxFov;

    [SerializeField] private float _scrollSpeed;      // 카메라 이동 속도
    [SerializeField] private float _edgeSize;         // 스크롤 감지 영역 (픽셀 단위)
    private float minX, maxX, minZ, maxZ;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    private void Start()
    {
        // Plane 크기 계산
        Vector3 size = _plane.localScale;
        float width = size.x * 3f;
        float height = size.z * 3f;

        // Plane 중심 기준 경계 설정
        Vector3 center = _plane.position;
        minX = center.x - width / 2f;
        maxX = center.x + width / 2f;
        minZ = center.z - height / 2f;
        maxZ = center.z + height / 2f;
    }
    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            _camera.fieldOfView -= scroll * _zoomSpeed;
            _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, _minFov, _maxFov);
        }

        Vector3 pos = _camera.transform.position;
        Vector3 mousePos = Input.mousePosition;

        // 화면 왼쪽
        if (mousePos.x <= _edgeSize)
            pos.x -= _scrollSpeed * Time.deltaTime;
        // 화면 오른쪽
        else if (mousePos.x >= Screen.width - _edgeSize)
            pos.x += _scrollSpeed * Time.deltaTime;

        // 화면 아래
        if (mousePos.y <= _edgeSize)
            pos.z -= _scrollSpeed * Time.deltaTime;
        // 화면 위
        else if (mousePos.y >= Screen.height - _edgeSize)
            pos.z += _scrollSpeed * Time.deltaTime;

        // 맵 경계 제한
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        _camera.transform.position = pos;
    }
}
