using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unselect : MonoBehaviour
{
		GameObject ChessTable;
		FieldController fc;

		// Start is called before the first frame update
		void Start()
    {
				ChessTable = GameObject.Find("chess-table");
				fc = ChessTable.GetComponent<FieldController>();
		}


		private void OnMouseDown()
		{
				fc.SetDefaultCubeColors();
		}

}
