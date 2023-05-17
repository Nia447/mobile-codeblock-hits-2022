using System;
using System.Collections.Generic;

namespace Codeblock.Model
{
	public class WhileBlock : Node
	{
		LogicObject CurrentLogicObject;
		public WhileBlock(CodeBlock currentCodeBlock) : base()
        {
			CurrentLogicObject = new LogicObject(currentCodeBlock);
		}
		public override void Compilation(CodeBlock CurrentCodeBlock)
		{
			while (Calculate(CurrentLogicObject.Input, "bool", CurrentCodeBlock) == "true")
            {
				CurrentLogicObject.Commands.Compilation();
				if (CurrentLogicObject.Commands.CompilationError)
                {
					CurrentCodeBlock.Error();
					break;
				}
            }
		}
	}
}