using System.Collections.Generic;

[System.Serializable]
public class Historia
{
    public int ID;
    public float InitialPositionX;
    public float InitialPositionY;
    public float InitialPositionZ;

    public Texto Texto;
    public Dicas Dicas;
    public List<int> OrdemTesouros;
}

[System.Serializable]
public class Texto
{
    public string titulo;
    public string tesouro1;
    public string tesouro2;
    public string tesouro3;
    public string final;
}

[System.Serializable]
public class Dicas
{
    public List<string> tesouro1;
    public List<string> tesouro2;
    public List<string> tesouro3;
}

[System.Serializable]
public class HistoriasRoot
{
    public List<Historia> Historias;
}
