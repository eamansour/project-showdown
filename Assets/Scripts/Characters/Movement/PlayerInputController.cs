using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerInputController : MonoBehaviour
{
    private string inputVerticalAxis = "Vertical";
    private string inputHorizontalAxis = "Horizontal";

    public float VerticalInput { get => Input.GetAxis(inputVerticalAxis); }
    public float HorizontalInput { get => Input.GetAxis(inputHorizontalAxis); }

    private CharacterMovement characterMovement;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        Vector3 movement = new Vector3(HorizontalInput, 0f, 0f);
        characterMovement.Move(movement);
    }
}
