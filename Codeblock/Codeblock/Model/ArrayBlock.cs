using System;
using System.Collections.Generic;
using Codeblock.ViewModel.UnitsView.OutputBlockFolder;

namespace Codeblock.Model
{
	public class ArrayBlock : Variable
	{
		//Name from Variable
		//Type from Variable
		public string Length;
		public int CurrentLength;
		public List<Variable> AreaVariable;
		public ArrayBlock(int currentLength = 0) : base()
		{
			CurrentLength = currentLength;
			Array = true;
			AreaVariable = new List<Variable>();
		}
		public void Initialization()
        {
			for (int i = CurrentLength; i < int.Parse(Length); i++)
            {
				AreaVariable.Add(new Variable());
            }
        }
		public void AddVariable()
        {
			AreaVariable[CurrentLength] = new Variable(Name + "[" + CurrentLength + "]", Type, "");
			CurrentLength++;
			if (CurrentLength.ToString() == Length)
            {
				//Remove button
            }
        }
		public override void WriteLineVariable(OutputPartView outputPartView)
		{
			for (int i = 0; i < int.Parse(Length); i++)
            {
				AreaVariable[i].WriteLineVariable(outputPartView);
            }
		}
		public override string GetValue(CodeBlock currentCodeBlock, string input = "")
		{
			return AreaVariable[int.Parse(Calculate(input, "index", currentCodeBlock))].GetValue(currentCodeBlock);
		}
		public override void Compilation(CodeBlock CurrentCodeBlock)
		{
			if (Assignment)
			{
				for (int i = CurrentCodeBlock.AreaVariable.Count - 1; i >= 0; i--)
				{
					for (int j = 0; j < CurrentCodeBlock.AreaVariable[i].Count; j++)
					{
						if (CurrentCodeBlock.AreaVariable[i][j].Name == Name && CurrentCodeBlock.AreaVariable[i][j].Type == Type && CurrentCodeBlock.AreaVariable[i][j].Array)
						{
							for (int l = CurrentCodeBlock.AreaVariable.Count - 1; l >= 0; l--)
							{
								for (int k = 0; k < CurrentCodeBlock.AreaVariable[l].Count; k++)
								{
									if (CurrentCodeBlock.AreaVariable[l][k].Name == Input && CurrentCodeBlock.AreaVariable[l][k].Type == Type && CurrentCodeBlock.AreaVariable[i][j].Array)
									{
										CurrentCodeBlock.AreaVariable[i][j] = CurrentCodeBlock.AreaVariable[l][k];
										l = -1;
										break;
									}
								}
								if (l == 0)
                                {
									Console.WriteLine("Exception: " + Input + " is undefined");
									CurrentCodeBlock.Error();
								}
							}
							i = -1;
							break;
						}
					}
					if (i == 0)
					{
						Console.WriteLine("Exception: " + Name + " is undefined");
						CurrentCodeBlock.Error();
					}
				}
			}
			else
			{
				Initialization();
			}
		}
	}
}