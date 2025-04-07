using UnityEngine;

public class TutorialPiece : MonoBehaviour
{
    [SerializeField]
    private TutorialPiece nextPiece = null;
    [SerializeField]
    private TutorialPiece previousPiece = null;

    public void GoToNextTutorial()
    {
        if (nextPiece != null)
        {
            gameObject.SetActive(false);
            nextPiece.gameObject.SetActive(true);
        }
    }
    public void GoToPreviousTutorial()
    {
        if (previousPiece != null)
        {
            gameObject.SetActive(false);
            previousPiece.gameObject.SetActive(true);
        }
    }
}
