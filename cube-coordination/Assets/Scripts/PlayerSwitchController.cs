using UnityEngine;

public class PlayerSwitchController : MonoBehaviour
{
    private PlayerController player;
    private PlayerController playerMirror;

    [SerializeField] SpriteRenderer levelCompleteP1;
    [SerializeField] SpriteRenderer levelCompleteP2;

    [SerializeField] Color highlightColor;

    Color baseColor;


    [SerializeField] private ParticleSystem ps;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerMirror = FindObjectOfType<PlayerMirror>();

        player.TakeControl();
        playerMirror.ReleaseControl();

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

        if(player.playerReached && playerMirror.playerReached)
        {
            print("you win");
            GameManager.Instance.LevelComplete();
            player.isControlled = false;
            playerMirror.isControlled = false;
        }

        if (player.playerReached)
        {
            levelCompleteP1.material.color = highlightColor;
        }
        else
        {
            levelCompleteP1.material.color = baseColor;
        }

        if (playerMirror.playerReached)
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
        if (player.isControlled)
        {
            player.ReleaseControl();
            playerMirror.TakeControl();
        }
        else
        {
            player.TakeControl();
            playerMirror.ReleaseControl();
        }
    }

    public void PlayerDie()
    {
        ParticleSystem _ps1  = Instantiate(ps, player.transform.position, Quaternion.identity);
        ParticleSystem _ps2 = Instantiate(ps, playerMirror.transform.position, Quaternion.identity);
        _ps1.Play();
        _ps2.Play();
        Destroy(player.gameObject);
        Destroy(playerMirror.gameObject);
    }
}
