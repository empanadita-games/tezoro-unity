using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof (PlayerController))]
public class PlayerInput : MonoBehaviour
{
    private PlayerController m_Character;
    private bool m_Jump;
    private float horizotal;

    [SerializeField] private bool isInputBlocked;

    public bool Blocked => isInputBlocked;

    private void Awake()
    {
        m_Character = GetComponent<PlayerController>();
    }


    private void Update()
    {
        if (isInputBlocked) return;

        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
    }


    private void FixedUpdate()
    {
        if (isInputBlocked)
        {
            horizotal = 0f;
            m_Jump = false;
            m_Character.Move(horizotal, false, false);
            return;
        }

        // Read the inputs.
        bool crouch = Input.GetKey(KeyCode.LeftControl);
        horizotal = CrossPlatformInputManager.GetAxis("Horizontal");

        // Pass all parameters to the character control script.
        m_Character.Move(horizotal, crouch, m_Jump);
        m_Jump = false;
    }

    public void BlockInput()
    {
        isInputBlocked = true;
    }

    public void UnblockInput()
    {
        isInputBlocked = false;
    }

    public void BlockInputByTime(float duration)
    {
        if (isInputBlocked) return;

        StartCoroutine(BlockInputByTimeCoroutine(duration));
    }

    private IEnumerator BlockInputByTimeCoroutine(float duration)
    {
        BlockInput();
        yield return new WaitForSeconds(duration);
        UnblockInput();
    }
}
