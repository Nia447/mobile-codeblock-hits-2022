using System;
using System.Collections.Generic;

namespace Codeblock.Model
{
	public class LogicBlock : Node
	{
		public CodeBlock CurrentCodeBlock;
		public List<LogicObject> AreaLogicObjects;
		public LogicBlock(CodeBlock currentCodeBlock) : base()
		{
			CurrentCodeBlock = currentCodeBlock;
			AreaLogicObjects = new List<LogicObject>();
		}
		public override void Compilation(CodeBlock CurrentCodeBlock)
		{
			foreach(LogicObject CurrentLogicObject in AreaLogicObjects)
            {
				Console.WriteLine(CurrentLogicObject.Input);
				CurrentLogicObject.Compilation(CurrentCodeBlock);

				if (CurrentLogicObject.Boolean == "true") break;
				if (CurrentLogicObject.Commands.CompilationError)
                {
					CurrentCodeBlock.Error();
					break;
				}
            }
		}
		public void AddElseIf(LogicObject logicObject)
		{
			AreaLogicObjects.Add(logicObject);
		}

		public void AddElse(LogicObject logicObject)
		{
			AreaLogicObjects.Add(logicObject);
		}
	}
}