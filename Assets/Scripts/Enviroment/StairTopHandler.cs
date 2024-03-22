using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StairTopHandler : MonoBehaviour
{
    [SerializeField] private float collisionTimeOut;
    private float _currentTimeOut;
    private Collider2D _stairTopCollider;
    private bool _colliderDisabled;

    private void Awake() {
        _stairTopCollider = GetComponent<Collider2D>();
    }

    private void Update(){
        _currentTimeOut += Time.deltaTime; 
        if(_currentTimeOut >= collisionTimeOut && _colliderDisabled){
            _stairTopCollider.enabled = true;
            _colliderDisabled = false;
        }
    }

    public void DisableCollider(){
        _currentTimeOut = 0;
        _stairTopCollider.enabled = false;
        _colliderDisabled = true;
    }

    
}
