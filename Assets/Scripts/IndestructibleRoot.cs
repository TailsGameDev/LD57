using UnityEngine;

public class IndestructibleRoot : MonoBehaviour
{
    [SerializeField]
    private SoundsManager soundsManager = null;

    private static IndestructibleRoot instance;

    public static IndestructibleRoot Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            soundsManager.Initialize();
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
