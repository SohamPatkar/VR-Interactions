using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class TheWall : MonoBehaviour
{
    public UnityEvent OnDestroy;

    [SerializeField] private ExplosiveInteractable explosiveInteractable;
    [SerializeField] private GameObject teleportationArea;
    [SerializeField] private GameObject[] columnsCount;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject socketedWallPrefab;
    [SerializeField] private GameObject emptyObject;
    [SerializeField] private int columns;
    [SerializeField] private int rows;
    [SerializeField] private int socketPositon;
    [SerializeField] private int columnToDelete;
    [SerializeField] private bool build;
    [SerializeField] private bool deleteWall;
    [SerializeField] private GameObject[] walls;
    [SerializeField] private int maxPower = 25;


    private XRSocketInteractor wallSocket;
    private float wallSpacing = 0.5f;
    private int spawnCount = 0;
    private Vector3 wallSize;
    private float spawnPosition;
    private float columnPosition;
    private bool spawnSocket;


    // Start is called before the first frame update
    void Start()
    {
        explosiveInteractable.OnDetonated += DestroyWall;

        wallSize = wallPrefab.GetComponent<Renderer>().bounds.size;

        columnPosition = transform.position.x;

        spawnSocket = false;

        spawnCount = 0;

        wallSpacing = 0.5f;

        BuildWall();
    }

    private void BuildWall()
    {
        GenerateColumns(rows, columns);
    }

    private void GenerateColumns(int rows, int columns)
    {
        walls = new GameObject[rows * columns];

        columnsCount = new GameObject[columns];

        for (int j = 0; j < columns; j++)
        {
            spawnPosition = transform.position.y;

            //create columns parent object
            GameObject newColumn = Instantiate(emptyObject, new Vector3(columnPosition, spawnPosition, transform.position.z), Quaternion.identity);

            columnsCount[j] = newColumn;

            //create rows
            for (int i = 0; i < rows; i++)
            {
                if (wallPrefab != null)
                {
                    walls[spawnCount] = Instantiate(wallPrefab, new Vector3(columnPosition, spawnPosition, transform.position.z), Quaternion.identity);
                    walls[spawnCount].transform.SetParent(newColumn.transform);
                }

                //create socket 
                if (!spawnSocket && socketPositon == spawnCount)
                {
                    if (socketPositon < 0 || socketPositon > rows * columns)
                    {
                        socketPositon = 0;
                    }

                    if (walls[socketPositon] != null)
                    {
                        Debug.Log(socketPositon + " " + spawnCount);
                        Vector3 position = walls[socketPositon].transform.position;
                        DestroyImmediate(walls[socketPositon].gameObject);

                        walls[socketPositon] = Instantiate(socketedWallPrefab, position, socketedWallPrefab.transform.localRotation);
                        walls[socketPositon].transform.SetParent(newColumn.transform);

                        //socketed column name changed
                        SetColumnName("Socketed", newColumn);

                        wallSocket = walls[socketPositon].GetComponentInChildren<XRSocketInteractor>();
                        spawnSocket = true;

                        if (wallSocket != null)
                        {
                            wallSocket.selectEntered.AddListener(OnSocketed);
                            wallSocket.selectExited.AddListener(OnSocketExited);
                        }
                    }
                }

                spawnCount++;

                spawnPosition += (wallSize.y - 0.4f) + wallSpacing;
            }

            columnPosition += wallSize.x - 0.5f + wallSpacing;
        }

        for (int i = 0; i < columnsCount.Length; i++)
        {
            if (columnsCount[i] != null)
            {
                columnsCount[i].transform.SetParent(transform);
            }
        }
    }

    private void DeleteColumn(int columnIndex)
    {
        DestroyImmediate(columnsCount[columnIndex].gameObject);
    }

    private void DestroyWall(int power)
    {
        for (int i = 0; i < walls.Length; i++)
        {
            Rigidbody rbWalls = walls[i].GetComponent<Rigidbody>();
            rbWalls.isKinematic = false;
            rbWalls.constraints = RigidbodyConstraints.None;
            rbWalls.AddForce(Random.onUnitSphere * power);
            OnDestroy?.Invoke();
        }
    }

    private void SetColumnName(string name, GameObject go)
    {
        go.name = name;
    }

    private void ActivateColumn()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            Rigidbody rbWalls = walls[i].GetComponent<Rigidbody>();
            rbWalls.isKinematic = false;
            rbWalls.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnSocketed(SelectEnterEventArgs args)
    {
        int power = maxPower / 2;

        ActivateColumn();

        teleportationArea.SetActive(true);
    }

    private void OnSocketExited(SelectExitEventArgs args)
    {
        for (int i = 0; i < walls.Length; i++)
        {
            Rigidbody rbWalls = walls[i].GetComponent<Rigidbody>();
            rbWalls.isKinematic = true;
        }

        teleportationArea.SetActive(false);
    }
}
