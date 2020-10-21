using UnityEngine;

public class FieldController : MonoBehaviour
{
    public int[,] Table = new int[8, 8];
    public MeshRenderer[] Cubes = new MeshRenderer[64];
    //Color[] CubeColors = new Color[64];
		Material[] CubeMaterials = new Material[64];
		public GameObject[] BlackPawns = new GameObject[16];
    public GameObject[] WhitePawns = new GameObject[16];
		public bool AILostGame = false;
    public bool playersTurn = true; //True = Player's turn, False = AI Turn


    public void SetDefaultCubeColors()
    {
        for (int i = 0; i < 64; i++)
        {
						Cubes[i].material = CubeMaterials[i];            
        }
    }

    bool FirstStart = true;

		// Start is called before the first frame update
		public void Start()
    {
        if (FirstStart)
        {
            for (int i = 0; i < 64; i++)
            {
								CubeMaterials[i] = Cubes[i].material;                
            }						
						FirstStart = false;						
        }
				
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Table[i, j] = 0;
            }
        }

        for (int i = 0; i < 16; i++)
        {
						int sor = 0, oszlop = 0;
						if (BlackPawns[i].active)
            {								
                if (Mathf.Approximately(BlackPawns[i].transform.position.z, 0.0f))
                    sor = 7;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.z, -2.7f))
                    sor = 6;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.z, -5.4f))
                    sor = 5;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.z, -8.1f))
                    sor = 4;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.z, -10.8f))
                    sor = 3;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.z, -13.5f))
                    sor = 2;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.z, -16.2f))
                    sor = 1;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.z, -18.9f))
                    sor = 0;


                if (Mathf.Approximately(BlackPawns[i].transform.position.x, 0.0f))
                    oszlop = 0;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.x, -2.7f))
                    oszlop = 1;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.x, -5.4f))
                    oszlop = 2;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.x, -8.1f))
                    oszlop = 3;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.x, -10.8f))
                    oszlop = 4;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.x, -13.5f))
                    oszlop = 5;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.x, -16.2f))
                    oszlop = 6;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.x, -18.9f))
                    oszlop = 7;

								if (sor == 7)
								{
										Debug.Log("Vesztes játék.");										
								}										

                Table[sor, oszlop] = 1;
            }

            if (WhitePawns[i].active)
            {
                if (Mathf.Approximately(WhitePawns[i].transform.position.z, 0.0f))
                    sor = 7;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.z, -2.7f))
                    sor = 6;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.z, -5.4f))
                    sor = 5;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.z, -8.1f))
                    sor = 4;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.z, -10.8f))
                    sor = 3;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.z, -13.5f))
                    sor = 2;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.z, -16.2f))
                    sor = 1;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.z, -18.9f))
                    sor = 0;

                if (Mathf.Approximately(WhitePawns[i].transform.position.x , 0.0f))
                    oszlop = 0;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.x, -2.7f))
                    oszlop = 1;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.x, -5.4f))
                    oszlop = 2;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.x, -8.1f))
                    oszlop = 3;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.x, -10.8f))
                    oszlop = 4;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.x, -13.5f))
                    oszlop = 5;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.x, -16.2f))
                    oszlop = 6;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.x, -18.9f))
                    oszlop = 7;

								if (sor == 0)
								{
										Debug.Log("Győztes játék.");
										AILostGame = true;
								}										

								Table[sor, oszlop] = 2;
            }
        }
    }
}
