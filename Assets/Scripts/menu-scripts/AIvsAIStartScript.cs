using Assets.Scripts.menu_scripts.play_scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIvsAIStartScript : MonoBehaviour
{
		private GameObject ScriptHolder;
		private AIDetails AiDetails;

		public Toggle StepIgnoreToggle;
		public Dropdown MaxLevelDropDown;

		#region White elements
		public InputField WhiteRow1;
		public InputField WhiteRow2;
		public InputField WhiteRow3;
		public InputField WhiteRow4;
		public InputField WhiteRow5;
		public InputField WhiteRow6;
		public InputField WhiteRow7;
		public InputField WhiteRow8;

		public InputField WhiteSafe;
		public InputField WhiteSafeOnSide;
		public InputField WhiteRowForm;
		public InputField WhiteRowFormOnSide;
		public InputField WhiteColumnForm;
		public InputField WhiteColumnFormOnSide;
		public InputField WhiteDiagonalForm;
		public InputField WhiteDiagonalFormOnSide;
		public InputField WhitePenalty;
		public InputField WhitePenaltyOnSide;
		public InputField WhiteDefender;
		public InputField WhiteDefenderOnSide;
		#endregion

		#region Black elements
		public InputField BlackRow1;
		public InputField BlackRow2;
		public InputField BlackRow3;
		public InputField BlackRow4;
		public InputField BlackRow5;
		public InputField BlackRow6;
		public InputField BlackRow7;
		public InputField BlackRow8;

		public InputField BlackSafe;
		public InputField BlackSafeOnSide;
		public InputField BlackRowForm;
		public InputField BlackRowFormOnSide;
		public InputField BlackColumnForm;
		public InputField BlackColumnFormOnSide;
		public InputField BlackDiagonalForm;
		public InputField BlackDiagonalFormOnSide;
		public InputField BlackPenalty;
		public InputField BlackPenaltyOnSide;
		public InputField BlackDefender;
		public InputField BlackDefenderOnSide;
		#endregion
		// Start is called before the first frame update
		void Start()
    {
				ScriptHolder = GameObject.Find("ScriptHolder");
				AiDetails = ScriptHolder.GetComponent<AIDetails>();

				#region Init
				//White
				WhiteRow1.textComponent.text = AiDetails.WhiteRowBonuses[0].ToString();
				WhiteRow2.textComponent.text = AiDetails.WhiteRowBonuses[1].ToString();
				WhiteRow3.textComponent.text = AiDetails.WhiteRowBonuses[2].ToString();
				WhiteRow4.textComponent.text = AiDetails.WhiteRowBonuses[3].ToString();
				WhiteRow5.textComponent.text = AiDetails.WhiteRowBonuses[4].ToString();
				WhiteRow6.textComponent.text = AiDetails.WhiteRowBonuses[5].ToString();
				WhiteRow7.textComponent.text = AiDetails.WhiteRowBonuses[6].ToString();
				WhiteRow8.textComponent.text = AiDetails.WhiteRowBonuses[7].ToString();

				//Black
				#endregion
		}

		// Update is called once per frame
		void Update()
    {
        
    }
}
