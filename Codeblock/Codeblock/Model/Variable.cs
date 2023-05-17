using System;
using Codeblock.ViewModel.UnitsView.OutputBlockFolder;

namespace Codeblock.Model
{
	public class Variable : Node
	{
		public string Name;
		public string Type;
		public string Input;
		public string Value;
		public bool Assignment = false;
		public bool Array = false;
		public Variable(string name = "", string type = "int", string input = "", string value = "None") : base()
		{
			Name = name;
			Type = type;
			Input = input;
			Value = value;
		}
		public virtual void WriteLineVariable(OutputPartView outputPartView)
		{
			outputPartView.WriteLine(Name + " = " + Value);
		}
		public virtual string GetValue(CodeBlock CurrentCodeBlock, string input = "")
		{
			if (input != "" && Type == "string")
			{
				if (Calculate(input, "index", CurrentCodeBlock) != "None" && int.Parse(Calculate(input, "index", CurrentCodeBlock)) < Value.Length && int.Parse(Calculate(input, "index", CurrentCodeBlock)) >= 0)
				{
					return Value[int.Parse(Calculate(input, "index", CurrentCodeBlock))].ToString();
				}
				else
				{
					Console.WriteLine("Exception: " + Value + " index out of range");
					CurrentCodeBlock.Error();
					return "None";
				}
			}
			else
			{
				return Value;
			}
		}
		public override void Compilation(CodeBlock CurrentCodeBlock)
		{
			Console.WriteLine("Name = " + Name);
			Console.WriteLine("Input = " + Input);
			if (Assignment)
			{
				for (int i = CurrentCodeBlock.AreaVariable.Count - 1; i >= 0; i--)
				{
					for (int j = 0; j < CurrentCodeBlock.AreaVariable[i].Count; j++)
					{
						if (CurrentCodeBlock.AreaVariable[i][j].Name == Name && CurrentCodeBlock.AreaVariable[i][j].Type == Type)
						{
							CurrentCodeBlock.AreaVariable[i][j].Value = Calculate(Input, Type, CurrentCodeBlock);
							i = -1;
							break;
						}
					}
					if (i == 0)
					{
						Console.WriteLine("Exception: " + Name + " is not found");
						CurrentCodeBlock.Error();
					}
				}
			}
			else
			{
				if (Input != "")
				{
					Value = Calculate(Input, Type, CurrentCodeBlock);
				}

				bool NewVariable = true;
				for (int i = 0; i < CurrentCodeBlock.AreaVariable[CurrentCodeBlock.AreaVariable.Count - 1].Count; i++)
				{
					if (CurrentCodeBlock.AreaVariable[CurrentCodeBlock.AreaVariable.Count - 1][i].Name == Name)
					{
						CurrentCodeBlock.AreaVariable[CurrentCodeBlock.AreaVariable.Count - 1][i] = this;
						NewVariable = false;
						break;
					}
				}
				if (NewVariable)
				{
					CurrentCodeBlock.AreaVariable[CurrentCodeBlock.AreaVariable.Count - 1].Add(this);
					Console.WriteLine("LOOOOOL " + (CurrentCodeBlock.AreaVariable.Count - 1));
				}
			}
		}
	}
}