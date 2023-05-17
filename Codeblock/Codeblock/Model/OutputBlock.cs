using System;
using Codeblock.ViewModel.UnitsView.OutputBlockFolder;

namespace Codeblock.Model
{
	public class OutputBlock : Node
	{
		public string Input;
		OutputPartView OutputPartView;
		public OutputBlock(OutputPartView outputPartView, string input = "") : base()
		{
			Input = input;
			OutputPartView = outputPartView;
		}
		public override void Compilation(CodeBlock CurrentCodeBlock)
		{
			int l = 0;
			while (l < Input.Length)
			{
				string CurrentName = "";

				while (l < Input.Length && Input[l] != ' ' && Input[l] != ',')
                {
					CurrentName += Input[l++];
                }

				if (CurrentName == "")
                {
					l++;
					continue;
                }

				Console.WriteLine(CurrentName);

				for (int i = CurrentCodeBlock.AreaVariable.Count - 1; i >= 0; i--)
				{
					for (int j = 0; j < CurrentCodeBlock.AreaVariable[i].Count; j++)
					{
						if (CurrentCodeBlock.AreaVariable[i][j].Name == CurrentName)
						{
							CurrentCodeBlock.AreaVariable[i][j].WriteLineVariable(OutputPartView);
							i = -1;
							break;
						}
					}
					if (i == 0)
					{
						Console.WriteLine("Exception: " + CurrentName + " is undefined");
						CurrentCodeBlock.Error();
					}
				}
				l++;
			}
		}
	}
}