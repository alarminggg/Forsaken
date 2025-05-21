using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");

    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        float snappedHorizontal;
        float snappedVertical;

        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            snappedHorizontal = 0.5f;
        else if (horizontalMovement >= 0.55f)
            snappedHorizontal = 1f;
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            snappedHorizontal = -0.5f;
        else if (horizontalMovement <= -0.55f)
            snappedHorizontal = -1f;
        else
            snappedHorizontal = 0f;

        if (isSprinting)
        {
            // Sprint: vertical = 1, horizontal stays snapped
            snappedVertical = 1f;
        }
        else
        {
            // Snap vertical movement for idle or walk only
            if (verticalMovement > 0 && verticalMovement < 0.55f)
                snappedVertical = 0.5f;  // walk
            else if (verticalMovement >= 0.55f)
                snappedVertical = 0.5f;  // still walk, no run
            else if (verticalMovement < 0)
                snappedVertical = 0f;  // no backward walk in this setup
            else
                snappedVertical = 0f;  // idle
        }

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}
