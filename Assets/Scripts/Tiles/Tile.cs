using UnityEngine;

public class Tile : MonoBehaviour
{
    //Properties
    //====================================================================================================================//
    
    public TILE_STATE CurrentState { get; private set; }

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
        CurrentState = startingState;
    }

    //====================================================================================================================//


    public void ChangeState(TILE_STATE targetState, Material newMaterial)
    {
        CurrentState = targetState;
        Renderer.material = newMaterial;
    }
    
    
}
