
using UnityEngine;

public class SingletonObjectToUnablleduplicate : MonoBehaviour
{
    public static SingletonObjectToUnablleduplicate Instance;

    void Awake()
    {
        // Verifica se já existe uma instância
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Já existe? destrói o duplicado!
        }
    }
}
