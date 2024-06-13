using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IAssemblyObjectParent {
    public static Player Instance { get; private set; }


    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseStation SelectedStation;
    }


    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [FormerlySerializedAs("countersLayerMask")] [SerializeField] private LayerMask stationsLayerMask;
    [FormerlySerializedAs("kitchenObjectHoldPoint")] [SerializeField] private Transform assemblyObjectHoldPoint;


    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseStation _selectedStation;
    private AssemblyObject _assemblyObject;


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }

        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e) {
        if (!OverloadedGameManager.Instance.IsGamePlaying()) return;

        if (_selectedStation != null) {
            _selectedStation.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (!OverloadedGameManager.Instance.IsGamePlaying()) return;

        if (_selectedStation != null) {
            _selectedStation.Interact(this);
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance,
                stationsLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out BaseStation baseCounter)) {
                // Has ClearCounter
                if (baseCounter != _selectedStation) {
                    SetSelectedCounter(baseCounter);
                }
            }
            else {
                SetSelectedCounter(null);
            }
        }
        else {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDir, moveDistance);

        if (!canMove) {
            // Cannot move towards moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position,
                transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                // Can move only on the X
                moveDir = moveDirX;
            }
            else {
                // Cannot move only on the X

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position,
                    transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    // Can move only on the Z
                    moveDir = moveDirZ;
                }
                else {
                    // Cannot move in any direction
                }
            }
        }

        if (canMove) {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(BaseStation selectedStation) {
        this._selectedStation = selectedStation;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            SelectedStation = selectedStation
        });
    }

    public Transform GetAssemblyObjectFollowTransform() {
        return assemblyObjectHoldPoint;
    }

    public void SetAssemblyObject(AssemblyObject assemblyObject) {
        this._assemblyObject = assemblyObject;

        if (assemblyObject != null) {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public AssemblyObject GetAssemblyObject() {
        return _assemblyObject;
    }

    public void ClearAssemblyObject() {
        _assemblyObject = null;
    }

    public bool HasAssemblyObject() {
        return _assemblyObject != null;
    }
}
