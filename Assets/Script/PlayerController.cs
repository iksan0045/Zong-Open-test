using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Zong
{
    public class PlayerController : MonoBehaviour
    {
        public float movementSpeed = 5f;

        private Rigidbody rb;
        private float currentStamina;
 
        [SerializeField]
        public bool isMove;

        private Vector3 originPos;


        void Awake()
        {
            originPos = transform.position;
        }

        void Start()
        {
            isMove = true;
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (isMove)
            {
                MovePlayer();  
            }

        }

        void MovePlayer()
        {
            float horizontalMove = Input.GetAxis("Horizontal");
            float verticalMove = Input.GetAxis("Vertical");

            Vector3 moveDirection = transform.forward * verticalMove + transform.right * horizontalMove;

            moveDirection = moveDirection.normalized * movementSpeed;

            rb.MovePosition(rb.position + moveDirection * Time.deltaTime);
        }

        public void BackToOrigin()
        {
            StartCoroutine(ReturnToOrigin());
        }

        private IEnumerator ReturnToOrigin()
        {
            yield return new WaitForSeconds(1.2f);

            transform.position = originPos;
        }
       
    }
}
