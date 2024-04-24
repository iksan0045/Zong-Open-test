using UnityEngine;
using UnityEngine.EventSystems;

namespace Zong
{
    public class CameraController : MonoBehaviour
    {
        public float mouseSensitivity = 100f;

        [SerializeField]
        private Transform playerBody;

        private Camera playerCamera;
        bool showCursor;
 
        
        float xRotation;


        void Start()
        {
            playerCamera = GetComponent<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            showCursor = false;
        }

        void Update()
        {
            RotateCamera();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("cursors status :" + showCursor);
                if (!showCursor)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    showCursor = true;
                }
                else
                {
                    if(Cursor.visible)
                    {
                    }
                    
                }
                
                
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Cursor.visible = true;
        }

        void RotateCamera()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

        
    }
}
