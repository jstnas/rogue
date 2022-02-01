using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Floor floor;
    [SerializeField] private Tile playerTile;
    private Vector3Int _position = Vector3Int.zero;
    private Tilemap _tilemap;

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    private void Start()
    {
        _tilemap.SetTile(_position, playerTile);
    }

    private void Update()
    {
        // Get input.
        var input = Vector3Int.zero;
        if (Input.GetKeyDown(KeyCode.W))
            input = Vector3Int.up;
        else if (Input.GetKeyDown(KeyCode.A))
            input = Vector3Int.left;
        else if (Input.GetKeyDown(KeyCode.S))
            input = Vector3Int.down;
        else if (Input.GetKeyDown(KeyCode.D))
            input = Vector3Int.right;
        // Only move on input.
        if (input != Vector3Int.zero)
        {
            var newPosition = _position + input;
            // Skip if can't move to new position.
            if (!floor.IsValidTile(newPosition))
                return;
            _tilemap.SetTile(_position, null);
            _position = newPosition;
            _tilemap.SetTile(_position, playerTile);
            _tilemap.CompressBounds();
        }
    }
}