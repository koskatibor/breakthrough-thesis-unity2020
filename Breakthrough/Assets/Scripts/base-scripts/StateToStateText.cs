using UnityEngine;

public class StateToStateText : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
    
    public TextMesh StateText3D;
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.T))
        {
						StateText3D.text = "";
            GameObject ChessTable = GameObject.Find("chess-table");
            FieldController fc = ChessTable.GetComponent<FieldController>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {                    
                        StateText3D.text += fc.table[i, j].ToString() + "  ";																																
								}
								StateText3D.text += '\n';
						}
        }
				if (Input.GetKey(KeyCode.Z))
				{
						StateText3D.text = "";
				}
		}    
}
