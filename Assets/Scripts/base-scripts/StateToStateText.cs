using UnityEngine;

public class StateToStateText : MonoBehaviour
{    
	public TextMesh StateText3D;
	// Update is called once per frame

	void Update () {
		if (Input.GetKey(KeyCode.T) && Time.timeScale != 0)
        {
						StateText3D.text = "";
            GameObject ChessTable = GameObject.Find("chess-table");
            FieldController fc = ChessTable.GetComponent<FieldController>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {                    
                        StateText3D.text += fc.Table[i, j].ToString() + "  ";																																
								}
								StateText3D.text += '\n';
						}
        }
		}    
}
