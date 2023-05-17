using System;
using System.Collections.Generic;

namespace Codeblock.Model
{
	public class LogicObject : Node
	{
		public CodeBlock Commands;

		public string Input;
		public string Boolean;
		public LogicObject(CodeBlock CurrentCodeBlock, string input = "") : base()
		{
			Input = input;
			Commands = new CodeBlock(CurrentCodeBlock.AreaVariable, CurrentCodeBlock.AreaFunctions);
		}
		public override void Compilation(CodeBlock CurrentCodeBlock)
		{
			Boolean = Calculate(Input, "bool", CurrentCodeBlock);
            if (Boolean == "true" && !CurrentCodeBlock.CompilationError)
            {
                Commands.Compilation();
            }
        }
	}
}