using UnityEngine;

public class Tile : MonoBehaviour
{
    //Properties
    //====================================================================================================================//

    public virtual TILE_STATE CurrentState => currentState;
    protected TILE_STATE currentState { get; private set; }

    public virtual bool InterruptsRiver => interruptsRiver;

    [SerializeField]
    private bool interruptsRiver;
    
    public Vector2Int coordinate;

    [SerializeField]
    private TILE_STATE startingState;


    private Renderer Renderer
    {
        get
        {
            if (_renderer == null)
                _renderer = GetComponent<Renderer>();

            return _renderer;
        }
    }
    private Renderer _renderer;

    //====================================================================================================================//

    private void Start()
    {
        Setup();
    }

    //====================================================================================================================//

    protected virtual void Setup()
    {
        currentState = startingState;
    }

    public virtual void ChangeState(TILE_STATE targetState, Material newMaterial)
    {
        currentState = targetState;
        Renderer.material = newMaterial;
    }
    
    
}
