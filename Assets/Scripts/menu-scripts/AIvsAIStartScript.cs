using Assets.Scripts.menu_scripts.play_scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIvsAIStartScript : MonoBehaviour
{
		public GameObject StartSettingsMenu;
		private GameObject ScriptHolder;
		private AIDetails AiDetails;

		public GameObject ErrorMessage;

		public Toggle StepIgnoreToggle;
		public InputField MaxLevel;

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

				StepIgnoreToggle.isOn = AiDetails.UseStepIgnore;
				MaxLevel.text = AiDetails.MaxLevel.ToString();
				
				//White
				WhiteRow1.text = AiDetails.WhiteRowBonuses[0].ToString();
				WhiteRow2.text = AiDetails.WhiteRowBonuses[1].ToString();
				WhiteRow3.text = AiDetails.WhiteRowBonuses[2].ToString();
				WhiteRow4.text = AiDetails.WhiteRowBonuses[3].ToString();
				WhiteRow5.text = AiDetails.WhiteRowBonuses[4].ToString();
				WhiteRow6.text = AiDetails.WhiteRowBonuses[5].ToString();
				WhiteRow7.text = AiDetails.WhiteRowBonuses[6].ToString();
				WhiteRow8.text = AiDetails.WhiteRowBonuses[7].ToString();

				WhiteSafe.text = AiDetails.WhiteSafeFieldBonus.ToString();
				WhiteSafeOnSide.text = AiDetails.WhiteSafeFieldBonusOnSide.ToString();
				WhiteRowForm.text = AiDetails.WhiteRowFormationBonus.ToString();
				WhiteRowFormOnSide.text = AiDetails.WhiteRowFormationBonusOnSide.ToString();
				WhiteColumnForm.text = AiDetails.WhiteColumnFormationBonus.ToString();
				WhiteColumnFormOnSide.text = AiDetails.WhiteColumnFormationBonusOnSide.ToString();
				WhiteDiagonalForm.text = AiDetails.WhiteDiagonalFormationBonus_SecondaryFormation.ToString();
				WhiteDiagonalFormOnSide.text = AiDetails.WhiteDiagonalFormationBonusOnSide_SecondaryFormation.ToString();
				WhitePenalty.text = AiDetails.WhitePenaltyForDangerousField.ToString();
				WhitePenaltyOnSide.text = AiDetails.WhitePenaltyForDangerousFieldOnSide.ToString();
				WhiteDefender.text = AiDetails.WhitePawnInDangerDefenderBonus.ToString();
				WhiteDefenderOnSide.text = AiDetails.WhitePawnInDangerDefenderBonusOnSide.ToString();

				//Black
				BlackRow1.text = AiDetails.BlackRowBonuses[0].ToString();
				BlackRow2.text = AiDetails.BlackRowBonuses[1].ToString();
				BlackRow3.text = AiDetails.BlackRowBonuses[2].ToString();
				BlackRow4.text = AiDetails.BlackRowBonuses[3].ToString();
				BlackRow5.text = AiDetails.BlackRowBonuses[4].ToString();
				BlackRow6.text = AiDetails.BlackRowBonuses[5].ToString();
				BlackRow7.text = AiDetails.BlackRowBonuses[6].ToString();
				BlackRow8.text = AiDetails.BlackRowBonuses[7].ToString();

				BlackSafe.text = AiDetails.BlackSafeFieldBonus.ToString();
				BlackSafeOnSide.text = AiDetails.BlackSafeFieldBonusOnSide.ToString();
				BlackRowForm.text = AiDetails.BlackRowFormationBonus.ToString();
				BlackRowFormOnSide.text = AiDetails.BlackRowFormationBonusOnSide.ToString();
				BlackColumnForm.text = AiDetails.BlackColumnFormationBonus.ToString();
				BlackColumnFormOnSide.text = AiDetails.BlackColumnFormationBonusOnSide.ToString();
				BlackDiagonalForm.text = AiDetails.BlackDiagonalFormationBonus_SecondaryFormation.ToString();
				BlackDiagonalFormOnSide.text = AiDetails.BlackDiagonalFormationBonusOnSide_SecondaryFormation.ToString();
				BlackPenalty.text = AiDetails.BlackPenaltyForDangerousField.ToString();
				BlackPenaltyOnSide.text = AiDetails.BlackPenaltyForDangerousFieldOnSide.ToString();
				BlackDefender.text = AiDetails.BlackPawnInDangerDefenderBonus.ToString();
				BlackDefenderOnSide.text = AiDetails.BlackPawnInDangerDefenderBonusOnSide.ToString();
				#endregion


				Time.timeScale = 0;
		}

		bool AllConvertable = true;
		public void StartButtonAction()
		{
				#region ParseGivenNumbers
				AiDetails.UseStepIgnore = StepIgnoreToggle.isOn;
				AiDetails.MaxLevel = Converter(MaxLevel.text);

				//White
				AiDetails.WhiteRowBonuses[0] = Converter(WhiteRow1.text);
				AiDetails.WhiteRowBonuses[1] = Converter(WhiteRow2.text);
				AiDetails.WhiteRowBonuses[2] = Converter(WhiteRow3.text);
				AiDetails.WhiteRowBonuses[3] = Converter(WhiteRow4.text);
				AiDetails.WhiteRowBonuses[4] = Converter(WhiteRow5.text);
				AiDetails.WhiteRowBonuses[5] = Converter(WhiteRow6.text);
				AiDetails.WhiteRowBonuses[6] = Converter(WhiteRow7.text);
				AiDetails.WhiteRowBonuses[7] = Converter(WhiteRow8.text);

				AiDetails.WhiteSafeFieldBonus = Converter(WhiteSafe.text);
				AiDetails.WhiteSafeFieldBonusOnSide = Converter(WhiteSafeOnSide.text);
				AiDetails.WhiteRowFormationBonus = Converter(WhiteRowForm.text);
				AiDetails.WhiteRowFormationBonusOnSide = Converter(WhiteRowFormOnSide.text);
				AiDetails.WhiteColumnFormationBonus = Converter(WhiteColumnForm.text);
				AiDetails.WhiteColumnFormationBonusOnSide = Converter(WhiteColumnFormOnSide.text);
				AiDetails.WhiteDiagonalFormationBonus_SecondaryFormation = Converter(WhiteDiagonalForm.text);
				AiDetails.WhiteDiagonalFormationBonusOnSide_SecondaryFormation = Converter(WhiteDiagonalFormOnSide.text);
				AiDetails.WhitePenaltyForDangerousField = Converter(WhitePenalty.text);
				AiDetails.WhitePenaltyForDangerousFieldOnSide = Converter(WhitePenaltyOnSide.text);
				AiDetails.WhitePawnInDangerDefenderBonus = Converter(WhiteDefender.text);
				AiDetails.WhitePawnInDangerDefenderBonusOnSide = Converter(WhiteDefenderOnSide.text);

				//Black
				AiDetails.BlackRowBonuses[0] = Converter(BlackRow1.text);
				AiDetails.BlackRowBonuses[1] = Converter(BlackRow2.text);
				AiDetails.BlackRowBonuses[2] = Converter(BlackRow3.text);
				AiDetails.BlackRowBonuses[3] = Converter(BlackRow4.text);
				AiDetails.BlackRowBonuses[4] = Converter(BlackRow5.text);
				AiDetails.BlackRowBonuses[5] = Converter(BlackRow6.text);
				AiDetails.BlackRowBonuses[6] = Converter(BlackRow7.text);
				AiDetails.BlackRowBonuses[7] = Converter(BlackRow8.text);

				AiDetails.BlackSafeFieldBonus = Converter(BlackSafe.text);
				AiDetails.BlackSafeFieldBonusOnSide = Converter(BlackSafeOnSide.text);
				AiDetails.BlackRowFormationBonus = Converter(BlackRowForm.text);
				AiDetails.BlackRowFormationBonusOnSide = Converter(BlackRowFormOnSide.text);
				AiDetails.BlackColumnFormationBonus = Converter(BlackColumnForm.text);
				AiDetails.BlackColumnFormationBonusOnSide = Converter(BlackColumnFormOnSide.text);
				AiDetails.BlackDiagonalFormationBonus_SecondaryFormation = Converter(BlackDiagonalForm.text);
				AiDetails.BlackDiagonalFormationBonusOnSide_SecondaryFormation = Converter(BlackDiagonalFormOnSide.text);
				AiDetails.BlackPenaltyForDangerousField = Converter(BlackPenalty.text);
				AiDetails.BlackPenaltyForDangerousFieldOnSide = Converter(BlackPenaltyOnSide.text);
				AiDetails.BlackPawnInDangerDefenderBonus = Converter(BlackDefender.text);
				AiDetails.BlackPawnInDangerDefenderBonusOnSide = Converter(BlackDefenderOnSide.text);

				if (AllConvertable)
				{
						ErrorMessage.SetActive(false);
						StartGame();
				}
				else
				{
						ErrorMessage.SetActive(true);
						AllConvertable = true;
				}
				#endregion
		}

		public void StartGame()
		{
				StartSettingsMenu.SetActive(false);
				Time.timeScale = 1;
				AutoPlay AutoPlayer = ScriptHolder.GetComponent<AutoPlay>();
				AutoPlayer.AutoPlayActive = true;
				AutoPlayer.AutomaticPlay();				
		}		

		private int Converter(string text)
		{
				int Temp;
				
				if (int.TryParse(text, out Temp))
				{
						return Temp;
				}
				else
				{
						AllConvertable = false;
						return 0;
				}
		}
}
