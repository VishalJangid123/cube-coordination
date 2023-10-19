using UnityEngine;

public class PlayerSwitchController : MonoBehaviour
{
    public PlayerController cubeA;
    public PlayerController cubeB;

    [SerializeField] SpriteRenderer levelCompleteP1;
    [SerializeField] SpriteRenderer levelCompleteP2;

    [SerializeField] Color highlightColor;

    Color baseColor;


    [SerializeField] private ParticleSystem ps;

    private void Start()
    {
        // Initialize control to CubeA
        cubeA.TakeControl();
        cubeB.ReleaseControl();

        baseColor = levelCompleteP1.color;

    }

    private void Update()
    {
        if (GameManager.Instance.gameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchControl();
        }

        if(cubeA.playerReached && cubeB.playerReached)
        {
            print("you win");
            GameManager.Instance.LevelComplete();
            cubeA.isControlled = false;
            cubeB.isControlled = false;
        }

        if (cubeA.playerReached)
        {
            levelCompleteP1.material.color = highlightColor;
        }
        else
        {
            levelCompleteP1.material.color = baseColor;
        }

        if (cubeB.playerReached)
        {
            levelCompleteP2.material.color = highlightColor;
        }
        else
        {
            levelCompleteP2.material.color = baseColor;
        }

    }

    private void SwitchControl()
    {
        if (cubeA.isControlled)
        {
            cubeA.ReleaseControl();
            cubeB.TakeControl();
        }
        else
        {
            cubeA.TakeControl();
            cubeB.ReleaseControl();
        }
    }

    public void PlayerDie()
    {
        ParticleSystem _ps1  = Instantiate(ps, cubeA.transform.position, Quaternion.identity);
        ParticleSystem _ps2 = Instantiate(ps, cubeB.transform.position, Quaternion.identity);
        _ps1.Play();
        _ps2.Play();
        Destroy(cubeA.gameObject);
        Destroy(cubeB.gameObject);
    }
}
