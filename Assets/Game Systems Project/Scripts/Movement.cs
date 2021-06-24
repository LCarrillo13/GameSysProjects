using UnityEngine;
namespace Debugging.Player
{
    [AddComponentMenu("RPG/Player/Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class Movement  : MonoBehaviour
    {
        [Header("Speed Vars")]
        public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;
        private float _gravity = 20.0f;
        private Vector3 _moveDir;
        private CharacterController _charC;

        private Animator characterAnimator;



        private void Start()
        {
            _charC = GetComponent<CharacterController>();
            characterAnimator = GetComponentInChildren<Animator>();
        }
        private void Update()
        {
            Move();

            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 3))
                {
                    if(hit.transform.tag == "NPC")
                    {
                        Dialog npcDialog = hit.transform.GetComponent<Dialog>();
                        if (npcDialog)
                        {
                            DialogManager.theManager.LoadDialogue(npcDialog);
                        }

                    }
                }
            }
        }
        private void Move()
        {
            Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            if (_charC.isGrounded)
            {

                if (Input.GetButton("Crouch"))
                {
                    moveSpeed = crouchSpeed;
                    characterAnimator.SetFloat("speed", 0.25f);
                }
                else
                {

                  if (Input.GetButton("Sprint"))
                    {
                        moveSpeed = runSpeed;
                        characterAnimator.SetFloat("speed", 3f);
                    }
                  else if(!Input.GetButton("Sprint"))
                    {
                        moveSpeed = walkSpeed;
                        characterAnimator.SetFloat("speed", 1f);
                    }
                }

                if(movementInput.magnitude > 0.05f)
                {
                    characterAnimator.SetBool("walking", true);
                }
                else
                {
                    characterAnimator.SetBool("walking", false);
                }
                
                _moveDir = transform.TransformDirection(new Vector3(movementInput.x, 0, movementInput.y) * moveSpeed);
                if (Input.GetButton("Jump"))
                {
                    _moveDir.y = jumpSpeed;
                }
            }
            _moveDir.y -= _gravity * Time.deltaTime;
            _charC.Move(_moveDir * Time.deltaTime);
        }
    }
 }

