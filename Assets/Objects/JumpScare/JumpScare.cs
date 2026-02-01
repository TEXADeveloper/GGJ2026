using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpScare : MonoBehaviour
{
    public void LoseGame()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("JumpScare");
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Scenes/Lose condition");
    }
}
